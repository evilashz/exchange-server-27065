using System;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x02000029 RID: 41
	public class LiveIdErrorInformation
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00009D3A File Offset: 0x00007F3A
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00009D42 File Offset: 0x00007F42
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
			set
			{
				this.exception = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00009D4B File Offset: 0x00007F4B
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00009D53 File Offset: 0x00007F53
		public string Message
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00009D5C File Offset: 0x00007F5C
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00009D64 File Offset: 0x00007F64
		public string MessageDetails
		{
			get
			{
				return this.messageDetails;
			}
			set
			{
				this.messageDetails = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00009D6D File Offset: 0x00007F6D
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00009D75 File Offset: 0x00007F75
		public bool SendWatsonReport
		{
			get
			{
				return this.sendWatsonReport;
			}
			set
			{
				this.sendWatsonReport = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00009D7E File Offset: 0x00007F7E
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00009D86 File Offset: 0x00007F86
		public string Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				this.icon = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00009D8F File Offset: 0x00007F8F
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00009D97 File Offset: 0x00007F97
		public string Background
		{
			get
			{
				return this.background;
			}
			set
			{
				this.background = value;
			}
		}

		// Token: 0x04000140 RID: 320
		internal const string ContextKey = "LiveIdErrorInformation";

		// Token: 0x04000141 RID: 321
		private Exception exception;

		// Token: 0x04000142 RID: 322
		private string message;

		// Token: 0x04000143 RID: 323
		private string messageDetails;

		// Token: 0x04000144 RID: 324
		private bool sendWatsonReport;

		// Token: 0x04000145 RID: 325
		private string icon;

		// Token: 0x04000146 RID: 326
		private string background;
	}
}
