using System;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B87 RID: 2951
	[Serializable]
	public class BlindCopyToAction : RecipientAction, IEquatable<BlindCopyToAction>
	{
		// Token: 0x06006F3F RID: 28479 RVA: 0x001C8B9D File Offset: 0x001C6D9D
		public BlindCopyToAction() : base("AddEnvelopeRecipient")
		{
		}

		// Token: 0x06006F40 RID: 28480 RVA: 0x001C8BAA File Offset: 0x001C6DAA
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<SmtpAddress>(this.Addresses);
		}

		// Token: 0x06006F41 RID: 28481 RVA: 0x001C8BB7 File Offset: 0x001C6DB7
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as BlindCopyToAction)));
		}

		// Token: 0x06006F42 RID: 28482 RVA: 0x001C8BF0 File Offset: 0x001C6DF0
		public bool Equals(BlindCopyToAction other)
		{
			if (this.Addresses == null)
			{
				return null == other.Addresses;
			}
			return this.Addresses.SequenceEqual(other.Addresses);
		}

		// Token: 0x170022BE RID: 8894
		// (get) Token: 0x06006F43 RID: 28483 RVA: 0x001C8C15 File Offset: 0x001C6E15
		// (set) Token: 0x06006F44 RID: 28484 RVA: 0x001C8C1D File Offset: 0x001C6E1D
		[LocDescription(RulesTasksStrings.IDs.ToAddressesDescription)]
		[ActionParameterName("BlindCopyTo")]
		[LocDisplayName(RulesTasksStrings.IDs.ToAddressesDisplayName)]
		public override SmtpAddress[] Addresses
		{
			get
			{
				return this.addresses;
			}
			set
			{
				this.addresses = value;
			}
		}

		// Token: 0x170022BF RID: 8895
		// (get) Token: 0x06006F45 RID: 28485 RVA: 0x001C8C26 File Offset: 0x001C6E26
		protected override RecipientAction.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new RecipientAction.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionBlindCopyTo);
			}
		}

		// Token: 0x06006F46 RID: 28486 RVA: 0x001C8C34 File Offset: 0x001C6E34
		internal static TransportRuleAction CreateFromInternalActions(ShortList<Action> actions, ref int index)
		{
			return RecipientAction.CreateFromInternalActions<BlindCopyToAction>(actions, ref index, "AddEnvelopeRecipient");
		}

		// Token: 0x040039B2 RID: 14770
		private const string InternalActionName = "AddEnvelopeRecipient";
	}
}
