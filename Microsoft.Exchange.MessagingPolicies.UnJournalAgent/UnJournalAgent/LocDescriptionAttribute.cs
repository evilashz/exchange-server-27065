using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.UnJournalAgent
{
	// Token: 0x02000025 RID: 37
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x00007A3D File Offset: 0x00005C3D
		public LocDescriptionAttribute(AgentStrings.IDs ids) : base(AgentStrings.GetLocalizedString(ids))
		{
		}
	}
}
