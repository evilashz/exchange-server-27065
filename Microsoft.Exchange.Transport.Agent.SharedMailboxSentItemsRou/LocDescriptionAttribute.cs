using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Agent.SharedMailboxSentItemsRoutingAgent
{
	// Token: 0x0200000F RID: 15
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class LocDescriptionAttribute : LocalizedDescriptionAttribute
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002A3C File Offset: 0x00000C3C
		public LocDescriptionAttribute(AgentStrings.IDs ids) : base(AgentStrings.GetLocalizedString(ids))
		{
		}
	}
}
