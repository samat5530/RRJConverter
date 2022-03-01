using RRJConverter.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RRJConverter.Domain
{

    /// <summary>
    /// Репозиторий для работы с базой данных
    /// </summary>
    public interface IRepository : IDisposable
    {
        /// <summary>
        /// Позволяет получить все операции конвертации из базы данных
        /// </summary>
        /// <returns>Коллекция всех операций конвертаций</returns>
        IEnumerable<DomainConvertingOperationModel> GetOperationList();

        /// <summary>
        /// Получить операцию конвертации по ID в базе
        /// </summary>
        /// <param name="id">Уникальный идентификатор ID </param>
        /// <returns>Возвращает данные о конвертации</returns>
        DomainConvertingOperationModel GetOperation(int id);

        /// <summary>
        /// Создать операцию в базе данных
        /// </summary>
        /// <param name="item"></param>
        void Create(DomainConvertingOperationModel item);

        /// <summary>
        /// Обновить операцию в базе данных
        /// </summary>
        /// <param name="item"></param>
        void Update(DomainConvertingOperationModel item);

        /// <summary>
        /// Удалить операцию по id в базе данных
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        /// Сохранить изменения в базе данных
        /// </summary>
        void Save();
    }
}
