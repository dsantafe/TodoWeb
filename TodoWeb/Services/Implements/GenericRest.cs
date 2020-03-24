using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TodoWeb.Services.Implements
{
    public class GenericRest<TEntity> : IGenericRest<TEntity>
    {   
        public async Task<TEntity> GetById(int id)
        {
            HttpResponseMessage response = await RestClientSingleton.Instance().GetAsync("api/TodoItems/" + id);
            string responseText = await response.Content.ReadAsStringAsync();

            response.Dispose();

            if (!response.IsSuccessStatusCode)
                throw new Exception(responseText);

            return JsonConvert.DeserializeObject<TEntity>(responseText);
        }

        public async Task<List<TEntity>> GetAll()
        {
            HttpResponseMessage response = await RestClientSingleton.Instance().GetAsync("api/TodoItems");
            string responseText = await response.Content.ReadAsStringAsync();

            response.Dispose();

            if (!response.IsSuccessStatusCode)
                throw new Exception(responseText);

            return JsonConvert.DeserializeObject<List<TEntity>>(responseText);
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await RestClientSingleton.Instance().PostAsync("api/TodoItems", content);
            string responseText = await response.Content.ReadAsStringAsync();

            response.Dispose();

            if (!response.IsSuccessStatusCode)
                throw new Exception(responseText);

            return JsonConvert.DeserializeObject<TEntity>(responseText);
        }

        public async Task Update(int id, TEntity entity)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await RestClientSingleton.Instance().PutAsync("api/TodoItems/" + id, content);

            string responseText = await response.Content.ReadAsStringAsync();

            response.Dispose();

            if (!response.IsSuccessStatusCode)
                throw new Exception(responseText);            
        }       

        public async Task Delete(int id)
        {
            HttpResponseMessage response = await RestClientSingleton.Instance().DeleteAsync("api/TodoItems/" + id);
            string responseText = await response.Content.ReadAsStringAsync();

            response.Dispose();

            if (!response.IsSuccessStatusCode)
                throw new Exception(responseText);            
        }
    }
}