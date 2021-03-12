using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005A1 RID: 1441
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct BodyConversionCallbacks
	{
		// Token: 0x04001F87 RID: 8071
		public HtmlCallbackBase HtmlCallback;

		// Token: 0x04001F88 RID: 8072
		public RtfCallbackBase RtfCallback;
	}
}
