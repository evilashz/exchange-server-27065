﻿using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Manager.Throttling;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000026 RID: 38
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DatabasePicker
	{
		// Token: 0x0600022C RID: 556 RVA: 0x0000EB34 File Offset: 0x0000CD34
		internal DatabasePicker(ISyncManagerConfiguration configuration, SyncLogSession syncLogSession, IDispatchEntryManager dispatchEntryManager, IHealthLogDispatchEntryReporter healthLogDispatchEntryReporter, SyncQueueManager syncQueueManager)
		{
			SyncUtilities.ThrowIfArgumentNull("configuration", configuration);
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			SyncUtilities.ThrowIfArgumentNull("syncQueueManager", syncQueueManager);
			this.syncLogSession = syncLogSession;
			this.databaseUnhealthyBackoffTime = configuration.DatabaseBackoffTime;
			this.maxDispatchedPerDB = configuration.MaxSyncsPerDB;
			this.refreshFrequency = configuration.DispatcherDatabaseRefreshFrequency;
			this.dispatchEntryManager = dispatchEntryManager;
			this.healthLogDispatchEntryReporter = healthLogDispatchEntryReporter;
			this.syncQueueManager = syncQueueManager;
			this.backedOffDatabases = new Dictionary<Guid, ExDateTime>();
			this.databaseGuids = new LinkedList<Guid>();
			this.lastProcessedNode = null;
			this.lastRefreshTime = ExDateTime.MinValue;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000EBE0 File Offset: 0x0000CDE0
		internal bool TryGetNextDatabaseToDispatchFrom(DispatchTrigger dispatchTrigger, out Guid databaseGuid, out IList<WorkType> workTypes)
		{
			databaseGuid = Guid.Empty;
			workTypes = null;
			bool result;
			lock (this.syncObject)
			{
				this.RefreshDatabasesList();
				if (this.databaseGuids.Count == 0)
				{
					this.syncLogSession.LogVerbose((TSLID)352UL, "DatabasePicker.TryGetNextDatabaseToDispatchFrom: There are no DBs to dispatch.", new object[0]);
					result = false;
				}
				else
				{
					for (int i = 0; i < this.databaseGuids.Count; i++)
					{
						LinkedListNode<Guid> linkedListNode;
						if (this.lastProcessedNode == null || this.lastProcessedNode.Next == null)
						{
							linkedListNode = this.databaseGuids.First;
						}
						else
						{
							linkedListNode = this.lastProcessedNode.Next;
						}
						this.lastProcessedNode = linkedListNode;
						Guid value = linkedListNode.Value;
						if (this.CanAttemptDispatchForDatabase(value, dispatchTrigger) && this.DoesDatabaseHaveWork(value, out workTypes))
						{
							databaseGuid = value;
							this.syncLogSession.LogDebugging((TSLID)1245UL, "DatabasePicker.TryGetNextDatabaseToDispatchFrom: Attempting Dispatch from DB: {0}.", new object[]
							{
								databaseGuid
							});
							return true;
						}
					}
					this.syncLogSession.LogVerbose((TSLID)1442UL, "DatabasePicker.TryGetNextDatabaseToDispatchFrom: All DBs are in backed off state.", new object[0]);
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000ED48 File Offset: 0x0000CF48
		internal void MarkDispatchCompleted(Guid databaseGuid, DispatchResult dispatchResult)
		{
			lock (this.syncObject)
			{
				bool flag2 = dispatchResult == DispatchResult.Success || (dispatchResult & DispatchResult.DatabaseLosesItsTurnAtTransientFailure) == DispatchResult.DatabaseLosesItsTurnAtTransientFailure;
				if (flag2)
				{
					if (dispatchResult == DispatchResult.DatabaseRpcLatencyUnhealthy || dispatchResult == DispatchResult.MailboxServerHAUnhealthy || dispatchResult == DispatchResult.DatabaseOverloaded)
					{
						this.BackoffDatabase(databaseGuid);
					}
				}
				else
				{
					this.syncLogSession.LogDebugging((TSLID)1526UL, "DatabasePicker: DB {0} being added to front of DB list.", new object[]
					{
						databaseGuid
					});
					if (this.databaseGuids.Contains(databaseGuid))
					{
						if (this.lastProcessedNode == null)
						{
							this.databaseGuids.Remove(databaseGuid);
							this.databaseGuids.AddFirst(databaseGuid);
						}
						else if (object.Equals(this.lastProcessedNode.Value, databaseGuid))
						{
							this.lastProcessedNode = this.lastProcessedNode.Previous;
						}
						else
						{
							this.databaseGuids.Remove(databaseGuid);
							this.databaseGuids.AddAfter(this.lastProcessedNode, databaseGuid);
						}
					}
				}
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000EE6C File Offset: 0x0000D06C
		internal void AddDiagnosticInfoTo(XElement parentElement)
		{
			XElement xelement = new XElement("DatabasePicker");
			XElement xelement2 = new XElement("DatabasesList");
			XElement xelement3 = new XElement("BackedOffDatabases");
			lock (this.syncObject)
			{
				xelement.Add(new XElement("lastProcessedDB", (this.lastProcessedNode == null) ? string.Empty : this.lastProcessedNode.Value.ToString()));
				foreach (Guid guid in this.databaseGuids)
				{
					xelement2.Add(new XElement("databaseId", guid));
				}
				foreach (Guid guid2 in this.backedOffDatabases.Keys)
				{
					XElement xelement4 = new XElement("Database");
					xelement4.Add(new XElement("databaseId", guid2));
					xelement4.Add(new XElement("backoffExpireTime", this.backedOffDatabases[guid2].ToString("o")));
					xelement3.Add(xelement4);
				}
			}
			xelement.Add(xelement2);
			xelement.Add(xelement3);
			parentElement.Add(xelement);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000F054 File Offset: 0x0000D254
		protected virtual ExDateTime GetCurrentTime()
		{
			return ExDateTime.UtcNow;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000F05C File Offset: 0x0000D25C
		private bool CanAttemptDispatchForDatabase(Guid databaseGuid, DispatchTrigger dispatchTrigger)
		{
			ExDateTime t;
			if (this.backedOffDatabases.TryGetValue(databaseGuid, out t))
			{
				ExDateTime currentTime = this.GetCurrentTime();
				if (currentTime < t)
				{
					this.syncLogSession.LogVerbose((TSLID)345UL, "DatabasePicker.CanAttemptDispatchForDatabase DB in backoff {0}", new object[]
					{
						databaseGuid
					});
					return false;
				}
				this.syncLogSession.LogVerbose((TSLID)346UL, "DatabasePicker.CanAttemptDispatchForDatabase DB no longer in backoff {0}", new object[]
				{
					databaseGuid
				});
				this.backedOffDatabases.Remove(databaseGuid);
			}
			if (this.maxDispatchedPerDB <= this.dispatchEntryManager.GetNumberOfEntriesForDatabase(databaseGuid))
			{
				this.syncLogSession.LogDebugging((TSLID)350UL, "DWC.CanAttemptDispatchForDatabase Max number of subscriptions dispatched for DB {0}", new object[]
				{
					databaseGuid
				});
				this.healthLogDispatchEntryReporter.ReportDispatchAttempt(null, dispatchTrigger, null, DispatchResult.MaxSyncsPerDatabase, null, null);
				return false;
			}
			return true;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000F160 File Offset: 0x0000D360
		private bool DoesDatabaseHaveWork(Guid databaseGuid, out IList<WorkType> workTypes)
		{
			ExDateTime currentTime = this.GetCurrentTime();
			workTypes = this.syncQueueManager.GetDueWorkTypesByNextPollingTime(databaseGuid, currentTime);
			if (workTypes == null || workTypes.Count == 0)
			{
				this.syncLogSession.LogDebugging((TSLID)197UL, "DM.DispatchSubscriptionForDatabase. No work due for DB {0}", new object[]
				{
					databaseGuid
				});
				return false;
			}
			return true;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000F1C0 File Offset: 0x0000D3C0
		private void BackoffDatabase(Guid databaseGuid)
		{
			if (!this.backedOffDatabases.ContainsKey(databaseGuid))
			{
				ExDateTime currentTime = this.GetCurrentTime();
				ExDateTime exDateTime = currentTime + this.databaseUnhealthyBackoffTime;
				this.backedOffDatabases.Add(databaseGuid, exDateTime);
				this.syncLogSession.LogDebugging((TSLID)343UL, "DatabasePicker: DB {0} going into backoff until {1}.", new object[]
				{
					databaseGuid,
					exDateTime
				});
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000F234 File Offset: 0x0000D434
		private void RefreshDatabasesList()
		{
			ExDateTime currentTime = this.GetCurrentTime();
			if (currentTime - this.lastRefreshTime <= this.refreshFrequency)
			{
				this.syncLogSession.LogDebugging((TSLID)1540UL, "DatabasePicker.RefreshDatabasesList: Skipping CurrentTime: {0}, LastRefreshTime: {1}, RefreshFrequency: {2}.", new object[]
				{
					currentTime,
					this.lastRefreshTime,
					this.refreshFrequency
				});
				return;
			}
			this.syncLogSession.LogDebugging((TSLID)1541UL, "DatabasePicker.RefreshDatabasesList: Enter. DB Count: {0} LastRefreshTime: {1}", new object[]
			{
				this.databaseGuids.Count,
				this.lastRefreshTime
			});
			Guid[] listOfDatabases = this.syncQueueManager.GetListOfDatabases();
			LinkedList<Guid> linkedList = new LinkedList<Guid>(listOfDatabases);
			for (LinkedListNode<Guid> linkedListNode = this.databaseGuids.Last; linkedListNode != null; linkedListNode = linkedListNode.Previous)
			{
				if (linkedList.Remove(linkedListNode.Value))
				{
					linkedList.AddFirst(linkedListNode.Value);
				}
			}
			if (this.lastProcessedNode != null)
			{
				LinkedListNode<Guid> next = this.lastProcessedNode;
				this.lastProcessedNode = linkedList.Find(next.Value);
				if (this.lastProcessedNode == null)
				{
					for (next = next.Next; next != null; next = next.Next)
					{
						this.lastProcessedNode = linkedList.Find(next.Value);
						if (this.lastProcessedNode != null)
						{
							this.lastProcessedNode = this.lastProcessedNode.Previous;
							break;
						}
					}
				}
			}
			this.databaseGuids = linkedList;
			this.lastRefreshTime = currentTime;
			this.syncLogSession.LogDebugging((TSLID)1542UL, "DatabasePicker.RefreshDatabasesList: Exit. DB Count: {0} LastRefreshTime: {1}", new object[]
			{
				this.databaseGuids.Count,
				this.lastRefreshTime
			});
		}

		// Token: 0x04000134 RID: 308
		private readonly object syncObject = new object();

		// Token: 0x04000135 RID: 309
		private readonly SyncLogSession syncLogSession;

		// Token: 0x04000136 RID: 310
		private readonly TimeSpan databaseUnhealthyBackoffTime;

		// Token: 0x04000137 RID: 311
		private readonly int maxDispatchedPerDB;

		// Token: 0x04000138 RID: 312
		private readonly TimeSpan refreshFrequency;

		// Token: 0x04000139 RID: 313
		private readonly IDispatchEntryManager dispatchEntryManager;

		// Token: 0x0400013A RID: 314
		private readonly IHealthLogDispatchEntryReporter healthLogDispatchEntryReporter;

		// Token: 0x0400013B RID: 315
		private readonly SyncQueueManager syncQueueManager;

		// Token: 0x0400013C RID: 316
		private readonly Dictionary<Guid, ExDateTime> backedOffDatabases;

		// Token: 0x0400013D RID: 317
		private LinkedList<Guid> databaseGuids;

		// Token: 0x0400013E RID: 318
		private LinkedListNode<Guid> lastProcessedNode;

		// Token: 0x0400013F RID: 319
		private ExDateTime lastRefreshTime;
	}
}
