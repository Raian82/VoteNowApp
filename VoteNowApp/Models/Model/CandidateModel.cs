﻿namespace VoteNowApp.Models.Model
{
    public class CandidateModel
    {
        public Guid Id { get; set; }
        public string candidatename { get; set; } = string.Empty;
        public int number { get; set; }
        public string course { get; set; } = string.Empty;
        public string yearlevel { get; set; } = string.Empty;
        public string position { get; set; } = string.Empty;
        public int Votes { get; set; }
    }
}
