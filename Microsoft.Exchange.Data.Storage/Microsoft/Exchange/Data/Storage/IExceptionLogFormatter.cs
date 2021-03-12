using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000F34 RID: 3892
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IExceptionLogFormatter
	{
		// Token: 0x060085D8 RID: 34264
		string FormatException(Exception exception);
	}
}
