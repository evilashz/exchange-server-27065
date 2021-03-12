using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PswsClient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200003E RID: 62
	[Cmdlet("Set", "EOPGroup", DefaultParameterSetName = "Identity")]
	public sealed class SetEOPGroup : EOPTask
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000D964 File Offset: 0x0000BB64
		// (set) Token: 0x060002DA RID: 730 RVA: 0x0000D96C File Offset: 0x0000BB6C
		[Parameter(Mandatory = false)]
		public GroupIdParameter Identity { get; set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000D975 File Offset: 0x0000BB75
		// (set) Token: 0x060002DC RID: 732 RVA: 0x0000D97D File Offset: 0x0000BB7D
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public string ExternalDirectoryObjectId { get; set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000D986 File Offset: 0x0000BB86
		// (set) Token: 0x060002DE RID: 734 RVA: 0x0000D98E File Offset: 0x0000BB8E
		[Parameter(Mandatory = false)]
		public string[] ManagedBy { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000D997 File Offset: 0x0000BB97
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x0000D99F File Offset: 0x0000BB9F
		[Parameter(Mandatory = false)]
		public string Notes { get; set; }

		// Token: 0x060002E1 RID: 737 RVA: 0x0000D9A8 File Offset: 0x0000BBA8
		protected override void InternalProcessRecord()
		{
			try
			{
				SetGroupCmdlet setGroupCmdlet = new SetGroupCmdlet();
				ADObjectId executingUserId;
				base.ExchangeRunspaceConfig.TryGetExecutingUserId(out executingUserId);
				setGroupCmdlet.Authenticator = Authenticator.Create(base.CurrentOrganizationId, executingUserId);
				setGroupCmdlet.HostServerName = EOPRecipient.GetPsWsHostServerName();
				if (string.IsNullOrEmpty(this.ExternalDirectoryObjectId) && this.Identity == null)
				{
					base.ThrowTaskError(new ArgumentException(CoreStrings.MissingIdentityParameter.ToString()));
				}
				EOPRecipient.SetProperty(setGroupCmdlet, Parameters.Identity, string.IsNullOrEmpty(this.ExternalDirectoryObjectId) ? this.Identity.ToString() : this.ExternalDirectoryObjectId);
				EOPRecipient.SetProperty(setGroupCmdlet, Parameters.Notes, this.Notes);
				EOPRecipient.SetProperty(setGroupCmdlet, Parameters.ManagedBy, this.ManagedBy);
				EOPRecipient.SetProperty(setGroupCmdlet, Parameters.Organization, base.Organization);
				setGroupCmdlet.Run();
				EOPRecipient.CheckForError(this, setGroupCmdlet);
			}
			catch (Exception e)
			{
				base.ThrowAndLogTaskError(e);
			}
		}
	}
}
