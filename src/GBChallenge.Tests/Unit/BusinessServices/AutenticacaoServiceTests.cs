using GBChallenge.Core.BusinessServices;
using GBChallenge.Core.Domain.Entities.Settings;
using GBChallenge.Tests.Mocks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace GBChallenge.Tests.Unit.BusinessServices
{
    public class AutenticacaoServiceTests
    {
        Mock<FakeUserManager> _userManagerMock;
        Mock<FakeSignInManager> _signInManagerMock;
        Mock<IOptions<GBChallengeSettings>> _settingsMock;

        public AutenticacaoServiceTests()
        {
            _userManagerMock = new Mock<FakeUserManager>();
            _signInManagerMock = new Mock<FakeSignInManager>();
            _settingsMock = new Mock<IOptions<GBChallengeSettings>>();

            var settings = new GBChallengeSettings
            {
                TokenSettings = new TokenSettings
                {
                    ChaveAPI = "6a204bd89f3c8348afd5c77c717a097a",
                    Emissor = "TESTE",
                    ExpiracaoMinutos = 5,
                    ValidoEm = "https://localhost"
                }
            };

            _settingsMock.SetupGet(c => c.Value).Returns(settings);

        }

        [Fact(DisplayName = "AutenticacaoService - Registro - Valido")]
        public async Task AutenticacaoService_Registrar_DeveRetornarValidoAsync()
        {
            //Arrange
            _userManagerMock.Setup((foo) => foo.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            var autService = new AutenticacaoService(_signInManagerMock.Object, _userManagerMock.Object, _settingsMock.Object);

            //Act
            var retorno = await autService.Registrar("teste", "teste@teste", "teste");

            //Assert
            Assert.True(retorno.Successo);
            Assert.NotNull(retorno.Token);

        }

        [Theory(DisplayName = "AutenticacaoService - Registrar - Campos Nulos")]
        [InlineData("", "teste@teste", "teste")]
        [InlineData("teste", "", "teste")]
        [InlineData("teste", "teste", "")]
        public async Task AutenticacaoService_Registrar_DeveRetornarCampoNuloAsync(string cpf, string email, string senha)
        {
            var autService = new AutenticacaoService(_signInManagerMock.Object, _userManagerMock.Object, _settingsMock.Object);

            //Act
            var retorno = await autService.Registrar(cpf, email, senha);

            //Assert
            Assert.False(retorno.Successo);
            Assert.Null(retorno.Token);

        }

        [Fact(DisplayName = "AutenticacaoService - Registro - Invalido")]
        public async Task AutenticacaoService_Registrar_DeveRetornarInvalidoAsync()
        {
            //Arrange
            _userManagerMock.Setup((foo) => foo.CreateAsync(It.IsAny<IdentityUser>(), "123456")).ReturnsAsync(new IdentityResult());

            var autService = new AutenticacaoService(_signInManagerMock.Object, _userManagerMock.Object, _settingsMock.Object);

            //Act
            var retorno = await autService.Registrar("teste", "teste@teste", "123456");

            //Assert
            Assert.False(retorno.Successo);
            Assert.Null(retorno.Token);

        }


        [Fact(DisplayName = "AutenticacaoService - Registro - Valido")]
        public async Task AutenticacaoService_Autenticar_DeveRetornarValidoAsync()
        {
            //Arrange
            _signInManagerMock.Setup((foo) => foo.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), false, false)).ReturnsAsync(SignInResult.Success);

            var autService = new AutenticacaoService(_signInManagerMock.Object, _userManagerMock.Object, _settingsMock.Object);

            //Act
            var retorno = await autService.Autenticar("teste", "teste@teste");

            //Assert
            Assert.True(retorno.Successo);
            Assert.NotNull(retorno.Token);

        }

        [Theory(DisplayName = "AutenticacaoService - Autenticar - Campos Nulos")]
        [InlineData("teste", "")]
        [InlineData("", "teste")]
        public async Task AutenticacaoService_Autenticar_DeveRetornarCampoNuloAsync(string cpf, string senha)
        {
            var autService = new AutenticacaoService(_signInManagerMock.Object, _userManagerMock.Object, _settingsMock.Object);

            //Act
            var retorno = await autService.Autenticar(cpf, senha);

            //Assert
            Assert.False(retorno.Successo);
            Assert.Null(retorno.Token);

        }

        [Fact(DisplayName = "AutenticacaoService - Registro - Invalido")]
        public async Task AutenticacaoService_Autenticar_DeveRetornarInvalidoAsync()
        {
            //Arrange
            _signInManagerMock.Setup((foo) => foo.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), false, false)).ReturnsAsync(new SignInResult());

            var autService = new AutenticacaoService(_signInManagerMock.Object, _userManagerMock.Object, _settingsMock.Object);

            //Act
            var retorno = await autService.Autenticar("teste", "teste@teste");

            //Assert
            Assert.False(retorno.Successo);
            Assert.Null(retorno.Token);

        }
    }
}
