using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport.StoreDriver;
using Microsoft.Exchange.Data.Transport.StoreDriverDelivery;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery;
using Microsoft.Exchange.MessageSecurity.MessageClassifications;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x02000082 RID: 130
	internal class ClassificationApplicationAgent : StoreDriverDeliveryAgent
	{
		// Token: 0x06000489 RID: 1161 RVA: 0x000178CB File Offset: 0x00015ACB
		public ClassificationApplicationAgent()
		{
			base.OnPromotedMessage += this.OnPromotedMessageHandler;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x000178E8 File Offset: 0x00015AE8
		public void OnPromotedMessageHandler(StoreDriverEventSource source, StoreDriverDeliveryEventArgs args)
		{
			StoreDriverDeliveryEventArgsImpl storeDriverDeliveryEventArgsImpl = (StoreDriverDeliveryEventArgsImpl)args;
			if (this.msgClassifications == null)
			{
				this.msgClassifications = ClassificationUtils.ExtractClassifications(storeDriverDeliveryEventArgsImpl.MailItemDeliver.MbxTransportMailItem.RootPart.Headers);
			}
			if (this.IsJournalReport(storeDriverDeliveryEventArgsImpl.MailItemDeliver.MbxTransportMailItem))
			{
				this.ProcessClassificationsForJournalReport(storeDriverDeliveryEventArgsImpl);
				return;
			}
			this.ProcessClassifications(storeDriverDeliveryEventArgsImpl);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00017948 File Offset: 0x00015B48
		private bool IsJournalReport(MbxTransportMailItem mailItem)
		{
			if (mailItem != null && mailItem.RootPart != null)
			{
				Header header = mailItem.RootPart.Headers.FindFirst("X-MS-Exchange-Organization-Journal-Report");
				return header != null;
			}
			return false;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00017980 File Offset: 0x00015B80
		private void ProcessClassifications(StoreDriverDeliveryEventArgsImpl argsImpl)
		{
			ClassificationSummary classificationSummary = this.GetClassificationSummary(argsImpl);
			if (classificationSummary != null)
			{
				this.SetBanner(classificationSummary, argsImpl);
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x000179A0 File Offset: 0x00015BA0
		private void ProcessClassificationsForJournalReport(StoreDriverDeliveryEventArgsImpl argsImpl)
		{
			ClassificationSummary classificationSummary = this.GetClassificationSummary(argsImpl);
			if (classificationSummary != null && classificationSummary.IsValid && classificationSummary.IsClassified)
			{
				using (ItemAttachment itemAttachment = this.TryOpenFirstAttachment(argsImpl.ReplayItem) as ItemAttachment)
				{
					if (itemAttachment != null)
					{
						using (MessageItem itemAsMessage = itemAttachment.GetItemAsMessage(StoreObjectSchema.ContentConversionProperties))
						{
							if (itemAsMessage != null)
							{
								ClassificationApplicationAgent.diag.TraceDebug<string>(0L, "Promote banner for recipient {0} on embedded message of journal report", argsImpl.MailRecipient.Email.ToString());
								itemAsMessage[ItemSchema.IsClassified] = classificationSummary.IsClassified;
								itemAsMessage[ItemSchema.Classification] = classificationSummary.DisplayName;
								itemAsMessage[ItemSchema.ClassificationDescription] = classificationSummary.RecipientDescription;
								itemAsMessage[ItemSchema.ClassificationGuid] = classificationSummary.ClassificationID.ToString();
								itemAsMessage[ItemSchema.ClassificationKeep] = classificationSummary.RetainClassificationEnabled;
								itemAsMessage.Save(SaveMode.NoConflictResolution);
								itemAttachment.Save();
							}
						}
					}
				}
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00017AD8 File Offset: 0x00015CD8
		private void SetBanner(ClassificationSummary summary, StoreDriverDeliveryEventArgsImpl args)
		{
			if (summary.IsValid)
			{
				if (summary.IsClassified)
				{
					ClassificationApplicationAgent.diag.TraceDebug<string>(0L, "Promote banner for recipient {0}", args.MailRecipient.Email.ToString());
					args.ReplayItem[ItemSchema.IsClassified] = summary.IsClassified;
					args.ReplayItem[ItemSchema.Classification] = summary.DisplayName;
					args.ReplayItem[ItemSchema.ClassificationDescription] = summary.RecipientDescription;
					args.ReplayItem[ItemSchema.ClassificationGuid] = summary.ClassificationID.ToString();
					args.ReplayItem[ItemSchema.ClassificationKeep] = summary.RetainClassificationEnabled;
					return;
				}
				ClassificationApplicationAgent.diag.TraceDebug<string>(0L, "Clear banner for recipient {0}", args.MailRecipient.Email.ToString());
				args.ReplayItem.DeleteProperties(new PropertyDefinition[]
				{
					ItemSchema.IsClassified,
					ItemSchema.Classification,
					ItemSchema.ClassificationDescription,
					ItemSchema.ClassificationGuid,
					ItemSchema.ClassificationKeep
				});
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00017C14 File Offset: 0x00015E14
		private ClassificationSummary GetClassificationSummary(StoreDriverDeliveryEventArgsImpl argsImpl)
		{
			if (this.msgClassifications == null || 0 >= this.msgClassifications.Count)
			{
				return null;
			}
			CultureInfo cultureInfo = argsImpl.IsPublicFolderRecipient ? CultureInfo.InvariantCulture : argsImpl.MailboxSession.PreferedCulture;
			if (cultureInfo.Equals(this.previousLocale))
			{
				return null;
			}
			this.previousLocale = cultureInfo;
			return Components.ClassificationConfig.Summarize(argsImpl.MailItemDeliver.MbxTransportMailItem.OrganizationId, this.msgClassifications, cultureInfo);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00017C8C File Offset: 0x00015E8C
		private Attachment TryOpenFirstAttachment(MessageItem messageItem)
		{
			using (IEnumerator<AttachmentHandle> enumerator = messageItem.AttachmentCollection.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					AttachmentHandle handle = enumerator.Current;
					return messageItem.AttachmentCollection.Open(handle, AttachmentType.EmbeddedMessage);
				}
			}
			return null;
		}

		// Token: 0x04000293 RID: 659
		private static readonly Trace diag = ExTraceGlobals.MapiDeliverTracer;

		// Token: 0x04000294 RID: 660
		private List<string> msgClassifications;

		// Token: 0x04000295 RID: 661
		private CultureInfo previousLocale;
	}
}
