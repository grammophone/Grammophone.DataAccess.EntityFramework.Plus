# Grammophone.DataAccess.EntityFramework.Plus

Optional Entity Framework 6 integration for set-based operations backed by Entity Framework Plus.

Use `EFDomainContainerPlus` as the base class for EF6 domain containers that should support portable set-based query operations such as `ExecuteDelete`, `ExecuteDeleteAsync`, `ExecuteUpdate` and `ExecuteUpdateAsync`.

## Setup

Derive the EF6 domain container from `EFDomainContainerPlus` instead of `EFDomainContainer`:

```csharp
public class EFMusicDomainContainer : EFDomainContainerPlus
{
	public EFMusicDomainContainer(string nameOrConnectionString)
		: base(nameOrConnectionString)
	{
	}

	public DbSet<Artist> Artists { get; set; }
	public DbSet<Album> Albums { get; set; }
	public DbSet<Track> Tracks { get; set; }
	public DbSet<Genre> Genres { get; set; }
}
```

The adapter can remain the normal EF6 domain container adapter.

## Delete Example

```csharp
using Grammophone.DataAccess.QueryExtensions;

var deleted = await musicDomainContainer.Tracks
	.Where(t => t.Album.Name == "Blue Integration")
	.ExecuteDeleteAsync();
```

This delegates to Entity Framework Plus `DeleteFromQueryAsync`.

## Update Example

```csharp
using Grammophone.DataAccess.QueryExtensions;

var updated = await musicDomainContainer.Tracks
	.Where(t => t.Album.Name == "Blue Integration")
	.ExecuteUpdateAsync(setters => setters
		.SetProperty(t => t.DurationSeconds, t => t.DurationSeconds + 5));
```

The portable setter-call expression is translated to the Entity Framework Plus update expression shape.

## Semantics

Set-based mutations execute immediately in the database. They do not materialize the selected entities, do not use change tracking to update or delete individual entities and do not synchronize already-tracked instances in the active domain container.
