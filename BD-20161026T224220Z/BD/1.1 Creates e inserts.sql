CREATE DATABASE PRJ_BATALHA_NAVAL
GO
--
USE PRJ_BATALHA_NAVAL
GO
--
--

-- INICIO LOGIN ADM

CREATE TABLE [dbo].[UserProfile] (
    [UserId]   INT            IDENTITY (1, 1) NOT NULL,
    [UserName] NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC)
);

CREATE TABLE [dbo].[webpages_Membership] (
    [UserId]                                  INT            NOT NULL,
    [CreateDate]                              DATETIME       NULL,
    [ConfirmationToken]                       NVARCHAR (128) NULL,
    [IsConfirmed]                             BIT            DEFAULT ((0)) NULL,
    [LastPasswordFailureDate]                 DATETIME       NULL,
    [PasswordFailuresSinceLastSuccess]        INT            DEFAULT ((0)) NOT NULL,
    [Password]                                NVARCHAR (128) NOT NULL,
    [PasswordChangedDate]                     DATETIME       NULL,
    [PasswordSalt]                            NVARCHAR (128) NOT NULL,
    [PasswordVerificationToken]               NVARCHAR (128) NULL,
    [PasswordVerificationTokenExpirationDate] DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC)
);

CREATE TABLE [dbo].[webpages_OAuthMembership] (
    [Provider]       NVARCHAR (30)  NOT NULL,
    [ProviderUserId] NVARCHAR (100) NOT NULL,
    [UserId]         INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Provider] ASC, [ProviderUserId] ASC)
);

CREATE TABLE [dbo].[webpages_Roles] (
    [RoleId]   INT            IDENTITY (1, 1) NOT NULL,
    [RoleName] NVARCHAR (256) NOT NULL,
    PRIMARY KEY CLUSTERED ([RoleId] ASC),
    UNIQUE NONCLUSTERED ([RoleName] ASC)
);

