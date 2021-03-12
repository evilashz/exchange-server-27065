using System;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Anchor;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.Providers;

namespace Microsoft.Exchange.MailboxLoadBalance.Diagnostics
{
	// Token: 0x0200005B RID: 91
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LoadBalanceBandSettingsStorageDiagnosable : LoadBalanceDiagnosableBase<LoadBalanceBandSettingsStorageDiagnosableArguments, LoadBalanceBandSettingsStorageDiagnosableResult>
	{
		// Token: 0x0600032E RID: 814 RVA: 0x00009B6E File Offset: 0x00007D6E
		public LoadBalanceBandSettingsStorageDiagnosable(LoadBalanceAnchorContext anchorContext) : base(anchorContext.Logger)
		{
			this.anchorContext = anchorContext;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00009B84 File Offset: 0x00007D84
		protected override LoadBalanceBandSettingsStorageDiagnosableResult ProcessDiagnostic()
		{
			LoadBalanceBandSettingsStorageDiagnosableResult loadBalanceBandSettingsStorageDiagnosableResult = new LoadBalanceBandSettingsStorageDiagnosableResult();
			LoadBalanceBandSettingsStorageDiagnosableResult result;
			using (IBandSettingsProvider bandSettingsProvider = this.anchorContext.CreateBandSettingsStorage())
			{
				if (base.Arguments.ShowPersistedBands)
				{
					loadBalanceBandSettingsStorageDiagnosableResult.PersistedBands = bandSettingsProvider.GetPersistedBands().ToArray<PersistedBandDefinition>();
				}
				if (base.Arguments.ShowActiveBands)
				{
					loadBalanceBandSettingsStorageDiagnosableResult.ActiveBands = bandSettingsProvider.GetBandSettings().ToArray<Band>();
				}
				if (base.Arguments.ProcessAction)
				{
					Band band = base.Arguments.CreateBand();
					switch (base.Arguments.Action)
					{
					case LoadBalanceBandSettingsStorageDiagnosableArguments.BandStorageActionType.Create:
						loadBalanceBandSettingsStorageDiagnosableResult.ModifiedBand = bandSettingsProvider.PersistBand(band, base.Arguments.Enabled);
						break;
					case LoadBalanceBandSettingsStorageDiagnosableArguments.BandStorageActionType.Remove:
						loadBalanceBandSettingsStorageDiagnosableResult.ModifiedBand = bandSettingsProvider.RemoveBand(band);
						break;
					case LoadBalanceBandSettingsStorageDiagnosableArguments.BandStorageActionType.Enable:
						loadBalanceBandSettingsStorageDiagnosableResult.ModifiedBand = bandSettingsProvider.EnableBand(band);
						break;
					case LoadBalanceBandSettingsStorageDiagnosableArguments.BandStorageActionType.Disable:
						loadBalanceBandSettingsStorageDiagnosableResult.ModifiedBand = bandSettingsProvider.DisableBand(band);
						break;
					}
				}
				result = loadBalanceBandSettingsStorageDiagnosableResult;
			}
			return result;
		}

		// Token: 0x040000EC RID: 236
		private readonly LoadBalanceAnchorContext anchorContext;
	}
}
