﻿using Ardalis.Specification;

namespace CleanArchitectureSample.SharedKernel.Interfaces;

// from Ardalis.Specification
public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}
