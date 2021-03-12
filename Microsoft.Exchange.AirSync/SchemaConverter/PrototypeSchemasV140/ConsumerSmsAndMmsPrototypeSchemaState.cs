using System;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.SchemaConverter.XSO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV140
{
	// Token: 0x020001D1 RID: 465
	internal class ConsumerSmsAndMmsPrototypeSchemaState : AirSyncXsoSchemaState
	{
		// Token: 0x0600130A RID: 4874 RVA: 0x00069EB5 File Offset: 0x000680B5
		public ConsumerSmsAndMmsPrototypeSchemaState() : base(ConsumerSmsAndMmsPrototypeSchemaState.supportedClassFilter)
		{
			base.InitConversionTable(2);
			this.CreatePropertyConversionTable();
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x00069ECF File Offset: 0x000680CF
		internal static QueryFilter SupportedClassQueryFilter
		{
			get
			{
				return ConsumerSmsAndMmsPrototypeSchemaState.supportedClassFilter;
			}
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x00069ED8 File Offset: 0x000680D8
		private void CreatePropertyConversionTable()
		{
			string xmlNodeNamespace = "Email:";
			string xmlNodeNamespace2 = "AirSyncBase:";
			string xmlNodeNamespace3 = "Email2:";
			string xmlNodeNamespace4 = "WindowsLive:";
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "To", false),
				new XsoToProperty()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "From", false),
				new XsoFromProperty()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncUtcDateTimeProperty(xmlNodeNamespace, "DateReceived", AirSyncDateFormat.Punctuate, false),
				new XsoSmsDateReceivedProperty()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncContent14Property(xmlNodeNamespace2, "Body", false),
				new XsoConsumerSmsMmsContentProperty()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "MessageClass", true),
				new XsoStringProperty(StoreObjectSchema.ItemClass)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncIntegerProperty(xmlNodeNamespace, "Importance", false),
				new XsoIntegerProperty(ItemSchema.Importance)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncBooleanProperty(xmlNodeNamespace, "Read", false),
				new XsoReadFlagProperty(MessageItemSchema.IsRead)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncIntegerProperty(xmlNodeNamespace, "InternetCPID", false),
				new XsoIntegerProperty(BodySchema.InternetCpid)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncFlagProperty(xmlNodeNamespace, "Flag", false),
				new XsoFlagProperty()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncByteArrayProperty(xmlNodeNamespace3, "ConversationId", false),
				new XsoConversationIdProperty(PropertyType.ReadOnly)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncByteArrayProperty(xmlNodeNamespace3, "ConversationIndex", false),
				new XsoConversationIndexProperty(PropertyType.ReadOnly)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "Subject", false),
				new XsoSubjectProperty()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace4, "SimSlotNumber", false),
				new XsoStringProperty(ItemSchema.XSimSlotNumber)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace4, "SentItem", false),
				new XsoStringProperty(ItemSchema.XSentItem)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace4, "SentTime", false),
				new XsoStringProperty(ItemSchema.XSentTime)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace4, "MmsMessageId", false),
				new XsoStringProperty(ItemSchema.XMmsMessageId, PropertyType.ReadOnly)
			});
		}

		// Token: 0x04000BA0 RID: 2976
		private static readonly string[] supportedClassTypes = new string[]
		{
			"IPM.NOTE.MOBILE.SMS",
			"IPM.NOTE.MOBILE.MMS"
		};

		// Token: 0x04000BA1 RID: 2977
		private static readonly QueryFilter supportedClassFilter = AirSyncXsoSchemaState.BuildMessageClassFilter(ConsumerSmsAndMmsPrototypeSchemaState.supportedClassTypes);
	}
}
