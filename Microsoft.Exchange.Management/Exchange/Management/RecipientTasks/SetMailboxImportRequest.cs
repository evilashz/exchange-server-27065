using System;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C97 RID: 3223
	[Cmdlet("Set", "MailboxImportRequest", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMailboxImportRequest : SetRequest<MailboxImportRequestIdParameter>
	{
		// Token: 0x17002666 RID: 9830
		// (get) Token: 0x06007BDC RID: 31708 RVA: 0x001FB66E File Offset: 0x001F986E
		// (set) Token: 0x06007BDD RID: 31709 RVA: 0x001FB676 File Offset: 0x001F9876
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public new PSCredential RemoteCredential
		{
			get
			{
				return base.RemoteCredential;
			}
			set
			{
				base.RemoteCredential = value;
			}
		}

		// Token: 0x17002667 RID: 9831
		// (get) Token: 0x06007BDE RID: 31710 RVA: 0x001FB67F File Offset: 0x001F987F
		// (set) Token: 0x06007BDF RID: 31711 RVA: 0x001FB687 File Offset: 0x001F9887
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public new Fqdn RemoteHostName
		{
			get
			{
				return base.RemoteHostName;
			}
			set
			{
				base.RemoteHostName = value;
			}
		}

		// Token: 0x06007BE0 RID: 31712 RVA: 0x001FB690 File Offset: 0x001F9890
		protected override void ModifyRequestInternal(TransactionalRequestJob requestJob, StringBuilder changedValuesTracker)
		{
			if (base.IsFieldSet("RemoteCredential"))
			{
				changedValuesTracker.AppendLine("RemoteCredential: <secure> -> <secure>");
				requestJob.RemoteCredential = RequestTaskHelper.GetNetworkCredential(this.RemoteCredential, null);
			}
			if (base.IsFieldSet("RemoteHostName"))
			{
				changedValuesTracker.AppendLine(string.Format("RemoteHostName: {0} -> {1}", requestJob.RemoteHostName, this.RemoteHostName));
				requestJob.RemoteHostName = this.RemoteHostName;
			}
			base.ModifyRequestInternal(requestJob, changedValuesTracker);
		}

		// Token: 0x06007BE1 RID: 31713 RVA: 0x001FB714 File Offset: 0x001F9914
		protected override void ValidateRequest(TransactionalRequestJob requestJob)
		{
			bool flag = !OrganizationId.ForestWideOrgId.Equals(base.ExecutingUserOrganizationId);
			bool flag2 = !string.IsNullOrEmpty(base.IsFieldSet("RemoteHostName") ? this.RemoteHostName : requestJob.RemoteHostName);
			if (flag && !flag2)
			{
				base.WriteError(new RemoteMailboxImportNeedRemoteProxyException(), ErrorCategory.InvalidArgument, this);
			}
			base.ValidateRequest(requestJob);
		}
	}
}
