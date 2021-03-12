using System;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C90 RID: 3216
	[Cmdlet("Set", "MailboxExportRequest", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMailboxExportRequest : SetRequest<MailboxExportRequestIdParameter>
	{
		// Token: 0x17002653 RID: 9811
		// (get) Token: 0x06007BA8 RID: 31656 RVA: 0x001FADC6 File Offset: 0x001F8FC6
		// (set) Token: 0x06007BA9 RID: 31657 RVA: 0x001FADCE File Offset: 0x001F8FCE
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

		// Token: 0x17002654 RID: 9812
		// (get) Token: 0x06007BAA RID: 31658 RVA: 0x001FADD7 File Offset: 0x001F8FD7
		// (set) Token: 0x06007BAB RID: 31659 RVA: 0x001FADDF File Offset: 0x001F8FDF
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

		// Token: 0x06007BAC RID: 31660 RVA: 0x001FADE8 File Offset: 0x001F8FE8
		protected override void ModifyRequestInternal(TransactionalRequestJob requestJob, StringBuilder changedValuesTracker)
		{
			if (base.IsFieldSet("RemoteCredential"))
			{
				string arg = (requestJob.RemoteCredential == null) ? null : requestJob.RemoteCredential.UserName;
				string arg2 = (this.RemoteCredential == null) ? null : this.RemoteCredential.UserName;
				changedValuesTracker.AppendLine(string.Format("UserName of RemoteCredential: {0} -> {1}", arg, arg2));
				requestJob.RemoteCredential = RequestTaskHelper.GetNetworkCredential(this.RemoteCredential, null);
			}
			if (base.IsFieldSet("RemoteHostName"))
			{
				changedValuesTracker.AppendLine(string.Format("RemoteHostName: {0} -> {1}", requestJob.RemoteHostName, this.RemoteHostName));
				requestJob.RemoteHostName = this.RemoteHostName;
			}
			base.ModifyRequestInternal(requestJob, changedValuesTracker);
		}
	}
}
