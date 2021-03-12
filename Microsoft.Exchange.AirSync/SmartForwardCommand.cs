using System;
using System.IO;
using System.Net;
using System.Xml;
using Microsoft.Exchange.AirSync.SchemaConverter.Entity;
using Microsoft.Exchange.AirSync.SchemaConverter.XSO;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200024F RID: 591
	internal class SmartForwardCommand : SendMailBase
	{
		// Token: 0x0600158C RID: 5516 RVA: 0x0007F74C File Offset: 0x0007D94C
		public SmartForwardCommand()
		{
			base.PerfCounter = AirSyncCounters.NumberOfSmartForwards;
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x0600158D RID: 5517 RVA: 0x0007F75F File Offset: 0x0007D95F
		// (set) Token: 0x0600158E RID: 5518 RVA: 0x0007F767 File Offset: 0x0007D967
		internal ICalendaringContainer CalendaringContainer { get; set; }

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x0007F770 File Offset: 0x0007D970
		// (set) Token: 0x06001590 RID: 5520 RVA: 0x0007F778 File Offset: 0x0007D978
		internal IStoreSession StoreSession { get; set; }

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x0007F781 File Offset: 0x0007D981
		protected override string RootNodeName
		{
			get
			{
				return "SmartForward";
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001592 RID: 5522 RVA: 0x0007F788 File Offset: 0x0007D988
		protected override bool IsInteractiveCommand
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x0007F78C File Offset: 0x0007D98C
		internal override void ParseXmlRequest()
		{
			base.ParseXmlRequest();
			if (base.Version >= 160)
			{
				if (base.XmlRequest["Mime"] != null)
				{
					if (base.XmlRequest["Importance"] != null || base.XmlRequest["Body", "AirSyncBase:"] != null || base.XmlRequest["Forwardees"] != null)
					{
						base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "InvalidMimeTags");
						throw new AirSyncPermanentException(HttpStatusCode.InternalServerError, StatusCode.InvalidMimeBodyCombination, null, false);
					}
				}
				else
				{
					if (base.XmlRequest["SaveInSentItems"] != null || base.XmlRequest["ReplaceMime"] != null || base.XmlRequest["TemplateID", "RightsManagement:"] != null)
					{
						base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "InvalidBodyTags");
						throw new AirSyncPermanentException(HttpStatusCode.InternalServerError, StatusCode.InvalidMimeBodyCombination, null, false);
					}
					try
					{
						this.forwardEventParameters = EventParametersParser.ParseForward(base.Request.CommandXml);
					}
					catch (RequestParsingException ex)
					{
						base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, ex.LogMessage);
						throw new AirSyncPermanentException(HttpStatusCode.InternalServerError, StatusCode.InvalidSmartForwardParameters, null, false);
					}
				}
			}
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x0007F8D0 File Offset: 0x0007DAD0
		internal override Command.ExecutionState ExecuteCommand()
		{
			base.ValidateBody();
			StoreObjectId smartItemId = base.GetSmartItemId();
			if (base.Version >= 160 && (smartItemId.ObjectType == StoreObjectType.CalendarItem || smartItemId.ObjectType == StoreObjectType.CalendarItemOccurrence || smartItemId.ObjectType == StoreObjectType.CalendarItemSeries))
			{
				return this.ForwardUsingEntities(smartItemId);
			}
			return this.ForwardUsingXso(smartItemId);
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x0007F924 File Offset: 0x0007DB24
		private Command.ExecutionState ForwardUsingEntities(StoreObjectId smartId)
		{
			if (base.Occurrence != ExDateTime.MinValue)
			{
				base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "NoOccurrenceSupport");
				throw new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.ServerError, null, false);
			}
			if (this.CalendaringContainer == null)
			{
				this.CalendaringContainer = new CalendaringContainer(base.MailboxSession, null);
			}
			if (this.StoreSession == null)
			{
				this.StoreSession = base.MailboxSession;
			}
			string key = EntitySyncItem.GetKey(this.StoreSession.MailboxGuid, smartId);
			IEvents events = EntitySyncItem.GetEvents(this.CalendaringContainer, this.StoreSession, smartId);
			events.Forward(key, this.forwardEventParameters, null);
			return Command.ExecutionState.Complete;
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x0007FA10 File Offset: 0x0007DC10
		private Command.ExecutionState ForwardUsingXso(StoreObjectId smartId)
		{
			Item smartItem = base.GetSmartItem(smartId);
			MessageItem messageItem = null;
			VersionedId versionedId = null;
			MessageItem messageItem2 = null;
			CalendarItemBase calendarItemBase = null;
			try
			{
				StoreObjectId defaultFolderId = base.MailboxSession.GetDefaultFolderId(DefaultFolderType.Drafts);
				messageItem = MessageItem.Create(base.MailboxSession, defaultFolderId);
				base.ParseMimeToMessage(messageItem);
				messageItem.Save(SaveMode.NoConflictResolution);
				messageItem.Load();
				versionedId = messageItem.Id;
				messageItem.Dispose();
				messageItem = MessageItem.Bind(base.MailboxSession, versionedId);
				RmsTemplate rmsTemplate = null;
				SendMailBase.IrmAction irmAction = base.GetIrmAction(delegate(RightsManagedMessageItem originalRightsManagedItem)
				{
					if (originalRightsManagedItem == null)
					{
						throw new ArgumentNullException("originalRightsManagedItem");
					}
					if (!originalRightsManagedItem.UsageRights.IsUsageRightGranted(ContentRight.Forward))
					{
						throw new AirSyncPermanentException(StatusCode.IRM_OperationNotPermitted, false)
						{
							ErrorStringForProtocolLogger = "sfcEOperationNotPermitted"
						};
					}
				}, ref smartItem, out rmsTemplate);
				Microsoft.Exchange.Data.Storage.BodyFormat bodyFormat = messageItem.Body.Format;
				MeetingMessage meetingMessage = smartItem as MeetingMessage;
				string text;
				if ((base.ReplaceMime || irmAction == SendMailBase.IrmAction.CreateNewPublishingLicenseAttachOriginalMessage) && meetingMessage != null && !meetingMessage.IsDelegated() && (meetingMessage is MeetingRequest || meetingMessage is MeetingCancellation))
				{
					text = string.Empty;
				}
				else
				{
					using (TextReader textReader = messageItem.Body.OpenTextReader(bodyFormat))
					{
						text = textReader.ReadToEnd();
					}
					Body body = (irmAction == SendMailBase.IrmAction.CreateNewPublishingLicenseInlineOriginalBody || irmAction == SendMailBase.IrmAction.ReusePublishingLicenseInlineOriginalBody) ? ((RightsManagedMessageItem)smartItem).ProtectedBody : smartItem.Body;
					if (body.Format == Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml)
					{
						if (bodyFormat == Microsoft.Exchange.Data.Storage.BodyFormat.TextPlain)
						{
							XmlDocument xmlDocument = new SafeXmlDocument();
							XmlNode xmlNode = xmlDocument.CreateElement("PRE");
							XmlAttribute xmlAttribute = xmlDocument.CreateAttribute("STYLE");
							xmlAttribute.Value = "word-wrap:break-word; font-size:10.0pt; font-family:Tahoma; color:black";
							xmlNode.Attributes.Append(xmlAttribute);
							xmlNode.InnerText = text;
							text = xmlNode.OuterXml;
						}
						bodyFormat = Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml;
					}
				}
				ReplyForwardConfiguration replyForwardConfiguration = new ReplyForwardConfiguration(bodyFormat);
				replyForwardConfiguration.ConversionOptionsForSmime = AirSyncUtility.GetInboundConversionOptions();
				replyForwardConfiguration.AddBodyPrefix(text);
				if (base.Version >= 120)
				{
					if (smartItem is MessageItem)
					{
						messageItem2 = ((MessageItem)smartItem).CreateForward(defaultFolderId, replyForwardConfiguration);
						if (irmAction == SendMailBase.IrmAction.CreateNewPublishingLicense || irmAction == SendMailBase.IrmAction.CreateNewPublishingLicenseInlineOriginalBody || irmAction == SendMailBase.IrmAction.CreateNewPublishingLicenseAttachOriginalMessage)
						{
							messageItem2 = base.GetRightsManagedReplyForward(messageItem2, irmAction, rmsTemplate);
						}
					}
					else if (smartItem is CalendarItem)
					{
						CalendarItem calendarItem = (CalendarItem)smartItem;
						calendarItemBase = base.GetCalendarItemBaseToReplyOrForward(calendarItem);
						messageItem2 = calendarItemBase.CreateForward(defaultFolderId, replyForwardConfiguration);
						if (!calendarItem.IsMeeting)
						{
							BodyConversionUtilities.CopyBody(messageItem, messageItem2);
						}
					}
					if (messageItem2 == null)
					{
						base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "ForwardFailed");
						throw new AirSyncPermanentException(HttpStatusCode.InternalServerError, StatusCode.MailSubmissionFailed, null, false);
					}
					if (base.ReplaceMime || irmAction == SendMailBase.IrmAction.CreateNewPublishingLicenseAttachOriginalMessage)
					{
						RightsManagedMessageItem rightsManagedMessageItem = messageItem2 as RightsManagedMessageItem;
						if (rightsManagedMessageItem != null)
						{
							rightsManagedMessageItem.ProtectedAttachmentCollection.RemoveAll();
						}
						else
						{
							messageItem2.AttachmentCollection.RemoveAll();
						}
					}
					base.CopyMessageContents(messageItem, messageItem2, false, (irmAction == SendMailBase.IrmAction.CreateNewPublishingLicenseAttachOriginalMessage) ? smartItem : null);
					base.SendMessage(messageItem2);
				}
				else if (smartItem is MessageItem)
				{
					using (ItemAttachment itemAttachment = messageItem.AttachmentCollection.AddExistingItem(smartItem))
					{
						MessageItem messageItem3 = (MessageItem)smartItem;
						itemAttachment.FileName = messageItem3.Subject + itemAttachment.FileExtension;
						itemAttachment.Save();
					}
					base.SendMessage(messageItem);
				}
				else if (smartItem is CalendarItem)
				{
					CalendarItem calendarItem2 = (CalendarItem)smartItem;
					messageItem2 = calendarItem2.CreateForward(defaultFolderId, replyForwardConfiguration);
					if (messageItem2 == null)
					{
						base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "ForwardFailed2");
						throw new AirSyncPermanentException(HttpStatusCode.InternalServerError, StatusCode.MailSubmissionFailed, null, false);
					}
					if (!calendarItem2.IsMeeting)
					{
						BodyConversionUtilities.CopyBody(messageItem, messageItem2);
					}
					base.CopyMessageContents(messageItem, messageItem2, false, null);
					base.SendMessage(messageItem2);
				}
			}
			finally
			{
				if (messageItem != null)
				{
					if (versionedId != null)
					{
						base.MailboxSession.Delete(DeleteItemFlags.HardDelete, new StoreId[]
						{
							versionedId
						});
					}
					messageItem.Dispose();
				}
				if (smartItem != null)
				{
					smartItem.Dispose();
				}
				if (messageItem2 != null)
				{
					messageItem2.Dispose();
				}
				if (calendarItemBase != null)
				{
					calendarItemBase.Dispose();
				}
			}
			return Command.ExecutionState.Complete;
		}

		// Token: 0x04000CAB RID: 3243
		private ForwardEventParameters forwardEventParameters;
	}
}
