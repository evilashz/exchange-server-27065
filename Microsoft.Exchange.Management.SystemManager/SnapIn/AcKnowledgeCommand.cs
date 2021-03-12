using System;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x02000284 RID: 644
	[Serializable]
	public class AcKnowledgeCommand : NotificationSharedDataCommand
	{
		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001B41 RID: 6977 RVA: 0x000781DC File Offset: 0x000763DC
		// (set) Token: 0x06001B42 RID: 6978 RVA: 0x000781E4 File Offset: 0x000763E4
		public Guid AcknowledgedId { get; set; }

		// Token: 0x06001B43 RID: 6979 RVA: 0x000781ED File Offset: 0x000763ED
		public override void PrimaryExecute(string key, CommunicationChannelCollection channels, NodeStructureSettingsStore store, SyncQueue syncQueue)
		{
			syncQueue.AcKnowledge(this.AcknowledgedId);
		}
	}
}
