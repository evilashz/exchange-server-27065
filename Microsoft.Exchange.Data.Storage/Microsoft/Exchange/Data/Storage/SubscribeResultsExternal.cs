using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DE2 RID: 3554
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SubscribeResultsExternal : SubscribeResults
	{
		// Token: 0x06007A51 RID: 31313 RVA: 0x0021CFDE File Offset: 0x0021B1DE
		internal SubscribeResultsExternal(SharingDataType dataType, string initiatorSmtpAddress, string initiatorName, string remoteFolderName, string remoteFolderId, StoreObjectId localFolderId, bool localFolderCreated, LocalizedString localFolderName) : base(dataType, initiatorSmtpAddress, initiatorName, remoteFolderName, localFolderId, localFolderCreated, localFolderName)
		{
			this.RemoteFolderId = remoteFolderId;
		}

		// Token: 0x170020B9 RID: 8377
		// (get) Token: 0x06007A52 RID: 31314 RVA: 0x0021CFFE File Offset: 0x0021B1FE
		// (set) Token: 0x06007A53 RID: 31315 RVA: 0x0021D006 File Offset: 0x0021B206
		public string RemoteFolderId { get; private set; }

		// Token: 0x06007A54 RID: 31316 RVA: 0x0021D010 File Offset: 0x0021B210
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				base.ToString(),
				", RemoteFolderId=",
				this.RemoteFolderId,
				", LocalFolderId=",
				base.LocalFolderId,
				", LocalFolderName=",
				base.LocalFolderName,
				", LocalFolderCreated=",
				base.LocalFolderCreated.ToString()
			});
		}
	}
}
