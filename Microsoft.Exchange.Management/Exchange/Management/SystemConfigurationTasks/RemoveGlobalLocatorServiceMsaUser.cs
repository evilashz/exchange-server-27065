using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A06 RID: 2566
	[Cmdlet("Remove", "GlobalLocatorServiceMsaUser", DefaultParameterSetName = "MsaUserNetIDParameterSet", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveGlobalLocatorServiceMsaUser : ManageGlobalLocatorServiceBase
	{
		// Token: 0x17001B88 RID: 7048
		// (get) Token: 0x06005BFF RID: 23551 RVA: 0x00184271 File Offset: 0x00182471
		// (set) Token: 0x06005C00 RID: 23552 RVA: 0x00184279 File Offset: 0x00182479
		private new Guid ExternalDirectoryOrganizationId { get; set; }

		// Token: 0x17001B89 RID: 7049
		// (get) Token: 0x06005C01 RID: 23553 RVA: 0x00184282 File Offset: 0x00182482
		// (set) Token: 0x06005C02 RID: 23554 RVA: 0x00184299 File Offset: 0x00182499
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0, ParameterSetName = "MsaUserNetIDParameterSet")]
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

		// Token: 0x17001B8A RID: 7050
		// (get) Token: 0x06005C03 RID: 23555 RVA: 0x001842AC File Offset: 0x001824AC
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveGls("MsaUser", this.MsaUserNetId.ToString());
			}
		}

		// Token: 0x06005C04 RID: 23556 RVA: 0x001842C4 File Offset: 0x001824C4
		protected override void InternalValidate()
		{
			GlsDirectorySession glsDirectorySession = new GlsDirectorySession();
			string text = this.MsaUserNetId.ToString();
			if (!glsDirectorySession.MSAUserExists(text))
			{
				base.WriteGlsMsaUserNotFoundError(text);
			}
			base.InternalValidate();
		}

		// Token: 0x06005C05 RID: 23557 RVA: 0x001842FC File Offset: 0x001824FC
		protected override void InternalProcessRecord()
		{
			GlsDirectorySession glsDirectorySession = new GlsDirectorySession();
			glsDirectorySession.RemoveMSAUser(this.MsaUserNetId.ToString());
		}
	}
}
