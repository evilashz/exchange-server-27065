using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000DC RID: 220
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x060006FA RID: 1786 RVA: 0x0000F4AD File Offset: 0x0000D6AD
		public LocDescriptionAttribute(UpgradeHandlerStrings.IDs ids) : base(UpgradeHandlerStrings.GetLocalizedString(ids))
		{
		}
	}
}
