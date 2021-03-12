using System;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Management.MailboxTransportSubmission.MapiProbe;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Transport.Probes
{
	// Token: 0x02000278 RID: 632
	internal class SendMapiMailDefinitionFactory
	{
		// Token: 0x060014DB RID: 5339 RVA: 0x0003EEE4 File Offset: 0x0003D0E4
		public static SendMapiMailDefinition CreateInstance(string lamNotificationId, ProbeDefinition probeDefinition, IMailboxProvider mailboxProviderInstance, out MailboxSelectionResult mailboxSelectionResult)
		{
			mailboxSelectionResult = MailboxSelectionResult.Success;
			if (string.IsNullOrEmpty(lamNotificationId))
			{
				throw new ArgumentNullException("lamNotificationId");
			}
			if (probeDefinition == null)
			{
				throw new ArgumentNullException("probeDefinition");
			}
			if (string.IsNullOrWhiteSpace(probeDefinition.ExtensionAttributes))
			{
				throw new ArgumentNullException("probeDefinition.ExtensionAttributes");
			}
			if (mailboxProviderInstance == null)
			{
				throw new ArgumentNullException("mailboxProviderInstance");
			}
			XmlDocument xmlDocument = new SafeXmlDocument();
			xmlDocument.LoadXml(probeDefinition.ExtensionAttributes);
			Guid mbxGuid;
			Guid mdbGuid;
			string emailAddress;
			mailboxSelectionResult = mailboxProviderInstance.TryGetMailboxToUse(out mbxGuid, out mdbGuid, out emailAddress);
			return SendMapiMailDefinitionFactory.FromXml(xmlDocument, lamNotificationId, emailAddress, mbxGuid, mdbGuid, mailboxSelectionResult != MailboxSelectionResult.Success);
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0003EF70 File Offset: 0x0003D170
		public static SendMapiMailDefinition CreateMapiMailInstance(string lamNotificationId, ProbeDefinition probeDefinition)
		{
			if (string.IsNullOrEmpty(lamNotificationId))
			{
				throw new ArgumentNullException("lamNotificationId");
			}
			if (probeDefinition == null)
			{
				throw new ArgumentNullException("probeDefinition");
			}
			if (string.IsNullOrWhiteSpace(probeDefinition.ExtensionAttributes))
			{
				throw new ArgumentNullException("probeDefinition.ExtensionAttributes");
			}
			XmlDocument xmlDocument = new SafeXmlDocument();
			xmlDocument.LoadXml(probeDefinition.ExtensionAttributes);
			return SendMapiMailDefinitionFactory.FromXml(xmlDocument, lamNotificationId, null, Guid.Empty, Guid.Empty, true);
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0003EFDC File Offset: 0x0003D1DC
		private static SendMapiMailDefinition FromXml(XmlDocument workContext, string lamNotificationId, string emailAddress, Guid mbxGuid, Guid mdbGuid, bool skipSenderValidation)
		{
			Utils.CheckXmlElement(workContext.SelectSingleNode("ExtensionAttributes/WorkContext/SendMapiMail"), "SendMapiMail");
			XmlElement xmlElement = Utils.CheckXmlElement(workContext.SelectSingleNode("ExtensionAttributes/WorkContext/SendMapiMail/Message"), "Message");
			string arg = xmlElement.GetAttribute("Subject") ?? "MapiSubmitLAMProbe";
			return SendMapiMailDefinition.CreateInstance(string.Format("{0}-{1}", lamNotificationId, arg), xmlElement.GetAttribute("Body"), Utils.CheckNullOrWhiteSpace(xmlElement.GetAttribute("MessageClass"), "MessageClass"), Utils.GetBoolean(xmlElement.GetAttribute("DoNotDeliver"), "DoNotDeliver", true), Utils.GetBoolean(xmlElement.GetAttribute("DropMessageInHub"), "DropMessageInHub", true), Utils.GetBoolean(xmlElement.GetAttribute("DeleteAfterSubmit"), "DeleteAfterSubmit", true), emailAddress, mbxGuid, mdbGuid, emailAddress, skipSenderValidation);
		}
	}
}
