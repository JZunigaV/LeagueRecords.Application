using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LeagueRecords.Application.Core.Dtos;
using LeagueRecords.Application.Core.Services;
using Microsoft.AspNetCore.Http;

namespace LeagueRecords.Application.Api.Controllers
{
    [Route("api/summoner")]
    public class SummonerController : ApiControllerBase
    {
        private readonly ISummonerService _summonerService;

        public SummonerController(ISummonerService summonerService)
        {
            _summonerService = summonerService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(SummonerDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSummoner(string summonerName)
        {
            var response = await _summonerService.GetSummonerInfo(summonerName);
            return Ok(response);
        }

        
        [HttpGet("matches-list")]
        [ProducesResponseType(typeof(MatchDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMatchesList(string summonerName, int count)
        {
            var response = await _summonerService.GetMatchList(summonerName, count);
            return Ok(response);
        }

        [HttpGet("match-details")]
        [ProducesResponseType(typeof(MatchDetailDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMatchesDetails(string summonerName, int count)
        {
            var response = await _summonerService.GetMatchDetails(summonerName, count);
            return Ok(response);
        }

    }
}
