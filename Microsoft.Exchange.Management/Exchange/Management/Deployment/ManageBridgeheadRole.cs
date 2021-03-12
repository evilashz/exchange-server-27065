using System;
using System.Management.Automation;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001B4 RID: 436
	[ClassAccessLevel(AccessLevel.Consumer)]
	public abstract class ManageBridgeheadRole : ManageRole
	{
		// Token: 0x06000F57 RID: 3927 RVA: 0x00044249 File Offset: 0x00042449
		protected ManageBridgeheadRole()
		{
			this.StartTransportService = true;
			this.DisableAMFiltering = false;
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x0004425F File Offset: 0x0004245F
		// (set) Token: 0x06000F59 RID: 3929 RVA: 0x00044276 File Offset: 0x00042476
		[Parameter(Mandatory = false)]
		public bool StartTransportService
		{
			get
			{
				return (bool)base.Fields["StartTransportService"];
			}
			set
			{
				base.Fields["StartTransportService"] = value;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x0004428E File Offset: 0x0004248E
		// (set) Token: 0x06000F5B RID: 3931 RVA: 0x000442A5 File Offset: 0x000424A5
		[Parameter(Mandatory = false)]
		public bool DisableAMFiltering
		{
			get
			{
				return (bool)base.Fields["DisableAMFiltering"];
			}
			set
			{
				base.Fields["DisableAMFiltering"] = value;
			}
		}
	}
}
