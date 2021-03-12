using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020006FC RID: 1788
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Notification
	{
		// Token: 0x060046E0 RID: 18144 RVA: 0x0012D97E File Offset: 0x0012BB7E
		internal Notification(NotificationType type)
		{
			EnumValidator.AssertValid<NotificationType>(type);
			this.type = type;
			this.createTime = Stopwatch.GetTimestamp();
		}

		// Token: 0x17001499 RID: 5273
		// (get) Token: 0x060046E1 RID: 18145 RVA: 0x0012D99E File Offset: 0x0012BB9E
		public NotificationType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700149A RID: 5274
		// (get) Token: 0x060046E2 RID: 18146 RVA: 0x0012D9A6 File Offset: 0x0012BBA6
		public long CreateTime
		{
			get
			{
				return this.createTime;
			}
		}

		// Token: 0x040026B6 RID: 9910
		private readonly NotificationType type;

		// Token: 0x040026B7 RID: 9911
		private readonly long createTime;
	}
}
