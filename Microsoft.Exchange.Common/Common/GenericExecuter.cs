using System;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000065 RID: 101
	internal class GenericExecuter : IExecuter
	{
		// Token: 0x06000228 RID: 552 RVA: 0x00009CD7 File Offset: 0x00007ED7
		public GenericExecuter(IExceptionTranslator exceptionTranslator)
		{
			this.exceptionTranslator = exceptionTranslator;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00009CE8 File Offset: 0x00007EE8
		public virtual void Execute(Action wrappedCall)
		{
			try
			{
				wrappedCall();
			}
			catch (Exception exception)
			{
				Exception ex;
				if (this.exceptionTranslator.TryTranslate(exception, out ex))
				{
					throw ex;
				}
				throw;
			}
		}

		// Token: 0x040001AD RID: 429
		private readonly IExceptionTranslator exceptionTranslator;
	}
}
