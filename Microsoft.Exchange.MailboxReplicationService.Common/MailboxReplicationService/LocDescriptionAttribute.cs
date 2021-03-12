using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200029C RID: 668
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x060022D2 RID: 8914 RVA: 0x0004D91F File Offset: 0x0004BB1F
		public LocDescriptionAttribute(MrsStrings.IDs ids) : base(MrsStrings.GetLocalizedString(ids))
		{
		}
	}
}
