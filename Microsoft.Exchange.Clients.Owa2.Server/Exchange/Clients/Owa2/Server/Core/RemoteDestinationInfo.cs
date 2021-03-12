using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001CC RID: 460
	internal class RemoteDestinationInfo : IDestinationInfo
	{
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x0003E6EE File Offset: 0x0003C8EE
		// (set) Token: 0x0600104A RID: 4170 RVA: 0x0003E6F6 File Offset: 0x0003C8F6
		public Uri Destination { get; private set; }

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x0600104B RID: 4171 RVA: 0x0003E6FF File Offset: 0x0003C8FF
		public IEnumerable<string> ChannelIds
		{
			get
			{
				return this.channelIds;
			}
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0003E707 File Offset: 0x0003C907
		public RemoteDestinationInfo(Uri destination, string channelId)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			this.Destination = destination;
			this.AddChannel(channelId);
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0003E73C File Offset: 0x0003C93C
		public void AddChannel(string channelId)
		{
			if (string.IsNullOrWhiteSpace(channelId))
			{
				throw new ArgumentException("channelId cannot be null or whitespace.");
			}
			this.channelIds.Add(channelId);
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0003E75D File Offset: 0x0003C95D
		public override string ToString()
		{
			return string.Format("Uri - {0}, Channels - {1}", this.Destination, string.Join(",", this.ChannelIds));
		}

		// Token: 0x040009B3 RID: 2483
		private List<string> channelIds = new List<string>();
	}
}
