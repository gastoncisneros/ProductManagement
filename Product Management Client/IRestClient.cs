using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Product_Management_Client
{
    public interface IRestClient
    {
        Task<HttpResponseMessage> Get(string url);
        Task<HttpResponseMessage> Post<TContent>(string url, TContent body);
        Task<string> PostAsyncToString<TContent>(string url, TContent body);
        Task<TResult> PostAsyncToResult<TContent, TResult>(string url, TContent body);
        Task<TResult> GetAsyncToResult<TResult>(string url);
    }
}
