using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport.RecipientAPI;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200009C RID: 156
	internal class IsInternalPredicate : PredicateCondition
	{
		// Token: 0x06000474 RID: 1140 RVA: 0x00016800 File Offset: 0x00014A00
		public IsInternalPredicate(Property property, ShortList<string> entries, RulesCreationContext creationContext) : base(property, entries, creationContext)
		{
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0001680B File Offset: 0x00014A0B
		public override string Name
		{
			get
			{
				return "isInternal";
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00016814 File Offset: 0x00014A14
		public override bool Evaluate(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			transportRulesEvaluationContext.PredicateName = this.Name;
			object value = base.Property.GetValue(transportRulesEvaluationContext);
			ITransportMailItemFacade transportMailItemFacade = TransportUtils.GetTransportMailItemFacade(transportRulesEvaluationContext.MailItem);
			OrganizationId orgId = (OrganizationId)transportMailItemFacade.OrganizationIdAsObject;
			List<string> list = new List<string>();
			IEnumerable<string> enumerable = value as IEnumerable<string>;
			bool flag = false;
			if (enumerable != null)
			{
				foreach (string text in enumerable)
				{
					if (IsInternalPredicate.IsInternal(transportRulesEvaluationContext.Server, text, orgId))
					{
						if (base.EvaluationMode == ConditionEvaluationMode.Optimized)
						{
							return true;
						}
						list.Add(text);
						flag = true;
					}
				}
				base.UpdateEvaluationHistory(baseContext, flag, list, 0);
				return flag;
			}
			flag = IsInternalPredicate.IsInternal(transportRulesEvaluationContext.Server, (string)value, orgId);
			if (base.EvaluationMode == ConditionEvaluationMode.Full)
			{
				if (flag)
				{
					list.Add((string)value);
				}
				base.UpdateEvaluationHistory(baseContext, flag, list, 0);
			}
			return flag;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00016924 File Offset: 0x00014B24
		protected override Value BuildValue(ShortList<string> entries, RulesCreationContext creationContext)
		{
			if (entries.Count != 0)
			{
				throw new RulesValidationException(RulesStrings.ValueIsNotAllowed(this.Name));
			}
			return null;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00016940 File Offset: 0x00014B40
		internal static bool IsInternal(SmtpServer server, string recipient, OrganizationId orgId)
		{
			if (string.IsNullOrEmpty(recipient))
			{
				return false;
			}
			RoutingAddress address = (RoutingAddress)recipient;
			AddressBookImpl addressBookImpl = server.AddressBook as AddressBookImpl;
			if (addressBookImpl != null)
			{
				return addressBookImpl.IsInternalTo(address, orgId, false);
			}
			return server.AddressBook.IsInternal(address);
		}
	}
}
