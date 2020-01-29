using GBChallenge.Core.BusinessServices;
using GBChallenge.Core.Domain.Entities;
using GBChallenge.Core.Domain.Entities.Dto;
using GBChallenge.Core.Domain.Interfaces;
using GBChallenge.Domain.SimpleTypes;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GBChallenge.Tests.Unit.BusinessServices
{
    public class CompraServiceTests
    {
        private Compra _compraMock;
        private RevendedorDto _revendedorMock;
        public CompraServiceTests()
        {
            _compraMock = new Compra()
            {
                Codigo = "XBTO800900",
                Data = DateTime.Now,
                IdRevendedor = 999,
                Id = 888,
                Valor = 1000
            };

            _revendedorMock = new RevendedorDto
            {
                CPF = "57246534008",
                Email = "email@teste.com",
                Id = 999
            };
        }

        [Fact(DisplayName ="CompraService - Adicionar - Cpf - Invalido")]
        public async Task CompraService_Adicionar_Cpf_Invalido()
        {
            //Arrange
            var autoMocker = new AutoMocker();
            var compraService = autoMocker.CreateInstance<CompraService>();

            var revService = autoMocker.GetMock<IRevendedorService>();
            revService.Setup((foo) => foo.Obter("")).ReturnsAsync(new Core.Domain.Entities.Dto.ObterRevendedorResponse("ERRO", 400));

            //Act
            var retorno = await compraService.Adicionar(_compraMock, "","");

            //Assert
            Assert.False(retorno.Successo);
            Assert.NotNull(retorno.Messagem);
            Assert.NotEmpty(retorno.Messagem);
            Assert.True(retorno.CodigoRetorno == 400);
        }


        [Fact(DisplayName = "CompraService - Adicionar - Erro - Analogia")]
        public async Task CompraService_Adicionar_Erro_Analogia()
        {
            //Arrange
            var autoMocker = new AutoMocker();
            var compraService = autoMocker.CreateInstance<CompraService>();

            var revService = autoMocker.GetMock<IRevendedorService>();
            revService.Setup((foo) => foo.Obter("")).ReturnsAsync(new Core.Domain.Entities.Dto.ObterRevendedorResponse(_revendedorMock));
            revService.Setup((foo) => foo.ValidarAnalogia("", _revendedorMock.Id)).ReturnsAsync(false);

            //Act
            var retorno = await compraService.Adicionar(_compraMock, "", "");

            //Assert
            Assert.False(retorno.Successo);
            Assert.NotNull(retorno.Messagem);
            Assert.NotEmpty(retorno.Messagem);
            Assert.True(retorno.CodigoRetorno == 400);
        }

        [Fact(DisplayName = "CompraService - Adicionar - Sucesso")]
        public async Task CompraService_Adicionar_Sucesso()
        {
            //Arrange
            var autoMocker = new AutoMocker();
            var compraService = autoMocker.CreateInstance<CompraService>();

            var revService = autoMocker.GetMock<IRevendedorService>();
            revService.Setup((foo) => foo.Obter("")).ReturnsAsync(new Core.Domain.Entities.Dto.ObterRevendedorResponse(_revendedorMock));
            revService.Setup((foo) => foo.ValidarAnalogia("", _revendedorMock.Id)).ReturnsAsync(true);

            var compraRep = autoMocker.GetMock<ICompraRepository>();
            compraRep.Setup((foo) => foo.Inserir(_compraMock)).ReturnsAsync(_compraMock.Id);

            //Act
            var retorno = await compraService.Adicionar(_compraMock, "", "");

            //Assert
            Assert.True(retorno.Successo);
            Assert.Empty(retorno.Messagem);
            Assert.True(retorno.CodigoRetorno == 201);
        }

        [Fact(DisplayName = "CompraService - Atualizar - Compra - Null")]
        public async Task CompraService_Atualizar_Compra_Null()
        {
            //Arrange
            var autoMocker = new AutoMocker();
            var compraService = autoMocker.CreateInstance<CompraService>();

            //Act
            var retorno = await compraService.Atualizar(null, "");

            //Assert
            Assert.False(retorno.Successo);
            Assert.NotNull(retorno.Messagem);
            Assert.NotEmpty(retorno.Messagem);
            Assert.True(retorno.CodigoRetorno == 400);
        }

        [Fact(DisplayName = "CompraService - Atualizar - Compra - Nao Encontrada")]
        public async Task CompraService_Atualizar_Compra_Nao_Encontrada()
        {
            //Arrange
            var autoMocker = new AutoMocker();
            var compraService = autoMocker.CreateInstance<CompraService>();

            var compraRep = autoMocker.GetMock<ICompraRepository>();
            compraRep.Setup((foo) => foo.Obter(_compraMock.Id)).ReturnsAsync((Compra) null);

            //Act
            var retorno = await compraService.Atualizar(_compraMock, "");

            //Assert
            Assert.False(retorno.Successo);
            Assert.NotNull(retorno.Messagem);
            Assert.NotEmpty(retorno.Messagem);
            Assert.True(retorno.CodigoRetorno == 404);
        }

        [Fact(DisplayName = "CompraService - Atualizar - Compra - Erro Analogia")]
        public async Task CompraService_Atualizar_Compra_Erro_Analogia()
        {
            //Arrange
            var autoMocker = new AutoMocker();
            var compraService = autoMocker.CreateInstance<CompraService>();

            var compraRep = autoMocker.GetMock<ICompraRepository>();
            compraRep.Setup((foo) => foo.Obter(_compraMock.Id)).ReturnsAsync(_compraMock);

            var revService = autoMocker.GetMock<IRevendedorService>();
            revService.Setup((foo) => foo.ValidarAnalogia("", _revendedorMock.Id)).ReturnsAsync(false);

            //Act
            var retorno = await compraService.Atualizar(_compraMock, "");

            //Assert
            Assert.False(retorno.Successo);
            Assert.NotNull(retorno.Messagem);
            Assert.NotEmpty(retorno.Messagem);
            Assert.True(retorno.CodigoRetorno == 400);
        }

        [Fact(DisplayName = "CompraService - Atualizar - Compra - Aprovada")]
        public async Task CompraService_Atualizar_Compra_Erro_CompraAprovada()
        {
            //Arrange
            var autoMocker = new AutoMocker();
            var compraService = autoMocker.CreateInstance<CompraService>();

            var compraRep = autoMocker.GetMock<ICompraRepository>();
            compraRep.Setup((foo) => foo.Obter(_compraMock.Id)).ReturnsAsync(_compraMock);

            var revService = autoMocker.GetMock<IRevendedorService>();
            revService.Setup((foo) => foo.ValidarAnalogia("", _revendedorMock.Id)).ReturnsAsync(true);

            _compraMock.Status = StatusCompra.Aprovado;

            //Act
            var retorno = await compraService.Atualizar(_compraMock, "");

            //Assert
            Assert.False(retorno.Successo);
            Assert.NotNull(retorno.Messagem);
            Assert.NotEmpty(retorno.Messagem);
            Assert.True(retorno.CodigoRetorno == 400);
        }

        [Fact(DisplayName = "CompraService - Atualizar - Sucesso")]
        public async Task CompraService_Atualizar_Sucesso()
        {
            //Arrange
            var autoMocker = new AutoMocker();
            var compraService = autoMocker.CreateInstance<CompraService>();

            var compraRep = autoMocker.GetMock<ICompraRepository>();
            compraRep.Setup((foo) => foo.Obter(_compraMock.Id)).ReturnsAsync(_compraMock);

            var revService = autoMocker.GetMock<IRevendedorService>();
            revService.Setup((foo) => foo.ValidarAnalogia("", _revendedorMock.Id)).ReturnsAsync(true);

            _compraMock.Status = StatusCompra.EmValidacao;

            //Act
            var retorno = await compraService.Atualizar(_compraMock, "");

            //Assert
            Assert.True(retorno.Successo);
            Assert.Empty(retorno.Messagem);
            Assert.True(retorno.CodigoRetorno == 200);
        }


        [Fact(DisplayName = "CompraService - Excluir - IdCompra - Invalido")]
        public async Task CompraService_Excluir_IdCompra_Invalido()
        {
            //Arrange
            var autoMocker = new AutoMocker();
            var compraService = autoMocker.CreateInstance<CompraService>();

            //Act
            var retorno = await compraService.Excluir(0, "");

            //Assert
            Assert.False(retorno.Successo);
            Assert.NotNull(retorno.Messagem);
            Assert.NotEmpty(retorno.Messagem);
            Assert.True(retorno.CodigoRetorno == 400);
        }


        [Fact(DisplayName = "CompraService - Excluir - Nao Encontrada")]
        public async Task CompraService_Excluir_NaoEncontrada()
        {
            //Arrange
            var autoMocker = new AutoMocker();
            var compraService = autoMocker.CreateInstance<CompraService>();

            var compraRep = autoMocker.GetMock<ICompraRepository>();
            compraRep.Setup((foo) => foo.Obter(_compraMock.Id)).ReturnsAsync((Compra)null);

            //Act
            var retorno = await compraService.Excluir(_compraMock.Id, "");

            //Assert
            Assert.False(retorno.Successo);
            Assert.NotNull(retorno.Messagem);
            Assert.NotEmpty(retorno.Messagem);
            Assert.True(retorno.CodigoRetorno == 404);
        }

        [Fact(DisplayName = "CompraService - Excluir - Erro Analogia")]
        public async Task CompraService_Excluir_ErroAnalogia()
        {
            //Arrange
            var autoMocker = new AutoMocker();
            var compraService = autoMocker.CreateInstance<CompraService>();

            var compraRep = autoMocker.GetMock<ICompraRepository>();
            compraRep.Setup((foo) => foo.Obter(_compraMock.Id)).ReturnsAsync(_compraMock);

            var revService = autoMocker.GetMock<IRevendedorService>();
            revService.Setup((foo) => foo.ValidarAnalogia("", _revendedorMock.Id)).ReturnsAsync(false);

            //Act
            var retorno = await compraService.Excluir(_compraMock.Id, "");

            //Assert
            Assert.False(retorno.Successo);
            Assert.NotNull(retorno.Messagem);
            Assert.NotEmpty(retorno.Messagem);
            Assert.True(retorno.CodigoRetorno == 400);
        }

        [Fact(DisplayName = "CompraService - Excluir - Sucesso")]
        public async Task CompraService_Excluir_Sucesso()
        {
            //Arrange
            var autoMocker = new AutoMocker();
            var compraService = autoMocker.CreateInstance<CompraService>();

            var compraRep = autoMocker.GetMock<ICompraRepository>();
            compraRep.Setup((foo) => foo.Obter(_compraMock.Id)).ReturnsAsync(_compraMock);
            compraRep.Setup((foo) => foo.Excluir(_compraMock)).Verifiable();

            var revService = autoMocker.GetMock<IRevendedorService>();
            revService.Setup((foo) => foo.ValidarAnalogia("", _revendedorMock.Id)).ReturnsAsync(true);

            //Act
            var retorno = await compraService.Excluir(_compraMock.Id, "");

            //Assert
            Assert.True(retorno.Successo);
            Assert.Empty(retorno.Messagem);
            Assert.True(retorno.CodigoRetorno == 200);
        }

        [Fact(DisplayName = "CompraService - Listar - Revendedor Não Encontrado")]
        public async Task CompraService_Listar_RevendedorNaoEncontrado()
        {
            //Arrange
            var autoMocker = new AutoMocker();
            var compraService = autoMocker.CreateInstance<CompraService>();

            var revService = autoMocker.GetMock<IRevendedorService>();
            revService.Setup((foo) => foo.Obter(_revendedorMock.CPF)).ReturnsAsync(new ObterRevendedorResponse("Erro", 404));

            //Act
            var retorno = await compraService.Listar(_revendedorMock.CPF);

            //Assert
            Assert.False(retorno.Successo);
            Assert.NotNull(retorno.Messagem);
            Assert.NotEmpty(retorno.Messagem);
            Assert.True(retorno.CodigoRetorno == 404);
        }

        [Fact(DisplayName = "CompraService - Listar - Sucesso")]
        public async Task CompraService_Listar_Sucesso()
        {
            //Arrange
            var autoMocker = new AutoMocker();
            var compraService = autoMocker.CreateInstance<CompraService>();

            var revService = autoMocker.GetMock<IRevendedorService>();
            revService.Setup((foo) => foo.Obter(_revendedorMock.CPF)).ReturnsAsync(new ObterRevendedorResponse(_revendedorMock));

            var compraRep = autoMocker.GetMock<ICompraRepository>();
            compraRep.Setup((foo) => foo.Listar(_revendedorMock.Id)).ReturnsAsync(new List<Compra>());

            //Act
            var retorno = await compraService.Listar(_revendedorMock.CPF);

            //Assert
            Assert.True(retorno.Successo);
            Assert.Empty(retorno.Messagem);
            Assert.True(retorno.CodigoRetorno == 200);
        }
    }
}
