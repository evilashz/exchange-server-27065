using System;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000064 RID: 100
	internal abstract class ExecuterProxy : IExecuter
	{
		// Token: 0x06000226 RID: 550 RVA: 0x00009CBA File Offset: 0x00007EBA
		protected ExecuterProxy(IExecuter delegateExecuter)
		{
			this.delegateExecuter = delegateExecuter;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00009CC9 File Offset: 0x00007EC9
		public virtual void Execute(Action wrappedCall)
		{
			this.delegateExecuter.Execute(wrappedCall);
		}

		// Token: 0x040001AC RID: 428
		private readonly IExecuter delegateExecuter;
	}
}
