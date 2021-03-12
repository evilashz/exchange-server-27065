using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription
{
	// Token: 0x020000B3 RID: 179
	[Serializable]
	public class AggregationSubscriptionIdentity : ObjectId, IEquatable<AggregationSubscriptionIdentity>
	{
		// Token: 0x0600050F RID: 1295 RVA: 0x00019B54 File Offset: 0x00017D54
		public AggregationSubscriptionIdentity()
		{
			this.subscriptionId = Guid.Empty;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00019B68 File Offset: 0x00017D68
		public AggregationSubscriptionIdentity(string id)
		{
			SyncUtilities.ThrowIfArgumentNull("id", id);
			int num = id.LastIndexOf('/');
			if (num <= 0 || num == id.Length - 1)
			{
				throw new ArgumentException(Strings.InvalidAggregationSubscriptionIdentity, "id");
			}
			string input = id.Substring(0, num);
			string g = id.Substring(num + 1);
			this.userId = ADObjectId.ParseDnOrGuid(input);
			this.subscriptionId = new Guid(g);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00019BDE File Offset: 0x00017DDE
		public AggregationSubscriptionIdentity(ADObjectId userId, Guid subscriptionId)
		{
			SyncUtilities.ThrowIfArgumentNull("userId", userId);
			SyncUtilities.ThrowIfGuidEmpty("subscriptionId", subscriptionId);
			this.userId = userId;
			this.subscriptionId = subscriptionId;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00019C0A File Offset: 0x00017E0A
		public AggregationSubscriptionIdentity(Guid subscriptionId, string legacyDN, string primaryMailboxLegacyDN)
		{
			SyncUtilities.ThrowIfGuidEmpty("subscriptionId", subscriptionId);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("legacyDN", legacyDN);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("primaryMailboxLegacyDN", primaryMailboxLegacyDN);
			this.subscriptionId = subscriptionId;
			this.legacyDN = legacyDN;
			this.primaryMailboxLegacyDN = primaryMailboxLegacyDN;
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x00019C48 File Offset: 0x00017E48
		// (set) Token: 0x06000514 RID: 1300 RVA: 0x00019C50 File Offset: 0x00017E50
		public ADObjectId AdUserId
		{
			get
			{
				return this.userId;
			}
			internal set
			{
				this.userId = value;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00019C59 File Offset: 0x00017E59
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x00019C61 File Offset: 0x00017E61
		public Guid SubscriptionId
		{
			get
			{
				return this.subscriptionId;
			}
			internal set
			{
				this.subscriptionId = value;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00019C6A File Offset: 0x00017E6A
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x00019C72 File Offset: 0x00017E72
		public string LegacyDN
		{
			get
			{
				return this.legacyDN;
			}
			internal set
			{
				this.legacyDN = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x00019C7B File Offset: 0x00017E7B
		// (set) Token: 0x0600051A RID: 1306 RVA: 0x00019C83 File Offset: 0x00017E83
		public string PrimaryMailboxLegacyDN
		{
			get
			{
				return this.primaryMailboxLegacyDN;
			}
			internal set
			{
				this.primaryMailboxLegacyDN = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00019C8C File Offset: 0x00017E8C
		public string GuidIdentityString
		{
			get
			{
				if (this.userId == null)
				{
					throw new InvalidOperationException("adUserId should not be null.");
				}
				if (this.subscriptionId == Guid.Empty)
				{
					throw new InvalidOperationException("subscriptionId should not be an Empty Guid.");
				}
				return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", new object[]
				{
					this.userId.ObjectGuid.ToString(),
					'/',
					this.subscriptionId
				});
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00019D14 File Offset: 0x00017F14
		public override byte[] GetBytes()
		{
			return null;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00019D18 File Offset: 0x00017F18
		public override string ToString()
		{
			if (this.userId == null || this.subscriptionId == Guid.Empty)
			{
				return string.Empty;
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", new object[]
			{
				this.userId.ToDNString(),
				'/',
				this.subscriptionId
			});
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00019D82 File Offset: 0x00017F82
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AggregationSubscriptionIdentity);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00019D90 File Offset: 0x00017F90
		public bool Equals(AggregationSubscriptionIdentity other)
		{
			return other != null && this.userId != null && this.userId.Equals(other.userId) && this.subscriptionId == other.SubscriptionId;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00019DC3 File Offset: 0x00017FC3
		public override int GetHashCode()
		{
			if (this.userId == null)
			{
				return 0;
			}
			return this.userId.GetHashCode() | this.subscriptionId.GetHashCode();
		}

		// Token: 0x040002C7 RID: 711
		private const char Separator = '/';

		// Token: 0x040002C8 RID: 712
		private ADObjectId userId;

		// Token: 0x040002C9 RID: 713
		private Guid subscriptionId;

		// Token: 0x040002CA RID: 714
		private string legacyDN;

		// Token: 0x040002CB RID: 715
		private string primaryMailboxLegacyDN;
	}
}