CREATE TABLE [dbo].[webpages_UsersInRoles] (
    [UserId] INT NOT NULL,
    [RoleId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [fk_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserProfile] ([UserId]),
    CONSTRAINT [fk_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[webpages_Roles] ([RoleId])
);


-- FIM LOGIN AMD

CREATE TABLE TB_GAME_SETTINGS (
	 ID_CONFIGURACAO			INT NOT NULL
	,DS_NOME_CONFIGURACAO		VARCHAR(20) NOT NULL	-- Nome que ir� aparecer na hora de selecionar o tipo de jogo
	,QT_COLUNAS					TINYINT					-- Quantidade de colunas
	,QT_LINHAS					TINYINT					-- Quantidade de linhas
	,QT_NAVIO1					TINYINT					-- Quantidade de navios 1x1
	,QT_NAVIO2					TINYINT					-- Quantidade de navios 1x2
	,QT_NAVIO3					TINYINT					-- Quantidade de navios 1x3
	,QT_NAVIO4					TINYINT					-- Quantidade de navios 1x4
	,QT_NAVIO5					TINYINT					-- Quantidade de navios 1x5
	,NR_TEMPO_POSICIONAMENTO	SMALLINT				-- Tempo em segundos para o jogador posicionar os navios antes do in�cio do jogo
	,NR_TEMPO_JOGADA			TINYINT					-- Tempo em segundos para o jogador realizar um ataque ao advers�rio
	,CONSTRAINT TB_GAME_SETTINGS_PK PRIMARY KEY (ID_CONFIGURACAO)
)
--
INSERT INTO TB_GAME_SETTINGS VALUES
 (1, 'F�cil', 10, 10, 0, 1, 2, 1, 1, 10, 120)
,(2, 'M�dio', 12, 12, 0, 1, 2, 1, 1, 10, 120)
,(3, 'Dif�cil', 15, 15, 0, 1, 2, 1, 1, 10, 120)
--
--
CREATE TABLE TB_USUARIOS (
	 ID_USUARIO					INT IDENTITY(1, 1) NOT NULL
	,DS_LOGIN					VARCHAR(20) NOT NULL	-- Login do jogador
	,DS_SENHA					VARCHAR(20) NOT NULL	-- Senha do jogador
	,QT_MOEDAS_DISPONIVEIS		INT						/* Quantidade de moedas dispon�veis atualmente: sempre que ele comprar ou ganhar moedas, 
															� atualizada esta quantidade e quando ele as usa, diminui */
	,ID_SKIN_TEMA				TINYINT					-- Id do skin de tema escolhido por jogador
	,ID_SKIN_AUDIO				TINYINT					-- Id do skin de �udio escolhido pelo jogador
	,CONSTRAINT TB_USERS_PK PRIMARY KEY (ID_USUARIO)
)
--
INSERT INTO TB_USUARIOS (DS_LOGIN, DS_SENHA, QT_MOEDAS_DISPONIVEIS, ID_SKIN_TEMA, ID_SKIN_AUDIO) VALUES
 ('victor', 'victor123', 0, 1, 1)
--
--
CREATE TABLE TB_PARTIDAS (
	 ID_PARTIDA					BIGINT IDENTITY(1, 1) NOT NULL
	,DT_INICIO					DATETIME				-- Data/hora do in�cio da partida
	,DT_FIM						DATETIME				-- Data/hora do t�rmino da partida
	,NR_PLAYER_VENCEDOR			TINYINT					-- N�mero do Player que ganhou: ou Player 1 ou 2
	,CONSTRAINT TB_PARTIDAS_PK PRIMARY KEY (ID_PARTIDA)
)
--
--
CREATE TABLE TB_JOGADAS (
	 ID_JOGADA					BIGINT IDENTITY(1, 1) NOT NULL
	,ID_PARTIDA					BIGINT NOT NULL		-- Id da partida
	,NR_PLAYER					TINYINT				-- N�mero do Player: 1 ou 2
	,ID_USUARIO					INT					-- Id do usu�rio
	,QT_NAVIOS_ACERTADOS		TINYINT				-- Quantidade de navios acertados por este jogador nesta partida
	,QT_PORDERES_USADOS			TINYINT				-- Quantidade de poderes utilizados por este jogador nesta partida
	,CONSTRAINT TB_JOGADAS_PK PRIMARY KEY (ID_JOGADA)
)
--
--
CREATE TABLE TB_TIPO_SKINS (
	 ID_TIPO_SKIN		TINYINT NOT NULL
	,DS_TIPO_SKIN		VARCHAR(20)
	,CONSTRAINT TB_TIPO_SKINS_PK PRIMARY KEY (ID_TIPO_SKIN)
)
--
INSERT INTO TB_TIPO_SKINS VALUES
 (1, 'Tema')
,(2, '�udio')
--
--
CREATE TABLE TB_INVENTARIO (
	 ID_INVENTARIO				BIGINT IDENTITY(1, 1) NOT NULL
	,ID_USUARIO					INT NOT NULL		-- Id do usu�rio
	,ID_TIPO_SKIN				TINYINT				-- Id do tipo de skin
	,ID_SKIN_DISPONIVEL			INT					-- Id do usu�rio
	,CONSTRAINT TB_INVENTARIO_PK PRIMARY KEY (ID_INVENTARIO)
)
--
INSERT INTO TB_INVENTARIO (ID_USUARIO, ID_TIPO_SKIN, ID_SKIN_DISPONIVEL) VALUES
 (1, 1, 1)
,(1, 2, 2)
,(1, 2, 3)
--
--
CREATE TABLE TB_SKINS_TEMA (
	 ID_SKIN_TEMA		INT
	,DS_NOME_SKIN_TEMA	VARCHAR(30)
	,DS_DESCRICAO_TEMA	VARCHAR(200)
	,CONSTRAINT TB_SKINS_TEMA_PK PRIMARY KEY (ID_SKIN_TEMA)
)
--
INSERT INTO TB_SKINS_TEMA VALUES
 (1, 'Padr�o', 'Skin padr�o.')
,(2, 'Espacial', 'Skin espacial.')
--
--
CREATE TABLE TB_SKINS_AUDIO (
	 ID_SKIN_AUDIO		INT
	,DS_NOME_SKIN_AUDIO	VARCHAR(30)
	,DS_DESCRICAO_AUDIO	VARCHAR(200)
	,CONSTRAINT TB_SKINS_AUDIO_PK PRIMARY KEY (ID_SKIN_AUDIO)
)
--
INSERT INTO TB_SKINS_AUDIO VALUES
 (1, 'Padr�o', '�udio padr�o.')
,(2, 'Espacial', '�udio espacial.')
,(3, 'Faust�o', 'Errrrrroooou do Faust�o.')





