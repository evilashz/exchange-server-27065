using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Autodiscover.ConfigurationCache
{
	// Token: 0x02000026 RID: 38
	public struct OfflineAddressBookCacheKey : IComparable, IComparable<OfflineAddressBookCacheKey>, IEquatable<OfflineAddressBookCacheKey>
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00007333 File Offset: 0x00005533
		[CLSCompliant(false)]
		public OfflineAddressBookCacheKey(ADObjectId key, FilterType type)
		{
			this = new OfflineAddressBookCacheKey(null, key, type);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000733E File Offset: 0x0000553E
		[CLSCompliant(false)]
		public OfflineAddressBookCacheKey(OrganizationId orgId, ADObjectId key, FilterType type)
		{
			this.organizationId = orgId;
			this.key = key;
			this.type = type;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00007355 File Offset: 0x00005555
		[CLSCompliant(false)]
		public OrganizationId OrganizationId
		{
			get
			{
				return this.organizationId;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600012E RID: 302 RVA: 0x0000735D File Offset: 0x0000555D
		[CLSCompliant(false)]
		public ADObjectId Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00007365 File Offset: 0x00005565
		public FilterType FilterType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000736D File Offset: 0x0000556D
		public int CompareTo(object other)
		{
			if (other is OfflineAddressBookCacheKey)
			{
				return this.CompareTo((OfflineAddressBookCacheKey)other);
			}
			throw new InvalidOperationException("type mismatch");
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00007390 File Offset: 0x00005590
		public int CompareTo(OfflineAddressBookCacheKey other)
		{
			string text = (this.key == null) ? string.Empty : this.key.ToString();
			string strB = (other.key == null) ? string.Empty : other.key.ToString();
			int num = text.CompareTo(strB);
			if (num == 0)
			{
				return this.type.CompareTo(other.type);
			}
			return num;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000073FE File Offset: 0x000055FE
		public bool Equals(OfflineAddressBookCacheKey other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000740A File Offset: 0x0000560A
		public override bool Equals(object other)
		{
			if (other is OfflineAddressBookCacheKey)
			{
				return this.Equals((OfflineAddressBookCacheKey)other);
			}
			throw new InvalidOperationException("type mismatch");
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000742B File Offset: 0x0000562B
		public override int GetHashCode()
		{
			return ((this.key == null) ? 0 : this.key.GetHashCode()) ^ this.type.GetHashCode();
		}

		// Token: 0x04000154 RID: 340
		private readonly OrganizationId organizationId;

		// Token: 0x04000155 RID: 341
		private readonly ADObjectId key;

		// Token: 0x04000156 RID: 342
		private readonly FilterType type;
	}
}
