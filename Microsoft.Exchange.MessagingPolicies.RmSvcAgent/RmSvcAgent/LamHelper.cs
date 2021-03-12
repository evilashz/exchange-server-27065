using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.Transport;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.MessagingPolicies.RmSvcAgent
{
	// Token: 0x02000002 RID: 2
	internal static class LamHelper
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static void PublishSuccessfulIrmDecryptionToLAM(MailItem mailItem)
		{
			ArgumentValidator.ThrowIfNull("mailItem", mailItem);
			if (mailItem.IsProbeMessage)
			{
				EventNotificationItem eventNotificationItem = LamHelper.CreateEventNotificationItem(mailItem);
				eventNotificationItem.AddCustomProperty("StateAttribute3", "IrmMessageSuccessfullyDecrypted");
				eventNotificationItem.Publish(false);
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002110 File Offset: 0x00000310
		public static void PublishSuccessfulIrmEncryptionToLAM(MailItem mailItem)
		{
			ArgumentValidator.ThrowIfNull("mailItem", mailItem);
			if (mailItem.IsProbeMessage)
			{
				EventNotificationItem eventNotificationItem = LamHelper.CreateEventNotificationItem(mailItem);
				eventNotificationItem.AddCustomProperty("StateAttribute3", "IrmMessageSuccessfullyEncrypted");
				eventNotificationItem.Publish(false);
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002150 File Offset: 0x00000350
		public static void PublishSuccessfulE4eDecryptionToLAM(MailItem mailItem)
		{
			ArgumentValidator.ThrowIfNull("mailItem", mailItem);
			if (mailItem.IsProbeMessage)
			{
				EventNotificationItem eventNotificationItem = LamHelper.CreateEventNotificationItem(mailItem);
				eventNotificationItem.AddCustomProperty("StateAttribute3", "E4eMessageSuccessfullyDecrypted");
				eventNotificationItem.Publish(false);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002190 File Offset: 0x00000390
		public static void PublishSuccessfulE4eEncryptionToLAM(MailItem mailItem)
		{
			ArgumentValidator.ThrowIfNull("mailItem", mailItem);
			if (mailItem.IsProbeMessage)
			{
				EventNotificationItem eventNotificationItem = LamHelper.CreateEventNotificationItem(mailItem);
				eventNotificationItem.AddCustomProperty("StateAttribute3", "E4eMessageSuccessfullyEncrypted");
				eventNotificationItem.Publish(false);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021D0 File Offset: 0x000003D0
		private static EventNotificationItem CreateEventNotificationItem(MailItem mailItem)
		{
			string text = string.Empty;
			TransportMailItem transportMailItem = TransportUtils.GetTransportMailItem(mailItem);
			if (transportMailItem != null)
			{
				text = transportMailItem.ProbeName;
				if (string.IsNullOrEmpty(text))
				{
					transportMailItem.UpdateCachedHeaders();
					text = transportMailItem.ProbeName;
				}
			}
			EventNotificationItem eventNotificationItem = new EventNotificationItem(ExchangeComponent.Transport.Name, ExchangeComponent.Rms.Name, text, ResultSeverityLevel.Verbose);
			eventNotificationItem.AddCustomProperty("StateAttribute1", mailItem.Message.MessageId);
			eventNotificationItem.AddCustomProperty("StateAttribute2", "AGENTINFO");
			eventNotificationItem.StateAttribute4 = mailItem.SystemProbeId.ToString();
			return eventNotificationItem;
		}

		// Token: 0x04000001 RID: 1
		private const string LamMessageIdAttributeName = "StateAttribute1";

		// Token: 0x04000002 RID: 2
		private const string LamTransportSmtpProbeResultTypeAttributeName = "StateAttribute2";

		// Token: 0x04000003 RID: 3
		private const string LamTransportSmtpProbeResultValueAttributeName = "StateAttribute3";

		// Token: 0x04000004 RID: 4
		private const string LamTransportSmtpProbeResultTypeValue = "AGENTINFO";

		// Token: 0x04000005 RID: 5
		private const string LamIrmTransportSmtpProbeSuccessfullyDecryptedValue = "IrmMessageSuccessfullyDecrypted";

		// Token: 0x04000006 RID: 6
		private const string LamIrmTransportSmtpProbeSuccessfullyEncryptedValue = "IrmMessageSuccessfullyEncrypted";

		// Token: 0x04000007 RID: 7
		private const string LamE4eTransportSmtpProbeSuccessfullyDecryptedValue = "E4eMessageSuccessfullyDecrypted";

		// Token: 0x04000008 RID: 8
		private const string LamE4eTransportSmtpProbeSuccessfullyEncryptedValue = "E4eMessageSuccessfullyEncrypted";
	}
}
