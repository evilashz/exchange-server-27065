using System;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020007FE RID: 2046
	internal class ChannelServicesData
	{
		// Token: 0x04002813 RID: 10259
		internal long remoteCalls;

		// Token: 0x04002814 RID: 10260
		internal CrossContextChannel xctxmessageSink;

		// Token: 0x04002815 RID: 10261
		internal CrossAppDomainChannel xadmessageSink;

		// Token: 0x04002816 RID: 10262
		internal bool fRegisterWellKnownChannels;
	}
}
