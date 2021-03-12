using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.MapiTasks
{
	// Token: 0x0200048E RID: 1166
	[Cmdlet("Get", "PublicFolderMailboxDiagnostics", SupportsShouldProcess = true)]
	public sealed class GetPublicFolderMailboxDiagnostics : RecipientObjectActionTask<MailboxIdParameter, ADRecipient>
	{
		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x0600294F RID: 10575 RVA: 0x000A3B77 File Offset: 0x000A1D77
		// (set) Token: 0x06002950 RID: 10576 RVA: 0x000A3B7F File Offset: 0x000A1D7F
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeDumpsterInfo { get; set; }

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06002951 RID: 10577 RVA: 0x000A3B88 File Offset: 0x000A1D88
		// (set) Token: 0x06002952 RID: 10578 RVA: 0x000A3B90 File Offset: 0x000A1D90
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeHierarchyInfo { get; set; }

		// Token: 0x06002953 RID: 10579 RVA: 0x000A3B9C File Offset: 0x000A1D9C
		protected override void InternalProcessRecord()
		{
			ADUser aduser = this.DataObject as ADUser;
			if (aduser == null || aduser.RecipientTypeDetails != RecipientTypeDetails.PublicFolderMailbox)
			{
				base.WriteError(new ObjectNotFoundException(Strings.PublicFolderMailboxNotFound), ExchangeErrorCategory.Client, null);
			}
			TenantPublicFolderConfiguration value = TenantPublicFolderConfigurationCache.Instance.GetValue(base.CurrentOrganizationId);
			if (value.GetLocalMailboxRecipient(aduser.ExchangeGuid) == null)
			{
				TenantPublicFolderConfigurationCache.Instance.RemoveValue(base.CurrentOrganizationId);
			}
			DiagnosticsLoadFlags diagnosticsLoadFlags = DiagnosticsLoadFlags.Default;
			if (this.IncludeDumpsterInfo)
			{
				diagnosticsLoadFlags |= DiagnosticsLoadFlags.DumpsterInfo;
			}
			if (this.IncludeHierarchyInfo)
			{
				diagnosticsLoadFlags |= DiagnosticsLoadFlags.HierarchyInfo;
			}
			PublicFolderMailboxDiagnosticsInfo sendToPipeline = PublicFolderMailboxDiagnosticsInfo.Load(base.CurrentOrganizationId, aduser.ExchangeGuid, diagnosticsLoadFlags, new Action<LocalizedString, LocalizedString, int>(base.WriteProgress));
			base.WriteObject(sendToPipeline);
		}
	}
}
