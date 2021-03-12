using System;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A6A RID: 2666
	[Serializable]
	public sealed class IPBlockListEntry : IPListEntry
	{
		// Token: 0x17001CB2 RID: 7346
		// (get) Token: 0x06005F3D RID: 24381 RVA: 0x0018F5E7 File Offset: 0x0018D7E7
		public override IPListEntryType ListType
		{
			get
			{
				return IPListEntryType.Block;
			}
		}
	}
}
