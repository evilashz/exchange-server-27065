using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200058C RID: 1420
	public abstract class GetServerHealthBase : Task
	{
		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x0600320A RID: 12810 RVA: 0x000CB5C4 File Offset: 0x000C97C4
		// (set) Token: 0x0600320B RID: 12811 RVA: 0x000CB5DB File Offset: 0x000C97DB
		[Alias(new string[]
		{
			"Server"
		})]
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		[ValidateNotNullOrEmpty]
		public ServerIdParameter Identity
		{
			get
			{
				return (ServerIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x0600320C RID: 12812 RVA: 0x000CB5EE File Offset: 0x000C97EE
		// (set) Token: 0x0600320D RID: 12813 RVA: 0x000CB5F6 File Offset: 0x000C97F6
		[Parameter(Mandatory = false)]
		public string HealthSet
		{
			get
			{
				return this.healthSet;
			}
			set
			{
				this.healthSet = value;
			}
		}

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x0600320E RID: 12814 RVA: 0x000CB5FF File Offset: 0x000C97FF
		// (set) Token: 0x0600320F RID: 12815 RVA: 0x000CB625 File Offset: 0x000C9825
		[Parameter(Mandatory = false)]
		public SwitchParameter HaImpactingOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields["HaImpactingOnly"] ?? false);
			}
			set
			{
				base.Fields["HaImpactingOnly"] = value;
			}
		}

		// Token: 0x04002345 RID: 9029
		private string healthSet;
	}
}
