using System;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Extensibility.Internal
{
	// Token: 0x02000047 RID: 71
	internal static class DsnVariantConfiguration
	{
		// Token: 0x060002BF RID: 703 RVA: 0x00010C20 File Offset: 0x0000EE20
		internal static bool SystemMessageOverridesEnabled()
		{
			return VariantConfiguration.InvariantNoFlightingSnapshot.Transport.SystemMessageOverrides.Enabled;
		}
	}
}
