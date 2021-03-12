using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000017 RID: 23
	public sealed class UserInfoTable
	{
		// Token: 0x06000426 RID: 1062 RVA: 0x0002ADFC File Offset: 0x00028FFC
		internal UserInfoTable()
		{
			this.userGuid = Factory.CreatePhysicalColumn("UserGuid", "UserGuid", typeof(Guid), false, false, false, false, false, Visibility.Public, 0, 16, 16);
			this.status = Factory.CreatePhysicalColumn("Status", "Status", typeof(short), false, false, false, false, false, Visibility.Public, 0, 2, 2);
			this.deletedOn = Factory.CreatePhysicalColumn("DeletedOn", "DeletedOn", typeof(DateTime), true, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.creationTime = Factory.CreatePhysicalColumn("CreationTime", "CreationTime", typeof(DateTime), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.lastModificationTime = Factory.CreatePhysicalColumn("LastModificationTime", "LastModificationTime", typeof(DateTime), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.changeNumber = Factory.CreatePhysicalColumn("ChangeNumber", "ChangeNumber", typeof(long), false, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.lastInteractiveLogonTime = Factory.CreatePhysicalColumn("LastInteractiveLogonTime", "LastInteractiveLogonTime", typeof(DateTime), true, false, false, false, false, Visibility.Public, 0, 8, 8);
			this.propertyBlob = Factory.CreatePhysicalColumn("PropertyBlob", "PropertyBlob", typeof(byte[]), true, false, false, false, false, Visibility.Public, 1073741824, 0, 30000);
			this.extensionBlob = Factory.CreatePhysicalColumn("ExtensionBlob", "ExtensionBlob", typeof(byte[]), true, false, false, false, false, Visibility.Public, 1073741824, 0, 1073741824);
			string name = "UserInfoPK";
			bool primaryKey = true;
			bool unique = true;
			bool schemaExtension = false;
			bool[] conditional = new bool[1];
			this.userInfoPK = new Index(name, primaryKey, unique, schemaExtension, conditional, new bool[]
			{
				true
			}, new PhysicalColumn[]
			{
				this.UserGuid
			});
			Index[] indexes = new Index[]
			{
				this.UserInfoPK
			};
			SpecialColumns specialCols = new SpecialColumns(this.PropertyBlob, null, null, 0);
			PhysicalColumn[] computedColumns = new PhysicalColumn[0];
			PhysicalColumn[] columns = new PhysicalColumn[]
			{
				this.UserGuid,
				this.Status,
				this.DeletedOn,
				this.CreationTime,
				this.LastModificationTime,
				this.ChangeNumber,
				this.LastInteractiveLogonTime,
				this.PropertyBlob,
				this.ExtensionBlob
			};
			this.table = Factory.CreateTable("UserInfo", TableClass.UserInfo, CultureHelper.DefaultCultureInfo, true, TableAccessHints.None, false, Visibility.Redacted, true, specialCols, indexes, computedColumns, columns);
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0002B07B File Offset: 0x0002927B
		public Table Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0002B083 File Offset: 0x00029283
		public PhysicalColumn UserGuid
		{
			get
			{
				return this.userGuid;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0002B08B File Offset: 0x0002928B
		public PhysicalColumn Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0002B093 File Offset: 0x00029293
		public PhysicalColumn DeletedOn
		{
			get
			{
				return this.deletedOn;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x0002B09B File Offset: 0x0002929B
		public PhysicalColumn CreationTime
		{
			get
			{
				return this.creationTime;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0002B0A3 File Offset: 0x000292A3
		public PhysicalColumn LastModificationTime
		{
			get
			{
				return this.lastModificationTime;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0002B0AB File Offset: 0x000292AB
		public PhysicalColumn ChangeNumber
		{
			get
			{
				return this.changeNumber;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0002B0B3 File Offset: 0x000292B3
		public PhysicalColumn LastInteractiveLogonTime
		{
			get
			{
				return this.lastInteractiveLogonTime;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0002B0BB File Offset: 0x000292BB
		public PhysicalColumn PropertyBlob
		{
			get
			{
				return this.propertyBlob;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0002B0C3 File Offset: 0x000292C3
		public PhysicalColumn ExtensionBlob
		{
			get
			{
				return this.extensionBlob;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0002B0CB File Offset: 0x000292CB
		public Index UserInfoPK
		{
			get
			{
				return this.userInfoPK;
			}
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0002B0D4 File Offset: 0x000292D4
		internal void PostMountInitialize(ComponentVersion databaseVersion)
		{
			PhysicalColumn physicalColumn = this.userGuid;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.userGuid = null;
			}
			physicalColumn = this.status;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.status = null;
			}
			physicalColumn = this.deletedOn;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.deletedOn = null;
			}
			physicalColumn = this.creationTime;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.creationTime = null;
			}
			physicalColumn = this.lastModificationTime;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.lastModificationTime = null;
			}
			physicalColumn = this.changeNumber;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.changeNumber = null;
			}
			physicalColumn = this.lastInteractiveLogonTime;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.lastInteractiveLogonTime = null;
			}
			physicalColumn = this.propertyBlob;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.propertyBlob = null;
			}
			physicalColumn = this.extensionBlob;
			if (physicalColumn.MinVersion > databaseVersion.Value || databaseVersion.Value > physicalColumn.MaxVersion)
			{
				this.Table.Columns.Remove(physicalColumn);
				this.Table.CommonColumns.Remove(physicalColumn);
				this.extensionBlob = null;
			}
			for (int i = this.Table.Columns.Count - 1; i >= 0; i--)
			{
				this.Table.Columns[i].Index = i;
			}
			Index index = this.userInfoPK;
			if (index.MinVersion > databaseVersion.Value || databaseVersion.Value > index.MaxVersion)
			{
				this.userInfoPK = null;
				this.Table.Indexes.Remove(index);
			}
		}

		// Token: 0x040001F5 RID: 501
		public const string UserGuidName = "UserGuid";

		// Token: 0x040001F6 RID: 502
		public const string StatusName = "Status";

		// Token: 0x040001F7 RID: 503
		public const string DeletedOnName = "DeletedOn";

		// Token: 0x040001F8 RID: 504
		public const string CreationTimeName = "CreationTime";

		// Token: 0x040001F9 RID: 505
		public const string LastModificationTimeName = "LastModificationTime";

		// Token: 0x040001FA RID: 506
		public const string ChangeNumberName = "ChangeNumber";

		// Token: 0x040001FB RID: 507
		public const string LastInteractiveLogonTimeName = "LastInteractiveLogonTime";

		// Token: 0x040001FC RID: 508
		public const string PropertyBlobName = "PropertyBlob";

		// Token: 0x040001FD RID: 509
		public const string ExtensionBlobName = "ExtensionBlob";

		// Token: 0x040001FE RID: 510
		public const string PhysicalTableName = "UserInfo";

		// Token: 0x040001FF RID: 511
		private PhysicalColumn userGuid;

		// Token: 0x04000200 RID: 512
		private PhysicalColumn status;

		// Token: 0x04000201 RID: 513
		private PhysicalColumn deletedOn;

		// Token: 0x04000202 RID: 514
		private PhysicalColumn creationTime;

		// Token: 0x04000203 RID: 515
		private PhysicalColumn lastModificationTime;

		// Token: 0x04000204 RID: 516
		private PhysicalColumn changeNumber;

		// Token: 0x04000205 RID: 517
		private PhysicalColumn lastInteractiveLogonTime;

		// Token: 0x04000206 RID: 518
		private PhysicalColumn propertyBlob;

		// Token: 0x04000207 RID: 519
		private PhysicalColumn extensionBlob;

		// Token: 0x04000208 RID: 520
		private Index userInfoPK;

		// Token: 0x04000209 RID: 521
		private Table table;
	}
}
