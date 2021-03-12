using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000176 RID: 374
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class GroupConcurrencyLogEvent : ILogEvent
	{
		// Token: 0x06000DBC RID: 3516 RVA: 0x00033AE3 File Offset: 0x00031CE3
		internal GroupConcurrencyLogEvent(string groupName, NotificationType notificationType, long elapsedTime, int oldConcurrency, int currentConcurrency)
		{
			if (groupName == null)
			{
				throw new ArgumentNullException("groupName");
			}
			this.groupNameValue = groupName;
			this.notificationType = notificationType;
			this.elapsedTime = elapsedTime;
			this.previousConcurrencyCountValue = oldConcurrency;
			this.currentConcurrencyCountValue = currentConcurrency;
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x00033B1E File Offset: 0x00031D1E
		public string EventId
		{
			get
			{
				return "GroupConcurrency";
			}
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x00033B28 File Offset: 0x00031D28
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			return new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("GN", this.groupNameValue),
				new KeyValuePair<string, object>("NT", this.notificationType.ToString()),
				new KeyValuePair<string, object>("GUCET", this.elapsedTime.ToString(CultureInfo.InvariantCulture)),
				new KeyValuePair<string, object>("GUCPC", this.previousConcurrencyCountValue),
				new KeyValuePair<string, object>("GUCC", this.currentConcurrencyCountValue)
			};
		}

		// Token: 0x04000851 RID: 2129
		private const string EventIdValue = "GroupConcurrency";

		// Token: 0x04000852 RID: 2130
		private const string GroupNameKey = "GN";

		// Token: 0x04000853 RID: 2131
		private const string NotificationTypeKey = "NT";

		// Token: 0x04000854 RID: 2132
		private const string ElapsedTimeKey = "GUCET";

		// Token: 0x04000855 RID: 2133
		private const string CurrentConcurrencyCountKey = "GUCC";

		// Token: 0x04000856 RID: 2134
		private const string PrevConcurrencyCountKey = "GUCPC";

		// Token: 0x04000857 RID: 2135
		private readonly NotificationType notificationType;

		// Token: 0x04000858 RID: 2136
		private readonly long elapsedTime;

		// Token: 0x04000859 RID: 2137
		private readonly int currentConcurrencyCountValue;

		// Token: 0x0400085A RID: 2138
		private readonly int previousConcurrencyCountValue;

		// Token: 0x0400085B RID: 2139
		private readonly string groupNameValue;
	}
}
