﻿using System;

namespace Hangfire.MemoryStorage.Dto
{
    public class CounterDto : IExpirable, IIdentifiedData<int>
    {
        public int Value { get; set; }
        public DateTime? ExpireAt { get; set; }
        public int Id { get; set; }
        public string Key { get; set; }
    }
}