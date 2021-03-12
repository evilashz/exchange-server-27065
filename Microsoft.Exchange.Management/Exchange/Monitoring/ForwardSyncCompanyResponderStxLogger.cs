using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200003F RID: 63
	internal class ForwardSyncCompanyResponderStxLogger : StxLoggerBase
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000691C File Offset: 0x00004B1C
		internal override string LogTypeName
		{
			get
			{
				return "ForwardSyncCompanyResponderStxLogger Logs";
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00006923 File Offset: 0x00004B23
		internal override string LogComponent
		{
			get
			{
				return "ForwardSyncCompanyResponder";
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000692A File Offset: 0x00004B2A
		internal override string LogFilePrefix
		{
			get
			{
				return "Test-ForwardSyncCompanyResponder_";
			}
		}
	}
}
