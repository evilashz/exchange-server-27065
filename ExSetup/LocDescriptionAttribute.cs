using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.ExSetup
{
	// Token: 0x02000009 RID: 9
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x06000048 RID: 72 RVA: 0x000034DA File Offset: 0x000016DA
		public LocDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
