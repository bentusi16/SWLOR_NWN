﻿using System;
using System.Collections.Generic;
using SWLOR.Game.Server.Legacy.Data.Contracts;
using SWLOR.Game.Server.Legacy.Enumeration;

namespace SWLOR.Game.Server.Legacy.ValueObject
{
    public class DatabaseAction
    {
        public IList<dynamic> Data { get; set; }
        public DatabaseActionType Action { get; set; }
        public Type DataType { get; }
        public bool IsEnumerable { get; }

        public DatabaseAction(IEntity data, DatabaseActionType action)
        {
            Data = new List<dynamic>();
            Data.Add(data);
            DataType = data.GetType();
            Action = action;
            IsEnumerable = false;
        }

    }
}