using System;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000067 RID: 103
	internal sealed class IdentityExceptionTranslator : IExceptionTranslator
	{
		// Token: 0x0600022B RID: 555 RVA: 0x00009D24 File Offset: 0x00007F24
		public bool TryTranslate(Exception exception, out Exception translatedException)
		{
			translatedException = null;
			return false;
		}
	}
}
