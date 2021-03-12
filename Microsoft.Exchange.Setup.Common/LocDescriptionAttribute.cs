using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000077 RID: 119
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x0600067D RID: 1661 RVA: 0x000165A2 File Offset: 0x000147A2
		public LocDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
