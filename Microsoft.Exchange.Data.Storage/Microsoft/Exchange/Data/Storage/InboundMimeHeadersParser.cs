using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005E3 RID: 1507
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class InboundMimeHeadersParser
	{
		// Token: 0x06003DE8 RID: 15848 RVA: 0x00100830 File Offset: 0x000FEA30
		private static Dictionary<object, InboundMimeHeadersParser.IHeaderPromotionRule> CreateHeaderRulesTable()
		{
			Dictionary<object, InboundMimeHeadersParser.IHeaderPromotionRule> dictionary = new Dictionary<object, InboundMimeHeadersParser.IHeaderPromotionRule>();
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.From, new InboundMimeHeadersParser.AddressHeaderRule(delegate(InboundMimeHeadersParser self, Header header, Participant from)
			{
				self.AddressCache.Participants[ConversionItemParticipants.ParticipantIndex.From] = from;
				if (!self.CheckIsHeaderPresent(HeaderId.Sender))
				{
					self.AddressCache.Participants[ConversionItemParticipants.ParticipantIndex.Sender] = from;
				}
			}, InboundMimeHeadersParser.AddressHeaderFlags.DeencapsulateIfSenderTrusted));
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.Sender, new InboundMimeHeadersParser.AddressHeaderRule(delegate(InboundMimeHeadersParser self, Header header, Participant sender)
			{
				self.AddressCache.Participants[ConversionItemParticipants.ParticipantIndex.Sender] = sender;
			}, InboundMimeHeadersParser.AddressHeaderFlags.DeencapsulateIfSenderTrusted));
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.To, new InboundMimeHeadersParser.AddressHeaderRule(delegate(InboundMimeHeadersParser self, Header header, List<Participant> toRecipients)
			{
				self.AddressCache.AddRecipients(toRecipients, RecipientItemType.To);
			}));
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.Cc, new InboundMimeHeadersParser.AddressHeaderRule(delegate(InboundMimeHeadersParser self, Header header, List<Participant> ccRecipients)
			{
				self.AddressCache.AddRecipients(ccRecipients, RecipientItemType.Cc);
			}));
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.Bcc, new InboundMimeHeadersParser.AddressHeaderRule(delegate(InboundMimeHeadersParser self, Header header, List<Participant> bccRecipients)
			{
				self.AddressCache.AddRecipients(bccRecipients, RecipientItemType.Bcc);
			}));
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.ReplyTo, new InboundMimeHeadersParser.AddressHeaderRule(delegate(InboundMimeHeadersParser self, Header header, List<Participant> replyToRecipients)
			{
				self.AddressCache.AddReplyTo(replyToRecipients);
			}));
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.MessageId, new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.InternetMessageId));
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.Date, new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.SentTime, new InboundMimeHeadersParser.HeaderPropertyRule.HeaderValueTransformation(InboundMimeHeadersParser.ToDateTime)));
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.DeferredDelivery, new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.DeferredDeliveryTime, new InboundMimeHeadersParser.HeaderPropertyRule.HeaderValueTransformation(InboundMimeHeadersParser.ToDateTime)));
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.References, new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.InternetReferences, 65536));
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.Sensitivity, new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.Sensitivity, new InboundMimeHeadersParser.HeaderPropertyRule.HeaderValueTransformation(InboundMimeHeadersParser.ToSensitivity)));
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.Importance, new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.Importance, new InboundMimeHeadersParser.HeaderPropertyRule.HeaderValueTransformation(InboundMimeHeadersParser.ToImportance)));
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.Priority, new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.Importance, new InboundMimeHeadersParser.HeaderPropertyRule.HeaderValueTransformation(InboundMimeHeadersParser.PriorityToImportance), new InboundMimeHeadersParser.HeaderPriorityList(new HeaderId[]
			{
				HeaderId.Importance
			})));
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.XMSMailPriority, new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.Importance, new InboundMimeHeadersParser.HeaderPropertyRule.HeaderValueTransformation(InboundMimeHeadersParser.ToImportance), new InboundMimeHeadersParser.HeaderPriorityList(new HeaderId[]
			{
				HeaderId.Importance,
				HeaderId.Priority
			})));
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.XPriority, new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.Importance, new InboundMimeHeadersParser.HeaderPropertyRule.HeaderValueTransformation(InboundMimeHeadersParser.XPriorityToImportance), new InboundMimeHeadersParser.HeaderPriorityList(new HeaderId[]
			{
				HeaderId.Importance,
				HeaderId.Priority,
				HeaderId.XMSMailPriority
			})));
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.Subject, new InboundMimeHeadersParser.CustomRule(new InboundMimeHeadersParser.CustomRule.PromotionDelegate(InboundMimeHeadersParser.PromoteSubject)));
			InboundMimeHeadersParser.AddRule(dictionary, "Thread-Topic", new InboundMimeHeadersParser.CustomRule(new InboundMimeHeadersParser.CustomRule.PromotionDelegate(InboundMimeHeadersParser.PromoteThreadTopic)));
			InboundMimeHeadersParser.AddRule(dictionary, "Thread-Index", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.ConversationIndex, delegate(InboundMimeHeadersParser self, Header header, string value)
			{
				object result;
				try
				{
					result = Convert.FromBase64String(value);
				}
				catch (FormatException)
				{
					StorageGlobals.ContextTraceDebug<string, string>(ExTraceGlobals.CcInboundMimeTracer, "InboundMimeHeadersParser: failed to parse header value ({0}: {1})", header.Name, value);
					result = null;
				}
				return result;
			}));
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.InReplyTo, new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.InReplyTo));
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.ReplyBy, new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.ReplyTime, new InboundMimeHeadersParser.HeaderPropertyRule.HeaderValueTransformation(InboundMimeHeadersParser.ToDateTime)));
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.ContentLanguage, new InboundMimeHeadersParser.CustomRule(new InboundMimeHeadersParser.CustomRule.PromotionDelegate(InboundMimeHeadersParser.PromoteContentLanguage)));
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.Keywords, new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.Categories, new InboundMimeHeadersParser.HeaderPropertyRule.HeaderValueTransformation(InboundMimeHeadersParser.KeywordsToCategories)));
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.Expires, new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.ExpiryTime, new InboundMimeHeadersParser.HeaderPropertyRule.HeaderValueTransformation(InboundMimeHeadersParser.ToDateTime)));
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.ExpiryDate, new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.ExpiryTime, new InboundMimeHeadersParser.HeaderPropertyRule.HeaderValueTransformation(InboundMimeHeadersParser.ToDateTime)));
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.DispositionNotificationTo, new InboundMimeHeadersParser.AddressHeaderRule(new InboundMimeHeadersParser.AddressHeaderRule.PromoteSingleAddress(InboundMimeHeadersParser.PromoteDispositionNotificationTo), InboundMimeHeadersParser.AddressHeaderFlags.AlwaysDeencapsulate));
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.ReturnReceiptTo, new InboundMimeHeadersParser.AddressHeaderRule(new InboundMimeHeadersParser.AddressHeaderRule.PromoteSingleAddress(InboundMimeHeadersParser.PromoteReturnReceiptTo), InboundMimeHeadersParser.AddressHeaderFlags.AlwaysDeencapsulate));
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.Precedence, new InboundMimeHeadersParser.CustomRule(new InboundMimeHeadersParser.CustomRule.PromotionDelegate(InboundMimeHeadersParser.PromotePrecedence)));
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.ContentClass, new InboundMimeHeadersParser.CustomRule(new InboundMimeHeadersParser.CustomRule.PromotionDelegate(InboundMimeHeadersParser.PromoteContentClass)));
			InboundMimeHeadersParser.AddRule(dictionary, "X-Message-Flag", new InboundMimeHeadersParser.CustomRule(new InboundMimeHeadersParser.CustomRule.PromotionDelegate(InboundMimeHeadersParser.PromoteXMessageFlag)));
			InboundMimeHeadersParser.AddRule(dictionary, "X-Auto-Response-Suppress", new InboundMimeHeadersParser.CustomRule(new InboundMimeHeadersParser.CustomRule.PromotionDelegate(InboundMimeHeadersParser.PromoteXAutoResponseSuppress)));
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.ListHelp, new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.ListHelp));
			InboundMimeHeadersParser.AddRule(dictionary, "X-List-Help", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.ListHelp, new InboundMimeHeadersParser.HeaderPriorityList(new HeaderId[]
			{
				HeaderId.ListHelp
			})));
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.ListSubscribe, new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.ListSubscribe));
			InboundMimeHeadersParser.AddRule(dictionary, "X-List-Subscribe", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.ListSubscribe, new InboundMimeHeadersParser.HeaderPriorityList(new HeaderId[]
			{
				HeaderId.ListSubscribe
			})));
			InboundMimeHeadersParser.AddRule(dictionary, HeaderId.ListUnsubscribe, new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.ListUnsubscribe));
			InboundMimeHeadersParser.AddRule(dictionary, "X-List-Unsubscribe", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.ListUnsubscribe, new InboundMimeHeadersParser.HeaderPriorityList(new HeaderId[]
			{
				HeaderId.ListUnsubscribe
			})));
			InboundMimeHeadersParser.AddRule(dictionary, "Accept-Language", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.AcceptLanguage));
			InboundMimeHeadersParser.AddRule(dictionary, "X-Accept-Language", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.AcceptLanguage, new InboundMimeHeadersParser.HeaderPriorityList(new string[]
			{
				"Accept-Language"
			})));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-Organization-SCL", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.SpamConfidenceLevel, delegate(InboundMimeHeadersParser self, Header header, string value)
			{
				int num = 0;
				if (int.TryParse(value, out num) && num >= -1 && num <= 10)
				{
					return num;
				}
				StorageGlobals.ContextTraceDebug<string, string>(ExTraceGlobals.CcInboundMimeTracer, "InboundMimeHeadersParser: failed to parse header value ({0}: {1})", header.Name, value);
				return null;
			}));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-Organization-Original-SCL", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.OriginalScl, new InboundMimeHeadersParser.HeaderPropertyRule.HeaderValueTransformation(InboundMimeHeadersParser.ToInt32)));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-Organization-PCL", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.ContentFilterPcl, delegate(InboundMimeHeadersParser self, Header header, string value)
			{
				int num;
				if (int.TryParse(value, out num) && ConvertUtils.IsValidPCL(num))
				{
					return num;
				}
				StorageGlobals.ContextTraceDebug<string, string>(ExTraceGlobals.CcInboundMimeTracer, "InboundMimeHeadersParser: failed to parse header value ({0}: {1})", header.Name, value);
				return null;
			}));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-Organization-PRD", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.PurportedSenderDomain));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-Organization-SenderIdResult", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.SenderIdStatus, delegate(InboundMimeHeadersParser self, Header header, string value)
			{
				SenderIdStatus senderIdStatus;
				if (EnumValidator<SenderIdStatus>.TryParse(value, EnumParseOptions.IgnoreCase, out senderIdStatus))
				{
					return (int)senderIdStatus;
				}
				StorageGlobals.ContextTraceDebug<string, string>(ExTraceGlobals.CcInboundMimeTracer, "InboundMimeHeadersParser: failed to parse header value ({0}: {1})", header.Name, value);
				return null;
			}));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-Organization-AVStamp-Mailbox", new InboundMimeHeadersParser.CustomRule(new InboundMimeHeadersParser.CustomRule.PromotionDelegate(InboundMimeHeadersParser.PromoteAVStampMailbox)));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-Organization-Recipient-P2-Type", new InboundMimeHeadersParser.CustomRule(new InboundMimeHeadersParser.CustomRule.PromotionDelegate(InboundMimeHeadersParser.PromoteRecipientP2Type)));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-Organization-Outlook-Protection-Rule-Addin-Version", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.XMSExchangeOutlookProtectionRuleVersion));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-Organization-Outlook-Protection-Rule-Config-Timestamp", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.XMSExchangeOutlookProtectionRuleConfigTimestamp));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-Organization-Outlook-Protection-Rule-Overridden", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.XMSExchangeOutlookProtectionRuleOverridden));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-Organization-DeliverAsRead", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.DeliverAsRead, (InboundMimeHeadersParser self, Header header, string value) => true));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-Organization-MailReplied", new InboundMimeHeadersParser.CustomRule(delegate(InboundMimeHeadersParser self, Header header, string value)
			{
				IconIndex valueOrDefault = self.Item.GetValueOrDefault<IconIndex>(InternalSchema.IconIndex, IconIndex.Default);
				IconIndex iconIndex = valueOrDefault;
				if (iconIndex <= IconIndex.MailUnread)
				{
					switch (iconIndex)
					{
					case IconIndex.Default:
					case IconIndex.PostItem:
						break;
					case (IconIndex)0:
						return;
					default:
						switch (iconIndex)
						{
						case IconIndex.BaseMail:
						case IconIndex.MailUnread:
							break;
						default:
							return;
						}
						break;
					}
				}
				else if (iconIndex != IconIndex.MailForwarded)
				{
					switch (iconIndex)
					{
					case IconIndex.MailEncrypted:
					case IconIndex.MailEncryptedForwarded:
					case IconIndex.MailEncryptedRead:
						self.Item[InternalSchema.IconIndex] = IconIndex.MailEncryptedReplied;
						return;
					case IconIndex.MailSmimeSigned:
					case IconIndex.MailSmimeSignedForwarded:
					case IconIndex.MailSmimeSignedRead:
						self.Item[InternalSchema.IconIndex] = IconIndex.MailSmimeSignedReplied;
						return;
					case (IconIndex)274:
					case IconIndex.MailEncryptedReplied:
					case IconIndex.MailSmimeSignedReplied:
						return;
					default:
						switch (iconIndex)
						{
						case IconIndex.MailIrm:
						case IconIndex.MailIrmForwarded:
							self.Item[InternalSchema.IconIndex] = IconIndex.MailIrmReplied;
							return;
						default:
							return;
						}
						break;
					}
				}
				self.Item[InternalSchema.IconIndex] = IconIndex.MailReplied;
			}));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-Organization-MailForwarded", new InboundMimeHeadersParser.CustomRule(delegate(InboundMimeHeadersParser self, Header header, string value)
			{
				IconIndex valueOrDefault = self.Item.GetValueOrDefault<IconIndex>(InternalSchema.IconIndex, IconIndex.Default);
				IconIndex iconIndex = valueOrDefault;
				if (iconIndex <= IconIndex.MailUnread)
				{
					switch (iconIndex)
					{
					case IconIndex.Default:
					case IconIndex.PostItem:
						break;
					case (IconIndex)0:
						return;
					default:
						switch (iconIndex)
						{
						case IconIndex.BaseMail:
						case IconIndex.MailUnread:
							break;
						default:
							return;
						}
						break;
					}
				}
				else if (iconIndex != IconIndex.MailReplied)
				{
					switch (iconIndex)
					{
					case IconIndex.MailEncrypted:
					case IconIndex.MailEncryptedReplied:
					case IconIndex.MailEncryptedRead:
						self.Item[InternalSchema.IconIndex] = IconIndex.MailEncryptedForwarded;
						return;
					case IconIndex.MailSmimeSigned:
					case IconIndex.MailSmimeSignedReplied:
					case IconIndex.MailSmimeSignedRead:
						self.Item[InternalSchema.IconIndex] = IconIndex.MailSmimeSignedForwarded;
						return;
					case (IconIndex)274:
					case IconIndex.MailEncryptedForwarded:
					case IconIndex.MailSmimeSignedForwarded:
						return;
					default:
						switch (iconIndex)
						{
						case IconIndex.MailIrm:
						case IconIndex.MailIrmReplied:
							self.Item[InternalSchema.IconIndex] = IconIndex.MailIrmForwarded;
							return;
						case IconIndex.MailIrmForwarded:
							return;
						default:
							return;
						}
						break;
					}
				}
				self.Item[InternalSchema.IconIndex] = IconIndex.MailForwarded;
			}));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-Organization-Category", new InboundMimeHeadersParser.CustomRule(delegate(InboundMimeHeadersParser self, Header header, string value)
			{
				string[] array = self.Item.GetValueOrDefault<string[]>(InternalSchema.Categories, null);
				if (array == null || array.Length == 0)
				{
					array = new string[]
					{
						value
					};
				}
				else
				{
					string[] array2 = new string[array.Length + 1];
					Array.Copy(array, 0, array2, 0, array.Length);
					array2[array.Length] = value;
					array = array2;
				}
				self.Item[InternalSchema.Categories] = array;
			}));
			InboundMimeHeadersParser.AddRule(dictionary, "X-Payload-Class", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.AttachPayloadClass));
			InboundMimeHeadersParser.AddRule(dictionary, "X-Payload-Provider-Guid", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.AttachPayloadProviderGuidString));
			InboundMimeHeadersParser.AddRule(dictionary, "x-microsoft-classified", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.IsClassified, new InboundMimeHeadersParser.HeaderPropertyRule.HeaderValueTransformation(InboundMimeHeadersParser.ToBoolean)));
			InboundMimeHeadersParser.AddRule(dictionary, "X-microsoft-classKeep", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.ClassificationKeep, new InboundMimeHeadersParser.HeaderPropertyRule.HeaderValueTransformation(InboundMimeHeadersParser.ToBoolean)));
			InboundMimeHeadersParser.AddRule(dictionary, "x-microsoft-classification", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.Classification));
			InboundMimeHeadersParser.AddRule(dictionary, "x-microsoft-classDesc", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.ClassificationDescription));
			InboundMimeHeadersParser.AddRule(dictionary, "x-microsoft-classID", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.ClassificationGuid));
			InboundMimeHeadersParser.AddRule(dictionary, "X-RequireProtectedPlayOnPhone", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.XRequireProtectedPlayOnPhone));
			InboundMimeHeadersParser.AddRule(dictionary, "X-CallingTelephoneNumber", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.SenderTelephoneNumber));
			InboundMimeHeadersParser.AddRule(dictionary, "X-VoiceMessageSenderName", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.VoiceMessageSenderName));
			InboundMimeHeadersParser.AddRule(dictionary, "X-AttachmentOrder", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.VoiceMessageAttachmentOrder));
			InboundMimeHeadersParser.AddRule(dictionary, "X-CallID", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.CallId));
			InboundMimeHeadersParser.AddRule(dictionary, "X-VoiceMessageDuration", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.VoiceMessageDuration, new InboundMimeHeadersParser.HeaderPropertyRule.HeaderValueTransformation(InboundMimeHeadersParser.ToInt32)));
			InboundMimeHeadersParser.AddRule(dictionary, "X-FaxNumberOfPages", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.FaxNumberOfPages, new InboundMimeHeadersParser.HeaderPropertyRule.HeaderValueTransformation(InboundMimeHeadersParser.ToInt32)));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-UM-PartnerContent", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.XMsExchangeUMPartnerContent));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-UM-PartnerContext", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.XMsExchangeUMPartnerContext));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-UM-PartnerStatus", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.XMsExchangeUMPartnerStatus));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-UM-PartnerAssignedID", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.XMsExchangeUMPartnerAssignedID));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-UM-DialPlanLanguage", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.XMsExchangeUMDialPlanLanguage));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-UM-CallerInformedOfAnalysis", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.XMsExchangeUMCallerInformedOfAnalysis));
			InboundMimeHeadersParser.AddRule(dictionary, "Received-SPF", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.ReceivedSPF));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-Organization-AuthAs", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.XMsExchOrganizationAuthAs));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-Organization-AuthDomain", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.XMsExchOrganizationAuthDomain));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-Organization-AuthMechanism", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.XMsExchOrganizationAuthMechanism));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-Organization-AuthSource", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.XMsExchOrganizationAuthSource));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-Organization-Sharing-Instance-Guid", new InboundMimeHeadersParser.HeaderPropertyRule(MessageItemSchema.SharingInstanceGuid, delegate(InboundMimeHeadersParser parser, Header header, string value)
			{
				Guid guid;
				if (GuidHelper.TryParseGuid(value, out guid))
				{
					return guid;
				}
				StorageGlobals.ContextTraceDebug<string, string>(ExTraceGlobals.CcInboundMimeTracer, "InboundMimeHeadersParser: failed to parse header value ({0}: {1})", header.Name, value);
				return null;
			}));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-Organization-Approval-Allowed-Decision-Makers", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.ApprovalAllowedDecisionMakers));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-Organization-Approval-Requestor", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.ApprovalRequestor));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-Organization-Original-Sender", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.QuarantineOriginalSender, delegate(InboundMimeHeadersParser self, Header header, string value)
			{
				if (!self.CanPromoteQuarantineHeaders())
				{
					return null;
				}
				return value;
			}));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-Organization-Journaling-Remote-Accounts", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.JournalingRemoteAccounts, new InboundMimeHeadersParser.HeaderPropertyRule.HeaderValueTransformation(InboundMimeHeadersParser.ToJournalRemoteAccounts)));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-Send-Outlook-Recall-Report", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.SendOutlookRecallReport, delegate(InboundMimeHeadersParser self, Header header, string value)
			{
				bool flag = ConvertUtils.MimeStringEquals(value, "false") && ObjectClass.IsOfClass(self.Item.ClassName, "IPM.Outlook.Recall", false);
				bool flag2 = false;
				if (!flag)
				{
					return null;
				}
				return flag2;
			}));
			InboundMimeHeadersParser.AddRule(dictionary, "x-sharing-browse-url", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.XSharingBrowseUrl));
			InboundMimeHeadersParser.AddRule(dictionary, "x-sharing-capabilities", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.XSharingCapabilities));
			InboundMimeHeadersParser.AddRule(dictionary, "x-sharing-flavor", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.XSharingFlavor));
			InboundMimeHeadersParser.AddRule(dictionary, "x-sharing-instance-guid", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.XSharingInstanceGuid));
			InboundMimeHeadersParser.AddRule(dictionary, "x-sharing-local-type", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.XSharingLocalType));
			InboundMimeHeadersParser.AddRule(dictionary, "x-sharing-provider-guid", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.XSharingProviderGuid));
			InboundMimeHeadersParser.AddRule(dictionary, "x-sharing-provider-name", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.XSharingProviderName));
			InboundMimeHeadersParser.AddRule(dictionary, "x-sharing-provider-url", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.XSharingProviderUrl));
			InboundMimeHeadersParser.AddRule(dictionary, "x-sharing-remote-name", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.XSharingRemoteName));
			InboundMimeHeadersParser.AddRule(dictionary, "x-sharing-remote-path", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.XSharingRemotePath));
			InboundMimeHeadersParser.AddRule(dictionary, "x-sharing-remote-type", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.XSharingRemoteType));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-GroupMailbox-Id", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.XGroupMailboxSmtpAddressId));
			InboundMimeHeadersParser.AddDefaultRule(dictionary, new HeaderId[]
			{
				HeaderId.ContentBase,
				HeaderId.ContentLocation,
				HeaderId.XRef
			});
			InboundMimeHeadersParser.AddNonPromotableRule(dictionary, new HeaderId[]
			{
				HeaderId.Received,
				HeaderId.ResentSender,
				HeaderId.ResentDate,
				HeaderId.ResentMessageId,
				HeaderId.ContentType,
				HeaderId.ContentDisposition,
				HeaderId.ContentDescription,
				HeaderId.ContentTransferEncoding,
				HeaderId.ContentId,
				HeaderId.ContentMD5,
				HeaderId.MimeVersion,
				HeaderId.ReturnPath,
				HeaderId.Comments,
				HeaderId.AdHoc,
				HeaderId.ApparentlyTo,
				HeaderId.Approved,
				HeaderId.Control,
				HeaderId.Distribution,
				HeaderId.Encoding,
				HeaderId.FollowUpTo,
				HeaderId.Lines,
				HeaderId.Bytes,
				HeaderId.Article,
				HeaderId.Supercedes,
				HeaderId.NewsGroups,
				HeaderId.NntpPostingHost,
				HeaderId.Organization,
				HeaderId.Path,
				HeaderId.RR,
				HeaderId.Summary,
				HeaderId.Encrypted
			});
			InboundMimeHeadersParser.AddNonPromotableRule(dictionary, new string[]
			{
				"X-MimeOle",
				"X-MS-TNEF-Correlator",
				"X-MS-Journal-Report",
				"X-MS-Exchange-Inbox-Rules-Loop"
			});
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-ApplicationFlags", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.ExchangeApplicationFlags));
			InboundMimeHeadersParser.AddRule(dictionary, "X-MS-Exchange-Calendar-Originator-Id", new InboundMimeHeadersParser.HeaderPropertyRule(InternalSchema.CalendarOriginatorId));
			return dictionary;
		}

		// Token: 0x06003DE9 RID: 15849 RVA: 0x00101494 File Offset: 0x000FF694
		private static void AddRule(Dictionary<object, InboundMimeHeadersParser.IHeaderPromotionRule> rulesList, string headerName, InboundMimeHeadersParser.IHeaderPromotionRule rule)
		{
			Header header = Header.Create(headerName);
			object headerKey = InboundMimeHeadersParser.GetHeaderKey(header);
			rulesList.Add(headerKey, rule);
		}

		// Token: 0x06003DEA RID: 15850 RVA: 0x001014B8 File Offset: 0x000FF6B8
		private static void AddRule(Dictionary<object, InboundMimeHeadersParser.IHeaderPromotionRule> rulesList, HeaderId headerId, InboundMimeHeadersParser.IHeaderPromotionRule rule)
		{
			object headerKey = InboundMimeHeadersParser.GetHeaderKey(headerId);
			rulesList.Add(headerKey, rule);
		}

		// Token: 0x06003DEB RID: 15851 RVA: 0x001014D4 File Offset: 0x000FF6D4
		private static void AddDefaultRule(Dictionary<object, InboundMimeHeadersParser.IHeaderPromotionRule> rulesList, params HeaderId[] headerIds)
		{
			InboundMimeHeadersParser.DefaultHeaderRule rule = new InboundMimeHeadersParser.DefaultHeaderRule();
			foreach (HeaderId headerId in headerIds)
			{
				InboundMimeHeadersParser.AddRule(rulesList, headerId, rule);
			}
		}

		// Token: 0x06003DEC RID: 15852 RVA: 0x00101504 File Offset: 0x000FF704
		private static void AddDefaultRule(Dictionary<object, InboundMimeHeadersParser.IHeaderPromotionRule> rulesList, params string[] headerNames)
		{
			InboundMimeHeadersParser.DefaultHeaderRule rule = new InboundMimeHeadersParser.DefaultHeaderRule();
			foreach (string headerName in headerNames)
			{
				InboundMimeHeadersParser.AddRule(rulesList, headerName, rule);
			}
		}

		// Token: 0x06003DED RID: 15853 RVA: 0x00101534 File Offset: 0x000FF734
		private static void AddNonPromotableRule(Dictionary<object, InboundMimeHeadersParser.IHeaderPromotionRule> rulesList, params HeaderId[] headerIds)
		{
			foreach (HeaderId headerId in headerIds)
			{
				InboundMimeHeadersParser.AddRule(rulesList, headerId, null);
			}
		}

		// Token: 0x06003DEE RID: 15854 RVA: 0x00101560 File Offset: 0x000FF760
		private static void AddNonPromotableRule(Dictionary<object, InboundMimeHeadersParser.IHeaderPromotionRule> rulesList, params string[] headerNames)
		{
			foreach (string headerName in headerNames)
			{
				InboundMimeHeadersParser.AddRule(rulesList, headerName, null);
			}
		}

		// Token: 0x06003DEF RID: 15855 RVA: 0x0010158C File Offset: 0x000FF78C
		private InboundMimeHeadersParser.IHeaderPromotionRule FindRule(Header header)
		{
			object headerKey = InboundMimeHeadersParser.GetHeaderKey(header);
			InboundMimeHeadersParser.IHeaderPromotionRule result = null;
			if (this.headerRulesCopy.TryGetValue(headerKey, out result))
			{
				return result;
			}
			return this.DefaultRule;
		}

		// Token: 0x06003DF0 RID: 15856 RVA: 0x001015BA File Offset: 0x000FF7BA
		private static object GetHeaderKey(Header header)
		{
			if (header.HeaderId != HeaderId.Unknown)
			{
				return InboundMimeHeadersParser.GetHeaderKey(header.HeaderId);
			}
			return header.Name.ToLowerInvariant();
		}

		// Token: 0x06003DF1 RID: 15857 RVA: 0x001015DC File Offset: 0x000FF7DC
		private static object GetHeaderKey(string headerName)
		{
			Header header = Header.Create(headerName);
			return InboundMimeHeadersParser.GetHeaderKey(header);
		}

		// Token: 0x06003DF2 RID: 15858 RVA: 0x001015F6 File Offset: 0x000FF7F6
		private static object GetHeaderKey(HeaderId headerId)
		{
			return headerId;
		}

		// Token: 0x06003DF3 RID: 15859 RVA: 0x001015FE File Offset: 0x000FF7FE
		internal bool CheckIsHeaderPresent(HeaderId headerId)
		{
			return this.Headers.FindFirst(headerId) != null;
		}

		// Token: 0x06003DF4 RID: 15860 RVA: 0x00101612 File Offset: 0x000FF812
		internal bool CheckIsHeaderPresent(string headerName)
		{
			return this.Headers.FindFirst(headerName) != null;
		}

		// Token: 0x06003DF5 RID: 15861 RVA: 0x00101628 File Offset: 0x000FF828
		internal static object ToDateTime(string dateTimeString)
		{
			DateHeader dateHeader = new DateHeader("<empty>", DateTime.UtcNow);
			if (!dateHeader.IsValueValid(dateTimeString))
			{
				return null;
			}
			dateHeader.Value = dateTimeString;
			ExDateTime exDateTime = ExDateTime.MinValue;
			try
			{
				exDateTime = new ExDateTime(ExTimeZone.UtcTimeZone, dateHeader.UtcDateTime);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				StorageGlobals.ContextTraceDebug<DateTime, string>(ExTraceGlobals.CcInboundMimeTracer, "InboundMimeHeadersParser::ToDateTime: failed to parse header value ({0}: {1})", dateHeader.UtcDateTime, ex.Message);
			}
			if (!(exDateTime != ExDateTime.MinValue))
			{
				return null;
			}
			return exDateTime;
		}

		// Token: 0x06003DF6 RID: 15862 RVA: 0x001016B8 File Offset: 0x000FF8B8
		internal static object ToDateTime(InboundMimeHeadersParser self, Header header, string value)
		{
			if (value == null)
			{
				return null;
			}
			object obj = InboundMimeHeadersParser.ToDateTime(value);
			if (obj == null)
			{
				StorageGlobals.ContextTraceDebug<string, string>(ExTraceGlobals.CcInboundMimeTracer, "InboundMimeHeadersParser::ToDateTime: failed to parse header value ({0}: {1})", header.Name, value);
			}
			return obj;
		}

		// Token: 0x06003DF7 RID: 15863 RVA: 0x001016EC File Offset: 0x000FF8EC
		private static object ToInt32(InboundMimeHeadersParser self, Header header, string value)
		{
			if (value == null)
			{
				return null;
			}
			int num;
			if (int.TryParse(value, out num))
			{
				return num;
			}
			StorageGlobals.ContextTraceDebug<string, string>(ExTraceGlobals.CcInboundMimeTracer, "InboundMimeHeadersParser::ToInt32: failed to parse header value ({0}: {1})", header.Name, value);
			return null;
		}

		// Token: 0x06003DF8 RID: 15864 RVA: 0x00101726 File Offset: 0x000FF926
		private static object ToBoolean(InboundMimeHeadersParser self, Header header, string value)
		{
			if (value == null)
			{
				return null;
			}
			return ConvertUtils.MimeStringEquals(value, "true");
		}

		// Token: 0x06003DF9 RID: 15865 RVA: 0x00101740 File Offset: 0x000FF940
		private static object ToJournalRemoteAccounts(InboundMimeHeadersParser self, Header header, string value)
		{
			string[] array = value.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length == 0)
			{
				return null;
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i].Trim();
			}
			return array;
		}

		// Token: 0x06003DFA RID: 15866 RVA: 0x00101784 File Offset: 0x000FF984
		private static object KeywordsToCategories(InboundMimeHeadersParser self, Header header, string value)
		{
			object result = null;
			if (!self.ConversionOptions.ClearCategories && value != null)
			{
				string[] array = value.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = array[i].Trim();
				}
				result = array;
			}
			return result;
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x001017D4 File Offset: 0x000FF9D4
		private static object ToSensitivity(InboundMimeHeadersParser self, Header header, string value)
		{
			if (ConvertUtils.MimeStringEquals(value, "normal"))
			{
				return Sensitivity.Normal;
			}
			if (ConvertUtils.MimeStringEquals(value, "personal"))
			{
				return Sensitivity.Personal;
			}
			if (ConvertUtils.MimeStringEquals(value, "private"))
			{
				return Sensitivity.Private;
			}
			if (ConvertUtils.MimeStringEquals(value, "company-confidential"))
			{
				return Sensitivity.CompanyConfidential;
			}
			StorageGlobals.ContextTraceDebug<string, string>(ExTraceGlobals.CcInboundMimeTracer, "InboundMimeHeadersParser::ToSensitivity: failed to parse header value ({0}: {1})", header.Name, value);
			return null;
		}

		// Token: 0x06003DFC RID: 15868 RVA: 0x00101848 File Offset: 0x000FFA48
		private static object ToImportance(InboundMimeHeadersParser self, Header header, string value)
		{
			if (ConvertUtils.MimeStringEquals(value, "high"))
			{
				return Importance.High;
			}
			if (ConvertUtils.MimeStringEquals(value, "low"))
			{
				return Importance.Low;
			}
			if (ConvertUtils.MimeStringEquals(value, "normal"))
			{
				return Importance.Normal;
			}
			StorageGlobals.ContextTraceDebug<string, string>(ExTraceGlobals.CcInboundMimeTracer, "InboundMimeHeadersParser::ToImportance: failed to parse header value ({0}: {1})", header.Name, value);
			return null;
		}

		// Token: 0x06003DFD RID: 15869 RVA: 0x001018A8 File Offset: 0x000FFAA8
		private static object PriorityToImportance(InboundMimeHeadersParser self, Header header, string priorityValue)
		{
			if (ConvertUtils.MimeStringEquals(priorityValue, "normal"))
			{
				return Importance.Normal;
			}
			if (ConvertUtils.MimeStringEquals(priorityValue, "urgent"))
			{
				return Importance.High;
			}
			if (ConvertUtils.MimeStringEquals(priorityValue, "non-urgent"))
			{
				return Importance.Low;
			}
			StorageGlobals.ContextTraceDebug<string, string>(ExTraceGlobals.CcInboundMimeTracer, "InboundMimeHeadersParser::PriorityToImportance: failed to parse header value ({0}: {1})", header.Name, priorityValue);
			return null;
		}

		// Token: 0x06003DFE RID: 15870 RVA: 0x00101908 File Offset: 0x000FFB08
		private static object XPriorityToImportance(InboundMimeHeadersParser self, Header header, string value)
		{
			if (value.Length == 1 || (value.Length > 1 && !char.IsDigit(value[1])))
			{
				switch (value[0])
				{
				case '1':
				case '2':
					return Importance.High;
				case '3':
					return Importance.Normal;
				case '4':
				case '5':
					return Importance.Low;
				}
			}
			StorageGlobals.ContextTraceDebug<string, string>(ExTraceGlobals.CcInboundMimeTracer, "InboundMimeHeadersParser::XPriorityToImportance: failed to parse header value ({0}: {1})", header.Name, value);
			return null;
		}

		// Token: 0x06003DFF RID: 15871 RVA: 0x00101988 File Offset: 0x000FFB88
		private bool CanPromoteQuarantineHeaders()
		{
			return !this.IsTopLevelMessage || this.IsStreamToStreamConversion;
		}

		// Token: 0x06003E00 RID: 15872 RVA: 0x0010199C File Offset: 0x000FFB9C
		private static void PromoteAVStampMailbox(InboundMimeHeadersParser self, Header header, string value)
		{
			Item item = self.Item;
			if (self.isTransportAVStampPresent)
			{
				string[] array = item.TryGetProperty(InternalSchema.XMsExchOrganizationAVStampMailbox) as string[];
				if (array != null)
				{
					string[] array2 = new string[array.Length + 1];
					array.CopyTo(array2, 0);
					array2[array.Length] = value;
					item[InternalSchema.XMsExchOrganizationAVStampMailbox] = array2;
					return;
				}
			}
			else
			{
				self.isTransportAVStampPresent = true;
				item[InternalSchema.XMsExchOrganizationAVStampMailbox] = new string[]
				{
					value
				};
			}
		}

		// Token: 0x06003E01 RID: 15873 RVA: 0x00101A10 File Offset: 0x000FFC10
		private static void PromoteRecipientP2Type(InboundMimeHeadersParser self, Header header, string value)
		{
			if (ConvertUtils.MimeStringEquals(value, "Bcc"))
			{
				self.Item[InternalSchema.MessageBccMe] = true;
			}
		}

		// Token: 0x06003E02 RID: 15874 RVA: 0x00101A35 File Offset: 0x000FFC35
		private static void PromoteContentLanguage(InboundMimeHeadersParser self, Header header, string value)
		{
			self.SetMessageLocaleId(value);
		}

		// Token: 0x06003E03 RID: 15875 RVA: 0x00101A3E File Offset: 0x000FFC3E
		private static void PromoteSubject(InboundMimeHeadersParser self, Header subjectHeader, string subjectValue)
		{
			self.isSubjectPromotedFromThreadTopic = false;
			SubjectProperty.ModifySubjectProperty(self.Item, InternalSchema.MapiSubject, subjectValue);
			SubjectProperty.TruncateSubject(self.Item, self.ConversionLimits.MaxMimeSubjectLength);
		}

		// Token: 0x06003E04 RID: 15876 RVA: 0x00101A70 File Offset: 0x000FFC70
		private static void PromoteThreadTopic(InboundMimeHeadersParser self, Header topicHeader, string topicValue)
		{
			string text = self.Item.TryGetProperty(InternalSchema.SubjectPrefixInternal) as string;
			string text2 = self.Item.TryGetProperty(InternalSchema.MapiSubject) as string;
			if (string.IsNullOrEmpty(text) && text2 != null)
			{
				text = SubjectProperty.ExtractPrefixUsingNormalizedSubject(text2, topicValue);
				if (text != null)
				{
					self.Item[InternalSchema.SubjectPrefix] = text;
					SubjectProperty.TruncateSubject(self.Item, self.ConversionLimits.MaxMimeSubjectLength);
					return;
				}
			}
			else if (text2 == null)
			{
				self.isSubjectPromotedFromThreadTopic = true;
				self.Item[InternalSchema.NormalizedSubject] = topicValue;
				SubjectProperty.TruncateSubject(self.Item, self.ConversionLimits.MaxMimeSubjectLength);
			}
		}

		// Token: 0x06003E05 RID: 15877 RVA: 0x00101B18 File Offset: 0x000FFD18
		private void PromoteReceivedTime()
		{
			ReceivedHeader receivedHeader = this.Headers.FindFirst(HeaderId.Received) as ReceivedHeader;
			if (receivedHeader != null && receivedHeader.Date != null)
			{
				object obj = InboundMimeHeadersParser.ToDateTime(receivedHeader.Date);
				if (obj != null)
				{
					this.Item[InternalSchema.ReceivedTime] = obj;
				}
			}
		}

		// Token: 0x06003E06 RID: 15878 RVA: 0x00101B64 File Offset: 0x000FFD64
		private void PromoteXLoop()
		{
			List<string> list = null;
			for (TextHeader textHeader = this.Headers.FindFirst("X-MS-Exchange-Inbox-Rules-Loop") as TextHeader; textHeader != null; textHeader = (this.Headers.FindNext(textHeader) as TextHeader))
			{
				if (list == null)
				{
					list = new List<string>(4);
				}
				string text = textHeader.Value;
				if (text.Length > 1000)
				{
					text = text.Substring(0, 1000);
				}
				list.Add(text);
				if (list.Count == 3)
				{
					break;
				}
			}
			if (list != null)
			{
				string[] value = list.ToArray();
				this.Item[InternalSchema.XLoop] = value;
			}
		}

		// Token: 0x06003E07 RID: 15879 RVA: 0x00101BF8 File Offset: 0x000FFDF8
		internal void SetMessageLocaleId(string cultureList)
		{
			if (cultureList != null)
			{
				string[] array = cultureList.Split(new char[]
				{
					','
				});
				foreach (string text in array)
				{
					string text2 = text.Trim();
					Culture culture;
					if (!string.IsNullOrEmpty(text2) && Culture.TryGetCulture(text2, out culture))
					{
						this.Item[InternalSchema.MessageLocaleId] = culture.LCID;
						return;
					}
				}
			}
		}

		// Token: 0x06003E08 RID: 15880 RVA: 0x00101C70 File Offset: 0x000FFE70
		private static void PromoteDispositionNotificationTo(InboundMimeHeadersParser self, Header header, Participant participant)
		{
			Item item = self.Item;
			InboundAddressCache addressCache = self.AddressCache;
			item[InternalSchema.IsReadReceiptRequested] = true;
			if (item.GetValueOrDefault<bool>(InternalSchema.IsReadReceiptRequested))
			{
				addressCache.Participants[ConversionItemParticipants.ParticipantIndex.ReadReceipt] = participant;
				item[InternalSchema.IsReadReceiptPendingInternal] = true;
				item[InternalSchema.IsNotReadReceiptPendingInternal] = true;
			}
		}

		// Token: 0x06003E09 RID: 15881 RVA: 0x00101CD8 File Offset: 0x000FFED8
		private static void PromoteReturnReceiptTo(InboundMimeHeadersParser self, Header header, Participant participant)
		{
			self.Item[InternalSchema.IsDeliveryReceiptRequested] = true;
		}

		// Token: 0x06003E0A RID: 15882 RVA: 0x00101CF0 File Offset: 0x000FFEF0
		private static void PromotePrecedence(InboundMimeHeadersParser self, Header header, string headerValue)
		{
			self.Item[InternalSchema.AutoResponseSuppress] = AutoResponseSuppress.All;
		}

		// Token: 0x06003E0B RID: 15883 RVA: 0x00101D08 File Offset: 0x000FFF08
		private static void PromoteContentClass(InboundMimeHeadersParser self, Header header, string value)
		{
			Item item = self.Item;
			self.DefaultRule.PromoteHeader(self, header);
			if (ObjectClass.IsOfClass(item.ClassName, "IPM.InfoPathForm"))
			{
				string text = value.Substring("InfoPathForm.".Length);
				int num = text.IndexOf('.');
				string value2 = text.Substring(num + 1);
				item[InternalSchema.InfoPathFormName] = value2;
			}
		}

		// Token: 0x06003E0C RID: 15884 RVA: 0x00101D6C File Offset: 0x000FFF6C
		private static void PromoteXMessageFlag(InboundMimeHeadersParser self, Header headerName, string value)
		{
			Item item = self.Item;
			item[InternalSchema.FlagRequest] = value;
			item[InternalSchema.MapiFlagStatus] = FlagStatus.Flagged;
			item[InternalSchema.FlagSubject] = item.GetValueOrDefault<string>(InternalSchema.Subject, string.Empty);
			item[InternalSchema.TaskStatus] = TaskStatus.NotStarted;
			item.Delete(InternalSchema.StartDate);
			item.Delete(InternalSchema.DueDate);
			item[InternalSchema.IsComplete] = false;
			item[InternalSchema.PercentComplete] = 0.0;
			item.Delete(InternalSchema.FlagCompleteTime);
			item.Delete(InternalSchema.CompleteDate);
			item[InternalSchema.IsFlagSetForRecipient] = true;
		}

		// Token: 0x06003E0D RID: 15885 RVA: 0x00101E30 File Offset: 0x00100030
		private static void PromoteXAutoResponseSuppress(InboundMimeHeadersParser self, Header headerName, string value)
		{
			Item item = self.Item;
			AutoResponseSuppress autoResponseSuppress;
			if (EnumValidator<AutoResponseSuppress>.TryParse(value, EnumParseOptions.IgnoreUnknownValues | EnumParseOptions.IgnoreCase, out autoResponseSuppress))
			{
				AutoResponseSuppress valueOrDefault = item.GetValueOrDefault<AutoResponseSuppress>(InternalSchema.AutoResponseSuppress);
				item[InternalSchema.AutoResponseSuppress] = (valueOrDefault | autoResponseSuppress);
				return;
			}
			StorageGlobals.ContextTraceDebug<string>(ExTraceGlobals.CcInboundMimeTracer, "InboundMimeHeadersParser::PromoteXAutoResponseSuppress: unable to parse the value for X-Auto-Response-Suppress: {0}", value);
		}

		// Token: 0x06003E0E RID: 15886 RVA: 0x00101E80 File Offset: 0x00100080
		private void PromoteTransportMessageHeaders()
		{
			if (this.isTransportMessageHeadersPromoted)
			{
				return;
			}
			this.isTransportMessageHeadersPromoted = true;
			InboundMimeHeadersParser.TransportMessageHeadersOutputFilter filter = new InboundMimeHeadersParser.TransportMessageHeadersOutputFilter(this.ConversionOptions);
			using (Stream stream = this.Item.OpenPropertyStream(InternalSchema.TransportMessageHeaders, PropertyOpenMode.Create))
			{
				using (Stream stream2 = new ConverterStream(stream, new TextToText(TextToTextConversionMode.ConvertCodePageOnly)
				{
					InputEncoding = CTSGlobals.AsciiEncoding,
					OutputEncoding = Encoding.Unicode
				}, ConverterStreamAccess.Write))
				{
					this.MessageRoot.WriteTo(stream2, null, filter);
				}
			}
		}

		// Token: 0x06003E0F RID: 15887 RVA: 0x00101F24 File Offset: 0x00100124
		private Participant ParseAddress(AddressHeader addressHeader, InboundMimeHeadersParser.AddressHeaderFlags flags)
		{
			MimeRecipient mimeRecipient = addressHeader.FirstChild as MimeRecipient;
			if (mimeRecipient == null)
			{
				return null;
			}
			return this.ParseAddress(mimeRecipient, flags);
		}

		// Token: 0x06003E10 RID: 15888 RVA: 0x00101F4C File Offset: 0x0010014C
		private List<Participant> ParseAddressList(AddressHeader addressHeader)
		{
			List<Participant> list = new List<Participant>();
			this.ParseAddressList(list, addressHeader);
			return list;
		}

		// Token: 0x06003E11 RID: 15889 RVA: 0x00101F68 File Offset: 0x00100168
		private void ParseAddressList(List<Participant> list, IEnumerable collection)
		{
			foreach (object obj in collection)
			{
				MimeRecipient mimeRecipient = obj as MimeRecipient;
				if (mimeRecipient != null)
				{
					Participant participant = this.ParseAddress(mimeRecipient, InboundMimeHeadersParser.AddressHeaderFlags.AlwaysDeencapsulate);
					if (participant != null)
					{
						list.Add(participant);
					}
				}
				else
				{
					MimeGroup mimeGroup = obj as MimeGroup;
					if (mimeGroup != null && InboundMimeHeadersParser.CanPromoteAddressGroup(mimeGroup))
					{
						this.ParseAddressList(list, mimeGroup);
					}
				}
			}
		}

		// Token: 0x06003E12 RID: 15890 RVA: 0x00101FF8 File Offset: 0x001001F8
		private Participant ParseAddress(MimeRecipient mimeRecipient, InboundMimeHeadersParser.AddressHeaderFlags flags)
		{
			string displayName = null;
			if (!mimeRecipient.TryGetDisplayName(out displayName))
			{
				StorageGlobals.ContextTraceDebug<string>(ExTraceGlobals.CcInboundMimeTracer, "InboundMimeHeadersParser::ParseAddress: failed to parse recipient's display name, {0}.", mimeRecipient.Email);
			}
			bool canDeencapsulate = this.ConversionOptions.IsSenderTrusted || (flags & InboundMimeHeadersParser.AddressHeaderFlags.AlwaysDeencapsulate) == InboundMimeHeadersParser.AddressHeaderFlags.AlwaysDeencapsulate;
			return InboundMimeHeadersParser.CreateParticipantFromMime(displayName, mimeRecipient.Email, this.ConversionOptions, canDeencapsulate);
		}

		// Token: 0x06003E13 RID: 15891 RVA: 0x00102050 File Offset: 0x00100250
		internal static Participant CreateParticipantFromMime(string displayName, string email, InboundConversionOptions options, bool canDeencapsulate)
		{
			string routingType = "SMTP";
			if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(displayName))
			{
				StorageGlobals.ContextTraceError(ExTraceGlobals.CcInboundMimeTracer, ServerStrings.ConversionEmptyAddress);
				return null;
			}
			if (email != null && canDeencapsulate && !options.IgnoreImceaDomain && !string.IsNullOrEmpty(options.ImceaEncapsulationDomain) && ImceaAddress.IsImceaAddress(email))
			{
				ImceaAddress.Decode(ref routingType, ref email, options.ImceaEncapsulationDomain);
			}
			if (string.IsNullOrEmpty(email))
			{
				routingType = null;
			}
			if (displayName != null && displayName.Length > 512)
			{
				int num = 512;
				if (char.IsHighSurrogate(displayName, num - 1))
				{
					num--;
				}
				displayName = displayName.Substring(0, num);
			}
			Participant participant = new Participant(displayName, email, routingType);
			if (string.IsNullOrWhiteSpace(participant.EmailAddress) && string.IsNullOrWhiteSpace(participant.DisplayName))
			{
				return null;
			}
			return participant;
		}

		// Token: 0x06003E14 RID: 15892 RVA: 0x0010211B File Offset: 0x0010031B
		private static bool CanPromoteAddressGroup(MimeGroup addressGroup)
		{
			return addressGroup.DisplayName != null && !ConvertUtils.MimeStringEquals(addressGroup.DisplayName, "undisclosed recipients");
		}

		// Token: 0x06003E15 RID: 15893 RVA: 0x0010213C File Offset: 0x0010033C
		internal void PromoteMessageHeaders()
		{
			this.PromoteTransportMessageHeaders();
			this.PromoteReceivedTime();
			this.PromoteXLoop();
			foreach (Header header in this.Headers)
			{
				if (!this.ConversionOptions.ApplyHeaderFirewall || !MimeConstants.IsInReservedHeaderNamespace(header.Name))
				{
					InboundMimeHeadersParser.IHeaderPromotionRule headerPromotionRule = this.FindRule(header);
					if (headerPromotionRule != null)
					{
						headerPromotionRule.PromoteHeader(this, header);
					}
				}
			}
			if (this.isSubjectPromotedFromThreadTopic)
			{
				this.Item.Delete(InternalSchema.Subject);
			}
		}

		// Token: 0x06003E16 RID: 15894 RVA: 0x001021E0 File Offset: 0x001003E0
		internal bool IsSentByLegacyExchange()
		{
			string headerValue = MimeHelpers.GetHeaderValue(this.Headers, "X-MimeOle", this.ConversionOptions);
			return headerValue != null && headerValue.IndexOf("Microsoft Exchange", StringComparison.OrdinalIgnoreCase) >= 0;
		}

		// Token: 0x06003E17 RID: 15895 RVA: 0x0010221B File Offset: 0x0010041B
		public InboundMimeHeadersParser(InboundMimeConverter owner)
		{
			this.owner = owner;
			this.headers = owner.MessageRoot.Headers;
			this.isSubjectPromotedFromThreadTopic = false;
		}

		// Token: 0x170012BF RID: 4799
		// (get) Token: 0x06003E18 RID: 15896 RVA: 0x0010224D File Offset: 0x0010044D
		private InboundConversionOptions ConversionOptions
		{
			get
			{
				return this.owner.ConversionOptions;
			}
		}

		// Token: 0x170012C0 RID: 4800
		// (get) Token: 0x06003E19 RID: 15897 RVA: 0x0010225A File Offset: 0x0010045A
		private ConversionLimits ConversionLimits
		{
			get
			{
				return this.ConversionOptions.Limits;
			}
		}

		// Token: 0x170012C1 RID: 4801
		// (get) Token: 0x06003E1A RID: 15898 RVA: 0x00102267 File Offset: 0x00100467
		private InboundAddressCache AddressCache
		{
			get
			{
				return this.owner.AddressCache;
			}
		}

		// Token: 0x170012C2 RID: 4802
		// (get) Token: 0x06003E1B RID: 15899 RVA: 0x00102274 File Offset: 0x00100474
		private Item Item
		{
			get
			{
				return this.owner.Item;
			}
		}

		// Token: 0x170012C3 RID: 4803
		// (get) Token: 0x06003E1C RID: 15900 RVA: 0x00102281 File Offset: 0x00100481
		private HeaderList Headers
		{
			get
			{
				return this.headers;
			}
		}

		// Token: 0x170012C4 RID: 4804
		// (get) Token: 0x06003E1D RID: 15901 RVA: 0x00102289 File Offset: 0x00100489
		private MimePart MessageRoot
		{
			get
			{
				return this.owner.EmailMessage.RootPart;
			}
		}

		// Token: 0x170012C5 RID: 4805
		// (get) Token: 0x06003E1E RID: 15902 RVA: 0x0010229B File Offset: 0x0010049B
		private InboundMimeHeadersParser.DefaultHeaderRule DefaultRule
		{
			get
			{
				if (this.defaultRule == null)
				{
					this.defaultRule = new InboundMimeHeadersParser.DefaultHeaderRule();
				}
				return this.defaultRule;
			}
		}

		// Token: 0x170012C6 RID: 4806
		// (get) Token: 0x06003E1F RID: 15903 RVA: 0x001022B6 File Offset: 0x001004B6
		private bool IsStreamToStreamConversion
		{
			get
			{
				return (this.owner.ConverterFlags & ConverterFlags.IsStreamToStreamConversion) == ConverterFlags.IsStreamToStreamConversion;
			}
		}

		// Token: 0x170012C7 RID: 4807
		// (get) Token: 0x06003E20 RID: 15904 RVA: 0x001022C8 File Offset: 0x001004C8
		private bool IsTopLevelMessage
		{
			get
			{
				return (this.owner.ConverterFlags & ConverterFlags.IsEmbeddedMessage) != ConverterFlags.IsEmbeddedMessage;
			}
		}

		// Token: 0x04002141 RID: 8513
		private static readonly Dictionary<object, InboundMimeHeadersParser.IHeaderPromotionRule> staticHeaderRules = InboundMimeHeadersParser.CreateHeaderRulesTable();

		// Token: 0x04002142 RID: 8514
		private readonly Dictionary<object, InboundMimeHeadersParser.IHeaderPromotionRule> headerRulesCopy = InboundMimeHeadersParser.staticHeaderRules;

		// Token: 0x04002143 RID: 8515
		private InboundMimeConverter owner;

		// Token: 0x04002144 RID: 8516
		private HeaderList headers;

		// Token: 0x04002145 RID: 8517
		private InboundMimeHeadersParser.DefaultHeaderRule defaultRule;

		// Token: 0x04002146 RID: 8518
		private bool isTransportAVStampPresent;

		// Token: 0x04002147 RID: 8519
		private bool isTransportMessageHeadersPromoted;

		// Token: 0x04002148 RID: 8520
		private bool isSubjectPromotedFromThreadTopic;

		// Token: 0x020005E4 RID: 1508
		private class TransportMessageHeadersOutputFilter : MimeOutputFilter
		{
			// Token: 0x06003E33 RID: 15923 RVA: 0x001022E9 File Offset: 0x001004E9
			public TransportMessageHeadersOutputFilter(InboundConversionOptions options)
			{
				this.options = options;
			}

			// Token: 0x06003E34 RID: 15924 RVA: 0x001022F8 File Offset: 0x001004F8
			public override bool FilterPartBody(MimePart part, Stream stream)
			{
				return true;
			}

			// Token: 0x06003E35 RID: 15925 RVA: 0x001022FC File Offset: 0x001004FC
			public override bool FilterHeader(Header header, Stream stream)
			{
				string name = header.Name;
				return MimeConstants.IsInReservedHeaderNamespace(name) && (this.options.ApplyHeaderFirewall || !MimeConstants.IsReservedHeaderAllowedOnDelivery(name));
			}

			// Token: 0x0400215A RID: 8538
			private InboundConversionOptions options;
		}

		// Token: 0x020005E5 RID: 1509
		private class HeaderPriorityList
		{
			// Token: 0x06003E36 RID: 15926 RVA: 0x00102334 File Offset: 0x00100534
			public HeaderPriorityList(params HeaderId[] dependencyIds)
			{
				this.dependentHeaderIds = dependencyIds;
			}

			// Token: 0x06003E37 RID: 15927 RVA: 0x00102344 File Offset: 0x00100544
			public HeaderPriorityList(params string[] dependencyNames)
			{
				List<string> list = new List<string>();
				List<HeaderId> list2 = new List<HeaderId>();
				foreach (string name in dependencyNames)
				{
					Header header = Header.Create(name);
					if (header.HeaderId != HeaderId.Unknown)
					{
						list2.Add(header.HeaderId);
					}
					else
					{
						list.Add(header.Name);
					}
				}
				if (list.Count != 0)
				{
					this.dependentHeaderNames = list.ToArray();
				}
				if (list2.Count != 0)
				{
					this.dependentHeaderIds = list2.ToArray();
				}
			}

			// Token: 0x06003E38 RID: 15928 RVA: 0x001023D0 File Offset: 0x001005D0
			public static bool CanPromoteHeader(InboundMimeHeadersParser parser, Header header, InboundMimeHeadersParser.HeaderPriorityList priorityList)
			{
				if (priorityList == null)
				{
					return true;
				}
				if (priorityList.dependentHeaderIds != null)
				{
					for (int num = 0; num != priorityList.dependentHeaderIds.Length; num++)
					{
						if (parser.CheckIsHeaderPresent(priorityList.dependentHeaderIds[num]))
						{
							return false;
						}
					}
				}
				if (priorityList.dependentHeaderNames != null)
				{
					for (int num2 = 0; num2 != priorityList.dependentHeaderNames.Length; num2++)
					{
						if (parser.CheckIsHeaderPresent(priorityList.dependentHeaderNames[num2]))
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x0400215B RID: 8539
			private string[] dependentHeaderNames;

			// Token: 0x0400215C RID: 8540
			private HeaderId[] dependentHeaderIds;
		}

		// Token: 0x020005E6 RID: 1510
		private interface IHeaderPromotionRule
		{
			// Token: 0x06003E39 RID: 15929
			void PromoteHeader(InboundMimeHeadersParser parser, Header header);
		}

		// Token: 0x020005E7 RID: 1511
		private class HeaderPropertyRule : InboundMimeHeadersParser.IHeaderPromotionRule
		{
			// Token: 0x06003E3A RID: 15930 RVA: 0x0010243D File Offset: 0x0010063D
			public HeaderPropertyRule(StorePropertyDefinition property)
			{
				this.property = property;
				this.lengthLimit = int.MaxValue;
				this.transformation = null;
				this.priorityList = null;
			}

			// Token: 0x06003E3B RID: 15931 RVA: 0x00102465 File Offset: 0x00100665
			public HeaderPropertyRule(StorePropertyDefinition property, InboundMimeHeadersParser.HeaderPriorityList priorityList)
			{
				this.property = property;
				this.lengthLimit = int.MaxValue;
				this.transformation = null;
				this.priorityList = priorityList;
			}

			// Token: 0x06003E3C RID: 15932 RVA: 0x0010248D File Offset: 0x0010068D
			public HeaderPropertyRule(StorePropertyDefinition property, int lengthLimit)
			{
				this.property = property;
				this.lengthLimit = lengthLimit;
				this.transformation = null;
				this.priorityList = null;
			}

			// Token: 0x06003E3D RID: 15933 RVA: 0x001024B1 File Offset: 0x001006B1
			public HeaderPropertyRule(StorePropertyDefinition property, InboundMimeHeadersParser.HeaderPropertyRule.HeaderValueTransformation transformation)
			{
				this.property = property;
				this.lengthLimit = int.MaxValue;
				this.transformation = transformation;
				this.priorityList = null;
			}

			// Token: 0x06003E3E RID: 15934 RVA: 0x001024D9 File Offset: 0x001006D9
			public HeaderPropertyRule(StorePropertyDefinition property, InboundMimeHeadersParser.HeaderPropertyRule.HeaderValueTransformation transformation, InboundMimeHeadersParser.HeaderPriorityList priorityList)
			{
				this.property = property;
				this.lengthLimit = int.MaxValue;
				this.transformation = transformation;
				this.priorityList = priorityList;
			}

			// Token: 0x06003E3F RID: 15935 RVA: 0x00102504 File Offset: 0x00100704
			public void PromoteHeader(InboundMimeHeadersParser parser, Header header)
			{
				if (!InboundMimeHeadersParser.HeaderPriorityList.CanPromoteHeader(parser, header, this.priorityList))
				{
					return;
				}
				int num = Math.Min(this.lengthLimit, parser.ConversionLimits.MaxMimeTextHeaderLength);
				string headerValue = MimeHelpers.GetHeaderValue(header, num);
				if (headerValue == null)
				{
					return;
				}
				object obj;
				if (this.transformation != null)
				{
					obj = this.transformation(parser, header, headerValue);
				}
				else
				{
					obj = headerValue;
				}
				if (obj != null)
				{
					parser.Item[this.property] = obj;
				}
			}

			// Token: 0x0400215D RID: 8541
			private StorePropertyDefinition property;

			// Token: 0x0400215E RID: 8542
			private int lengthLimit;

			// Token: 0x0400215F RID: 8543
			private InboundMimeHeadersParser.HeaderPropertyRule.HeaderValueTransformation transformation;

			// Token: 0x04002160 RID: 8544
			private InboundMimeHeadersParser.HeaderPriorityList priorityList;

			// Token: 0x020005E8 RID: 1512
			// (Invoke) Token: 0x06003E41 RID: 15937
			public delegate object HeaderValueTransformation(InboundMimeHeadersParser parser, Header header, string value);
		}

		// Token: 0x020005E9 RID: 1513
		private class DefaultHeaderRule : InboundMimeHeadersParser.IHeaderPromotionRule
		{
			// Token: 0x06003E44 RID: 15940 RVA: 0x00102576 File Offset: 0x00100776
			public DefaultHeaderRule()
			{
				this.lengthLimit = int.MaxValue;
			}

			// Token: 0x06003E45 RID: 15941 RVA: 0x00102589 File Offset: 0x00100789
			public DefaultHeaderRule(int lengthLimit)
			{
				this.lengthLimit = lengthLimit;
			}

			// Token: 0x06003E46 RID: 15942 RVA: 0x00102598 File Offset: 0x00100798
			public void PromoteHeader(InboundMimeHeadersParser parser, Header header)
			{
				int num = Math.Min(this.lengthLimit, parser.ConversionLimits.MaxMimeTextHeaderLength);
				string headerValue = MimeHelpers.GetHeaderValue(header, num);
				if (!parser.IsTopLevelMessage || parser.IsStreamToStreamConversion || !MimeConstants.IsInReservedHeaderNamespace(header.Name))
				{
					if (headerValue == null)
					{
						AddressHeader addressHeader = header as AddressHeader;
						if (addressHeader != null)
						{
							List<Participant> list = parser.ParseAddressList(addressHeader);
							StringBuilder stringBuilder = new StringBuilder(list.Count * 32);
							int num2 = 0;
							foreach (Participant participant in list)
							{
								if (participant.EmailAddress != null)
								{
									string displayName = participant.DisplayName;
									string text = null;
									if (participant.RoutingType == "SMTP")
									{
										text = participant.EmailAddress;
									}
									else if (!parser.ConversionOptions.IgnoreImceaDomain)
									{
										text = ImceaAddress.Encode(participant.RoutingType, participant.EmailAddress, parser.ConversionOptions.ImceaEncapsulationDomain);
									}
									if (text != null)
									{
										if (displayName == null || displayName == text)
										{
											stringBuilder.AppendFormat("{0}{1}", (num2 == 0) ? string.Empty : ", ", text);
										}
										else
										{
											stringBuilder.AppendFormat("{0}\"{1}\" <{2}>", (num2 == 0) ? string.Empty : ", ", displayName, text);
										}
										num2++;
									}
								}
							}
							InboundMimeHeadersParser.DefaultHeaderRule.SetInternetHeader(parser.Item, header.Name, stringBuilder.ToString());
							return;
						}
					}
					else
					{
						InboundMimeHeadersParser.DefaultHeaderRule.SetInternetHeader(parser.Item, header.Name, headerValue);
					}
				}
			}

			// Token: 0x06003E47 RID: 15943 RVA: 0x00102740 File Offset: 0x00100940
			private static void SetInternetHeader(Item item, string headerName, string headerValue)
			{
				string propertyName = headerName.ToLowerInvariant();
				if (GuidNamePropertyDefinition.IsValidName(WellKnownPropertySet.InternetHeaders, propertyName))
				{
					StorePropertyDefinition propertyDefinition = GuidNamePropertyDefinition.CreateCustom(string.Empty, typeof(string), WellKnownPropertySet.InternetHeaders, propertyName, PropertyFlags.None);
					item[propertyDefinition] = headerValue;
					return;
				}
				StorageGlobals.ContextTraceDebug<string, string>(ExTraceGlobals.CcInboundMimeTracer, "InboundMimeHeadersParser: not promoting X-header to a NamedProperty with an invalid name:\r\n({0}: {1})", headerName, headerValue);
			}

			// Token: 0x04002161 RID: 8545
			private int lengthLimit;
		}

		// Token: 0x020005EA RID: 1514
		public enum AddressHeaderFlags
		{
			// Token: 0x04002163 RID: 8547
			DeencapsulateIfSenderTrusted = 1,
			// Token: 0x04002164 RID: 8548
			AlwaysDeencapsulate
		}

		// Token: 0x020005EB RID: 1515
		private class AddressHeaderRule : InboundMimeHeadersParser.IHeaderPromotionRule
		{
			// Token: 0x06003E48 RID: 15944 RVA: 0x00102797 File Offset: 0x00100997
			public AddressHeaderRule(InboundMimeHeadersParser.AddressHeaderRule.PromoteAddressList listPromotionDelegate)
			{
				this.listPromotionDelegate = listPromotionDelegate;
			}

			// Token: 0x06003E49 RID: 15945 RVA: 0x001027A6 File Offset: 0x001009A6
			public AddressHeaderRule(InboundMimeHeadersParser.AddressHeaderRule.PromoteSingleAddress addressPromotionDelegate, InboundMimeHeadersParser.AddressHeaderFlags flags)
			{
				this.addressPromotionDelegate = addressPromotionDelegate;
				this.flags = flags;
			}

			// Token: 0x06003E4A RID: 15946 RVA: 0x001027BC File Offset: 0x001009BC
			public void PromoteHeader(InboundMimeHeadersParser parser, Header header)
			{
				AddressHeader addressHeader = (AddressHeader)header;
				if (this.IsSingleAddress)
				{
					Participant participant = parser.ParseAddress(addressHeader, this.flags);
					this.addressPromotionDelegate(parser, header, participant);
					return;
				}
				List<Participant> participantList = parser.ParseAddressList(addressHeader);
				this.listPromotionDelegate(parser, header, participantList);
			}

			// Token: 0x170012C8 RID: 4808
			// (get) Token: 0x06003E4B RID: 15947 RVA: 0x0010280B File Offset: 0x00100A0B
			private bool IsSingleAddress
			{
				get
				{
					return this.addressPromotionDelegate != null;
				}
			}

			// Token: 0x04002165 RID: 8549
			private InboundMimeHeadersParser.AddressHeaderRule.PromoteAddressList listPromotionDelegate;

			// Token: 0x04002166 RID: 8550
			private InboundMimeHeadersParser.AddressHeaderRule.PromoteSingleAddress addressPromotionDelegate;

			// Token: 0x04002167 RID: 8551
			private InboundMimeHeadersParser.AddressHeaderFlags flags;

			// Token: 0x020005EC RID: 1516
			// (Invoke) Token: 0x06003E4D RID: 15949
			public delegate void PromoteAddressList(InboundMimeHeadersParser parser, Header header, List<Participant> participantList);

			// Token: 0x020005ED RID: 1517
			// (Invoke) Token: 0x06003E51 RID: 15953
			public delegate void PromoteSingleAddress(InboundMimeHeadersParser parser, Header header, Participant participant);
		}

		// Token: 0x020005EE RID: 1518
		private class CustomRule : InboundMimeHeadersParser.IHeaderPromotionRule
		{
			// Token: 0x06003E54 RID: 15956 RVA: 0x00102819 File Offset: 0x00100A19
			public CustomRule(InboundMimeHeadersParser.CustomRule.PromotionDelegate promotionDelegate)
			{
				this.promotionDelegate = promotionDelegate;
				this.lengthLimit = int.MaxValue;
			}

			// Token: 0x06003E55 RID: 15957 RVA: 0x00102833 File Offset: 0x00100A33
			public CustomRule(InboundMimeHeadersParser.CustomRule.PromotionDelegate promotionDelegate, int lengthLimit)
			{
				this.promotionDelegate = promotionDelegate;
				this.lengthLimit = lengthLimit;
			}

			// Token: 0x06003E56 RID: 15958 RVA: 0x0010284C File Offset: 0x00100A4C
			public void PromoteHeader(InboundMimeHeadersParser parser, Header header)
			{
				int num = Math.Min(this.lengthLimit, parser.ConversionOptions.Limits.MaxMimeTextHeaderLength);
				string headerValue = MimeHelpers.GetHeaderValue(header, num);
				if (headerValue != null)
				{
					this.promotionDelegate(parser, header, headerValue);
				}
			}

			// Token: 0x04002168 RID: 8552
			private InboundMimeHeadersParser.CustomRule.PromotionDelegate promotionDelegate;

			// Token: 0x04002169 RID: 8553
			private int lengthLimit;

			// Token: 0x020005EF RID: 1519
			// (Invoke) Token: 0x06003E58 RID: 15960
			public delegate void PromotionDelegate(InboundMimeHeadersParser parser, Header header, string value);
		}
	}
}
