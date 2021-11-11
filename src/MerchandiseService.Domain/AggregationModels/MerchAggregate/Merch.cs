﻿using System;
using System.Collections.Generic;
using MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using MerchandiseService.Domain.Events;
using MerchandiseService.Domain.Exceptions.MerchAggregate;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.MerchAggregate
{
    /// <summary>
    /// Мерч
    /// </summary>
    public class Merch : Entity
    {
        public Merch(
            Employee employee,
            MerchType type)
        {
            if (employee is null)
            {
                throw new MerchException("Employee cannot be null");
            }
            
            if (type is null)
            {
                throw new MerchException("Merch type cannot be null");
            }
            
            Employee = employee;
            Type = type;
            
            Items = new List<MerchItem>();
            Status = MerchStatus.New;
            CreatedAt = DateTime.UtcNow;
        }
        
        /// <summary>
        /// Сотрудник
        /// </summary>
        public Employee Employee { get; }
        
        /// <summary>
        /// Статус
        /// </summary>
        public MerchStatus Status { get; private set; }

        /// <summary>
        /// Тип набора
        /// </summary>
        public MerchType Type  { get; }
        
        /// <summary>
        /// Дата создания запроса
        /// </summary>
        public DateTime CreatedAt  { get; }
        
        /// <summary>
        /// Дата выдачи
        /// </summary>
        public DateTime? IssuedAt  { get; private set; }
        
        /// <summary>
        /// Список товаров
        /// </summary>
        private List<MerchItem> Items { get; }

        public List<MerchItem> GetMerchItems()
        {
            return new List<MerchItem>(Items);
        }
        
        /// <summary>
        /// Добавление товара в список
        /// </summary>
        /// <param name="item"></param>
        public bool TryAddMerchItem(MerchItem item, out string reason)
        {
            reason = string.Empty;
            
            if (item is null)
            {
                reason = "MerchItem cannot be null";
                return false;
            }
            
            if (Items.Exists(x => x.Sku.Equals(item.Sku)))
            {
                reason = $"MerchItem with sku {item.Sku.Code} already exists";
                return false;
            }
            
            Items.Add(item);
            
            return true;
        }

        public void SetStatusInWork()
        {
            Status = MerchStatus.InWork;
            
            var merchStatusChangedToInWorkDomainEvent = new MerchStatusChangedToInWorkDomainEvent(Id);
            
            AddDomainEvent(merchStatusChangedToInWorkDomainEvent);
        }

        public void SetStatusSupplyAwaits()
        {
            Status = MerchStatus.SupplyAwaits;
            
            var merchStatusChangedToSupplyAwaitsDomainEvent = new MerchStatusChangedToSupplyAwaitsDomainEvent(Id);
            
            AddDomainEvent(merchStatusChangedToSupplyAwaitsDomainEvent);
        }

        public void SetStatusDone()
        {
            Status = MerchStatus.Done;
            IssuedAt = DateTime.UtcNow;
            
            var merchStatusChangedToDoneDomainEvent = new MerchStatusChangedToDoneDomainEvent(Id);
            
            AddDomainEvent(merchStatusChangedToDoneDomainEvent);
        }
    }
}