using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000037 RID: 55
	internal sealed class EventBasedAssistantCollection : Base, IDisposable, IEnumerable<AssistantCollectionEntry>, IEnumerable
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x00009A14 File Offset: 0x00007C14
		private EventBasedAssistantCollection(DatabaseInfo databaseInfo, AssistantType[] assistantTypes)
		{
			this.databaseInfo = databaseInfo;
			List<PropertyDefinition> list = new List<PropertyDefinition>();
			this.eventMask = (MapiEventTypeFlags)0;
			this.needsMailboxSession = false;
			foreach (AssistantType assistantType in assistantTypes)
			{
				if (!databaseInfo.IsPublic || assistantType.ProcessesPublicDatabases)
				{
					this.eventMask |= assistantType.EventMask;
					this.needsMailboxSession |= assistantType.NeedsMailboxSession;
					if (assistantType.PreloadItemProperties != null && assistantType.PreloadItemProperties.Length > 0)
					{
						list.AddRange(assistantType.PreloadItemProperties);
					}
				}
			}
			this.preloadItemProperties = list.ToArray();
			this.listOfAssistants = new List<AssistantCollectionEntry>(assistantTypes.Length);
			bool flag = false;
			try
			{
				foreach (AssistantType assistantType2 in assistantTypes)
				{
					if (!databaseInfo.IsPublic || assistantType2.ProcessesPublicDatabases)
					{
						this.listOfAssistants.Add(new AssistantCollectionEntry(assistantType2, databaseInfo));
					}
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.Dispose();
				}
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00009B2C File Offset: 0x00007D2C
		public int Count
		{
			get
			{
				return this.listOfAssistants.Count;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00009B39 File Offset: 0x00007D39
		public MapiEventTypeFlags EventMask
		{
			get
			{
				return this.eventMask;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00009B41 File Offset: 0x00007D41
		public bool NeedsMailboxSession
		{
			get
			{
				return this.needsMailboxSession;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00009B49 File Offset: 0x00007D49
		public PropertyDefinition[] PreloadItemProperties
		{
			get
			{
				return this.preloadItemProperties;
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00009B54 File Offset: 0x00007D54
		public static EventBasedAssistantCollection Create(DatabaseInfo databaseInfo, AssistantType[] assistantTypes)
		{
			EventBasedAssistantCollection eventBasedAssistantCollection = new EventBasedAssistantCollection(databaseInfo, assistantTypes);
			if (eventBasedAssistantCollection.Count == 0)
			{
				eventBasedAssistantCollection.Dispose();
				return null;
			}
			return eventBasedAssistantCollection;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00009B7A File Offset: 0x00007D7A
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "Event-based assistant collection for database '" + this.databaseInfo.DisplayName + "'";
			}
			return this.toString;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00009BAA File Offset: 0x00007DAA
		public AssistantCollectionEntry GetAssistantForPublicFolder()
		{
			return this.listOfAssistants[0];
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00009BB8 File Offset: 0x00007DB8
		public void ShutdownAssistants(HangDetector hangDetector)
		{
			foreach (AssistantCollectionEntry assistantCollectionEntry in this.listOfAssistants)
			{
				try
				{
					if (assistantCollectionEntry != null)
					{
						AIBreadcrumbs.ShutdownTrail.Drop("Stopping event assistant " + assistantCollectionEntry.Name);
						hangDetector.AssistantName = assistantCollectionEntry.Name;
						assistantCollectionEntry.Shutdown();
						AIBreadcrumbs.ShutdownTrail.Drop("Finished stopping " + assistantCollectionEntry.Name);
					}
				}
				finally
				{
					hangDetector.AssistantName = "Common Code";
				}
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00009C68 File Offset: 0x00007E68
		public void Dispose()
		{
			if (this.listOfAssistants != null)
			{
				foreach (AssistantCollectionEntry assistantCollectionEntry in this.listOfAssistants)
				{
					IDisposable disposable = assistantCollectionEntry.Instance as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
				this.listOfAssistants = null;
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00009CD8 File Offset: 0x00007ED8
		public void RemoveAssistant(AssistantCollectionEntry assistantToRemove)
		{
			if (assistantToRemove != null)
			{
				this.listOfAssistants.Remove(assistantToRemove);
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00009CEA File Offset: 0x00007EEA
		public IEnumerator<AssistantCollectionEntry> GetEnumerator()
		{
			return ((IEnumerable<AssistantCollectionEntry>)this.listOfAssistants).GetEnumerator();
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00009CF7 File Offset: 0x00007EF7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.listOfAssistants.GetEnumerator();
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00009D0C File Offset: 0x00007F0C
		internal static EventBasedAssistantCollection CreateForTest(DatabaseInfo databaseInfo, IEventBasedAssistantType[] eventBasedAssistantTypes)
		{
			PerformanceCountersPerAssistantInstance performanceCountersPerAssistantsTotal = new PerformanceCountersPerAssistantInstance("TestAssistant-Total", null);
			AssistantType[] assistantTypes = AssistantType.CreateArray(eventBasedAssistantTypes, performanceCountersPerAssistantsTotal);
			EventBasedAssistantCollection eventBasedAssistantCollection = new EventBasedAssistantCollection(databaseInfo, assistantTypes);
			if (eventBasedAssistantCollection.Count == 0)
			{
				eventBasedAssistantCollection.Dispose();
				return null;
			}
			return eventBasedAssistantCollection;
		}

		// Token: 0x04000171 RID: 369
		private MapiEventTypeFlags eventMask;

		// Token: 0x04000172 RID: 370
		private bool needsMailboxSession;

		// Token: 0x04000173 RID: 371
		private PropertyDefinition[] preloadItemProperties;

		// Token: 0x04000174 RID: 372
		private List<AssistantCollectionEntry> listOfAssistants;

		// Token: 0x04000175 RID: 373
		private string toString;

		// Token: 0x04000176 RID: 374
		private DatabaseInfo databaseInfo;
	}
}
