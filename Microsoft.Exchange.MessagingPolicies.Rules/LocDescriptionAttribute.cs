using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies
{
	// Token: 0x020000A8 RID: 168
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x060004EA RID: 1258 RVA: 0x00018084 File Offset: 0x00016284
		public LocDescriptionAttribute(TransportRulesStrings.IDs ids) : base(TransportRulesStrings.GetLocalizedString(ids))
		{
		}
	}
}
