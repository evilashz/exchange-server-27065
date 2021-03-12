using System;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B7C RID: 2940
	[Serializable]
	public class AddManagerAsRecipientTypeAction : TransportRuleAction, IEquatable<AddManagerAsRecipientTypeAction>
	{
		// Token: 0x06006EEB RID: 28395 RVA: 0x001C7D67 File Offset: 0x001C5F67
		public override int GetHashCode()
		{
			return this.RecipientType.GetHashCode();
		}

		// Token: 0x06006EEC RID: 28396 RVA: 0x001C7D79 File Offset: 0x001C5F79
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as AddManagerAsRecipientTypeAction)));
		}

		// Token: 0x06006EED RID: 28397 RVA: 0x001C7DB2 File Offset: 0x001C5FB2
		public bool Equals(AddManagerAsRecipientTypeAction other)
		{
			return this.RecipientType.Equals(other.RecipientType);
		}

		// Token: 0x170022AD RID: 8877
		// (get) Token: 0x06006EEE RID: 28398 RVA: 0x001C7DCF File Offset: 0x001C5FCF
		// (set) Token: 0x06006EEF RID: 28399 RVA: 0x001C7DD7 File Offset: 0x001C5FD7
		[LocDisplayName(RulesTasksStrings.IDs.RecipientTypeDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.RecipientTypeDescription)]
		[ActionParameterName("AddManagerAsRecipientType")]
		public AddedRecipientType RecipientType
		{
			get
			{
				return this.addedRecipientType;
			}
			set
			{
				this.addedRecipientType = value;
			}
		}

		// Token: 0x170022AE RID: 8878
		// (get) Token: 0x06006EF0 RID: 28400 RVA: 0x001C7DE0 File Offset: 0x001C5FE0
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionAddManagerAsRecipientType(LocalizedDescriptionAttribute.FromEnum(typeof(AddedRecipientType), this.RecipientType));
			}
		}

		// Token: 0x06006EF1 RID: 28401 RVA: 0x001C7E08 File Offset: 0x001C6008
		internal static TransportRuleAction CreateFromInternalAction(Action action)
		{
			if (action.Name != "AddManagerAsRecipientType")
			{
				return null;
			}
			AddedRecipientType recipientType;
			try
			{
				recipientType = (AddedRecipientType)Enum.Parse(typeof(AddedRecipientType), TransportRuleAction.GetStringValue(action.Arguments[0]));
			}
			catch (ArgumentException)
			{
				return null;
			}
			catch (OverflowException)
			{
				return null;
			}
			return new AddManagerAsRecipientTypeAction
			{
				RecipientType = recipientType
			};
		}

		// Token: 0x06006EF2 RID: 28402 RVA: 0x001C7E88 File Offset: 0x001C6088
		internal override void Reset()
		{
			this.addedRecipientType = AddedRecipientType.To;
			base.Reset();
		}

		// Token: 0x06006EF3 RID: 28403 RVA: 0x001C7E98 File Offset: 0x001C6098
		internal override Action ToInternalAction()
		{
			return TransportRuleParser.Instance.CreateAction("AddManagerAsRecipientType", new ShortList<Argument>
			{
				new Value(this.addedRecipientType.ToString())
			}, Utils.GetActionName(this));
		}

		// Token: 0x06006EF4 RID: 28404 RVA: 0x001C7EDC File Offset: 0x001C60DC
		internal override string GetActionParameters()
		{
			return Enum.GetName(typeof(AddedRecipientType), this.RecipientType);
		}

		// Token: 0x0400399F RID: 14751
		private AddedRecipientType addedRecipientType;
	}
}
