using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.GUI
{
	// Token: 0x0200001A RID: 26
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x06000157 RID: 343 RVA: 0x0000C35E File Offset: 0x0000A55E
		public LocDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
