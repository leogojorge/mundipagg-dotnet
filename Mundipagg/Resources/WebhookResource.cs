﻿using Mundipagg.Models.Commons;
using Mundipagg.Models.Request;
using Mundipagg.Models.Response;
using Mundipagg.Models.Webhooks;
using Mundipagg.Resources.Interface;
using Mundipagg.Utils;
using Newtonsoft.Json;
using RestSharp.Easy.Models;
using System;
using System.Net.Http;

namespace Mundipagg.Resources
{
    public class WebhookResource : BaseResource, IWebhookResource
    {
        public WebhookResource(Configuration configuration) : base(configuration) { }

        public BaseResponse<GetWebhookResponse, MundipaggErrorsResponse> GetWebhook(string webhookId)
        {
            var method = HttpMethod.Get;
            var endpoint = $"/hooks/{webhookId}";

            return this.SendRequest<GetWebhookResponse>(method, endpoint, null);
        }

        public BaseResponse<PagingResponse<GetWebhookResponse>, MundipaggErrorsResponse> ListWebhooks(ListWebhooksRequest request)
        {
            var method = HttpMethod.Get;
            var endpoint = $"/hooks";
            var query = request.ToDictionary();

            return this.SendRequest<PagingResponse<GetWebhookResponse>>(method, endpoint, null, query);
        }
        
        public WebhookReceived ParseWebhook(string json)
        {
            if (string.IsNullOrWhiteSpace(json) == true)
                throw new NullReferenceException($"{nameof(json)} can not be null or empty");

            return JsonConvert.DeserializeObject<WebhookReceived>(json, JsonSerializerUtil.SnakeCaseSettings);
        }

        public BaseResponse<GetWebhookResponse, MundipaggErrorsResponse> RetryWebhook(string webhookId)
        {
            var method = HttpMethod.Post;
            var endpoint = $"/hooks/{webhookId}/retry";

            return this.SendRequest<GetWebhookResponse>(method, endpoint, null);
        }
    }
}