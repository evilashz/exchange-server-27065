using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DE4 RID: 3556
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SubscribeResultsWebCal : SubscribeResults
	{
		// Token: 0x06007A5B RID: 31323 RVA: 0x0021D10E File Offset: 0x0021B30E
		internal SubscribeResultsWebCal(SharingDataType dataType, string initiatorSmtpAddress, string initiatorName, string remoteFolderName, Uri url, StoreObjectId localFolderId, bool localFolderCreated, LocalizedString localFolderName) : base(dataType, initiatorSmtpAddress, initiatorName, remoteFolderName, localFolderId, localFolderCreated, localFolderName)
		{
			this.Url = url;
		}

		// Token: 0x170020BC RID: 8380
		// (get) Token: 0x06007A5C RID: 31324 RVA: 0x0021D12E File Offset: 0x0021B32E
		// (set) Token: 0x06007A5D RID: 31325 RVA: 0x0021D136 File Offset: 0x0021B336
		public Uri Url { get; private set; }

		// Token: 0x06007A5E RID: 31326 RVA: 0x0021D140 File Offset: 0x0021B340
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				base.ToString(),
				", Url=",
				this.Url,
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
