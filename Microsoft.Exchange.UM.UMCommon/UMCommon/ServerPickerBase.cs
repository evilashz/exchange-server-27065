using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200002D RID: 45
	internal abstract class ServerPickerBase<ManagedObjectType, ContextObjectType> : IServerPicker<ManagedObjectType, ContextObjectType> where ManagedObjectType : class
	{
		// Token: 0x06000284 RID: 644 RVA: 0x0000A0D4 File Offset: 0x000082D4
		protected ServerPickerBase() : this(ServerPickerBase<ManagedObjectType, ContextObjectType>.DefaultRetryInterval, ServerPickerBase<ManagedObjectType, ContextObjectType>.DefaultRefreshInterval, ServerPickerBase<ManagedObjectType, ContextObjectType>.DefaultRefreshIntervalOnFailure, ExTraceGlobals.UtilTracer)
		{
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000A0F0 File Offset: 0x000082F0
		protected ServerPickerBase(TimeSpan retryInterval, TimeSpan refreshInterval, TimeSpan refreshIntervalOnFailure, Trace tracer)
		{
			this.tracer = tracer;
			this.refreshInterval = refreshInterval;
			this.refreshIntervalOnFailure = refreshIntervalOnFailure;
			this.servers = new List<ManagedObjectType>();
			this.unavailableServers = new RetryQueue<ManagedObjectType>(tracer, retryInterval);
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000A148 File Offset: 0x00008348
		// (set) Token: 0x06000287 RID: 647 RVA: 0x0000A150 File Offset: 0x00008350
		internal ServerPickerBase<ManagedObjectType, ContextObjectType>.LoadConfigurationDelegate InternalLoadConfigurationDelegate
		{
			get
			{
				return this.loadConfigurationDelegate;
			}
			set
			{
				this.loadConfigurationDelegate = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000A159 File Offset: 0x00008359
		// (set) Token: 0x06000289 RID: 649 RVA: 0x0000A161 File Offset: 0x00008361
		internal ServerPickerBase<ManagedObjectType, ContextObjectType>.IsHealthyDelegate InternalIsHealthyDelegate
		{
			get
			{
				return this.healthyDelegate;
			}
			set
			{
				this.healthyDelegate = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000A16A File Offset: 0x0000836A
		// (set) Token: 0x0600028B RID: 651 RVA: 0x0000A172 File Offset: 0x00008372
		internal ServerPickerBase<ManagedObjectType, ContextObjectType>.IsValidDelegate InternalIsValidDelegate
		{
			get
			{
				return this.validDelegate;
			}
			set
			{
				this.validDelegate = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000A17B File Offset: 0x0000837B
		protected Trace Tracer
		{
			get
			{
				return this.tracer;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000A183 File Offset: 0x00008383
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000A190 File Offset: 0x00008390
		protected TimeSpan RetryInterval
		{
			get
			{
				return this.unavailableServers.RetryInterval;
			}
			set
			{
				this.unavailableServers.RetryInterval = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000A19E File Offset: 0x0000839E
		private bool TimeToRefresh
		{
			get
			{
				return ExDateTime.Compare(ExDateTime.UtcNow, this.nextRefresh) >= 0;
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000A1B8 File Offset: 0x000083B8
		public List<ManagedObjectType> GetRegisteredUMServers()
		{
			lock (this.lockObject)
			{
				if (this.TimeToRefresh)
				{
					this.RefreshServers();
				}
			}
			return this.servers;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000A208 File Offset: 0x00008408
		public virtual ManagedObjectType PickNextServer(ContextObjectType context)
		{
			int num = 0;
			return this.PickNextServer(context, out num);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000A220 File Offset: 0x00008420
		public virtual ManagedObjectType PickNextServer(ContextObjectType context, out int totalServers)
		{
			totalServers = 0;
			List<ManagedObjectType> list = new List<ManagedObjectType>();
			List<ManagedObjectType> list2 = new List<ManagedObjectType>();
			ManagedObjectType managedObjectType = default(ManagedObjectType);
			lock (this.lockObject)
			{
				if (this.TimeToRefresh)
				{
					this.RefreshServers();
				}
				this.UpdateRetryQueue();
				totalServers = this.servers.Count + this.unavailableServers.Count;
				if (this.servers.Count == 0)
				{
					CallIdTracer.TraceError(this.Tracer, this.GetHashCode(), "ServerPickerBase::PickNextServer() No servers found", new object[0]);
					return default(ManagedObjectType);
				}
				if (this.currentServerIndex >= this.servers.Count)
				{
					this.currentServerIndex = 0;
				}
				for (int i = 0; i < this.servers.Count; i++)
				{
					list.Add(this.servers[(this.currentServerIndex + i) % this.servers.Count]);
				}
				this.unavailableServers.CopyTo(list2);
				this.currentServerIndex++;
			}
			for (int j = 0; j < list.Count; j++)
			{
				managedObjectType = list[j];
				if (this.CanUse(context, managedObjectType))
				{
					return managedObjectType;
				}
			}
			for (int k = 0; k < list2.Count; k++)
			{
				managedObjectType = list2[k];
				if (this.CanUse(context, managedObjectType))
				{
					return managedObjectType;
				}
			}
			return default(ManagedObjectType);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000A3B4 File Offset: 0x000085B4
		public virtual ManagedObjectType PickNextServer(ContextObjectType context, Guid tenantGuid, out int totalServers)
		{
			return this.PickNextServer(context, out totalServers);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000A3C0 File Offset: 0x000085C0
		public virtual void ServerUnavailable(ManagedObjectType server)
		{
			CallIdTracer.TraceDebug(this.Tracer, this.GetHashCode(), "ServerPickerBase::ServerUnavailable {0}", new object[]
			{
				server
			});
			lock (this.lockObject)
			{
				if (this.servers.Remove(server))
				{
					this.unavailableServers.Enqueue(server);
					CallIdTracer.TraceDebug(this.Tracer, this.GetHashCode(), "Server {0} being added to unavailable list", new object[]
					{
						server
					});
				}
				if (this.servers.Count == 0)
				{
					CallIdTracer.TraceDebug(this.Tracer, this.GetHashCode(), "We don't have any more good servers. Trying to promote a bad server now...", new object[0]);
					ManagedObjectType managedObjectType = this.unavailableServers.Dequeue(true);
					if (managedObjectType != null)
					{
						CallIdTracer.TraceDebug(this.Tracer, this.GetHashCode(), "Server {0} being promoted from Unavailable to Active because we don't have any other good servers!", new object[]
						{
							managedObjectType
						});
						this.servers.Add(managedObjectType);
					}
				}
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000A4F0 File Offset: 0x000086F0
		internal void ServerInvalid(ManagedObjectType server)
		{
			CallIdTracer.TraceDebug(this.Tracer, this.GetHashCode(), "ServerPiclerBase::ServerInvalid {0}", new object[]
			{
				server
			});
			lock (this.lockObject)
			{
				this.servers.Remove(server);
				this.unavailableServers.Remove(server);
			}
		}

		// Token: 0x06000296 RID: 662
		protected abstract List<ManagedObjectType> LoadConfiguration();

		// Token: 0x06000297 RID: 663 RVA: 0x0000A570 File Offset: 0x00008770
		protected virtual bool IsHealthy(ContextObjectType context, ManagedObjectType item)
		{
			return true;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000A573 File Offset: 0x00008773
		protected virtual bool IsValid(ContextObjectType context, ManagedObjectType candidate)
		{
			return true;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000A578 File Offset: 0x00008778
		protected bool ServerInRetry(ManagedObjectType server)
		{
			CallIdTracer.TraceDebug(this.Tracer, this.GetHashCode(), "ServerPickerBase::ServerInRetry Server = {0}", new object[]
			{
				server
			});
			lock (this.lockObject)
			{
				if (this.unavailableServers.Contains(server))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000A5F4 File Offset: 0x000087F4
		protected bool RemoveServer(ManagedObjectType server)
		{
			CallIdTracer.TraceDebug(this.Tracer, this.GetHashCode(), "ServerPickerBase::RemoveServer Server = {0}", new object[]
			{
				server
			});
			bool result = false;
			lock (this.lockObject)
			{
				result = this.servers.Remove(server);
			}
			return result;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000A66C File Offset: 0x0000886C
		protected void RefreshServers()
		{
			Exception ex = null;
			CallIdTracer.TraceDebug(this.Tracer, this.GetHashCode(), "ServerPickerBase::RefreshServers", new object[0]);
			lock (this.lockObject)
			{
				CallIdTracer.TraceDebug(this.Tracer, this.GetHashCode(), "ServerPickerBase::RefreshServers. TimeToRefresh", new object[0]);
				try
				{
					this.nextRefresh = ExDateTime.UtcNow.Add(this.refreshInterval);
					List<ManagedObjectType> list = this.InternalLoadConfiguration();
					this.unavailableServers.DeleteInvalid(list);
					List<ManagedObjectType> list2 = new List<ManagedObjectType>();
					foreach (ManagedObjectType managedObjectType in list)
					{
						if (!this.ServerInRetry(managedObjectType))
						{
							CallIdTracer.TraceDebug(this.Tracer, this.GetHashCode(), "Adding {0} to the list", new object[]
							{
								managedObjectType
							});
							list2.Add(managedObjectType);
						}
						else
						{
							CallIdTracer.TraceDebug(this.Tracer, this.GetHashCode(), "{0} is in retry, skipping", new object[]
							{
								managedObjectType
							});
						}
					}
					this.servers = list2;
				}
				catch (ADTransientException ex2)
				{
					ex = ex2;
				}
				catch (ADOperationException ex3)
				{
					ex = ex3;
				}
				catch (DataValidationException ex4)
				{
					ex = ex4;
				}
				catch (ExClusTransientException ex5)
				{
					ex = ex5;
				}
				if (ex != null)
				{
					this.nextRefresh = ExDateTime.UtcNow.Add(this.refreshIntervalOnFailure);
					CallIdTracer.TraceError(this.Tracer, this.GetHashCode(), "LoadConfiguration failed. We will retry at {0}: Error={1}", new object[]
					{
						this.nextRefresh,
						ex
					});
				}
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000A8C0 File Offset: 0x00008AC0
		private bool CanUse(ContextObjectType context, ManagedObjectType nextServer)
		{
			CallIdTracer.TraceDebug(this.Tracer, this.GetHashCode(), "ServerPickerBase::CanUse() Found {0} as the next server", new object[]
			{
				nextServer
			});
			if (!this.InternalIsValid(context, nextServer))
			{
				CallIdTracer.TraceDebug(this.Tracer, this.GetHashCode(), "ServerPickerBase::IsValid {0} returned false", new object[]
				{
					nextServer
				});
			}
			else
			{
				if (this.InternalIsHealthy(context, nextServer))
				{
					return true;
				}
				this.ServerUnavailable(nextServer);
			}
			return false;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000A947 File Offset: 0x00008B47
		private bool InternalIsHealthy(ContextObjectType context, ManagedObjectType item)
		{
			if (this.InternalIsHealthyDelegate != null)
			{
				return this.InternalIsHealthyDelegate(context, item);
			}
			return this.IsHealthy(context, item);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000A967 File Offset: 0x00008B67
		private bool InternalIsValid(ContextObjectType context, ManagedObjectType candidate)
		{
			if (this.InternalIsValidDelegate != null)
			{
				return this.InternalIsValidDelegate(context, candidate);
			}
			return this.IsValid(context, candidate);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000A987 File Offset: 0x00008B87
		private List<ManagedObjectType> InternalLoadConfiguration()
		{
			if (this.InternalLoadConfigurationDelegate != null)
			{
				return this.InternalLoadConfigurationDelegate();
			}
			return this.LoadConfiguration();
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000A9A4 File Offset: 0x00008BA4
		private void UpdateRetryQueue()
		{
			ManagedObjectType managedObjectType = default(ManagedObjectType);
			do
			{
				managedObjectType = this.unavailableServers.Dequeue();
				if (managedObjectType != null)
				{
					CallIdTracer.TraceDebug(this.Tracer, this.GetHashCode(), "Server {0} being moved from retry to active", new object[]
					{
						managedObjectType
					});
					this.servers.Add(managedObjectType);
				}
			}
			while (managedObjectType != null);
		}

		// Token: 0x040000C9 RID: 201
		internal static readonly TimeSpan DefaultRetryInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x040000CA RID: 202
		internal static readonly TimeSpan DefaultRefreshInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x040000CB RID: 203
		internal static readonly TimeSpan DefaultRefreshIntervalOnFailure = TimeSpan.FromMinutes(1.0);

		// Token: 0x040000CC RID: 204
		private ServerPickerBase<ManagedObjectType, ContextObjectType>.LoadConfigurationDelegate loadConfigurationDelegate;

		// Token: 0x040000CD RID: 205
		private ServerPickerBase<ManagedObjectType, ContextObjectType>.IsHealthyDelegate healthyDelegate;

		// Token: 0x040000CE RID: 206
		private ServerPickerBase<ManagedObjectType, ContextObjectType>.IsValidDelegate validDelegate;

		// Token: 0x040000CF RID: 207
		private List<ManagedObjectType> servers;

		// Token: 0x040000D0 RID: 208
		private RetryQueue<ManagedObjectType> unavailableServers;

		// Token: 0x040000D1 RID: 209
		private int currentServerIndex;

		// Token: 0x040000D2 RID: 210
		private ExDateTime nextRefresh = ExDateTime.UtcNow;

		// Token: 0x040000D3 RID: 211
		private TimeSpan refreshInterval;

		// Token: 0x040000D4 RID: 212
		private TimeSpan refreshIntervalOnFailure;

		// Token: 0x040000D5 RID: 213
		private Trace tracer;

		// Token: 0x040000D6 RID: 214
		private object lockObject = new object();

		// Token: 0x0200002E RID: 46
		// (Invoke) Token: 0x060002A3 RID: 675
		internal delegate List<ManagedObjectType> LoadConfigurationDelegate();

		// Token: 0x0200002F RID: 47
		// (Invoke) Token: 0x060002A7 RID: 679
		internal delegate bool IsHealthyDelegate(ContextObjectType context, ManagedObjectType server);

		// Token: 0x02000030 RID: 48
		// (Invoke) Token: 0x060002AB RID: 683
		internal delegate bool IsValidDelegate(ContextObjectType context, ManagedObjectType server);
	}
}
