using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Mobility
{
	// Token: 0x0200005D RID: 93
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x06000438 RID: 1080 RVA: 0x000101A9 File Offset: 0x0000E3A9
		public LocDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
