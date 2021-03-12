using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA
{
	// Token: 0x02000029 RID: 41
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x060001C9 RID: 457 RVA: 0x000050B5 File Offset: 0x000032B5
		public LocDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
