using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Mobility
{
	// Token: 0x0200005E RID: 94
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDisplayNameAttribute : LocalizedDisplayNameAttribute
	{
		// Token: 0x06000439 RID: 1081 RVA: 0x000101B7 File Offset: 0x0000E3B7
		public LocDisplayNameAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
