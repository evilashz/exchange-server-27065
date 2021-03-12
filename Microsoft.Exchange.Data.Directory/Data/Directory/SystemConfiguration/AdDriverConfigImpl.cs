using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000296 RID: 662
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AdDriverConfigImpl : ConfigBase<AdDriverConfigSchema>
	{
		// Token: 0x06001F04 RID: 7940 RVA: 0x0008AAC2 File Offset: 0x00088CC2
		public static void Refresh()
		{
			ConfigBase<AdDriverConfigSchema>.Provider.GetDiagnosticInfo(DiagnosableParameters.Create("invokescan", false, false, null));
		}
	}
}
