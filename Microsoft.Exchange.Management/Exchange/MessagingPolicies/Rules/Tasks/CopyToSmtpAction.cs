using System;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B8A RID: 2954
	[Serializable]
	public class CopyToSmtpAction : SmtpAddressAction, IEquatable<CopyToSmtpAction>
	{
		// Token: 0x06006F56 RID: 28502 RVA: 0x001C8D93 File Offset: 0x001C6F93
		public CopyToSmtpAction() : base("AddCcRecipientSmtpOnly")
		{
		}

		// Token: 0x06006F57 RID: 28503 RVA: 0x001C8DA0 File Offset: 0x001C6FA0
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<SmtpAddress>(this.Addresses);
		}

		// Token: 0x06006F58 RID: 28504 RVA: 0x001C8DAD File Offset: 0x001C6FAD
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as CopyToSmtpAction)));
		}

		// Token: 0x06006F59 RID: 28505 RVA: 0x001C8DE6 File Offset: 0x001C6FE6
		public bool Equals(CopyToSmtpAction other)
		{
			if (this.Addresses == null)
			{
				return null == other.Addresses;
			}
			return this.Addresses.SequenceEqual(other.Addresses);
		}

		// Token: 0x170022C3 RID: 8899
		// (get) Token: 0x06006F5A RID: 28506 RVA: 0x001C8E0B File Offset: 0x001C700B
		protected override SmtpAddressAction.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new SmtpAddressAction.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionCopyTo);
			}
		}

		// Token: 0x06006F5B RID: 28507 RVA: 0x001C8E19 File Offset: 0x001C7019
		internal static TransportRuleAction CreateFromInternalActions(ShortList<Action> actions, ref int index)
		{
			return SmtpAddressAction.CreateFromInternalActions<CopyToSmtpAction>(actions, ref index, "AddCcRecipientSmtpOnly");
		}

		// Token: 0x040039B5 RID: 14773
		private const string InternalActionName = "AddCcRecipientSmtpOnly";
	}
}
