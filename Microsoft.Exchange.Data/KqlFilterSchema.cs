using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200004E RID: 78
	internal class KqlFilterSchema : FilterSchema
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000870C File Offset: 0x0000690C
		public override string And
		{
			get
			{
				return "AND";
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00008713 File Offset: 0x00006913
		public override string Or
		{
			get
			{
				return "OR";
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000871A File Offset: 0x0000691A
		public override string Not
		{
			get
			{
				return "NOT";
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00008721 File Offset: 0x00006921
		public override string Like
		{
			get
			{
				return ":";
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00008728 File Offset: 0x00006928
		public override string QuotationMark
		{
			get
			{
				return "\"";
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000872F File Offset: 0x0000692F
		public override bool SupportQuotedPrefix
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00008734 File Offset: 0x00006934
		public override string GetRelationalOperator(ComparisonOperator op)
		{
			switch (op)
			{
			case ComparisonOperator.Equal:
				return "=";
			case ComparisonOperator.NotEqual:
				return "<>";
			case ComparisonOperator.LessThan:
				return "<";
			case ComparisonOperator.LessThanOrEqual:
				return "<=";
			case ComparisonOperator.GreaterThan:
				return ">";
			case ComparisonOperator.GreaterThanOrEqual:
				return ">=";
			case ComparisonOperator.Like:
				return ":";
			default:
				return null;
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00008794 File Offset: 0x00006994
		public override string EscapeStringValue(object o)
		{
			if (o == null)
			{
				return null;
			}
			string text;
			if (o is ExDateTime)
			{
				text = ((ExDateTime)o).ToString();
			}
			else if (o is string)
			{
				text = (string)o;
				if (text.IndexOf('"') >= 0)
				{
					throw new ArgumentOutOfRangeException(DataStrings.ErrorQuotionMarkNotSupportedInKql);
				}
			}
			else
			{
				text = o.ToString();
			}
			return text.Replace("\"", "\"\"");
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00008806 File Offset: 0x00006A06
		public override string GetExistsFilter(ExistsFilter filter)
		{
			throw new NotSupportedException("KQL doesn't support exists operator.");
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00008812 File Offset: 0x00006A12
		public override string GetFalseFilter()
		{
			return "false";
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000881C File Offset: 0x00006A1C
		public override string GetPropertyName(string propertyName)
		{
			switch (propertyName)
			{
			case "SearchSender":
				return "from";
			case "SearchRecipientsTo":
				return "to";
			case "SearchRecipientsCc":
				return "cc";
			case "SearchRecipientsBcc":
				return "bcc";
			case "SearchRecipients":
				return "participants";
			case "TextBody":
				return "body";
			case "SubjectProperty":
				return "subject";
			case "AttachmentContent":
				return "attachment";
			case "SentTime":
				return "sent";
			case "ReceivedTime":
				return "received";
			case "ItemClass":
				return "kind";
			case "Categories":
				return "category";
			case "Importance":
				return "importance";
			case "Size":
				return "size";
			case "SearchAllIndexedProps":
				return null;
			}
			return propertyName;
		}

		// Token: 0x040000C7 RID: 199
		public const string QueryFilterPropertySearchSender = "SearchSender";

		// Token: 0x040000C8 RID: 200
		public const string QueryFilterPropertySearchRecipientsTo = "SearchRecipientsTo";

		// Token: 0x040000C9 RID: 201
		public const string QueryFilterPropertySearchRecipientsCc = "SearchRecipientsCc";

		// Token: 0x040000CA RID: 202
		public const string QueryFilterPropertySearchRecipientsBcc = "SearchRecipientsBcc";

		// Token: 0x040000CB RID: 203
		public const string QueryFilterPropertySearchRecipients = "SearchRecipients";

		// Token: 0x040000CC RID: 204
		public const string QueryFilterPropertyTextBody = "TextBody";

		// Token: 0x040000CD RID: 205
		public const string QueryFilterPropertySubjectProperty = "SubjectProperty";

		// Token: 0x040000CE RID: 206
		public const string QueryFilterPropertyAttachmentContent = "AttachmentContent";

		// Token: 0x040000CF RID: 207
		public const string QueryFilterPropertySentTime = "SentTime";

		// Token: 0x040000D0 RID: 208
		public const string QueryFilterPropertyReceivedTime = "ReceivedTime";

		// Token: 0x040000D1 RID: 209
		public const string QueryFilterPropertyItemClass = "ItemClass";

		// Token: 0x040000D2 RID: 210
		public const string QueryFilterPropertyCategories = "Categories";

		// Token: 0x040000D3 RID: 211
		public const string QueryFilterPropertyImportance = "Importance";

		// Token: 0x040000D4 RID: 212
		public const string QueryFilterPropertySize = "Size";

		// Token: 0x040000D5 RID: 213
		public const string QueryFilterPropertySearchAllIndexedProps = "SearchAllIndexedProps";

		// Token: 0x040000D6 RID: 214
		public const string KqlPropertyFrom = "from";

		// Token: 0x040000D7 RID: 215
		public const string KqlPropertyTo = "to";

		// Token: 0x040000D8 RID: 216
		public const string KqlPropertyCc = "cc";

		// Token: 0x040000D9 RID: 217
		public const string KqlPropertyBcc = "bcc";

		// Token: 0x040000DA RID: 218
		public const string KqlPropertyParticipants = "participants";

		// Token: 0x040000DB RID: 219
		public const string KqlPropertyBody = "body";

		// Token: 0x040000DC RID: 220
		public const string KqlPropertySubject = "subject";

		// Token: 0x040000DD RID: 221
		public const string KqlPropertyAttachment = "attachment";

		// Token: 0x040000DE RID: 222
		public const string KqlPropertySent = "sent";

		// Token: 0x040000DF RID: 223
		public const string KqlPropertyReceived = "received";

		// Token: 0x040000E0 RID: 224
		public const string KqlPropertyKind = "kind";

		// Token: 0x040000E1 RID: 225
		public const string KqlPropertyCategory = "category";

		// Token: 0x040000E2 RID: 226
		public const string KqlPropertyImportance = "importance";

		// Token: 0x040000E3 RID: 227
		public const string KqlPropertySize = "size";
	}
}
