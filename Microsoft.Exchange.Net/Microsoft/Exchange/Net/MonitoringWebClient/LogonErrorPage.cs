using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x0200078A RID: 1930
	internal abstract class LogonErrorPage
	{
		// Token: 0x06002657 RID: 9815 RVA: 0x00050A0A File Offset: 0x0004EC0A
		protected LogonErrorPage(string errorString)
		{
			this.errorString = errorString;
			this.logonErrorType = LogonErrorType.Unknown;
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06002658 RID: 9816 RVA: 0x00050A20 File Offset: 0x0004EC20
		public string ErrorString
		{
			get
			{
				return this.errorString;
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06002659 RID: 9817 RVA: 0x00050A28 File Offset: 0x0004EC28
		public LogonErrorType LogonErrorType
		{
			get
			{
				return this.logonErrorType;
			}
		}

		// Token: 0x0400231B RID: 8987
		private readonly string errorString;

		// Token: 0x0400231C RID: 8988
		protected LogonErrorType logonErrorType;
	}
}
