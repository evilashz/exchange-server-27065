using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.ApplicationLogic.Diagnostics
{
	// Token: 0x020000D0 RID: 208
	internal class ConditionalHandlerSchema : ObjectSchema
	{
		// Token: 0x060008E5 RID: 2277 RVA: 0x000231E9 File Offset: 0x000213E9
		internal static SimpleProviderPropertyDefinition BuildStringPropDef(string name)
		{
			return ConditionalHandlerSchema.BuildRefTypePropDef<string>(name);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x000231F1 File Offset: 0x000213F1
		internal static SimpleProviderPropertyDefinition BuildRefTypePropDef<T>(string name)
		{
			return new SimpleProviderPropertyDefinition(name, ExchangeObjectVersion.Current, typeof(T), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00023214 File Offset: 0x00021414
		internal static SimpleProviderPropertyDefinition BuildValueTypePropDef<T>(string name, T defaultValue)
		{
			return new SimpleProviderPropertyDefinition(name, ExchangeObjectVersion.Current, typeof(T), PropertyDefinitionFlags.PersistDefaultValue, defaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0002323D File Offset: 0x0002143D
		internal static SimpleProviderPropertyDefinition BuildUnlimitedPropDef(string name)
		{
			return new SimpleProviderPropertyDefinition(name, ExchangeObjectVersion.Current, typeof(Unlimited<uint>), PropertyDefinitionFlags.None, Unlimited<uint>.UnlimitedValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0002326C File Offset: 0x0002146C
		internal static SimpleProviderPropertyDefinition BuildValueTypePropDef<T>(string name)
		{
			return ConditionalHandlerSchema.BuildValueTypePropDef<T>(name, default(T));
		}

		// Token: 0x0400040A RID: 1034
		public static readonly SimpleProviderPropertyDefinition SmtpAddress = ConditionalHandlerSchema.BuildStringPropDef("SmtpAddress");

		// Token: 0x0400040B RID: 1035
		public static readonly SimpleProviderPropertyDefinition DisplayName = ConditionalHandlerSchema.BuildStringPropDef("DisplayName");

		// Token: 0x0400040C RID: 1036
		public static readonly SimpleProviderPropertyDefinition TenantName = ConditionalHandlerSchema.BuildStringPropDef("TenantName");

		// Token: 0x0400040D RID: 1037
		public static readonly SimpleProviderPropertyDefinition WindowsLiveId = ConditionalHandlerSchema.BuildStringPropDef("WindowsLiveId");

		// Token: 0x0400040E RID: 1038
		public static readonly SimpleProviderPropertyDefinition MailboxServer = ConditionalHandlerSchema.BuildStringPropDef("MailboxServer");

		// Token: 0x0400040F RID: 1039
		public static readonly SimpleProviderPropertyDefinition MailboxDatabase = ConditionalHandlerSchema.BuildStringPropDef("MailboxDatabase");

		// Token: 0x04000410 RID: 1040
		public static readonly SimpleProviderPropertyDefinition MailboxServerVersion = ConditionalHandlerSchema.BuildStringPropDef("MailboxServerVersion");

		// Token: 0x04000411 RID: 1041
		public static readonly SimpleProviderPropertyDefinition IsMonitoringUser = ConditionalHandlerSchema.BuildValueTypePropDef<bool>("IsMonitoringUser");

		// Token: 0x04000412 RID: 1042
		public static readonly SimpleProviderPropertyDefinition IsOverBudgetAtStart = ConditionalHandlerSchema.BuildValueTypePropDef<bool>("IsOverBudgetAtStart");

		// Token: 0x04000413 RID: 1043
		public static readonly SimpleProviderPropertyDefinition IsOverBudgetAtEnd = ConditionalHandlerSchema.BuildValueTypePropDef<bool>("IsOverBudgetAtEnd");

		// Token: 0x04000414 RID: 1044
		public static readonly SimpleProviderPropertyDefinition BudgetBalanceStart = ConditionalHandlerSchema.BuildValueTypePropDef<float>("BudgetBalanceStart");

		// Token: 0x04000415 RID: 1045
		public static readonly SimpleProviderPropertyDefinition BudgetBalanceEnd = ConditionalHandlerSchema.BuildValueTypePropDef<float>("BudgetBalanceEnd");

		// Token: 0x04000416 RID: 1046
		public static readonly SimpleProviderPropertyDefinition BudgetDelay = ConditionalHandlerSchema.BuildValueTypePropDef<float>("BudgetDelay");

		// Token: 0x04000417 RID: 1047
		public static readonly SimpleProviderPropertyDefinition BudgetUsed = ConditionalHandlerSchema.BuildValueTypePropDef<float>("BudgetUsed");

		// Token: 0x04000418 RID: 1048
		public static readonly SimpleProviderPropertyDefinition BudgetLockedOut = ConditionalHandlerSchema.BuildValueTypePropDef<bool>("BudgetLockedOut");

		// Token: 0x04000419 RID: 1049
		public static readonly SimpleProviderPropertyDefinition BudgetLockedUntil = ConditionalHandlerSchema.BuildValueTypePropDef<ExDateTime>("BudgetLockedUntil");

		// Token: 0x0400041A RID: 1050
		public static readonly SimpleProviderPropertyDefinition ActivityId = ConditionalHandlerSchema.BuildStringPropDef("ActivityId");

		// Token: 0x0400041B RID: 1051
		public static readonly SimpleProviderPropertyDefinition Cmd = ConditionalHandlerSchema.BuildStringPropDef("Cmd");

		// Token: 0x0400041C RID: 1052
		public static readonly SimpleProviderPropertyDefinition ElapsedTime = ConditionalHandlerSchema.BuildValueTypePropDef<TimeSpan>("ElapsedTime");

		// Token: 0x0400041D RID: 1053
		public static readonly SimpleProviderPropertyDefinition Exception = ConditionalHandlerSchema.BuildStringPropDef("Exception");

		// Token: 0x0400041E RID: 1054
		public static readonly SimpleProviderPropertyDefinition PreWlmElapsed = ConditionalHandlerSchema.BuildValueTypePropDef<TimeSpan>("PreWlmDelay");

		// Token: 0x0400041F RID: 1055
		public static readonly SimpleProviderPropertyDefinition WlmQueueElapsed = ConditionalHandlerSchema.BuildValueTypePropDef<TimeSpan>("WlmQueueElapsed");

		// Token: 0x04000420 RID: 1056
		public static readonly SimpleProviderPropertyDefinition PostWlmElapsed = ConditionalHandlerSchema.BuildValueTypePropDef<TimeSpan>("PostWlmDelay");

		// Token: 0x04000421 RID: 1057
		public static readonly SimpleProviderPropertyDefinition CommandElapsed = ConditionalHandlerSchema.BuildValueTypePropDef<TimeSpan>("CommandElapsed");

		// Token: 0x04000422 RID: 1058
		public static readonly SimpleProviderPropertyDefinition ThrottlingPolicyName = ConditionalHandlerSchema.BuildStringPropDef("ThrottlingPolicyName");

		// Token: 0x04000423 RID: 1059
		public static readonly SimpleProviderPropertyDefinition MaxConcurrency = ConditionalHandlerSchema.BuildUnlimitedPropDef("MaxConcurrency");

		// Token: 0x04000424 RID: 1060
		public static readonly SimpleProviderPropertyDefinition MaxBurst = ConditionalHandlerSchema.BuildUnlimitedPropDef("MaxBurst");

		// Token: 0x04000425 RID: 1061
		public static readonly SimpleProviderPropertyDefinition RechargeRate = ConditionalHandlerSchema.BuildUnlimitedPropDef("RechargeRate");

		// Token: 0x04000426 RID: 1062
		public static readonly SimpleProviderPropertyDefinition CutoffBalance = ConditionalHandlerSchema.BuildUnlimitedPropDef("CutoffBalance");

		// Token: 0x04000427 RID: 1063
		public static readonly SimpleProviderPropertyDefinition ThrottlingPolicyScope = ConditionalHandlerSchema.BuildStringPropDef("ThrottlingPolicyScope");

		// Token: 0x04000428 RID: 1064
		public static readonly SimpleProviderPropertyDefinition ConcurrencyStart = ConditionalHandlerSchema.BuildValueTypePropDef<int>("ConcurrencyStart");

		// Token: 0x04000429 RID: 1065
		public static readonly SimpleProviderPropertyDefinition ConcurrencyEnd = ConditionalHandlerSchema.BuildValueTypePropDef<int>("ConcurrencyEnd");
	}
}
