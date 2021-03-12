using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.Analysis.Builders
{
	// Token: 0x02000053 RID: 83
	internal sealed class SettingBuildContext<T> : BuildContext<T>
	{
		// Token: 0x06000218 RID: 536 RVA: 0x00007F2F File Offset: 0x0000612F
		public SettingBuildContext(Func<SettingBuildContext<T>, AnalysisMember<T>> constructor)
		{
			if (constructor == null)
			{
				throw new ArgumentNullException("constructor");
			}
			this.constructor = constructor;
			this.SetFunction = null;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00007F53 File Offset: 0x00006153
		// (set) Token: 0x0600021A RID: 538 RVA: 0x00007F5B File Offset: 0x0000615B
		public Func<Result, IEnumerable<Result<T>>> SetFunction { get; set; }

		// Token: 0x0600021B RID: 539 RVA: 0x00007F64 File Offset: 0x00006164
		public override AnalysisMember<T> Construct()
		{
			return this.constructor(this);
		}

		// Token: 0x0400013F RID: 319
		private Func<SettingBuildContext<T>, AnalysisMember<T>> constructor;
	}
}
