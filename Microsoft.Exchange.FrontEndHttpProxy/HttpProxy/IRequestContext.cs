using System;
using System.Web;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000090 RID: 144
	internal interface IRequestContext
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000449 RID: 1097
		HttpContext HttpContext { get; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600044A RID: 1098
		RequestDetailsLogger Logger { get; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600044B RID: 1099
		LatencyTracker LatencyTracker { get; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600044C RID: 1100
		int TraceContext { get; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600044D RID: 1101
		Guid ActivityId { get; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600044E RID: 1102
		IAuthBehavior AuthBehavior { get; }
	}
}
