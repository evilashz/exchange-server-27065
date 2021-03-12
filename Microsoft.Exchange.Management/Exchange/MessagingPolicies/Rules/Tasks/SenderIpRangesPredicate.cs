using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BEA RID: 3050
	[Serializable]
	public class SenderIpRangesPredicate : TransportRulePredicate, IEquatable<SenderIpRangesPredicate>
	{
		// Token: 0x06007300 RID: 29440 RVA: 0x001D41DE File Offset: 0x001D23DE
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<IPRange>(this.IpRanges);
		}

		// Token: 0x06007301 RID: 29441 RVA: 0x001D41EB File Offset: 0x001D23EB
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as SenderIpRangesPredicate)));
		}

		// Token: 0x06007302 RID: 29442 RVA: 0x001D4224 File Offset: 0x001D2424
		public bool Equals(SenderIpRangesPredicate other)
		{
			if (this.IpRanges == null)
			{
				return null == other.IpRanges;
			}
			return this.IpRanges.SequenceEqual(other.IpRanges);
		}

		// Token: 0x1700236F RID: 9071
		// (get) Token: 0x06007303 RID: 29443 RVA: 0x001D4249 File Offset: 0x001D2449
		// (set) Token: 0x06007304 RID: 29444 RVA: 0x001D4251 File Offset: 0x001D2451
		[ConditionParameterName("SenderIpRanges")]
		[ExceptionParameterName("ExceptIfSenderIpRanges")]
		[LocDescription(RulesTasksStrings.IDs.SenderIpRangesDescription)]
		[LocDisplayName(RulesTasksStrings.IDs.SenderIpRangesDisplayName)]
		public List<IPRange> IpRanges { get; set; }

		// Token: 0x17002370 RID: 9072
		// (get) Token: 0x06007305 RID: 29445 RVA: 0x001D4264 File Offset: 0x001D2464
		internal override string Description
		{
			get
			{
				List<string> stringValues = (from range in this.IpRanges
				select range.ToString()).ToList<string>();
				string lists = RuleDescription.BuildDescriptionStringFromStringArray(stringValues, RulesTasksStrings.RuleDescriptionOrDelimiter, base.MaxDescriptionListLength);
				return RulesTasksStrings.RuleDescriptionIpRanges(lists);
			}
		}

		// Token: 0x06007306 RID: 29446 RVA: 0x001D42C1 File Offset: 0x001D24C1
		public SenderIpRangesPredicate()
		{
		}

		// Token: 0x06007307 RID: 29447 RVA: 0x001D42C9 File Offset: 0x001D24C9
		internal SenderIpRangesPredicate(IEnumerable<IPRange> ipRanges)
		{
			this.IpRanges = ipRanges.ToList<IPRange>();
		}

		// Token: 0x06007308 RID: 29448 RVA: 0x001D42E8 File Offset: 0x001D24E8
		internal override Condition ToInternalCondition()
		{
			ShortList<string> valueEntries = new ShortList<string>(from x in this.IpRanges
			select x.ToString());
			return TransportRuleParser.Instance.CreatePredicate("ipMatch", TransportRuleParser.Instance.CreateProperty("Message.SenderIp"), valueEntries);
		}

		// Token: 0x06007309 RID: 29449 RVA: 0x001D4344 File Offset: 0x001D2544
		internal static TransportRulePredicate CreateFromInternalCondition(Condition condition)
		{
			IpMatchPredicate ipMatchPredicate = condition as IpMatchPredicate;
			if (ipMatchPredicate == null || !ipMatchPredicate.Name.Equals("ipMatch"))
			{
				return null;
			}
			object value = ipMatchPredicate.Value.GetValue(null);
			IEnumerable<string> source;
			if (value is string)
			{
				source = new List<string>
				{
					value as string
				};
			}
			else
			{
				source = (IEnumerable<string>)value;
			}
			return new SenderIpRangesPredicate(source.Select(new Func<string, IPRange>(IPRange.Parse)));
		}

		// Token: 0x0600730A RID: 29450 RVA: 0x001D43B8 File Offset: 0x001D25B8
		internal override void Reset()
		{
			this.IpRanges = null;
			base.Reset();
		}

		// Token: 0x0600730B RID: 29451 RVA: 0x001D43C7 File Offset: 0x001D25C7
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (this.IpRanges == null || !this.IpRanges.Any<IPRange>())
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, base.Name));
				return;
			}
			base.ValidateRead(errors);
		}

		// Token: 0x0600730C RID: 29452 RVA: 0x001D4404 File Offset: 0x001D2604
		internal override string GetPredicateParameters()
		{
			return string.Join(", ", (from range in this.IpRanges
			select range.ToString()).ToArray<string>());
		}

		// Token: 0x04003A85 RID: 14981
		private const string PropertyName = "Message.SenderIp";
	}
}
