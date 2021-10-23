using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OxyplotTest
{
    class PlotViewModel
    {
        /// <summary>
        /// 画直线
        /// </summary>
        public PlotModel SimplePlotModel { get; set; }

        //每条线对应一个队列用作实时数据统计
        private ConcurrentQueue<int> queueSend = new ConcurrentQueue<int>();
        private ConcurrentQueue<int> queueUnsend = new ConcurrentQueue<int>();
        private ConcurrentQueue<int> queueConfirm = new ConcurrentQueue<int>();
        private ConcurrentQueue<int> queueDeal = new ConcurrentQueue<int>();

        public void addSendCount(int iCount)
        {

            queueSend.Enqueue(iCount);
        }

        public void addUnsendCount(int iCount)
        {

            queueUnsend.Enqueue(iCount);
        }

        public void addConfirmCount(int iCount)
        {

            queueConfirm.Enqueue(iCount);
        }

        public void addDealCount(int iCount)
        {

            queueDeal.Enqueue(iCount);
        }

        public int getSendCount()
        {

            int iSingle = 0;
            int iTotal = 0;
            while (queueSend.TryDequeue(out iSingle))
            {

                iTotal += iSingle;
            }

            return iTotal;
        }

        public int getUnsendCount()
        {

            int iSingle = 0;
            int iTotal = 0;
            while (queueUnsend.TryDequeue(out iSingle))
            {

                iTotal += iSingle;
            }

            return iTotal;
        }

        public int getConfirmCount()
        {

            int iSingle = 0;
            int iTotal = 0;
            while (queueConfirm.TryDequeue(out iSingle))
            {

                iTotal += iSingle;
            }

            return iTotal;
        }

        public int getDealCount()
        {

            int iSingle = 0;
            int iTotal = 0;
            while (queueDeal.TryDequeue(out iSingle))
            {

                iTotal += iSingle;
            }

            return iTotal;
        }

        public PlotViewModel()
        {

            SimplePlotModel = new PlotModel();

            //创建于建立初始化数据节点
            var lineSend = new LineSeries() { Title = "报送" };
            lineSend.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Now), 0));
            SimplePlotModel.Series.Add(lineSend);

            var lineUnsend = new LineSeries() { Title = "待报送" };
            lineUnsend.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Now), 0));
            SimplePlotModel.Series.Add(lineUnsend);

            var lineConfirm = new LineSeries() { Title = "确认" };
            lineConfirm.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Now), 0));
            SimplePlotModel.Series.Add(lineConfirm);

            var lineDeal = new LineSeries() { Title = "成交" };
            lineDeal.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Now), 0));
            SimplePlotModel.Series.Add(lineDeal);

            //定义y轴
            LinearAxis leftAxis = new LinearAxis()
            {

                Position = AxisPosition.Left,
                Minimum = 0,
                Maximum = 100,
                Title = "笔数",//显示标题内容
                TitlePosition = 0,//显示标题位置
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.None,
            };

            //定义x轴 报盘监控界面x轴统一为时间
            DateTimeAxis bottomAxis = new DateTimeAxis()
            {

                Position = AxisPosition.Bottom,
                StringFormat = "hh:mm:ss",
                Minimum = DateTimeAxis.ToDouble(DateTime.Now),
                Maximum = DateTimeAxis.ToDouble(DateTime.Now.AddMinutes(1)),
                Title = "时间",
                TitlePosition = 0,
                IntervalLength = 60,
                MinorIntervalType = DateTimeIntervalType.Seconds,
                IntervalType = DateTimeIntervalType.Seconds,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.None,
            };

            SimplePlotModel.Axes.Add(leftAxis);
            SimplePlotModel.Axes.Add(bottomAxis);

            bool bToMove = false;
            Task.Factory.StartNew(() =>
            {

                while (true)
                {

                    lineSend.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Now), getSendCount()));
                    lineUnsend.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Now), getUnsendCount()));
                    lineConfirm.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Now), getConfirmCount()));
                    lineDeal.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Now), getDealCount()));

                    if (!bToMove)
                    {

                        //当前时间减去起始时间达到30秒后开始左移时间轴
                        TimeSpan timeSpan = DateTime.Now - DateTimeAxis.ToDateTime(bottomAxis.Minimum);
                        if (timeSpan.TotalSeconds >= 30)
                        {

                            bToMove = true;
                        }
                    }
                    else
                    {

                        //左移时间轴，跨度维持在60秒
                        bottomAxis.Minimum = DateTimeAxis.ToDouble(DateTime.Now.AddSeconds(-30));
                        bottomAxis.Maximum = DateTimeAxis.ToDouble(DateTime.Now.AddSeconds(30));

                        //删除历史节点，防止DataPoint过多影响效率，也防止出现内存泄漏                        
                        lineSend.Points.RemoveAt(0);
                        lineConfirm.Points.RemoveAt(0);
                        lineUnsend.Points.RemoveAt(0);
                        lineDeal.Points.RemoveAt(0);
                    }

                    //根据报单笔数判断是否需要更新y轴刻度                    

                    //首先找出四条统计线中当前最大的节点
                    double iMax = lineSend.MaxY;
                    if (iMax < lineConfirm.MaxY)
                    {
                        iMax = lineConfirm.MaxY;
                    }
                    if (iMax < lineUnsend.MaxY)
                    {
                        iMax = lineUnsend.MaxY;
                    }
                    if (iMax < lineDeal.MaxY)
                    {
                        iMax = lineDeal.MaxY;
                    }

                    //如果当前的y轴最大刻度小于数据集中的最大值，放大
                    leftAxis.Maximum = iMax + (100 - iMax % 100);
                    leftAxis.IntervalLength = leftAxis.Maximum / 5;

                    //每秒刷新一次视图                                       
                    SimplePlotModel.InvalidatePlot(true);
                    Thread.Sleep(1000);
                }
            });
        }
    }
}
