using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.ManagementGUI.Resources
{
	// Token: 0x02000012 RID: 18
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	public sealed class LocDisplayNameAttribute : LocalizedDisplayNameAttribute
	{
		// Token: 0x060010AA RID: 4266 RVA: 0x00036F0A File Offset: 0x0003510A
		public LocDisplayNameAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
