using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000043 RID: 67
	internal class ActiveDirectoryConnectivityConfigDCStxLogger : StxLoggerBase
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00006990 File Offset: 0x00004B90
		internal override string LogTypeName
		{
			get
			{
				return "ADConnectivityConfigDC Logs";
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00006997 File Offset: 0x00004B97
		internal override string LogComponent
		{
			get
			{
				return "ADConnectivityConfigDC";
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000699E File Offset: 0x00004B9E
		internal override string LogFilePrefix
		{
			get
			{
				return "Test-ADConnectivityConfigDC_";
			}
		}
	}
}
