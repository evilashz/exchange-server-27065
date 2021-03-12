using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x02000005 RID: 5
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002551 File Offset: 0x00000751
		public LocDescriptionAttribute(MigrationWorkflowServiceStrings.IDs ids) : base(MigrationWorkflowServiceStrings.GetLocalizedString(ids))
		{
		}
	}
}
