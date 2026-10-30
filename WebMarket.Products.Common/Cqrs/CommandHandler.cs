using WebMarket.DbProvider;
using WebMarket.Products.Common.Cqrs.Abstractions;

namespace WebMarket.Products.Common.Cqrs;

/// <summary>
/// Базовый обработчик команды без результата.
/// </summary>
public abstract class CommandHandler<TCommand>(WebMarketDbContext context) : ICommandHandler<TCommand> where TCommand : ICommand
{
    protected WebMarketDbContext Context { get; } = context;

    public abstract Task HandleAsync(TCommand command, CancellationToken ct = default);
}

/// <summary>
/// Базовый обработчик команды с результатом.
/// </summary>
public abstract class CommandHandler<TCommand, TResult>(WebMarketDbContext context) : ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{
    protected WebMarketDbContext Context { get; } = context;

    public abstract Task<TResult> HandleAsync(TCommand command, CancellationToken ct = default);
}
