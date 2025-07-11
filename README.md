# 📚 Gerenciador de Cursos - Backend (.NET 8)

Este é o backend da aplicação de gerenciamento de cursos, alunos e matrículas, desenvolvido em ASP.NET Core 8 com Entity Framework Core.

## ✅ Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [SQL Server LocalDB](https://learn.microsoft.com/pt-br/sql/database-engine/configure-windows/sql-server-express-localdb)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) ou outro editor compatível
- Git

## 🚀 Como rodar localmente

1. **Clone o repositório**

```bash
git clone https://github.com/joaopsilvam/GerenciadorCursos.git
cd GerenciadorCursos
```

2. **Configure a string de conexão no `appsettings.json`**

Edite o arquivo `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=GerenciadorCursosDb;Trusted_Connection=True;"
}
```

3. **Configure a porta HTTPS no `Program.cs`**

```csharp
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(7010, listenOptions =>
    {
        listenOptions.UseHttps();
    });
});
```

4. **Rode as migrations**

```bash
dotnet ef database update
```

5. **Execute o projeto**

```bash
dotnet run
```

A API estará disponível em:  
`https://localhost:7010`

## 📂 Endpoints principais

- `GET /api/alunos`
- `GET /api/cursos`
- `POST /api/matriculas`
- Entre outros

## 🛠 Tecnologias

- .NET 8
- Entity Framework Core
- C#
- Swagger (habilitado por padrão)
