using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A07 RID: 2567
	[Cmdlet("Set", "GlobalLocatorServiceMsaUser", DefaultParameterSetName = "MsaUserNetIDParameterSet", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class SetGlobalLocatorServiceMsaUser : ManageGlobalLocatorServiceBase
	{
		// Token: 0x17001B8B RID: 7051
		// (get) Token: 0x06005C07 RID: 23559 RVA: 0x00184328 File Offset: 0x00182528
		// (set) Token: 0x06005C08 RID: 23560 RVA: 0x0018433F File Offset: 0x0018253F
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "MsaUserNetIDParameterSet")]
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

		// Token: 0x17001B8C RID: 7052
		// (get) Token: 0x06005C09 RID: 23561 RVA: 0x00184357 File Offset: 0x00182557
		// (set) Token: 0x06005C0A RID: 23562 RVA: 0x0018436E File Offset: 0x0018256E
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

		// Token: 0x17001B8D RID: 7053
		// (get) Token: 0x06005C0B RID: 23563 RVA: 0x00184381 File Offset: 0x00182581
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetGls("MsaUser", this.MsaUserNetId.ToString());
			}
		}

		// Token: 0x06005C0C RID: 23564 RVA: 0x00184398 File Offset: 0x00182598
		protected override void InternalValidate()
		{
			GlsDirectorySession glsDirectorySession = new GlsDirectorySession();
			if (!base.Fields.IsModified("ExternalDirectoryOrganizationId") && !base.Fields.IsModified("MsaUserMemberName"))
			{
				base.WriteError(new TaskArgumentException(Strings.ErrorNoPropertyWasModified), ExchangeErrorCategory.Client, null);
			}
			if (base.Fields.IsModified("ExternalDirectoryOrganizationId"))
			{
				Guid externalDirectoryOrganizationId = base.ExternalDirectoryOrganizationId;
				GlobalLocatorServiceTenant globalLocatorServiceTenant;
				if (!glsDirectorySession.TryGetTenantInfoByOrgGuid(externalDirectoryOrganizationId, out globalLocatorServiceTenant))
				{
					base.WriteGlsTenantNotFoundError(externalDirectoryOrganizationId);
				}
			}
			this.currentGlsMsaUser = new GlobalLocatorServiceMsaUser
			{
				MsaUserNetId = this.MsaUserNetId
			};
			string address = null;
			string text = this.MsaUserNetId.ToString();
			Guid externalDirectoryOrganizationId2;
			string resourceForest;
			string accountForest;
			string tenantContainerCN;
			if (!glsDirectorySession.TryGetTenantForestsByMSAUserNetID(text, out externalDirectoryOrganizationId2, out resourceForest, out accountForest, out tenantContainerCN) || !glsDirectorySession.TryGetMSAUserMemberName(text, out address))
			{
				base.WriteGlsMsaUserNotFoundError(text);
			}
			this.currentGlsMsaUser.MsaUserMemberName = SmtpAddress.Parse(address);
			this.currentGlsMsaUser.ExternalDirectoryOrganizationId = externalDirectoryOrganizationId2;
			this.currentGlsMsaUser.ResourceForest = resourceForest;
			this.currentGlsMsaUser.AccountForest = accountForest;
			this.currentGlsMsaUser.TenantContainerCN = tenantContainerCN;
			base.InternalValidate();
		}

		// Token: 0x06005C0D RID: 23565 RVA: 0x001844B0 File Offset: 0x001826B0
		protected override void InternalProcessRecord()
		{
			GlsDirectorySession glsDirectorySession = new GlsDirectorySession();
			GlobalLocatorServiceMsaUser globalLocatorServiceMsaUser = new GlobalLocatorServiceMsaUser
			{
				MsaUserNetId = this.MsaUserNetId,
				ExternalDirectoryOrganizationId = (base.Fields.IsModified("ExternalDirectoryOrganizationId") ? base.ExternalDirectoryOrganizationId : this.currentGlsMsaUser.ExternalDirectoryOrganizationId),
				MsaUserMemberName = (base.Fields.IsModified("MsaUserMemberName") ? this.MsaUserMemberName : this.currentGlsMsaUser.MsaUserMemberName)
			};
			glsDirectorySession.UpdateMSAUser(globalLocatorServiceMsaUser.MsaUserNetId.ToString(), globalLocatorServiceMsaUser.MsaUserMemberName.ToString(), globalLocatorServiceMsaUser.ExternalDirectoryOrganizationId);
			base.WriteObject(globalLocatorServiceMsaUser);
		}

		// Token: 0x0400345E RID: 13406
		private GlobalLocatorServiceMsaUser currentGlsMsaUser;
	}
}
