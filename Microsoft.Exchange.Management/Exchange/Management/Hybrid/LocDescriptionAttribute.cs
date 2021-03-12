using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02001233 RID: 4659
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x0600BBD8 RID: 48088 RVA: 0x002AB810 File Offset: 0x002A9A10
		public LocDescriptionAttribute(HybridStrings.IDs ids) : base(HybridStrings.GetLocalizedString(ids))
		{
		}
	}
}
