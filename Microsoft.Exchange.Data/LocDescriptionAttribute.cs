using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000D4 RID: 212
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x060007BF RID: 1983 RVA: 0x0001A634 File Offset: 0x00018834
		public LocDescriptionAttribute(DataStrings.IDs ids) : base(DataStrings.GetLocalizedString(ids))
		{
		}
	}
}
