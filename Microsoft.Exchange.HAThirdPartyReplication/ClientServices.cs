using System;
using System.ServiceModel;

namespace Microsoft.Exchange.ThirdPartyReplication
{
	// Token: 0x02000002 RID: 2
	internal static class ClientServices
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static void CallService(ClientServices.GenericCallDelegate del)
		{
			try
			{
				del();
			}
			catch (TimeoutException ex)
			{
				throw new FailedCommunicationException(ex.Message, ex);
			}
			catch (CommunicationException ex2)
			{
				throw new FailedCommunicationException(ex2.Message, ex2);
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002120 File Offset: 0x00000320
		public static NetNamedPipeBinding SetupBinding(TimeSpan openTimeout, TimeSpan sendTimeout, TimeSpan receiveTimeout)
		{
			return new NetNamedPipeBinding
			{
				OpenTimeout = openTimeout,
				SendTimeout = sendTimeout,
				ReceiveTimeout = receiveTimeout
			};
		}

		// Token: 0x02000003 RID: 3
		// (Invoke) Token: 0x06000004 RID: 4
		public delegate void GenericCallDelegate();
	}
}
