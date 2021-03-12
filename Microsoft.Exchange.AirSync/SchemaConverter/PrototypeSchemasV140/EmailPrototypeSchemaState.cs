using System;
using System.Collections.Generic;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.SchemaConverter.XSO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV140
{
	// Token: 0x020001D3 RID: 467
	internal class EmailPrototypeSchemaState : AirSyncXsoSchemaState
	{
		// Token: 0x06001312 RID: 4882 RVA: 0x0006AE84 File Offset: 0x00069084
		static EmailPrototypeSchemaState()
		{
			IList<string> supportedIpmTypes;
			if (GlobalSettings.SupportedIPMTypes.Count > 0)
			{
				List<string> list = new List<string>(EmailPrototypeSchemaState.supportedClassTypes.Length + GlobalSettings.SupportedIPMTypes.Count);
				list.AddRange(EmailPrototypeSchemaState.supportedClassTypes);
				list.AddRange(GlobalSettings.SupportedIPMTypes);
				supportedIpmTypes = list;
			}
			else
			{
				supportedIpmTypes = EmailPrototypeSchemaState.supportedClassTypes;
			}
			EmailPrototypeSchemaState.supportedClassFilter = new AndFilter(new QueryFilter[]
			{
				AirSyncXsoSchemaState.BuildMessageClassFilter(supportedIpmTypes),
				new NotFilter(ConsumerSmsAndMmsPrototypeSchemaState.SupportedClassQueryFilter)
			});
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0006AFED File Offset: 0x000691ED
		public EmailPrototypeSchemaState(IdMapping idmapping) : base(EmailPrototypeSchemaState.supportedClassFilter)
		{
			base.InitConversionTable(2);
			this.CreatePropertyConversionTable(idmapping);
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x0006B008 File Offset: 0x00069208
		internal static QueryFilter SupportedClassQueryFilter
		{
			get
			{
				return EmailPrototypeSchemaState.supportedClassFilter;
			}
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0006B010 File Offset: 0x00069210
		private void CreatePropertyConversionTable(IdMapping idmapping)
		{
			string xmlNodeNamespace = "Email:";
			string xmlNodeNamespace2 = "AirSyncBase:";
			string xmlNodeNamespace3 = "Email2:";
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "To", false),
				new XsoToProperty(PropertyType.ReadOnly)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "CC", false),
				new XsoCCProperty(PropertyType.ReadOnly)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "From", false),
				new XsoFromProperty(PropertyType.ReadOnly)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "Subject", false),
				new XsoEmailSubjectProperty(PropertyType.ReadOnly)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "ReplyTo", false),
				new XsoReplyToProperty(PropertyType.ReadOnly)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncUtcDateTimeProperty(xmlNodeNamespace, "DateReceived", AirSyncDateFormat.Punctuate, false),
				new XsoUtcDateTimeProperty(ItemSchema.ReceivedTime, PropertyType.ReadOnly)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "DisplayTo", false),
				new XsoStringProperty(ItemSchema.DisplayTo, PropertyType.ReadOnly)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "ThreadTopic", false),
				new XsoStringProperty(ItemSchema.ConversationTopic, PropertyType.ReadOnly)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncIntegerProperty(xmlNodeNamespace, "Importance", false),
				new XsoIntegerProperty(ItemSchema.Importance, PropertyType.ReadOnly)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncBooleanProperty(xmlNodeNamespace, "Read", false),
				new XsoReadFlagProperty(MessageItemSchema.IsRead)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSync14AttachmentsProperty(xmlNodeNamespace2, "Attachments", false),
				new Xso14AttachmentsProperty(idmapping, PropertyType.ReadOnly)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncContent14Property(xmlNodeNamespace2, "Body", false),
				new XsoEmailBodyContent14Property(PropertyType.ReadOnly)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "MessageClass", false),
				new XsoMessageClassProperty(PropertyType.ReadOnly)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncMeetingRequestProperty(xmlNodeNamespace, "MeetingRequest", false, 140),
				new XsoMeetingRequestProperty(140)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncIntegerProperty(xmlNodeNamespace, "InternetCPID", false),
				new XsoIntegerProperty(BodySchema.InternetCpid, PropertyType.ReadOnly)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncFlagProperty(xmlNodeNamespace, "Flag", false),
				new XsoFlagProperty()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "ContentClass", false),
				new XsoContentClassProperty(PropertyType.ReadOnly)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncIntegerProperty(xmlNodeNamespace2, "NativeBodyType", false),
				new XsoNativeBodyProperty()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace3, "UmCallerID", false),
				new XsoEvmCallerIdProperty(PropertyType.ReadOnly)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncChangeTrackedStringProperty(xmlNodeNamespace3, "UmUserNotes", false),
				new XsoEvmStringProperty(MessageItemSchema.MessageAudioNotes, PropertyType.ReadOnly)
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
				new AirSyncIntegerProperty(xmlNodeNamespace3, "LastVerbExecuted", false),
				new XsoLastVerbExecutedProperty()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncUtcDateTimeProperty(xmlNodeNamespace3, "LastVerbExecutionTime", AirSyncDateFormat.Punctuate, false),
				new XsoUtcDateTimeProperty(MessageItemSchema.LastVerbExecutionTime, PropertyType.ReadOnly)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncBooleanProperty(xmlNodeNamespace3, "ReceivedAsBcc", false),
				new XsoBooleanProperty(MessageItemSchema.MessageBccMe, PropertyType.ReadOnly)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace3, "Sender", false),
				new XsoSenderProperty(PropertyType.ReadOnly)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncChangeTrackedMultiValuedStringProperty(xmlNodeNamespace, "Categories", "Category", false),
				new XsoCategoriesProperty()
			});
		}

		// Token: 0x04000BAE RID: 2990
		private static readonly string[] supportedClassTypes = new string[]
		{
			"IPM.NOTE",
			"IPM.INFOPATHFORM",
			"IPM.SCHEDULE.MEETING",
			"IPM.NOTIFICATION.MEETING",
			"IPM.POST",
			"IPM.OCTEL.VOICE",
			"IPM.VOICENOTES",
			"IPM.SHARING",
			"REPORT.IPM.NOTE.NDR",
			"REPORT.IPM.NOTE.DR",
			"REPORT.IPM.NOTE.DELAYED",
			"REPORT.IPM.NOTE.IPNRN",
			"REPORT.IPM.NOTE.IPNNRN",
			"REPORT.IPM.SCHEDULE.MEETING.REQUEST.NDR",
			"REPORT.IPM.SCHEDULE.MEETING.RESP.NEG.NDR",
			"REPORT.IPM.SCHEDULE.MEETING.RESP.POS.NDR",
			"REPORT.IPM.SCHEDULE.MEETING.RESP.TENT.NDR",
			"REPORT.IPM.SCHEDULE.MEETING.CANCELED.NDR",
			"REPORT.IPM.NOTE.SMIME.NDR",
			"REPORT.IPM.NOTE.SMIME.DR",
			"REPORT.IPM.NOTE.SMIME.IPNRN",
			"REPORT.IPM.NOTE.SMIME.IPNNRN",
			"REPORT.IPM.NOTE.SMIME.MULTIPARTSIGNED.NDR",
			"REPORT.IPM.NOTE.SMIME.MULTIPARTSIGNED.DR",
			"REPORT.IPM.NOTE.SMIME.MULTIPARTSIGNED.IPNRN",
			"REPORT.IPM.NOTE.SMIME.MULTIPARTSIGNED.IPNNRN"
		};

		// Token: 0x04000BAF RID: 2991
		private static readonly QueryFilter supportedClassFilter;
	}
}
