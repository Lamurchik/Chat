using System;
using System.ComponentModel.DataAnnotations;

namespace ChatLaba3
{
    public class Msg
    {
        //public int Id {  get; set; }
        public string Message { get; set; }
        [Key]
        public DateTime Time { get; set; }
    }
}
