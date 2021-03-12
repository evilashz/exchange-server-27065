using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B80 RID: 2944
	[Serializable]
	public abstract class SmtpAddressAction : TransportRuleAction
	{
		// Token: 0x06006F0D RID: 28429 RVA: 0x001C81FB File Offset: 0x001C63FB
		protected SmtpAddressAction(string internalActionName)
		{
			this.internalActionName = internalActionName;
		}

		// Token: 0x170022B4 RID: 8884
		// (get) Token: 0x06006F0E RID: 28430 RVA: 0x001C820A File Offset: 0x001C640A
		// (set) Token: 0x06006F0F RID: 28431 RVA: 0x001C8214 File Offset: 0x001C6414
		[LocDisplayName(RulesTasksStrings.IDs.ToAddressesDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.ToAddressesDescription)]
		public virtual SmtpAddress[] Addresses
		{
			get
			{
				return this.addresses;
			}
			set
			{
				if (value != null)
				{
					for (int i = 0; i < value.Length; i++)
					{
						SmtpAddress smtpAddress = value[i];
						if (!smtpAddress.IsValidAddress)
						{
							throw new ArgumentException(RulesTasksStrings.InvalidSmtpAddress(smtpAddress.ToString()), "Address");
						}
					}
				}
				this.addresses = value;
			}
		}

		// Token: 0x170022B5 RID: 8885
		// (get) Token: 0x06006F10 RID: 28432 RVA: 0x001C8273 File Offset: 0x001C6473
		internal override string Description
		{
			get
			{
				return RulesTasksStrings.RuleDescriptionAddToRecipient(RuleDescription.BuildDescriptionStringFromStringArray(Utils.BuildSmtpAddressStringList(this.Addresses, false), RulesTasksStrings.RuleDescriptionAndDelimiter, base.MaxDescriptionListLength));
			}
		}

		// Token: 0x170022B6 RID: 8886
		// (get) Token: 0x06006F11 RID: 28433
		protected abstract SmtpAddressAction.LocalizedStringDescriptionDelegate LocalizedStringDescription { get; }

		// Token: 0x06006F12 RID: 28434 RVA: 0x001C82A0 File Offset: 0x001C64A0
		internal static TransportRuleAction CreateFromInternalActions<T>(ShortList<Action> actions, ref int index, string internalActionName) where T : SmtpAddressAction, new()
		{
			List<SmtpAddress> list = new List<SmtpAddress>();
			while (index < actions.Count && !(actions[index].Name != internalActionName))
			{
				SmtpAddress item = new SmtpAddress(TransportRuleAction.GetStringValue(actions[index].Arguments[0]));
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

		// Token: 0x06006F13 RID: 28435 RVA: 0x001C8332 File Offset: 0x001C6532
		internal override void Reset()
		{
			this.addresses = null;
			base.Reset();
		}

		// Token: 0x06006F14 RID: 28436 RVA: 0x001C8344 File Offset: 0x001C6544
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
					errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.InvalidSmtpAddress(smtpAddress.ToString()), base.Name));
					return;
				}
			}
			base.ValidateRead(errors);
		}

		// Token: 0x06006F15 RID: 28437 RVA: 0x001C83D4 File Offset: 0x001C65D4
		internal override Action[] ToInternalActions()
		{
			List<Action> list = new List<Action>();
			foreach (SmtpAddress smtpAddress in this.Addresses)
			{
				Action item = TransportRuleParser.Instance.CreateAction(this.internalActionName, new ShortList<Argument>
				{
					new Value(smtpAddress.ToString())
				}, Utils.GetActionName(this));
				list.Add(item);
			}
			return list.ToArray();
		}

		// Token: 0x06006F16 RID: 28438 RVA: 0x001C8454 File Offset: 0x001C6654
		internal override string GetActionParameters()
		{
			return string.Join(", ", Utils.BuildSmtpAddressStringList(this.Addresses, true));
		}

		// Token: 0x06006F17 RID: 28439 RVA: 0x001C846C File Offset: 0x001C666C
		internal override void SuppressPiiData()
		{
			string[] array;
			string[] array2;
			this.Addresses = SuppressingPiiData.Redact(this.Addresses, out array, out array2);
		}

		// Token: 0x040039A3 RID: 14755
		private readonly string internalActionName;

		// Token: 0x040039A4 RID: 14756
		private SmtpAddress[] addresses;

		// Token: 0x02000B81 RID: 2945
		// (Invoke) Token: 0x06006F19 RID: 28441
		protected delegate LocalizedString LocalizedStringDescriptionDelegate(string addresses);
	}
}
