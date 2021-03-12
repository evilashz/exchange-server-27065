using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Inference.Common;
using Microsoft.Exchange.Inference.PeopleRelevance;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Inference.Mdb
{
	// Token: 0x02000019 RID: 25
	internal class PeopleRelevanceDocumentFactory
	{
		// Token: 0x060000BE RID: 190 RVA: 0x000046D9 File Offset: 0x000028D9
		protected PeopleRelevanceDocumentFactory()
		{
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000046E1 File Offset: 0x000028E1
		public static PeopleRelevanceDocumentFactory Current
		{
			get
			{
				return PeopleRelevanceDocumentFactory.instance.Value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000046ED File Offset: 0x000028ED
		internal static Hookable<PeopleRelevanceDocumentFactory> Instance
		{
			get
			{
				return PeopleRelevanceDocumentFactory.instance;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000046F4 File Offset: 0x000028F4
		internal IDocument CreatePeopleRelevanceDocument(PeopleModelItem peopleModelItem, IDocument sentItemsTrainingSubDocument, Guid mailboxGuid, string mailboxAlias)
		{
			Util.ThrowOnNullArgument(peopleModelItem, "modelItem");
			Util.ThrowOnNullArgument(mailboxAlias, "mailboxAlias");
			if (sentItemsTrainingSubDocument == null)
			{
				sentItemsTrainingSubDocument = MdbInferenceFactory.Current.CreateDocument(mailboxGuid, true);
			}
			Document document = MdbInferenceFactory.Current.CreateDocument(mailboxGuid);
			document.SetProperty(PeopleRelevanceSchema.MailboxAlias, mailboxAlias);
			document.SetProperty(PeopleRelevanceSchema.ContactList, peopleModelItem.CreateContactDictionary());
			document.SetProperty(PeopleRelevanceSchema.CurrentTimeWindowNumber, peopleModelItem.CurrentTimeWindowNumber);
			document.SetProperty(PeopleRelevanceSchema.CurrentTimeWindowStartTime, new ExDateTime(ExTimeZone.UtcTimeZone, peopleModelItem.CurrentTimeWindowStartTime.ToUniversalTime()));
			document.SetProperty(PeopleRelevanceSchema.LastRecipientCacheValidationTime, new ExDateTime(ExTimeZone.UtcTimeZone, peopleModelItem.LastRecipientCacheValidationTime.ToUniversalTime()));
			document.SetProperty(PeopleRelevanceSchema.LastProcessedMessageSentTime, new ExDateTime(ExTimeZone.UtcTimeZone, peopleModelItem.LastProcessedMessageSentTime.ToUniversalTime()));
			document.SetProperty(PeopleRelevanceSchema.SentItemsTrainingSubDocument, sentItemsTrainingSubDocument);
			return document;
		}

		// Token: 0x04000051 RID: 81
		internal static readonly PropertyDefinition[] SentItemsFullDocumentProperties = new PropertyDefinition[]
		{
			PeopleRelevanceSchema.RecipientsTo,
			PeopleRelevanceSchema.RecipientsCc,
			PeopleRelevanceSchema.SentTime,
			PeopleRelevanceSchema.IsReply
		};

		// Token: 0x04000052 RID: 82
		private static readonly Hookable<PeopleRelevanceDocumentFactory> instance = Hookable<PeopleRelevanceDocumentFactory>.Create(true, new PeopleRelevanceDocumentFactory());
	}
}
