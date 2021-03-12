using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200000A RID: 10
	internal interface ITransportComponent
	{
		// Token: 0x06000035 RID: 53
		void Load();

		// Token: 0x06000036 RID: 54
		void Unload();

		// Token: 0x06000037 RID: 55
		string OnUnhandledException(Exception e);
	}
}
