﻿using System.ComponentModel.DataAnnotations;

namespace Intex2.Models
{
    public class Recommendation
    {
        [Key]
        public int RecID { get; set; }
        public int Rec1 { get; set; }
        public int Rec2 { get; set; }
        public int Rec3 { get; set; }
        public int Rec4 { get; set; }
        public int Rec5 { get; set; }
        public int Rec6 { get; set; }
        public int Rec7 { get; set; }
        public int Rec8 { get; set; }
        public int Rec9 { get; set; }
        public int Rec10 { get; set; }
    }
}
