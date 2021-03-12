using System;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.SchemaConverter.XSO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV141
{
	// Token: 0x020001DF RID: 479
	internal class SmsPrototypeSchemaState : AirSyncXsoSchemaState
	{
		// Token: 0x0600133A RID: 4922 RVA: 0x0006DBA3 File Offset: 0x0006BDA3
		public SmsPrototypeSchemaState() : base(SmsPrototypeSchemaState.supportedClassFilter)
		{
			base.InitConversionTable(2);
			this.CreatePropertyConversionTable();
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x0600133B RID: 4923 RVA: 0x0006DBBD File Offset: 0x0006BDBD
		internal static QueryFilter SupportedClassQueryFilter
		{
			get
			{
				return SmsPrototypeSchemaState.supportedClassFilter;
			}
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0006DBC4 File Offset: 0x0006BDC4
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

		// Token: 0x04000BC8 RID: 3016
		private static readonly string[] supportedClassTypes = new string[]
		{
			"IPM.NOTE.MOBILE.SMS"
		};

		// Token: 0x04000BC9 RID: 3017
		private static readonly QueryFilter supportedClassFilter = AirSyncXsoSchemaState.BuildMessageClassFilter(SmsPrototypeSchemaState.supportedClassTypes);
	}
}
