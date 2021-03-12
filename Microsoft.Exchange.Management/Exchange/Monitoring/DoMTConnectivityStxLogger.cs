using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000046 RID: 70
	internal class DoMTConnectivityStxLogger : StxLoggerBase
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000069E7 File Offset: 0x00004BE7
		internal override string LogTypeName
		{
			get
			{
				return "DoMTConnectivity Logs";
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600019C RID: 412 RVA: 0x000069EE File Offset: 0x00004BEE
		internal override string LogComponent
		{
			get
			{
				return "DoMTConnectivity";
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600019D RID: 413 RVA: 0x000069F5 File Offset: 0x00004BF5
		internal override string LogFilePrefix
		{
			get
			{
				return "DoMTConnectivity_";
			}
		}
	}
}
