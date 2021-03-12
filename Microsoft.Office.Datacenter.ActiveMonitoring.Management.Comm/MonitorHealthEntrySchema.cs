using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring.Management.Common
{
	// Token: 0x02000007 RID: 7
	internal class MonitorHealthEntrySchema : SimpleProviderObjectSchema
	{
		// Token: 0x0400001D RID: 29
		public static readonly SimpleProviderPropertyDefinition Server = new SimpleProviderPropertyDefinition("Server", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400001E RID: 30
		public static readonly SimpleProviderPropertyDefinition CurrentHealthSetState = new SimpleProviderPropertyDefinition("CurrentHealthSetState", ExchangeObjectVersion.Exchange2010, typeof(MonitorServerComponentState), PropertyDefinitionFlags.None, MonitorServerComponentState.Unknown, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400001F RID: 31
		public static readonly SimpleProviderPropertyDefinition Name = new SimpleProviderPropertyDefinition("Name", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000020 RID: 32
		public static readonly SimpleProviderPropertyDefinition TargetResource = new SimpleProviderPropertyDefinition("TargetResource", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000021 RID: 33
		public static readonly SimpleProviderPropertyDefinition HealthSetName = new SimpleProviderPropertyDefinition("HealthSetName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000022 RID: 34
		public static readonly SimpleProviderPropertyDefinition HealthGroupName = new SimpleProviderPropertyDefinition("HealthGroupName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000023 RID: 35
		public static readonly SimpleProviderPropertyDefinition AlertValue = new SimpleProviderPropertyDefinition("AlertValue", ExchangeObjectVersion.Exchange2010, typeof(MonitorAlertState), PropertyDefinitionFlags.None, MonitorAlertState.Unknown, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000024 RID: 36
		public static readonly SimpleProviderPropertyDefinition FirstAlertObservedTime = new SimpleProviderPropertyDefinition("FirstAlertObservedTime", ExchangeObjectVersion.Exchange2010, typeof(DateTime), PropertyDefinitionFlags.PersistDefaultValue, DateTime.MinValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000025 RID: 37
		public static readonly SimpleProviderPropertyDefinition Description = new SimpleProviderPropertyDefinition("Description", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000026 RID: 38
		public static readonly SimpleProviderPropertyDefinition IsHaImpacting = new SimpleProviderPropertyDefinition("IsHaImpacting", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000027 RID: 39
		public static readonly SimpleProviderPropertyDefinition RecurranceInterval = new SimpleProviderPropertyDefinition("RecurranceInterval", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000028 RID: 40
		public static readonly SimpleProviderPropertyDefinition DefinitionCreatedTime = new SimpleProviderPropertyDefinition("DefinitionCreatedTime", ExchangeObjectVersion.Exchange2010, typeof(DateTime), PropertyDefinitionFlags.PersistDefaultValue, DateTime.MinValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000029 RID: 41
		public static readonly SimpleProviderPropertyDefinition HealthSetDescription = new SimpleProviderPropertyDefinition("HealthSetDescription", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400002A RID: 42
		public static readonly SimpleProviderPropertyDefinition ServerComponentName = new SimpleProviderPropertyDefinition("ServerComponentName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400002B RID: 43
		public static readonly SimpleProviderPropertyDefinition LastTransitionTime = new SimpleProviderPropertyDefinition("LastTransitionTime", ExchangeObjectVersion.Exchange2010, typeof(DateTime), PropertyDefinitionFlags.PersistDefaultValue, DateTime.MinValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400002C RID: 44
		public static readonly SimpleProviderPropertyDefinition LastExecutionTime = new SimpleProviderPropertyDefinition("LastExecutionTime", ExchangeObjectVersion.Exchange2010, typeof(DateTime), PropertyDefinitionFlags.PersistDefaultValue, DateTime.MinValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400002D RID: 45
		public static readonly SimpleProviderPropertyDefinition LastExecutionResult = new SimpleProviderPropertyDefinition("LastExecutionResult", ExchangeObjectVersion.Exchange2010, typeof(ResultType), PropertyDefinitionFlags.None, ResultType.Abandoned, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400002E RID: 46
		public static readonly SimpleProviderPropertyDefinition ResultId = new SimpleProviderPropertyDefinition("ResultId", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400002F RID: 47
		public static readonly SimpleProviderPropertyDefinition WorkItemId = new SimpleProviderPropertyDefinition("WorkItemId", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000030 RID: 48
		public static readonly SimpleProviderPropertyDefinition IsStale = new SimpleProviderPropertyDefinition("IsStale", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000031 RID: 49
		public static readonly SimpleProviderPropertyDefinition Error = new SimpleProviderPropertyDefinition("Error", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000032 RID: 50
		public static readonly SimpleProviderPropertyDefinition Exception = new SimpleProviderPropertyDefinition("Exception", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000033 RID: 51
		public static readonly SimpleProviderPropertyDefinition IsNotified = new SimpleProviderPropertyDefinition("IsNotified", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000034 RID: 52
		public static readonly SimpleProviderPropertyDefinition LastFailedProbeId = new SimpleProviderPropertyDefinition("LastFailedProbeId", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000035 RID: 53
		public static readonly SimpleProviderPropertyDefinition LastFailedProbeResultId = new SimpleProviderPropertyDefinition("LastFailedProbeResultId", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000036 RID: 54
		public static readonly SimpleProviderPropertyDefinition ServicePriority = new SimpleProviderPropertyDefinition("ServicePriority", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
