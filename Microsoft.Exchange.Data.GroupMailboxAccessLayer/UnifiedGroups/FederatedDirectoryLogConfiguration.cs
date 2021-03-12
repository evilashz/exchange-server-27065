using System;
using Microsoft.Exchange.Data.Storage.Optics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.UnifiedGroups
{
	// Token: 0x02000003 RID: 3
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class FederatedDirectoryLogConfiguration : LogConfigurationBase
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000023CF File Offset: 0x000005CF
		public static FederatedDirectoryLogConfiguration Default
		{
			get
			{
				if (FederatedDirectoryLogConfiguration.defaultInstance == null)
				{
					FederatedDirectoryLogConfiguration.defaultInstance = new FederatedDirectoryLogConfiguration();
				}
				return FederatedDirectoryLogConfiguration.defaultInstance;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000023E7 File Offset: 0x000005E7
		protected override string Component
		{
			get
			{
				return "FederatedDirectory";
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000023EE File Offset: 0x000005EE
		protected override string Type
		{
			get
			{
				return "Federated Directory Log";
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000023F5 File Offset: 0x000005F5
		protected override Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ModernGroupsTracer;
			}
		}

		// Token: 0x04000003 RID: 3
		private static FederatedDirectoryLogConfiguration defaultInstance;
	}
}
