using System;
using System.Collections.Generic;
using Microsoft.Exchange.Management.Analysis.Builders;
using Microsoft.Exchange.Management.Analysis.Features;

namespace Microsoft.Exchange.Management.Analysis
{
	// Token: 0x02000049 RID: 73
	internal sealed class Setting<T> : AnalysisMember<T>
	{
		// Token: 0x060001FA RID: 506 RVA: 0x00007C04 File Offset: 0x00005E04
		private Setting(Func<AnalysisMember> parent, ConcurrencyType runAs, Analysis analysis, IEnumerable<Feature> features, Func<Result, IEnumerable<Result<T>>> setFunction) : base(parent, runAs, analysis, features, setFunction)
		{
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00007C38 File Offset: 0x00005E38
		public static SettingParentBuilder<T> Build()
		{
			SettingBuildContext<T> context = new SettingBuildContext<T>((SettingBuildContext<T> x) => new Setting<T>(x.Parent, x.RunAs, x.Analysis, x.Features, x.SetFunction));
			return new SettingParentBuilder<T>(context);
		}
	}
}
