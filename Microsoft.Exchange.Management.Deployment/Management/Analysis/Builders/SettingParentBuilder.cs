using System;

namespace Microsoft.Exchange.Management.Analysis.Builders
{
	// Token: 0x02000057 RID: 87
	internal sealed class SettingParentBuilder<T>
	{
		// Token: 0x06000225 RID: 549 RVA: 0x00008162 File Offset: 0x00006362
		public SettingParentBuilder(SettingBuildContext<T> context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			this.context = context;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000817F File Offset: 0x0000637F
		public SettingInBuilder<T, TParent> WithParent<TParent>(Func<AnalysisMember<TParent>> parent)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			this.context.Parent = parent;
			return new SettingInBuilder<T, TParent>(this.context);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000081A6 File Offset: 0x000063A6
		public SettingInBuilder<T, object> AsRootSetting()
		{
			this.context.Parent = null;
			return new SettingInBuilder<T, object>(this.context);
		}

		// Token: 0x04000144 RID: 324
		private SettingBuildContext<T> context;
	}
}
