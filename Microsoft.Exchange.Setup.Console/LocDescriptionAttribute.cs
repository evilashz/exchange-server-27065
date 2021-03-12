using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Console
{
	// Token: 0x02000006 RID: 6
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x06000018 RID: 24 RVA: 0x0000280C File Offset: 0x00000A0C
		public LocDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
