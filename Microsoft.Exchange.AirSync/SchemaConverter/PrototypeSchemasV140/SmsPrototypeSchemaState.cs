using System;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.SchemaConverter.XSO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV140
{
	// Token: 0x020001D7 RID: 471
	internal class SmsPrototypeSchemaState : AirSyncXsoSchemaState
	{
		// Token: 0x0600131E RID: 4894 RVA: 0x0006B99F File Offset: 0x00069B9F
		public SmsPrototypeSchemaState() : base(SmsPrototypeSchemaState.supportedClassFilter)
		{
			base.InitConversionTable(2);
			this.CreatePropertyConversionTable();
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x0006B9B9 File Offset: 0x00069BB9
		internal static QueryFilter SupportedClassQueryFilter
		{
			get
			{
				return SmsPrototypeSchemaState.supportedClassFilter;
			}
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0006B9C0 File Offset: 0x00069BC0
		private void CreatePropertyConversionTable()
		{
			string xmlNodeNamespace = "Email:";
			string xmlNodeNamespace2 = "AirSyncBase:";
			string xmlNodeNamespace3 = "Email2:";
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
				new AirSyncContent14Property(xmlNodeNamespace2, "Body", false),
				new XsoSMSContentProperty()
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
		}

		// Token: 0x04000BB2 RID: 2994
		private static readonly string[] supportedClassTypes = new string[]
		{
			"IPM.NOTE.MOBILE.SMS"
		};

		// Token: 0x04000BB3 RID: 2995
		private static readonly QueryFilter supportedClassFilter = AirSyncXsoSchemaState.BuildMessageClassFilter(SmsPrototypeSchemaState.supportedClassTypes);
	}
}
