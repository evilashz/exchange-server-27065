using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x0200023F RID: 575
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x06001714 RID: 5908 RVA: 0x00047828 File Offset: 0x00045A28
		public LocDescriptionAttribute(HygieneDataStrings.IDs ids) : base(HygieneDataStrings.GetLocalizedString(ids))
		{
		}
	}
}
