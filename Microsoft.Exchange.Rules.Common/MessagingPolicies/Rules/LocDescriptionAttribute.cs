using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000055 RID: 85
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x06000282 RID: 642 RVA: 0x0000A964 File Offset: 0x00008B64
		public LocDescriptionAttribute(RulesStrings.IDs ids) : base(RulesStrings.GetLocalizedString(ids))
		{
		}
	}
}
