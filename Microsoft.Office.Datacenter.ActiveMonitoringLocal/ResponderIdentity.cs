using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000028 RID: 40
	public class ResponderIdentity : WorkItemIdentity.Typed<ResponderDefinition>
	{
		// Token: 0x060002C5 RID: 709 RVA: 0x0000C0F5 File Offset: 0x0000A2F5
		public ResponderIdentity(Component component, string baseName, string verb, string targetResource) : base(component, ResponderIdentity.ExpandBaseName(baseName, verb), targetResource)
		{
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000C107 File Offset: 0x0000A307
		private static string ExpandBaseName(string baseName, string displayType)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("baseName", baseName);
			ArgumentValidator.ThrowIfNullOrEmpty("displayType", displayType);
			return WorkItemIdentity.ToLocalName(baseName, displayType);
		}

		// Token: 0x02000029 RID: 41
		public static class Verb
		{
			// Token: 0x04000272 RID: 626
			public const string Restart = "Restart";

			// Token: 0x04000273 RID: 627
			public const string Failover = "Failover";

			// Token: 0x04000274 RID: 628
			public const string KillServer = "KillServer";

			// Token: 0x04000275 RID: 629
			public const string Escalate = "Escalate";
		}
	}
}
