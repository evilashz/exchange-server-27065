using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend
{
	// Token: 0x0200003B RID: 59
	internal static class JobDefinitionProperties
	{
		// Token: 0x04000153 RID: 339
		internal static readonly BackgroundJobBackendPropertyDefinition BackgroundJobIdProperty = new BackgroundJobBackendPropertyDefinition("BackgroundJobId", typeof(Guid), PropertyDefinitionFlags.Mandatory, Guid.Empty);

		// Token: 0x04000154 RID: 340
		internal static readonly BackgroundJobBackendPropertyDefinition NameProperty = new BackgroundJobBackendPropertyDefinition("Name", typeof(string), PropertyDefinitionFlags.Mandatory, null);

		// Token: 0x04000155 RID: 341
		internal static readonly BackgroundJobBackendPropertyDefinition PathProperty = new BackgroundJobBackendPropertyDefinition("Path", typeof(string), PropertyDefinitionFlags.Mandatory, null);

		// Token: 0x04000156 RID: 342
		internal static readonly BackgroundJobBackendPropertyDefinition CommandLineProperty = new BackgroundJobBackendPropertyDefinition("CommandLine", typeof(string), PropertyDefinitionFlags.Mandatory, null);

		// Token: 0x04000157 RID: 343
		internal static readonly BackgroundJobBackendPropertyDefinition DescriptionProperty = new BackgroundJobBackendPropertyDefinition("Description", typeof(string), PropertyDefinitionFlags.Mandatory, null);

		// Token: 0x04000158 RID: 344
		internal static readonly BackgroundJobBackendPropertyDefinition RoleIdProperty = new BackgroundJobBackendPropertyDefinition("RoleId", typeof(Guid), PropertyDefinitionFlags.Mandatory, Guid.Empty);

		// Token: 0x04000159 RID: 345
		internal static readonly BackgroundJobBackendPropertyDefinition SingleInstancePerMachineProperty = new BackgroundJobBackendPropertyDefinition("SingleInstancePerMachine", typeof(bool), PropertyDefinitionFlags.Mandatory, false);

		// Token: 0x0400015A RID: 346
		internal static readonly BackgroundJobBackendPropertyDefinition SchedulingStrategyProperty = new BackgroundJobBackendPropertyDefinition("SchedulingStrategy", typeof(byte), PropertyDefinitionFlags.Mandatory, 0);

		// Token: 0x0400015B RID: 347
		internal static readonly BackgroundJobBackendPropertyDefinition TimeoutProperty = new BackgroundJobBackendPropertyDefinition("Timeout", typeof(int), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, 0);

		// Token: 0x0400015C RID: 348
		internal static readonly BackgroundJobBackendPropertyDefinition MaxLocalRetryCountProperty = new BackgroundJobBackendPropertyDefinition("MaxLocalRetryCount", typeof(byte), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, 0);

		// Token: 0x0400015D RID: 349
		internal static readonly BackgroundJobBackendPropertyDefinition MaxFailoverCountProperty = new BackgroundJobBackendPropertyDefinition("MaxFailoverCount", typeof(short), PropertyDefinitionFlags.Mandatory, 0);
	}
}
