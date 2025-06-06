using System;

namespace order_processing
{
    public class Notification
    {
        public int OrderId { get; set; }
        public string Action { get; set; } // Например: "Создан", "Отменён", "Редактирован", "Завершён"
        public DateTime ActionDate { get; set; }
    }
}