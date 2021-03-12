using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.ThirdPartyReplication
{
	// Token: 0x02000014 RID: 20
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00002F83 File Offset: 0x00001183
		public LocDescriptionAttribute(ThirdPartyReplication.IDs ids) : base(ThirdPartyReplication.GetLocalizedString(ids))
		{
		}
	}
}
