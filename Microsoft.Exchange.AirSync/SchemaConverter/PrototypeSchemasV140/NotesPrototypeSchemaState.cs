using System;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.SchemaConverter.XSO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV140
{
	// Token: 0x020001D5 RID: 469
	internal class NotesPrototypeSchemaState : AirSyncXsoSchemaState
	{
		// Token: 0x06001318 RID: 4888 RVA: 0x0006B791 File Offset: 0x00069991
		public NotesPrototypeSchemaState() : base(NotesPrototypeSchemaState.supportedClassFilter)
		{
			base.InitConversionTable(2);
			this.CreatePropertyConversionTable();
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x0006B7AB File Offset: 0x000699AB
		internal static QueryFilter SupportedClassQueryFilter
		{
			get
			{
				return NotesPrototypeSchemaState.supportedClassFilter;
			}
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0006B7B4 File Offset: 0x000699B4
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

		// Token: 0x04000BB0 RID: 2992
		private static readonly string[] supportedClassTypes = new string[]
		{
			"IPM.STICKYNOTE"
		};

		// Token: 0x04000BB1 RID: 2993
		private static readonly QueryFilter supportedClassFilter = AirSyncXsoSchemaState.BuildMessageClassFilter(NotesPrototypeSchemaState.supportedClassTypes);
	}
}
