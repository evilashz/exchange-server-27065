using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.ManagementEndpoint
{
	// Token: 0x02000481 RID: 1153
	[Cmdlet("Remove", "ManagementEndpointHook", SupportsShouldProcess = true)]
	public sealed class RemoveManagementEndpointHook : ManagementEndpointBase
	{
		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x06002886 RID: 10374 RVA: 0x0009F0E2 File Offset: 0x0009D2E2
		// (set) Token: 0x06002887 RID: 10375 RVA: 0x0009F0EA File Offset: 0x0009D2EA
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, Position = 0)]
		public Guid ExternalDirectoryOrganizationId { get; set; }

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x06002888 RID: 10376 RVA: 0x0009F0F3 File Offset: 0x0009D2F3
		// (set) Token: 0x06002889 RID: 10377 RVA: 0x0009F0FB File Offset: 0x0009D2FB
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public SmtpDomain DomainName { get; set; }

		// Token: 0x0600288A RID: 10378 RVA: 0x0009F104 File Offset: 0x0009D304
		internal override void ProcessRedirectionEntry(IGlobalDirectorySession session)
		{
			if (this.DomainName == null)
			{
				try
				{
					session.RemoveTenant(this.ExternalDirectoryOrganizationId);
					return;
				}
				catch (GlsTenantNotFoundException ex)
				{
					base.WriteWarning(string.Format("GlsTenantNotFoundException ignored during removal. exception: {0}", ex.Message));
					return;
				}
			}
			try
			{
				session.RemoveAcceptedDomain(this.ExternalDirectoryOrganizationId, this.DomainName.Domain);
			}
			catch (GlsPermanentException ex2)
			{
				if (ex2.Message.IndexOf("DomainBelongsToDifferentTenantException", StringComparison.OrdinalIgnoreCase) == -1 && ex2.Message.IndexOf("DataNotFound", StringComparison.OrdinalIgnoreCase) == -1)
				{
					throw ex2;
				}
				base.WriteWarning(string.Format("Gls exception ignored during removal. exception: {0}", ex2.Message));
			}
			catch (GlsTenantNotFoundException)
			{
			}
		}
	}
}
