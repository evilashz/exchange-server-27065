using System;
using System.Data;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend
{
	// Token: 0x02000041 RID: 65
	internal static class ScheduleItemProperties
	{
		// Token: 0x04000182 RID: 386
		internal static readonly BackgroundJobBackendPropertyDefinition ScheduleIdProperty = new BackgroundJobBackendPropertyDefinition("ScheduleId", typeof(Guid), PropertyDefinitionFlags.Mandatory, Guid.Empty);

		// Token: 0x04000183 RID: 387
		internal static readonly BackgroundJobBackendPropertyDefinition BackgroundJobIdProperty = new BackgroundJobBackendPropertyDefinition("BackgroundJobId", typeof(Guid), PropertyDefinitionFlags.Mandatory, Guid.Empty);

		// Token: 0x04000184 RID: 388
		internal static readonly BackgroundJobBackendPropertyDefinition SchedulingTypeProperty = new BackgroundJobBackendPropertyDefinition("SchedulingType", typeof(byte), PropertyDefinitionFlags.Mandatory, 0);

		// Token: 0x04000185 RID: 389
		internal static readonly BackgroundJobBackendPropertyDefinition StartTimeProperty = new BackgroundJobBackendPropertyDefinition("StartTime", typeof(DateTime?), PropertyDefinitionFlags.Mandatory, null);

		// Token: 0x04000186 RID: 390
		internal static readonly BackgroundJobBackendPropertyDefinition SchedulingIntervalProperty = new BackgroundJobBackendPropertyDefinition("SchedulingInterval", typeof(int?), PropertyDefinitionFlags.Mandatory, null);

		// Token: 0x04000187 RID: 391
		internal static readonly BackgroundJobBackendPropertyDefinition ScheduleDaysSetProperty = new BackgroundJobBackendPropertyDefinition("ScheduleDaysSet", typeof(byte?), PropertyDefinitionFlags.Mandatory, null);

		// Token: 0x04000188 RID: 392
		internal static readonly BackgroundJobBackendPropertyDefinition DCSelectionSetProperty = new BackgroundJobBackendPropertyDefinition("DCSelectionSet", typeof(long), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, 0L);

		// Token: 0x04000189 RID: 393
		internal static readonly BackgroundJobBackendPropertyDefinition RegionSelectionSetProperty = new BackgroundJobBackendPropertyDefinition("RegionSelectionSet", typeof(int), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, 0);

		// Token: 0x0400018A RID: 394
		internal static readonly BackgroundJobBackendPropertyDefinition TargetMachineNameProperty = new BackgroundJobBackendPropertyDefinition("TargetMachineName", typeof(string), PropertyDefinitionFlags.Mandatory, null);

		// Token: 0x0400018B RID: 395
		internal static readonly BackgroundJobBackendPropertyDefinition InstancesToRunProperty = new BackgroundJobBackendPropertyDefinition("InstancesToRun", typeof(byte), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, 0);

		// Token: 0x0400018C RID: 396
		internal static readonly BackgroundJobBackendPropertyDefinition RoleIdProperty = new BackgroundJobBackendPropertyDefinition("RoleId", typeof(Guid), PropertyDefinitionFlags.Mandatory, Guid.Empty);

		// Token: 0x0400018D RID: 397
		internal static readonly BackgroundJobBackendPropertyDefinition SingleInstancePerMachineProperty = new BackgroundJobBackendPropertyDefinition("SingleInstancePerMachine", typeof(bool), PropertyDefinitionFlags.Mandatory, false);

		// Token: 0x0400018E RID: 398
		internal static readonly BackgroundJobBackendPropertyDefinition SchedulingStrategyProperty = new BackgroundJobBackendPropertyDefinition("SchedulingStrategy", typeof(byte), PropertyDefinitionFlags.Mandatory, 0);

		// Token: 0x0400018F RID: 399
		internal static readonly BackgroundJobBackendPropertyDefinition TimeoutProperty = new BackgroundJobBackendPropertyDefinition("Timeout", typeof(int), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, 0);

		// Token: 0x04000190 RID: 400
		internal static readonly BackgroundJobBackendPropertyDefinition MaxLocalRetryCountProperty = new BackgroundJobBackendPropertyDefinition("MaxLocalRetryCount", typeof(byte), PropertyDefinitionFlags.Mandatory, 0);

		// Token: 0x04000191 RID: 401
		internal static readonly BackgroundJobBackendPropertyDefinition MaxFailoverCountProperty = new BackgroundJobBackendPropertyDefinition("MaxFailoverCount", typeof(short), PropertyDefinitionFlags.Mandatory, 0);

		// Token: 0x04000192 RID: 402
		internal static readonly BackgroundJobBackendPropertyDefinition LastScheduledTimeProperty = new BackgroundJobBackendPropertyDefinition("LastScheduledTime", typeof(DateTime?), PropertyDefinitionFlags.Mandatory, null);

		// Token: 0x04000193 RID: 403
		internal static readonly BackgroundJobBackendPropertyDefinition CreatedDatetimeProperty = new BackgroundJobBackendPropertyDefinition("CreatedDatetime", typeof(DateTime), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, new DateTime(0L));

		// Token: 0x04000194 RID: 404
		internal static readonly BackgroundJobBackendPropertyDefinition ChangedDatetimeProperty = new BackgroundJobBackendPropertyDefinition("ChangedDatetime", typeof(DateTime), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, new DateTime(0L));

		// Token: 0x04000195 RID: 405
		internal static readonly BackgroundJobBackendPropertyDefinition NextActiveJobIdProperty = new BackgroundJobBackendPropertyDefinition("NextActiveJobId", typeof(Guid), PropertyDefinitionFlags.Mandatory, Guid.Empty);

		// Token: 0x04000196 RID: 406
		internal static readonly BackgroundJobBackendPropertyDefinition ActiveProperty = new BackgroundJobBackendPropertyDefinition("Active", typeof(bool), PropertyDefinitionFlags.Mandatory, false);

		// Token: 0x04000197 RID: 407
		internal static readonly BackgroundJobBackendPropertyDefinition SchedIdToDCIdMappingProperty = new BackgroundJobBackendPropertyDefinition("tvp_BJMScheduleIdToDataCenterId", typeof(DataTable), PropertyDefinitionFlags.Mandatory, null);
	}
}
