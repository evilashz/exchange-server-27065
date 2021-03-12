using System;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B82 RID: 2946
	[Serializable]
	public class AddToRecipientSmtpAction : SmtpAddressAction, IEquatable<AddToRecipientSmtpAction>
	{
		// Token: 0x06006F1C RID: 28444 RVA: 0x001C848E File Offset: 0x001C668E
		public AddToRecipientSmtpAction() : base("AddToRecipientSmtpOnly")
		{
		}

		// Token: 0x06006F1D RID: 28445 RVA: 0x001C849B File Offset: 0x001C669B
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<SmtpAddress>(this.Addresses);
		}

		// Token: 0x06006F1E RID: 28446 RVA: 0x001C84A8 File Offset: 0x001C66A8
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as AddToRecipientSmtpAction)));
		}

		// Token: 0x06006F1F RID: 28447 RVA: 0x001C84E1 File Offset: 0x001C66E1
		public bool Equals(AddToRecipientSmtpAction other)
		{
			if (this.Addresses == null)
			{
				return null == other.Addresses;
			}
			return this.Addresses.SequenceEqual(other.Addresses);
		}

		// Token: 0x170022B7 RID: 8887
		// (get) Token: 0x06006F20 RID: 28448 RVA: 0x001C8506 File Offset: 0x001C6706
		protected override SmtpAddressAction.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new SmtpAddressAction.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionAddToRecipient);
			}
		}

		// Token: 0x06006F21 RID: 28449 RVA: 0x001C8514 File Offset: 0x001C6714
		internal static TransportRuleAction CreateFromInternalActions(ShortList<Action> actions, ref int index)
		{
			return SmtpAddressAction.CreateFromInternalActions<AddToRecipientSmtpAction>(actions, ref index, "AddToRecipientSmtpOnly");
		}

		// Token: 0x040039A5 RID: 14757
		private const string InternalActionName = "AddToRecipientSmtpOnly";
	}
}
