﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Common.Interfaces
{
    public interface IRecentConversationsWindow
    {
        object[] RecentList { set; }
    }
}
