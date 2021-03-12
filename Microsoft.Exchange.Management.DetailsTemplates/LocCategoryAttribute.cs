using System;
using System.ComponentModel;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.DetailsTemplates
{
	// Token: 0x0200001F RID: 31
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocCategoryAttribute : CategoryAttribute
	{
		// Token: 0x060000DC RID: 220 RVA: 0x000062D2 File Offset: 0x000044D2
		public LocCategoryAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
