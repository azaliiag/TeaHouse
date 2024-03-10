using System;
using System.Collections.Generic;

namespace CourseReady.teabase;

public partial class Пользователь
{
    public int IdПользователь { get; set; }

    public string Логин { get; set; }

    public string Пароль { get; set; }

    public string Роль { get; set; }

    public virtual ICollection<Заказ> Заказs { get; } = new List<Заказ>();

    public virtual ICollection<Корзина> Корзинаs { get; } = new List<Корзина>();
}
