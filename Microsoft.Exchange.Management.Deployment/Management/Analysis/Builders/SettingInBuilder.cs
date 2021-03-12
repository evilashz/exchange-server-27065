using System;

namespace Microsoft.Exchange.Management.Analysis.Builders
{
	// Token: 0x02000056 RID: 86
	internal sealed class SettingInBuilder<T, TParent>
	{
		// Token: 0x06000223 RID: 547 RVA: 0x0000811E File Offset: 0x0000631E
		public SettingInBuilder(SettingBuildContext<T> context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			this.context = context;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000813B File Offset: 0x0000633B
		public SettingConcurrencyBuilder<T, TParent> In(Analysis analysis)
		{
			if (analysis == null)
			{
				throw new ArgumentNullException("analysis");
			}
			this.context.Analysis = analysis;
			return new SettingConcurrencyBuilder<T, TParent>(this.context);
		}

		// Token: 0x04000143 RID: 323
		private SettingBuildContext<T> context;
	}
}
