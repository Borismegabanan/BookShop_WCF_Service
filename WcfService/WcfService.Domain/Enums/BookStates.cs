﻿using System.ComponentModel;

namespace WcfService.Domain.Enums
{
    /// <summary>
    /// Возможные состояния книги
    /// </summary>
    public enum BookStates
    {
        [Description("Продано")]
        Sold = 1,

        [Description("В наличии")]
        InStock = 2,

        [Description("Неизвестно")]
        Unknown = 3
    }
}
