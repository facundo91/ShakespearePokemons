# Shakespeare Pokemons
Project for job interview created by Facundo Aita


## How to build using **dotnet**
### Requirements
* Git is required - [Download Git](https://git-scm.com/downloads)
* .NET 5 is required  - [Download .NET 5.0](https://dotnet.microsoft.com/download/dotnet/5.0)

### Steps

1. Clone this repository
`git clone https://github.com/facundo91/ShakespearePokemons.git`

2. Go to the repository folder
`cd ShakespearePokemons`

3. Build the project `dotnet build .\ShakespearePokemons\ShakespearePokemons.csproj`

4. Run the excecutable `.\ShakespearePokemons\bin\Debug\net5.0\publish\ShakespearePokemons.exe`

5. Open a browser and go to  [localhost:5001](https://localhost:5001)

## How to build using **docker**
### Requirements
* Git is required - [Download Git](https://git-scm.com/downloads)
* Docker is required  - [Download Docker](https://www.docker.com/get-started)

### Steps

1. Clone this repository
`git clone https://github.com/facundo91/ShakespearePokemons.git`

2. Go to the repository folder
`cd ShakespearePokemons`

3. Build the image & run a container `docker-compose -p .\docker-compose.dcproj up -d`

5. Open a browser and go to  [localhost:32768](https://localhost:32768)