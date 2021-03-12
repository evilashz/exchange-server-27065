using System;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DE3 RID: 3555
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SubscribeResultsInternal : SubscribeResults
	{
		// Token: 0x06007A55 RID: 31317 RVA: 0x0021D080 File Offset: 0x0021B280
		internal SubscribeResultsInternal(SharingDataType dataType, string initiatorSmtpAddress, string initiatorName, string remoteFolderName, StoreObjectId remoteFolderId, byte[] remoteMailboxId) : base(dataType, initiatorSmtpAddress, initiatorName, remoteFolderName, null, false, null)
		{
			this.RemoteFolderId = remoteFolderId;
			this.RemoteMailboxId = remoteMailboxId;
		}

		// Token: 0x170020BA RID: 8378
		// (get) Token: 0x06007A56 RID: 31318 RVA: 0x0021D0A0 File Offset: 0x0021B2A0
		// (set) Token: 0x06007A57 RID: 31319 RVA: 0x0021D0A8 File Offset: 0x0021B2A8
		public StoreObjectId RemoteFolderId { get; private set; }

		// Token: 0x170020BB RID: 8379
		// (get) Token: 0x06007A58 RID: 31320 RVA: 0x0021D0B1 File Offset: 0x0021B2B1
		// (set) Token: 0x06007A59 RID: 31321 RVA: 0x0021D0B9 File Offset: 0x0021B2B9
		public byte[] RemoteMailboxId { get; private set; }

		// Token: 0x06007A5A RID: 31322 RVA: 0x0021D0C4 File Offset: 0x0021B2C4
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				base.ToString(),
				", RemoteFolderId=",
				this.RemoteFolderId,
				", RemoteMailboxId=",
				HexConverter.ByteArrayToHexString(this.RemoteMailboxId)
			});
		}
	}
}
