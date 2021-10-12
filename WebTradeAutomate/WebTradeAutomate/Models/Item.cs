using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTradeAutomate.Models
{
   public class Item
   {
      public int Id { get; set; }
      [Required(ErrorMessage = "Укажите название продукта")]
      public string Name { get; set; }
      [Range(1, 999, ErrorMessage = "код напитка должен быть в диапазоне от 1 до 999")]
      public int Code { get; set; }

      [Range(1, 100, ErrorMessage = "Стоимость должна быть в промежутке от 1 до 100")]
      [Required(ErrorMessage = "Укажите стоимость продукта")]
      public int cost { get; set; }
      [Range(1, 20, ErrorMessage = "Количество напитков должно быть от 1 до 20")]
      [Required(ErrorMessage = "Укажите количество продукта")]
      public int quantity { get; set; }
   }
}
