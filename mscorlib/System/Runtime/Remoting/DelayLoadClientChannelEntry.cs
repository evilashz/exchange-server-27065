using System;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x02000783 RID: 1923
	internal class DelayLoadClientChannelEntry
	{
		// Token: 0x06005423 RID: 21539 RVA: 0x0012A4EA File Offset: 0x001286EA
		internal DelayLoadClientChannelEntry(RemotingXmlConfigFileData.ChannelEntry entry, bool ensureSecurity)
		{
			this._entry = entry;
			this._channel = null;
			this._bRegistered = false;
			this._ensureSecurity = ensureSecurity;
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06005424 RID: 21540 RVA: 0x0012A50E File Offset: 0x0012870E
		internal IChannelSender Channel
		{
			[SecurityCritical]
			get
			{
				if (this._channel == null && !this._bRegistered)
				{
					this._channel = (IChannelSender)RemotingConfigHandler.CreateChannelFromConfigEntry(this._entry);
					this._entry = null;
				}
				return this._channel;
			}
		}

		// Token: 0x06005425 RID: 21541 RVA: 0x0012A543 File Offset: 0x00128743
		internal void RegisterChannel()
		{
			ChannelServices.RegisterChannel(this._channel, this._ensureSecurity);
			this._bRegistered = true;
			this._channel = null;
		}

		// Token: 0x04002683 RID: 9859
		private RemotingXmlConfigFileData.ChannelEntry _entry;

		// Token: 0x04002684 RID: 9860
		private IChannelSender _channel;

		// Token: 0x04002685 RID: 9861
		private bool _bRegistered;

		// Token: 0x04002686 RID: 9862
		private bool _ensureSecurity;
	}
}
