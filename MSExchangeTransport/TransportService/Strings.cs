using System;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.TransportService
{
	// Token: 0x02000008 RID: 8
	internal static class Strings
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00004E10 File Offset: 0x00003010
		public static LocalizedString UsageText(string processName)
		{
			return new LocalizedString("UsageText", "ExA026AE", false, true, Strings.ResourceManager, new object[]
			{
				processName
			});
		}

		// Token: 0x040002B3 RID: 691
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Transport.TransportService.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000009 RID: 9
		private enum ParamIDs
		{
			// Token: 0x040002B5 RID: 693
			UsageText
		}
	}
}
