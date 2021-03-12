using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.RmSvcAgent
{
	// Token: 0x02000009 RID: 9
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	public sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x0600002D RID: 45 RVA: 0x0000311C File Offset: 0x0000131C
		public LocDescriptionAttribute(RMSvcAgentStrings.IDs ids) : base(RMSvcAgentStrings.GetLocalizedString(ids))
		{
		}
	}
}
