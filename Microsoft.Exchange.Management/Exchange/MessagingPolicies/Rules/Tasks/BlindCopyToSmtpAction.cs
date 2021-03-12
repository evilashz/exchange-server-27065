using System;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B88 RID: 2952
	[Serializable]
	public class BlindCopyToSmtpAction : SmtpAddressAction, IEquatable<BlindCopyToSmtpAction>
	{
		// Token: 0x06006F47 RID: 28487 RVA: 0x001C8C42 File Offset: 0x001C6E42
		public BlindCopyToSmtpAction() : base("AddEnvelopeRecipient")
		{
		}

		// Token: 0x06006F48 RID: 28488 RVA: 0x001C8C4F File Offset: 0x001C6E4F
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<SmtpAddress>(this.Addresses);
		}

		// Token: 0x06006F49 RID: 28489 RVA: 0x001C8C5C File Offset: 0x001C6E5C
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as BlindCopyToSmtpAction)));
		}

		// Token: 0x06006F4A RID: 28490 RVA: 0x001C8C95 File Offset: 0x001C6E95
		public bool Equals(BlindCopyToSmtpAction other)
		{
			if (this.Addresses == null)
			{
				return null == other.Addresses;
			}
			return this.Addresses.SequenceEqual(other.Addresses);
		}

		// Token: 0x170022C0 RID: 8896
		// (get) Token: 0x06006F4B RID: 28491 RVA: 0x001C8CBA File Offset: 0x001C6EBA
		protected override SmtpAddressAction.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new SmtpAddressAction.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionBlindCopyTo);
			}
		}

		// Token: 0x06006F4C RID: 28492 RVA: 0x001C8CC8 File Offset: 0x001C6EC8
		internal static TransportRuleAction CreateFromInternalActions(ShortList<Action> actions, ref int index)
		{
			return SmtpAddressAction.CreateFromInternalActions<BlindCopyToSmtpAction>(actions, ref index, "AddEnvelopeRecipient");
		}

		// Token: 0x040039B3 RID: 14771
		private const string InternalActionName = "AddEnvelopeRecipient";
	}
}
