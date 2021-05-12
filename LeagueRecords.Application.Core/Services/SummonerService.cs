using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LeagueRecords.Application.Core.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LeagueRecords.Application.Core.Services
{

    public interface ISummonerService
    {
        Task<SummonerDto> GetSummonerInfo(string summonerName);
        Task<MatchDto> GetMatchList(string summonerName, int count);
        Task<List<MatchDetailDto>> GetMatchDetails(string summonerName, int count);
    }

    public class SummonerService : ISummonerService
    {

        private readonly HttpClient _httpClient;

        public SummonerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SummonerDto> GetSummonerInfo(string summonerName)
        {
            
            var requestUrl = ($"summoner/v4/summoners/by-name/{summonerName}?api_key=RGAPI-7c2ed504-695b-436b-9896-09fa7ad9af9b");
            var response = await _httpClient.GetAsync(requestUrl);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SummonerDto>(content);
        }

        public async Task<MatchDto> GetMatchList(string summonerName, int count)
        {
            var matchDto = new MatchDto();

            var summonerInfo = await GetSummonerInfo(summonerName);
            var requestUrl = ($"https://americas.api.riotgames.com/lol/match/v5/matches/by-puuid/{summonerInfo.PersonalUUid}/ids?start=0&count={count}&api_key=RGAPI-7c2ed504-695b-436b-9896-09fa7ad9af9b");
            var response = await _httpClient.GetAsync(requestUrl);
            var content = await response.Content.ReadAsStringAsync();
            var matchJArray = JsonConvert.DeserializeObject<JArray>(content);
            var elements = matchJArray.Select(e => (string)e).ToList();
            elements.ForEach(e => matchDto.MatchList.Add(e.ToString()));
            return matchDto;
        }

        public async Task<List<MatchDetailDto>> GetMatchDetails(string summonerName, int count)
        {
            
            var matchIds =  await GetMatchList(summonerName, count);
            var matches = new List<MatchDetailDto>();
            
            foreach (var matchId in matchIds.MatchList)
            {
                var requestUrl = ($"https://americas.api.riotgames.com/lol/match/v5/matches/{matchId}?api_key=RGAPI-7c2ed504-695b-436b-9896-09fa7ad9af9b");
                var response = await _httpClient.GetAsync(requestUrl);
                var content = await response.Content.ReadAsStringAsync();
                var matchDetail = JsonConvert.DeserializeObject<MatchDetailDto>(content);
                matches.Add(matchDetail);
            }


            return matches;
        }
    }
}