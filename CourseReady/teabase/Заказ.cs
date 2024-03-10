using System;
using System.Collections.Generic;

namespace CourseReady.teabase;

public partial class Заказ
{
    public int IdЗаказ { get; set; }

    public string Адрес { get; set; }

    public string СпособОплаты { get; set; }

    public int ОбщаяСтоимость { get; set; }

    public int IdПользователя { get; set; }

    public virtual Пользователь IdПользователяNavigation { get; set; }
}
