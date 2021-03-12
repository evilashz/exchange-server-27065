using System;

namespace Microsoft.Exchange.Transport.MessageThrottling
{
	// Token: 0x02000131 RID: 305
	internal sealed class MessageThrottlingManagerConfig : IMessageThrottlingManagerConfig
	{
		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x00030FFA File Offset: 0x0002F1FA
		public bool Enabled
		{
			get
			{
				return Components.TransportAppConfig.MessageThrottlingConfig.Enabled;
			}
		}
	}
}
