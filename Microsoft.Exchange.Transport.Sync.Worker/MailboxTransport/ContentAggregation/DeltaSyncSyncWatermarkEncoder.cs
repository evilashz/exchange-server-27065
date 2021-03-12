using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.Protocols.DeltaSync;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000077 RID: 119
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DeltaSyncSyncWatermarkEncoder
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x00019080 File Offset: 0x00017280
		public string Encode(string folderSyncKey, string emailSyncKey)
		{
			SyncUtilities.ThrowIfArgumentNull("folderSyncKey", folderSyncKey);
			SyncUtilities.ThrowIfArgumentNull("emailSyncKey", emailSyncKey);
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}", new object[]
			{
				this.elementEncoder.Encode(folderSyncKey),
				this.elementEncoder.Encode(emailSyncKey)
			});
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x000190D8 File Offset: 0x000172D8
		public void Decode(string toDecode, SyncLogSession syncLogSession, out string decodedFolderSyncKey, out string decodedEmailSyncKey)
		{
			SyncUtilities.ThrowIfArgumentNull("toDecode", toDecode);
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			if (toDecode.Length == 0)
			{
				this.SetToDefaultSyncKeys(out decodedFolderSyncKey, out decodedEmailSyncKey);
				syncLogSession.LogDebugging((TSLID)510UL, "DeltaSyncSyncWatermarkEncoder.Decode: toDecode was empty string. Using defaults.", new object[0]);
				return;
			}
			int offset;
			if (!this.elementEncoder.TryDecodeElementFrom(toDecode, 0, out decodedFolderSyncKey, out offset))
			{
				this.SetToDefaultSyncKeys(out decodedFolderSyncKey, out decodedEmailSyncKey);
				syncLogSession.LogError((TSLID)511UL, "DeltaSyncSyncWatermarkEncoder.Decode: unable to parse folder sync key. Using defaults.", new object[0]);
				return;
			}
			int num;
			if (!this.elementEncoder.TryDecodeElementFrom(toDecode, offset, out decodedEmailSyncKey, out num))
			{
				this.SetToDefaultSyncKeys(out decodedFolderSyncKey, out decodedEmailSyncKey);
				syncLogSession.LogError((TSLID)512UL, "DeltaSyncSyncWatermarkEncoder.Decode: unable to parse email sync key. Using defaults.", new object[0]);
			}
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00019199 File Offset: 0x00017399
		private void SetToDefaultSyncKeys(out string decodedFolderSyncKey, out string decodedEmailSyncKey)
		{
			decodedFolderSyncKey = DeltaSyncCommon.DefaultSyncKey;
			decodedEmailSyncKey = DeltaSyncCommon.DefaultSyncKey;
		}

		// Token: 0x040002E7 RID: 743
		private readonly SyncWatermarkElementEncoder elementEncoder = new SyncWatermarkElementEncoder();
	}
}
