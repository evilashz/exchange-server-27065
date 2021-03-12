using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.ExSetup
{
	// Token: 0x0200000A RID: 10
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDisplayNameAttribute : LocalizedDisplayNameAttribute
	{
		// Token: 0x06000049 RID: 73 RVA: 0x000034E8 File Offset: 0x000016E8
		public LocDisplayNameAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
