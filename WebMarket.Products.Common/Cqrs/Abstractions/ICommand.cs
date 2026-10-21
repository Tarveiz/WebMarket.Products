namespace WebMarket.Products.Common.Cqrs.Abstractions;

/// <summary>
/// Команда изменения данных без результата.
/// </summary>
public interface ICommand;

/// <summary>
/// Команда изменения данных с результатом TResult.
/// </summary>
public interface ICommand<TResult>;
