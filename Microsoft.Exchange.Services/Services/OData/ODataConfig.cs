using System;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000DF0 RID: 3568
	internal static class ODataConfig
	{
		// Token: 0x06005C82 RID: 23682 RVA: 0x0012064B File Offset: 0x0011E84B
		public static void Initialize()
		{
			if (ODataConfig.ShouldEnabledOData())
			{
				HandlerInstaller.Initialize();
			}
		}

		// Token: 0x06005C83 RID: 23683 RVA: 0x00120659 File Offset: 0x0011E859
		private static bool ShouldEnabledOData()
		{
			return ODataConfig.ODataEnabled.Member;
		}

		// Token: 0x0400323B RID: 12859
		private static LazyMember<bool> ODataEnabled = new LazyMember<bool>(() => VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Ews.OData.Enabled);
	}
}
