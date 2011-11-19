using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskReminder.Core.EntityFramework
{
    public class BaseRepository
    {
        protected DataContext context = new DataContext();
    }
}
