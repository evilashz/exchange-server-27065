using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Agent.AntiSpam.Common
{
	// Token: 0x02000020 RID: 32
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x000053B6 File Offset: 0x000035B6
		public LocDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
