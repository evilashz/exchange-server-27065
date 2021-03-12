using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B7D RID: 2941
	[Serializable]
	public abstract class RecipientAction : TransportRuleAction
	{
		// Token: 0x06006EF6 RID: 28406 RVA: 0x001C7F00 File Offset: 0x001C6100
		protected RecipientAction(string internalActionName)
		{
			this.internalActionName = internalActionName;
		}

		// Token: 0x170022AF RID: 8879
		// (get) Token: 0x06006EF7 RID: 28407
		// (set) Token: 0x06006EF8 RID: 28408
		public abstract SmtpAddress[] Addresses { get; set; }

		// Token: 0x170022B0 RID: 8880
		// (get) Token: 0x06006EF9 RID: 28409 RVA: 0x001C7F0F File Offset: 0x001C610F
		internal override string Description
		{
			get
			{
				return this.LocalizedStringDescription(RuleDescription.BuildDescriptionStringFromStringArray(Utils.BuildSmtpAddressStringList(this.Addresses, false), RulesTasksStrings.RuleDescriptionAndDelimiter, base.MaxDescriptionListLength));
			}
		}

		// Token: 0x170022B1 RID: 8881
		// (get) Token: 0x06006EFA RID: 28410
		protected abstract RecipientAction.LocalizedStringDescriptionDelegate LocalizedStringDescription { get; }

		// Token: 0x06006EFB RID: 28411 RVA: 0x001C7F44 File Offset: 0x001C6144
		internal static TransportRuleAction CreateFromInternalActions<T>(ShortList<Action> actions, ref int index, string internalActionName) where T : RecipientAction, new()
		{
			List<SmtpAddress> list = new List<SmtpAddress>();
			while (index < actions.Count && !(actions[index].Name != internalActionName))
			{
				string stringValue = TransportRuleAction.GetStringValue(actions[index].Arguments[0]);
				SmtpAddress item = new SmtpAddress(stringValue);
				if (!item.IsValidAddress)
				{
					break;
				}
				list.Add(item);
				index++;
			}
			if (list.Count == 0)
			{
				return null;
			}
			T t = Activator.CreateInstance<T>();
			t.Addresses = list.ToArray();
			return t;
		}

		// Token: 0x06006EFC RID: 28412 RVA: 0x001C7FD8 File Offset: 0x001C61D8
		internal override void Reset()
		{
			this.addresses = null;
			base.Reset();
		}

		// Token: 0x06006EFD RID: 28413 RVA: 0x001C7FE8 File Offset: 0x001C61E8
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

		// Token: 0x06006EFE RID: 28414 RVA: 0x001C8078 File Offset: 0x001C6278
		internal override Action[] ToInternalActions()
		{
			List<Action> list = new List<Action>();
			foreach (SmtpAddress smtpAddress in this.Addresses)
			{
				ShortList<Argument> shortList = new ShortList<Argument>();
				string text = smtpAddress.ToString();
				if (string.IsNullOrEmpty(text))
				{
					throw new ArgumentException(RulesTasksStrings.InvalidRecipient(text), "Addresses");
				}
				shortList.Add(new Value(text));
				Action item = TransportRuleParser.Instance.CreateAction(this.internalActionName, shortList, Utils.GetActionName(this));
				list.Add(item);
			}
			return list.ToArray();
		}

		// Token: 0x06006EFF RID: 28415 RVA: 0x001C811A File Offset: 0x001C631A
		internal override string GetActionParameters()
		{
			return string.Join(", ", Utils.BuildSmtpAddressStringList(this.Addresses, true));
		}

		// Token: 0x06006F00 RID: 28416 RVA: 0x001C8134 File Offset: 0x001C6334
		internal override void SuppressPiiData()
		{
			string[] array;
			string[] array2;
			this.Addresses = SuppressingPiiData.Redact(this.Addresses, out array, out array2);
		}

		// Token: 0x040039A0 RID: 14752
		private readonly string internalActionName;

		// Token: 0x040039A1 RID: 14753
		protected SmtpAddress[] addresses;

		// Token: 0x02000B7E RID: 2942
		// (Invoke) Token: 0x06006F02 RID: 28418
		protected delegate LocalizedString LocalizedStringDescriptionDelegate(string addresses);
	}
}
