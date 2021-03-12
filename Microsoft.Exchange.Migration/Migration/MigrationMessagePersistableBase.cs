using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000051 RID: 81
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class MigrationMessagePersistableBase : MigrationPersistableBase
	{
		// Token: 0x060003DD RID: 989 RVA: 0x0000E6F4 File Offset: 0x0000C8F4
		public override bool TryLoad(IMigrationDataProvider dataProvider, StoreObjectId id)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			MigrationUtil.ThrowOnNullArgument(id, "id");
			bool success = true;
			MigrationUtil.RunTimedOperation(delegate()
			{
				using (IMigrationStoreObject migrationStoreObject = this.FindStoreObject(dataProvider, id, this.InitializationPropertyDefinitions))
				{
					this.OrganizationId = dataProvider.OrganizationId;
					if (!this.InitializeFromMessageItem(migrationStoreObject))
					{
						success = false;
					}
					else
					{
						migrationStoreObject.Load(this.PropertyDefinitions);
						if (!this.ReadFromMessageItem(migrationStoreObject))
						{
							success = false;
						}
						else
						{
							this.LoadLinkedStoredObjects(migrationStoreObject, dataProvider);
							this.CheckVersion();
						}
					}
				}
			}, this);
			return success;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000E75B File Offset: 0x0000C95B
		public IMigrationMessageItem FindMessageItem(IMigrationDataProvider dataProvider, StoreObjectId id, PropertyDefinition[] properties)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			MigrationUtil.ThrowOnNullArgument(id, "id");
			return dataProvider.FindMessage(id, properties);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000E77B File Offset: 0x0000C97B
		public IMigrationMessageItem FindMessageItem(IMigrationDataProvider dataProvider, PropertyDefinition[] properties)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			ExAssert.RetailAssert(base.StoreObjectId != null, "Need to persist the objects before trying to retrieve their storage object.");
			return this.FindMessageItem(dataProvider, base.StoreObjectId, properties);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000E7AC File Offset: 0x0000C9AC
		public override IMigrationStoreObject FindStoreObject(IMigrationDataProvider dataProvider, StoreObjectId id, PropertyDefinition[] properties)
		{
			return this.FindMessageItem(dataProvider, id, properties);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000E7B7 File Offset: 0x0000C9B7
		protected override IMigrationStoreObject CreateStoreObject(IMigrationDataProvider dataProvider)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			return dataProvider.CreateMessage();
		}
	}
}
