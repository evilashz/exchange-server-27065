using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001EB RID: 491
	internal sealed class OwaNotificationManager : DisposeTrackableBase
	{
		// Token: 0x06000FE5 RID: 4069 RVA: 0x000631C4 File Offset: 0x000613C4
		internal OwaNotificationManager()
		{
			this.conditionAdvisorTable = new Dictionary<OwaStoreObjectId, OwaConditionAdvisor>();
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x000631D8 File Offset: 0x000613D8
		public void CreateOwaConditionAdvisor(UserContext userContext, MailboxSession mailboxSession, OwaStoreObjectId folderId, EventObjectType objectType, EventType eventType)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			if (!userContext.IsWebPartRequest)
			{
				OwaConditionAdvisor value = new OwaConditionAdvisor(mailboxSession, folderId, objectType, eventType);
				if (this.conditionAdvisorTable == null)
				{
					this.conditionAdvisorTable = new Dictionary<OwaStoreObjectId, OwaConditionAdvisor>();
				}
				this.conditionAdvisorTable.Add(folderId, value);
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x00063235 File Offset: 0x00061435
		public Dictionary<OwaStoreObjectId, OwaConditionAdvisor> ConditionAdvisorTable
		{
			get
			{
				return this.conditionAdvisorTable;
			}
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0006323D File Offset: 0x0006143D
		public void DeleteOwaConditionAdvisor(OwaStoreObjectId folderId)
		{
			OwaNotificationManager.DeleteAdvisorFromTable<OwaConditionAdvisor>(folderId, this.conditionAdvisorTable);
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0006324C File Offset: 0x0006144C
		private static void DeleteAdvisorFromTable<T>(OwaStoreObjectId folderId, Dictionary<OwaStoreObjectId, T> advisorTable) where T : IDisposable
		{
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			if (advisorTable == null || !advisorTable.ContainsKey(folderId))
			{
				return;
			}
			T t = advisorTable[folderId];
			advisorTable.Remove(folderId);
			t.Dispose();
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x00063294 File Offset: 0x00061494
		public void DisposeOwaConditionAdvisorTable()
		{
			if (this.conditionAdvisorTable != null)
			{
				IDictionaryEnumerator dictionaryEnumerator = this.conditionAdvisorTable.GetEnumerator();
				while (dictionaryEnumerator.MoveNext())
				{
					OwaConditionAdvisor owaConditionAdvisor = (OwaConditionAdvisor)dictionaryEnumerator.Value;
					owaConditionAdvisor.Dispose();
				}
				this.conditionAdvisorTable.Clear();
				this.conditionAdvisorTable = null;
			}
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x000632E8 File Offset: 0x000614E8
		public void CreateDelegateOwaFolderCountAdvisor(MailboxSession mailboxSession, OwaStoreObjectId folderId, EventObjectType objectType, EventType eventType)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			if (this.delegateFolderCountAdvisorTable == null)
			{
				this.delegateFolderCountAdvisorTable = new Dictionary<OwaStoreObjectId, OwaFolderCountAdvisor>();
			}
			OwaFolderCountAdvisor value;
			if (!this.delegateFolderCountAdvisorTable.TryGetValue(folderId, out value))
			{
				value = new OwaFolderCountAdvisor(mailboxSession, folderId, objectType, eventType);
				this.delegateFolderCountAdvisorTable.Add(folderId, value);
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000FEC RID: 4076 RVA: 0x0006334C File Offset: 0x0006154C
		public Dictionary<OwaStoreObjectId, OwaFolderCountAdvisor> DelegateFolderCountAdvisorTable
		{
			get
			{
				return this.delegateFolderCountAdvisorTable;
			}
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x00063354 File Offset: 0x00061554
		public void DeleteDelegateFolderCountAdvisor(OwaStoreObjectId folderId)
		{
			OwaNotificationManager.DeleteAdvisorFromTable<OwaFolderCountAdvisor>(folderId, this.delegateFolderCountAdvisorTable);
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00063364 File Offset: 0x00061564
		public void DisposeDelegateOwaFolderCountAdvisorTable()
		{
			if (this.delegateFolderCountAdvisorTable != null)
			{
				IDictionaryEnumerator dictionaryEnumerator = this.delegateFolderCountAdvisorTable.GetEnumerator();
				while (dictionaryEnumerator.MoveNext())
				{
					OwaFolderCountAdvisor owaFolderCountAdvisor = (OwaFolderCountAdvisor)dictionaryEnumerator.Value;
					owaFolderCountAdvisor.Dispose();
				}
				this.delegateFolderCountAdvisorTable.Clear();
				this.delegateFolderCountAdvisorTable = null;
			}
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x000633B8 File Offset: 0x000615B8
		public void CreateArchiveOwaFolderCountAdvisor(MailboxSession mailboxSession)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			if (this.archiveFolderCountAdvisorTable == null)
			{
				this.archiveFolderCountAdvisorTable = new Dictionary<string, OwaFolderCountAdvisor>(StringComparer.InvariantCultureIgnoreCase);
			}
			OwaFolderCountAdvisor value;
			if (!this.archiveFolderCountAdvisorTable.TryGetValue(mailboxSession.MailboxOwnerLegacyDN, out value))
			{
				value = new OwaFolderCountAdvisor(mailboxSession, null, EventObjectType.Folder, EventType.ObjectModified);
				this.archiveFolderCountAdvisorTable.Add(mailboxSession.MailboxOwnerLegacyDN, value);
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000FF0 RID: 4080 RVA: 0x0006341C File Offset: 0x0006161C
		public Dictionary<string, OwaFolderCountAdvisor> ArchiveFolderCountAdvisorTable
		{
			get
			{
				return this.archiveFolderCountAdvisorTable;
			}
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00063424 File Offset: 0x00061624
		public void DisposeArchiveOwaFolderCountAdvisorTable()
		{
			if (this.archiveFolderCountAdvisorTable != null)
			{
				IDictionaryEnumerator dictionaryEnumerator = this.archiveFolderCountAdvisorTable.GetEnumerator();
				while (dictionaryEnumerator.MoveNext())
				{
					OwaFolderCountAdvisor owaFolderCountAdvisor = (OwaFolderCountAdvisor)dictionaryEnumerator.Value;
					owaFolderCountAdvisor.Dispose();
				}
				this.archiveFolderCountAdvisorTable.Clear();
				this.archiveFolderCountAdvisorTable = null;
			}
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00063478 File Offset: 0x00061678
		public void CreateOwaFolderCountAdvisor(MailboxSession mailboxSession)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			if (mailboxSession.LogonType != LogonType.Delegated)
			{
				this.folderCountAdvisor = new OwaFolderCountAdvisor(mailboxSession, null, EventObjectType.Folder, EventType.ObjectModified);
			}
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x000634A0 File Offset: 0x000616A0
		public void DisposeOwaFolderCountAdvisor()
		{
			if (this.folderCountAdvisor != null)
			{
				this.folderCountAdvisor.Dispose();
				this.folderCountAdvisor = null;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x000634BC File Offset: 0x000616BC
		public OwaFolderCountAdvisor FolderCountAdvisor
		{
			get
			{
				return this.folderCountAdvisor;
			}
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x000634C4 File Offset: 0x000616C4
		public void CreateOwaLastEventAdvisor(UserContext userContext, StoreObjectId folderId, EventObjectType objectType, EventType eventType)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			if (userContext.MailboxSession.LogonType != LogonType.Delegated)
			{
				this.lastEventAdvisor = new OwaLastEventAdvisor(userContext, folderId, objectType, eventType);
			}
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x00063500 File Offset: 0x00061700
		public void DisposeOwaLastEventAdvisor()
		{
			if (this.lastEventAdvisor != null)
			{
				this.lastEventAdvisor.Dispose();
				this.lastEventAdvisor = null;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x0006351C File Offset: 0x0006171C
		// (set) Token: 0x06000FF8 RID: 4088 RVA: 0x00063524 File Offset: 0x00061724
		public OwaLastEventAdvisor LastEventAdvisor
		{
			get
			{
				return this.lastEventAdvisor;
			}
			set
			{
				this.lastEventAdvisor = value;
			}
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0006352D File Offset: 0x0006172D
		protected override void InternalDispose(bool isDisposing)
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<bool>((long)this.GetHashCode(), "OwaNotificationManager.Dispose. IsDisposing: {0}", isDisposing);
			if (isDisposing)
			{
				this.DisposeOwaConditionAdvisorTable();
				this.DisposeOwaFolderCountAdvisor();
				this.DisposeOwaLastEventAdvisor();
				this.DisposeDelegateOwaFolderCountAdvisorTable();
				this.DisposeArchiveOwaFolderCountAdvisorTable();
			}
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x00063567 File Offset: 0x00061767
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OwaNotificationManager>(this);
		}

		// Token: 0x04000AAE RID: 2734
		private Dictionary<OwaStoreObjectId, OwaConditionAdvisor> conditionAdvisorTable;

		// Token: 0x04000AAF RID: 2735
		private OwaFolderCountAdvisor folderCountAdvisor;

		// Token: 0x04000AB0 RID: 2736
		private Dictionary<OwaStoreObjectId, OwaFolderCountAdvisor> delegateFolderCountAdvisorTable;

		// Token: 0x04000AB1 RID: 2737
		private OwaLastEventAdvisor lastEventAdvisor;

		// Token: 0x04000AB2 RID: 2738
		private Dictionary<string, OwaFolderCountAdvisor> archiveFolderCountAdvisorTable;
	}
}
