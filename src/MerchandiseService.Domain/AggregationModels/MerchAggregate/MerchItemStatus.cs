﻿using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.MerchAggregate
{
    /// <summary>
    /// Статус товара в наборе
    /// </summary>
    public class MerchItemStatus : Enumeration
    {
        private MerchItemStatus(int id, string name)
            : base(id, name)
        {
        }
        
        /// <summary>
        /// Ждет поставки
        /// </summary>
        public static MerchItemStatus SupplyAwaits = new(1, nameof(SupplyAwaits));
        
        /// <summary>
        /// Выдан
        /// </summary>
        public static MerchItemStatus Done = new(2, nameof(Done));
    }
}