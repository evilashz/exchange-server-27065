using System;
using System.IO;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000F6 RID: 246
	internal interface IRelayable
	{
		// Token: 0x060006F7 RID: 1783
		void WriteTo(Stream stream);
	}
}
