using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Parser
{
	// Token: 0x02000012 RID: 18
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDisplayNameAttribute : LocalizedDisplayNameAttribute
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00004D53 File Offset: 0x00002F53
		public LocDisplayNameAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
