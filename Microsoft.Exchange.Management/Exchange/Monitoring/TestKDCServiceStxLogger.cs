using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000045 RID: 69
	internal class TestKDCServiceStxLogger : StxLoggerBase
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000197 RID: 407 RVA: 0x000069CA File Offset: 0x00004BCA
		internal override string LogTypeName
		{
			get
			{
				return "TestKDCService Logs";
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000198 RID: 408 RVA: 0x000069D1 File Offset: 0x00004BD1
		internal override string LogComponent
		{
			get
			{
				return "TestKDCService";
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000199 RID: 409 RVA: 0x000069D8 File Offset: 0x00004BD8
		internal override string LogFilePrefix
		{
			get
			{
				return "TestKDCService_";
			}
		}
	}
}
