using System;
using System.Management.Automation;

namespace Microsoft.Office.Datacenter.ActiveMonitoring.Management
{
	// Token: 0x02000005 RID: 5
	public abstract class GetHealthBase : PSCmdlet
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000022DC File Offset: 0x000004DC
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000022E4 File Offset: 0x000004E4
		[Alias(new string[]
		{
			"Server"
		})]
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		[ValidateNotNullOrEmpty]
		public string Identity { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022ED File Offset: 0x000004ED
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000022F5 File Offset: 0x000004F5
		[Parameter(Mandatory = false)]
		public string HealthSet { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000022FE File Offset: 0x000004FE
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002306 File Offset: 0x00000506
		[Parameter(Mandatory = false)]
		public SwitchParameter HaImpactingOnly { get; set; }
	}
}
