using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BAA RID: 2986
	[Serializable]
	public abstract class SmtpAddressesPredicate : TransportRulePredicate
	{
		// Token: 0x0600709B RID: 28827 RVA: 0x001CD0A8 File Offset: 0x001CB2A8
		protected SmtpAddressesPredicate(string internalPredicateName, string internalPropertyName)
		{
			this.internalPredicateName = internalPredicateName;
			this.internalPropertyName = internalPropertyName;
		}

		// Token: 0x170022FB RID: 8955
		// (get) Token: 0x0600709C RID: 28828
		// (set) Token: 0x0600709D RID: 28829
		public abstract SmtpAddress[] Addresses { get; set; }

		// Token: 0x170022FC RID: 8956
		// (get) Token: 0x0600709E RID: 28830 RVA: 0x001CD0BE File Offset: 0x001CB2BE
		internal override string Description
		{
			get
			{
				return this.LocalizedStringDescription(RuleDescription.BuildDescriptionStringFromStringArray(Utils.BuildSmtpAddressStringList(this.Addresses, false), RulesTasksStrings.RuleDescriptionOrDelimiter, base.MaxDescriptionListLength));
			}
		}

		// Token: 0x170022FD RID: 8957
		// (get) Token: 0x0600709F RID: 28831
		protected abstract SmtpAddressesPredicate.LocalizedStringDescriptionDelegate LocalizedStringDescription { get; }

		// Token: 0x060070A0 RID: 28832 RVA: 0x001CD0F4 File Offset: 0x001CB2F4
		internal static TransportRulePredicate CreateFromInternalCondition<T>(Condition condition, string internalPredicateName, string internalPropertyName) where T : SmtpAddressesPredicate, new()
		{
			if (condition.ConditionType != ConditionType.Predicate)
			{
				return null;
			}
			PredicateCondition predicateCondition = (PredicateCondition)condition;
			if (!predicateCondition.Name.Equals(internalPredicateName) || !predicateCondition.Property.Name.Equals(internalPropertyName))
			{
				return null;
			}
			List<SmtpAddress> list = new List<SmtpAddress>(predicateCondition.Value.RawValues.Count);
			for (int i = 0; i < predicateCondition.Value.RawValues.Count; i++)
			{
				SmtpAddress item = new SmtpAddress(predicateCondition.Value.RawValues[i]);
				if (item.IsValidAddress)
				{
					list.Add(item);
				}
			}
			T t = Activator.CreateInstance<T>();
			if (list.Count > 0)
			{
				t.Addresses = list.ToArray();
			}
			return t;
		}

		// Token: 0x060070A1 RID: 28833 RVA: 0x001CD1B9 File Offset: 0x001CB3B9
		internal override void Reset()
		{
			this.addresses = null;
			base.Reset();
		}

		// Token: 0x060070A2 RID: 28834 RVA: 0x001CD1C8 File Offset: 0x001CB3C8
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

		// Token: 0x060070A3 RID: 28835 RVA: 0x001CD258 File Offset: 0x001CB458
		internal override Condition ToInternalCondition()
		{
			ShortList<string> shortList = new ShortList<string>();
			foreach (SmtpAddress smtpAddress in this.Addresses)
			{
				string text = smtpAddress.ToString();
				if (string.IsNullOrEmpty(text))
				{
					throw new ArgumentException(RulesTasksStrings.InvalidRecipient(text), "Address");
				}
				shortList.Add(text);
			}
			return TransportRuleParser.Instance.CreatePredicate(this.internalPredicateName, TransportRuleParser.Instance.CreateProperty(this.internalPropertyName), shortList);
		}

		// Token: 0x060070A4 RID: 28836 RVA: 0x001CD2EB File Offset: 0x001CB4EB
		internal override string GetPredicateParameters()
		{
			return string.Join(", ", Utils.BuildSmtpAddressStringList(this.Addresses, true));
		}

		// Token: 0x060070A5 RID: 28837 RVA: 0x001CD304 File Offset: 0x001CB504
		internal override void SuppressPiiData()
		{
			string[] array;
			string[] array2;
			this.Addresses = SuppressingPiiData.Redact(this.Addresses, out array, out array2);
		}

		// Token: 0x04003A1A RID: 14874
		private readonly string internalPredicateName;

		// Token: 0x04003A1B RID: 14875
		private readonly string internalPropertyName;

		// Token: 0x04003A1C RID: 14876
		protected SmtpAddress[] addresses;

		// Token: 0x02000BAB RID: 2987
		// (Invoke) Token: 0x060070A7 RID: 28839
		protected delegate LocalizedString LocalizedStringDescriptionDelegate(string addresses);
	}
}
