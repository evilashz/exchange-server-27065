using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B93 RID: 2963
	[Serializable]
	public class ModerateMessageByUserAction : TransportRuleAction, IEquatable<ModerateMessageByUserAction>
	{
		// Token: 0x06006FB4 RID: 28596 RVA: 0x001C9AFD File Offset: 0x001C7CFD
		public override int GetHashCode()
		{
			return Utils.GetHashCodeForArray<SmtpAddress>(this.Addresses);
		}

		// Token: 0x06006FB5 RID: 28597 RVA: 0x001C9B0A File Offset: 0x001C7D0A
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as ModerateMessageByUserAction)));
		}

		// Token: 0x06006FB6 RID: 28598 RVA: 0x001C9B43 File Offset: 0x001C7D43
		public bool Equals(ModerateMessageByUserAction other)
		{
			if (this.Addresses == null)
			{
				return null == other.Addresses;
			}
			return this.Addresses.SequenceEqual(other.Addresses);
		}

		// Token: 0x170022D1 RID: 8913
		// (get) Token: 0x06006FB7 RID: 28599 RVA: 0x001C9B68 File Offset: 0x001C7D68
		// (set) Token: 0x06006FB8 RID: 28600 RVA: 0x001C9B70 File Offset: 0x001C7D70
		[ActionParameterName("ModerateMessageByUser")]
		[LocDescription(RulesTasksStrings.IDs.ToAddressesDescription)]
		[LocDisplayName(RulesTasksStrings.IDs.ToAddressesDisplayName)]
		public virtual SmtpAddress[] Addresses
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

		// Token: 0x170022D2 RID: 8914
		// (get) Token: 0x06006FB9 RID: 28601 RVA: 0x001C9B79 File Offset: 0x001C7D79
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionModerateMessageByUser(RuleDescription.BuildDescriptionStringFromStringArray(Utils.BuildSmtpAddressStringList(this.Addresses, false), RulesTasksStrings.RuleDescriptionAndDelimiter, base.MaxDescriptionListLength));
			}
		}

		// Token: 0x06006FBA RID: 28602 RVA: 0x001C9BA8 File Offset: 0x001C7DA8
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (this.Addresses == null || this.Addresses.Length == 0)
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, base.Name));
				return;
			}
			foreach (SmtpAddress smtpAddress in this.Addresses)
			{
				if (!smtpAddress.IsValidAddress)
				{
					errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.InvalidRecipient(smtpAddress.ToString()), base.Name));
					return;
				}
			}
			base.ValidateRead(errors);
		}

		// Token: 0x06006FBB RID: 28603 RVA: 0x001C9C38 File Offset: 0x001C7E38
		internal static TransportRuleAction CreateFromInternalAction(Action action)
		{
			if (action.Name != "ModerateMessageByUser")
			{
				return null;
			}
			List<SmtpAddress> list = new List<SmtpAddress>();
			string stringValue = TransportRuleAction.GetStringValue(action.Arguments[0]);
			string[] array = stringValue.Split(new string[]
			{
				";"
			}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string text in array)
			{
				SmtpAddress item = new SmtpAddress(text.Trim());
				list.Add(item);
			}
			if (list.Count == 0)
			{
				return null;
			}
			return new ModerateMessageByUserAction
			{
				Addresses = list.ToArray()
			};
		}

		// Token: 0x06006FBC RID: 28604 RVA: 0x001C9CE0 File Offset: 0x001C7EE0
		internal override Action ToInternalAction()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (SmtpAddress smtpAddress in this.Addresses)
			{
				if (!smtpAddress.IsValidAddress)
				{
					throw new ArgumentException(RulesTasksStrings.InvalidRecipient(smtpAddress.ToString()), "Addresses");
				}
				string value = smtpAddress.ToString();
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException(RulesTasksStrings.InvalidRecipient(smtpAddress.ToString()), "Addresses");
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(";");
				}
				stringBuilder.Append(value);
			}
			return TransportRuleParser.Instance.CreateAction("ModerateMessageByUser", new ShortList<Argument>
			{
				new Value(stringBuilder.ToString())
			}, Utils.GetActionName(this));
		}

		// Token: 0x06006FBD RID: 28605 RVA: 0x001C9DD0 File Offset: 0x001C7FD0
		internal override string GetActionParameters()
		{
			return string.Join(", ", Utils.BuildSmtpAddressStringList(this.Addresses, true));
		}

		// Token: 0x06006FBE RID: 28606 RVA: 0x001C9DE8 File Offset: 0x001C7FE8
		internal override void SuppressPiiData()
		{
			string[] array;
			string[] array2;
			this.Addresses = SuppressingPiiData.Redact(this.Addresses, out array, out array2);
		}

		// Token: 0x040039BF RID: 14783
		private const string InternalActionName = "ModerateMessageByUser";

		// Token: 0x040039C0 RID: 14784
		private const string AddressDelimiter = ";";

		// Token: 0x040039C1 RID: 14785
		private SmtpAddress[] addresses;
	}
}
