using System;
using System.Linq;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200027C RID: 636
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class ClientPerformance : IClientPerformanceService
	{
		// Token: 0x060029CC RID: 10700 RVA: 0x00083590 File Offset: 0x00081790
		public string ReportWatson(ClientWatson report)
		{
			if (report != null)
			{
				try
				{
					ClientExceptionLogger.Instance.LogEvent(new ClientExceptionLoggerEvent(report));
				}
				catch
				{
				}
			}
			return "OK";
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x000835E0 File Offset: 0x000817E0
		public bool LogClientDatapoint(Datapoint[] datapoints)
		{
			if (datapoints == null)
			{
				return false;
			}
			ClientLogger.Instance.LogEvent((from c in datapoints
			where c != null
			select new ClientLogEvent(c)).ToArray<ClientLogEvent>());
			return true;
		}
	}
}
