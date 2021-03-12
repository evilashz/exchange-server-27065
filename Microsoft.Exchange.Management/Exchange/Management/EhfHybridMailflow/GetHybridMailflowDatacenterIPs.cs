using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Monitoring;

namespace Microsoft.Exchange.Management.EhfHybridMailflow
{
	// Token: 0x02000875 RID: 2165
	[Cmdlet("Get", "HybridMailflowDatacenterIPs")]
	public sealed class GetHybridMailflowDatacenterIPs : Task
	{
		// Token: 0x06004B0B RID: 19211 RVA: 0x001374CE File Offset: 0x001356CE
		protected override void InternalProcessRecord()
		{
			this.WriteResult(this.GetDatacenterIPs());
		}

		// Token: 0x06004B0C RID: 19212 RVA: 0x001374DC File Offset: 0x001356DC
		private void WriteResult(object dataObject)
		{
			base.WriteObject(dataObject);
		}

		// Token: 0x06004B0D RID: 19213 RVA: 0x001374E8 File Offset: 0x001356E8
		private HybridMailflowDatacenterIPs GetDatacenterIPs()
		{
			MultiValuedProperty<IPRange> datacenterIPs;
			HybridMailflowDatacenterIPs result;
			if (HygieneDCSettings.GetFfoDCPublicIPAddresses(out datacenterIPs))
			{
				result = new HybridMailflowDatacenterIPs(datacenterIPs);
			}
			else
			{
				LocalizedException exception = new LocalizedException(Strings.HybridMailflowGetFfoDCPublicIPAddressesFailed);
				base.WriteError(exception, ExchangeErrorCategory.ServerOperation, this);
				result = null;
			}
			return result;
		}

		// Token: 0x06004B0E RID: 19214 RVA: 0x00137522 File Offset: 0x00135722
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || DataAccessHelper.IsDataAccessKnownException(exception) || MonitoringHelper.IsKnownExceptionForMonitoring(exception);
		}

		// Token: 0x06004B0F RID: 19215 RVA: 0x0013753D File Offset: 0x0013573D
		private void WriteErrorAndMonitoringEvent(Exception exception, ExchangeErrorCategory errorCategory, object target, int eventId, string eventSource)
		{
			this.monitoringData.Events.Add(new MonitoringEvent(eventSource, eventId, EventTypeEnumeration.Error, exception.Message));
			base.WriteError(exception, (ErrorCategory)errorCategory, target);
		}

		// Token: 0x04002D08 RID: 11528
		private const string CmdletNoun = "HybridMailflowDatacenterIPs";

		// Token: 0x04002D09 RID: 11529
		private const string CmdletMonitoringEventSource = "MSExchange Monitoring HybridMailflowDatacenterIPs";

		// Token: 0x04002D0A RID: 11530
		private MonitoringData monitoringData = new MonitoringData();
	}
}
