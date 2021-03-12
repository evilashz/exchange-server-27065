using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A05 RID: 2565
	[Cmdlet("New", "GlobalLocatorServiceMsaUser", DefaultParameterSetName = "MsaUserNetIDParameterSet", SupportsShouldProcess = true)]
	public sealed class NewGlobalLocatorServiceMsaUser : ManageGlobalLocatorServiceBase
	{
		// Token: 0x17001B84 RID: 7044
		// (get) Token: 0x06005BF5 RID: 23541 RVA: 0x00184107 File Offset: 0x00182307
		// (set) Token: 0x06005BF6 RID: 23542 RVA: 0x0018411E File Offset: 0x0018231E
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "MsaUserNetIDParameterSet")]
		public new Guid ExternalDirectoryOrganizationId
		{
			get
			{
				return (Guid)base.Fields["ExternalDirectoryOrganizationId"];
			}
			set
			{
				base.Fields["ExternalDirectoryOrganizationId"] = value;
			}
		}

		// Token: 0x17001B85 RID: 7045
		// (get) Token: 0x06005BF7 RID: 23543 RVA: 0x00184136 File Offset: 0x00182336
		// (set) Token: 0x06005BF8 RID: 23544 RVA: 0x0018414D File Offset: 0x0018234D
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public SmtpAddress MsaUserMemberName
		{
			get
			{
				return (SmtpAddress)base.Fields["MsaUserMemberName"];
			}
			set
			{
				base.Fields["MsaUserMemberName"] = value;
			}
		}

		// Token: 0x17001B86 RID: 7046
		// (get) Token: 0x06005BF9 RID: 23545 RVA: 0x00184165 File Offset: 0x00182365
		// (set) Token: 0x06005BFA RID: 23546 RVA: 0x0018417C File Offset: 0x0018237C
		[Parameter(Mandatory = true)]
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

		// Token: 0x17001B87 RID: 7047
		// (get) Token: 0x06005BFB RID: 23547 RVA: 0x0018418F File Offset: 0x0018238F
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewGls("MsaUser", this.MsaUserNetId.ToString());
			}
		}

		// Token: 0x06005BFC RID: 23548 RVA: 0x001841A8 File Offset: 0x001823A8
		protected override void InternalValidate()
		{
			GlsDirectorySession glsDirectorySession = new GlsDirectorySession();
			string text = this.MsaUserNetId.ToString();
			if (glsDirectorySession.MSAUserExists(text))
			{
				base.WriteGlsMsaUserAlreadyExistsError(text);
			}
			Guid externalDirectoryOrganizationId = this.ExternalDirectoryOrganizationId;
			GlobalLocatorServiceTenant globalLocatorServiceTenant;
			if (!glsDirectorySession.TryGetTenantInfoByOrgGuid(externalDirectoryOrganizationId, out globalLocatorServiceTenant))
			{
				base.WriteGlsTenantNotFoundError(externalDirectoryOrganizationId);
			}
			base.InternalValidate();
		}

		// Token: 0x06005BFD RID: 23549 RVA: 0x001841F8 File Offset: 0x001823F8
		protected override void InternalProcessRecord()
		{
			GlsDirectorySession glsDirectorySession = new GlsDirectorySession();
			GlobalLocatorServiceMsaUser globalLocatorServiceMsaUser = new GlobalLocatorServiceMsaUser
			{
				ExternalDirectoryOrganizationId = this.ExternalDirectoryOrganizationId,
				MsaUserMemberName = this.MsaUserMemberName,
				MsaUserNetId = this.MsaUserNetId
			};
			glsDirectorySession.AddMSAUser(globalLocatorServiceMsaUser.MsaUserNetId.ToString(), globalLocatorServiceMsaUser.MsaUserMemberName.ToString(), globalLocatorServiceMsaUser.ExternalDirectoryOrganizationId);
			base.WriteObject(globalLocatorServiceMsaUser);
		}
	}
}
