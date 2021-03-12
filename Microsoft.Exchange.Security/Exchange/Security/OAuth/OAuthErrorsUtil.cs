using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000D7 RID: 215
	internal static class OAuthErrorsUtil
	{
		// Token: 0x06000759 RID: 1881 RVA: 0x00033B48 File Offset: 0x00031D48
		static OAuthErrorsUtil()
		{
			foreach (FieldInfo fieldInfo in typeof(OAuthErrors).GetFields(BindingFlags.Static | BindingFlags.Public))
			{
				OAuthErrorsUtil.errorDict.Add((OAuthErrors)fieldInfo.GetValue(null), fieldInfo.GetCustomAttribute(false).Description);
			}
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00033BA5 File Offset: 0x00031DA5
		public static string GetDescription(OAuthErrors error)
		{
			return OAuthErrorsUtil.errorDict[error];
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00033BB2 File Offset: 0x00031DB2
		public static OAuthErrorCategory GetErrorCategory(OAuthErrors error)
		{
			if (error >= OAuthErrors.OfficeSharedErrorCodes)
			{
				return OAuthErrorCategory.OAuthNotAvailable + (error - OAuthErrors.OfficeSharedErrorCodes) / 1000 - 1;
			}
			return OAuthErrorCategory.InvalidSignature + (int)(error / (OAuthErrors)1000) - 1;
		}

		// Token: 0x040006FA RID: 1786
		private static Dictionary<OAuthErrors, string> errorDict = new Dictionary<OAuthErrors, string>();
	}
}
