using System;
using System.Collections;
using System.IO;
using System.Text;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004A9 RID: 1193
	[OwaEventNamespace("EditMessage")]
	internal sealed class EditMessageEventHandler : MessageEventHandler
	{
		// Token: 0x06002DDD RID: 11741 RVA: 0x00104256 File Offset: 0x00102456
		[OwaEvent("LOD")]
		[OwaEventVerb(OwaEventVerb.Get)]
		public void LoadOptionsDialog()
		{
			this.HttpContext.Server.Execute("forms/premium/messageoptionsdialog.aspx", this.Writer);
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x00104273 File Offset: 0x00102473
		[OwaEventParameter("Text", typeof(bool), false, true)]
		[OwaEventParameter("ReadRcpt", typeof(bool), false, true)]
		[OwaEventParameter("CmpAc", typeof(string), false, true)]
		[OwaEventParameter("HideMailTipsByDefault", typeof(bool), false, true)]
		[OwaEvent("Save")]
		[OwaEventParameter("CK", typeof(string), false, true)]
		[OwaEventParameter("Subj", typeof(string), false, true)]
		[OwaEventParameter("Imp", typeof(Importance), false, true)]
		[OwaEventParameter("Sensitivity", typeof(Sensitivity), false, true)]
		[OwaEventParameter("Body", typeof(string), false, true)]
		[OwaEventParameter("To", typeof(RecipientInfo), true, true)]
		[OwaEventParameter("Cc", typeof(RecipientInfo), true, true)]
		[OwaEventParameter("Bcc", typeof(RecipientInfo), true, true)]
		[OwaEventParameter("From", typeof(RecipientInfo), false, true)]
		[OwaEventParameter("Recips", typeof(RecipientInfoAC), true, true)]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("DeliveryRcpt", typeof(bool), false, true)]
		public void Save()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "EditMessageEventHandler.Save");
			this.ProcessMessageRequest(MessageAction.Save);
		}

		// Token: 0x06002DDF RID: 11743 RVA: 0x00104292 File Offset: 0x00102492
		[OwaEventParameter("Subj", typeof(string), false, true)]
		[OwaEventParameter("ReadRcpt", typeof(bool), false, true)]
		[OwaEventParameter("CmpAc", typeof(string), false, true)]
		[OwaEventParameter("UpdRcpAs", typeof(bool))]
		[OwaEvent("AutoSave")]
		[OwaEventParameter("CK", typeof(string), false, true)]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("Imp", typeof(Importance), false, true)]
		[OwaEventParameter("Sensitivity", typeof(Sensitivity), false, true)]
		[OwaEventParameter("Body", typeof(string), false, true)]
		[OwaEventParameter("To", typeof(RecipientInfo), true, true)]
		[OwaEventParameter("Cc", typeof(RecipientInfo), true, true)]
		[OwaEventParameter("Bcc", typeof(RecipientInfo), true, true)]
		[OwaEventParameter("From", typeof(RecipientInfo), false, true)]
		[OwaEventParameter("Recips", typeof(RecipientInfoAC), true, true)]
		[OwaEventParameter("Text", typeof(bool), false, true)]
		[OwaEventParameter("DeliveryRcpt", typeof(bool), false, true)]
		public void AutoSave()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "EditMessageEventHandler.AutoSave");
			this.TryProcessMessageRequestForAutoSave(MessageAction.AutoSave);
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x001042B1 File Offset: 0x001024B1
		[OwaEventParameter("To", typeof(RecipientInfo), true, true)]
		[OwaEventParameter("Sensitivity", typeof(Sensitivity), false, true)]
		[OwaEventParameter("Bcc", typeof(RecipientInfo), true, true)]
		[OwaEventParameter("From", typeof(RecipientInfo), false, true)]
		[OwaEventParameter("Recips", typeof(RecipientInfoAC), true, true)]
		[OwaEventParameter("Text", typeof(bool), false, true)]
		[OwaEventParameter("DeliveryRcpt", typeof(bool), false, true)]
		[OwaEventParameter("ReadRcpt", typeof(bool), false, true)]
		[OwaEventParameter("CmpAc", typeof(string), false, true)]
		[OwaEventParameter("HideMailTipsByDefault", typeof(bool), false, true)]
		[OwaEvent("Send")]
		[OwaEventParameter("Body", typeof(string), false, true)]
		[OwaEventParameter("Cc", typeof(RecipientInfo), true, true)]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("CK", typeof(string), false, true)]
		[OwaEventParameter("Subj", typeof(string), false, true)]
		[OwaEventParameter("Imp", typeof(Importance), false, true)]
		public void Send()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "EditMessageEventHandler.Send");
			this.ProcessMessageRequest(MessageAction.Send);
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x001042D0 File Offset: 0x001024D0
		[OwaEventParameter("DavSubmitData", typeof(string), false, true)]
		[OwaEventParameter("Cc", typeof(RecipientInfo), true, true)]
		[OwaEventParameter("Bcc", typeof(RecipientInfo), true, true)]
		[OwaEventParameter("Recips", typeof(RecipientInfoAC), true, true)]
		[OwaEventParameter("CmpAc", typeof(string), false, true)]
		[OwaEventParameter("HideMailTipsByDefault", typeof(bool), false, true)]
		[OwaEvent("SendMime")]
		[OwaEventParameter("CK", typeof(string), false, true)]
		[OwaEventParameter("MimeBlob", typeof(string))]
		[OwaEventParameter("Encrypted", typeof(bool), false, true)]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("SavToSnt", typeof(bool), false, true)]
		[OwaEventParameter("To", typeof(RecipientInfo), true, true)]
		public void SendMime()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "EditMessageEventHandler.SendMime");
			this.ProcessMessageRequest(MessageAction.SendMime);
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x001042EF File Offset: 0x001024EF
		[OwaEventParameter("To", typeof(RecipientInfo), true, true)]
		[OwaEventParameter("Recips", typeof(RecipientInfoAC), true, true)]
		[OwaEventParameter("HideMailTipsByDefault", typeof(bool), false, true)]
		[OwaEvent("SaveMime")]
		[OwaEventParameter("CmpAc", typeof(string), false, true)]
		[OwaEventParameter("CK", typeof(string), false, true)]
		[OwaEventParameter("MimeBlob", typeof(string))]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("Cc", typeof(RecipientInfo), true, true)]
		[OwaEventParameter("Bcc", typeof(RecipientInfo), true, true)]
		public void SaveMime()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "EditMessageEventHandler.SaveMime");
			this.ProcessMessageRequest(MessageAction.SaveMime);
		}

		// Token: 0x06002DE3 RID: 11747 RVA: 0x0010430E File Offset: 0x0010250E
		[OwaEventParameter("CmpAc", typeof(string), false, true)]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("UpdRcpAs", typeof(bool))]
		[OwaEvent("AutoSaveMime")]
		[OwaEventParameter("CK", typeof(string), false, true)]
		[OwaEventParameter("MimeBlob", typeof(string))]
		[OwaEventParameter("To", typeof(RecipientInfo), true, true)]
		[OwaEventParameter("Cc", typeof(RecipientInfo), true, true)]
		[OwaEventParameter("Bcc", typeof(RecipientInfo), true, true)]
		[OwaEventParameter("Recips", typeof(RecipientInfoAC), true, true)]
		public void AutoSaveMime()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "EditMessageEventHandler.MethodAutoSaveMime");
			this.TryProcessMessageRequestForAutoSave(MessageAction.AutoSaveMime);
		}

		// Token: 0x06002DE4 RID: 11748 RVA: 0x00104330 File Offset: 0x00102530
		[OwaEventParameter("CK", typeof(string), false, true)]
		[OwaEvent("PromoteInlineAttachments")]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("Body", typeof(string), false, true)]
		public void PromoteInlineAttachments()
		{
			MessageItem messageItem = null;
			try
			{
				bool flag = base.IsParameterSet("Id");
				bool flag2 = false;
				if (flag)
				{
					messageItem = base.GetRequestItem<MessageItem>(new PropertyDefinition[0]);
				}
				else
				{
					messageItem = this.CreateNewMessageItem();
				}
				if (OwaContext.Current.UserContext.IsIrmEnabled)
				{
					Utilities.IrmDecryptIfRestricted(messageItem, OwaContext.Current.UserContext, true);
				}
				if (base.IsParameterSet("Body") && (string)base.GetParameter("Body") != null)
				{
					flag2 = AttachmentUtility.RemoveUnlinkedInlineAttachments(messageItem, (string)base.GetParameter("Body"));
				}
				bool flag3 = AttachmentUtility.PromoteInlineAttachments(messageItem);
				flag2 = (flag2 || flag3);
				if (flag2)
				{
					messageItem.Load();
					base.WriteChangeKey(messageItem);
					if (flag3)
					{
						if (OwaContext.Current.UserContext.IsIrmEnabled)
						{
							Utilities.IrmDecryptIfRestricted(messageItem, OwaContext.Current.UserContext, true);
						}
						ArrayList attachmentInformation = AttachmentWell.GetAttachmentInformation(messageItem, null, base.UserContext.IsPublicLogon);
						RenderingUtilities.RenderAttachmentItems(this.SanitizingWriter, attachmentInformation, base.UserContext);
					}
				}
			}
			finally
			{
				if (messageItem != null)
				{
					messageItem.Dispose();
					messageItem = null;
				}
			}
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x0010444C File Offset: 0x0010264C
		private void ClearMessagePropertiesForMimeImport(MessageItem item)
		{
			if (item.AttachmentCollection != null && item.AttachmentCollection.Count > 0)
			{
				item.AttachmentCollection.RemoveAll();
			}
			ItemUtility.SetItemBody(item, BodyFormat.TextPlain, string.Empty);
			MessageEventHandler.ClearRecipients(item, new RecipientItemType[0]);
			item.Sensitivity = Sensitivity.Normal;
			item.Importance = Microsoft.Exchange.Data.Storage.Importance.Normal;
			item.IsReadReceiptRequested = false;
			item.IsDeliveryReceiptRequested = false;
			item.DeleteProperties(new PropertyDefinition[]
			{
				ItemSchema.IconIndex
			});
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x001044C4 File Offset: 0x001026C4
		private void ImportMessageMime(MessageItem item)
		{
			this.ClearMessagePropertiesForMimeImport(item);
			InboundConversionOptions options = Utilities.CreateInboundConversionOptions(base.UserContext);
			using (MemoryStream memoryStream = new MemoryStream(Encoding.ASCII.GetBytes((string)base.GetParameter("MimeBlob"))))
			{
				ItemConversion.ConvertAnyMimeToItem(item, memoryStream, options);
			}
			if (Utilities.IsIrmDecrypted(item))
			{
				RightsManagedMessageItem rightsManagedMessageItem = (RightsManagedMessageItem)item;
				rightsManagedMessageItem.SetProtectedData(item.Body, item.AttachmentCollection);
				rightsManagedMessageItem.SetDefaultEnvelopeBody(null);
			}
			item.From = null;
			item.Sender = null;
			base.UpdateComplianceAction(item);
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x00104570 File Offset: 0x00102770
		private void ProcessMessageRequest(MessageAction action)
		{
			bool flag = false;
			MessageItem messageItem = null;
			bool flag2 = base.IsParameterSet("Id");
			bool flag3 = false;
			RecipientInfoAC[] array = (RecipientInfoAC[])base.GetParameter("Recips");
			if (array != null && array.Length != 0)
			{
				AutoCompleteCache.UpdateAutoCompleteCacheFromRecipientInfoList(array, base.OwaContext.UserContext);
			}
			try
			{
				messageItem = this.GetMessageItem(flag2);
				base.SaveHideMailTipsByDefault();
				switch (action)
				{
				case MessageAction.Send:
				case MessageAction.Save:
				{
					StoreObjectType storeObjectType = StoreObjectType.Message;
					if (ObjectClass.IsMeetingRequest(messageItem.ClassName) || ObjectClass.IsMeetingCancellation(messageItem.ClassName) || ObjectClass.IsMeetingResponse(messageItem.ClassName))
					{
						storeObjectType = StoreObjectType.MeetingMessage;
					}
					flag = base.UpdateMessage(messageItem, storeObjectType);
					break;
				}
				case MessageAction.SendMime:
				case MessageAction.SaveMime:
					this.ImportMessageMime(messageItem);
					flag3 = (base.IsParameterSet("SavToSnt") && !(bool)base.GetParameter("SavToSnt"));
					if (action == MessageAction.SendMime && base.IsParameterSet("Encrypted") && (bool)base.GetParameter("Encrypted") && base.IsParameterSet("DavSubmitData"))
					{
						messageItem[MessageItemSchema.DavSubmitData] = (string)base.GetParameter("DavSubmitData");
					}
					else
					{
						base.UpdateRecipients(messageItem);
					}
					break;
				}
				if (action == MessageAction.Save || action == MessageAction.SaveMime)
				{
					ExTraceGlobals.MailTracer.TraceDebug((long)this.GetHashCode(), "Saving message");
					Utilities.SaveItem(messageItem, flag2);
					base.WriteIdAndChangeKey(messageItem, flag2);
					if (!flag2 && ExTraceGlobals.MailDataTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.MailDataTracer.TraceDebug<string>((long)this.GetHashCode(), "EditMessageEventHandler.ProcessMessageRequest - New message ID is '{0}'", messageItem.Id.ObjectId.ToBase64String());
					}
				}
				else if (action == MessageAction.Send || action == MessageAction.SendMime)
				{
					if (flag)
					{
						throw new OwaEventHandlerException("Unresolved recipients", LocalizedStrings.GetNonEncoded(2063734279));
					}
					RightsManagedMessageItem rightsManagedMessageItem = messageItem as RightsManagedMessageItem;
					bool flag4 = rightsManagedMessageItem != null && rightsManagedMessageItem.Restriction != null;
					if (messageItem.Recipients.Count == 0 && (action == MessageAction.Send || action != MessageAction.SendMime || !base.IsParameterSet("Encrypted") || !(bool)base.GetParameter("Encrypted")))
					{
						throw new OwaEventHandlerException("No recipients", LocalizedStrings.GetNonEncoded(1878192149));
					}
					if (Utilities.RecipientsOnlyHaveEmptyPDL<Recipient>(base.UserContext, messageItem.Recipients))
					{
						base.RenderPartialFailure(1389137820);
					}
					else
					{
						ExTraceGlobals.MailTracer.TraceDebug((long)this.GetHashCode(), "Sending message");
						try
						{
							if (flag3)
							{
								messageItem.SendWithoutSavingMessage();
							}
							else
							{
								messageItem.Send();
							}
							if (null != base.FromParticipant)
							{
								Participant v = new Participant(base.UserContext.ExchangePrincipal);
								if (!base.FromParticipant.AreAddressesEqual(v))
								{
									RecipientInfoCacheEntry fromRecipientEntry = base.GetFromRecipientEntry(base.FromParticipant.EmailAddress);
									if (fromRecipientEntry != null)
									{
										RecipientCache recipientCache = SendFromCache.TryGetCache(base.UserContext);
										if (recipientCache != null)
										{
											recipientCache.AddEntry(fromRecipientEntry);
										}
									}
								}
							}
						}
						catch (Exception ex)
						{
							ExTraceGlobals.MailDataTracer.TraceError<string>((long)this.GetHashCode(), "EditMessageEventHandler.ProcessMessageRequestmessage - Exception: {0}", ex.Message);
							if (Utilities.ShouldSendChangeKeyForException(ex))
							{
								ExTraceGlobals.MailDataTracer.TraceDebug<string>((long)this.GetHashCode(), "EditMessageEventHandler.ProcessMessageRequestmessage attempt to send change key on exception: {0}", ex.Message);
								base.SaveIdAndChangeKeyInCustomErrorInfo(messageItem);
							}
							throw;
						}
						if (Globals.ArePerfCountersEnabled)
						{
							OwaSingleCounters.MessagesSent.Increment();
							if (flag4)
							{
								OwaSingleCounters.IRMMessagesSent.Increment();
							}
						}
					}
				}
			}
			finally
			{
				if (messageItem != null)
				{
					messageItem.Dispose();
					messageItem = null;
				}
			}
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x001048FC File Offset: 0x00102AFC
		private void TryProcessMessageRequestForAutoSave(MessageAction action)
		{
			ExTraceGlobals.MailTracer.TraceDebug((long)this.GetHashCode(), "Auto-saving message");
			MessageItem messageItem = null;
			bool flag = base.IsParameterSet("Id");
			try
			{
				messageItem = this.GetMessageItem(flag);
				switch (action)
				{
				case MessageAction.AutoSave:
					base.UpdateMessageForAutoSave(messageItem, StoreObjectType.Message);
					break;
				case MessageAction.AutoSaveMime:
					this.ImportMessageMime(messageItem);
					if (base.UpdateRecipientsOnAutosave())
					{
						base.UpdateRecipients(messageItem);
					}
					break;
				}
				Utilities.SaveItem(messageItem, flag);
				base.WriteIdAndChangeKey(messageItem, flag);
				if (!flag && ExTraceGlobals.MailDataTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.MailDataTracer.TraceDebug<string>((long)this.GetHashCode(), "EditMessageEventHandler.TryProcessMessageRequestForAutoSave - New message ID is '{0}'", messageItem.Id.ObjectId.ToBase64String());
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.MailTracer.TraceError<string>((long)this.GetHashCode(), "EditMessageEventHandler.TryProcessMessageRequestForAutoSave - Exception {0} thrown during autosave", ex.Message);
				if (Utilities.ShouldSendChangeKeyForException(ex))
				{
					ExTraceGlobals.MailDataTracer.TraceDebug<string>((long)this.GetHashCode(), "EditMessageEventHandler.TryProcessMessageRequestForAutoSave attempt to send change key on exception: {0}", ex.Message);
					base.SaveIdAndChangeKeyInCustomErrorInfo(messageItem);
				}
				base.RenderErrorForAutoSave(ex);
			}
			finally
			{
				if (messageItem != null)
				{
					messageItem.Dispose();
					messageItem = null;
				}
			}
		}

		// Token: 0x06002DE9 RID: 11753 RVA: 0x00104A30 File Offset: 0x00102C30
		private MessageItem GetMessageItem(bool existingMessage)
		{
			MessageItem messageItem;
			if (existingMessage)
			{
				ExTraceGlobals.MailTracer.TraceDebug((long)this.GetHashCode(), "ItemId set in the request. Existing message");
				messageItem = base.GetRequestItem<MessageItem>(new PropertyDefinition[0]);
				if (base.UserContext.IsIrmEnabled && !Utilities.IrmDecryptIfRestricted(messageItem, base.UserContext, true) && this.ApplyingIrmCompliance())
				{
					messageItem = RightsManagedMessageItem.Create(messageItem, Utilities.CreateOutboundConversionOptions(base.UserContext));
				}
			}
			else
			{
				ExTraceGlobals.MailTracer.TraceDebug((long)this.GetHashCode(), "ItemId wasn't set in the request. Creating new message in the drafts folder.");
				messageItem = this.CreateNewMessageItem();
				messageItem[ItemSchema.ConversationIndexTracking] = true;
				if (Globals.ArePerfCountersEnabled)
				{
					OwaSingleCounters.ItemsCreated.Increment();
				}
			}
			return messageItem;
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x00104AE0 File Offset: 0x00102CE0
		private MessageItem CreateNewMessageItem()
		{
			MessageItem result;
			if (this.ApplyingIrmCompliance())
			{
				result = RightsManagedMessageItem.Create(base.UserContext.MailboxSession, base.UserContext.DraftsFolderId, Utilities.CreateOutboundConversionOptions(base.UserContext));
			}
			else
			{
				result = MessageItem.Create(base.UserContext.MailboxSession, base.UserContext.DraftsFolderId);
			}
			return result;
		}

		// Token: 0x06002DEB RID: 11755 RVA: 0x00104B3C File Offset: 0x00102D3C
		private bool ApplyingIrmCompliance()
		{
			object parameter = base.GetParameter("CmpAc");
			if (parameter == null)
			{
				return false;
			}
			string g = (string)parameter;
			Guid empty = Guid.Empty;
			return GuidHelper.TryParseGuid(g, out empty) && ComplianceType.RmsTemplate == base.UserContext.ComplianceReader.GetComplianceType(empty, base.UserContext.UserCulture);
		}

		// Token: 0x04001EED RID: 7917
		public const string EventNamespace = "EditMessage";

		// Token: 0x04001EEE RID: 7918
		public const string MethodSend = "Send";

		// Token: 0x04001EEF RID: 7919
		public const string MethodLoadOptionsDialog = "LOD";

		// Token: 0x04001EF0 RID: 7920
		public const string MethodSendMime = "SendMime";

		// Token: 0x04001EF1 RID: 7921
		public const string MethodSaveMime = "SaveMime";

		// Token: 0x04001EF2 RID: 7922
		public const string MethodAutoSaveMime = "AutoSaveMime";

		// Token: 0x04001EF3 RID: 7923
		public const string MimeBlob = "MimeBlob";

		// Token: 0x04001EF4 RID: 7924
		public const string Encrypted = "Encrypted";

		// Token: 0x04001EF5 RID: 7925
		public const string DavSubmitData = "DavSubmitData";

		// Token: 0x04001EF6 RID: 7926
		public const string SaveToSentItems = "SavToSnt";
	}
}
