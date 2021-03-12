using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000D9 RID: 217
	public class NotificationContext : DisposableBase
	{
		// Token: 0x06000899 RID: 2201 RVA: 0x000295CE File Offset: 0x000277CE
		public NotificationContext(INotificationSession session)
		{
			this.session = session;
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x000295DD File Offset: 0x000277DD
		// (set) Token: 0x0600089B RID: 2203 RVA: 0x000295E4 File Offset: 0x000277E4
		public static NotificationContext Current
		{
			get
			{
				return NotificationContext.currentContext;
			}
			set
			{
				NotificationContext.currentContext = value;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x000295EC File Offset: 0x000277EC
		public INotificationSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x000295F4 File Offset: 0x000277F4
		public bool HasPendingEvents
		{
			get
			{
				return this.eventQueue != null || this.dictionaryOfEventQueues != null;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x0002960C File Offset: 0x0002780C
		// (set) Token: 0x0600089F RID: 2207 RVA: 0x00029614 File Offset: 0x00027814
		private int VisitCookie { get; set; }

		// Token: 0x060008A0 RID: 2208 RVA: 0x00029620 File Offset: 0x00027820
		internal static void AssignCompletionPort(IoCompletionPort completionPort, uint completionKey)
		{
			using (LockManager.Lock(NotificationContext.pendingContextList))
			{
				if (completionPort != null && !completionPort.IsInvalid)
				{
					NotificationContext.completionPort = completionPort;
					NotificationContext.completionKey = completionKey;
				}
				else
				{
					NotificationContext.completionPort = null;
					NotificationContext.completionKey = 0U;
				}
			}
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00029680 File Offset: 0x00027880
		internal void EnqueueEvent(NotificationEvent nev)
		{
			using (LockManager.Lock(this, LockManager.LockType.NotificationContext))
			{
				Queue<NotificationEvent> queue;
				if (this.eventQueue == null && this.dictionaryOfEventQueues == null)
				{
					queue = (this.eventQueue = new Queue<NotificationEvent>(16));
				}
				else if (this.eventQueue != null)
				{
					NotificationEvent notificationEvent = this.eventQueue.Peek();
					if (nev.MailboxNumber == notificationEvent.MailboxNumber && nev.MdbGuid == notificationEvent.MdbGuid)
					{
						queue = this.eventQueue;
					}
					else
					{
						this.dictionaryOfEventQueues = new Dictionary<NotificationContext.MailboxIdentifier, Queue<NotificationEvent>>(8);
						this.dictionaryOfEventQueues.Add(new NotificationContext.MailboxIdentifier(notificationEvent.MdbGuid, notificationEvent.MailboxNumber), this.eventQueue);
						this.eventQueue = null;
						queue = new Queue<NotificationEvent>(16);
						this.dictionaryOfEventQueues.Add(new NotificationContext.MailboxIdentifier(nev.MdbGuid, nev.MailboxNumber), queue);
					}
				}
				else
				{
					NotificationContext.MailboxIdentifier key = new NotificationContext.MailboxIdentifier(nev.MdbGuid, nev.MailboxNumber);
					if (!this.dictionaryOfEventQueues.TryGetValue(key, out queue))
					{
						queue = new Queue<NotificationEvent>(16);
						this.dictionaryOfEventQueues.Add(key, queue);
					}
				}
				if (!queue.Contains(nev))
				{
					queue.Enqueue(nev);
					if (this.eventQueue != null && this.eventQueue.Count == 1)
					{
						using (LockManager.Lock(NotificationContext.pendingContextList))
						{
							this.pendingListNode = NotificationContext.pendingContextList.AddLast(this);
							this.VisitCookie = 0;
						}
					}
					using (LockManager.Lock(NotificationContext.pendingContextList))
					{
						if (NotificationContext.completionPort != null)
						{
							NotificationContext.completionPort.PostQueuedCompletionStatus(1U, NotificationContext.completionKey, IntPtr.Zero);
						}
					}
				}
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00029888 File Offset: 0x00027A88
		public NotificationEvent PeekEvent()
		{
			NotificationEvent result = null;
			using (LockManager.Lock(this, LockManager.LockType.NotificationContext))
			{
				if (this.eventQueue != null)
				{
					result = this.eventQueue.Peek();
				}
				else if (this.dictionaryOfEventQueues != null)
				{
					using (Dictionary<NotificationContext.MailboxIdentifier, Queue<NotificationEvent>>.Enumerator enumerator = this.dictionaryOfEventQueues.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							KeyValuePair<NotificationContext.MailboxIdentifier, Queue<NotificationEvent>> keyValuePair = enumerator.Current;
							result = keyValuePair.Value.Peek();
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0002992C File Offset: 0x00027B2C
		public NotificationEvent DequeueEvent(Guid mdbGuid, int mailboxNumber)
		{
			NotificationEvent result = null;
			using (LockManager.Lock(this, LockManager.LockType.NotificationContext))
			{
				if (this.eventQueue != null)
				{
					NotificationEvent notificationEvent = this.eventQueue.Peek();
					if (notificationEvent.MailboxNumber == mailboxNumber && notificationEvent.MdbGuid == mdbGuid)
					{
						result = this.eventQueue.Dequeue();
						if (this.eventQueue.Count == 0)
						{
							if (this.pendingListNode != null)
							{
								using (LockManager.Lock(NotificationContext.pendingContextList))
								{
									NotificationContext.pendingContextList.Remove(this.pendingListNode);
								}
								this.pendingListNode = null;
							}
							this.eventQueue = null;
						}
					}
				}
				else if (this.dictionaryOfEventQueues != null)
				{
					NotificationContext.MailboxIdentifier key = new NotificationContext.MailboxIdentifier(mdbGuid, mailboxNumber);
					Queue<NotificationEvent> queue;
					if (this.dictionaryOfEventQueues.TryGetValue(key, out queue))
					{
						result = queue.Dequeue();
						if (queue.Count == 0)
						{
							this.dictionaryOfEventQueues.Remove(key);
							if (this.dictionaryOfEventQueues.Count == 1)
							{
								using (Dictionary<NotificationContext.MailboxIdentifier, Queue<NotificationEvent>>.Enumerator enumerator = this.dictionaryOfEventQueues.GetEnumerator())
								{
									if (enumerator.MoveNext())
									{
										KeyValuePair<NotificationContext.MailboxIdentifier, Queue<NotificationEvent>> keyValuePair = enumerator.Current;
										this.eventQueue = keyValuePair.Value;
										this.dictionaryOfEventQueues = null;
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00029AD0 File Offset: 0x00027CD0
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<NotificationContext>(this);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00029AD8 File Offset: 0x00027CD8
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				using (LockManager.Lock(this, LockManager.LockType.NotificationContext))
				{
					if (this.pendingListNode != null)
					{
						if (this.eventQueue != null)
						{
							this.eventQueue.Clear();
							this.eventQueue = null;
						}
						else if (this.dictionaryOfEventQueues != null)
						{
							this.dictionaryOfEventQueues.Clear();
							this.dictionaryOfEventQueues = null;
						}
						using (LockManager.Lock(NotificationContext.pendingContextList))
						{
							NotificationContext.pendingContextList.Remove(this.pendingListNode);
						}
						this.pendingListNode = null;
					}
					this.session = null;
				}
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00029B98 File Offset: 0x00027D98
		public static NotificationContext GetNextUnvisitedPendingContext(int visitCookie)
		{
			NotificationContext notificationContext = null;
			using (LockManager.Lock(NotificationContext.pendingContextList))
			{
				LinkedListNode<NotificationContext> first = NotificationContext.pendingContextList.First;
				if (first != null)
				{
					notificationContext = first.Value;
					if (notificationContext.VisitCookie == visitCookie)
					{
						notificationContext = null;
					}
					else
					{
						notificationContext.VisitCookie = visitCookie;
						NotificationContext.pendingContextList.RemoveFirst();
						NotificationContext.pendingContextList.AddLast(first);
					}
				}
			}
			return notificationContext;
		}

		// Token: 0x040004EF RID: 1263
		private const int AvgEventsQueuedPerNotificationContext = 16;

		// Token: 0x040004F0 RID: 1264
		private const int AvgMailboxesPerNotificationContext = 8;

		// Token: 0x040004F1 RID: 1265
		[ThreadStatic]
		private static NotificationContext currentContext;

		// Token: 0x040004F2 RID: 1266
		private static LinkedList<NotificationContext> pendingContextList = new LinkedList<NotificationContext>();

		// Token: 0x040004F3 RID: 1267
		private static IoCompletionPort completionPort = null;

		// Token: 0x040004F4 RID: 1268
		private static uint completionKey = 0U;

		// Token: 0x040004F5 RID: 1269
		private INotificationSession session;

		// Token: 0x040004F6 RID: 1270
		private Queue<NotificationEvent> eventQueue;

		// Token: 0x040004F7 RID: 1271
		private Dictionary<NotificationContext.MailboxIdentifier, Queue<NotificationEvent>> dictionaryOfEventQueues;

		// Token: 0x040004F8 RID: 1272
		private LinkedListNode<NotificationContext> pendingListNode;

		// Token: 0x020000DA RID: 218
		private struct MailboxIdentifier : IEquatable<NotificationContext.MailboxIdentifier>
		{
			// Token: 0x060008A8 RID: 2216 RVA: 0x00029C2C File Offset: 0x00027E2C
			internal MailboxIdentifier(Guid databaseGuid, int mailboxNumber)
			{
				this.databaseGuid = databaseGuid;
				this.mailboxNumber = mailboxNumber;
			}

			// Token: 0x1700023C RID: 572
			// (get) Token: 0x060008A9 RID: 2217 RVA: 0x00029C3C File Offset: 0x00027E3C
			public Guid DatabaseGuid
			{
				get
				{
					return this.databaseGuid;
				}
			}

			// Token: 0x1700023D RID: 573
			// (get) Token: 0x060008AA RID: 2218 RVA: 0x00029C44 File Offset: 0x00027E44
			public int MailboxNumber
			{
				get
				{
					return this.mailboxNumber;
				}
			}

			// Token: 0x060008AB RID: 2219 RVA: 0x00029C4C File Offset: 0x00027E4C
			public override int GetHashCode()
			{
				return this.mailboxNumber;
			}

			// Token: 0x060008AC RID: 2220 RVA: 0x00029C54 File Offset: 0x00027E54
			public override bool Equals(object obj)
			{
				return obj is NotificationContext.MailboxIdentifier && this.Equals((NotificationContext.MailboxIdentifier)obj);
			}

			// Token: 0x060008AD RID: 2221 RVA: 0x00029C6C File Offset: 0x00027E6C
			public bool Equals(NotificationContext.MailboxIdentifier compare)
			{
				return this.mailboxNumber == compare.mailboxNumber && this.databaseGuid == compare.databaseGuid;
			}

			// Token: 0x040004FA RID: 1274
			private Guid databaseGuid;

			// Token: 0x040004FB RID: 1275
			private int mailboxNumber;
		}
	}
}
