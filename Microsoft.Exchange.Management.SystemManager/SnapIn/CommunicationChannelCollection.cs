using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x02000286 RID: 646
	public class CommunicationChannelCollection
	{
		// Token: 0x1700064C RID: 1612
		public MMCCommunicationChannel this[string key]
		{
			get
			{
				if (!this.channels.ContainsKey(key))
				{
					throw new IndexOutOfRangeException();
				}
				return this.channels[key];
			}
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x00078250 File Offset: 0x00076450
		public void Add(string key, MMCCommunicationChannel channel)
		{
			this.channels.Add(key, channel);
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001B4C RID: 6988 RVA: 0x0007825F File Offset: 0x0007645F
		// (set) Token: 0x06001B4D RID: 6989 RVA: 0x00078267 File Offset: 0x00076467
		public string LocalOnPremiseKey { get; set; }

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001B4E RID: 6990 RVA: 0x00078270 File Offset: 0x00076470
		public bool AllInitiated
		{
			get
			{
				bool result = true;
				foreach (string key in this.channels.Keys)
				{
					if (!this.channels[key].Initiated)
					{
						result = false;
						break;
					}
				}
				return result;
			}
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x000782DC File Offset: 0x000764DC
		public bool ContainsKey(string key)
		{
			return this.channels.ContainsKey(key);
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001B50 RID: 6992 RVA: 0x000782EA File Offset: 0x000764EA
		public Dictionary<string, MMCCommunicationChannel>.KeyCollection Keys
		{
			get
			{
				return this.channels.Keys;
			}
		}

		// Token: 0x04000A22 RID: 2594
		private Dictionary<string, MMCCommunicationChannel> channels = new Dictionary<string, MMCCommunicationChannel>();
	}
}
