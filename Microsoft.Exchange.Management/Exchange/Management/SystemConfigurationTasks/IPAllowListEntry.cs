using System;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A69 RID: 2665
	[Serializable]
	public sealed class IPAllowListEntry : IPListEntry
	{
		// Token: 0x17001CB1 RID: 7345
		// (get) Token: 0x06005F3B RID: 24379 RVA: 0x0018F5DC File Offset: 0x0018D7DC
		public override IPListEntryType ListType
		{
			get
			{
				return IPListEntryType.Allow;
			}
		}
	}
}
