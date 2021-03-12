using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200003B RID: 59
	internal class ActiveDirectoryConnectivityStxLogger : StxLoggerBase
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000067A8 File Offset: 0x000049A8
		internal override string LogTypeName
		{
			get
			{
				return "ADConnectivity Logs";
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000170 RID: 368 RVA: 0x000067AF File Offset: 0x000049AF
		internal override string LogComponent
		{
			get
			{
				return "ADConnectivity";
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000171 RID: 369 RVA: 0x000067B6 File Offset: 0x000049B6
		internal override string LogFilePrefix
		{
			get
			{
				return "Test-ADConnectivity_";
			}
		}
	}
}
