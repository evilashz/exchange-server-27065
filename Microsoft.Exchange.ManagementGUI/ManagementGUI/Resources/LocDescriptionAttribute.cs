using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.ManagementGUI.Resources
{
	// Token: 0x02000011 RID: 17
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	public sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x060010A9 RID: 4265 RVA: 0x00036EFC File Offset: 0x000350FC
		public LocDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
