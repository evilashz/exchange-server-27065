using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200003D RID: 61
	internal class ForwardSyncCookieResponderStxLogger : StxLoggerBase
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000688C File Offset: 0x00004A8C
		internal override string LogTypeName
		{
			get
			{
				return "ForwardSyncCookieResponderStxLogger Logs";
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00006893 File Offset: 0x00004A93
		internal override string LogComponent
		{
			get
			{
				return "ForwardSyncCookieResponder";
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000689A File Offset: 0x00004A9A
		internal override string LogFilePrefix
		{
			get
			{
				return "Test-ForwardSyncCookieResponder_";
			}
		}
	}
}
