using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Console
{
	// Token: 0x02000007 RID: 7
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDisplayNameAttribute : LocalizedDisplayNameAttribute
	{
		// Token: 0x06000019 RID: 25 RVA: 0x0000281A File Offset: 0x00000A1A
		public LocDisplayNameAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
