using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000038 RID: 56
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CoreMailboxObject : CoreObject
	{
		// Token: 0x0600052B RID: 1323 RVA: 0x0002AF83 File Offset: 0x00029183
		internal CoreMailboxObject(StoreSession session, PersistablePropertyBag propertyBag, StoreObjectId storeObjectId, byte[] changeKey, ICollection<PropertyDefinition> prefetchProperties) : base(session, propertyBag, storeObjectId, changeKey, Origin.Existing, ItemLevel.TopLevel, prefetchProperties)
		{
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0002AF94 File Offset: 0x00029194
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<CoreMailboxObject>(this);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0002AF9C File Offset: 0x0002919C
		public FolderSaveResult Save()
		{
			this.CheckDisposed(null);
			this.ValidateStoreObject();
			try
			{
				CoreObject.GetPersistablePropertyBag(this).FlushChanges();
			}
			catch (PropertyErrorException ex)
			{
				return new FolderSaveResult(OperationResult.Failed, ex, ex.PropertyErrors);
			}
			CoreObject.GetPersistablePropertyBag(this).SaveChanges(false);
			return FolderPropertyBag.SuccessfulSave;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0002AFF8 File Offset: 0x000291F8
		protected override Schema GetSchema()
		{
			this.CheckDisposed(null);
			return MailboxSchema.Instance;
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0002B006 File Offset: 0x00029206
		protected override StorePropertyDefinition IdProperty
		{
			get
			{
				return InternalSchema.MailboxId;
			}
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0002B010 File Offset: 0x00029210
		private void ValidateStoreObject()
		{
			ValidationContext context = new ValidationContext(base.Session);
			Validation.Validate(this, context);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0002B030 File Offset: 0x00029230
		public void GetLocalReplicationIds(uint numberOfIds, out Guid replicationGuid, out byte[] globCount)
		{
			this.CheckDisposed(null);
			Guid empty = Guid.Empty;
			byte[] array = null;
			StoreSession session = base.Session;
			object thisObject = null;
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
				base.Session.Mailbox.MapiStore.GetLocalRepIds(numberOfIds, out empty, out array);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetLocalRepIds, ex, session, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Failed to get local replication IDs from store", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetLocalRepIds, ex2, session, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Failed to get local replication IDs from store", new object[0]),
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
			replicationGuid = empty;
			globCount = array;
		}
	}
}
