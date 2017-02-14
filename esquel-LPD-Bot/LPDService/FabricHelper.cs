using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using esquel_LPD_Bot.Model;

namespace esquel_LPD_Bot.LPDService
{
    public class FabricHelper
    {
        public async Task<Fabric> FabricSearch(string pi_FabricNo)
        {
            Fabric l_returnValue = null;
            string l_APIUrl = Common.ConfigHelper.GetConfigValue("LPD-FabricSearchAPIUrl");

            try
            {
                if (!string.IsNullOrEmpty(l_APIUrl) && !string.IsNullOrEmpty(pi_FabricNo))
                {
                    #region search entity
                    var requestEntity = new Model.SelectionFilter()
                    {
                        FilterType = Model.SelectionFilter.FilterTypeLeaf,
                        Filters = new Model.SelectionFilter[] { },
                        AttributeName = "item_number",
                        FilterValue = pi_FabricNo
                    };
                    #endregion

                    string l_returnjson = await new Common.HttpClientHelper().HttpClient_PostAsync(l_APIUrl, JsonConvert.SerializeObject(requestEntity).ToString());
                    BaseResponse<List<PagingData<Fabric>>> l_returnEntity = await JsonConvert.DeserializeObjectAsync<BaseResponse<List<PagingData<Fabric>>>>(l_returnjson);

                    #region get style entity
                    if (l_returnEntity != null)
                    {
                        if (l_returnEntity.results != null && l_returnEntity.results.Count > 0)
                        {
                            if (l_returnEntity.results.First().data != null && l_returnEntity.results.First().data.Count() > 0)
                            {
                                l_returnValue = l_returnEntity.results.First().data.First();
                            }
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {

            }

            return l_returnValue;
        }
    }
}