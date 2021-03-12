using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.ReportingTask.Common
{
	// Token: 0x020006B1 RID: 1713
	internal class TaskContext : ITaskContext
	{
		// Token: 0x06003C98 RID: 15512 RVA: 0x00101F0D File Offset: 0x0010010D
		public TaskContext(Task task)
		{
			this.task = task;
		}

		// Token: 0x1700121A RID: 4634
		// (get) Token: 0x06003C99 RID: 15513 RVA: 0x00101F1C File Offset: 0x0010011C
		public Guid CurrentOrganizationGuid
		{
			get
			{
				return this.task.CurrentOrganizationId.OrganizationalUnit.ObjectGuid;
			}
		}

		// Token: 0x1700121B RID: 4635
		// (get) Token: 0x06003C9A RID: 15514 RVA: 0x00101F33 File Offset: 0x00100133
		public Guid CurrentOrganizationExternalDirectoryId
		{
			get
			{
				return Guid.Parse(this.task.CurrentOrganizationId.ToExternalDirectoryOrganizationId());
			}
		}

		// Token: 0x1700121C RID: 4636
		// (get) Token: 0x06003C9B RID: 15515 RVA: 0x00101F4A File Offset: 0x0010014A
		public bool IsCurrentOrganizationForestWide
		{
			get
			{
				return this.task.CurrentOrganizationId.Equals(OrganizationId.ForestWideOrgId);
			}
		}

		// Token: 0x06003C9C RID: 15516 RVA: 0x00101F61 File Offset: 0x00100161
		public void WriteError(LocalizedException localizedException, ExchangeErrorCategory exchangeErrorCategory, object target)
		{
			this.task.WriteError(localizedException, exchangeErrorCategory, target);
		}

		// Token: 0x06003C9D RID: 15517 RVA: 0x00101F71 File Offset: 0x00100171
		public void WriteWarning(LocalizedString text)
		{
			this.task.WriteWarning(text);
		}

		// Token: 0x06003C9E RID: 15518 RVA: 0x00101F7F File Offset: 0x0010017F
		public void WriteVerbose(LocalizedString text)
		{
			this.task.WriteVerbose(text);
		}

		// Token: 0x04002746 RID: 10054
		private Task task;
	}
}
