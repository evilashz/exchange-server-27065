using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001A0 RID: 416
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PeopleIKnowRowNotificationLogEvent : ILogEvent
	{
		// Token: 0x06000F0A RID: 3850 RVA: 0x0003A925 File Offset: 0x00038B25
		internal PeopleIKnowRowNotificationLogEvent(double browsePeopleTime, int personaCount)
		{
			this.browsePeopleTime = browsePeopleTime;
			this.personaCount = personaCount;
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000F0B RID: 3851 RVA: 0x0003A93B File Offset: 0x00038B3B
		public string EventId
		{
			get
			{
				return "PeopleIKnowRowNotification";
			}
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0003A944 File Offset: 0x00038B44
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			return new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("PIK.BPT", this.browsePeopleTime),
				new KeyValuePair<string, object>("PIK.PC", this.personaCount)
			};
		}

		// Token: 0x0400091A RID: 2330
		private readonly double browsePeopleTime;

		// Token: 0x0400091B RID: 2331
		private readonly int personaCount;
	}
}
