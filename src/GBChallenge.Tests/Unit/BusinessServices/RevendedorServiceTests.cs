using GBChallenge.Core.BusinessServices;
using GBChallenge.Core.Domain.Entities;
using GBChallenge.Core.Domain.Interfaces;
using Moq;
using Moq.AutoMock;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GBChallenge.Tests.Unit.BusinessServices
{
    public class RevendedorServiceTests
    {
        Revendedor _revendedorMock;
        public RevendedorServiceTests()
        {
            _revendedorMock = new Revendedor
            {
                CPF = "57246534008",
                Email = "email@teste.com",
                Senha = "$Senha0002",
                Id = 999
            };
        }

        [Theory(DisplayName = "RevendedorService - Registrar - Dados Invalidos")]
        [InlineData("", "email@teste.com")]
        [InlineData("Cpf_Invalido", "email@teste.com")]
        [InlineData("572.465.340-01", "email@teste.com")]
        [InlineData("572.465.340-08", "")]
        [InlineData("572.465.340-08", "Email_Invalido")]
        public async Task RevendedorServiceTests_Registrar_DadosInvalidos(string cpf, string email)
        {

            //Arrange
            var autoMocker = new AutoMocker();
            var revendedorService = autoMocker.CreateInstance<RevendedorService>();

            var revendedor = new Revendedor
            {
                CPF = cpf,
                Email = email
            };

            //Act
            var retorno = await revendedorService.Adicionar(revendedor);

            //Assert
            Assert.False(retorno.Successo);
            Assert.NotNull(retorno.Messagem);
            Assert.True(retorno.CodigoRetorno >= 400);

        }

        [Fact(DisplayName = "RevendedorService - Registrar - Token Nao Gerado")]
        public async Task RevendedorServiceTests_Registrar_TokenNaoGerado()
        {

            //Arrange
            var autoMocker = new AutoMocker();
            var revendedorService = autoMocker.CreateInstance<RevendedorService>();


            var authService = autoMocker.GetMock<IAutenticacaoService>();
            authService.Setup((foo) => foo.Registrar(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new Core.Domain.Entities.Dto.TokenResponse("Erro", 400));

            //Act
            var retorno = await revendedorService.Adicionar(_revendedorMock);

            //Assert
            Assert.False(retorno.Successo);
            Assert.NotNull(retorno.Messagem);

        }

        [Fact(DisplayName = "RevendedorService - Registrar - Token Nao Gerado")]
        public async Task RevendedorServiceTests_Registrar_TokenGerado()
        {

            //Arrange
            var autoMocker = new AutoMocker();
            var revendedorService = autoMocker.CreateInstance<RevendedorService>();

            var authService = autoMocker.GetMock<IAutenticacaoService>();
            authService.Setup((foo) => foo.Registrar(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new Core.Domain.Entities.Dto.TokenResponse(new Token("TokenJwt", "20990101")));

            var revRepMock = autoMocker.GetMock<IRevendedorRepository>();
            revRepMock.Setup((foo) => foo.Inserir(It.IsAny<Revendedor>())).Verifiable();

            //Act
            var retorno = await revendedorService.Adicionar(_revendedorMock);

            //Assert
            Assert.True(retorno.Successo);
            Assert.Empty(retorno.Messagem);
            Assert.NotEmpty(retorno.Token.TokenJwt);
            Assert.True(retorno.CodigoRetorno == 201);

        }


        [Theory(DisplayName = "RevendedorService - Validar - Dados Invalidos")]
        [InlineData("")]
        [InlineData("Login_Invalido")]
        [InlineData("572.465.340-01")]
        public async Task RevendedorServiceTests_Validar_DadosInvalidos(string login)
        {
            //Arrange
            var autoMocker = new AutoMocker();
            var revendedorService = autoMocker.CreateInstance<RevendedorService>();

            //Act
            var retorno = await revendedorService.Validar(login, "$Senha009");

            //Assert
            Assert.False(retorno.Successo);
            Assert.NotNull(retorno.Messagem);
            Assert.True(retorno.CodigoRetorno >= 400);

        }

        [Fact(DisplayName = "RevendedorService - Validar - Revendedor Nao Encontrado")]
        public async Task RevendedorServiceTests_Registrar_RevendedorNaoEncontrado()
        {

            //Arrange
            var autoMocker = new AutoMocker();
            var revendedorService = autoMocker.CreateInstance<RevendedorService>();

            var revRepMock = autoMocker.GetMock<IRevendedorRepository>();
            revRepMock.Setup((foo) => foo.Buscar(_revendedorMock.CPF)).ReturnsAsync(new Revendedor { });

            //Act
            var retorno = await revendedorService.Validar(_revendedorMock.CPF, _revendedorMock.Senha);

            //Assert
            Assert.False(retorno.Successo);
            Assert.NotNull(retorno.Messagem);
            Assert.True(retorno.CodigoRetorno >= 400);

        }

        [Fact(DisplayName = "RevendedorService - Validar - Revendedor Encontrado")]
        public async Task RevendedorServiceTests_Registrar_RevendedorEncontrado()
        {

            //Arrange
            var autoMocker = new AutoMocker();
            var revendedorService = autoMocker.CreateInstance<RevendedorService>();


            var revRepMock = autoMocker.GetMock<IRevendedorRepository>();
            revRepMock.Setup((foo) => foo.Buscar(_revendedorMock.CPF)).ReturnsAsync(_revendedorMock);

            var authService = autoMocker.GetMock<IAutenticacaoService>();
            authService.Setup((foo) => foo.Autenticar(_revendedorMock.CPF, _revendedorMock.Senha)).ReturnsAsync(new Core.Domain.Entities.Dto.TokenResponse(new Token("TokenJwt", "20990101")));

            //Act
            var retorno = await revendedorService.Validar(_revendedorMock.CPF, _revendedorMock.Senha);

            //Assert
            Assert.True(retorno.Successo);
            Assert.Empty(retorno.Messagem);
            Assert.NotEmpty(retorno.Token.TokenJwt);
            Assert.True(retorno.CodigoRetorno == 200);

        }

        [Theory(DisplayName = "RevendedorService - Obter - Dados Invalidos")]
        [InlineData("")]
        [InlineData("Cpf_Invalido")]
        [InlineData("572.465.340-01")]
        public async Task RevendedorServiceTests_Obter_DadosInvalidos(string cpf)
        {
            //Arrange
            var autoMocker = new AutoMocker();
            var revendedorService = autoMocker.CreateInstance<RevendedorService>();

            //Act
            var retorno = await revendedorService.Obter(cpf);

            //Assert
            Assert.False(retorno.Successo);
            Assert.NotNull(retorno.Messagem);
            Assert.True(retorno.CodigoRetorno >= 400);

        }

        [Fact(DisplayName = "RevendedorService - Obter - Revendedor Aprovacao Automatica")]
        public async Task RevendedorServiceTests_Obter_Sucesso_AprovacaoAutomatica()
        {

            //Arrange
            var autoMocker = new AutoMocker();
            var revendedorService = autoMocker.CreateInstance<RevendedorService>();

            var revendedor = new Revendedor
            {
                CPF = "15350946056",
                Email = "email@teste.com",
                Senha = "$Senha0002"
            };

            var revRepMock = autoMocker.GetMock<IRevendedorRepository>();
            revRepMock.Setup((foo) => foo.Buscar(revendedor.CPF)).ReturnsAsync(revendedor);

            //Act
            var retorno = await revendedorService.Obter(revendedor.CPF);


            //Assert
            Assert.True(retorno.Successo);
            Assert.True(retorno.Revendedor.CompraAutoAprovada);
            Assert.Empty(retorno.Messagem);
            Assert.True(retorno.CodigoRetorno == 200);

        }

        [Fact(DisplayName = "RevendedorService - Obter - Revendedor Compra Validacao")]
        public async Task RevendedorServiceTests_Obter_Sucesso_CompraEmValidacao()
        {

            //Arrange
            var autoMocker = new AutoMocker();
            var revendedorService = autoMocker.CreateInstance<RevendedorService>();

            var revRepMock = autoMocker.GetMock<IRevendedorRepository>();
            revRepMock.Setup((foo) => foo.Buscar(_revendedorMock.CPF)).ReturnsAsync(_revendedorMock);

            //Act
            var retorno = await revendedorService.Obter(_revendedorMock.CPF);

            //Assert
            Assert.True(retorno.Successo);
            Assert.False(retorno.Revendedor.CompraAutoAprovada);
            Assert.Empty(retorno.Messagem);
            Assert.True(retorno.CodigoRetorno == 200);

        }

        [Theory(DisplayName = "RevendedorService - ObterAcumulado - Dados Invalidos")]
        [InlineData("")]
        [InlineData("Cpf_Invalido")]
        [InlineData("572.465.340-01")]
        public async Task RevendedorServiceTests_ObterAcumulado_DadosInvalidos(string cpf)
        {
            //Arrange
            var autoMocker = new AutoMocker();
            var revendedorService = autoMocker.CreateInstance<RevendedorService>();

            //Act
            var retorno = await revendedorService.ObterAcumulado(cpf);

            //Assert
            Assert.False(retorno.Successo);
            Assert.NotNull(retorno.Messagem);
            Assert.True(retorno.CodigoRetorno >= 400);

        }

        [Fact(DisplayName = "RevendedorService - ObterAcumulado - Nao Encontrado na Base")]
        public async Task RevendedorServiceTests_ObterAcumulado_NaoEncontradoBase()
        {
            //Arrange
            var autoMocker = new AutoMocker();
            var revendedorService = autoMocker.CreateInstance<RevendedorService>();

            var revRepMock = autoMocker.GetMock<IRevendedorRepository>();
            revRepMock.Setup((foo) => foo.Buscar(_revendedorMock.CPF)).ReturnsAsync(new Revendedor { });

            //Act
            var retorno = await revendedorService.ObterAcumulado(_revendedorMock.CPF);

            //Assert
            Assert.False(retorno.Successo);
            Assert.NotNull(retorno.Messagem);
            Assert.True(retorno.CodigoRetorno >= 400);

        }

        [Fact(DisplayName = "RevendedorService - ObterAcumulado - Nao Encontrado na Base")]
        public async Task RevendedorServiceTests_ObterAcumulado_NaoEncontradoAPI()
        {
            //Arrange
            var autoMocker = new AutoMocker();
            var revendedorService = autoMocker.CreateInstance<RevendedorService>();

            var revRepMock = autoMocker.GetMock<IRevendedorRepository>();
            revRepMock.Setup((foo) => foo.Buscar(_revendedorMock.CPF)).ReturnsAsync(_revendedorMock);

            var cbClientMock = autoMocker.GetMock<ICashBackClient>();
            cbClientMock.Setup((foo) => foo.ObterAcumulado(_revendedorMock.CPF)).ReturnsAsync(new Core.Domain.Entities.ClientResponses.ObterAcumuladoClientResponse());

            //Act
            Func<Task> retorno = () => revendedorService.ObterAcumulado(_revendedorMock.CPF);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(retorno);

        }


        [Fact(DisplayName = "RevendedorService - ObterAcumulado - Sucesso")]
        public async Task RevendedorServiceTests_ObterAcumulado_Sucesso()
        {
            //Arrange
            var autoMocker = new AutoMocker();
            var revendedorService = autoMocker.CreateInstance<RevendedorService>();

            var revRepMock = autoMocker.GetMock<IRevendedorRepository>();
            revRepMock.Setup((foo) => foo.Buscar(_revendedorMock.CPF)).ReturnsAsync(_revendedorMock);

            var creditoAcumulado = new Core.Domain.Entities.ClientResponses.ObterAcumuladoClientResponse()
            {
                Body = new Core.Domain.Entities.ClientResponses.ResponseBody
                {
                    Credit = 999
                }
            };

            var cbClientMock = autoMocker.GetMock<ICashBackClient>();
            cbClientMock.Setup((foo) => foo.ObterAcumulado(_revendedorMock.CPF)).ReturnsAsync(creditoAcumulado);

            //Act
            var retorno = await revendedorService.ObterAcumulado(_revendedorMock.CPF);

            //Assert
            Assert.True(retorno.Successo);
            Assert.True(retorno.Credito == 999);
            Assert.Empty(retorno.Messagem);
            Assert.True(retorno.CodigoRetorno == 200);

        }
    }
}
