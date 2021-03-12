using System;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200080E RID: 2062
	internal class DispatchChannelSinkProvider : IServerChannelSinkProvider
	{
		// Token: 0x060058DE RID: 22750 RVA: 0x001389C6 File Offset: 0x00136BC6
		internal DispatchChannelSinkProvider()
		{
		}

		// Token: 0x060058DF RID: 22751 RVA: 0x001389CE File Offset: 0x00136BCE
		[SecurityCritical]
		public void GetChannelData(IChannelDataStore channelData)
		{
		}

		// Token: 0x060058E0 RID: 22752 RVA: 0x001389D0 File Offset: 0x00136BD0
		[SecurityCritical]
		public IServerChannelSink CreateSink(IChannelReceiver channel)
		{
			return new DispatchChannelSink();
		}

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x060058E1 RID: 22753 RVA: 0x001389D7 File Offset: 0x00136BD7
		// (set) Token: 0x060058E2 RID: 22754 RVA: 0x001389DA File Offset: 0x00136BDA
		public IServerChannelSinkProvider Next
		{
			[SecurityCritical]
			get
			{
				return null;
			}
			[SecurityCritical]
			set
			{
				throw new NotSupportedException();
			}
		}
	}
}
