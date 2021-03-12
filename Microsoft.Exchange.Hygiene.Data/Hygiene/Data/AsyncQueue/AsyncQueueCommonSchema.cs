using System;
using System.Data.SqlTypes;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.AsyncQueue
{
	// Token: 0x02000010 RID: 16
	internal class AsyncQueueCommonSchema
	{
		// Token: 0x04000018 RID: 24
		internal static readonly HygienePropertyDefinition PriorityProperty = new HygienePropertyDefinition("Priority", typeof(byte));

		// Token: 0x04000019 RID: 25
		internal static readonly HygienePropertyDefinition RequestIdProperty = new HygienePropertyDefinition("RequestId", typeof(Guid));

		// Token: 0x0400001A RID: 26
		internal static readonly HygienePropertyDefinition RequestStepIdProperty = new HygienePropertyDefinition("RequestStepId", typeof(Guid));

		// Token: 0x0400001B RID: 27
		internal static readonly HygienePropertyDefinition FriendlyNameProperty = new HygienePropertyDefinition("FriendlyName", typeof(string));

		// Token: 0x0400001C RID: 28
		internal static readonly HygienePropertyDefinition RequestFlagsProperty = new HygienePropertyDefinition("RequestFlags", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400001D RID: 29
		internal static readonly HygienePropertyDefinition FlagsProperty = new HygienePropertyDefinition("Flags", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400001E RID: 30
		internal static readonly HygienePropertyDefinition OwnerIdProperty = new HygienePropertyDefinition("OwnerId", typeof(string));

		// Token: 0x0400001F RID: 31
		internal static readonly HygienePropertyDefinition RequestStatusProperty = new HygienePropertyDefinition("RequestStatus", typeof(short), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000020 RID: 32
		internal static readonly HygienePropertyDefinition RequestCookieProperty = new HygienePropertyDefinition("RequestCookie", typeof(string));

		// Token: 0x04000021 RID: 33
		internal static readonly HygienePropertyDefinition CreatedDatetimeProperty = new HygienePropertyDefinition("CreatedDatetime", typeof(DateTime), SqlDateTime.MinValue.Value, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000022 RID: 34
		internal static readonly HygienePropertyDefinition LastModifiedDatetimeProperty = new HygienePropertyDefinition("LastModifiedDatetime", typeof(DateTime), SqlDateTime.MinValue.Value, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000023 RID: 35
		internal static readonly HygienePropertyDefinition OrganizationalUnitRootProperty = new HygienePropertyDefinition("OrganizationalUnitRoot", typeof(Guid));

		// Token: 0x04000024 RID: 36
		internal static readonly HygienePropertyDefinition DependantOrganizationalUnitRootProperty = new HygienePropertyDefinition("DepOrganizationalUnitRoot", typeof(Guid?));

		// Token: 0x04000025 RID: 37
		internal static readonly HygienePropertyDefinition DependantRequestIdProperty = new HygienePropertyDefinition("DependantRequestId", typeof(Guid?));

		// Token: 0x04000026 RID: 38
		internal static readonly HygienePropertyDefinition StepNumberProperty = new HygienePropertyDefinition("StepNumber", typeof(short), short.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000027 RID: 39
		internal static readonly HygienePropertyDefinition StepNameProperty = new HygienePropertyDefinition("StepName", typeof(string));

		// Token: 0x04000028 RID: 40
		internal static readonly HygienePropertyDefinition StepOrdinalProperty = new HygienePropertyDefinition("StepOrdinal", typeof(short), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000029 RID: 41
		internal static readonly HygienePropertyDefinition FetchCountProperty = new HygienePropertyDefinition("FetchCount", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400002A RID: 42
		internal static readonly HygienePropertyDefinition ErrorCountProperty = new HygienePropertyDefinition("ErrorCount", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400002B RID: 43
		internal static readonly HygienePropertyDefinition ProcessInstanceNameProperty = new HygienePropertyDefinition("ProcessInstanceName", typeof(string));

		// Token: 0x0400002C RID: 44
		internal static readonly HygienePropertyDefinition MaxExecutionTimeProperty = new HygienePropertyDefinition("MaxExecutionTime", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400002D RID: 45
		internal static readonly HygienePropertyDefinition CookieProperty = new HygienePropertyDefinition("Cookie", typeof(string));

		// Token: 0x0400002E RID: 46
		internal static readonly HygienePropertyDefinition BatchSizeQueryProperty = new HygienePropertyDefinition("BatchSize", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400002F RID: 47
		internal static readonly HygienePropertyDefinition FailoverWaitInSecondsQueryProperty = new HygienePropertyDefinition("FailoverWaitInSeconds", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000030 RID: 48
		internal static readonly HygienePropertyDefinition PageSizeQueryProperty = new HygienePropertyDefinition("PageSize", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000031 RID: 49
		internal static readonly HygienePropertyDefinition MinCreationDateTimeQueryProperty = new HygienePropertyDefinition("MinCreateDateTime", typeof(DateTime?));

		// Token: 0x04000032 RID: 50
		internal static readonly HygienePropertyDefinition MaxCreationDateTimeQueryProperty = new HygienePropertyDefinition("MaxCreateDateTime", typeof(DateTime?));

		// Token: 0x04000033 RID: 51
		internal static readonly HygienePropertyDefinition DependantOnRequestIdQueryProperty = new HygienePropertyDefinition("DependantOnRequestId", typeof(Guid));

		// Token: 0x04000034 RID: 52
		internal static readonly HygienePropertyDefinition OwnerListQueryProperty = new HygienePropertyDefinition("OwnerProperties", typeof(object), null, ADPropertyDefinitionFlags.MultiValued);

		// Token: 0x04000035 RID: 53
		internal static readonly HygienePropertyDefinition StepFlagsProperty = new HygienePropertyDefinition("StepFlags", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000036 RID: 54
		internal static readonly HygienePropertyDefinition StepStatusProperty = new HygienePropertyDefinition("StepStatus", typeof(short), 0, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
