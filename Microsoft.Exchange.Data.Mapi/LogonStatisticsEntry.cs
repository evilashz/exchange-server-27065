using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Mapi.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Mapi;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x0200002F RID: 47
	[Serializable]
	public class LogonStatisticsEntry : MapiObject
	{
		// Token: 0x06000175 RID: 373 RVA: 0x0000A2D4 File Offset: 0x000084D4
		protected sealed override ObjectId GetIdentity()
		{
			object obj;
			if ((obj = this[MapiObjectSchema.Identity]) == null)
			{
				obj = (this[MapiObjectSchema.Identity] = new MailboxId());
			}
			return (ObjectId)obj;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000176 RID: 374 RVA: 0x0000A308 File Offset: 0x00008508
		protected sealed override MapiObject.RetrievePropertiesScope RetrievePropertiesScopeForFinding
		{
			get
			{
				return MapiObject.RetrievePropertiesScope.Database;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000A30B File Offset: 0x0000850B
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return LogonStatisticsEntry.schema;
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000A312 File Offset: 0x00008512
		internal sealed override void Save(bool keepUnmanagedResources)
		{
			throw new NotImplementedException("The method Save is not implemented.");
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000A31E File Offset: 0x0000851E
		internal sealed override void Read(bool keepUnmanagedResources)
		{
			throw new NotImplementedException("The method Read is not implemented.");
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000A32A File Offset: 0x0000852A
		internal sealed override void Delete()
		{
			throw new NotImplementedException("The method Delete is not implemented.");
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000A336 File Offset: 0x00008536
		internal sealed override MapiProp RawMapiEntry
		{
			get
			{
				throw new NotImplementedException("The property RawMapiEntry is not implemented.");
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000A344 File Offset: 0x00008544
		protected sealed override void UpdateIdentity(MapiObject.UpdateIdentityFlags flags)
		{
			bool flag = MapiObject.UpdateIdentityFlags.Nop == (MapiObject.UpdateIdentityFlags.SkipIfExists & flags);
			MapiEntryId mapiEntryId = this.Identity.MapiEntryId;
			DatabaseId mailboxDatabaseId = this.Identity.MailboxDatabaseId;
			Guid mailboxGuid = this.Identity.MailboxGuid;
			string mailboxExchangeLegacyDn = this.Identity.MailboxExchangeLegacyDn;
			if ((MapiObject.UpdateIdentityFlags.LegacyDistinguishedName & flags) != MapiObject.UpdateIdentityFlags.Nop && (string.IsNullOrEmpty(this.Identity.MailboxExchangeLegacyDn) || flag))
			{
				mailboxExchangeLegacyDn = (string)this[LogonStatisticsSchema.FullMailboxDirectoryName];
			}
			this.Identity = new MailboxId(mapiEntryId, mailboxDatabaseId, mailboxGuid, mailboxExchangeLegacyDn);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000A3C9 File Offset: 0x000085C9
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<LogonStatisticsEntry>(this);
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000A3D1 File Offset: 0x000085D1
		// (set) Token: 0x0600017F RID: 383 RVA: 0x0000A3DE File Offset: 0x000085DE
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

		// Token: 0x06000180 RID: 384 RVA: 0x0000A3E7 File Offset: 0x000085E7
		internal override T[] Find<T>(QueryFilter filter, MapiObjectId root, QueryScope scope, SortBy sort, int maximumResultsSize)
		{
			return new List<T>(this.FindPaged<T>(filter, root, scope, sort, 0, maximumResultsSize)).ToArray();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000AAF0 File Offset: 0x00008CF0
		internal override IEnumerable<T> FindPaged<T>(QueryFilter filter, MapiObjectId root, QueryScope scope, SortBy sort, int pageSize, int maximumResultsSize)
		{
			if (!base.GetType().IsAssignableFrom(typeof(T)))
			{
				throw new ArgumentException("T");
			}
			DatabaseId databaseId = root as DatabaseId;
			if (!(null != databaseId))
			{
				throw new NotSupportedException(Strings.ExceptionIdentityTypeInvalid);
			}
			if (MapiObject.RetrievePropertiesScope.Database != this.RetrievePropertiesScopeForFinding && this.RetrievePropertiesScopeForFinding != MapiObject.RetrievePropertiesScope.Instance)
			{
				yield break;
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
			using (LogonStatisticsEntry logonStatisticsEntry = (LogonStatisticsEntry)((object)((default(T) == null) ? Activator.CreateInstance<T>() : default(T))))
			{
				tagsToRead = logonStatisticsEntry.GetPropertyTagsToRead();
			}
			if (tagsToRead == null || tagsToRead.Length == 0)
			{
				tagsToRead = new PropTag[]
				{
					PropTag.MailboxDN
				};
			}
			MdbFlags[] flagsSetToBeTried = new MdbFlags[]
			{
				MdbFlags.Private | MdbFlags.System | MdbFlags.User,
				MdbFlags.Public | MdbFlags.System | MdbFlags.User
			};
			int count = 0;
			MdbFlags[] array = flagsSetToBeTried;
			for (int i = 0; i < array.Length; i++)
			{
				LogonStatisticsEntry.<>c__DisplayClass5<T> CS$<>8__locals3 = new LogonStatisticsEntry.<>c__DisplayClass5<T>();
				CS$<>8__locals3.flags = array[i];
				PropValue[][] entries = null;
				base.MapiSession.InvokeWithWrappedException(delegate()
				{
					ExTraceGlobals.LogonStatisticsTracer.TraceDebug<DatabaseId, string, MdbFlags>((long)this.GetHashCode(), "To Find LogonStatistics from in the database '{0}' on server '{1}' with flag '{2}'.", databaseId, this.MapiSession.ServerName, CS$<>8__locals3.flags);
					entries = this.MapiSession.Administration.GetLogonTable(CS$<>8__locals3.flags, databaseId.Guid, tagsToRead);
				}, Strings.ExceptionFindObject(typeof(T).Name, (null == root) ? Strings.ConstantNull : root.ToString()), databaseId);
				foreach (PropValue[] entry in entries)
				{
					LogonStatisticsEntry logonStatisitcs = (LogonStatisticsEntry)((object)((default(T) == null) ? Activator.CreateInstance<T>() : default(T)));
					try
					{
						logonStatisitcs.Instantiate(entry);
						logonStatisitcs.MapiSession = base.MapiSession;
						logonStatisitcs.UpdateIdentity(logonStatisitcs.UpdateIdentityFlagsForFinding);
						logonStatisitcs.OriginatingServer = Fqdn.Parse(logonStatisitcs.MapiSession.ServerName);
						logonStatisitcs.ResetChangeTrackingAndObjectState();
					}
					finally
					{
						logonStatisitcs.Dispose();
					}
					yield return (T)((object)logonStatisitcs);
					count++;
					if (0 < maximumResultsSize && count == maximumResultsSize)
					{
						yield break;
					}
				}
				if (0 < count)
				{
					break;
				}
			}
			yield break;
		}

		// Token: 0x04000100 RID: 256
		private static MapiObjectSchema schema = ObjectSchema.GetInstance<LogonStatisticsEntry.LogonStatisticsEntrySchema>();

		// Token: 0x02000030 RID: 48
		private class LogonStatisticsEntrySchema : MapiObjectSchema
		{
		}
	}
}
