using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Mapi.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x02000025 RID: 37
	[Serializable]
	public class MailboxResourceMonitorEntry : MapiObject
	{
		// Token: 0x06000108 RID: 264 RVA: 0x00006A94 File Offset: 0x00004C94
		protected sealed override ObjectId GetIdentity()
		{
			object obj;
			if ((obj = this[MapiObjectSchema.Identity]) == null)
			{
				obj = (this[MapiObjectSchema.Identity] = new MailboxId());
			}
			return (ObjectId)obj;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00006AC8 File Offset: 0x00004CC8
		protected sealed override MapiObject.RetrievePropertiesScope RetrievePropertiesScopeForFinding
		{
			get
			{
				return MapiObject.RetrievePropertiesScope.Database;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00006ACB File Offset: 0x00004CCB
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MailboxResourceMonitorEntry.schema;
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00006AD2 File Offset: 0x00004CD2
		internal sealed override void Save(bool keepUnmanagedResources)
		{
			throw new NotImplementedException("The method Save is not implemented.");
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006ADE File Offset: 0x00004CDE
		internal sealed override void Read(bool keepUnmanagedResources)
		{
			throw new NotImplementedException("The method Read is not implemented.");
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006AEA File Offset: 0x00004CEA
		internal sealed override void Delete()
		{
			throw new NotImplementedException("The method Delete is not implemented.");
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00006AF6 File Offset: 0x00004CF6
		internal sealed override MapiProp RawMapiEntry
		{
			get
			{
				throw new NotImplementedException("The property RawMapiEntry is not implemented.");
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006B04 File Offset: 0x00004D04
		protected sealed override void UpdateIdentity(MapiObject.UpdateIdentityFlags flags)
		{
			MapiEntryId mapiEntryId = this.Identity.MapiEntryId;
			DatabaseId mailboxDatabaseId = this.Identity.MailboxDatabaseId;
			Guid mailboxGuid = this.Identity.MailboxGuid;
			string mailboxExchangeLegacyDn = this.Identity.MailboxExchangeLegacyDn;
			this.Identity = new MailboxId(mapiEntryId, mailboxDatabaseId, mailboxGuid, mailboxExchangeLegacyDn);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00006B50 File Offset: 0x00004D50
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MailboxResourceMonitorEntry>(this);
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00006B58 File Offset: 0x00004D58
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00006B65 File Offset: 0x00004D65
		public new MailboxId Identity
		{
			get
			{
				return (MailboxId)base.MapiIdentity;
			}
			internal set
			{
				base.MapiIdentity = value;
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00006B6E File Offset: 0x00004D6E
		internal override T[] Find<T>(QueryFilter filter, MapiObjectId root, QueryScope scope, SortBy sort, int maximumResultsSize)
		{
			return new List<T>(this.FindPaged<T>(filter, root, scope, sort, 0, maximumResultsSize)).ToArray();
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00007138 File Offset: 0x00005338
		internal override IEnumerable<T> FindPaged<T>(QueryFilter filter, MapiObjectId root, QueryScope scope, SortBy sort, int pageSize, int maximumResultsSize)
		{
			MailboxResourceMonitorEntry.<>c__DisplayClass1<T> CS$<>8__locals1 = new MailboxResourceMonitorEntry.<>c__DisplayClass1<T>();
			CS$<>8__locals1.<>4__this = this;
			if (!base.GetType().IsAssignableFrom(typeof(T)))
			{
				throw new ArgumentException("T");
			}
			CS$<>8__locals1.databaseId = (root as DatabaseId);
			if (null == CS$<>8__locals1.databaseId)
			{
				throw new NotSupportedException(Strings.ExceptionIdentityTypeInvalid);
			}
			if (QueryScope.SubTree != scope)
			{
				throw new ArgumentException("scope");
			}
			if (sort != null)
			{
				throw new ArgumentException("sort");
			}
			if (0 > maximumResultsSize)
			{
				throw new ArgumentException("maximumResultsSize");
			}
			if (base.MapiSession == null)
			{
				throw new MapiInvalidOperationException(Strings.ExceptionSessionNull);
			}
			base.EnableDisposeTracking();
			PropTag[] tagsToRead;
			using (MailboxResourceMonitorEntry mailboxResourceMonitorEntry2 = (MailboxResourceMonitorEntry)((object)((default(T) == null) ? Activator.CreateInstance<T>() : default(T))))
			{
				tagsToRead = mailboxResourceMonitorEntry2.GetPropertyTagsToRead();
			}
			if (tagsToRead == null || tagsToRead.Length == 0)
			{
				tagsToRead = new PropTag[]
				{
					PropTag.UserGuid,
					PropTag.EmailAddress
				};
			}
			PropValue[][] entries = null;
			base.MapiSession.InvokeWithWrappedException(delegate()
			{
				entries = CS$<>8__locals1.<>4__this.MapiSession.Administration.GetResourceMonitorDigest(CS$<>8__locals1.databaseId.Guid, tagsToRead);
			}, Strings.ExceptionFindObject(typeof(T).Name, (null == root) ? Strings.ConstantNull : root.ToString()), CS$<>8__locals1.databaseId);
			int resultSize = entries.Length;
			if (0 < maximumResultsSize && maximumResultsSize < resultSize)
			{
				resultSize = maximumResultsSize;
			}
			foreach (PropValue[] entry in entries)
			{
				MailboxResourceMonitorEntry mailboxResourceMonitorEntry = (MailboxResourceMonitorEntry)((object)((default(T) == null) ? Activator.CreateInstance<T>() : default(T)));
				try
				{
					mailboxResourceMonitorEntry.Instantiate(entry);
					mailboxResourceMonitorEntry.MapiSession = base.MapiSession;
					if (mailboxResourceMonitorEntry[MapiPropertyDefinitions.MailboxGuid] != null)
					{
						mailboxResourceMonitorEntry.Identity = new MailboxId(CS$<>8__locals1.databaseId, (Guid)mailboxResourceMonitorEntry[MapiPropertyDefinitions.MailboxGuid]);
					}
					mailboxResourceMonitorEntry.UpdateIdentity(mailboxResourceMonitorEntry.UpdateIdentityFlagsForFinding);
					mailboxResourceMonitorEntry.OriginatingServer = Fqdn.Parse(mailboxResourceMonitorEntry.MapiSession.ServerName);
					mailboxResourceMonitorEntry.ResetChangeTracking(true);
				}
				finally
				{
					mailboxResourceMonitorEntry.Dispose();
				}
				yield return (T)((object)mailboxResourceMonitorEntry);
			}
			yield break;
			yield break;
		}

		// Token: 0x040000DC RID: 220
		private static MapiObjectSchema schema = ObjectSchema.GetInstance<MailboxResourceMonitorEntry.MailboxResourceMonitorEntrySchema>();

		// Token: 0x02000026 RID: 38
		private class MailboxResourceMonitorEntrySchema : MapiObjectSchema
		{
		}
	}
}
