# Monsterflix 

Aplicação BackEnd para o armazenamento de uma lista de filmes através da API do TheMovieDB (https://www.themoviedb.org/documentation/api).  
É tipo um Netflix, só que com energia alternativa e ASP.NET!

### Tecnologias

**BackEnd:** C#, .Net Core, Entity Framework Core, ASP.NET MVC, ASP.NET Authentication Jwt Bearer  
**Banco de dados:** Sql Server  
**Documentação:** Swagger  
** Versões e demais pacotes utilizados estão no arquivo "Monsterflix.Api.csproj" localizado na raiz do projeto.

### Para rodar a aplicação 

Prepare o ambiente para as tecnologias backend descritas na seção anterior, depois siga as seguites instruções:
* Clone o projeto.  
* Crie um banco de dados SQL Server.  
    ** Caso queira criar uma imagem Docker SQL Server: https://docs.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-ver15&preserve-view=true&pivots=cs1-bash#connect-to-sql-server
* Gere uma chave na Api do serviço The Movie DB: https://developers.themoviedb.org/3/getting-started/authentication  
* Crie uma senha de sua preferência para o Token.  
    ** Caso queira, altere o tempo de expiração do token em "TimeExpirationTokenJwtBearer"  
* Assim que criados, troque suas senhas e chave no arquivo "appsettings.Development.json" e/ou "appsettings.json" na raiz do projeto.  
* Aperte F5 para rodar localmente, e começar a seção de debug.

### Para teste 

Navegue até o Swagger para testar as requisições, a partir do link https://localhost:5001/swagger/index.html  
** Caso se faça necessário, clique em "avançado" para continuar, pois a API faz o uso de HTTPS. 

**Autenticação**  
* Crie uma conta em "POST /v1/accounts"
* Logue em "POST /v1/accounts/login"  
    * Essa ação te retornará um token, copie.
    * Clique em "Authorization" e cole seu token, isso permitirá o acesso aos demais métodos.
** Execute "GET /v1/accounts/login" para retornar em qual conta esta logado.

### Features do projeto

- [x] Criar conta  
    - [x] Autenticação (Login)  
    - [x] Criar perfil  
    - [x] Listar perfils  
    - [x] Deletar perfis  
  
- [x] Perfil  
    - [x] Buscar filmes (através da API TMDB)  
    - [x] Listar  
        - [x] Listar filmes PARA ASSISTIR  
        - [x] Listar filmes ASSISTIDOS  
    - [x] Marcar  
        - [x] Marcar filme na lista PARA ASSISTIR  
        - [x] Marcar filmes de PARA ASSISTIR como ASSISTIDO  

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
    Método único para todos as listas, recebendo o status ao qual se quer os filmes da lista, recurso declarado em "MovieController" através do código:
    ```c#
    public async Task<IList<Movie>> GetSearchMovieByStatus(int idProfile, [FromQuery] EStatusMovie statusMovie)
    {
        return await _movieRepository.GetListMoviesByStatus(idProfile, statusMovie);
    }
    ```  
    
    "MovieRepository":
    ```c#
    public async Task<IList<Movie>> GetListMoviesByStatus(int idProfile, EStatusMovie statusMovie)
    {
        return await _context.ProfileMovies.Where(profileMovie => profileMovie.IdProfile == idProfile && profileMovie.StatusWatch == statusMovie).Select(profileMovie => profileMovie.Movie).ToListAsync();
    }
    ```

* **Marcar**  
    Método único para todas as marcações, feitas através da mudança de status no corpo do método, ja que apenas filmes previamente marcados como "para assistir" podem ter seus status mudado, recurso declarado em "MovieController" através do código:
    ```c#
    public async Task UpdateStatusMovieToWatched(int idProfile, int idMovieService)
    {
        ProfileMovie profileMovie = await _profileMovieRepository.SearchProfileMovie(idProfile, idMovieService);
        profileMovie.StatusWatch = EStatusMovie.Watched;

        await _profileMovieRepository.Update(profileMovie);
    }
    ```  

    "ProfileMovieRepository":
    ```c#
    public async Task<ProfileMovie> SearchProfileMovie(int idProfile, int idMovieService)
    {
        return await _context.ProfileMovies.Where(profileMovie => profileMovie.IdProfile == idProfile && profileMovie.Movie.IdMovieService == idMovieService).FirstOrDefaultAsync();
    }

    public async Task<ProfileMovie> Update(ProfileMovie profileMovie)
    {
        _context.ProfileMovies.Update(profileMovie);
        await _context.SaveChangesAsync();

        return profileMovie;
    }
    ```