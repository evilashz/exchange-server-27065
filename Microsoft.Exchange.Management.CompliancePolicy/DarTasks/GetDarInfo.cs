using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.CompliancePolicy.LocStrings;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.Management.CompliancePolicy.DarTasks
{
	// Token: 0x02000003 RID: 3
	[Cmdlet("Get", "DarInfo")]
	public sealed class GetDarInfo : Task
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002304 File Offset: 0x00000504
		public GetDarInfo()
		{
			this.ExecutionUnit = new ServerIdParameter();
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002317 File Offset: 0x00000517
		// (set) Token: 0x0600001C RID: 28 RVA: 0x0000231F File Offset: 0x0000051F
		[Parameter(Mandatory = false)]
		public OrganizationIdParameter TenantId { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002328 File Offset: 0x00000528
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002330 File Offset: 0x00000530
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public ServerIdParameter ExecutionUnit { get; set; }

		// Token: 0x0600001F RID: 31 RVA: 0x0000233C File Offset: 0x0000053C
		protected override void InternalProcessRecord()
		{
			if (this.TenantId != null)
			{
				try
				{
					base.CurrentOrganizationId = GetDarTask.ResolveOrganizationId(this.TenantId);
				}
				catch (ArgumentException exception)
				{
					base.WriteError(exception, ErrorCategory.InvalidArgument, this.TenantId);
					return;
				}
				base.WriteObject(string.Format(Strings.ResolvedOrg, base.CurrentOrganizationId));
				this.ExecutionUnit = GetDarTask.ResolveServerId(base.CurrentOrganizationId);
				base.WriteObject(string.Format(Strings.ResolvedServer, this.ExecutionUnit));
			}
			try
			{
				using (HostRpcClient hostRpcClient = new HostRpcClient(this.ExecutionUnit.Fqdn))
				{
					string darInfo = hostRpcClient.GetDarInfo();
					if (!string.IsNullOrEmpty(darInfo))
					{
						foreach (string sendToPipeline in darInfo.Split(new char[]
						{
							'\n'
						}))
						{
							base.WriteObject(sendToPipeline);
						}
					}
				}
			}
			catch (ServerUnavailableException exception2)
			{
				base.WriteError(exception2, ErrorCategory.ConnectionError, this.ExecutionUnit.Fqdn);
			}
		}
	}
}
