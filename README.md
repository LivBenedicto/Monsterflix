# Monsterflix 

Aplicação BackEnd para o armazenamento de uma lista de filmes através da API do TheMovieDB (https://www.themoviedb.org/documentation/api).

### Para rodar a aplicação 

Prepare o ambiente para as tecnologias backend descritas na seção a seguir, e siga as seguites instruções:
* Clone o projeto.
* Banco de dados SQL Server. 
    **Caso queira criar uma imagem Docker SQL Server: https://docs.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-ver15&preserve-view=true&pivots=cs1-bash#connect-to-sql-server
* Api Key do serviço The Movie DB: https://developers.themoviedb.org/3/getting-started/authentication 
* Assim que criados, troque sua senha e chave no arquivo "appsettings.json" na raiz do projeto.
* Aperte F5 para rodar localmente, e começar a seção de debug.

### Tecnologias

BackEnd: C#, .Net Core, Entity Framework Core, ASP.NET MVC 
Banco de dados: Sql Server, Docker 
Documentação: Swagger 
**Versões no arquivo "Monsterflix.Api.csproj" localizado na raiz do projeto. 

### Features do projeto

[x] CRIAR CONTA 
    [] AUTENTICAÇÃO (LOGIN) 
    [x] CRIAR PERFIL 
    [x] LISTAR PERFIS 
    [x] DELETAR PERFIL 

[x] PERFIL 
    [x] BUSCAR FILMES (através da API TMDB) 
    [x] LISTAR 
        [x] LISTAR FILMES PARA ASSISTIR 
        [x] LISTAR FILMES ASSISTIDOS 
        [x] LISTAR FILMES SUGERIDOS 
    [x] MARCAR 
        [x] MARCAR FILME NA LISTA PARA ASSISTIR 
        [x] MARCAR FILMES DE PARA ASSISTIR COMO ASSISTIDO 

### Explicações

* **Docker** 
    Imagem SQL "mcr.microsoft.com/mssql/server:2019-latest".

* **Interfaces** 
    Utilizadas para facilitação de manutenção de código.

* **Status dos filmes** 
    Declarados na classe Enum, como:
        Para assistir = 0,
        Assistido = 1
    Os filmes tem seu status "inicializado" no momento em que são adicionados como "para assistir".

* **Listar** 
    Método único para todos as listas, recebendo o status ao qual se quer os filmes da lista, recurso feito através do código:
    ```c#
    public async Task<IList<Movie>> GetListMoviesByStatus(int idProfile, EStatusMovie statusMovie)
        {
            return await _context.ProfileMovies.Where(profileMovie => profileMovie.IdProfile == idProfile && profileMovie.StatusWatch == statusMovie).Select(profileMovie => profileMovie.Movie).ToListAsync();
        }
    ```

* **Marcar** 
    Método único para todas as marcações, feitas através da mudança de status no corpo do método, ja que apenas filmes previamente marcados como "para assistir" podem ter seus status mudados, recurso feito através do código:
    ```c#
    public async Task<ProfileMovie> UpdateMovieStatus(int idProfile, int idMovie)
        {
            ProfileMovie profileMovie = await _context.ProfileMovies.Where(profileMovie => profileMovie.IdProfile == idProfile && profileMovie.IdMovie == idMovie).FirstOrDefaultAsync();
            profileMovie.StatusWatch = EStatusMovie.Watched;
            _context.ProfileMovies.Update(profileMovie);
            
            await _context.SaveChangesAsync();
            
            return profileMovie;
        }
    ```