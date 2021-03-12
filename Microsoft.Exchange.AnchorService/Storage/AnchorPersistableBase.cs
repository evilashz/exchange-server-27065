using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AnchorService.Storage
{
	// Token: 0x02000039 RID: 57
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AnchorPersistableBase : IAnchorPersistable, IAnchorSerializable
	{
		// Token: 0x06000261 RID: 609 RVA: 0x00008F74 File Offset: 0x00007174
		protected AnchorPersistableBase(AnchorContext context)
		{
			this.AnchorContext = context;
			this.ExtendedProperties = new PersistableDictionary();
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00008F8E File Offset: 0x0000718E
		// (set) Token: 0x06000263 RID: 611 RVA: 0x00008F96 File Offset: 0x00007196
		public StoreObjectId StoreObjectId { get; protected set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000264 RID: 612 RVA: 0x00008F9F File Offset: 0x0000719F
		// (set) Token: 0x06000265 RID: 613 RVA: 0x00008FA7 File Offset: 0x000071A7
		public ExDateTime CreationTime { get; protected set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00008FB0 File Offset: 0x000071B0
		// (set) Token: 0x06000267 RID: 615 RVA: 0x00008FB8 File Offset: 0x000071B8
		public PersistableDictionary ExtendedProperties { get; protected set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00008FC1 File Offset: 0x000071C1
		public virtual PropertyDefinition[] PropertyDefinitions
		{
			get
			{
				return AnchorPersistableBase.MigrationBaseDefinitions;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00008FC8 File Offset: 0x000071C8
		// (set) Token: 0x0600026A RID: 618 RVA: 0x00008FD0 File Offset: 0x000071D0
		protected AnchorContext AnchorContext { get; set; }

		// Token: 0x0600026B RID: 619 RVA: 0x00008FD9 File Offset: 0x000071D9
		public bool TryLoad(IAnchorDataProvider dataProvider, StoreObjectId id)
		{
			return this.TryLoad(dataProvider, id, null);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000090A0 File Offset: 0x000072A0
		public virtual bool TryLoad(IAnchorDataProvider dataProvider, StoreObjectId id, Action<IAnchorStoreObject> streamAction)
		{
			AnchorUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			AnchorUtil.ThrowOnNullArgument(id, "id");
			bool success = true;
			AnchorUtil.RunTimedOperation(dataProvider.AnchorContext, delegate()
			{
				using (IAnchorStoreObject anchorStoreObject = this.FindStoreObject(dataProvider, id, this.PropertyDefinitions))
				{
					if (anchorStoreObject.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationPersistableDictionary, null) == null)
					{
						success = false;
					}
					else
					{
						anchorStoreObject.Load(this.PropertyDefinitions);
						if (!this.ReadFromMessageItem(anchorStoreObject))
						{
							success = false;
						}
						else
						{
							if (streamAction != null)
							{
								streamAction(anchorStoreObject);
							}
							this.LoadLinkedStoredObjects(anchorStoreObject, dataProvider);
						}
					}
				}
			}, this);
			return success;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00009119 File Offset: 0x00007319
		public virtual bool ReadFromMessageItem(IAnchorStoreObject message)
		{
			AnchorUtil.ThrowOnNullArgument(message, "message");
			this.ExtendedProperties = AnchorHelper.GetDictionaryProperty(message, MigrationBatchMessageSchema.MigrationPersistableDictionary, true);
			this.ReadStoreObjectIdProperties(message);
			return true;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00009140 File Offset: 0x00007340
		public virtual void CreateInStore(IAnchorDataProvider dataProvider, Action<IAnchorStoreObject> streamAction)
		{
			AnchorUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			AnchorUtil.AssertOrThrow(this.StoreObjectId == null, "Object should not have been created already.", new object[0]);
			using (IAnchorStoreObject anchorStoreObject = this.CreateStoreObject(dataProvider))
			{
				if (streamAction != null)
				{
					streamAction(anchorStoreObject);
				}
				this.WriteToMessageItem(anchorStoreObject, false);
				anchorStoreObject.Save(SaveMode.FailOnAnyConflict);
				anchorStoreObject.Load(this.PropertyDefinitions);
				this.ReadStoreObjectIdProperties(anchorStoreObject);
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000091C4 File Offset: 0x000073C4
		public virtual void WriteToMessageItem(IAnchorStoreObject message, bool loaded)
		{
			AnchorUtil.ThrowOnNullArgument(message, "message");
			this.WriteExtendedPropertiesToMessageItem(message);
		}

		// Token: 0x06000270 RID: 624
		public abstract IAnchorStoreObject FindStoreObject(IAnchorDataProvider dataProvider, StoreObjectId id, PropertyDefinition[] properties);

		// Token: 0x06000271 RID: 625 RVA: 0x000091D8 File Offset: 0x000073D8
		public IAnchorStoreObject FindStoreObject(IAnchorDataProvider dataProvider, PropertyDefinition[] properties)
		{
			ExAssert.RetailAssert(this.StoreObjectId != null, "Need to persist the objects before trying to retrieve their storage object.");
			if (properties == null)
			{
				properties = this.PropertyDefinitions;
			}
			return this.FindStoreObject(dataProvider, this.StoreObjectId, properties);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00009209 File Offset: 0x00007409
		public IAnchorStoreObject FindStoreObject(IAnchorDataProvider dataProvider)
		{
			return this.FindStoreObject(dataProvider, null);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00009213 File Offset: 0x00007413
		protected virtual void LoadLinkedStoredObjects(IAnchorStoreObject item, IAnchorDataProvider dataProvider)
		{
		}

		// Token: 0x06000274 RID: 628
		protected abstract IAnchorStoreObject CreateStoreObject(IAnchorDataProvider dataProvider);

		// Token: 0x06000275 RID: 629 RVA: 0x00009218 File Offset: 0x00007418
		protected virtual void SaveExtendedProperties(IAnchorDataProvider provider)
		{
			AnchorUtil.AssertOrThrow(this.StoreObjectId != null, "Object should have been created before trying to save properties.", new object[0]);
			using (IAnchorStoreObject anchorStoreObject = this.FindStoreObject(provider, AnchorPersistableBase.MigrationBaseDefinitions))
			{
				anchorStoreObject.OpenAsReadWrite();
				this.WriteExtendedPropertiesToMessageItem(anchorStoreObject);
				anchorStoreObject.Save(SaveMode.FailOnAnyConflict);
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00009280 File Offset: 0x00007480
		protected virtual void WriteExtendedPropertiesToMessageItem(IAnchorStoreObject message)
		{
			AnchorHelper.SetDictionaryProperty(message, MigrationBatchMessageSchema.MigrationPersistableDictionary, this.ExtendedProperties);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00009293 File Offset: 0x00007493
		protected void ReadStoreObjectIdProperties(IAnchorStoreObject storeObject)
		{
			this.StoreObjectId = storeObject.Id;
			this.CreationTime = storeObject.CreationTime;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000092B0 File Offset: 0x000074B0
		private static object GetDiagnosticObject(object obj)
		{
			byte[] array = obj as byte[];
			if (array != null)
			{
				try
				{
					obj = new Guid(array);
				}
				catch (ArgumentException)
				{
				}
			}
			return obj;
		}

		// Token: 0x040000A1 RID: 161
		protected static readonly PropertyDefinition[] MigrationBaseDefinitions = new PropertyDefinition[]
		{
			MigrationBatchMessageSchema.MigrationPersistableDictionary
		};
	}
}
