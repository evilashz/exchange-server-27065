using System;
using System.Net;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.ThrottlingService.Client;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000BE2 RID: 3042
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class LimitChecker
	{
		// Token: 0x06006C08 RID: 27656 RVA: 0x001CF4D6 File Offset: 0x001CD6D6
		public LimitChecker(IRuleEvaluationContext context)
		{
			this.context = context;
		}

		// Token: 0x17001D5A RID: 7514
		// (get) Token: 0x06006C09 RID: 27657 RVA: 0x001CF4E5 File Offset: 0x001CD6E5
		// (set) Token: 0x06006C0A RID: 27658 RVA: 0x001CF4ED File Offset: 0x001CD6ED
		public IPAddress ServerIPAddress { protected get; set; }

		// Token: 0x06006C0B RID: 27659 RVA: 0x001CF4F6 File Offset: 0x001CD6F6
		public bool CheckAndIncrementContentRestrictionCount(int count, string valueToCompare)
		{
			return string.IsNullOrEmpty(valueToCompare) || 500000 >= valueToCompare.Length || this.CheckAndIncrementContentRestrictionCount(count);
		}

		// Token: 0x06006C0C RID: 27660 RVA: 0x001CF518 File Offset: 0x001CD718
		public bool CheckAndIncrementContentRestrictionCount(int count, byte[] valueToCompare)
		{
			return RuleUtil.IsNullOrEmpty(valueToCompare) || 1000000 >= valueToCompare.Length || this.CheckAndIncrementContentRestrictionCount(count);
		}

		// Token: 0x06006C0D RID: 27661 RVA: 0x001CF537 File Offset: 0x001CD737
		public bool CheckAndIncrementForwardeeCount(int count)
		{
			if (count == 0)
			{
				this.context.TraceDebug("No check passed since count is 0");
				return true;
			}
			this.LoadThrottlingPolicy();
			return !this.DoesExceedPerMessageForwardeeLimit(count) && !this.DoesExceedPerDayForwardeeLimit(count);
		}

		// Token: 0x06006C0E RID: 27662 RVA: 0x001CF56C File Offset: 0x001CD76C
		public bool DoesExceedAutoReplyLimit()
		{
			bool result = false;
			MailboxSession mailboxSession = this.context.StoreSession as MailboxSession;
			if (mailboxSession == null)
			{
				this.context.TraceDebug<StoreSession>("No throttling applied to non mailbox session {0}", this.context.StoreSession);
			}
			else
			{
				this.LoadThrottlingPolicy();
				if (!MailboxThrottle.Instance.ObtainUserSubmissionTokens(mailboxSession.MailboxOwner.MailboxInfo.Location.ServerFqdn, mailboxSession.MailboxOwner.MailboxInfo.Location.ServerVersion, this.recipientMailboxGuid, 1, this.throttlingPolicy.RecipientRateLimit, mailboxSession.ClientInfoString))
				{
					this.context.TraceError<string, Unlimited<uint>>("Rule {0} exceeded the auto reply limit of {1}.", this.context.CurrentRule.Name, this.throttlingPolicy.RecipientRateLimit);
					this.MessageTrackThrottle<Unlimited<uint>, Unlimited<uint>>("AutoReplyRate", this.throttlingPolicy.RecipientRateLimit, this.throttlingPolicy.RecipientRateLimit);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06006C0F RID: 27663 RVA: 0x001CF654 File Offset: 0x001CD854
		public bool DoesExceedXLoopMaxCount(int count)
		{
			bool flag = this.LoadHostedMailboxLimits();
			int num = flag ? 1 : 3;
			if (count >= num)
			{
				this.context.TraceDebug<int, bool>("Possible loop detected: too many X-Loop headers present. Maximum allowed is {0} since loadHostedMailboxLimits is {1}", num, flag);
				this.MessageTrackThrottle<int, int>("XLoopHeaderCount", count, num);
				return true;
			}
			return false;
		}

		// Token: 0x06006C10 RID: 27664 RVA: 0x001CF698 File Offset: 0x001CD898
		private bool CheckAndIncrementContentRestrictionCount(int count)
		{
			this.contentRestrictionCount += count;
			if (!this.LoadHostedMailboxLimits())
			{
				this.context.TraceDebug<int>("Skipping content restriction count check since we are not in Microsoft datacenter, current count is {0}", this.contentRestrictionCount);
				return true;
			}
			if (150 < this.contentRestrictionCount)
			{
				this.context.TraceDebug<int, int>("Content restriction count exceeded limit, evaluation will return false. Count {0}, Limit {1}", this.contentRestrictionCount, 150);
				this.MessageTrackThrottle<int, int>("ContentRestrictionCount", this.contentRestrictionCount, 150);
				return false;
			}
			this.context.TraceDebug<int, int>("Content restriction count is under limit, evaluation will continue. Count {0}, Limit {1}", this.contentRestrictionCount, 150);
			return true;
		}

		// Token: 0x06006C11 RID: 27665 RVA: 0x001CF730 File Offset: 0x001CD930
		private bool LoadHostedMailboxLimits()
		{
			if (this.loadHostedMailboxLimits != null)
			{
				return this.loadHostedMailboxLimits.Value;
			}
			bool result;
			try
			{
				this.loadHostedMailboxLimits = new bool?(false);
				if (VariantConfiguration.InvariantNoFlightingSnapshot.DataStorage.LoadHostedMailboxLimits.Enabled)
				{
					this.loadHostedMailboxLimits = new bool?(true);
				}
				result = this.loadHostedMailboxLimits.Value;
			}
			catch (CannotDetermineExchangeModeException argument)
			{
				this.context.TraceError<CannotDetermineExchangeModeException>("Could not determine if LoadHostedMailboxLimits feature is enabled, assuming the more restrictive HostedMailbox mode. Exception {0}", argument);
				result = true;
			}
			return result;
		}

		// Token: 0x06006C12 RID: 27666 RVA: 0x001CF7BC File Offset: 0x001CD9BC
		private void LoadThrottlingPolicy()
		{
			if (this.throttlingPolicy == null)
			{
				Result<ADRawEntry> result = this.context.RecipientCache.FindAndCacheRecipient(this.context.Recipient);
				if (result.Data == null)
				{
					this.context.TraceError("Failed to load throttling policy from recipient cache and AD, using global policy instead.");
					this.throttlingPolicy = ThrottlingPolicyCache.Singleton.GetGlobalThrottlingPolicy();
					return;
				}
				this.throttlingPolicy = ThrottlingPolicyCache.Singleton.Get(this.context.RecipientCache.OrganizationId, (ADObjectId)result.Data[ADRecipientSchema.ThrottlingPolicy]);
				this.recipientMailboxGuid = (Guid)result.Data[IADMailStorageSchema.ExchangeGuid];
				this.context.TraceDebug<Guid>("Obtained recipient mailbox GUID {0} from recipient cache as part of throttling policy loading.", this.recipientMailboxGuid);
			}
		}

		// Token: 0x06006C13 RID: 27667 RVA: 0x001CF884 File Offset: 0x001CDA84
		private bool DoesExceedPerMessageForwardeeLimit(int forwardees)
		{
			this.forwardeeCount += forwardees;
			bool result = false;
			if (this.throttlingPolicy.ForwardeeLimit.IsUnlimited || 0U == this.throttlingPolicy.ForwardeeLimit)
			{
				this.context.TraceDebug<string>("No throttling limit was set on throttling policy {0}", this.throttlingPolicy.GetIdentityString());
			}
			else if ((long)this.forwardeeCount > (long)((ulong)this.throttlingPolicy.ForwardeeLimit.Value))
			{
				this.context.TraceDebug<int, Unlimited<uint>>("Per message forwardee limit was exceeded. Forwardees {0}, Limit {1}", this.forwardeeCount, this.throttlingPolicy.ForwardeeLimit);
				this.MessageTrackThrottle<int, Unlimited<uint>>("PerMessageForwardee", this.forwardeeCount, this.throttlingPolicy.ForwardeeLimit);
				result = true;
			}
			return result;
		}

		// Token: 0x06006C14 RID: 27668 RVA: 0x001CF948 File Offset: 0x001CDB48
		private bool DoesExceedPerDayForwardeeLimit(int forwardees)
		{
			bool result = false;
			Unlimited<uint> unlimited = this.throttlingPolicy.RecipientRateLimit * 4;
			MailboxSession mailboxSession = this.context.StoreSession as MailboxSession;
			if (mailboxSession == null)
			{
				this.context.TraceDebug<StoreSession>("No throttling applied to non mailbox session {0}", this.context.StoreSession);
			}
			else if (!MailboxThrottle.Instance.ObtainRuleSubmissionTokens(mailboxSession.MailboxOwner.MailboxInfo.Location.ServerFqdn, mailboxSession.MailboxOwner.MailboxInfo.Location.ServerVersion, this.recipientMailboxGuid, forwardees, unlimited, mailboxSession.ClientInfoString))
			{
				this.context.TraceDebug<string, Unlimited<uint>>("Rule {0} exceeded the forward-by-rule limit of {1}.", this.context.CurrentRule.Name, unlimited);
				this.MessageTrackThrottle<Unlimited<uint>, Unlimited<uint>>("PerDayForwardee", unlimited, unlimited);
				result = true;
			}
			return result;
		}

		// Token: 0x06006C15 RID: 27669 RVA: 0x001CFA14 File Offset: 0x001CDC14
		protected virtual void MessageTrackThrottle<C, L>(string limitType, C count, L limit)
		{
		}

		// Token: 0x04003DC5 RID: 15813
		private const int MaxContentRestrictionCount = 150;

		// Token: 0x04003DC6 RID: 15814
		private const int MaxCharContentRestriction = 500000;

		// Token: 0x04003DC7 RID: 15815
		private const int MaxBytesContentRestriction = 1000000;

		// Token: 0x04003DC8 RID: 15816
		protected IRuleEvaluationContext context;

		// Token: 0x04003DC9 RID: 15817
		private int contentRestrictionCount;

		// Token: 0x04003DCA RID: 15818
		private int forwardeeCount;

		// Token: 0x04003DCB RID: 15819
		private bool? loadHostedMailboxLimits;

		// Token: 0x04003DCC RID: 15820
		private IThrottlingPolicy throttlingPolicy;

		// Token: 0x04003DCD RID: 15821
		private Guid recipientMailboxGuid;

		// Token: 0x02000BE3 RID: 3043
		private struct LimitType
		{
			// Token: 0x04003DCF RID: 15823
			internal const string AutoReplyRate = "AutoReplyRate";

			// Token: 0x04003DD0 RID: 15824
			internal const string XLoopHeaderCount = "XLoopHeaderCount";

			// Token: 0x04003DD1 RID: 15825
			internal const string ContentRestrictionCount = "ContentRestrictionCount";

			// Token: 0x04003DD2 RID: 15826
			internal const string PerMessageForwardee = "PerMessageForwardee";

			// Token: 0x04003DD3 RID: 15827
			internal const string PerDayForwardee = "PerDayForwardee";
		}
	}
}
