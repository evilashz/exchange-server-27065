using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000035 RID: 53
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x060001B9 RID: 441 RVA: 0x00008EA4 File Offset: 0x000070A4
		public LocDescriptionAttribute(MigrationMonitorStrings.IDs ids) : base(MigrationMonitorStrings.GetLocalizedString(ids))
		{
		}
	}
}
