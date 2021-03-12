using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200006F RID: 111
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class SynchronizationUploadContextBase<TMapiCollector> : DisposableObject, ISynchronizationUploadContextBase, IDisposable where TMapiCollector : MapiUnk
	{
		// Token: 0x060007B7 RID: 1975 RVA: 0x0003C578 File Offset: 0x0003A778
		protected SynchronizationUploadContextBase(CoreFolder folder, StorageIcsState initialState)
		{
			Util.ThrowOnNullArgument(folder, "folder");
			this.folder = folder;
			StoreSession session = this.Session;
			bool flag = false;
			try
			{
				if (session != null)
				{
					session.BeginMapiCall();
					session.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				this.mapiCollector = this.MapiCreateCollector(initialState);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.CannotCreateCollector(base.GetType()), ex, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("SynchronizationUploadContext::MapiCreateCollector failed", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.CannotCreateCollector(base.GetType()), ex2, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("SynchronizationUploadContext::MapiCreateCollector failed", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (session != null)
					{
						session.EndMapiCall();
						if (flag)
						{
							session.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			this.mapiCollector.AllowWarnings = true;
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0003C6DC File Offset: 0x0003A8DC
		public StoreSession Session
		{
			get
			{
				this.CheckDisposed(null);
				return this.folder.Session;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0003C6F0 File Offset: 0x0003A8F0
		public StoreObjectId SyncRootFolderId
		{
			get
			{
				this.CheckDisposed(null);
				return this.folder.Id.ObjectId;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x0003C709 File Offset: 0x0003A909
		public CoreFolder CoreFolder
		{
			get
			{
				this.CheckDisposed(null);
				return this.folder;
			}
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0003C718 File Offset: 0x0003A918
		public void GetCurrentState(ref StorageIcsState currentState)
		{
			this.CheckDisposed(null);
			StoreSession session = this.Session;
			bool flag = false;
			try
			{
				if (session != null)
				{
					session.BeginMapiCall();
					session.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				this.MapiGetCurrentState(ref currentState);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.CannotObtainSynchronizationUploadState(typeof(TMapiCollector)), ex, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("SynchronizationUploadContext::GetCurrentState failed", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.CannotObtainSynchronizationUploadState(typeof(TMapiCollector)), ex2, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("SynchronizationUploadContext::GetCurrentState failed", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (session != null)
					{
						session.EndMapiCall();
						if (flag)
						{
							session.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0003C854 File Offset: 0x0003AA54
		internal void ImportDeletes(DeleteItemFlags deleteItemFlags, byte[][] sourceKeys)
		{
			this.CheckDisposed(null);
			Util.ThrowOnNullArgument(sourceKeys, "sourceKeys");
			ImportDeletionFlags importDeletionFlags = ((deleteItemFlags & DeleteItemFlags.HardDelete) == DeleteItemFlags.HardDelete) ? ImportDeletionFlags.HardDelete : ImportDeletionFlags.None;
			PropValue[] array = new PropValue[sourceKeys.Length];
			for (int i = 0; i < sourceKeys.Length; i++)
			{
				array[i] = new PropValue(PropTag.SourceKey, sourceKeys[i]);
			}
			StoreSession session = this.Session;
			bool flag = false;
			try
			{
				if (session != null)
				{
					session.BeginMapiCall();
					session.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				this.MapiImportDeletes(importDeletionFlags, array);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.CannotImportDeletion, ex, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Import of object deletions failed", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.CannotImportDeletion, ex2, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Import of object deletions failed", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (session != null)
					{
						session.EndMapiCall();
						if (flag)
						{
							session.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0003C9C8 File Offset: 0x0003ABC8
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				Util.DisposeIfPresent(this.mapiCollector);
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x0003C9E4 File Offset: 0x0003ABE4
		protected TMapiCollector MapiCollector
		{
			get
			{
				return this.mapiCollector;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x0003C9EC File Offset: 0x0003ABEC
		protected MapiFolder MapiFolder
		{
			get
			{
				return (MapiFolder)CoreObject.GetPersistablePropertyBag(this.folder).MapiProp;
			}
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0003CA03 File Offset: 0x0003AC03
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SynchronizationUploadContextBase<TMapiCollector>>(this);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0003CA0C File Offset: 0x0003AC0C
		protected List<PropValue> GetPropValuesFromValues(ExTimeZone exTimeZone, IList<PropertyDefinition> propertyDefinitions, IList<object> propertyValues)
		{
			ICollection<PropTag> collection = PropertyTagCache.Cache.PropTagsFromPropertyDefinitions<PropertyDefinition>(this.Session.Mailbox.MapiStore, this.Session, false, propertyDefinitions);
			List<PropValue> list = new List<PropValue>(propertyDefinitions.Count);
			int num = 0;
			foreach (PropTag propTag in collection)
			{
				InternalSchema.CheckPropertyValueType(propertyDefinitions[num], propertyValues[num]);
				list.Add(MapiPropertyBag.GetPropValueFromValue(this.Session, exTimeZone, propTag, propertyValues[num]));
				num++;
			}
			return list;
		}

		// Token: 0x060007C2 RID: 1986
		protected abstract TMapiCollector MapiCreateCollector(StorageIcsState initialState);

		// Token: 0x060007C3 RID: 1987
		protected abstract void MapiGetCurrentState(ref StorageIcsState finalState);

		// Token: 0x060007C4 RID: 1988
		protected abstract void MapiImportDeletes(ImportDeletionFlags importDeletionFlags, PropValue[] sourceKeys);

		// Token: 0x0400021A RID: 538
		private readonly TMapiCollector mapiCollector = default(TMapiCollector);

		// Token: 0x0400021B RID: 539
		private readonly CoreFolder folder;
	}
}
