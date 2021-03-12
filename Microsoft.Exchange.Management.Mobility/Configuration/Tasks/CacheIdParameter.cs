using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Management.Aggregation;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000046 RID: 70
	[Serializable]
	public sealed class CacheIdParameter : IIdentityParameter
	{
		// Token: 0x0600029A RID: 666 RVA: 0x0000C0F3 File Offset: 0x0000A2F3
		public CacheIdParameter()
		{
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000C0FB File Offset: 0x0000A2FB
		public CacheIdParameter(INamedIdentity namedIdentity) : this(namedIdentity.Identity)
		{
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000C109 File Offset: 0x0000A309
		public CacheIdParameter(string identity)
		{
			this.mailboxId = new MailboxIdParameter(identity);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000C11D File Offset: 0x0000A31D
		public CacheIdParameter(PimSubscriptionProxy subscriptionProxy) : this(subscriptionProxy.Subscription.AdUserId)
		{
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000C130 File Offset: 0x0000A330
		public CacheIdParameter(SubscriptionsCache cache)
		{
			this.Initialize(cache.Identity);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000C144 File Offset: 0x0000A344
		public CacheIdParameter(AggregationSubscriptionIdentity subscriptionIdentity) : this(subscriptionIdentity.AdUserId)
		{
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000C152 File Offset: 0x0000A352
		public CacheIdParameter(AggregationSubscriptionIdParameter subscriptionIdParameter) : this(subscriptionIdParameter.MailboxIdParameter)
		{
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000C160 File Offset: 0x0000A360
		public CacheIdParameter(MailboxIdParameter mailboxIdParameter)
		{
			this.mailboxId = mailboxIdParameter;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000C16F File Offset: 0x0000A36F
		public CacheIdParameter(Mailbox mailbox) : this(mailbox.Id)
		{
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000C17D File Offset: 0x0000A37D
		public CacheIdParameter(ADObjectId objectId)
		{
			this.Initialize(objectId);
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000C18C File Offset: 0x0000A38C
		public string RawIdentity
		{
			get
			{
				return this.mailboxId.ToString();
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000C199 File Offset: 0x0000A399
		internal MailboxIdParameter MailboxId
		{
			get
			{
				return this.mailboxId;
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000C1A1 File Offset: 0x0000A3A1
		public override string ToString()
		{
			return this.mailboxId.ToString();
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000C1B0 File Offset: 0x0000A3B0
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session) where T : IConfigurable, new()
		{
			LocalizedString? localizedString;
			return this.GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000C1C8 File Offset: 0x0000A3C8
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			notFoundReason = null;
			return session.FindPaged<T>(null, rootId, false, null, 0);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000C1DD File Offset: 0x0000A3DD
		public void Initialize(ObjectId objectId)
		{
			this.mailboxId = new MailboxIdParameter((ADObjectId)objectId);
		}

		// Token: 0x040000BC RID: 188
		private MailboxIdParameter mailboxId;
	}
}
