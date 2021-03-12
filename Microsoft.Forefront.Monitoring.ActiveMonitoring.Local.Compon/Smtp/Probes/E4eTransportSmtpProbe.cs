using System;
using System.IO;
using Microsoft.Exchange.Transport.RightsManagement;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x02000262 RID: 610
	public class E4eTransportSmtpProbe : TransportSmtpProbe
	{
		// Token: 0x06001458 RID: 5208 RVA: 0x0003C1FA File Offset: 0x0003A3FA
		protected override void SendMail(SmtpProbeWorkDefinition.SendMailDefinition sendMailDefinition, string lamNotificationID)
		{
			if (this.GetSenderTenantIdAsGuid(sendMailDefinition, out this.senderTenantId))
			{
				SendMailHelper.SendMail(base.Definition.Name, sendMailDefinition, lamNotificationID, new SendMailHelper.CreateMessageStreamDelegate(this.CreateE4eProtectedMessageStream));
			}
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0003C229 File Offset: 0x0003A429
		protected override string GetProbeResultComponent()
		{
			return ExchangeComponent.Rms.Name;
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x0003C238 File Offset: 0x0003A438
		private void CreateE4eProtectedMessageStream(string probeName, SmtpProbeWorkDefinition.SendMailDefinition sendMailDefinition, MemoryStream encryptedProbeMessageStream, string lamNotificationID)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				SendMailHelper.CreateDefaultMessageStream(probeName, sendMailDefinition, memoryStream, lamNotificationID);
				E4eProbeHelper.EncryptProbeMail(this.senderTenantId, sendMailDefinition.SenderUsername, sendMailDefinition.RecipientUsername, memoryStream, encryptedProbeMessageStream);
			}
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x0003C28C File Offset: 0x0003A48C
		private bool GetSenderTenantIdAsGuid(SmtpProbeWorkDefinition.SendMailDefinition sendMailDefinition, out Guid senderTenantGuid)
		{
			if (!Guid.TryParse(sendMailDefinition.SenderTenantID, out senderTenantGuid))
			{
				this.TraceError("Unable to parse SendMailDefinition.SenderTenantID as a Guid: {0}", new object[]
				{
					sendMailDefinition.SenderTenantID
				});
				return false;
			}
			return true;
		}

		// Token: 0x040009CA RID: 2506
		private Guid senderTenantId;
	}
}
