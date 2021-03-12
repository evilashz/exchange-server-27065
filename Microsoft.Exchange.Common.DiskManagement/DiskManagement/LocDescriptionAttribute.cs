using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Common.DiskManagement
{
	// Token: 0x0200000C RID: 12
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00004471 File Offset: 0x00002671
		public LocDescriptionAttribute(DiskManagementStrings.IDs ids) : base(DiskManagementStrings.GetLocalizedString(ids))
		{
		}
	}
}
