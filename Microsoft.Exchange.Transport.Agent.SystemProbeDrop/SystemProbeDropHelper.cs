using System;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Transport.Agent.SystemProbeDrop
{
	// Token: 0x02000002 RID: 2
	internal class SystemProbeDropHelper
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static bool ShouldDropMessage(HeaderList headerList, string currentDropLocation)
		{
			ArgumentValidator.ThrowIfNull("headerList", headerList);
			ArgumentValidator.ThrowIfNullOrEmpty("currentDropLocation", currentDropLocation);
			if (headerList.FindFirst("X-LAMNotificationId") == null)
			{
				return false;
			}
			Header header = headerList.FindFirst("X-Exchange-System-Probe-Drop");
			return header != null && string.Equals(currentDropLocation, header.Value, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002124 File Offset: 0x00000324
		public static void DiscardMessage(MailItem mailItem)
		{
			ArgumentValidator.ThrowIfNull("mailItem", mailItem);
			while (mailItem.Recipients.Count != 0)
			{
				EnvelopeRecipient recipient = mailItem.Recipients[0];
				mailItem.Recipients.Remove(recipient, DsnType.Expanded, SmtpResponse.ProbeMessageDropped);
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000216C File Offset: 0x0000036C
		public static bool IsAgentEnabled()
		{
			return VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Transport.SystemProbeDropAgent.Enabled;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002197 File Offset: 0x00000397
		public static bool CheckMailItemHeaders(MailItem mailitem)
		{
			return mailitem != null && mailitem.MimeDocument != null && mailitem.MimeDocument.RootPart != null && mailitem.MimeDocument.RootPart.Headers != null;
		}
	}
}
