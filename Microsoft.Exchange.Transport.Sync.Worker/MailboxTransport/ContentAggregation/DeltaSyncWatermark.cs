using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.Protocols.DeltaSync;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000236 RID: 566
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DeltaSyncWatermark : BaseWatermark
	{
		// Token: 0x060014CD RID: 5325 RVA: 0x0004AF46 File Offset: 0x00049146
		public DeltaSyncWatermark(SyncLogSession syncLogSession, string mailboxServerSyncWatermark) : base(syncLogSession, mailboxServerSyncWatermark, null, true)
		{
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x0004AF5D File Offset: 0x0004915D
		public DeltaSyncWatermark(SyncLogSession syncLogSession, ISimpleStateStorage stateStorage) : base(syncLogSession, null, stateStorage, false)
		{
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x0004AF74 File Offset: 0x00049174
		public void Load(out string folderSyncKey, out string emailSyncKey)
		{
			if (base.LoadedFromMailboxServer)
			{
				this.encoder.Decode(base.MailboxServerSyncWatermark, base.SyncLogSession, out folderSyncKey, out emailSyncKey);
				return;
			}
			if (!base.StateStorage.TryGetPropertyValue("FolderSyncKey", out folderSyncKey))
			{
				folderSyncKey = DeltaSyncCommon.DefaultSyncKey;
			}
			if (!base.StateStorage.TryGetPropertyValue("EmailSyncKey", out emailSyncKey))
			{
				emailSyncKey = DeltaSyncCommon.DefaultSyncKey;
			}
			base.StateStorageEncodedSyncWatermark = this.encoder.Encode(folderSyncKey, emailSyncKey);
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x0004AFEC File Offset: 0x000491EC
		public void Save(string folderSyncKey, string emailSyncKey)
		{
			SyncUtilities.ThrowIfArgumentNull("folderSyncKey", folderSyncKey);
			SyncUtilities.ThrowIfArgumentNull("emailSyncKey", emailSyncKey);
			if (base.LoadedFromMailboxServer)
			{
				base.MailboxServerSyncWatermark = this.encoder.Encode(folderSyncKey, emailSyncKey);
				base.SyncLogSession.LogVerbose((TSLID)1243UL, "Saved Watermark that was loaded from Mailbox Server: {0}", new object[]
				{
					base.MailboxServerSyncWatermark
				});
				return;
			}
			if (base.StateStorage.ContainsProperty("FolderSyncKey"))
			{
				base.StateStorage.ChangePropertyValue("FolderSyncKey", folderSyncKey);
			}
			else
			{
				base.StateStorage.AddProperty("FolderSyncKey", folderSyncKey);
			}
			if (base.StateStorage.ContainsProperty("EmailSyncKey"))
			{
				base.StateStorage.ChangePropertyValue("EmailSyncKey", emailSyncKey);
			}
			else
			{
				base.StateStorage.AddProperty("EmailSyncKey", emailSyncKey);
			}
			base.StateStorageEncodedSyncWatermark = this.encoder.Encode(folderSyncKey, emailSyncKey);
		}

		// Token: 0x04000AC7 RID: 2759
		private const string FolderSyncKeyPropertyName = "FolderSyncKey";

		// Token: 0x04000AC8 RID: 2760
		private const string EmailSyncKeyPropertyName = "EmailSyncKey";

		// Token: 0x04000AC9 RID: 2761
		private readonly DeltaSyncSyncWatermarkEncoder encoder = new DeltaSyncSyncWatermarkEncoder();
	}
}
