using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Inference.MdbCommon
{
	// Token: 0x02000024 RID: 36
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x00005715 File Offset: 0x00003915
		public LocDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
