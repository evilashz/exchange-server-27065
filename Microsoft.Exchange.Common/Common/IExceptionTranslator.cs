using System;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000066 RID: 102
	internal interface IExceptionTranslator
	{
		// Token: 0x0600022A RID: 554
		bool TryTranslate(Exception exception, out Exception translatedException);
	}
}
