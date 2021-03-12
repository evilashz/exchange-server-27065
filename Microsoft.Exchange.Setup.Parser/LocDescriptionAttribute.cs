using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Parser
{
	// Token: 0x02000011 RID: 17
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00004D45 File Offset: 0x00002F45
		public LocDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
