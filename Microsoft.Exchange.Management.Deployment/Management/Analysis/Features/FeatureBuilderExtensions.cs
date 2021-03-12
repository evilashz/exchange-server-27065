using System;
using Microsoft.Exchange.Management.Analysis.Builders;

namespace Microsoft.Exchange.Management.Analysis.Features
{
	// Token: 0x02000060 RID: 96
	internal static class FeatureBuilderExtensions
	{
		// Token: 0x06000247 RID: 583 RVA: 0x0000839C File Offset: 0x0000659C
		public static T Mode<T>(this T builder, SetupMode modes) where T : IFeatureBuilder
		{
			builder.AddFeature(new AppliesToModeFeature(modes));
			return builder;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000083B2 File Offset: 0x000065B2
		public static T Role<T>(this T builder, SetupRole roles) where T : IFeatureBuilder
		{
			builder.AddFeature(new AppliesToRoleFeature(roles));
			return builder;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000083C8 File Offset: 0x000065C8
		public static T Error<T>(this T builder) where T : IRuleFeatureBuilder
		{
			builder.AddFeature(new RuleTypeFeature(RuleType.Error));
			return builder;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000083DE File Offset: 0x000065DE
		public static T Warning<T>(this T builder) where T : IRuleFeatureBuilder
		{
			builder.AddFeature(new RuleTypeFeature(RuleType.Warning));
			return builder;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000083F4 File Offset: 0x000065F4
		public static T Info<T>(this T builder) where T : IRuleFeatureBuilder
		{
			builder.AddFeature(new RuleTypeFeature(RuleType.Info));
			return builder;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000840A File Offset: 0x0000660A
		public static T Message<T>(this T builder, Func<Result, string> textFunction) where T : IRuleFeatureBuilder
		{
			builder.AddFeature(new MessageFeature(textFunction));
			return builder;
		}
	}
}
