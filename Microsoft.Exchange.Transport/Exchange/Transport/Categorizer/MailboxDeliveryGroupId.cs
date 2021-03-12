using System;
using System.Globalization;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200023F RID: 575
	internal struct MailboxDeliveryGroupId : IEquatable<MailboxDeliveryGroupId>
	{
		// Token: 0x06001930 RID: 6448 RVA: 0x00065BEA File Offset: 0x00063DEA
		public MailboxDeliveryGroupId(string stringId)
		{
			RoutingUtils.ThrowIfNullOrEmpty(stringId, "stringId");
			this.stringId = stringId;
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x00065C00 File Offset: 0x00063E00
		public MailboxDeliveryGroupId(string siteName, int version)
		{
			RoutingUtils.ThrowIfNullOrEmpty(siteName, "siteName");
			this.stringId = string.Format(CultureInfo.InvariantCulture, "site:{0}; version:{1}", new object[]
			{
				siteName.ToLowerInvariant(),
				version
			});
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x00065C47 File Offset: 0x00063E47
		public override int GetHashCode()
		{
			return this.stringId.GetHashCode();
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x00065C54 File Offset: 0x00063E54
		public bool Equals(MailboxDeliveryGroupId other)
		{
			return this.stringId.Equals(other.stringId, StringComparison.Ordinal);
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x00065C69 File Offset: 0x00063E69
		public override bool Equals(object other)
		{
			return other != null && other is MailboxDeliveryGroupId && this.Equals((MailboxDeliveryGroupId)other);
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x00065C84 File Offset: 0x00063E84
		public override string ToString()
		{
			return this.stringId;
		}

		// Token: 0x04000C0E RID: 3086
		private const string Format = "site:{0}; version:{1}";

		// Token: 0x04000C0F RID: 3087
		private string stringId;
	}
}
