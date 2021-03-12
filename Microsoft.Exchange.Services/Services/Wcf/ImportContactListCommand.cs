using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.Aggregation;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009BE RID: 2494
	internal sealed class ImportContactListCommand : SingleCmdletCommandBase<ImportContactListRequest, ImportContactListResponse, Microsoft.Exchange.Management.Aggregation.ImportContactList, ImportContactListResult>
	{
		// Token: 0x060046C0 RID: 18112 RVA: 0x000FB9B3 File Offset: 0x000F9BB3
		public ImportContactListCommand(CallContext callContext, ImportContactListRequest request) : base(callContext, request, "Import-ContactList", ScopeLocation.RecipientRead)
		{
		}

		// Token: 0x060046C1 RID: 18113 RVA: 0x000FB9C4 File Offset: 0x000F9BC4
		protected override void PopulateTaskParameters()
		{
			Microsoft.Exchange.Management.Aggregation.ImportContactList task = this.cmdletRunner.TaskWrapper.Task;
			this.cmdletRunner.SetTaskParameter("CSV", task, new SwitchParameter(true));
			if (this.request != null && this.request.ImportedContactList.CSVData != null)
			{
				this.cmdletRunner.SetTaskParameter("CSVData", task, this.request.ImportedContactList.CSVData);
			}
			this.cmdletRunner.SetTaskParameter("Identity", task, new MailboxIdParameter(base.CallContext.AccessingPrincipal.ObjectId));
		}

		// Token: 0x060046C2 RID: 18114 RVA: 0x000FBA60 File Offset: 0x000F9C60
		protected override void PopulateResponseData(ImportContactListResponse response)
		{
			ImportContactListResult result = this.cmdletRunner.TaskWrapper.Result;
			response.NumberOfContactsImported = result.ContactsImported;
		}

		// Token: 0x060046C3 RID: 18115 RVA: 0x000FBA8A File Offset: 0x000F9C8A
		protected override PSLocalTask<Microsoft.Exchange.Management.Aggregation.ImportContactList, ImportContactListResult> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateImportContactListTask(base.CallContext.AccessingPrincipal);
		}
	}
}
