using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000036 RID: 54
	internal class LiveIdAuthenticationStxLogger : StxLoggerBase
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00006717 File Offset: 0x00004917
		internal override string LogTypeName
		{
			get
			{
				return "LiveIdAuthentication Logs";
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000671E File Offset: 0x0000491E
		internal override string LogComponent
		{
			get
			{
				return "LiveIdAuthentication";
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00006725 File Offset: 0x00004925
		internal override string LogFilePrefix
		{
			get
			{
				return "Test-LiveIdAuthentication_";
			}
		}
	}
}
