using System;
using System.IO;
using Microsoft.Exchange.Transport.RightsManagement;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x02000263 RID: 611
	public class IrmTransportSmtpProbe : TransportSmtpProbe
	{
		// Token: 0x0600145D RID: 5213 RVA: 0x0003C2CE File Offset: 0x0003A4CE
		protected override void SendMail(SmtpProbeWorkDefinition.SendMailDefinition sendMailDefinition, string lamNotificationID)
		{
			if (this.GetRecipientTenantIdAsGuid(sendMailDefinition, out this.recipientTenantId))
			{
				SendMailHelper.SendMail(base.Definition.Name, sendMailDefinition, lamNotificationID, new SendMailHelper.CreateMessageStreamDelegate(this.CreateIrmProtectedMessageStream));
			}
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x0003C2FD File Offset: 0x0003A4FD
		protected override string GetProbeResultComponent()
		{
			return ExchangeComponent.Rms.Name;
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x0003C30C File Offset: 0x0003A50C
		private void CreateIrmProtectedMessageStream(string probeName, SmtpProbeWorkDefinition.SendMailDefinition sendMailDefinition, MemoryStream encryptedProbeMessageStream, string lamNotificationID)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				SendMailHelper.CreateDefaultMessageStream(probeName, sendMailDefinition, memoryStream, lamNotificationID);
				IrmProbeHelper.EncryptProbeMail(this.recipientTenantId, memoryStream, encryptedProbeMessageStream);
			}
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0003C354 File Offset: 0x0003A554
		private bool GetRecipientTenantIdAsGuid(SmtpProbeWorkDefinition.SendMailDefinition sendMailDefinition, out Guid recipientTenantGuid)
		{
			if (!Guid.TryParse(sendMailDefinition.RecipientTenantID, out recipientTenantGuid))
			{
				this.TraceError("Unable to parse SendMailDefinition.RecipientTenantID as a Guid: {0}", new object[]
				{
					sendMailDefinition.RecipientTenantID
				});
				return false;
			}
			return true;
		}

		// Token: 0x040009CB RID: 2507
		private Guid recipientTenantId;
	}
}
