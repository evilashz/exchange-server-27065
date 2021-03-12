using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001CB RID: 459
	public class RemoteChannelInfo : IEquatable<RemoteChannelInfo>
	{
		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x0600103F RID: 4159 RVA: 0x0003E64B File Offset: 0x0003C84B
		// (set) Token: 0x06001040 RID: 4160 RVA: 0x0003E653 File Offset: 0x0003C853
		public string ChannelId { get; private set; }

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001041 RID: 4161 RVA: 0x0003E65C File Offset: 0x0003C85C
		// (set) Token: 0x06001042 RID: 4162 RVA: 0x0003E664 File Offset: 0x0003C864
		public string User { get; private set; }

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x0003E66D File Offset: 0x0003C86D
		// (set) Token: 0x06001044 RID: 4164 RVA: 0x0003E675 File Offset: 0x0003C875
		internal string EndpointTestOverride { get; set; }

		// Token: 0x06001045 RID: 4165 RVA: 0x0003E67E File Offset: 0x0003C87E
		public RemoteChannelInfo(string channelId, string user)
		{
			this.ChannelId = channelId;
			this.User = user;
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0003E694 File Offset: 0x0003C894
		public override bool Equals(object obj)
		{
			RemoteChannelInfo remoteChannelInfo = obj as RemoteChannelInfo;
			return remoteChannelInfo != null && this.Equals(remoteChannelInfo);
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0003E6B4 File Offset: 0x0003C8B4
		public bool Equals(RemoteChannelInfo other)
		{
			return other != null && string.Equals(this.ChannelId, other.ChannelId, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0003E6CD File Offset: 0x0003C8CD
		public override int GetHashCode()
		{
			if (this.ChannelId == null)
			{
				return base.GetHashCode();
			}
			return this.ChannelId.ToUpperInvariant().GetHashCode();
		}
	}
}
