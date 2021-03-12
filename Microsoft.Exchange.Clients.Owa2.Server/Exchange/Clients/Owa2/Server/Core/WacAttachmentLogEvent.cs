using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200004F RID: 79
	internal class WacAttachmentLogEvent : ILogEvent
	{
		// Token: 0x0600022C RID: 556 RVA: 0x00008A24 File Offset: 0x00006C24
		public WacAttachmentLogEvent(string msg) : this(msg, null)
		{
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00008A2E File Offset: 0x00006C2E
		public WacAttachmentLogEvent(string msg, Exception ex)
		{
			this.message = msg;
			this.handledException = ex;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00008A44 File Offset: 0x00006C44
		public string EventId
		{
			get
			{
				return "WacAttachmentLogEvent";
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00008A4C File Offset: 0x00006C4C
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			ICollection<KeyValuePair<string, object>> collection = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("MSG", this.message)
			};
			if (this.handledException != null)
			{
				collection.Add(new KeyValuePair<string, object>("Ex", this.handledException.Message));
				collection.Add(new KeyValuePair<string, object>("ExS", this.handledException.StackTrace));
				Exception innerException = this.handledException.InnerException;
				int num = 1;
				while (innerException != null && num <= 10)
				{
					collection.Add(new KeyValuePair<string, object>("Ex" + num, innerException.Message ?? "Not specified"));
					collection.Add(new KeyValuePair<string, object>("ExS" + num, innerException.StackTrace ?? "Not specified"));
					innerException = innerException.InnerException;
					num++;
				}
			}
			return collection;
		}

		// Token: 0x040000F4 RID: 244
		private readonly string message;

		// Token: 0x040000F5 RID: 245
		private readonly Exception handledException;
	}
}
