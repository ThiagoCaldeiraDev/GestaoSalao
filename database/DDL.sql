CREATE TABLE [dbo].[Usuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
	Nome [varchar](max) NOT NULL,
	Cpf [varchar](14) NOT NULL,
	Senha [varchar](50) NOT NULL,
	Telefone [varchar](20) NOT NULL,
	Email [varchar](max) NOT NULL,
	Tipo int NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

ALTER TABLE [dbo].[Usuario] ADD  DEFAULT (newsequentialid()) FOR [Guid]

----------------------------

CREATE TABLE [dbo].[Produto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
	Nome [varchar](max) NOT NULL,
	Descricao [varchar](max) NOT NULL,
	Valor decimal(18,2) not null
 CONSTRAINT [PK_Produto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

ALTER TABLE [dbo].[Produto] ADD  DEFAULT (newsequentialid()) FOR [Guid]

----------------------------

CREATE TABLE [dbo].[Servico](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
	Nome [varchar](max) NOT NULL,
	Descricao [varchar](max) NOT NULL,
	Valor decimal(18,2) not null,
	Tempo datetime not null,
 CONSTRAINT [PK_Servico] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

ALTER TABLE [dbo].[Servico] ADD  DEFAULT (newsequentialid()) FOR [Guid]

----------------------------

CREATE TABLE [dbo].[Venda](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
	Tipo int not null,
	IdServico int null,
	IdProduto int null,
	Valor decimal(18,2) not null,
	Quantidade int not null,
	DataOperacao datetime not null,
	IdVendedor int not null,
 CONSTRAINT [PK_Venda] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

ALTER TABLE [dbo].Venda ADD  DEFAULT (newsequentialid()) FOR [Guid]

----------------------------

CREATE TABLE [dbo].[Agendamento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
	IdServico int not null,
	IdProfissional int not null,
	IdUsuario int not null,
	DataAgendamentoInicial datetime not null,
	DataAgendamentoFinal datetime not null,
 CONSTRAINT [PK_Agendamento] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

ALTER TABLE [dbo].Agendamento ADD  DEFAULT (newsequentialid()) FOR [Guid]

INSERT INTO Usuario VALUES(NEWID(), 'Admin', '111.111.111-11', '123', '11 99911-1234', '', 1);