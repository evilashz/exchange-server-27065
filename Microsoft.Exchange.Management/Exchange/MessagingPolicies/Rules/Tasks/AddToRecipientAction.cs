using System;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B7F RID: 2943
	[Serializable]
	public class AddToRecipientAction : RecipientAction, IEquatable<AddToRecipientAction>
	{
		// Token: 0x06006F05 RID: 28421 RVA: 0x001C8156 File Offset: 0x001C6356
		public AddToRecipientAction() : base("AddToRecipientSmtpOnly")
		{
		}

		// Token: 0x06006F06 RID: 28422 RVA: 0x001C8163 File Offset: 0x001C6363
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<SmtpAddress>(this.Addresses);
		}

		// Token: 0x06006F07 RID: 28423 RVA: 0x001C8170 File Offset: 0x001C6370
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as AddToRecipientAction)));
		}

		// Token: 0x06006F08 RID: 28424 RVA: 0x001C81A9 File Offset: 0x001C63A9
		public bool Equals(AddToRecipientAction other)
		{
			if (this.Addresses == null)
			{
				return null == other.Addresses;
			}
			return this.Addresses.SequenceEqual(other.Addresses);
		}

		// Token: 0x170022B2 RID: 8882
		// (get) Token: 0x06006F09 RID: 28425 RVA: 0x001C81CE File Offset: 0x001C63CE
		// (set) Token: 0x06006F0A RID: 28426 RVA: 0x001C81D6 File Offset: 0x001C63D6
		[LocDisplayName(RulesTasksStrings.IDs.ToAddressesDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.ToAddressesDescription)]
		[ActionParameterName("AddToRecipients")]
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

		// Token: 0x170022B3 RID: 8883
		// (get) Token: 0x06006F0B RID: 28427 RVA: 0x001C81DF File Offset: 0x001C63DF
		protected override RecipientAction.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new RecipientAction.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionAddToRecipient);
			}
		}

		// Token: 0x06006F0C RID: 28428 RVA: 0x001C81ED File Offset: 0x001C63ED
		internal static TransportRuleAction CreateFromInternalActions(ShortList<Action> actions, ref int index)
		{
			return RecipientAction.CreateFromInternalActions<AddToRecipientAction>(actions, ref index, "AddToRecipientSmtpOnly");
		}

		// Token: 0x040039A2 RID: 14754
		private const string InternalActionName = "AddToRecipientSmtpOnly";
	}
}
