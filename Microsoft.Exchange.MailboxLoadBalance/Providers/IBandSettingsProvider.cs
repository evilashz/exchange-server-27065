using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Band;

namespace Microsoft.Exchange.MailboxLoadBalance.Providers
{
	// Token: 0x02000016 RID: 22
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IBandSettingsProvider : IDisposable
	{
		// Token: 0x060000D1 RID: 209
		IEnumerable<Band> GetBandSettings();

		// Token: 0x060000D2 RID: 210
		PersistedBandDefinition PersistBand(Band band, bool enabled);

		// Token: 0x060000D3 RID: 211
		IEnumerable<PersistedBandDefinition> GetPersistedBands();

		// Token: 0x060000D4 RID: 212
		PersistedBandDefinition DisableBand(Band band);

		// Token: 0x060000D5 RID: 213
		PersistedBandDefinition EnableBand(Band band);

		// Token: 0x060000D6 RID: 214
		PersistedBandDefinition RemoveBand(Band band);
	}
}
