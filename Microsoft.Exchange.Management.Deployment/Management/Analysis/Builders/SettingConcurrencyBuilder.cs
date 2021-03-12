using System;

namespace Microsoft.Exchange.Management.Analysis.Builders
{
	// Token: 0x02000055 RID: 85
	internal sealed class SettingConcurrencyBuilder<T, TParent>
	{
		// Token: 0x06000220 RID: 544 RVA: 0x000080CF File Offset: 0x000062CF
		public SettingConcurrencyBuilder(SettingBuildContext<T> context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			this.context = context;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000080EC File Offset: 0x000062EC
		public SettingBuilder<T, TParent> AsSync()
		{
			this.context.RunAs = ConcurrencyType.Synchronous;
			return new SettingBuilder<T, TParent>(this.context);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00008105 File Offset: 0x00006305
		public SettingBuilder<T, TParent> AsAsync()
		{
			this.context.RunAs = ConcurrencyType.ASynchronous;
			return new SettingBuilder<T, TParent>(this.context);
		}

		// Token: 0x04000142 RID: 322
		private SettingBuildContext<T> context;
	}
}
