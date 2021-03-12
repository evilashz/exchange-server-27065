using System;

namespace Microsoft.Exchange.Management.MailboxTransportSubmission.MapiProbe
{
	// Token: 0x02000052 RID: 82
	internal class MapiSubmitMonitoringSystemProbeFactory : MapiSubmitSystemProbeFactory
	{
		// Token: 0x060002F1 RID: 753 RVA: 0x0000BDA2 File Offset: 0x00009FA2
		private MapiSubmitMonitoringSystemProbeFactory()
		{
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000BDAA File Offset: 0x00009FAA
		public static MapiSubmitMonitoringSystemProbeFactory CreateInstance()
		{
			return new MapiSubmitMonitoringSystemProbeFactory();
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000BDB1 File Offset: 0x00009FB1
		public override IMapiMessageSubmitter MakeMapiMessageSubmitter()
		{
			return MapiMessageSubmitter.CreateInstance();
		}
	}
}
