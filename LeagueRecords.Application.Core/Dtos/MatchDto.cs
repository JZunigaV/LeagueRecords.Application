using System.Collections.Generic;

namespace LeagueRecords.Application.Core.Dtos
{
    public class MatchDto
    {
        public MatchDto()
        {
            MatchList = new List<string>();
        }

        public List<string> MatchList { get; set; }
    }

}
    