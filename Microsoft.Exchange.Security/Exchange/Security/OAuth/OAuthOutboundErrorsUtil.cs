using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000E2 RID: 226
	public static class OAuthOutboundErrorsUtil
	{
		// Token: 0x060007A8 RID: 1960 RVA: 0x000359A4 File Offset: 0x00033BA4
		static OAuthOutboundErrorsUtil()
		{
			foreach (FieldInfo fieldInfo in typeof(OAuthOutboundErrorCodes).GetFields(BindingFlags.Static | BindingFlags.Public))
			{
				OAuthOutboundErrorsUtil.errorDict.Add((OAuthOutboundErrorCodes)fieldInfo.GetValue(null), fieldInfo.GetCustomAttribute(false).Description);
			}
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00035A01 File Offset: 0x00033C01
		public static string GetDescription(OAuthOutboundErrorCodes error, object[] args)
		{
			if (args == null || args.Length <= 0)
			{
				return OAuthOutboundErrorsUtil.errorDict[error];
			}
			return string.Format(OAuthOutboundErrorsUtil.errorDict[error], args);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00035A29 File Offset: 0x00033C29
		public static string GetDescription(OAuthOutboundErrorCodes error, string args = null)
		{
			if (args == null)
			{
				return OAuthOutboundErrorsUtil.errorDict[error];
			}
			return string.Format(OAuthOutboundErrorsUtil.errorDict[error], args);
		}

		// Token: 0x0400073B RID: 1851
		private static readonly Dictionary<OAuthOutboundErrorCodes, string> errorDict = new Dictionary<OAuthOutboundErrorCodes, string>();
	}
}
