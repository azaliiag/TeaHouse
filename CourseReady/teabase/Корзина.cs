using System;
using System.Collections.Generic;

namespace CourseReady.teabase;

public partial class Корзина
{
    public int IdКорзина { get; set; }

    public int? Количество { get; set; }

    public int? ОбщаяСтоимость { get; set; }

    public int ПользовательIdПользователь { get; set; }

    public virtual Пользователь ПользовательIdПользовательNavigation { get; set; }

    public virtual ICollection<Товар> ТоварIdТоварs { get; } = new List<Товар>();
}
