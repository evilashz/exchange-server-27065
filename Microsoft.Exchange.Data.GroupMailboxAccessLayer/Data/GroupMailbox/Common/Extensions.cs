using System;

namespace Microsoft.Exchange.Data.GroupMailbox.Common
{
	// Token: 0x02000027 RID: 39
	internal static class Extensions
	{
		// Token: 0x0600013D RID: 317 RVA: 0x00009888 File Offset: 0x00007A88
		internal static bool Contains(this GroupMailboxConfigurationAction groupMailboxConfigurationActionMask, GroupMailboxConfigurationAction flag)
		{
			return (groupMailboxConfigurationActionMask & flag) != GroupMailboxConfigurationAction.None;
		}
	}
}
