using System;
using Microsoft.Exchange.Hygiene.Data.Directory;

namespace Microsoft.Forefront.Reporting.OnDemandQuery
{
	// Token: 0x0200000B RID: 11
	public static class OnDemandQueryHelper
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00003774 File Offset: 0x00001974
		internal static string GetSchemaUrl(OnDemandQueryRequest request)
		{
			return OnDemandQueryHelper.GetSchemaUrl(request.CosmosResultUri, request.QueryType.ToString());
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003794 File Offset: 0x00001994
		internal static string GetSchemaUrl(string cosmosResultUrl, string schemaName)
		{
			if (string.IsNullOrWhiteSpace(cosmosResultUrl))
			{
				return string.Empty;
			}
			string arg = cosmosResultUrl.Substring(0, cosmosResultUrl.LastIndexOf("/Results/"));
			return string.Format("{0}/{1}.schema", arg, schemaName);
		}
	}
}
