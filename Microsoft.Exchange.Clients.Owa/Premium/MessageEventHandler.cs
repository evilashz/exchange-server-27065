using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.MessageSecurity.MessageClassifications;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004A7 RID: 1191
	internal abstract class MessageEventHandler : ItemEventHandler
	{
		// Token: 0x06002DC3 RID: 11715 RVA: 0x001035C0 File Offset: 0x001017C0
		protected static void ClearRecipients(MessageItem message, params RecipientItemType[] recipientTypes)
		{
			int num = message.Recipients.Count;
			message.Load(new PropertyDefinition[]
			{
				MessageItemSchema.IsResend
			});
			object obj = message.TryGetProperty(MessageItemSchema.IsResend);
			int i = 0;
			while (i < num)
			{
				if (obj is bool && (bool)obj && message.Recipients[i].Submitted)
				{
					i++;
				}
				else
				{
					bool flag = true;
					if (recipientTypes != null && recipientTypes.Length > 0)
					{
						flag = false;
						foreach (RecipientItemType recipientItemType in recipientTypes)
						{
							if (message.Recipients[i].RecipientItemType == recipientItemType)
							{
								flag = true;
								break;
							}
						}
					}
					if (flag)
					{
						num--;
						message.Recipients.RemoveAt(i);
					}
					else
					{
						i++;
					}
				}
			}
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x00103694 File Offset: 0x00101894
		protected bool AddMessageRecipients(RecipientCollection recipients, RecipientItemType recipientItemType, string wellName)
		{
			bool flag = false;
			this.Writer.Write("<div id=\"");
			this.Writer.Write(wellName);
			this.Writer.Write("\">");
			RecipientInfo[] array = (RecipientInfo[])base.GetParameter(wellName);
			if (array == null)
			{
				this.Writer.Write("</div>");
				return false;
			}
			List<Participant> list = new List<Participant>();
			foreach (RecipientInfo recipientInfo in array)
			{
				flag |= base.GetExchangeParticipantsFromRecipientInfo(recipientInfo, list);
			}
			for (int j = 0; j < list.Count; j++)
			{
				recipients.Add(list[j], recipientItemType);
			}
			this.Writer.Write("</div>");
			return flag;
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x06002DC5 RID: 11717 RVA: 0x0010374C File Offset: 0x0010194C
		// (set) Token: 0x06002DC6 RID: 11718 RVA: 0x00103754 File Offset: 0x00101954
		protected Participant FromParticipant
		{
			get
			{
				return this.fromParticipant;
			}
			set
			{
				this.fromParticipant = value;
			}
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x0010376C File Offset: 0x0010196C
		protected bool AddFromRecipient(MessageItem message)
		{
			bool flag = false;
			bool flag2 = false;
			List<Participant> list = new List<Participant>();
			RecipientInfo recipientInfo = (RecipientInfo)base.GetParameter("From");
			if (recipientInfo != null)
			{
				flag |= base.GetExchangeParticipantsFromRecipientInfo(recipientInfo, list);
				if (list.Count == 1)
				{
					message.From = list[0];
					SubscriptionCacheEntry subscriptionCacheEntry;
					if (RecipientCache.RunGetCacheOperationUnderDefaultExceptionHandler(delegate
					{
						SubscriptionCache.GetCache(base.UserContext);
					}, this.GetHashCode()) && base.UserContext.SubscriptionCache.TryGetEntry(message.From, out subscriptionCacheEntry))
					{
						flag2 = true;
						message[MessageItemSchema.SharingInstanceGuid] = subscriptionCacheEntry.Id;
						message[ItemSchema.SentRepresentingEmailAddress] = subscriptionCacheEntry.Address;
						message[ItemSchema.SentRepresentingDisplayName] = subscriptionCacheEntry.DisplayName;
					}
					if (!flag2)
					{
						this.fromParticipant = message.From;
					}
				}
			}
			else
			{
				message.From = null;
			}
			return flag;
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x00103854 File Offset: 0x00101A54
		protected RecipientInfoCacheEntry GetFromRecipientEntry(string routingAddress)
		{
			RecipientInfoAC[] array = (RecipientInfoAC[])base.GetParameter("Recips");
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].RoutingAddress == routingAddress)
					{
						return AutoCompleteCacheEntry.ParseClientEntry(array[i]);
					}
				}
			}
			return null;
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x001038A0 File Offset: 0x00101AA0
		protected bool UpdateRecipientsOnAutosave()
		{
			bool result = true;
			object parameter = base.GetParameter("UpdRcpAs");
			if (parameter != null)
			{
				result = (bool)parameter;
			}
			return result;
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x001038C8 File Offset: 0x00101AC8
		protected void UpdateReadMessage(MessageItem message)
		{
			this.UpdateSubject(message);
			object parameter = base.GetParameter("AudioNotes");
			if (parameter != null)
			{
				message[MessageItemSchema.MessageAudioNotes] = (string)parameter;
			}
			parameter = base.GetParameter("AlWbBcn");
			if (parameter != null)
			{
				if (Utilities.IsPublic(message))
				{
					throw new OwaEventHandlerException("Allow web beacon parameter not valid in public folder");
				}
				message[ItemSchema.BlockStatus] = BlockStatus.NoNeverAgain;
			}
			parameter = base.GetParameter("StLnkEnbl");
			if (parameter != null && parameter is bool && (bool)parameter)
			{
				message[ItemSchema.LinkEnabled] = true;
			}
		}

		// Token: 0x06002DCB RID: 11723 RVA: 0x00103961 File Offset: 0x00101B61
		protected bool UpdateMessage(MessageItem message, StoreObjectType storeObjectType)
		{
			this.UpdateSubject(message);
			this.UpdateCommonMessageProperties(message);
			this.UpdateComplianceAction(message);
			this.UpdateBody(message, storeObjectType);
			return this.UpdateRecipients(message, storeObjectType);
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x00103988 File Offset: 0x00101B88
		protected bool UpdateMessageForAutoSave(MessageItem message, StoreObjectType storeObjectType)
		{
			bool result = false;
			this.TryUpdateSubject(message);
			this.UpdateCommonMessageProperties(message);
			this.UpdateComplianceAction(message);
			this.UpdateBody(message, storeObjectType);
			if (this.UpdateRecipientsOnAutosave())
			{
				result = this.UpdateRecipients(message, storeObjectType);
			}
			return result;
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x001039C8 File Offset: 0x00101BC8
		protected void UpdateComplianceAction(MessageItem message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			object parameter = base.GetParameter("CmpAc");
			if (parameter == null)
			{
				return;
			}
			string text = (string)parameter;
			if (text == "0")
			{
				message[ItemSchema.IsClassified] = false;
				message[ItemSchema.ClassificationGuid] = string.Empty;
				message[ItemSchema.ClassificationDescription] = string.Empty;
				message[ItemSchema.Classification] = string.Empty;
				if (Utilities.IsIrmDecrypted(message))
				{
					((RightsManagedMessageItem)message).SetRestriction(null);
				}
				return;
			}
			Guid empty = Guid.Empty;
			ComplianceType complianceType = ComplianceType.Unknown;
			if (GuidHelper.TryParseGuid(text, out empty))
			{
				complianceType = OwaContext.Current.UserContext.ComplianceReader.GetComplianceType(empty, base.UserContext.UserCulture);
			}
			switch (complianceType)
			{
			case ComplianceType.MessageClassification:
			{
				ClassificationSummary classificationSummary = base.UserContext.ComplianceReader.MessageClassificationReader.LookupMessageClassification(empty, base.UserContext.UserCulture);
				if (classificationSummary == null)
				{
					throw new OwaEventHandlerException("Invalid classification being set from client", LocalizedStrings.GetNonEncoded(-1799006479), OwaEventHandlerErrorCode.ComplianceLabelNotFoundError);
				}
				message[ItemSchema.IsClassified] = true;
				message[ItemSchema.ClassificationGuid] = classificationSummary.ClassificationID.ToString();
				message[ItemSchema.ClassificationDescription] = classificationSummary.SenderDescription;
				message[ItemSchema.Classification] = classificationSummary.DisplayName;
				message[ItemSchema.ClassificationKeep] = classificationSummary.RetainClassificationEnabled;
				if (Utilities.IsIrmDecrypted(message))
				{
					((RightsManagedMessageItem)message).SetRestriction(null);
					return;
				}
				return;
			}
			case ComplianceType.RmsTemplate:
				if (Utilities.IsIrmDecrypted(message))
				{
					RmsTemplate rmsTemplate = base.UserContext.ComplianceReader.RmsTemplateReader.LookupRmsTemplate(empty);
					if (rmsTemplate == null)
					{
						throw new OwaEventHandlerException("Invalid RMS template was sent from client.", LocalizedStrings.GetNonEncoded(-1799006479), OwaEventHandlerErrorCode.ComplianceLabelNotFoundError);
					}
					((RightsManagedMessageItem)message).SetRestriction(rmsTemplate);
					if (message.Sender == null)
					{
						message.Sender = new Participant(base.UserContext.MailboxSession.MailboxOwner);
					}
				}
				message[ItemSchema.IsClassified] = false;
				message[ItemSchema.ClassificationGuid] = string.Empty;
				message[ItemSchema.ClassificationDescription] = string.Empty;
				message[ItemSchema.Classification] = string.Empty;
				return;
			}
			if (!OwaContext.Current.UserContext.ComplianceReader.RmsTemplateReader.IsInternalLicensingEnabled)
			{
				throw new OwaEventHandlerException("Unable to determine compliance type because licensing against internal RMS server has been disabled.", LocalizedStrings.GetNonEncoded(-27910813), OwaEventHandlerErrorCode.ComplianceLabelNotFoundError, true);
			}
			if (OwaContext.Current.UserContext.ComplianceReader.RmsTemplateReader.TemplateAcquisitionFailed)
			{
				throw new OwaEventHandlerException("Unable to determine compliance type because there was an error loading templates from the RMS server.", LocalizedStrings.GetNonEncoded(1084956906), OwaEventHandlerErrorCode.ComplianceLabelNotFoundError, true);
			}
			throw new OwaEventHandlerException("Invalid compliance label was sent from client.", LocalizedStrings.GetNonEncoded(-1799006479), OwaEventHandlerErrorCode.ComplianceLabelNotFoundError);
		}

		// Token: 0x06002DCE RID: 11726 RVA: 0x00103C9E File Offset: 0x00101E9E
		private string GetSubject(MessageItem message)
		{
			return (string)base.GetParameter("Subj");
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x00103CB0 File Offset: 0x00101EB0
		private void UpdateSubject(MessageItem message)
		{
			string subject = this.GetSubject(message);
			if (subject == null)
			{
				return;
			}
			if (subject.Length <= 255)
			{
				message.Subject = subject;
				return;
			}
			throw new OwaEventHandlerException("The subject exceeds the max length " + 255);
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x00103CF8 File Offset: 0x00101EF8
		private void TryUpdateSubject(MessageItem message)
		{
			string subject = this.GetSubject(message);
			if (subject != null && subject.Length <= 255)
			{
				message.Subject = subject;
			}
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x00103D24 File Offset: 0x00101F24
		private void UpdateBody(MessageItem message, StoreObjectType storeObjectType)
		{
			string text = (string)base.GetParameter("Body");
			object parameter = base.GetParameter("Text");
			if (text != null && parameter != null)
			{
				Markup markup = ((bool)parameter) ? Markup.PlainText : Markup.Html;
				BodyConversionUtilities.SetBody(message, text, markup, storeObjectType, base.UserContext);
			}
		}

		// Token: 0x06002DD2 RID: 11730 RVA: 0x00103D74 File Offset: 0x00101F74
		protected bool UpdateRecipients(MessageItem message, StoreObjectType storeObjectType)
		{
			bool flag = this.UpdateRecipients(message);
			if (storeObjectType == StoreObjectType.Message)
			{
				flag |= this.AddFromRecipient(message);
			}
			return flag;
		}

		// Token: 0x06002DD3 RID: 11731 RVA: 0x00103D9C File Offset: 0x00101F9C
		protected bool UpdateRecipients(MessageItem message)
		{
			bool flag = false;
			MessageEventHandler.ClearRecipients(message, new RecipientItemType[0]);
			flag |= this.AddMessageRecipients(message.Recipients, RecipientItemType.To, "To");
			flag |= this.AddMessageRecipients(message.Recipients, RecipientItemType.Cc, "Cc");
			return flag | this.AddMessageRecipients(message.Recipients, RecipientItemType.Bcc, "Bcc");
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x00103DF8 File Offset: 0x00101FF8
		private void UpdateCommonMessageProperties(MessageItem message)
		{
			object parameter = base.GetParameter("Imp");
			if (parameter != null)
			{
				message.Importance = (Importance)parameter;
			}
			parameter = base.GetParameter("Sensitivity");
			if (parameter != null)
			{
				message.Sensitivity = (Sensitivity)parameter;
			}
			parameter = base.GetParameter("AudioNotes");
			if (parameter != null)
			{
				message[MessageItemSchema.MessageAudioNotes] = (string)parameter;
			}
			parameter = base.GetParameter("DeliveryRcpt");
			if (parameter != null)
			{
				message.IsDeliveryReceiptRequested = (bool)parameter;
			}
			parameter = base.GetParameter("ReadRcpt");
			if (parameter != null)
			{
				message.IsReadReceiptRequested = (bool)parameter;
			}
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x00103E94 File Offset: 0x00102094
		protected void RenderErrorForAutoSave(Exception exception)
		{
			ExTraceGlobals.CoreTracer.TraceDebug<string>((long)this.GetHashCode(), "MessageEventHandler.RenderErrorForAutoSave. Exception {0} thrown", exception.Message);
			try
			{
				ErrorInformation exceptionHandlingInformation = Utilities.GetExceptionHandlingInformation(exception, base.OwaContext.MailboxIdentity);
				Exception ex = (exception.InnerException == null) ? exception : exception.InnerException;
				StringBuilder stringBuilder = new StringBuilder();
				using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
				{
					OwaEventHttpHandler.RenderError(base.OwaContext, stringWriter, exceptionHandlingInformation.Message, exceptionHandlingInformation.MessageDetails, exceptionHandlingInformation.OwaEventHandlerErrorCode, exceptionHandlingInformation.HideDebugInformation ? null : ex);
				}
				this.Writer.Write(stringBuilder.ToString());
				base.OwaContext.ErrorSent = true;
			}
			finally
			{
				Utilities.HandleException(base.OwaContext, exception);
			}
		}

		// Token: 0x04001ED6 RID: 7894
		public const string Subject = "Subj";

		// Token: 0x04001ED7 RID: 7895
		public const string Importance = "Imp";

		// Token: 0x04001ED8 RID: 7896
		public const string Private = "Sensitivity";

		// Token: 0x04001ED9 RID: 7897
		public const string To = "To";

		// Token: 0x04001EDA RID: 7898
		public const string Cc = "Cc";

		// Token: 0x04001EDB RID: 7899
		public const string Bcc = "Bcc";

		// Token: 0x04001EDC RID: 7900
		public const string From = "From";

		// Token: 0x04001EDD RID: 7901
		public const string Body = "Body";

		// Token: 0x04001EDE RID: 7902
		public const string Text = "Text";

		// Token: 0x04001EDF RID: 7903
		public const string Recipients = "Recips";

		// Token: 0x04001EE0 RID: 7904
		public const string AudioNotes = "AudioNotes";

		// Token: 0x04001EE1 RID: 7905
		public const string IsDeliveryReceiptRequested = "DeliveryRcpt";

		// Token: 0x04001EE2 RID: 7906
		public const string IsReadReceiptRequested = "ReadRcpt";

		// Token: 0x04001EE3 RID: 7907
		public const string ComplianceAction = "CmpAc";

		// Token: 0x04001EE4 RID: 7908
		public const string AllowWebBeacon = "AlWbBcn";

		// Token: 0x04001EE5 RID: 7909
		public const string SetLinkEnabled = "StLnkEnbl";

		// Token: 0x04001EE6 RID: 7910
		public const string AutosaveUpdateRecipients = "UpdRcpAs";

		// Token: 0x04001EE7 RID: 7911
		private Participant fromParticipant;
	}
}
