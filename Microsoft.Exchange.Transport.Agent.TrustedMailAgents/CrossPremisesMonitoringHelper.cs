using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.SecureMail;

namespace Microsoft.Exchange.Transport.Agent.TrustedMail
{
	// Token: 0x02000004 RID: 4
	internal class CrossPremisesMonitoringHelper
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000024C8 File Offset: 0x000006C8
		public static bool TryHandleCrossPremisesProbe(MailItem mailItem, SmtpServer smtpServer)
		{
			if (CrossPremisesMonitoringHelper.IsCrossPremisesProbe(mailItem))
			{
				EmailMessage response = CrossPremisesMonitoringHelper.GetResponse(mailItem);
				SubmitHelper.CreateTransportMailItemWithNullReversePathAndSubmitWithoutDSNs(((TransportMailItemWrapper)mailItem).TransportMailItem, response, smtpServer.Name, smtpServer.Version, "InboundTrustAgent");
				CrossPremisesMonitoringHelper.RemoveProbeRecipient(mailItem);
				return true;
			}
			return false;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002510 File Offset: 0x00000710
		internal static bool IsCrossPremisesProbe(MailItem mailItem)
		{
			if (mailItem == null || mailItem.Recipients.Count != 1 || !MultilevelAuth.IsInternalMail(mailItem.MimeDocument.RootPart.Headers))
			{
				return false;
			}
			string subject = mailItem.Message.Subject;
			string text = mailItem.FromAddress.ToString();
			string text2 = mailItem.Recipients[0].Address.ToString();
			Guid guid;
			return !string.IsNullOrEmpty(subject) && subject.StartsWith("CrossPremiseMailFlowMonitoring-") && text.StartsWith("SystemMailbox") && text2.StartsWith("FederatedEmail.4c1f4d8b-8179-4148-93bf-00a95fa1e042") && GuidHelper.TryParseGuid(subject.Substring("CrossPremiseMailFlowMonitoring-".Length), out guid);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000025D4 File Offset: 0x000007D4
		internal static EmailMessage GetResponse(MailItem mailItem)
		{
			EmailMessage emailMessage = EmailMessage.Create(BodyFormat.Text);
			emailMessage.Subject = "RSP: " + mailItem.Message.Subject;
			emailMessage.From = new EmailRecipient(string.Empty, mailItem.Recipients[0].Address.ToString());
			emailMessage.To.Add(new EmailRecipient(string.Empty, mailItem.FromAddress.ToString()));
			using (Stream contentWriteStream = emailMessage.Body.GetContentWriteStream())
			{
				using (new StreamWriter(contentWriteStream, Encoding.ASCII))
				{
					foreach (Header header in mailItem.Message.RootPart.Headers)
					{
						header.WriteTo(contentWriteStream);
					}
				}
			}
			return emailMessage;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000026F8 File Offset: 0x000008F8
		private static void RemoveProbeRecipient(MailItem mailItem)
		{
			((MailRecipientCollectionWrapper)mailItem.Recipients).Remove(mailItem.Recipients[0], false);
		}

		// Token: 0x04000004 RID: 4
		private const string SystemMailboxAddressPrefix = "SystemMailbox";

		// Token: 0x04000005 RID: 5
		private const string TargetLocalPart = "FederatedEmail.4c1f4d8b-8179-4148-93bf-00a95fa1e042";

		// Token: 0x04000006 RID: 6
		private const string ProbeSubjectPrefix = "CrossPremiseMailFlowMonitoring-";

		// Token: 0x04000007 RID: 7
		private const string ResponsePrefix = "RSP: ";

		// Token: 0x04000008 RID: 8
		private const string AgentName = "InboundTrustAgent";
	}
}
