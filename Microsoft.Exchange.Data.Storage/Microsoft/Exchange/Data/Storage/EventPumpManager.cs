using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000701 RID: 1793
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class EventPumpManager
	{
		// Token: 0x06004707 RID: 18183 RVA: 0x0012E07D File Offset: 0x0012C27D
		private EventPumpManager()
		{
		}

		// Token: 0x06004708 RID: 18184 RVA: 0x0012E09C File Offset: 0x0012C29C
		internal void RegisterEventSink(StoreSession session, EventSink eventSink)
		{
			EventPump eventPump = this.GetEventPump(session);
			bool flag = false;
			try
			{
				eventPump.AddEventSink(eventSink);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					eventPump.Release();
					eventPump = null;
				}
			}
		}

		// Token: 0x170014B3 RID: 5299
		// (get) Token: 0x06004709 RID: 18185 RVA: 0x0012E0DC File Offset: 0x0012C2DC
		internal static EventPumpManager Instance
		{
			get
			{
				return EventPumpManager.instance;
			}
		}

		// Token: 0x0600470A RID: 18186 RVA: 0x0012E0E4 File Offset: 0x0012C2E4
		internal void RemoveEventPump(EventPump eventPump)
		{
			bool flag = false;
			lock (this.thisLock)
			{
				if (eventPump.ReferenceCount == 0)
				{
					this.RemoveEventPumpFromList(eventPump);
					flag = true;
				}
			}
			if (flag)
			{
				eventPump.Dispose();
			}
		}

		// Token: 0x0600470B RID: 18187 RVA: 0x0012E13C File Offset: 0x0012C33C
		internal void RemoveBrokenEventPump(EventPump eventPump)
		{
			this.RemoveEventPumpFromList(eventPump);
		}

		// Token: 0x0600470C RID: 18188 RVA: 0x0012E148 File Offset: 0x0012C348
		private void RemoveEventPumpFromList(EventPump eventPump)
		{
			lock (this.thisLock)
			{
				EventPump eventPump2 = null;
				if (this.TryGetEventPump(eventPump.MdbGuid, out eventPump2) && eventPump == eventPump2)
				{
					this.eventPumps.Remove(eventPump.MdbGuid);
				}
			}
		}

		// Token: 0x0600470D RID: 18189 RVA: 0x0012E1AC File Offset: 0x0012C3AC
		private EventPump GetEventPump(StoreSession session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			string text = session.IsRemote ? null : session.ServerFullyQualifiedDomainName;
			if (text == null)
			{
				throw new NotSupportedException("Reliable notifications are not supported for remote connections.");
			}
			int num = text.IndexOf('.');
			if (num == -1)
			{
				throw new CorruptDataException(ServerStrings.ExInvalidFullyQualifiedServerName);
			}
			string server = text.Substring(0, num);
			Guid mdbGuid = session.MdbGuid;
			if (mdbGuid == Guid.Empty)
			{
				throw new CorruptDataException(ServerStrings.ExInvalidMdbGuid);
			}
			EventPump eventPump = null;
			lock (this.thisLock)
			{
				if (this.TryGetEventPump(mdbGuid, out eventPump))
				{
					eventPump.AddRef();
				}
			}
			EventPump eventPump2 = null;
			try
			{
				if (eventPump == null)
				{
					eventPump2 = new EventPump(this, server, mdbGuid);
					eventPump2.AddRef();
					lock (this.thisLock)
					{
						if (this.TryGetEventPump(mdbGuid, out eventPump))
						{
							eventPump.AddRef();
						}
						else
						{
							if (eventPump2.Exception != null)
							{
								throw eventPump2.Exception;
							}
							this.AddEventPump(mdbGuid, eventPump2);
							eventPump = eventPump2;
							eventPump2 = null;
						}
					}
				}
			}
			finally
			{
				if (eventPump2 != null)
				{
					eventPump2.Release();
					eventPump2 = null;
				}
			}
			return eventPump;
		}

		// Token: 0x0600470E RID: 18190 RVA: 0x0012E308 File Offset: 0x0012C508
		private bool TryGetEventPump(Guid mdbGuid, out EventPump eventPump)
		{
			eventPump = null;
			lock (this.thisLock)
			{
				WeakReference weakReference = null;
				if (this.eventPumps.TryGetValue(mdbGuid, out weakReference))
				{
					eventPump = (EventPump)weakReference.Target;
					if (weakReference.IsAlive)
					{
						return true;
					}
					this.eventPumps.Remove(mdbGuid);
				}
			}
			return false;
		}

		// Token: 0x0600470F RID: 18191 RVA: 0x0012E380 File Offset: 0x0012C580
		private void AddEventPump(Guid mdbGuid, EventPump eventPump)
		{
			lock (this.thisLock)
			{
				this.eventPumps.Add(mdbGuid, new WeakReference(eventPump));
			}
		}

		// Token: 0x040026D6 RID: 9942
		private Dictionary<Guid, WeakReference> eventPumps = new Dictionary<Guid, WeakReference>();

		// Token: 0x040026D7 RID: 9943
		private static EventPumpManager instance = new EventPumpManager();

		// Token: 0x040026D8 RID: 9944
		private readonly object thisLock = new object();
	}
}
