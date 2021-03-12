using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x020000E9 RID: 233
	internal class CallerIdRuleEvaluator : IRuleEvaluator, ICallerIdRuleEvaluator
	{
		// Token: 0x060007AA RID: 1962 RVA: 0x0001E019 File Offset: 0x0001C219
		public CallerIdRuleEvaluator(List<CallerIdBase> conditions)
		{
			this.conditions = conditions;
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x0001E028 File Offset: 0x0001C228
		public PersonalContactInfo[] MatchedPersonalContacts
		{
			get
			{
				return this.matchedContacts;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x0001E030 File Offset: 0x0001C230
		public ADContactInfo MatchedADContact
		{
			get
			{
				return this.matchedADContact;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x0001E038 File Offset: 0x0001C238
		public List<string> MatchedPersonaEmails
		{
			get
			{
				return this.matchedPersonaEmails;
			}
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0001E040 File Offset: 0x0001C240
		public PhoneNumber GetCallerId()
		{
			return this.callerid;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0001E048 File Offset: 0x0001C248
		public UMSubscriber GetUMSubscriber()
		{
			return this.subscriber;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0001E060 File Offset: 0x0001C260
		public bool Evaluate(IDataLoader dataLoader)
		{
			if (this.conditions.Count == 0)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "CallerIdRuleEvaluator:Evaluate() no callerid conditions defined. Returning true", new object[0]);
				return true;
			}
			this.callerid = dataLoader.GetCallerId();
			this.subscriber = dataLoader.GetUMSubscriber();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			this.conditions.Sort((CallerIdBase left, CallerIdBase right) => left.EvaluationCost - right.EvaluationCost);
			for (int i = 0; i < this.conditions.Count; i++)
			{
				CallerIdBase callerIdBase = this.conditions[i];
				if (!flag && (callerIdBase is ADContactCallerId || callerIdBase is PersonaContactCallerId))
				{
					flag = true;
					this.matchedADContact = dataLoader.GetMatchingADContact(this.callerid);
				}
				if (!flag2 && (callerIdBase is ContactItemCallerId || callerIdBase is ContactFolderCallerId || callerIdBase is PersonaContactCallerId))
				{
					flag2 = true;
					this.matchedContacts = dataLoader.GetMatchingPersonalContacts(this.callerid);
				}
				if (!flag3 && callerIdBase is PersonaContactCallerId)
				{
					flag3 = true;
					this.matchedPersonaEmails = dataLoader.GetMatchingPersonaEmails();
				}
				if (callerIdBase.Evaluate(this))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400045C RID: 1116
		private PersonalContactInfo[] matchedContacts;

		// Token: 0x0400045D RID: 1117
		private List<CallerIdBase> conditions;

		// Token: 0x0400045E RID: 1118
		private ADContactInfo matchedADContact;

		// Token: 0x0400045F RID: 1119
		private List<string> matchedPersonaEmails;

		// Token: 0x04000460 RID: 1120
		private PhoneNumber callerid;

		// Token: 0x04000461 RID: 1121
		private UMSubscriber subscriber;
	}
}
