using System;
using System.Text;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005DC RID: 1500
	public sealed class TestOAuthWacConnectivityHelper
	{
		// Token: 0x06003512 RID: 13586 RVA: 0x000DA2C4 File Offset: 0x000D84C4
		public static ResultType SendWacOAuthRequest(string wopiUrl, string wacTemplateUrl, ADUser user, out string diagnosticMessage)
		{
			StringBuilder stringBuilder = new StringBuilder();
			WacResult wacResult = null;
			try
			{
				wacResult = WacWorker.ExecuteWacRequest(wacTemplateUrl, wopiUrl, user, stringBuilder);
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("Unhandled Exception while running Wac Probe.");
				stringBuilder.AppendLine(ex.ToString());
				diagnosticMessage = stringBuilder.ToString();
				return ResultType.Error;
			}
			diagnosticMessage = stringBuilder.ToString();
			if (wacResult.Error)
			{
				return ResultType.Error;
			}
			return ResultType.Success;
		}

		// Token: 0x04002489 RID: 9353
		public const string ComponentId = "OAuthWacProbe:";
	}
}
