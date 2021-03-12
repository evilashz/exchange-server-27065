using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000169 RID: 361
	internal interface IOrarGeneratorComponent : ITransportComponent
	{
		// Token: 0x06000FBC RID: 4028
		void GenerateOrarMessage(IReadOnlyMailItem mailItem);

		// Token: 0x06000FBD RID: 4029
		void GenerateOrarMessage(IReadOnlyMailItem mailItem, bool resetTime);
	}
}
