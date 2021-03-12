using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A04 RID: 2564
	[Cmdlet("Get", "GlobalLocatorServiceMsaUser", DefaultParameterSetName = "MsaUserNetIDParameterSet")]
	public sealed class GetGlobalLocatorServiceMsaUser : ManageGlobalLocatorServiceBase
	{
		// Token: 0x17001B82 RID: 7042
		// (get) Token: 0x06005BEF RID: 23535 RVA: 0x00184033 File Offset: 0x00182233
		// (set) Token: 0x06005BF0 RID: 23536 RVA: 0x0018403B File Offset: 0x0018223B
		private new Guid ExternalDirectoryOrganizationId { get; set; }

		// Token: 0x17001B83 RID: 7043
		// (get) Token: 0x06005BF1 RID: 23537 RVA: 0x00184044 File Offset: 0x00182244
		// (set) Token: 0x06005BF2 RID: 23538 RVA: 0x0018405B File Offset: 0x0018225B
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0, ParameterSetName = "MsaUserNetIDParameterSet")]
		[ValidateNotNullOrEmpty]
		public NetID MsaUserNetId
		{
			get
			{
				return (NetID)base.Fields["MsaUserNetID"];
			}
			set
			{
				base.Fields["MsaUserNetID"] = value;
			}
		}

		// Token: 0x06005BF3 RID: 23539 RVA: 0x00184070 File Offset: 0x00182270
		protected override void InternalProcessRecord()
		{
			GlsDirectorySession glsDirectorySession = new GlsDirectorySession();
			GlobalLocatorServiceMsaUser globalLocatorServiceMsaUser = new GlobalLocatorServiceMsaUser
			{
				MsaUserNetId = this.MsaUserNetId
			};
			string text = globalLocatorServiceMsaUser.MsaUserNetId.ToString();
			string address = null;
			Guid externalDirectoryOrganizationId;
			string resourceForest;
			string accountForest;
			string tenantContainerCN;
			if (!glsDirectorySession.TryGetTenantForestsByMSAUserNetID(text, out externalDirectoryOrganizationId, out resourceForest, out accountForest, out tenantContainerCN) || !glsDirectorySession.TryGetMSAUserMemberName(text, out address))
			{
				base.WriteGlsMsaUserNotFoundError(text);
			}
			globalLocatorServiceMsaUser.MsaUserMemberName = SmtpAddress.Parse(address);
			globalLocatorServiceMsaUser.ExternalDirectoryOrganizationId = externalDirectoryOrganizationId;
			globalLocatorServiceMsaUser.ResourceForest = resourceForest;
			globalLocatorServiceMsaUser.AccountForest = accountForest;
			globalLocatorServiceMsaUser.TenantContainerCN = tenantContainerCN;
			base.WriteObject(globalLocatorServiceMsaUser);
		}
	}
}
