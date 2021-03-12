using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000039 RID: 57
	internal class GlobalLocatorServiceStxLogger : StxLoggerBase
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000676E File Offset: 0x0000496E
		internal override string LogTypeName
		{
			get
			{
				return "GlobalLocatorService Logs";
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00006775 File Offset: 0x00004975
		internal override string LogComponent
		{
			get
			{
				return "GlobalLocatorService";
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000677C File Offset: 0x0000497C
		internal override string LogFilePrefix
		{
			get
			{
				return "Test-GlobalLocatorService_";
			}
		}
	}
}
