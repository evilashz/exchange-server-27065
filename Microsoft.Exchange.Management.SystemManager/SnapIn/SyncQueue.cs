using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x02000287 RID: 647
	public class SyncQueue
	{
		// Token: 0x06001B52 RID: 6994 RVA: 0x0007830C File Offset: 0x0007650C
		public void Add(string key, CommunicationChannelCollection channels, SyncQueueSharedDataCommand command)
		{
			this.syncQueue.Enqueue(new SyncQueue.WaitingCommand
			{
				Key = key,
				Channels = channels,
				Command = command
			});
			if (this.Count == 1)
			{
				command.PrimaryExecute(key, channels, null, null);
			}
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x00078354 File Offset: 0x00076554
		public void AcKnowledge(Guid id)
		{
			this.syncQueue.Dequeue();
			if (this.Count > 0)
			{
				SyncQueue.WaitingCommand waitingCommand = this.syncQueue.Peek();
				waitingCommand.Command.PrimaryExecute(waitingCommand.Key, waitingCommand.Channels, null, null);
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001B54 RID: 6996 RVA: 0x0007839B File Offset: 0x0007659B
		public int Count
		{
			get
			{
				return this.syncQueue.Count;
			}
		}

		// Token: 0x04000A24 RID: 2596
		private Queue<SyncQueue.WaitingCommand> syncQueue = new Queue<SyncQueue.WaitingCommand>();

		// Token: 0x02000288 RID: 648
		private class WaitingCommand
		{
			// Token: 0x17000651 RID: 1617
			// (get) Token: 0x06001B56 RID: 6998 RVA: 0x000783BB File Offset: 0x000765BB
			// (set) Token: 0x06001B57 RID: 6999 RVA: 0x000783C3 File Offset: 0x000765C3
			public string Key { get; set; }

			// Token: 0x17000652 RID: 1618
			// (get) Token: 0x06001B58 RID: 7000 RVA: 0x000783CC File Offset: 0x000765CC
			// (set) Token: 0x06001B59 RID: 7001 RVA: 0x000783D4 File Offset: 0x000765D4
			public CommunicationChannelCollection Channels { get; set; }

			// Token: 0x17000653 RID: 1619
			// (get) Token: 0x06001B5A RID: 7002 RVA: 0x000783DD File Offset: 0x000765DD
			// (set) Token: 0x06001B5B RID: 7003 RVA: 0x000783E5 File Offset: 0x000765E5
			public SyncQueueSharedDataCommand Command { get; set; }
		}
	}
}
