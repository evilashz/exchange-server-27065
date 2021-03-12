using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x02000479 RID: 1145
	public class FlightConfigurationException : Exception
	{
		// Token: 0x060026CA RID: 9930 RVA: 0x0008CA3C File Offset: 0x0008AC3C
		public FlightConfigurationException(string message) : base(message)
		{
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x0008CA45 File Offset: 0x0008AC45
		public FlightConfigurationException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
