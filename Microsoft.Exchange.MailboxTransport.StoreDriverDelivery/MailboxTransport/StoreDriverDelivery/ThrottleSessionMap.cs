using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000035 RID: 53
	internal class ThrottleSessionMap
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000C282 File Offset: 0x0000A482
		// (set) Token: 0x06000273 RID: 627 RVA: 0x0000C28A File Offset: 0x0000A48A
		private protected ReaderWriterLockSlim RwLock { protected get; private set; }

		// Token: 0x06000274 RID: 628 RVA: 0x0000C293 File Offset: 0x0000A493
		internal ThrottleSessionMap()
		{
			this.session = new Dictionary<long, ThrottleSession>();
			this.RwLock = new ReaderWriterLockSlim();
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000C2B4 File Offset: 0x0000A4B4
		internal ThrottleSession TryGetSession(long sessionId)
		{
			this.RwLock.EnterReadLock();
			ThrottleSession result;
			try
			{
				ThrottleSession throttleSession;
				if (this.session.TryGetValue(sessionId, out throttleSession))
				{
					result = throttleSession;
				}
				else
				{
					result = null;
				}
			}
			finally
			{
				this.RwLock.ExitReadLock();
			}
			return result;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000C304 File Offset: 0x0000A504
		internal void AddSession(long sessionId)
		{
			this.RwLock.EnterUpgradeableReadLock();
			try
			{
				if (this.session.ContainsKey(sessionId))
				{
					throw new ArgumentException(string.Format("Failed to add session. Session Id={0} already exist.", sessionId));
				}
				this.RwLock.EnterWriteLock();
				try
				{
					this.session.Add(sessionId, new ThrottleSession(sessionId));
				}
				finally
				{
					this.RwLock.ExitWriteLock();
				}
			}
			finally
			{
				this.RwLock.ExitUpgradeableReadLock();
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000C398 File Offset: 0x0000A598
		internal void RemoveSession(long sessionId)
		{
			this.RwLock.EnterWriteLock();
			try
			{
				this.session.Remove(sessionId);
			}
			finally
			{
				this.RwLock.ExitWriteLock();
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000C3DC File Offset: 0x0000A5DC
		internal void SetMdb(long sessionId, Guid mdbGuid)
		{
			this.RwLock.EnterUpgradeableReadLock();
			try
			{
				if (!this.session.ContainsKey(sessionId))
				{
					throw new ArgumentException(string.Format("Failed to set Mdb. Session Id={0} not found.", sessionId));
				}
				if (this.session[sessionId].Mdb != null)
				{
					throw new ArgumentException(string.Format("Failed to set Mdb for SessionId={0}: Mdb is already set for current session.", sessionId));
				}
				this.RwLock.EnterWriteLock();
				try
				{
					this.session[sessionId].Mdb = new Guid?(mdbGuid);
				}
				finally
				{
					this.RwLock.ExitWriteLock();
				}
			}
			finally
			{
				this.RwLock.ExitUpgradeableReadLock();
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000C4A4 File Offset: 0x0000A6A4
		internal void AddRecipient(long sessionId, RoutingAddress address)
		{
			this.RwLock.EnterUpgradeableReadLock();
			try
			{
				if (!this.session.ContainsKey(sessionId))
				{
					throw new ArgumentException(string.Format("Failed to add recipient. Session Id={0} not found.", sessionId));
				}
				this.RwLock.EnterWriteLock();
				try
				{
					int num = 0;
					if (this.session[sessionId].Recipients.ContainsKey(address))
					{
						num = this.session[sessionId].Recipients[address];
					}
					this.session[sessionId].Recipients[address] = num + 1;
				}
				finally
				{
					this.RwLock.ExitWriteLock();
				}
			}
			finally
			{
				this.RwLock.ExitUpgradeableReadLock();
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000C574 File Offset: 0x0000A774
		internal void RemoveRecipient(long sessionId, RoutingAddress address)
		{
			this.RwLock.EnterUpgradeableReadLock();
			try
			{
				if (this.session.Count != 0)
				{
					if (!this.session.ContainsKey(sessionId))
					{
						throw new ArgumentException(string.Format("Failed to remove recipient. Session Id={0} not found.", sessionId));
					}
					this.RwLock.EnterWriteLock();
					try
					{
						int num = this.session[sessionId].Recipients[address];
						if (num == 1)
						{
							this.session[sessionId].Recipients.Remove(address);
						}
						else
						{
							this.session[sessionId].Recipients[address] = num - 1;
						}
					}
					finally
					{
						this.RwLock.ExitWriteLock();
					}
				}
			}
			finally
			{
				this.RwLock.ExitUpgradeableReadLock();
			}
		}

		// Token: 0x04000111 RID: 273
		private Dictionary<long, ThrottleSession> session;
	}
}
