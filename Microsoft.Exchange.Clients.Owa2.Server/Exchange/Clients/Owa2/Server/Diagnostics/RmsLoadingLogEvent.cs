using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000458 RID: 1112
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class RmsLoadingLogEvent : ILogEvent
	{
		// Token: 0x06002567 RID: 9575 RVA: 0x00087A00 File Offset: 0x00085C00
		public RmsLoadingLogEvent(OrganizationId organizationId, Exception exception)
		{
			this.organizationId = organizationId;
			this.exception = exception;
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06002568 RID: 9576 RVA: 0x00087A16 File Offset: 0x00085C16
		public string EventId
		{
			get
			{
				return "RmsLoadingLogEvent";
			}
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x00087A20 File Offset: 0x00085C20
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			return new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("Org", (this.organizationId == null) ? "null" : this.organizationId.ToString()),
				new KeyValuePair<string, object>("Exc", (this.exception == null) ? "null" : this.exception.ToString())
			};
		}

		// Token: 0x04001589 RID: 5513
		private readonly OrganizationId organizationId;

		// Token: 0x0400158A RID: 5514
		private readonly Exception exception;
	}
}
