using System;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Monitoring;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000421 RID: 1057
	internal class MailboxServerCheck : AssistantTroubleshooterBase
	{
		// Token: 0x060024BB RID: 9403 RVA: 0x00092993 File Offset: 0x00090B93
		public MailboxServerCheck(PropertyBag fields) : base(fields)
		{
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x0009299C File Offset: 0x00090B9C
		public override MonitoringData InternalRunCheck()
		{
			MonitoringData monitoringData = new MonitoringData();
			if (!base.ExchangeServer.IsMailboxServer)
			{
				monitoringData.Events.Add(new MonitoringEvent(AssistantTroubleshooterBase.EventSource, 5200, EventTypeEnumeration.Error, Strings.TSNotAMailboxServer(base.ExchangeServer.Name)));
			}
			return monitoringData;
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x000929ED File Offset: 0x00090BED
		public override MonitoringData Resolve(MonitoringData monitoringData)
		{
			monitoringData.Events.Add(base.TSResolutionFailed(base.ExchangeServer.Name));
			return monitoringData;
		}
	}
}
