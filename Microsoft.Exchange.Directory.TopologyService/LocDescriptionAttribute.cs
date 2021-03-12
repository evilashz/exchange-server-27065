using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x0200003A RID: 58
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x06000238 RID: 568 RVA: 0x0000F06D File Offset: 0x0000D26D
		public LocDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
