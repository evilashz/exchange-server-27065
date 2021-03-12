using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000035 RID: 53
	public class UMLanguageListSource : ObjectListSource
	{
		// Token: 0x0600021B RID: 539 RVA: 0x000088A3 File Offset: 0x00006AA3
		public UMLanguageListSource(UMLanguage[] languages) : base(languages)
		{
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000088AC File Offset: 0x00006AAC
		protected override string GetValueText(object objectValue)
		{
			string result = string.Empty;
			UMLanguage umlanguage = objectValue as UMLanguage;
			if (umlanguage != null)
			{
				result = umlanguage.DisplayName;
			}
			return result;
		}
	}
}
