using System;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000044 RID: 68
	public static class WorkItemIdentityExtensions
	{
		// Token: 0x060004DD RID: 1245 RVA: 0x0001209F File Offset: 0x0001029F
		public static TDefinition Apply<TDefinition>(this TDefinition definition, WorkItemIdentity.Typed<TDefinition> identity) where TDefinition : WorkDefinition
		{
			identity.ApplyTo(definition);
			return definition;
		}
	}
}
