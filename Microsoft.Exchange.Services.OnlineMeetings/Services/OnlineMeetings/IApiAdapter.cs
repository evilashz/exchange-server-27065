using System;
using System.Threading.Tasks;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x02000014 RID: 20
	internal interface IApiAdapter
	{
		// Token: 0x06000045 RID: 69
		Task<Uri> FindTokenAsync(string token);

		// Token: 0x06000046 RID: 70
		Task<TResponse> SendRequestToTokenAsync<TResponse>(string token, string method, object request = null) where TResponse : class;

		// Token: 0x06000047 RID: 71
		Task<TResponse> SendRequestAsync<TResponse>(Uri uri, string method, object request = null) where TResponse : class;
	}
}
