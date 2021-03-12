using System;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020007FD RID: 2045
	internal class RegisteredChannelList
	{
		// Token: 0x06005873 RID: 22643 RVA: 0x00137411 File Offset: 0x00135611
		internal RegisteredChannelList()
		{
			this._channels = new RegisteredChannel[0];
		}

		// Token: 0x06005874 RID: 22644 RVA: 0x00137425 File Offset: 0x00135625
		internal RegisteredChannelList(RegisteredChannel[] channels)
		{
			this._channels = channels;
		}

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x06005875 RID: 22645 RVA: 0x00137434 File Offset: 0x00135634
		internal RegisteredChannel[] RegisteredChannels
		{
			get
			{
				return this._channels;
			}
		}

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x06005876 RID: 22646 RVA: 0x0013743C File Offset: 0x0013563C
		internal int Count
		{
			get
			{
				if (this._channels == null)
				{
					return 0;
				}
				return this._channels.Length;
			}
		}

		// Token: 0x06005877 RID: 22647 RVA: 0x00137450 File Offset: 0x00135650
		internal IChannel GetChannel(int index)
		{
			return this._channels[index].Channel;
		}

		// Token: 0x06005878 RID: 22648 RVA: 0x0013745F File Offset: 0x0013565F
		internal bool IsSender(int index)
		{
			return this._channels[index].IsSender();
		}

		// Token: 0x06005879 RID: 22649 RVA: 0x0013746E File Offset: 0x0013566E
		internal bool IsReceiver(int index)
		{
			return this._channels[index].IsReceiver();
		}

		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x0600587A RID: 22650 RVA: 0x00137480 File Offset: 0x00135680
		internal int ReceiverCount
		{
			get
			{
				if (this._channels == null)
				{
					return 0;
				}
				int num = 0;
				for (int i = 0; i < this._channels.Length; i++)
				{
					if (this.IsReceiver(i))
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x0600587B RID: 22651 RVA: 0x001374BC File Offset: 0x001356BC
		internal int FindChannelIndex(IChannel channel)
		{
			for (int i = 0; i < this._channels.Length; i++)
			{
				if (channel == this.GetChannel(i))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600587C RID: 22652 RVA: 0x001374EC File Offset: 0x001356EC
		[SecurityCritical]
		internal int FindChannelIndex(string name)
		{
			for (int i = 0; i < this._channels.Length; i++)
			{
				if (string.Compare(name, this.GetChannel(i).ChannelName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x04002812 RID: 10258
		private RegisteredChannel[] _channels;
	}
}
