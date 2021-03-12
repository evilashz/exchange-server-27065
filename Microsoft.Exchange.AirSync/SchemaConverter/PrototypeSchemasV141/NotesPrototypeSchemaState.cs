using System;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.SchemaConverter.XSO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV141
{
	// Token: 0x020001DD RID: 477
	internal class NotesPrototypeSchemaState : AirSyncXsoSchemaState
	{
		// Token: 0x06001334 RID: 4916 RVA: 0x0006D995 File Offset: 0x0006BB95
		public NotesPrototypeSchemaState() : base(NotesPrototypeSchemaState.supportedClassFilter)
		{
			base.InitConversionTable(2);
			this.CreatePropertyConversionTable();
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001335 RID: 4917 RVA: 0x0006D9AF File Offset: 0x0006BBAF
		internal static QueryFilter SupportedClassQueryFilter
		{
			get
			{
				return NotesPrototypeSchemaState.supportedClassFilter;
			}
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0006D9B8 File Offset: 0x0006BBB8
		private void CreatePropertyConversionTable()
		{
			string xmlNodeNamespace = "Notes:";
			string xmlNodeNamespace2 = "AirSyncBase:";
			base.AddProperty(new IProperty[]
			{
				new AirSyncContent14Property(xmlNodeNamespace2, "Body", true),
				new XsoContent14Property()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "Subject", true),
				new XsoStringProperty(ItemSchema.Subject)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "MessageClass", true),
				new XsoStringProperty(StoreObjectSchema.ItemClass)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncUtcDateTimeProperty(xmlNodeNamespace, "LastModifiedDate", true),
				new XsoUtcDateTimeProperty(StoreObjectSchema.LastModifiedTime)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncMultiValuedStringProperty(xmlNodeNamespace, "Categories", "Category", true),
				new XsoCategoriesProperty()
			});
		}

		// Token: 0x04000BC6 RID: 3014
		private static readonly string[] supportedClassTypes = new string[]
		{
			"IPM.STICKYNOTE"
		};

		// Token: 0x04000BC7 RID: 3015
		private static readonly QueryFilter supportedClassFilter = AirSyncXsoSchemaState.BuildMessageClassFilter(NotesPrototypeSchemaState.supportedClassTypes);
	}
}
