using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200005A RID: 90
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMapiFxProxy : IDisposeTrackable, IDisposable
	{
		// Token: 0x0600025C RID: 604
		void ProcessRequest(FxOpcodes opCode, byte[] request);

		// Token: 0x0600025D RID: 605
		byte[] GetObjectData();
	}
}
