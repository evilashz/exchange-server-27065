using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	// Token: 0x020003F5 RID: 1013
	[__DynamicallyInvokable]
	public class EventListener : IDisposable
	{
		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060033D0 RID: 13264 RVA: 0x000C9040 File Offset: 0x000C7240
		// (remove) Token: 0x060033D1 RID: 13265 RVA: 0x000C9078 File Offset: 0x000C7278
		private event EventHandler<EventSourceCreatedEventArgs> _EventSourceCreated;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060033D2 RID: 13266 RVA: 0x000C90B0 File Offset: 0x000C72B0
		// (remove) Token: 0x060033D3 RID: 13267 RVA: 0x000C9108 File Offset: 0x000C7308
		public event EventHandler<EventSourceCreatedEventArgs> EventSourceCreated
		{
			add
			{
				object obj = EventListener.s_EventSourceCreatedLock;
				lock (obj)
				{
					this.CallBackForExistingEventSources(false, value);
					this._EventSourceCreated = (EventHandler<EventSourceCreatedEventArgs>)Delegate.Combine(this._EventSourceCreated, value);
				}
			}
			remove
			{
				object obj = EventListener.s_EventSourceCreatedLock;
				lock (obj)
				{
					this._EventSourceCreated = (EventHandler<EventSourceCreatedEventArgs>)Delegate.Remove(this._EventSourceCreated, value);
				}
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060033D4 RID: 13268 RVA: 0x000C9158 File Offset: 0x000C7358
		// (remove) Token: 0x060033D5 RID: 13269 RVA: 0x000C9190 File Offset: 0x000C7390
		public event EventHandler<EventWrittenEventArgs> EventWritten;

		// Token: 0x060033D6 RID: 13270 RVA: 0x000C91C5 File Offset: 0x000C73C5
		[__DynamicallyInvokable]
		public EventListener()
		{
			this.CallBackForExistingEventSources(true, delegate(object obj, EventSourceCreatedEventArgs args)
			{
				args.EventSource.AddListener(this);
			});
		}

		// Token: 0x060033D7 RID: 13271 RVA: 0x000C91E0 File Offset: 0x000C73E0
		[__DynamicallyInvokable]
		public virtual void Dispose()
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				if (EventListener.s_Listeners != null)
				{
					if (this == EventListener.s_Listeners)
					{
						EventListener listenerToRemove = EventListener.s_Listeners;
						EventListener.s_Listeners = this.m_Next;
						EventListener.RemoveReferencesToListenerInEventSources(listenerToRemove);
					}
					else
					{
						EventListener eventListener = EventListener.s_Listeners;
						EventListener next;
						for (;;)
						{
							next = eventListener.m_Next;
							if (next == null)
							{
								break;
							}
							if (next == this)
							{
								goto Block_6;
							}
							eventListener = next;
						}
						return;
						Block_6:
						eventListener.m_Next = next.m_Next;
						EventListener.RemoveReferencesToListenerInEventSources(next);
					}
				}
			}
		}

		// Token: 0x060033D8 RID: 13272 RVA: 0x000C9280 File Offset: 0x000C7480
		[__DynamicallyInvokable]
		public void EnableEvents(EventSource eventSource, EventLevel level)
		{
			this.EnableEvents(eventSource, level, EventKeywords.None);
		}

		// Token: 0x060033D9 RID: 13273 RVA: 0x000C928C File Offset: 0x000C748C
		[__DynamicallyInvokable]
		public void EnableEvents(EventSource eventSource, EventLevel level, EventKeywords matchAnyKeyword)
		{
			this.EnableEvents(eventSource, level, matchAnyKeyword, null);
		}

		// Token: 0x060033DA RID: 13274 RVA: 0x000C9298 File Offset: 0x000C7498
		[__DynamicallyInvokable]
		public void EnableEvents(EventSource eventSource, EventLevel level, EventKeywords matchAnyKeyword, IDictionary<string, string> arguments)
		{
			if (eventSource == null)
			{
				throw new ArgumentNullException("eventSource");
			}
			eventSource.SendCommand(this, 0, 0, EventCommand.Update, true, level, matchAnyKeyword, arguments);
		}

		// Token: 0x060033DB RID: 13275 RVA: 0x000C92C4 File Offset: 0x000C74C4
		[__DynamicallyInvokable]
		public void DisableEvents(EventSource eventSource)
		{
			if (eventSource == null)
			{
				throw new ArgumentNullException("eventSource");
			}
			eventSource.SendCommand(this, 0, 0, EventCommand.Update, false, EventLevel.LogAlways, EventKeywords.None, null);
		}

		// Token: 0x060033DC RID: 13276 RVA: 0x000C92EE File Offset: 0x000C74EE
		[__DynamicallyInvokable]
		public static int EventSourceIndex(EventSource eventSource)
		{
			return eventSource.m_id;
		}

		// Token: 0x060033DD RID: 13277 RVA: 0x000C92F8 File Offset: 0x000C74F8
		[__DynamicallyInvokable]
		protected internal virtual void OnEventSourceCreated(EventSource eventSource)
		{
			EventHandler<EventSourceCreatedEventArgs> eventSourceCreated = this._EventSourceCreated;
			if (eventSourceCreated != null)
			{
				eventSourceCreated(this, new EventSourceCreatedEventArgs
				{
					EventSource = eventSource
				});
			}
		}

		// Token: 0x060033DE RID: 13278 RVA: 0x000C9324 File Offset: 0x000C7524
		[__DynamicallyInvokable]
		protected internal virtual void OnEventWritten(EventWrittenEventArgs eventData)
		{
			EventHandler<EventWrittenEventArgs> eventWritten = this.EventWritten;
			if (eventWritten != null)
			{
				eventWritten(this, eventData);
			}
		}

		// Token: 0x060033DF RID: 13279 RVA: 0x000C9344 File Offset: 0x000C7544
		internal static void AddEventSource(EventSource newEventSource)
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				if (EventListener.s_EventSources == null)
				{
					EventListener.s_EventSources = new List<WeakReference>(2);
				}
				if (!EventListener.s_EventSourceShutdownRegistered)
				{
					EventListener.s_EventSourceShutdownRegistered = true;
					AppDomain.CurrentDomain.ProcessExit += EventListener.DisposeOnShutdown;
					AppDomain.CurrentDomain.DomainUnload += EventListener.DisposeOnShutdown;
				}
				int num = -1;
				if (EventListener.s_EventSources.Count % 64 == 63)
				{
					int num2 = EventListener.s_EventSources.Count;
					while (0 < num2)
					{
						num2--;
						WeakReference weakReference = EventListener.s_EventSources[num2];
						if (!weakReference.IsAlive)
						{
							num = num2;
							weakReference.Target = newEventSource;
							break;
						}
					}
				}
				if (num < 0)
				{
					num = EventListener.s_EventSources.Count;
					EventListener.s_EventSources.Add(new WeakReference(newEventSource));
				}
				newEventSource.m_id = num;
				for (EventListener next = EventListener.s_Listeners; next != null; next = next.m_Next)
				{
					newEventSource.AddListener(next);
				}
			}
		}

		// Token: 0x060033E0 RID: 13280 RVA: 0x000C9458 File Offset: 0x000C7658
		private static void DisposeOnShutdown(object sender, EventArgs e)
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				foreach (WeakReference weakReference in EventListener.s_EventSources)
				{
					EventSource eventSource = weakReference.Target as EventSource;
					if (eventSource != null)
					{
						eventSource.Dispose();
					}
				}
			}
		}

		// Token: 0x060033E1 RID: 13281 RVA: 0x000C94E4 File Offset: 0x000C76E4
		private static void RemoveReferencesToListenerInEventSources(EventListener listenerToRemove)
		{
			using (List<WeakReference>.Enumerator enumerator = EventListener.s_EventSources.GetEnumerator())
			{
				IL_7E:
				while (enumerator.MoveNext())
				{
					WeakReference weakReference = enumerator.Current;
					EventSource eventSource = weakReference.Target as EventSource;
					if (eventSource != null)
					{
						if (eventSource.m_Dispatchers.m_Listener == listenerToRemove)
						{
							eventSource.m_Dispatchers = eventSource.m_Dispatchers.m_Next;
						}
						else
						{
							EventDispatcher eventDispatcher = eventSource.m_Dispatchers;
							EventDispatcher next;
							for (;;)
							{
								next = eventDispatcher.m_Next;
								if (next == null)
								{
									goto IL_7E;
								}
								if (next.m_Listener == listenerToRemove)
								{
									break;
								}
								eventDispatcher = next;
							}
							eventDispatcher.m_Next = next.m_Next;
						}
					}
				}
			}
		}

		// Token: 0x060033E2 RID: 13282 RVA: 0x000C9598 File Offset: 0x000C7798
		[Conditional("DEBUG")]
		internal static void Validate()
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				Dictionary<EventListener, bool> dictionary = new Dictionary<EventListener, bool>();
				for (EventListener next = EventListener.s_Listeners; next != null; next = next.m_Next)
				{
					dictionary.Add(next, true);
				}
				int num = -1;
				foreach (WeakReference weakReference in EventListener.s_EventSources)
				{
					num++;
					EventSource eventSource = weakReference.Target as EventSource;
					if (eventSource != null)
					{
						for (EventDispatcher eventDispatcher = eventSource.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
						{
						}
						foreach (EventListener eventListener in dictionary.Keys)
						{
							EventDispatcher eventDispatcher = eventSource.m_Dispatchers;
							while (eventDispatcher.m_Listener != eventListener)
							{
								eventDispatcher = eventDispatcher.m_Next;
							}
						}
					}
				}
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x060033E3 RID: 13283 RVA: 0x000C96C8 File Offset: 0x000C78C8
		internal static object EventListenersLock
		{
			get
			{
				if (EventListener.s_EventSources == null)
				{
					Interlocked.CompareExchange<List<WeakReference>>(ref EventListener.s_EventSources, new List<WeakReference>(2), null);
				}
				return EventListener.s_EventSources;
			}
		}

		// Token: 0x060033E4 RID: 13284 RVA: 0x000C96E8 File Offset: 0x000C78E8
		private void CallBackForExistingEventSources(bool addToListenersList, EventHandler<EventSourceCreatedEventArgs> callback)
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				if (EventListener.s_CreatingListener)
				{
					throw new InvalidOperationException(Environment.GetResourceString("EventSource_ListenerCreatedInsideCallback"));
				}
				try
				{
					EventListener.s_CreatingListener = true;
					if (addToListenersList)
					{
						this.m_Next = EventListener.s_Listeners;
						EventListener.s_Listeners = this;
					}
					foreach (WeakReference weakReference in EventListener.s_EventSources.ToArray())
					{
						EventSource eventSource = weakReference.Target as EventSource;
						if (eventSource != null)
						{
							callback(this, new EventSourceCreatedEventArgs
							{
								EventSource = eventSource
							});
						}
					}
				}
				finally
				{
					EventListener.s_CreatingListener = false;
				}
			}
		}

		// Token: 0x040016BA RID: 5818
		private static readonly object s_EventSourceCreatedLock = new object();

		// Token: 0x040016BD RID: 5821
		internal volatile EventListener m_Next;

		// Token: 0x040016BE RID: 5822
		internal ActivityFilter m_activityFilter;

		// Token: 0x040016BF RID: 5823
		internal static EventListener s_Listeners;

		// Token: 0x040016C0 RID: 5824
		internal static List<WeakReference> s_EventSources;

		// Token: 0x040016C1 RID: 5825
		private static bool s_CreatingListener = false;

		// Token: 0x040016C2 RID: 5826
		private static bool s_EventSourceShutdownRegistered = false;
	}
}
