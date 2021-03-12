using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000237 RID: 567
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class StringWatermark : BaseWatermark
	{
		// Token: 0x060014D1 RID: 5329 RVA: 0x0004B0D6 File Offset: 0x000492D6
		public StringWatermark(SyncLogSession syncLogSession, string mailboxServerSyncWatermark) : base(syncLogSession, mailboxServerSyncWatermark, null, true)
		{
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x0004B0E2 File Offset: 0x000492E2
		public StringWatermark(SyncLogSession syncLogSession, ISimpleStateStorage stateStorage) : base(syncLogSession, null, stateStorage, false)
		{
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x0004B0EE File Offset: 0x000492EE
		public virtual void Load(out string watermark)
		{
			if (base.LoadedFromMailboxServer)
			{
				watermark = base.MailboxServerSyncWatermark;
				return;
			}
			if (!base.StateStorage.TryGetPropertyValue("Watermark", out watermark))
			{
				watermark = StringWatermark.DefaultWatermark;
			}
			base.StateStorageEncodedSyncWatermark = watermark;
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x0004B124 File Offset: 0x00049324
		public virtual void Save(string watermark)
		{
			SyncUtilities.ThrowIfArgumentNull("watermark", watermark);
			if (base.StateStorage.ContainsProperty("Watermark"))
			{
				base.StateStorage.ChangePropertyValue("Watermark", watermark);
			}
			else
			{
				base.StateStorage.AddProperty("Watermark", watermark);
			}
			base.StateStorageEncodedSyncWatermark = watermark;
		}

		// Token: 0x04000ACA RID: 2762
		private const string WatermarkPropertyName = "Watermark";

		// Token: 0x04000ACB RID: 2763
		private static readonly string DefaultWatermark = string.Empty;
	}
}
