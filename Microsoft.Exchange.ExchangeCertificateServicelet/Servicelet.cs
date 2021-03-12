using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ServiceHost;
using Microsoft.Exchange.Servicelets.ExchangeCertificate.Messages;

namespace Microsoft.Exchange.Servicelets.ExchangeCertificate
{
	// Token: 0x02000002 RID: 2
	public class Servicelet : Servicelet
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020F0 File Offset: 0x000002F0
		public override void Work()
		{
			bool flag = false;
			try
			{
				Exception ex;
				if (!ExchangeCertificateServer.Start(out ex))
				{
					this.eventLog.LogEvent(MSExchangeExchangeCertificateEventLogConstants.Tuple_PermanentException, null, new object[]
					{
						ex.Message
					});
				}
				else if (!ExchangeCertificateServer2.Start(out ex))
				{
					this.eventLog.LogEvent(MSExchangeExchangeCertificateEventLogConstants.Tuple_PermanentException, null, new object[]
					{
						ex.Message
					});
					ExchangeCertificateServer.Stop();
				}
				else
				{
					flag = true;
					base.StopEvent.WaitOne();
				}
			}
			finally
			{
				if (flag)
				{
					ExchangeCertificateServer2.Stop();
					ExchangeCertificateServer.Stop();
				}
			}
		}

		// Token: 0x04000001 RID: 1
		private static readonly Guid ComponentGuid = new Guid("7ACE8E2A-A2F7-4229-8641-CACE7C48EC2B");

		// Token: 0x04000002 RID: 2
		private readonly ExEventLog eventLog = new ExEventLog(Servicelet.ComponentGuid, "MSExchange Certificate");
	}
}
