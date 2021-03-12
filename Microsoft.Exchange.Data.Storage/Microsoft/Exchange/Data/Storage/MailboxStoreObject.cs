using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000016 RID: 22
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class MailboxStoreObject : StoreObject
	{
		// Token: 0x0600027C RID: 636 RVA: 0x00013FCA File Offset: 0x000121CA
		private MailboxStoreObject(CoreMailboxObject coreObject) : base(coreObject, false)
		{
			this.mailbox = new Mailbox(this);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00013FE0 File Offset: 0x000121E0
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MailboxStoreObject>(this);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00013FE8 File Offset: 0x000121E8
		internal static MailboxStoreObject Bind(StoreSession session, MapiStore mapiStore, ICollection<PropertyDefinition> requestedProperties)
		{
			return MailboxStoreObject.Bind(session, mapiStore, requestedProperties, true, false);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00013FF4 File Offset: 0x000121F4
		internal static MailboxStoreObject Bind(StoreSession session, MapiStore mapiStore, ICollection<PropertyDefinition> requestedProperties, bool getMappingSignature, bool overridePropertyList)
		{
			ICollection<PropertyDefinition> collection = InternalSchema.Combine<PropertyDefinition>(overridePropertyList ? new PropertyTagPropertyDefinition[]
			{
				MailboxSchema.MailboxType,
				MailboxSchema.MailboxTypeDetail
			} : MailboxSchema.Instance.AutoloadProperties, requestedProperties);
			PersistablePropertyBag persistablePropertyBag = null;
			CoreMailboxObject coreMailboxObject = null;
			MailboxStoreObject mailboxStoreObject = null;
			bool flag = false;
			MailboxStoreObject result;
			try
			{
				byte[] array = null;
				if (getMappingSignature)
				{
					object thisObject = null;
					bool flag2 = false;
					try
					{
						if (session != null)
						{
							session.BeginMapiCall();
							session.BeginServerHealthCall();
							flag2 = true;
						}
						if (StorageGlobals.MapiTestHookBeforeCall != null)
						{
							StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
						}
						using (MapiFolder rootFolder = mapiStore.GetRootFolder())
						{
							array = (rootFolder.GetProp(PropTag.MappingSignature).Value as byte[]);
						}
					}
					catch (MapiPermanentException ex)
					{
						throw StorageGlobals.TranslateMapiException(ServerStrings.StoreOperationFailed, ex, session, thisObject, "{0}. MapiException = {1}.", new object[]
						{
							string.Format("Failed to get mapping signature.", new object[0]),
							ex
						});
					}
					catch (MapiRetryableException ex2)
					{
						throw StorageGlobals.TranslateMapiException(ServerStrings.StoreOperationFailed, ex2, session, thisObject, "{0}. MapiException = {1}.", new object[]
						{
							string.Format("Failed to get mapping signature.", new object[0]),
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
								if (flag2)
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
				if (array != null)
				{
					session.MappingSignature = Convert.ToBase64String(array);
				}
				persistablePropertyBag = new StoreObjectPropertyBag(session, mapiStore, collection);
				coreMailboxObject = new CoreMailboxObject(session, persistablePropertyBag, null, null, collection);
				mailboxStoreObject = new MailboxStoreObject(coreMailboxObject);
				flag = true;
				result = mailboxStoreObject;
			}
			finally
			{
				if (!flag)
				{
					if (mailboxStoreObject != null)
					{
						mailboxStoreObject.Dispose();
						mailboxStoreObject = null;
					}
					if (coreMailboxObject != null)
					{
						coreMailboxObject.Dispose();
						coreMailboxObject = null;
					}
					if (persistablePropertyBag != null)
					{
						persistablePropertyBag.Dispose();
						persistablePropertyBag = null;
					}
				}
			}
			return result;
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00014248 File Offset: 0x00012448
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return MailboxSchema.Instance;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0001425A File Offset: 0x0001245A
		internal bool IsContentIndexingEnabled
		{
			get
			{
				this.CheckDisposed("IsContentIndexingEnabled::get");
				return base.GetValueOrDefault<bool>(InternalSchema.IsContentIndexingEnabled);
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00014274 File Offset: 0x00012474
		internal void Save()
		{
			FolderSaveResult folderSaveResult = ((CoreMailboxObject)base.CoreObject).Save();
			if (folderSaveResult.OperationResult != OperationResult.Succeeded)
			{
				throw folderSaveResult.ToException(ServerStrings.ErrorFolderSave(base.CoreObject.Id.ObjectId.ToString(), folderSaveResult.ToString()));
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000283 RID: 643 RVA: 0x000142C2 File Offset: 0x000124C2
		// (set) Token: 0x06000284 RID: 644 RVA: 0x000142D4 File Offset: 0x000124D4
		public override string ClassName
		{
			get
			{
				this.CheckDisposed("ClassName::get.");
				return "Mailbox";
			}
			set
			{
				this.CheckDisposed("ClassName::set.");
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x000142E8 File Offset: 0x000124E8
		internal void ForceReload(params PropertyDefinition[] propsToLoad)
		{
			this.CheckDisposed("ForceReload");
			if (propsToLoad == null)
			{
				throw new ArgumentNullException("propsToLoad");
			}
			StoreObjectPropertyBag storeObjectPropertyBag = (StoreObjectPropertyBag)base.PropertyBag;
			storeObjectPropertyBag.ForceReload(propsToLoad);
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000286 RID: 646 RVA: 0x00014321 File Offset: 0x00012521
		internal Mailbox Mailbox
		{
			get
			{
				this.CheckDisposed("Mailbox::get");
				return this.mailbox;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00014334 File Offset: 0x00012534
		internal MapiStore MapiStore
		{
			get
			{
				return (MapiStore)base.MapiProp;
			}
		}

		// Token: 0x040000A8 RID: 168
		private readonly Mailbox mailbox;
	}
}
