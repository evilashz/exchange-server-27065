using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000117 RID: 279
	internal class DirectoryExceptionTranslator : IExceptionTranslator
	{
		// Token: 0x06000917 RID: 2327 RVA: 0x0003BC8B File Offset: 0x00039E8B
		public bool TryTranslate(Exception exception, out Exception translatedException)
		{
			translatedException = null;
			if (exception is DataValidationException || exception is DataSourceOperationException || exception is DataSourceTransientException)
			{
				translatedException = new TokenMungingException("Directory exception occurred during token munging", exception);
				return true;
			}
			return false;
		}
	}
}
