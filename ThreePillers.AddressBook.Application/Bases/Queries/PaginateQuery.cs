﻿namespace ThreePillers.AddressBook.Application.Bases.Queries;

public record PaginateQuery
{
    public virtual int PageIndex { get; set; } = 1;
    public virtual int PageSize { get; set; } = 10;
    public string Search { get; set; } = string.Empty;
    public SortDirection SortDirection { get; set; } = SortDirection.Ascending;
}
