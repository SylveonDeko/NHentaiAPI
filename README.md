# HentaiAPI

[![Build status](https://ci.appveyor.com/api/projects/status/9u2xoxn47irix7gp?svg=true)](https://ci.appveyor.com/project/andy840119/nhentaiapi)
[![NuGet](https://img.shields.io/nuget/v/NHentaiAPI.svg)](https://www.nuget.org/packages/NHentaiAPI)
[![NuGet](https://img.shields.io/nuget/dt/NHentaiAPI.svg)](https://www.nuget.org/packages/NHentaiAPI)
[![NuGet](https://img.shields.io/badge/月子我婆-passed-ff69b4.svg)](https://github.com/andy840119/NHentaiAPI)

A (full)  nHentai API implementation for .NET

If N-Hentai change the api format, please throw a issue to let me know :)

# Check out my other projects
## Mewdeko https://github.com/Sylveon76/Mewdeko
## MartineApi https://github.com/Sylveon76/MartineApi.Net
## NekosBestApi https://github.com/Sylveon76/Nekos.Best-API

## This package can get

Search:

1. Home page search result

2. Search result by `keyword`

3. Search result by `tag`, can be sort by popular

4. Search tags can be filtered by putting `-` in front of them

Book detail:

1. Book detail

2. Related book

Picture:

1. Page picture (preview picture, thumbnail and origin picture)
2. Cover picture (preview picture and thumbnail)

## Demo

Search book:

```CSharp
//generate client
var client = new NHentaiClient();

//https://nhentai.net/api/galleries/search?query=school%20swimsuit%20loli%20full%20color&page=2
var result = await client.GetSearchPageListAsync("school swimsuit full color -loli",2);
```

Get book detail:

```CSharp
//generate client
var client = new NHentaiClient();

//get book no 123
var book = await client.GetBookAsync(123);

```

Get cover and image:

```CSharp
//get book no 123
var book = await client.GetBookAsync(123);

//https://i.nhentai.net/galleries/635/1.jpg
byte[] picture = await client.GetPictureAsync(book, 1);

//https://i.nhentai.net/galleries/635/1.jpg
byte[] cover = await client.GetBigCoverPictureAsync(book);
```

