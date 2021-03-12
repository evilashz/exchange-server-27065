using System;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.DNSPlusPlus
{
	// Token: 0x02000008 RID: 8
	public class DnsMonitorException : Exception
	{
		// Token: 0x06000040 RID: 64 RVA: 0x0000382F File Offset: 0x00001A2F
		public DnsMonitorException()
		{
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003837 File Offset: 0x00001A37
		public DnsMonitorException(Exception innerException) : base(null, innerException)
		{
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003841 File Offset: 0x00001A41
		public DnsMonitorException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
