using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000164 RID: 356
	internal sealed class ChannelLocation : NotificationLocation
	{
		// Token: 0x06000D35 RID: 3381 RVA: 0x00031B42 File Offset: 0x0002FD42
		public ChannelLocation(string channelId)
		{
			if (string.IsNullOrEmpty(channelId))
			{
				throw new ArgumentException("The channel id cannot be null or empty string.", "channelId");
			}
			this.channelId = channelId;
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x00031B69 File Offset: 0x0002FD69
		public override KeyValuePair<string, object> GetEventData()
		{
			return new KeyValuePair<string, object>("ChannelId", this.channelId);
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x00031B7B File Offset: 0x0002FD7B
		public override int GetHashCode()
		{
			return ChannelLocation.TypeHashCode ^ this.channelId.GetHashCode();
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x00031B90 File Offset: 0x0002FD90
		public override bool Equals(object obj)
		{
			ChannelLocation channelLocation = obj as ChannelLocation;
			return channelLocation != null && this.channelId.Equals(channelLocation.channelId);
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00031BBA File Offset: 0x0002FDBA
		public override string ToString()
		{
			return this.channelId;
		}

		// Token: 0x04000800 RID: 2048
		private const string EventKey = "ChannelId";

		// Token: 0x04000801 RID: 2049
		private static readonly int TypeHashCode = typeof(ChannelLocation).GetHashCode();

		// Token: 0x04000802 RID: 2050
		private readonly string channelId;
	}
}
