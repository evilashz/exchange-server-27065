using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x0200044A RID: 1098
	internal class OwaAppStartLogEvent : ILogEvent
	{
		// Token: 0x060024FE RID: 9470 RVA: 0x00086198 File Offset: 0x00084398
		public OwaAppStartLogEvent(double startDuration)
		{
			this.startDuration = (int)startDuration;
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x060024FF RID: 9471 RVA: 0x000861A8 File Offset: 0x000843A8
		public string EventId
		{
			get
			{
				return "OwaAppStart";
			}
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x000861B0 File Offset: 0x000843B0
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			return new Dictionary<string, object>
			{
				{
					"ST",
					this.startDuration
				}
			};
		}

		// Token: 0x040014F7 RID: 5367
		private readonly int startDuration;
	}
}
