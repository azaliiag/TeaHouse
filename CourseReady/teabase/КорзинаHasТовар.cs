using System;
using System.Collections.Generic;

namespace CourseReady.teabase;

public partial class КорзинаHasТовар
{
    public int КорзинаIdКорзина { get; set; }

    public int ТоварIdТовар { get; set; }

    public int? Количество { get; set; }

    public int? ОбщаяСтоимость { get; set; }
    public string Название => this.ТоварIdТоварNavigation.Название;
    public string Стоимость => this.ТоварIdТоварNavigation.Стоимость;

    public virtual Корзина КорзинаIdКорзинаNavigation { get; set; } = null!;

    public virtual Товар ТоварIdТоварNavigation { get; set; } = null!;
}
