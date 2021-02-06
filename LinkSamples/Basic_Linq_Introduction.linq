<Query Kind="Expression">
  <Connection>
    <ID>c0c556f5-db0a-46ed-a471-50cd80c988e1</ID>
    <Persist>true</Persist>
    <Server>AYMAN-PC</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

// full table row method syntax (code as objects)
Albums.Select(x => x)

// full table row query syntax
from x in Albums select x

// partial table row query in query syntax
from x in Albums
select new
{
	Title = x.Title,
	Year = x.ReleaseYear
}

// partial table row query in method syntax
Albums.Select(x => new {Title = x.Title, Year = x.ReleaseYear})

// partial table row query in query syntax with filter (where)
from x in Albums
where x.ReleaseYear == 1990
select new
{
	Title = x.Title,
	Year = x.ReleaseYear
}

// partial table row query in method syntax with filter (where)
Albums.Where(x => x.ReleaseYear == 1990).Select(x => new {Title = x.Title, Year = x.ReleaseYear})

// using OrderBy with query syntax
from x in Albums
where x.ReleaseYear >= 1990 && x.ReleaseYear < 2000
orderby x.ReleaseYear ascending, x.Title descending
select new
{
	Title = x.Title,
	Year = x.ReleaseYear
}

// using OrderBy with query syntax
Albums
	.Where(x => x.ReleaseYear >= 1990 && x.ReleaseYear < 2000)
	.OrderBy(x => x.ReleaseYear)
	.ThenByDescending(x => x.Title)
	.Select(x => new { Title = x.Title, Year = x.ReleaseYear })
	
// create a list of albums showing the album title, artist, and release year for the good old 70s, order by artist then by title

// method
Albums
	.Where(x => x.ReleaseYear >= 1970 && x.ReleaseYear < 1980)
	.OrderBy(x => x.Artist.Name)
	.ThenBy(x => x.Title)
	.Select(x => new { Artist = x.Artist.Name, Title = x.Title, Year = x.ReleaseYear })
	
// query
from x in Albums
where x.ReleaseYear >= 1970 && x.ReleaseYear < 1980
orderby x.Artist.Name, x.Title
select new
{
	Artist = x.Artist.Name,
	Title = x.Title,
	Year = x.ReleaseYear
}