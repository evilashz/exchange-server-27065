using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend
{
	// Token: 0x02000037 RID: 55
	internal static class BackgroundJobMgrInstanceProperties
	{
		// Token: 0x04000139 RID: 313
		internal static readonly BackgroundJobBackendPropertyDefinition MachineIdProperty = new BackgroundJobBackendPropertyDefinition("MachineId", typeof(Guid), PropertyDefinitionFlags.Mandatory, Guid.Empty);

		// Token: 0x0400013A RID: 314
		internal static readonly BackgroundJobBackendPropertyDefinition MachineNameProperty = new BackgroundJobBackendPropertyDefinition("MachineName", typeof(string), PropertyDefinitionFlags.Mandatory, null);

		// Token: 0x0400013B RID: 315
		internal static readonly BackgroundJobBackendPropertyDefinition RoleIdProperty = new BackgroundJobBackendPropertyDefinition("RoleId", typeof(Guid), PropertyDefinitionFlags.Mandatory, Guid.Empty);

		// Token: 0x0400013C RID: 316
		internal static readonly BackgroundJobBackendPropertyDefinition HeartBeatProperty = new BackgroundJobBackendPropertyDefinition("Heartbeat", typeof(DateTime), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, new DateTime(0L));

		// Token: 0x0400013D RID: 317
		internal static readonly BackgroundJobBackendPropertyDefinition ActiveProperty = new BackgroundJobBackendPropertyDefinition("Active", typeof(bool), PropertyDefinitionFlags.Mandatory, false);

		// Token: 0x0400013E RID: 318
		internal static readonly BackgroundJobBackendPropertyDefinition DCProperty = new BackgroundJobBackendPropertyDefinition("DC", typeof(long), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, 0L);

		// Token: 0x0400013F RID: 319
		internal static readonly BackgroundJobBackendPropertyDefinition RegionProperty = new BackgroundJobBackendPropertyDefinition("Region", typeof(int), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, 0);

		// Token: 0x04000140 RID: 320
		internal static readonly BackgroundJobBackendPropertyDefinition SyncContextProperty = new BackgroundJobBackendPropertyDefinition("SyncContext", typeof(Guid), PropertyDefinitionFlags.Mandatory, Guid.Empty);
	}
}
