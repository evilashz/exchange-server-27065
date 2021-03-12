using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000030 RID: 48
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x060000EC RID: 236 RVA: 0x0000374D File Offset: 0x0000194D
		public LocDescriptionAttribute(CXStrings.IDs ids) : base(CXStrings.GetLocalizedString(ids))
		{
		}
	}
}
