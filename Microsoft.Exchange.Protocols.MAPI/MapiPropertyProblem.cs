using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200001A RID: 26
	public struct MapiPropertyProblem
	{
		// Token: 0x040000E1 RID: 225
		public StorePropTag MapiPropTag;

		// Token: 0x040000E2 RID: 226
		public ErrorCodeValue ErrorCode;
	}
}
