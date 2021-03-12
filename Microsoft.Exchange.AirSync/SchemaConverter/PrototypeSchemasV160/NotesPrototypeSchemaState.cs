using System;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.SchemaConverter.XSO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV160
{
	// Token: 0x020001E4 RID: 484
	internal class NotesPrototypeSchemaState : AirSyncXsoSchemaState
	{
		// Token: 0x0600136E RID: 4974 RVA: 0x00070006 File Offset: 0x0006E206
		public NotesPrototypeSchemaState() : base(NotesPrototypeSchemaState.supportedClassFilter)
		{
			base.InitConversionTable(2);
			this.CreatePropertyConversionTable();
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x0600136F RID: 4975 RVA: 0x00070020 File Offset: 0x0006E220
		internal static QueryFilter SupportedClassQueryFilter
		{
			get
			{
				return NotesPrototypeSchemaState.supportedClassFilter;
			}
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x00070028 File Offset: 0x0006E228
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

		// Token: 0x04000BFC RID: 3068
		private static readonly string[] supportedClassTypes = new string[]
		{
			"IPM.STICKYNOTE"
		};

		// Token: 0x04000BFD RID: 3069
		private static readonly QueryFilter supportedClassFilter = AirSyncXsoSchemaState.BuildMessageClassFilter(NotesPrototypeSchemaState.supportedClassTypes);
	}
}
