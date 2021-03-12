using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DB3 RID: 3507
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct PublishingSubscriptionKey : IEquatable<PublishingSubscriptionKey>
	{
		// Token: 0x0600788F RID: 30863 RVA: 0x002142C3 File Offset: 0x002124C3
		public PublishingSubscriptionKey(Uri publishingUrl)
		{
			this.publishingUrl = publishingUrl;
		}

		// Token: 0x17002042 RID: 8258
		// (get) Token: 0x06007890 RID: 30864 RVA: 0x002142CC File Offset: 0x002124CC
		public Uri PublishingUrl
		{
			get
			{
				return this.publishingUrl;
			}
		}

		// Token: 0x06007891 RID: 30865 RVA: 0x002142D4 File Offset: 0x002124D4
		public bool Equals(PublishingSubscriptionKey other)
		{
			return object.Equals(this.PublishingUrl, other.PublishingUrl);
		}

		// Token: 0x06007892 RID: 30866 RVA: 0x002142E8 File Offset: 0x002124E8
		public override string ToString()
		{
			return this.PublishingUrl.OriginalString;
		}

		// Token: 0x04005355 RID: 21333
		private Uri publishingUrl;
	}
}
