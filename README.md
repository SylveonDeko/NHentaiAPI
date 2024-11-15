# NHentaiAPI

[![Build status](https://ci.appveyor.com/api/projects/status/32r7s2skrgm9ubva?svg=true)](https://ci.appveyor.com/project/SylveonDeko/nhentaiapi)
[![NuGet](https://img.shields.io/nuget/v/NHentaiAPI.svg)](https://www.nuget.org/packages/NHentaiAPI)
[![NuGet](https://img.shields.io/nuget/dt/NHentaiAPI.svg)](https://www.nuget.org/packages/NHentaiAPI)
[![NuGet](https://img.shields.io/badge/月子我婆-passed-ff69b4.svg)](https://github.com/SylveonDeko/NHentaiAPI)

A full nHentai API implementation for .NET

⚠️ If nHentai changes their API format, please create an issue to let me know!

## Important Notes

### User Agent Requirements
A User-Agent is **required** to use this API. The client will throw an error if none is provided. You can get your User-Agent by:
1. Going to https://www.whatismybrowser.com/detect/what-is-my-user-agent/
2. Or by opening Developer Tools (F12) in your browser, going to Network tab, and looking at the "User-Agent" header in any request

Example:
```csharp
var client = new NHentaiClient("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
```

### CSRF Token (If Required)
If nHentai implements CSRF protection, you can get the token by:
1. Opening Developer Tools (F12)
2. Going to Network tab
3. Looking for a request header named 'x-csrf-token' or a cookie named 'csrf_token'
4. Pass it to the client using the cookies dictionary

**Important**: The CSRF token must be obtained from the same IP address and User-Agent that will be used with the API. Using a token from a different IP or User-Agent will result in authentication failures.

```csharp
var cookies = new Dictionary<string, string>
{
    {"csrf_token", "your-token-here"}
};
var client = new NHentaiClient("your-user-agent", cookies);
```

## Features

### Search Capabilities:

1. Browse homepage content
2. Search by keywords
3. Search by tags with optional popularity sorting
4. Filter tags using `-` prefix (exclusion)

### Book Operations:

1. Fetch book details
2. Get related books

### Image Operations:

1. Page images (preview, thumbnail, and original quality)
2. Cover images (preview and thumbnail)

## Usage Examples

### Search Books:
```csharp
// Initialize client with User-Agent
var client = new NHentaiClient("your-user-agent-string");

// Search with filters
var result = await client.GetSearchPageListAsync("school swimsuit full color -loli", 2);

// Browse homepage
var homeResults = await client.GetHomePageListAsync(1);
```

### Get Book Details:
```csharp
var client = new NHentaiClient("your-user-agent-string");

// Get book by ID
var book = await client.GetBookAsync(123);

// Get related books
var related = await client.GetBookRecommendAsync(123);
```

### Get Images:
```csharp
var book = await client.GetBookAsync(123);

// Get full page image
byte[] picture = await client.GetPictureAsync(book, 1);

// Get cover image
byte[] cover = await client.GetBigCoverPictureAsync(book);

// Get thumbnails
byte[] thumbnail = await client.GetThumbPictureAsync(book, 1);
byte[] coverThumb = await client.GetBookThumbPictureAsync(book);
```

## Check Out My Other Projects
- [Mewdeko](https://github.com/SylveonDeko/Mewdeko) - Discord Bot
- [MartineApi](https://github.com/SylveonDeko/MartineApi.Net) - Image API Wrapper
- [NekosBestApi](https://github.com/SylveonDeko/Nekos.Best-API) - Anime Image API

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.