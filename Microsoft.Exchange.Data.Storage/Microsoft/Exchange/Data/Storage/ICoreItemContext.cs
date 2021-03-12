using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000074 RID: 116
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ICoreItemContext
	{
		// Token: 0x060007F1 RID: 2033
		void GetContextCharsetDetectionData(StringBuilder stringBuilder, CharsetDetectionDataFlags flags);
	}
}
