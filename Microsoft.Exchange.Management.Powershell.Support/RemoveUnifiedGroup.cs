using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Net.AAD;
using Microsoft.Exchange.UnifiedGroups;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x0200001E RID: 30
	[Cmdlet("Remove", "UnifiedGroup")]
	public sealed class RemoveUnifiedGroup : UnifiedGroupTask
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00006DB8 File Offset: 0x00004FB8
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00006DC0 File Offset: 0x00004FC0
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public Guid Identity { get; set; }

		// Token: 0x06000166 RID: 358 RVA: 0x00006DCC File Offset: 0x00004FCC
		protected override void InternalProcessRecord()
		{
			AADClient aadclient = AADClientFactory.Create(base.OrganizationId, GraphProxyVersions.Version14);
			if (aadclient == null)
			{
				base.WriteError(new TaskException(Strings.ErrorUnableToSessionWithAAD), ExchangeErrorCategory.Client, null);
			}
			try
			{
				base.WriteVerbose("Calling DeleteGroup", new object[0]);
				aadclient.DeleteGroup(this.Identity.ToString());
				base.WriteVerbose("DeleteGroup succeeded", new object[0]);
			}
			catch (AADException ex)
			{
				base.WriteVerbose("DeleteGroup failed with exception: {0}", new object[]
				{
					ex
				});
				base.WriteError(new TaskException(Strings.ErrorUnableToRemove(this.Identity.ToString())), base.GetErrorCategory(ex), null);
			}
		}
	}
}
