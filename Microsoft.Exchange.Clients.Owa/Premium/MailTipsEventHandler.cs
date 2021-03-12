using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004C1 RID: 1217
	[OwaEventNamespace("MailTips")]
	internal sealed class MailTipsEventHandler : OwaEventHandlerBase
	{
		// Token: 0x06002E71 RID: 11889 RVA: 0x001094CC File Offset: 0x001076CC
		[OwaEventParameter("From", typeof(RecipientInfo), false, true)]
		[OwaEvent("Get")]
		[OwaEventParameter("Recips", typeof(RecipientInfo), true, false)]
		[OwaEventParameter("DoesNeedConfig", typeof(bool), false, true)]
		[OwaEventParameter("HideMailTipsByDefault", typeof(bool), false, true)]
		public void GetMailTips()
		{
			RecipientInfo[] array = (RecipientInfo[])base.GetParameter("Recips");
			if (array != null && 0 < array.Length)
			{
				RecipientInfo senderInfo = (RecipientInfo)base.GetParameter("From");
				object parameter = base.GetParameter("DoesNeedConfig");
				bool doesNeedConfig = parameter != null && (bool)parameter;
				base.SaveHideMailTipsByDefault();
				base.UserContext.MailTipsNotificationHandler.BeginGetMailTipsInBatches(array, senderInfo, doesNeedConfig, null, null);
			}
		}

		// Token: 0x0400202A RID: 8234
		public const string EventNamespace = "MailTips";

		// Token: 0x0400202B RID: 8235
		public const string MethodGet = "Get";

		// Token: 0x0400202C RID: 8236
		public const string From = "From";

		// Token: 0x0400202D RID: 8237
		public const string Recipients = "Recips";

		// Token: 0x0400202E RID: 8238
		public const string DoesNeedConfig = "DoesNeedConfig";
	}
}
