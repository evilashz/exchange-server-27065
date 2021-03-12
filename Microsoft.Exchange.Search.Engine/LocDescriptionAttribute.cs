using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Search.Engine
{
	// Token: 0x02000019 RID: 25
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x000082B2 File Offset: 0x000064B2
		public LocDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
