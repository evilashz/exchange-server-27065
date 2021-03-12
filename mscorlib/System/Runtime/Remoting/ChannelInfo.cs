using System;
using System.Runtime.Remoting.Channels;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x0200078D RID: 1933
	[Serializable]
	internal sealed class ChannelInfo : IChannelInfo
	{
		// Token: 0x0600547C RID: 21628 RVA: 0x0012B566 File Offset: 0x00129766
		[SecurityCritical]
		internal ChannelInfo()
		{
			this.ChannelData = ChannelServices.CurrentChannelData;
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x0600547D RID: 21629 RVA: 0x0012B579 File Offset: 0x00129779
		// (set) Token: 0x0600547E RID: 21630 RVA: 0x0012B581 File Offset: 0x00129781
		public object[] ChannelData
		{
			[SecurityCritical]
			get
			{
				return this.channelData;
			}
			[SecurityCritical]
			set
			{
				this.channelData = value;
			}
		}

		// Token: 0x040026AD RID: 9901
		private object[] channelData;
	}
}
