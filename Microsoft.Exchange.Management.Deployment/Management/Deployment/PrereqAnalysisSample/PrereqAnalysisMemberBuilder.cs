using System;
using System.Collections.Generic;
using Microsoft.Exchange.Management.Deployment.Analysis;

namespace Microsoft.Exchange.Management.Deployment.PrereqAnalysisSample
{
	// Token: 0x0200006F RID: 111
	internal sealed class PrereqAnalysisMemberBuilder : AnalysisMemberBuilder
	{
		// Token: 0x06000A66 RID: 2662 RVA: 0x00026354 File Offset: 0x00024554
		public Setting<TResult> Setting<TResult>(Func<TResult> setValue, Optional<Evaluate> evaluate = default(Optional<Evaluate>), Optional<SetupRole> roles = default(Optional<SetupRole>), Optional<SetupMode> modes = default(Optional<SetupMode>))
		{
			return base.Setting<TResult>(setValue, evaluate.DefaultTo(Evaluate.OnDemand), new Feature[]
			{
				new RoleFeature(roles.DefaultTo(SetupRole.All)),
				new ModeFeature(modes.DefaultTo(SetupMode.All))
			});
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x000263A0 File Offset: 0x000245A0
		public Setting<TResult> Setting<TResult>(Func<IEnumerable<TResult>> setValues, Optional<Evaluate> evaluate = default(Optional<Evaluate>), Optional<SetupRole> roles = default(Optional<SetupRole>), Optional<SetupMode> modes = default(Optional<SetupMode>))
		{
			return base.Setting<TResult>(setValues, evaluate.DefaultTo(Evaluate.OnDemand), new Feature[]
			{
				new RoleFeature(roles.DefaultTo(SetupRole.All)),
				new ModeFeature(modes.DefaultTo(SetupMode.All))
			});
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x000263EC File Offset: 0x000245EC
		public Setting<TResult> Setting<TResult>(Func<IEnumerable<Result<TResult>>> setResults, Optional<Evaluate> evaluate = default(Optional<Evaluate>), Optional<SetupRole> roles = default(Optional<SetupRole>), Optional<SetupMode> modes = default(Optional<SetupMode>))
		{
			return base.Setting<TResult>(setResults, evaluate.DefaultTo(Evaluate.OnDemand), new Feature[]
			{
				new RoleFeature(roles.DefaultTo(SetupRole.All)),
				new ModeFeature(modes.DefaultTo(SetupMode.All))
			});
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00026438 File Offset: 0x00024638
		public Setting<TResult> Setting<TResult, TParent>(Func<AnalysisMember<TParent>> forEachResult, Func<Result<TParent>, TResult> setValue, Optional<Evaluate> evaluate = default(Optional<Evaluate>), Optional<SetupRole> roles = default(Optional<SetupRole>), Optional<SetupMode> modes = default(Optional<SetupMode>))
		{
			return base.Setting<TResult, TParent>(forEachResult, setValue, evaluate.DefaultTo(Evaluate.OnDemand), new Feature[]
			{
				new RoleFeature(roles.DefaultTo(SetupRole.All)),
				new ModeFeature(modes.DefaultTo(SetupMode.All))
			});
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00026484 File Offset: 0x00024684
		public Setting<TResult> Setting<TResult, TParent>(Func<AnalysisMember<TParent>> forEachResult, Func<Result<TParent>, IEnumerable<TResult>> setValues, Optional<Evaluate> evaluate = default(Optional<Evaluate>), Optional<SetupRole> roles = default(Optional<SetupRole>), Optional<SetupMode> modes = default(Optional<SetupMode>))
		{
			return base.Setting<TResult, TParent>(forEachResult, setValues, evaluate.DefaultTo(Evaluate.OnDemand), new Feature[]
			{
				new RoleFeature(roles.DefaultTo(SetupRole.All)),
				new ModeFeature(modes.DefaultTo(SetupMode.All))
			});
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x000264D0 File Offset: 0x000246D0
		public Setting<TResult> Setting<TResult, TParent>(Func<AnalysisMember<TParent>> forEachResult, Func<Result<TParent>, IEnumerable<Result<TResult>>> setResults, Optional<Evaluate> evaluate = default(Optional<Evaluate>), Optional<SetupRole> roles = default(Optional<SetupRole>), Optional<SetupMode> modes = default(Optional<SetupMode>))
		{
			return base.Setting<TResult, TParent>(forEachResult, setResults, evaluate.DefaultTo(Evaluate.OnDemand), new Feature[]
			{
				new RoleFeature(roles.DefaultTo(SetupRole.All)),
				new ModeFeature(modes.DefaultTo(SetupMode.All))
			});
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00026538 File Offset: 0x00024738
		public Rule Rule(Func<RuleResult, string> message, Func<bool> condition, Optional<Evaluate> evaluate = default(Optional<Evaluate>), Optional<SetupRole> roles = default(Optional<SetupRole>), Optional<SetupMode> modes = default(Optional<SetupMode>), Optional<Severity> severity = default(Optional<Severity>))
		{
			return base.Rule(condition, evaluate.DefaultTo(Evaluate.OnDemand), severity.DefaultTo(Severity.Error), new Feature[]
			{
				new RoleFeature(roles.DefaultTo(SetupRole.All)),
				new ModeFeature(modes.DefaultTo(SetupMode.All)),
				new MessageFeature((Result x) => message((RuleResult)x))
			});
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x000265C8 File Offset: 0x000247C8
		public Rule Rule<TParent>(Func<AnalysisMember<TParent>> forEachResult, Func<RuleResult, string> message, Func<Result<TParent>, bool> condition, Optional<Evaluate> evaluate = default(Optional<Evaluate>), Optional<SetupRole> roles = default(Optional<SetupRole>), Optional<SetupMode> modes = default(Optional<SetupMode>), Optional<Severity> severity = default(Optional<Severity>))
		{
			return base.Rule<TParent>(forEachResult, condition, evaluate.DefaultTo(Evaluate.OnDemand), severity.DefaultTo(Severity.Error), new Feature[]
			{
				new RoleFeature(roles.DefaultTo(SetupRole.All)),
				new ModeFeature(modes.DefaultTo(SetupMode.All)),
				new MessageFeature((Result x) => message((RuleResult)x))
			});
		}
	}
}
