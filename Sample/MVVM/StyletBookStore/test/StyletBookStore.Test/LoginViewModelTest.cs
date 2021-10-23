using System;
using System.Linq.Expressions;
using System.Windows;
using Moq;
using Shouldly;
using Stylet;
using StyletBookStore.Pages;
using StyletIoC;
using Xunit;

namespace StyletBookStore.Test
{
    public class LoginViewModelTest
    {
        private readonly IContainer _container;
        private readonly Mock<IWindowManager> _mockWindowManager;
        private readonly Mock<IChildDelegate> _mockChildDelegate;
        private readonly Expression<Func<IWindowManager, MessageBoxResult>> _showMessageBoxExpr = wm => wm.ShowMessageBox("�û��������벻��ȷ", "��¼ʧ��", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.None, MessageBoxResult.None, null, null, null);

        public LoginViewModelTest()
        {
            // ʹ��Moq����IWindowManager
            _mockWindowManager = new Mock<IWindowManager>();
            _mockWindowManager.Setup(_showMessageBoxExpr).Returns(MessageBoxResult.OK);

            // ʹ��Moq����IChildDelegate
            _mockChildDelegate = new Mock<IChildDelegate>();

            // ��Stylet��IoC��ע�����
            var builder = new StyletIoCBuilder();
            builder.Bind<LoginViewModel>().ToSelf();
            builder.Bind<IWindowManager>().ToInstance(_mockWindowManager.Object);    // ע��IWindowManager
            builder.Bind<IChildDelegate>().ToInstance(_mockChildDelegate.Object);    // ע��IChildDelegate
            _container = builder.BuildContainer();
        }

        #region ���Թ��ܵ�: ��"�û���"��"����"Ϊ��ʱ, �ǲ�������¼��("��¼"��ť���ڽ���״̬).

        /// <summary>
        /// ����δ����, �����������¼
        /// </summary>
        [Fact]
        public void CanLoginTest_NoPassword()
        {
            // Arrange
            var vm = _container.Get<LoginViewModel>();
            vm.UserName = "waku";
            vm.Password = String.Empty;

            // Act
            bool canLogin = vm.CanLogin;

            // Assert
            canLogin.ShouldBe(false);
        }

        /// <summary>
        /// �û���δ����, �����������¼
        /// </summary>
        [Fact]
        public void CanLoginTest_NoUserName()
        {
            // Arrange
            var vm = _container.Get<LoginViewModel>();
            vm.UserName = string.Empty;
            vm.Password = "123";

            // Act
            bool canLogin = vm.CanLogin;

            // Assert
            canLogin.ShouldBe(false);
        }

        /// <summary>
        /// �û��������붼������, ���������¼
        /// </summary>
        [Fact]
        public void CanLoginTest()
        {
            // Arrange
            var vm = _container.Get<LoginViewModel>();
            vm.UserName = "waku";
            vm.Password = "123";

            // Act
            bool canLogin = vm.CanLogin;

            // Assert
            canLogin.ShouldBe(true);
        }

        #endregion

        #region ���Թ��ܵ�: �û�������"waku", ������������"123", ���"��¼"��ť, ��¼���ڹر�, �ص�������.

        /// <summary>
        /// ��ȷ���û���������
        /// </summary>
        [Fact]
        public void LoginTest()
        {
            // Arrange
            var vm = _container.Get<LoginViewModel>();
            var childDelegate = _container.Get<IChildDelegate>();
            vm.UserName = "waku";
            vm.Password = "123";
            vm.Parent = childDelegate;

            // Act
            vm.Login();
            
            // Assert
            _mockWindowManager.Verify(_showMessageBoxExpr, Times.Never); // ��Ӧ����ʾ��Ϣ��
            _mockChildDelegate.Verify(cd => cd.CloseItem(vm, true), Times.Once);    // Ӧ�ùرմ���,������true
        }

        #endregion

        #region  ���Թ��ܵ�: �û��������벻��ȷ��ʾ"�û��������벻��ȷ"����Ϣ��.

        /// <summary>
        /// �û�������
        /// </summary>
        [Fact]
        public void LoginTest_WrongUserName()
        {
            // Arrange
            var vm = _container.Get<LoginViewModel>();
            vm.UserName = "wrong_username";
            vm.Password = "123";

            // Act
            vm.Login();

            // Assert
            _mockWindowManager.Verify(_showMessageBoxExpr, Times.Once); // Ӧ����ʾ��Ϣ��
        }

        /// <summary>
        /// �������
        /// </summary>
        [Fact]
        public void LoginTest_WrongPassword()
        {
            // Arrange
            var vm = _container.Get<LoginViewModel>();
            vm.UserName = "waku";
            vm.Password = "wrong_password";

            // Act
            vm.Login();

            // Assert
            _mockWindowManager.Verify(_showMessageBoxExpr, Times.Once); // Ӧ����ʾ��Ϣ��
        }

        #endregion
    }
}