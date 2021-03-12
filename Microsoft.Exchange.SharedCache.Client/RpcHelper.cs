using System;
using Microsoft.Exchange.Rpc.SharedCache;

namespace Microsoft.Exchange.SharedCache.Client
{
	// Token: 0x02000005 RID: 5
	internal static class RpcHelper
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000020E3 File Offset: 0x000002E3
		public static bool ValidateValuedBasedResponse(CacheResponse response)
		{
			if (response == null)
			{
				return false;
			}
			if (response.ResponseCode == ResponseCode.OK)
			{
				byte[] value = response.Value;
			}
			return response.ResponseCode == ResponseCode.OK && response.Value != null;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002110 File Offset: 0x00000310
		public static string CreateTransactionString(string clientName, string action, Guid requestCorrelationId, string key)
		{
			return string.Concat(new string[]
			{
				clientName,
				"_ReqId(",
				requestCorrelationId.ToString(),
				")_",
				action,
				"(",
				key,
				")"
			});
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002166 File Offset: 0x00000366
		public static void SetCommonOutParameters(CacheResponse response, out byte[] valueBlob, out string diagnosticsString)
		{
			if (response != null)
			{
				valueBlob = response.Value;
				diagnosticsString = (response.Diagnostics ?? string.Empty);
				return;
			}
			valueBlob = null;
			diagnosticsString = string.Empty;
		}
	}
}
