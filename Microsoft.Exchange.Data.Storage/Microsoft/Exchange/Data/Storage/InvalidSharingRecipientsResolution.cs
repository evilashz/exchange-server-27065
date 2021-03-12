using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D9F RID: 3487
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class InvalidSharingRecipientsResolution
	{
		// Token: 0x1700200C RID: 8204
		// (get) Token: 0x060077E4 RID: 30692 RVA: 0x00211347 File Offset: 0x0020F547
		public InvalidSharingRecipientsResolutionType Resolution
		{
			get
			{
				return this.resolution;
			}
		}

		// Token: 0x1700200D RID: 8205
		// (get) Token: 0x060077E5 RID: 30693 RVA: 0x00211350 File Offset: 0x0020F550
		public StoreObjectId FolderId
		{
			get
			{
				InvalidSharingRecipientsResolutionType invalidSharingRecipientsResolutionType = this.Resolution;
				if (invalidSharingRecipientsResolutionType == InvalidSharingRecipientsResolutionType.PublishAndTryAgain)
				{
					return this.folderId;
				}
				throw new InvalidOperationException("FolderId");
			}
		}

		// Token: 0x1700200E RID: 8206
		// (get) Token: 0x060077E6 RID: 30694 RVA: 0x0021137C File Offset: 0x0020F57C
		public string BrowseUrl
		{
			get
			{
				InvalidSharingRecipientsResolutionType invalidSharingRecipientsResolutionType = this.Resolution;
				if (invalidSharingRecipientsResolutionType == InvalidSharingRecipientsResolutionType.SendPublishLinks)
				{
					return this.browseUrl;
				}
				throw new InvalidOperationException("BrowseUrl");
			}
		}

		// Token: 0x1700200F RID: 8207
		// (get) Token: 0x060077E7 RID: 30695 RVA: 0x002113A8 File Offset: 0x0020F5A8
		public string ICalUrl
		{
			get
			{
				InvalidSharingRecipientsResolutionType invalidSharingRecipientsResolutionType = this.Resolution;
				if (invalidSharingRecipientsResolutionType == InvalidSharingRecipientsResolutionType.SendPublishLinks)
				{
					return this.iCalUrl;
				}
				throw new InvalidOperationException("ICalUrl");
			}
		}

		// Token: 0x060077E8 RID: 30696 RVA: 0x002113D1 File Offset: 0x0020F5D1
		internal InvalidSharingRecipientsResolution(StoreObjectId folderId)
		{
			Util.ThrowOnNullArgument(folderId, "folderId");
			this.resolution = InvalidSharingRecipientsResolutionType.PublishAndTryAgain;
			this.folderId = folderId;
		}

		// Token: 0x060077E9 RID: 30697 RVA: 0x002113F2 File Offset: 0x0020F5F2
		internal InvalidSharingRecipientsResolution(string browseUrl, string iCalUrl)
		{
			Util.ThrowOnNullOrEmptyArgument(browseUrl, "browseUrl");
			Util.ThrowOnNullOrEmptyArgument(iCalUrl, "iCalUrl");
			this.resolution = InvalidSharingRecipientsResolutionType.SendPublishLinks;
			this.browseUrl = browseUrl;
			this.iCalUrl = iCalUrl;
		}

		// Token: 0x060077EA RID: 30698 RVA: 0x00211428 File Offset: 0x0020F628
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("Resolution: " + this.Resolution.ToString());
			switch (this.Resolution)
			{
			case InvalidSharingRecipientsResolutionType.PublishAndTryAgain:
				stringBuilder.Append(", FolderId:" + this.FolderId.ToBase64String());
				break;
			case InvalidSharingRecipientsResolutionType.SendPublishLinks:
				stringBuilder.Append(", BrowseUrl:" + this.BrowseUrl);
				stringBuilder.Append(", ICalUrl:" + this.ICalUrl);
				break;
			default:
				stringBuilder.Append(" <Unknown Resolution Value>");
				break;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04005301 RID: 21249
		private readonly InvalidSharingRecipientsResolutionType resolution;

		// Token: 0x04005302 RID: 21250
		private readonly StoreObjectId folderId;

		// Token: 0x04005303 RID: 21251
		private readonly string browseUrl;

		// Token: 0x04005304 RID: 21252
		private readonly string iCalUrl;
	}
}
