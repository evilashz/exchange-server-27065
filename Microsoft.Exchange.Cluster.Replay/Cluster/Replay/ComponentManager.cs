using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002CA RID: 714
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ComponentManager
	{
		// Token: 0x06001BE4 RID: 7140 RVA: 0x00078918 File Offset: 0x00076B18
		public ComponentManager(TimeSpan periodicStartInterval)
		{
			this.m_periodicStartInterval = periodicStartInterval;
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x00078934 File Offset: 0x00076B34
		public virtual void Start()
		{
			bool flag = false;
			this.m_componentsOnline = new HashSet<IServiceComponent>();
			foreach (IServiceComponent serviceComponent in this.m_components)
			{
				lock (this.m_locker)
				{
					if (this.m_fShutdown)
					{
						ExTraceGlobals.ReplayManagerTracer.TraceDebug((long)this.GetHashCode(), "ComponentManager: Start() is bailing due to shutdown.");
						return;
					}
					if (!serviceComponent.IsEnabled || serviceComponent.Start())
					{
						this.RecordComponentOnline(serviceComponent);
					}
					else
					{
						if (serviceComponent.IsCritical)
						{
							throw new ReplayCriticalComponentFailedToStartException(serviceComponent.Name);
						}
						if (serviceComponent.IsRetriableOnError)
						{
							this.LogRetriableComponentStartFailed(serviceComponent);
							flag = true;
						}
					}
				}
			}
			if (flag)
			{
				lock (this.m_locker)
				{
					if (!this.m_fShutdown)
					{
						this.m_periodicStarter = new ComponentManager.PeriodicComponentStarter(this, this.m_periodicStartInterval);
						this.m_periodicStarter.Start();
					}
				}
				return;
			}
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x00078A58 File Offset: 0x00076C58
		public virtual void Stop()
		{
			lock (this.m_locker)
			{
				this.m_fShutdown = true;
			}
			this.StopPeriodicStarter();
			foreach (IServiceComponent serviceComponent in this.m_components.Reverse<IServiceComponent>())
			{
				if (serviceComponent.IsEnabled && this.m_componentsOnline.Contains(serviceComponent))
				{
					ExTraceGlobals.ReplayManagerTracer.TraceDebug<string>((long)this.GetHashCode(), "ComponentManager: Component '{0}' is being stopped.", serviceComponent.Name);
					serviceComponent.Stop();
					this.RecordComponentOffline(serviceComponent);
				}
			}
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x00078B18 File Offset: 0x00076D18
		internal void HavePossibleHungComponentInvoke(Action toInvoke)
		{
			if (this.m_fShutdown)
			{
				foreach (IServiceComponent serviceComponent in this.m_components.Reverse<IServiceComponent>())
				{
					bool flag = false;
					lock (this.m_locker)
					{
						flag = this.m_componentsOnline.Contains(serviceComponent);
					}
					if (serviceComponent.IsEnabled && flag)
					{
						serviceComponent.Invoke(toInvoke);
						break;
					}
				}
			}
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x00078BBC File Offset: 0x00076DBC
		private void StartRetriableComponents()
		{
			IEnumerable<IServiceComponent> enumerable = null;
			lock (this.m_locker)
			{
				if (this.m_fShutdown)
				{
					ExTraceGlobals.ReplayManagerTracer.TraceDebug((long)this.GetHashCode(), "ComponentManager: StartRetriableComponents() is bailing due to shutdown.");
					return;
				}
				enumerable = this.GetOfflineRetriableComponents();
			}
			foreach (IServiceComponent serviceComponent in enumerable)
			{
				lock (this.m_locker)
				{
					if (this.m_fShutdown)
					{
						ExTraceGlobals.ReplayManagerTracer.TraceDebug((long)this.GetHashCode(), "ComponentManager: StartRetriableComponents() is bailing due to shutdown.");
						break;
					}
					if (!serviceComponent.IsEnabled || serviceComponent.Start())
					{
						this.RecordComponentOnline(serviceComponent);
					}
					else
					{
						ExTraceGlobals.ReplayManagerTracer.TraceDebug<string>((long)this.GetHashCode(), "ComponentManager: StartRetriableComponents() failed to start retriable component '{0}'.", serviceComponent.Name);
						this.LogRetriableComponentStartFailed(serviceComponent);
					}
				}
			}
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x00078CF0 File Offset: 0x00076EF0
		private void RecordComponentOnline(IServiceComponent component)
		{
			bool flag = false;
			if (component.IsEnabled)
			{
				ExTraceGlobals.ReplayManagerTracer.TraceDebug<string>((long)this.GetHashCode(), "ComponentManager: Component '{0}' was successfully started.", component.Name);
				ReplayCrimsonEvents.FacilityReady.Log<string>(component.Name);
			}
			else
			{
				ExTraceGlobals.ReplayManagerTracer.TraceDebug<string>((long)this.GetHashCode(), "ComponentManager: Component '{0}' was not started because it's disabled.", component.Name);
			}
			lock (this.m_locker)
			{
				if (!this.m_componentsOnline.Contains(component))
				{
					this.m_onlineCount++;
					this.m_componentsOnline.Add(component);
					if (this.m_componentsOnline.Count == this.m_components.Length)
					{
						flag = true;
					}
				}
			}
			if (flag)
			{
				ExTraceGlobals.ReplayManagerTracer.TraceDebug((long)this.GetHashCode(), "ComponentManager: All components have been successfully started.");
				ReplayEventLogConstants.Tuple_AllFacilitiesAreOnline.LogEvent(null, new object[0]);
				ThreadPool.QueueUserWorkItem(delegate(object param0)
				{
					this.StopPeriodicStarter();
				});
			}
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x00078E04 File Offset: 0x00077004
		private void RecordComponentOffline(IServiceComponent component)
		{
			if (component.IsEnabled)
			{
				lock (this.m_locker)
				{
					if (this.m_componentsOnline.Contains(component))
					{
						this.m_onlineCount--;
						this.m_componentsOnline.Remove(component);
					}
				}
				ExTraceGlobals.ReplayManagerTracer.TraceDebug<string>((long)this.GetHashCode(), "ComponentManager: Component '{0}' was stopped.", component.Name);
				ReplayCrimsonEvents.FacilityOffline.Log<string>(component.Name);
				return;
			}
			ExTraceGlobals.ReplayManagerTracer.TraceDebug<string>((long)this.GetHashCode(), "ComponentManager: Component '{0}' was not stopped because it's disabled.", component.Name);
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x00078EB8 File Offset: 0x000770B8
		private List<IServiceComponent> GetOfflineRetriableComponents()
		{
			List<IServiceComponent> list = new List<IServiceComponent>(this.m_components.Length);
			foreach (IServiceComponent serviceComponent in this.m_components)
			{
				if (!this.m_componentsOnline.Contains(serviceComponent) && serviceComponent.IsRetriableOnError)
				{
					list.Add(serviceComponent);
				}
			}
			return list;
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x00078F0C File Offset: 0x0007710C
		private void LogRetriableComponentStartFailed(IServiceComponent component)
		{
			ReplayEventLogConstants.Tuple_FailedToStartRetriableComponent.LogEvent(component.Name, new object[]
			{
				component.Name,
				(int)this.m_periodicStartInterval.TotalSeconds
			});
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x00078F51 File Offset: 0x00077151
		private void StopPeriodicStarter()
		{
			if (this.m_periodicStarter != null)
			{
				ExTraceGlobals.ReplayManagerTracer.TraceDebug((long)this.GetHashCode(), "ComponentManager: PeriodicComponentStarter is being stopped.");
				this.m_periodicStarter.Stop();
				this.m_periodicStarter = null;
			}
		}

		// Token: 0x04000BA8 RID: 2984
		protected IServiceComponent[] m_components;

		// Token: 0x04000BA9 RID: 2985
		private readonly TimeSpan m_periodicStartInterval;

		// Token: 0x04000BAA RID: 2986
		private ComponentManager.PeriodicComponentStarter m_periodicStarter;

		// Token: 0x04000BAB RID: 2987
		private HashSet<IServiceComponent> m_componentsOnline;

		// Token: 0x04000BAC RID: 2988
		private int m_onlineCount;

		// Token: 0x04000BAD RID: 2989
		private bool m_fShutdown;

		// Token: 0x04000BAE RID: 2990
		private object m_locker = new object();

		// Token: 0x020002CB RID: 715
		[ClassAccessLevel(AccessLevel.Implementation)]
		private class PeriodicComponentStarter : TimerComponent
		{
			// Token: 0x06001BEF RID: 7151 RVA: 0x00078F83 File Offset: 0x00077183
			public PeriodicComponentStarter(ComponentManager componentManager, TimeSpan periodicStartInterval) : base(periodicStartInterval, periodicStartInterval, "PeriodicComponentStarter")
			{
				this.m_componentManager = componentManager;
			}

			// Token: 0x06001BF0 RID: 7152 RVA: 0x00078F99 File Offset: 0x00077199
			protected override void TimerCallbackInternal()
			{
				this.m_componentManager.StartRetriableComponents();
			}

			// Token: 0x04000BAF RID: 2991
			private ComponentManager m_componentManager;
		}
	}
}
