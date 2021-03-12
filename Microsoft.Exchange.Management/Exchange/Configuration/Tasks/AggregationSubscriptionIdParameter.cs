using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000CCF RID: 3279
	[Serializable]
	public sealed class AggregationSubscriptionIdParameter : IIdentityParameter
	{
		// Token: 0x06007E70 RID: 32368 RVA: 0x00204ECB File Offset: 0x002030CB
		public AggregationSubscriptionIdParameter()
		{
		}

		// Token: 0x06007E71 RID: 32369 RVA: 0x00204EEC File Offset: 0x002030EC
		public AggregationSubscriptionIdParameter(string id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (id.Length == 0)
			{
				throw new ArgumentException(Strings.ErrorEmptyParameter(base.GetType().ToString()), "identity");
			}
			this.Parse(id);
		}

		// Token: 0x06007E72 RID: 32370 RVA: 0x00204F52 File Offset: 0x00203152
		public AggregationSubscriptionIdParameter(ObjectId subId)
		{
			this.Initialize(subId);
		}

		// Token: 0x06007E73 RID: 32371 RVA: 0x00204F77 File Offset: 0x00203177
		public AggregationSubscriptionIdParameter(Mailbox mailbox)
		{
			this.mailboxId = new MailboxIdParameter(mailbox);
		}

		// Token: 0x06007E74 RID: 32372 RVA: 0x00204FA1 File Offset: 0x002031A1
		public AggregationSubscriptionIdParameter(PimSubscriptionProxy subscriptionProxy) : this(subscriptionProxy.Identity)
		{
		}

		// Token: 0x06007E75 RID: 32373 RVA: 0x00204FAF File Offset: 0x002031AF
		public AggregationSubscriptionIdParameter(INamedIdentity namedIdentity) : this(namedIdentity.Identity)
		{
		}

		// Token: 0x1700274B RID: 10059
		// (get) Token: 0x06007E76 RID: 32374 RVA: 0x00204FBD File Offset: 0x002031BD
		string IIdentityParameter.RawIdentity
		{
			get
			{
				return this.RawIdentity;
			}
		}

		// Token: 0x1700274C RID: 10060
		// (get) Token: 0x06007E77 RID: 32375 RVA: 0x00204FC5 File Offset: 0x002031C5
		public string RawIdentity
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x1700274D RID: 10061
		// (get) Token: 0x06007E78 RID: 32376 RVA: 0x00204FCD File Offset: 0x002031CD
		// (set) Token: 0x06007E79 RID: 32377 RVA: 0x00204FD5 File Offset: 0x002031D5
		public string Name
		{
			get
			{
				return this.subscriptionName;
			}
			set
			{
				this.subscriptionName = value;
			}
		}

		// Token: 0x1700274E RID: 10062
		// (get) Token: 0x06007E7A RID: 32378 RVA: 0x00204FDE File Offset: 0x002031DE
		// (set) Token: 0x06007E7B RID: 32379 RVA: 0x00204FE6 File Offset: 0x002031E6
		public Guid SubscriptionId
		{
			get
			{
				return this.subscriptionId;
			}
			set
			{
				this.subscriptionId = value;
			}
		}

		// Token: 0x1700274F RID: 10063
		// (get) Token: 0x06007E7C RID: 32380 RVA: 0x00204FEF File Offset: 0x002031EF
		public MailboxIdParameter MailboxIdParameter
		{
			get
			{
				return this.mailboxId;
			}
		}

		// Token: 0x17002750 RID: 10064
		// (get) Token: 0x06007E7D RID: 32381 RVA: 0x00204FF7 File Offset: 0x002031F7
		// (set) Token: 0x06007E7E RID: 32382 RVA: 0x00204FFF File Offset: 0x002031FF
		public SmtpAddress EmailAddress
		{
			get
			{
				return this.emailAddress;
			}
			set
			{
				this.emailAddress = value;
			}
		}

		// Token: 0x17002751 RID: 10065
		// (get) Token: 0x06007E7F RID: 32383 RVA: 0x00205008 File Offset: 0x00203208
		public string GuidIdentityString
		{
			get
			{
				if (this.subscriptionIdentity != null)
				{
					return this.subscriptionIdentity.GuidIdentityString;
				}
				throw new InvalidOperationException("SubscriptionIdentity should not be null.");
			}
		}

		// Token: 0x17002752 RID: 10066
		// (get) Token: 0x06007E80 RID: 32384 RVA: 0x00205028 File Offset: 0x00203228
		// (set) Token: 0x06007E81 RID: 32385 RVA: 0x00205030 File Offset: 0x00203230
		internal AggregationSubscriptionType? SubscriptionType
		{
			get
			{
				return this.subscriptionType;
			}
			set
			{
				this.subscriptionType = value;
			}
		}

		// Token: 0x17002753 RID: 10067
		// (get) Token: 0x06007E82 RID: 32386 RVA: 0x00205039 File Offset: 0x00203239
		// (set) Token: 0x06007E83 RID: 32387 RVA: 0x00205041 File Offset: 0x00203241
		internal AggregationType? AggregationType
		{
			get
			{
				return this.aggregationType;
			}
			set
			{
				this.aggregationType = value;
			}
		}

		// Token: 0x17002754 RID: 10068
		// (get) Token: 0x06007E84 RID: 32388 RVA: 0x0020504A File Offset: 0x0020324A
		internal AggregationSubscriptionIdentity SubscriptionIdentity
		{
			get
			{
				return this.subscriptionIdentity;
			}
		}

		// Token: 0x17002755 RID: 10069
		// (get) Token: 0x06007E85 RID: 32389 RVA: 0x00205052 File Offset: 0x00203252
		internal bool IsUniqueIdentity
		{
			get
			{
				return this.SubscriptionIdentity != null || this.Name != null || this.EmailAddress != SmtpAddress.Empty || this.SubscriptionId != Guid.Empty;
			}
		}

		// Token: 0x06007E86 RID: 32390 RVA: 0x00205088 File Offset: 0x00203288
		public void Initialize(ObjectId subId)
		{
			AggregationSubscriptionIdentity aggregationSubscriptionIdentity = subId as AggregationSubscriptionIdentity;
			if (aggregationSubscriptionIdentity != null)
			{
				this.subscriptionIdentity = aggregationSubscriptionIdentity;
				this.mailboxId = new MailboxIdParameter(aggregationSubscriptionIdentity.AdUserId);
			}
		}

		// Token: 0x06007E87 RID: 32391 RVA: 0x002050B8 File Offset: 0x002032B8
		public override string ToString()
		{
			if (this.subscriptionIdentity != null)
			{
				return this.subscriptionIdentity.ToString();
			}
			if (this.subscriptionName != null)
			{
				return this.subscriptionName;
			}
			if (this.emailAddress != SmtpAddress.Empty)
			{
				return this.emailAddress.ToString();
			}
			if (this.subscriptionId != Guid.Empty)
			{
				return this.subscriptionId.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06007E88 RID: 32392 RVA: 0x00205138 File Offset: 0x00203338
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session) where T : IConfigurable, new()
		{
			LocalizedString? localizedString;
			return ((IIdentityParameter)this).GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x06007E89 RID: 32393 RVA: 0x00205150 File Offset: 0x00203350
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			notFoundReason = null;
			AggregationSubscriptionQueryFilter filter = new AggregationSubscriptionQueryFilter(this);
			return session.FindPaged<T>(filter, rootId, false, null, 0);
		}

		// Token: 0x06007E8A RID: 32394 RVA: 0x00205178 File Offset: 0x00203378
		private void Parse(string identity)
		{
			identity = identity.Trim();
			if (string.IsNullOrEmpty(identity))
			{
				return;
			}
			if (this.TryParseDNAndGuid(identity))
			{
				return;
			}
			try
			{
				this.emailAddress = SmtpAddress.Parse(identity);
			}
			catch (FormatException)
			{
			}
			if (this.emailAddress == SmtpAddress.Empty && !GuidHelper.TryParseGuid(identity, out this.subscriptionId))
			{
				this.subscriptionName = identity;
			}
		}

		// Token: 0x06007E8B RID: 32395 RVA: 0x002051E8 File Offset: 0x002033E8
		private bool TryParseDNAndGuid(string input)
		{
			if (input == null || input.Length < 3)
			{
				return false;
			}
			int num = input.LastIndexOf('/');
			if (num == -1)
			{
				return false;
			}
			this.mailboxId = new MailboxIdParameter(input.Substring(0, num));
			if (!GuidHelper.TryParseGuid(input.Substring(num + 1), out this.subscriptionId))
			{
				return false;
			}
			this.subscriptionIdentity = new AggregationSubscriptionIdentity(this.mailboxId.InternalADObjectId, this.subscriptionId);
			return true;
		}

		// Token: 0x04003E2F RID: 15919
		private AggregationSubscriptionIdentity subscriptionIdentity;

		// Token: 0x04003E30 RID: 15920
		private MailboxIdParameter mailboxId;

		// Token: 0x04003E31 RID: 15921
		private string subscriptionName;

		// Token: 0x04003E32 RID: 15922
		private Guid subscriptionId = Guid.Empty;

		// Token: 0x04003E33 RID: 15923
		private SmtpAddress emailAddress = SmtpAddress.Empty;

		// Token: 0x04003E34 RID: 15924
		private AggregationSubscriptionType? subscriptionType;

		// Token: 0x04003E35 RID: 15925
		private AggregationType? aggregationType;
	}
}
