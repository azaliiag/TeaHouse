using System;
using System.Collections.Generic;

namespace CourseReady.teabase;

public partial class Товар
{
    public int IdТовар { get; set; }

    public string Название { get; set; }

    public int Стоимость { get; set; }

    public int? Количество { get; set; }

    public string Категория { get; set; }

    public byte[] Фото { get; set; }

    public virtual ICollection<Корзина> КорзинаIdКорзинаs { get; } = new List<Корзина>();
}
