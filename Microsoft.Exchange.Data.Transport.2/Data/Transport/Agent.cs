using System;
using System.Collections;
using System.Collections.Specialized;
using System.Threading;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x0200001E RID: 30
	public abstract class Agent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002271 File Offset: 0x00000471
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002279 File Offset: 0x00000479
		internal string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002282 File Offset: 0x00000482
		// (set) Token: 0x0600005B RID: 91 RVA: 0x0000228D File Offset: 0x0000048D
		internal bool Synchronous
		{
			get
			{
				return this.asynchronous == 0;
			}
			set
			{
				this.asynchronous = (value ? 0 : 1);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000229C File Offset: 0x0000049C
		// (set) Token: 0x0600005D RID: 93 RVA: 0x000022A4 File Offset: 0x000004A4
		internal string EventArgId
		{
			get
			{
				return this.eventArgId;
			}
			set
			{
				this.eventArgId = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000022AD File Offset: 0x000004AD
		// (set) Token: 0x0600005F RID: 95 RVA: 0x000022B5 File Offset: 0x000004B5
		internal SnapshotWriter SnapshotWriter
		{
			get
			{
				return this.snapshotWriter;
			}
			set
			{
				this.snapshotWriter = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000022BE File Offset: 0x000004BE
		// (set) Token: 0x06000061 RID: 97 RVA: 0x000022C6 File Offset: 0x000004C6
		internal bool SnapshotEnabled
		{
			get
			{
				return this.snapshotEnabled;
			}
			set
			{
				this.snapshotEnabled = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000022CF File Offset: 0x000004CF
		// (set) Token: 0x06000063 RID: 99 RVA: 0x000022D7 File Offset: 0x000004D7
		internal IExecutionControl Session
		{
			get
			{
				return this.session;
			}
			set
			{
				this.session = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000022E0 File Offset: 0x000004E0
		internal IDictionary Handlers
		{
			get
			{
				return this.handlers;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000022E8 File Offset: 0x000004E8
		// (set) Token: 0x06000066 RID: 102 RVA: 0x000022F0 File Offset: 0x000004F0
		internal object HostStateInternal
		{
			get
			{
				return this.hostState;
			}
			set
			{
				this.hostState = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000022F9 File Offset: 0x000004F9
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002301 File Offset: 0x00000501
		internal virtual object HostState
		{
			get
			{
				return this.hostState;
			}
			set
			{
				this.hostState = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000230A File Offset: 0x0000050A
		internal virtual string SnapshotPrefix
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002311 File Offset: 0x00000511
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00002319 File Offset: 0x00000519
		internal MailItem MailItem
		{
			get
			{
				return this.mailItem;
			}
			set
			{
				this.mailItem = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002322 File Offset: 0x00000522
		// (set) Token: 0x0600006D RID: 109 RVA: 0x0000232A File Offset: 0x0000052A
		protected internal string Name
		{
			get
			{
				return this.name;
			}
			internal set
			{
				this.name = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002333 File Offset: 0x00000533
		// (set) Token: 0x0600006F RID: 111 RVA: 0x0000233B File Offset: 0x0000053B
		protected internal string EventTopic
		{
			get
			{
				return this.topic;
			}
			internal set
			{
				this.topic = value;
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002344 File Offset: 0x00000544
		internal void AddHandler(string eventTopic, Delegate handler)
		{
			try
			{
				this.handlers.Add(eventTopic, handler);
			}
			catch (ArgumentException innerException)
			{
				throw new InvalidOperationException(string.Format("A transport agent has attempted to subscribe to the same event - {0} - more than once", eventTopic), innerException);
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002384 File Offset: 0x00000584
		internal void RemoveHandler(string eventTopic)
		{
			try
			{
				this.handlers.Remove(eventTopic);
			}
			catch (ArgumentException innerException)
			{
				throw new InvalidOperationException(string.Format("A transport agent has attempted to subscribe to the same event - {0} - more than once", eventTopic), innerException);
			}
		}

		// Token: 0x06000072 RID: 114
		internal abstract void Invoke(string eventTopic, object source, object e);

		// Token: 0x06000073 RID: 115 RVA: 0x000023C4 File Offset: 0x000005C4
		internal virtual void AsyncComplete()
		{
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000023C6 File Offset: 0x000005C6
		internal void EnsureMimeWriteStreamClosed()
		{
			if (!this.mailItem.MimeWriteStreamOpen)
			{
				return;
			}
			this.mailItem.RestoreLastSavedMime(this.name, this.topic);
			this.mailItem.Recipients.Clear();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000023FD File Offset: 0x000005FD
		protected AgentAsyncContext GetAgentAsyncContext()
		{
			if (Interlocked.Increment(ref this.asynchronous) != 1)
			{
				throw new InvalidOperationException(string.Format("Agent '{0}' ({1}) tried to acquire its asynchronous context object more than once while handling event '{2}'", this.name, this.id, this.topic));
			}
			return new AgentAsyncContext(this);
		}

		// Token: 0x0400003B RID: 59
		private string id;

		// Token: 0x0400003C RID: 60
		private string name;

		// Token: 0x0400003D RID: 61
		private string topic;

		// Token: 0x0400003E RID: 62
		private int asynchronous;

		// Token: 0x0400003F RID: 63
		private string eventArgId;

		// Token: 0x04000040 RID: 64
		private SnapshotWriter snapshotWriter;

		// Token: 0x04000041 RID: 65
		private bool snapshotEnabled;

		// Token: 0x04000042 RID: 66
		private IExecutionControl session;

		// Token: 0x04000043 RID: 67
		private object hostState;

		// Token: 0x04000044 RID: 68
		private HybridDictionary handlers = new HybridDictionary();

		// Token: 0x04000045 RID: 69
		private MailItem mailItem;
	}
}
