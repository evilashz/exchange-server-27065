using System;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Monitoring;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200041E RID: 1054
	internal class MailboxAssistantsServiceStatusCheck : AssistantTroubleshooterBase
	{
		// Token: 0x060024A9 RID: 9385 RVA: 0x00092477 File Offset: 0x00090677
		public MailboxAssistantsServiceStatusCheck(PropertyBag fields) : base(fields)
		{
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x00092480 File Offset: 0x00090680
		public override MonitoringData InternalRunCheck()
		{
			MonitoringData monitoringData = new MonitoringData();
			using (ServiceController serviceController = new ServiceController("MsExchangeMailboxAssistants", base.ExchangeServer.Fqdn))
			{
				if (serviceController.Status != ServiceControllerStatus.Running)
				{
					monitoringData.Events.Add(new MonitoringEvent(AssistantTroubleshooterBase.EventSource, 5201, EventTypeEnumeration.Error, Strings.MailboxAssistantsServiceNotRunning(base.ExchangeServer.Fqdn, serviceController.Status.ToString())));
				}
			}
			return monitoringData;
		}
	}
}
