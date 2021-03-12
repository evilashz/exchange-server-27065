using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Search
{
	// Token: 0x02000226 RID: 550
	internal class SearchLoggingEvent : EventArgs
	{
		// Token: 0x06000F27 RID: 3879 RVA: 0x00043EE5 File Offset: 0x000420E5
		internal SearchLoggingEvent(LocalizedString loggingMessage)
		{
			this.loggingMessage = loggingMessage;
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x00043EF4 File Offset: 0x000420F4
		// (set) Token: 0x06000F29 RID: 3881 RVA: 0x00043EFC File Offset: 0x000420FC
		internal LocalizedString LoggingMessage
		{
			get
			{
				return this.loggingMessage;
			}
			set
			{
				this.loggingMessage = value;
			}
		}

		// Token: 0x04000A69 RID: 2665
		private LocalizedString loggingMessage;
	}
}
