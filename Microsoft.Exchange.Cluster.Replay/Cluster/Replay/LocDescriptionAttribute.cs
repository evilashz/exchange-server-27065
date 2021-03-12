using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000397 RID: 919
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x06002741 RID: 10049 RVA: 0x000B59DC File Offset: 0x000B3BDC
		public LocDescriptionAttribute(ReplayStrings.IDs ids) : base(ReplayStrings.GetLocalizedString(ids))
		{
		}
	}
}
