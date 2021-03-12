using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000037 RID: 55
	internal class NtlmConnectivityStxLogger : StxLoggerBase
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00006734 File Offset: 0x00004934
		internal override string LogTypeName
		{
			get
			{
				return "NtlmConnectivity Logs";
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000673B File Offset: 0x0000493B
		internal override string LogComponent
		{
			get
			{
				return "NtlmConnectivity";
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00006742 File Offset: 0x00004942
		internal override string LogFilePrefix
		{
			get
			{
				return "Test-NtlmConnectivity_";
			}
		}
	}
}
