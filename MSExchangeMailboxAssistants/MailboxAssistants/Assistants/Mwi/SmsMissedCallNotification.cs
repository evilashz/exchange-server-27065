using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.InfoWorker.AssistantsClientResources;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Mwi
{
	// Token: 0x0200010A RID: 266
	internal class SmsMissedCallNotification : SmsNotification
	{
		// Token: 0x06000AF2 RID: 2802 RVA: 0x00047420 File Offset: 0x00045620
		internal SmsMissedCallNotification(MailboxSession session, CultureInfo preferredCulture, StoreObject item, UMDialPlan dialPlan) : base(session, preferredCulture, item, dialPlan)
		{
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x00047430 File Offset: 0x00045630
		protected override string FullBaseMessage
		{
			get
			{
				return Strings.SMSMissedCallWithCallerInfo(base.Name, base.CallerId).ToString(base.GetMailboxCulture());
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x0004745C File Offset: 0x0004565C
		protected override string BaseMessageWithName
		{
			get
			{
				return Strings.SMSMissedCallWithCallerId(base.Name).ToString(base.GetMailboxCulture());
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x00047484 File Offset: 0x00045684
		protected override string BaseMessageWithCallerId
		{
			get
			{
				return Strings.SMSMissedCallWithCallerId(base.CallerId).ToString(base.GetMailboxCulture());
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x000474AA File Offset: 0x000456AA
		protected override int MinimumMaxUsableCharacters
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x040006F9 RID: 1785
		private const int MinimumUsableCharacters = 11;
	}
}
