using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000064 RID: 100
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x060002EF RID: 751 RVA: 0x0001150C File Offset: 0x0000F70C
		public LocDescriptionAttribute(Strings.IDs ids) : base(Strings.GetLocalizedString(ids))
		{
		}
	}
}
