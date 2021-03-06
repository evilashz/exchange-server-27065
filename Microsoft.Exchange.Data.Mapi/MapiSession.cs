using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Mapi.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Mapi;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x02000034 RID: 52
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class MapiSession : IConfigDataProvider, IDisposeTrackable, IDisposable
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x0000B308 File Offset: 0x00009508
		public void Delete(IConfigurable instance)
		{
			MapiObject mapiObject = instance as MapiObject;
			if (mapiObject == null)
			{
				throw new ArgumentException("instance");
			}
			if (mapiObject.Identity == null)
			{
				throw new ArgumentException(Strings.ExceptionIdentityInvalid);
			}
			if (mapiObject.MapiSession == null)
			{
				mapiObject.MapiSession = this;
			}
			this.InvokeWithWrappedException(delegate()
			{
				mapiObject.Delete();
			}, Strings.ExceptionDeleteObject(instance.Identity.ToString()), null);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000B393 File Offset: 0x00009593
		public IConfigurable[] Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy) where T : IConfigurable, new()
		{
			return (IConfigurable[])this.Find<T>(filter, (MapiObjectId)rootId, deepSearch ? QueryScope.SubTree : QueryScope.OneLevel, sortBy, 0);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000B3B1 File Offset: 0x000095B1
		public IConfigurable Read<T>(ObjectId identity) where T : IConfigurable, new()
		{
			return this.Read<T>(identity, false);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000B3C0 File Offset: 0x000095C0
		public void Save(IConfigurable instance)
		{
			this.Save(instance, false);
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000B3CA File Offset: 0x000095CA
		public string Source
		{
			get
			{
				return this.ServerName;
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000B3D2 File Offset: 0x000095D2
		public IEnumerable<T> FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize) where T : IConfigurable, new()
		{
			return this.FindPaged<T>(filter, (MapiObjectId)rootId, deepSearch ? QueryScope.SubTree : QueryScope.OneLevel, sortBy, pageSize, 0);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000B3ED File Offset: 0x000095ED
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MapiSession>(this);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000B3F5 File Offset: 0x000095F5
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000B40C File Offset: 0x0000960C
		internal static void ThrowWrappedException(LocalizedException exception, LocalizedString message, MapiObjectId source, MapiSession mapiSession)
		{
			if (exception is MapiExceptionNotFound)
			{
				if (source is PublicFolderId)
				{
					throw new PublicFolderNotFoundException(source.ToString(), exception);
				}
				if (source is MailboxId)
				{
					mapiSession.AnalyzeMailboxNotFoundAndThrow((MailboxId)source, exception);
				}
				throw new MapiObjectNotFoundException(message, exception);
			}
			else if (exception is MapiExceptionLogonFailed)
			{
				if (source is PublicFolderId)
				{
					throw new PublicStoreLogonFailedException(mapiSession.ServerName, exception);
				}
				if (source is MailboxId)
				{
					mapiSession.AnalyzeMailboxLogonFailedAndThrow((MailboxId)source, exception);
				}
				throw new MapiLogonFailedException(Strings.LogonFailedExceptionError(message, mapiSession.ServerName), exception);
			}
			else
			{
				if (exception is MapiExceptionSessionLimit)
				{
					throw new MapiLogonFailedException(Strings.SessionLimitExceptionError, exception);
				}
				if (exception is MapiExceptionUnknownMailbox)
				{
					if (source is MailboxId)
					{
						mapiSession.AnalyzeMailboxNotFoundAndThrow((MailboxId)source, exception);
					}
					throw new MapiObjectNotFoundException(message, exception);
				}
				if (exception is MapiExceptionMdbOffline)
				{
					if (mapiSession == null)
					{
						throw new DatabaseUnavailableException(Strings.DatabaseUnavailableExceptionErrorSimple, exception);
					}
					DatabaseId databaseId = null;
					if (source is MailboxId && null != ((MailboxId)source).MailboxDatabaseId)
					{
						databaseId = ((MailboxId)source).MailboxDatabaseId;
					}
					else if (source is DatabaseId)
					{
						databaseId = (DatabaseId)source;
					}
					if (null == databaseId)
					{
						throw new DatabaseUnavailableException(Strings.DatabaseUnavailableExceptionError(mapiSession.ServerName), exception);
					}
					throw new DatabaseUnavailableException(Strings.DatabaseUnavailableByIdentityExceptionError(mapiSession.GetDatabaseIdentityString(databaseId), mapiSession.ServerName), exception);
				}
				else
				{
					if (exception is MapiExceptionPartialCompletion)
					{
						throw new MapiPartialCompletionException(message, exception);
					}
					if (exception is MapiExceptionNetworkError)
					{
						if (mapiSession != null)
						{
							throw new MapiNetworkErrorException(Strings.MapiNetworkErrorExceptionError(mapiSession.ServerName), exception);
						}
						throw new MapiNetworkErrorException(Strings.MapiNetworkErrorExceptionErrorSimple, exception);
					}
					else if (exception is MapiExceptionNoAccess)
					{
						if (null != source)
						{
							throw new MapiAccessDeniedException(Strings.MapiAccessDeniedExceptionError(source.ToString()), exception);
						}
						throw new MapiAccessDeniedException(Strings.MapiAccessDeniedExceptionErrorSimple, exception);
					}
					else
					{
						if (exception is MapiRetryableException)
						{
							throw new MapiTransientException(message, exception);
						}
						if (exception is MapiPermanentException)
						{
							throw new MapiOperationException(message, exception);
						}
						if (exception is MapiInvalidOperationException)
						{
							throw new MapiOperationException(message, exception);
						}
						return;
					}
				}
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000B5F3 File Offset: 0x000097F3
		internal void DisposeCheck()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000B60E File Offset: 0x0000980E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000B61D File Offset: 0x0000981D
		private void ReturnBackConnections()
		{
			if (this.exRpcAdmin != null)
			{
				this.connectionPool.ReturnBack(this.exRpcAdmin);
				this.exRpcAdmin = null;
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000B63F File Offset: 0x0000983F
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					this.ReturnBackConnections();
					if (this.disposeTracker != null)
					{
						this.disposeTracker.Dispose();
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000B688 File Offset: 0x00009888
		public T Read<T>(ObjectId identity, bool keepUnmanagedResources) where T : IConfigurable, new()
		{
			this.DisposeCheck();
			if (!typeof(MapiObject).IsAssignableFrom(typeof(T)))
			{
				throw new ArgumentException("T");
			}
			if (!(identity is MapiObjectId))
			{
				throw new ArgumentException("identity");
			}
			MapiObject mapiObject = ((default(T) == null) ? Activator.CreateInstance<T>() : default(T)) as MapiObject;
			mapiObject.MapiIdentity = (MapiObjectId)identity;
			mapiObject.MapiSession = this;
			this.InvokeWithWrappedException(delegate()
			{
				mapiObject.Read(keepUnmanagedResources);
			}, Strings.ExceptionReadObject(typeof(T).Name, identity.ToString()), null);
			return (T)((object)mapiObject);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000B784 File Offset: 0x00009984
		public void Save(IConfigurable instance, bool keepUnmanagedResources)
		{
			this.DisposeCheck();
			MapiObject mapiObject = instance as MapiObject;
			if (mapiObject == null)
			{
				throw new ArgumentException("instance");
			}
			if (mapiObject.MapiSession == null)
			{
				mapiObject.MapiSession = this;
			}
			ValidationError[] array = mapiObject.Validate();
			if (array != null && 0 < array.Length)
			{
				throw new DataValidationException(array[0]);
			}
			this.InvokeWithWrappedException(delegate()
			{
				mapiObject.Save(keepUnmanagedResources);
			}, (instance.ObjectState == ObjectState.New) ? Strings.ExceptionNewObject((instance.Identity == null) ? Strings.ConstantNull : instance.Identity.ToString()) : Strings.ExceptionSaveObject((instance.Identity == null) ? Strings.ConstantNull : instance.Identity.ToString()), null);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000B85F File Offset: 0x00009A5F
		public T[] Find<T>(QueryFilter filter, MapiObjectId root, QueryScope scope, SortBy sort, int maximumResultsSize) where T : IConfigurable, new()
		{
			return this.Find<T, T>(filter, root, scope, sort, maximumResultsSize);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000B8DC File Offset: 0x00009ADC
		private TTarget[] Find<TSource, TTarget>(QueryFilter filter, MapiObjectId root, QueryScope scope, SortBy sort, int maximumResultsSize) where TSource : IConfigurable, new() where TTarget : IConfigurable, new()
		{
			MapiSession.<>c__DisplayClassa<TSource, TTarget> CS$<>8__locals1 = new MapiSession.<>c__DisplayClassa<TSource, TTarget>();
			CS$<>8__locals1.filter = filter;
			CS$<>8__locals1.root = root;
			CS$<>8__locals1.scope = scope;
			CS$<>8__locals1.sort = sort;
			CS$<>8__locals1.maximumResultsSize = maximumResultsSize;
			this.DisposeCheck();
			if (!typeof(MapiObject).IsAssignableFrom(typeof(TSource)))
			{
				throw new ArgumentException("TSource");
			}
			if (!typeof(MapiObject).IsAssignableFrom(typeof(TTarget)))
			{
				throw new ArgumentException("TTarget");
			}
			CS$<>8__locals1.objects = null;
			using (MapiObject mapiObject = ((default(TSource) == null) ? Activator.CreateInstance<TSource>() : default(TSource)) as MapiObject)
			{
				mapiObject.MapiSession = this;
				this.InvokeWithWrappedException(delegate()
				{
					CS$<>8__locals1.objects = mapiObject.Find<TTarget>(CS$<>8__locals1.filter, CS$<>8__locals1.root, CS$<>8__locals1.scope, CS$<>8__locals1.sort, CS$<>8__locals1.maximumResultsSize);
				}, Strings.ExceptionFindObject(typeof(TTarget).Name, (null == CS$<>8__locals1.root) ? Strings.ConstantNull : CS$<>8__locals1.root.ToString()), null);
			}
			if (CS$<>8__locals1.objects != null)
			{
				return CS$<>8__locals1.objects;
			}
			return new TTarget[0];
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000BA48 File Offset: 0x00009C48
		public IEnumerable<T> FindPaged<T>(QueryFilter filter, MapiObjectId root, QueryScope scope, SortBy sort, int pageSize, int maximumResultsSize) where T : IConfigurable, new()
		{
			return this.FindPaged<T, T>(filter, root, scope, sort, pageSize, maximumResultsSize);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000BAD4 File Offset: 0x00009CD4
		public IEnumerable<TTarget> FindPaged<TSource, TTarget>(QueryFilter filter, MapiObjectId root, QueryScope scope, SortBy sort, int pageSize, int maximumResultsSize) where TSource : IConfigurable, new() where TTarget : IConfigurable, new()
		{
			MapiSession.<>c__DisplayClass10<TSource, TTarget> CS$<>8__locals1 = new MapiSession.<>c__DisplayClass10<TSource, TTarget>();
			CS$<>8__locals1.filter = filter;
			CS$<>8__locals1.root = root;
			CS$<>8__locals1.scope = scope;
			CS$<>8__locals1.sort = sort;
			CS$<>8__locals1.pageSize = pageSize;
			CS$<>8__locals1.maximumResultsSize = maximumResultsSize;
			this.DisposeCheck();
			if (!typeof(MapiObject).IsAssignableFrom(typeof(TSource)))
			{
				throw new ArgumentException("TSource");
			}
			if (!typeof(MapiObject).IsAssignableFrom(typeof(TTarget)))
			{
				throw new ArgumentException("TTarget");
			}
			CS$<>8__locals1.objects = null;
			using (MapiObject mapiObject = ((default(TSource) == null) ? Activator.CreateInstance<TSource>() : default(TSource)) as MapiObject)
			{
				mapiObject.MapiSession = this;
				this.InvokeWithWrappedException(delegate()
				{
					CS$<>8__locals1.objects = mapiObject.FindPaged<TTarget>(CS$<>8__locals1.filter, CS$<>8__locals1.root, CS$<>8__locals1.scope, CS$<>8__locals1.sort, CS$<>8__locals1.pageSize, CS$<>8__locals1.maximumResultsSize);
				}, Strings.ExceptionFindObject(typeof(TTarget).Name, (null == CS$<>8__locals1.root) ? Strings.ConstantNull : CS$<>8__locals1.root.ToString()), null);
			}
			if (CS$<>8__locals1.objects != null)
			{
				return CS$<>8__locals1.objects;
			}
			return (IEnumerable<TTarget>)new TTarget[0];
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000BC4C File Offset: 0x00009E4C
		internal ExRpcAdmin Administration
		{
			get
			{
				ExRpcAdmin result;
				if ((result = this.exRpcAdmin) == null)
				{
					result = (this.exRpcAdmin = this.connectionPool.GetAdministration(this.serverName));
				}
				return result;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000BC7D File Offset: 0x00009E7D
		public bool IsConnectionConfigurated
		{
			get
			{
				return !string.IsNullOrEmpty(this.serverExchangeLegacyDn);
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000BC8D File Offset: 0x00009E8D
		public void RedirectServer(string serverExchangeLegacyDn, Fqdn serverFqdn)
		{
			if (string.Equals(this.serverExchangeLegacyDn, serverExchangeLegacyDn, StringComparison.OrdinalIgnoreCase))
			{
				return;
			}
			this.ReturnBackConnections();
			this.serverName = serverFqdn.ToString().ToLowerInvariant();
			this.serverExchangeLegacyDn = serverExchangeLegacyDn.ToLowerInvariant();
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000BCC2 File Offset: 0x00009EC2
		public MapiSession(string serverExchangeLegacyDn, Fqdn serverFqdn) : this(serverExchangeLegacyDn, serverFqdn, ConsistencyMode.PartiallyConsistent)
		{
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		public MapiSession(string serverExchangeLegacyDn, Fqdn serverFqdn, ConsistencyMode consistencyMode)
		{
			this.serverName = serverFqdn.ToString().ToLowerInvariant();
			this.serverExchangeLegacyDn = serverExchangeLegacyDn.ToLowerInvariant();
			this.consistencyMode = consistencyMode;
			this.disposeTracker = this.GetDisposeTracker();
			this.disposed = false;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000BD25 File Offset: 0x00009F25
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000BD2D File Offset: 0x00009F2D
		public string ServerExchangeLegacyDn
		{
			get
			{
				return this.serverExchangeLegacyDn;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000BD35 File Offset: 0x00009F35
		// (set) Token: 0x060001CB RID: 459 RVA: 0x0000BD3D File Offset: 0x00009F3D
		public ConsistencyMode ConsistencyMode
		{
			get
			{
				return this.consistencyMode;
			}
			set
			{
				this.consistencyMode = value;
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000BD48 File Offset: 0x00009F48
		protected static string GetCommonNameFromLegacyDistinguishName(string legacyDn)
		{
			if (string.IsNullOrEmpty(legacyDn))
			{
				return null;
			}
			int num = legacyDn.LastIndexOf("/cn=", StringComparison.OrdinalIgnoreCase);
			if (-1 != num)
			{
				return legacyDn.Substring("/cn=".Length + num);
			}
			return null;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000BD84 File Offset: 0x00009F84
		internal bool CheckIfInconsistentObjectDisallowed(LocalizedString message)
		{
			if (ConsistencyMode.IgnoreInvalid == this.ConsistencyMode)
			{
				return true;
			}
			if (this.ConsistencyMode == ConsistencyMode.FullyConsistent)
			{
				throw new MapiInconsistentObjectException(message);
			}
			return false;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000BDA4 File Offset: 0x00009FA4
		private string GetDatabaseIdentityString(DatabaseId databaseId)
		{
			this.DisposeCheck();
			if (null == databaseId)
			{
				throw new ArgumentNullException("databaseId");
			}
			if (string.IsNullOrEmpty(databaseId.DatabaseName))
			{
				try
				{
					MdbStatus databaseStatus = this.GetDatabaseStatus(databaseId, false);
					if (databaseStatus == null)
					{
						ExTraceGlobals.MapiSessionTracer.TraceError<DatabaseId>((long)this.GetHashCode(), "Cannot get status for database '{0}'", databaseId);
					}
					else
					{
						databaseId = new DatabaseId(databaseId.MapiEntryId, databaseStatus.VServerName, databaseStatus.MdbName, databaseId.Guid);
					}
				}
				catch (MapiTransientException ex)
				{
					ExTraceGlobals.MapiSessionTracer.TraceError<DatabaseId, string>((long)this.GetHashCode(), "Getting status for database '{0}' caught an exception: '{1}'", databaseId, ex.Message);
				}
				catch (MapiOperationException ex2)
				{
					ExTraceGlobals.MapiSessionTracer.TraceError<DatabaseId, string>((long)this.GetHashCode(), "Getting status for database '{0}' caught an exception: '{1}'", databaseId, ex2.Message);
				}
			}
			return databaseId.ToString();
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000BE84 File Offset: 0x0000A084
		private void AnalyzeMailboxNotFoundAndThrow(MailboxId mailboxId, Exception innerException)
		{
			if (null == mailboxId)
			{
				throw new ArgumentNullException("mailboxId");
			}
			if (null != mailboxId.MailboxDatabaseId)
			{
				throw new MailboxNotFoundException(Strings.MailboxNotFoundInDatabaseExceptionError(mailboxId.MailboxGuid.ToString(), this.GetDatabaseIdentityString(mailboxId.MailboxDatabaseId)), innerException);
			}
			if (string.IsNullOrEmpty(mailboxId.MailboxExchangeLegacyDn))
			{
				throw new MailboxNotFoundException(Strings.MailboxNotFoundExceptionError(mailboxId.ToString()), innerException);
			}
			throw new MailboxNotFoundException(Strings.MailboxNotFoundExceptionError(mailboxId.MailboxExchangeLegacyDn), innerException);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000BF10 File Offset: 0x0000A110
		private void AnalyzeMailboxLogonFailedAndThrow(MailboxId mailboxId, Exception innerException)
		{
			if (null == mailboxId)
			{
				throw new ArgumentNullException("mailboxId");
			}
			if (null != mailboxId.MailboxDatabaseId)
			{
				throw new MailboxLogonFailedException(Strings.MailboxLogonFailedInDatabaseExceptionError(mailboxId.MailboxGuid.ToString(), this.GetDatabaseIdentityString(mailboxId.MailboxDatabaseId), this.ServerName), innerException);
			}
			if (string.IsNullOrEmpty(mailboxId.MailboxExchangeLegacyDn))
			{
				throw new MailboxLogonFailedException(Strings.MailboxLogonFailedExceptionError(mailboxId.ToString(), this.ServerName), innerException);
			}
			throw new MailboxLogonFailedException(Strings.MailboxLogonFailedExceptionError(mailboxId.MailboxExchangeLegacyDn, this.ServerName), innerException);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000BFAD File Offset: 0x0000A1AD
		internal void InvokeWithWrappedException(ParameterlessReturnlessDelegate invoke, LocalizedString message, MapiObjectId source)
		{
			this.InvokeWithWrappedException(invoke, message, source, null);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000BFBC File Offset: 0x0000A1BC
		internal void InvokeWithWrappedException(ParameterlessReturnlessDelegate invoke, LocalizedString message, MapiObjectId source, MapiSession.ErrorTranslatorDelegate translateError)
		{
			try
			{
				invoke();
			}
			catch (MapiRetryableException ex)
			{
				LocalizedException ex2;
				if (translateError != null && (ex2 = translateError(ref message, ex)) != null)
				{
					throw ex2;
				}
				MapiSession.ThrowWrappedException(ex, message, source, this);
			}
			catch (MapiPermanentException ex3)
			{
				LocalizedException ex2;
				if (translateError != null && (ex2 = translateError(ref message, ex3)) != null)
				{
					throw ex2;
				}
				MapiSession.ThrowWrappedException(ex3, message, source, this);
			}
			catch (MapiInvalidOperationException ex4)
			{
				LocalizedException ex2;
				if (translateError != null && (ex2 = translateError(ref message, ex4)) != null)
				{
					throw ex2;
				}
				MapiSession.ThrowWrappedException(ex4, message, source, this);
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000C05C File Offset: 0x0000A25C
		public MdbStatus GetDatabaseStatus(DatabaseId databaseId)
		{
			return this.GetDatabaseStatus(databaseId, true);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000C068 File Offset: 0x0000A268
		public MdbStatus GetDatabaseStatus(DatabaseId databaseId, bool basicInformation)
		{
			MdbStatus[] databaseStatus = this.GetDatabaseStatus(new DatabaseId[]
			{
				databaseId
			}, basicInformation);
			if (databaseStatus.Length != 0)
			{
				return databaseStatus[0];
			}
			return null;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000C094 File Offset: 0x0000A294
		public MdbStatus[] GetDatabaseStatus(DatabaseId[] databaseIds, bool basicInformation)
		{
			this.DisposeCheck();
			List<Guid> list = new List<Guid>();
			if (databaseIds != null && 0 < databaseIds.Length)
			{
				list.Capacity = databaseIds.Length;
				foreach (DatabaseId databaseId in databaseIds)
				{
					if (null == databaseId)
					{
						throw new ArgumentException("databaseIds");
					}
					list.Add(databaseId.Guid);
				}
			}
			MdbStatus[] result;
			try
			{
				MdbStatus[] array;
				if (list.Count == 0)
				{
					array = this.Administration.ListMdbStatus(basicInformation);
				}
				else
				{
					array = this.Administration.ListMdbStatus(list.ToArray());
					if (!basicInformation && array != null && 0 < array.Length)
					{
						Dictionary<Guid, MdbStatus> dictionary = new Dictionary<Guid, MdbStatus>(array.Length);
						foreach (MdbStatus mdbStatus in array)
						{
							dictionary[mdbStatus.MdbGuid] = mdbStatus;
						}
						MdbStatus[] array3 = this.Administration.ListMdbStatus(basicInformation);
						if (array3 != null && 0 < array3.Length)
						{
							foreach (MdbStatus mdbStatus2 in array3)
							{
								dictionary[mdbStatus2.MdbGuid] = mdbStatus2;
							}
						}
						List<MdbStatus> list2 = new List<MdbStatus>(list.Count);
						foreach (Guid key in list)
						{
							list2.Add(dictionary[key]);
						}
						array = list2.ToArray();
					}
				}
				result = array;
			}
			catch (MapiExceptionNetworkError innerException)
			{
				throw new MapiNetworkErrorException(Strings.MapiNetworkErrorExceptionError(this.ServerName), innerException);
			}
			catch (MapiRetryableException ex)
			{
				throw new MapiTransientException(Strings.ExceptionGetDatabaseStatus(ex.Message), ex);
			}
			catch (MapiPermanentException ex2)
			{
				throw new MapiOperationException(Strings.ExceptionGetDatabaseStatus(ex2.Message), ex2);
			}
			return result;
		}

		// Token: 0x04000122 RID: 290
		internal const string LiteralApplicationIdentity = "Client=MSExchangeRPC;Action=MapiDriver";

		// Token: 0x04000123 RID: 291
		private bool disposed;

		// Token: 0x04000124 RID: 292
		private string serverName;

		// Token: 0x04000125 RID: 293
		private string serverExchangeLegacyDn;

		// Token: 0x04000126 RID: 294
		private ConsistencyMode consistencyMode;

		// Token: 0x04000127 RID: 295
		private ExRpcAdmin exRpcAdmin;

		// Token: 0x04000128 RID: 296
		private DisposeTracker disposeTracker;

		// Token: 0x04000129 RID: 297
		private ConnectionPool connectionPool = ConnectionPool.Instance;

		// Token: 0x02000035 RID: 53
		// (Invoke) Token: 0x060001D7 RID: 471
		internal delegate LocalizedException ErrorTranslatorDelegate(ref LocalizedString localizedString, Exception innerException);
	}
}
