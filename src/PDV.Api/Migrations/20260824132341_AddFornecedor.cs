using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PDV.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddFornecedor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Agencia",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bairro",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(80)",
                maxLength: 80,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Banco",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Celular",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cep",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(8)",
                maxLength: 8,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(80)",
                maxLength: 80,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CnaePrincipal",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodigoMunicipioIbge",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(7)",
                maxLength: 7,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Complemento",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CondicaoPagamento",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Conta",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContatoComercial",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                schema: "pdv",
                table: "Fornecedores",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentoTitularConta",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailComercial",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailFinanceiro",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InscricaoEstadual",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InscricaoEstadualIsento",
                schema: "pdv",
                table: "Fornecedores",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "InscricaoMunicipal",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LimiteCredito",
                schema: "pdv",
                table: "Fornecedores",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logradouro",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(180)",
                maxLength: 180,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeFantasia",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Numero",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ObservacaoComercial",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ObservacaoFinanceira",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ObservacaoFiscal",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observacoes",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pix",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrazoPagamento",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(80)",
                maxLength: 80,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegimeTributario",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Responsavel",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefone2",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoConta",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoPessoa",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitularConta",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Uf",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(2)",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VendedorResponsavel",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedores_EmpresaId_Codigo",
                schema: "pdv",
                table: "Fornecedores",
                columns: new[] { "EmpresaId", "Codigo" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Fornecedores_EmpresaId_Codigo",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "Agencia",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "Bairro",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "Banco",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "Celular",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "Cep",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "Cidade",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "CnaePrincipal",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "Codigo",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "CodigoMunicipioIbge",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "Complemento",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "CondicaoPagamento",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "Conta",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "ContatoComercial",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "DocumentoTitularConta",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "EmailComercial",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "EmailFinanceiro",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "InscricaoEstadual",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "InscricaoEstadualIsento",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "InscricaoMunicipal",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "LimiteCredito",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "Logradouro",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "NomeFantasia",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "Numero",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "ObservacaoComercial",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "ObservacaoFinanceira",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "ObservacaoFiscal",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "Observacoes",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "Pix",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "PrazoPagamento",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "RegimeTributario",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "Responsavel",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "Telefone2",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "TipoConta",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "TipoPessoa",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "TitularConta",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "Uf",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "VendedorResponsavel",
                schema: "pdv",
                table: "Fornecedores");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "pdv",
                table: "Fornecedores",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150,
                oldNullable: true);
        }
    }
}
