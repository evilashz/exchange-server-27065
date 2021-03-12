using System;
using System.Linq;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B98 RID: 2968
	[Serializable]
	public class RedirectMessageAction : RecipientAction, IEquatable<RedirectMessageAction>
	{
		// Token: 0x06006FE7 RID: 28647 RVA: 0x001CA612 File Offset: 0x001C8812
		public RedirectMessageAction() : base("RedirectMessage")
		{
		}

		// Token: 0x06006FE8 RID: 28648 RVA: 0x001CA61F File Offset: 0x001C881F
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<SmtpAddress>(this.Addresses);
		}

		// Token: 0x06006FE9 RID: 28649 RVA: 0x001CA62C File Offset: 0x001C882C
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as RedirectMessageAction)));
		}

		// Token: 0x06006FEA RID: 28650 RVA: 0x001CA665 File Offset: 0x001C8865
		public bool Equals(RedirectMessageAction other)
		{
			if (this.Addresses == null)
			{
				return null == other.Addresses;
			}
			return this.Addresses.SequenceEqual(other.Addresses);
		}

		// Token: 0x170022DA RID: 8922
		// (get) Token: 0x06006FEB RID: 28651 RVA: 0x001CA68A File Offset: 0x001C888A
		// (set) Token: 0x06006FEC RID: 28652 RVA: 0x001CA692 File Offset: 0x001C8892
		[LocDisplayName(RulesTasksStrings.IDs.ToAddressesDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.ToAddressesDescription)]
		[ActionParameterName("RedirectMessageTo")]
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

		// Token: 0x170022DB RID: 8923
		// (get) Token: 0x06006FED RID: 28653 RVA: 0x001CA69B File Offset: 0x001C889B
		protected override RecipientAction.LocalizedStringDescriptionDelegate LocalizedStringDescription
		{
			get
			{
				return new RecipientAction.LocalizedStringDescriptionDelegate(RulesTasksStrings.RuleDescriptionRedirectMessage);
			}
		}

		// Token: 0x06006FEE RID: 28654 RVA: 0x001CA6A9 File Offset: 0x001C88A9
		internal static TransportRuleAction CreateFromInternalActions(ShortList<Action> actions, ref int index)
		{
			return RecipientAction.CreateFromInternalActions<RedirectMessageAction>(actions, ref index, "RedirectMessage");
		}

		// Token: 0x040039CF RID: 14799
		private const string InternalActionName = "RedirectMessage";
	}
}
