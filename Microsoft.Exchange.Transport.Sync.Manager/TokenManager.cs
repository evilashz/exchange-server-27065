using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000013 RID: 19
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class TokenManager
	{
		// Token: 0x060000BC RID: 188 RVA: 0x0000716C File Offset: 0x0000536C
		internal TokenManager(int capacity, int maxCapacity)
		{
			this.capacity = capacity;
			this.syncLogSession = this.GetSyncLogSession();
			this.userMailboxAffinity = new Dictionary<Guid, Token>(this.capacity);
			this.userMailboxWorkers = new Dictionary<Guid, Queue<PoolItem<ManualResetEvent>>>(this.capacity);
			this.eventsPool = this.CreateManualResetEventPool(this.capacity, maxCapacity);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000071D2 File Offset: 0x000053D2
		internal void Shutdown()
		{
			this.eventsPool.Shutdown();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000071E0 File Offset: 0x000053E0
		internal void ReleaseToken(Guid mailboxGuid, Token? token)
		{
			lock (this.tokenLock)
			{
				this.userMailboxAffinity.Remove(mailboxGuid);
				if (this.userMailboxWorkers.ContainsKey(mailboxGuid))
				{
					Queue<PoolItem<ManualResetEvent>> queue = this.userMailboxWorkers[mailboxGuid];
					PoolItem<ManualResetEvent> poolItem = queue.Dequeue();
					int num = 0;
					while (num < 2 && !poolItem.Item.Set())
					{
						this.syncLogSession.LogError((TSLID)218UL, TokenManager.diag, (long)this.GetHashCode(), Guid.Empty, mailboxGuid, "Failed to notify other threads waiting on release of this token.", new object[0]);
						Thread.Sleep(TimeSpan.FromSeconds(1.0));
						num++;
					}
					if (queue.Count == 0)
					{
						this.userMailboxWorkers.Remove(mailboxGuid);
					}
				}
			}
			this.syncLogSession.LogDebugging((TSLID)164UL, TokenManager.diag, (long)this.GetHashCode(), Guid.Empty, mailboxGuid, "Token {0} released.", new object[]
			{
				token
			});
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00007314 File Offset: 0x00005514
		internal Token? GetToken(Guid mailboxGuid)
		{
			ExDateTime utcNow = ExDateTime.UtcNow;
			Token? result;
			for (;;)
			{
				PoolItem<ManualResetEvent> item;
				lock (this.tokenLock)
				{
					Token? token;
					if (this.TryGetToken(mailboxGuid, out token))
					{
						ExDateTime utcNow2 = ExDateTime.UtcNow;
						this.SetTimeTakenToGetToken(ExDateTime.TimeDiff(utcNow2, utcNow));
						result = token;
						break;
					}
					bool flag2 = false;
					item = this.eventsPool.GetItem(out flag2);
					if (item == null)
					{
						this.syncLogSession.LogError((TSLID)165UL, TokenManager.diag, (long)this.GetHashCode(), Guid.Empty, mailboxGuid, "Failed to get a manual reset event, so we're failing to get the token.", new object[0]);
						result = null;
						break;
					}
					if (!this.userMailboxWorkers.ContainsKey(mailboxGuid))
					{
						this.userMailboxWorkers[mailboxGuid] = new Queue<PoolItem<ManualResetEvent>>(this.capacity);
					}
					this.userMailboxWorkers[mailboxGuid].Enqueue(item);
				}
				this.syncLogSession.LogVerbose((TSLID)166UL, TokenManager.diag, (long)this.GetHashCode(), Guid.Empty, mailboxGuid, "Waiting on token.", new object[0]);
				try
				{
					if (item.Item.WaitOne(ContentAggregationConfig.TokenWaitTimeOutInterval, false))
					{
						continue;
					}
					this.syncLogSession.LogError((TSLID)167UL, TokenManager.diag, (long)this.GetHashCode(), Guid.Empty, mailboxGuid, "Token wait has timed out. Will fail token operation.", new object[0]);
					StackTrace stackTrace = new StackTrace();
					ContentAggregationConfig.EventLogger.LogEvent(TransportSyncManagerEventLogConstants.Tuple_SyncManagerTokenWaitTimedout, null, new object[]
					{
						mailboxGuid,
						ContentAggregationConfig.TokenWaitTimeOutInterval.TotalMinutes,
						stackTrace.ToString()
					});
					result = null;
				}
				finally
				{
					bool flag3 = item.Item.Reset();
					if (!flag3)
					{
						this.syncLogSession.LogError((TSLID)219UL, TokenManager.diag, (long)this.GetHashCode(), Guid.Empty, mailboxGuid, "Failed to reset manual reset event to false state, hence disposing it and NOT reusing it.", new object[0]);
					}
					bool reuse = flag3;
					this.eventsPool.ReturnItem(item, reuse);
				}
				break;
			}
			return result;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00007564 File Offset: 0x00005764
		internal bool TryGetToken(Guid mailboxGuid, out Token? token)
		{
			token = null;
			lock (this.tokenLock)
			{
				if (!this.userMailboxAffinity.ContainsKey(mailboxGuid))
				{
					token = new Token?(this.userMailboxAffinity[mailboxGuid] = new Token(Guid.NewGuid()));
				}
			}
			if (token != null)
			{
				this.syncLogSession.LogDebugging((TSLID)168UL, TokenManager.diag, (long)this.GetHashCode(), Guid.Empty, mailboxGuid, "Token {0} issued.", new object[]
				{
					token
				});
				return true;
			}
			this.syncLogSession.LogDebugging((TSLID)169UL, TokenManager.diag, (long)this.GetHashCode(), Guid.Empty, mailboxGuid, "Token was NOT issued.", new object[0]);
			return false;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00007664 File Offset: 0x00005864
		protected virtual GlobalSyncLogSession GetSyncLogSession()
		{
			return ContentAggregationConfig.SyncLogSession;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000766B File Offset: 0x0000586B
		protected virtual void SetTimeTakenToGetToken(TimeSpan timeTakenToGetToken)
		{
			ManagerPerfCounterHandler.Instance.SetWaitToGetSubscriptionsCacheToken((long)timeTakenToGetToken.TotalMilliseconds);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000767F File Offset: 0x0000587F
		protected virtual ManualResetEventPool CreateManualResetEventPool(int capacity, int maxCapacity)
		{
			return new ManualResetEventPool(this.capacity, maxCapacity);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00007690 File Offset: 0x00005890
		[Conditional("DEBUG")]
		private void ValidateToken(Guid mailboxGuid, Token? token)
		{
			if (token == null)
			{
				throw new InvalidOperationException("Token is null.");
			}
			lock (this.tokenLock)
			{
				if (!this.userMailboxAffinity.ContainsKey(mailboxGuid))
				{
					throw new InvalidOperationException("No token ever issued for this userMailbox: " + mailboxGuid);
				}
				if (this.userMailboxAffinity[mailboxGuid] != token)
				{
					throw new InvalidOperationException("Invalid Token for this userMailbox: " + mailboxGuid);
				}
			}
			this.syncLogSession.LogDebugging((TSLID)170UL, TokenManager.diag, (long)this.GetHashCode(), Guid.Empty, mailboxGuid, "Token {0} validated.", new object[]
			{
				token
			});
		}

		// Token: 0x0400005B RID: 91
		private static readonly Microsoft.Exchange.Diagnostics.Trace diag = ExTraceGlobals.TokenManagerTracer;

		// Token: 0x0400005C RID: 92
		private readonly object tokenLock = new object();

		// Token: 0x0400005D RID: 93
		private readonly int capacity;

		// Token: 0x0400005E RID: 94
		private readonly Dictionary<Guid, Token> userMailboxAffinity;

		// Token: 0x0400005F RID: 95
		private readonly Dictionary<Guid, Queue<PoolItem<ManualResetEvent>>> userMailboxWorkers;

		// Token: 0x04000060 RID: 96
		private readonly GlobalSyncLogSession syncLogSession;

		// Token: 0x04000061 RID: 97
		private ManualResetEventPool eventsPool;
	}
}
