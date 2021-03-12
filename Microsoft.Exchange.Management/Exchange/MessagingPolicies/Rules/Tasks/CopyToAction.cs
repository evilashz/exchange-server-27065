using System;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B89 RID: 2953
	[Serializable]
	public class CopyToAction : RecipientAction, IEquatable<CopyToAction>
	{
		// Token: 0x06006F4D RID: 28493 RVA: 0x001C8CD6 File Offset: 0x001C6ED6
		public CopyToAction() : base("AddCcRecipientSmtpOnly")
		{
		}

		// Token: 0x06006F4E RID: 28494 RVA: 0x001C8CE3 File Offset: 0x001C6EE3
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<SmtpAddress>(this.Addresses);
		}

		// Token: 0x06006F4F RID: 28495 RVA: 0x001C8CF0 File Offset: 0x001C6EF0
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as CopyToAction)));
		}

		// Token: 0x06006F50 RID: 28496 RVA: 0x001C8D29 File Offset: 0x001C6F29
		public bool Equals(CopyToAction other)
		{
			if (this.Addresses == null)
			{
				return null == other.Addresses;
			}
			return this.Addresses.SequenceEqual(other.Addresses);
		}

		// Token: 0x170022C1 RID: 8897
		// (get) Token: 0x06006F51 RID: 28497 RVA: 0x001C8D4E File Offset: 0x001C6F4E
		// (set) Token: 0x06006F52 RID: 28498 RVA: 0x001C8D56 File Offset: 0x001C6F56
		[LocDisplayName(RulesTasksStrings.IDs.ToAddressesDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.ToAddressesDescription)]
		[ActionParameterName("CopyTo")]
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

		// Token: 0x170022C2 RID: 8898
		// (get) Token: 0x06006F53 RID: 28499 RVA: 0x001C8D5F File Offset: 0x001C6F5F
		protected override RecipientAction.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new RecipientAction.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionCopyTo);
			}
		}

		// Token: 0x06006F54 RID: 28500 RVA: 0x001C8D6D File Offset: 0x001C6F6D
		internal static TransportRuleAction CreateFromInternalActions(ShortList<Action> actions, ref int index)
		{
			return RecipientAction.CreateFromInternalActions<CopyToAction>(actions, ref index, "AddCcRecipientSmtpOnly");
		}

		// Token: 0x06006F55 RID: 28501 RVA: 0x001C8D7B File Offset: 0x001C6F7B
		internal override string GetActionParameters()
		{
			return string.Join(", ", Utils.BuildSmtpAddressStringList(this.Addresses, true));
		}

		// Token: 0x040039B4 RID: 14772
		private const string InternalActionName = "AddCcRecipientSmtpOnly";
	}
}
