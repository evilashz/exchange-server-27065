using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.Management.OutlookProtectionRules
{
	// Token: 0x02000B00 RID: 2816
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class Utils
	{
		// Token: 0x06006435 RID: 25653 RVA: 0x001A287A File Offset: 0x001A0A7A
		public static ADObjectId GetRuleCollectionId(IConfigDataProvider session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			return RuleIdParameter.GetRuleCollectionId(session, "OutlookProtectionRules");
		}

		// Token: 0x06006436 RID: 25654 RVA: 0x001A2898 File Offset: 0x001A0A98
		public static ADObjectId GetRuleId(IConfigDataProvider session, string name)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			ADObjectId ruleCollectionId = Utils.GetRuleCollectionId(session);
			return ruleCollectionId.GetChildId(name);
		}

		// Token: 0x06006437 RID: 25655 RVA: 0x001A28D4 File Offset: 0x001A0AD4
		public static bool IsEmptyCondition(TransportRule rule)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			OutlookProtectionRulePresentationObject outlookProtectionRulePresentationObject = new OutlookProtectionRulePresentationObject(rule);
			return outlookProtectionRulePresentationObject.IsEmptyCondition();
		}

		// Token: 0x06006438 RID: 25656 RVA: 0x001A28FC File Offset: 0x001A0AFC
		public static IEnumerable<SmtpAddress> ResolveRecipientIdParameters(IRecipientSession recipientSession, IEnumerable<RecipientIdParameter> recipientIdParameters, out LocalizedException resolutionError)
		{
			resolutionError = null;
			if (recipientSession == null)
			{
				throw new ArgumentNullException("recipientSession");
			}
			if (recipientIdParameters == null)
			{
				return Utils.EmptySmtpAddressArray;
			}
			LinkedList<SmtpAddress> linkedList = new LinkedList<SmtpAddress>();
			foreach (RecipientIdParameter recipientIdParameter in recipientIdParameters)
			{
				SmtpAddress smtpAddress = Utils.ResolveRecipientIdParameter(recipientSession, recipientIdParameter, out resolutionError);
				if (resolutionError != null || SmtpAddress.Empty.Equals(smtpAddress))
				{
					return null;
				}
				linkedList.AddLast(smtpAddress);
			}
			return linkedList;
		}

		// Token: 0x06006439 RID: 25657 RVA: 0x001A2990 File Offset: 0x001A0B90
		public static bool IsChildOfOutlookProtectionRuleContainer(RuleIdParameter ruleId)
		{
			return ruleId == null || ruleId.InternalADObjectId == null || ruleId.InternalADObjectId.Parent == null || string.Equals(ruleId.InternalADObjectId.Parent.Name, "OutlookProtectionRules", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600643A RID: 25658 RVA: 0x001A29C8 File Offset: 0x001A0BC8
		private static SmtpAddress ResolveRecipientIdParameter(IRecipientSession recipientSession, RecipientIdParameter recipientIdParameter, out LocalizedException resolutionError)
		{
			resolutionError = null;
			if (SmtpAddress.IsValidSmtpAddress(recipientIdParameter.RawIdentity))
			{
				return SmtpAddress.Parse(recipientIdParameter.RawIdentity);
			}
			IEnumerable<ADRecipient> objects = recipientIdParameter.GetObjects<ADRecipient>(null, recipientSession);
			ADRecipient adrecipient = null;
			using (IEnumerator<ADRecipient> enumerator = objects.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					resolutionError = new NoRecipientsForRecipientIdException(recipientIdParameter.ToString());
					return SmtpAddress.Empty;
				}
				adrecipient = enumerator.Current;
				if (enumerator.MoveNext())
				{
					resolutionError = new MoreThanOneRecipientForRecipientIdException(recipientIdParameter.ToString());
					return SmtpAddress.Empty;
				}
			}
			if (SmtpAddress.Empty.Equals(adrecipient.PrimarySmtpAddress))
			{
				resolutionError = new NoSmtpAddressForRecipientIdException(recipientIdParameter.ToString());
				return SmtpAddress.Empty;
			}
			return adrecipient.PrimarySmtpAddress;
		}

		// Token: 0x0400361C RID: 13852
		private static readonly SmtpAddress[] EmptySmtpAddressArray = new SmtpAddress[0];
	}
}
