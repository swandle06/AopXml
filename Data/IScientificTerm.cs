using System;

namespace AopWikiExporter.Data
{
    public interface IScientificTerm : ITerm
    {
        int Id { get; }
        DateTime CreatedAt { get; }
        string Source { get; }
        string SourceId { get; }
        string Synonym { get; }
        DateTime UpdatedAt { get; }
    }
}