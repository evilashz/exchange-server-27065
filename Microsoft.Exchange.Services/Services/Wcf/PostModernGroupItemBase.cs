using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActivityLog;
using Microsoft.Exchange.Data.Storage.GroupMailbox.Escalation;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200092E RID: 2350
	internal abstract class PostModernGroupItemBase<RequestType, SingleItemType> : MultiStepServiceCommand<RequestType, SingleItemType>, IDisposeTrackable, IDisposable where RequestType : BaseRequest
	{
		// Token: 0x06004406 RID: 17414 RVA: 0x000E8458 File Offset: 0x000E6658
		public PostModernGroupItemBase(CallContext callContext, RequestType request) : base(callContext, request)
		{
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x06004407 RID: 17415 RVA: 0x000E846E File Offset: 0x000E666E
		protected bool NeedSend
		{
			get
			{
				return this.needSend;
			}
		}

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x06004408 RID: 17416 RVA: 0x000E8476 File Offset: 0x000E6676
		// (set) Token: 0x06004409 RID: 17417 RVA: 0x000E847E File Offset: 0x000E667E
		private protected MailboxSession GroupSession { protected get; private set; }

		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x0600440A RID: 17418 RVA: 0x000E8487 File Offset: 0x000E6687
		protected Participant GroupParticipant
		{
			get
			{
				return this.groupParticipant;
			}
		}

		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x0600440B RID: 17419 RVA: 0x000E848F File Offset: 0x000E668F
		protected EmailAddressWrapper[] ToRecipients
		{
			get
			{
				if (this.toRecipients == null)
				{
					this.toRecipients = this.GetToRecipients();
				}
				return this.toRecipients;
			}
		}

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x0600440C RID: 17420 RVA: 0x000E84AB File Offset: 0x000E66AB
		protected EmailAddressWrapper[] CcRecipients
		{
			get
			{
				if (this.ccRecipients == null)
				{
					this.ccRecipients = this.GetCcRecipients();
				}
				return this.ccRecipients;
			}
		}

		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x0600440D RID: 17421 RVA: 0x000E84C7 File Offset: 0x000E66C7
		// (set) Token: 0x0600440E RID: 17422 RVA: 0x000E84CF File Offset: 0x000E66CF
		protected EmailAddressWrapper ModernGroupEmailAddress
		{
			get
			{
				return this.modernGroupEmailAddress;
			}
			set
			{
				this.modernGroupEmailAddress = value;
			}
		}

		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x0600440F RID: 17423 RVA: 0x000E84D8 File Offset: 0x000E66D8
		protected ConversationId ConversationId
		{
			get
			{
				return this.conversationId;
			}
		}

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x06004410 RID: 17424 RVA: 0x000E84E0 File Offset: 0x000E66E0
		// (set) Token: 0x06004411 RID: 17425 RVA: 0x000E84E8 File Offset: 0x000E66E8
		public string ConversationShapeName
		{
			get
			{
				return this.conversationShapeName;
			}
			set
			{
				this.conversationShapeName = value;
			}
		}

		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x06004412 RID: 17426 RVA: 0x000E84F1 File Offset: 0x000E66F1
		protected bool IsFromYammer
		{
			get
			{
				return base.CallContext.CallerApplicationId == WellknownPartnerApplicationIdentifiers.Yammer;
			}
		}

		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x06004413 RID: 17427 RVA: 0x000E8508 File Offset: 0x000E6708
		protected virtual bool IsReplying
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004414 RID: 17428 RVA: 0x000E850B File Offset: 0x000E670B
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<PostModernGroupItemBase<RequestType, SingleItemType>>(this);
		}

		// Token: 0x06004415 RID: 17429 RVA: 0x000E8513 File Offset: 0x000E6713
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06004416 RID: 17430 RVA: 0x000E8528 File Offset: 0x000E6728
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			this.DisposeHelper();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004417 RID: 17431 RVA: 0x000E854C File Offset: 0x000E674C
		internal override void PreExecuteCommand()
		{
			this.GroupSession = (MailboxSession)base.IdConverter.ConvertTargetFolderIdToIdAndContentSession(this.GetGroupFolderId().BaseFolderId, true).Session;
			this.groupParticipant = new Participant(this.GroupSession.MailboxOwner);
			this.needSend = (this.HasOtherRecipients(this.ToRecipients) || this.HasOtherRecipients(this.CcRecipients));
		}

		// Token: 0x06004418 RID: 17432 RVA: 0x000E85BC File Offset: 0x000E67BC
		protected void OnBeforeSend(MessageItem messageItem)
		{
			messageItem.SuppressAllAutoResponses();
			messageItem.MarkRecipientAsSubmitted(new Participant[]
			{
				this.GroupParticipant
			});
		}

		// Token: 0x06004419 RID: 17433 RVA: 0x000E85E8 File Offset: 0x000E67E8
		protected void OnBeforeSaveOrSend(MessageItem messageItem)
		{
			this.onBeforeSaveOrSendCalled = true;
			messageItem.From = this.GetMeParticipant();
			messageItem.Sender = this.GroupParticipant;
			if (!this.IsGroupInRecipients(messageItem.Recipients))
			{
				messageItem.Recipients.Add(this.GroupParticipant);
			}
		}

		// Token: 0x0600441A RID: 17434 RVA: 0x000E8634 File Offset: 0x000E6834
		protected void OnAfterSaveOrSend(MessageItem messageItem)
		{
			this.conversationId = (ConversationId)PropertyCommand.GetPropertyValueFromStoreObject(messageItem, ItemSchema.ConversationId);
			this.EscalateIfNecessary(messageItem);
			if (this.GroupSession != null && this.GroupSession.ActivitySession != null)
			{
				this.GroupSession.ActivitySession.CaptureActivity(this.IsReplying ? ActivityId.ModernGroupsQuickReply : ActivityId.ModernGroupsQuickCompose, messageItem.StoreObjectId, null, base.CallContext.GetAccessingInformation());
			}
		}

		// Token: 0x0600441B RID: 17435
		protected abstract void DisposeHelper();

		// Token: 0x0600441C RID: 17436
		protected abstract EmailAddressWrapper[] GetToRecipients();

		// Token: 0x0600441D RID: 17437
		protected abstract EmailAddressWrapper[] GetCcRecipients();

		// Token: 0x0600441E RID: 17438 RVA: 0x000E86A4 File Offset: 0x000E68A4
		protected TargetFolderId GetGroupFolderId()
		{
			return new TargetFolderId(new DistinguishedFolderId
			{
				Mailbox = this.ModernGroupEmailAddress,
				Id = DistinguishedFolderIdName.inbox
			});
		}

		// Token: 0x0600441F RID: 17439 RVA: 0x000E86D0 File Offset: 0x000E68D0
		protected void AddConversationToResponseItem(ItemType item)
		{
			if (item != null && this.conversationShapeName != null)
			{
				ConversationType conversation = Util.LoadConversationUsingConversationId(this.conversationId, this.conversationShapeName, this.GetGroupFolderId(), base.IdConverter, this.GetHashCode(), base.CallContext.ProtocolLog);
				item.Conversation = conversation;
			}
		}

		// Token: 0x06004420 RID: 17440 RVA: 0x000E8720 File Offset: 0x000E6920
		protected void MoveToInboxAndSendIfNeeded(ItemType responseItem)
		{
			ServiceResult<ItemType> serviceResult = this.MoveItemToInbox(responseItem.ItemId.Id);
			if (serviceResult != null && serviceResult.Value != null && serviceResult.Value.ItemId != null)
			{
				responseItem.ItemId = serviceResult.Value.ItemId;
				if (this.NeedSend && this.SendMessageAfterPostingDraft(responseItem.ItemId))
				{
					this.LoadUpdatedChangeKeyForItem(responseItem.ItemId);
				}
			}
		}

		// Token: 0x06004421 RID: 17441 RVA: 0x000E878C File Offset: 0x000E698C
		private void LoadUpdatedChangeKeyForItem(ItemId itemId)
		{
			GetItemRequest getItemRequest = new GetItemRequest();
			getItemRequest.Ids = new BaseItemId[]
			{
				itemId
			};
			getItemRequest.ItemShape = new ItemResponseShape(ShapeEnum.IdOnly, BodyResponseType.Best, false, null);
			GetItem getItem = new GetItem(base.CallContext, getItemRequest);
			if (getItem.PreExecute())
			{
				ServiceResult<ItemType[]> serviceResult = getItem.Execute();
				ExTraceGlobals.ModernGroupsTracer.TraceDebug((long)this.GetHashCode(), "PostModernGroupItemBase.LoadUpdatedChangeKeyForItem: Execute completed.");
				if (serviceResult.Value != null && serviceResult.Value.Length > 0 && serviceResult.Value[0] != null && serviceResult.Value[0].ItemId != null)
				{
					itemId.ChangeKey = serviceResult.Value[0].ItemId.ChangeKey;
					return;
				}
			}
			else
			{
				ExTraceGlobals.ModernGroupsTracer.TraceError((long)this.GetHashCode(), "PostModernGroupItemBase.LoadUpdatedChangeKeyForItem: pre-execute failed.");
			}
		}

		// Token: 0x06004422 RID: 17442 RVA: 0x000E8850 File Offset: 0x000E6A50
		private ServiceResult<ItemType> MoveItemToInbox(string itemId)
		{
			MoveItemRequest moveItemRequest = new MoveItemRequest();
			ServiceResult<ItemType> result = null;
			moveItemRequest.ToFolderId = this.GetGroupFolderId();
			moveItemRequest.ReturnNewItemIds = true;
			moveItemRequest.Ids = new BaseItemId[]
			{
				new ItemId(itemId, null)
			};
			MoveItem moveItem = new MoveItem(base.CallContext, moveItemRequest);
			if (moveItem.PreExecute())
			{
				result = moveItem.Execute();
				ExTraceGlobals.ModernGroupsTracer.TraceDebug((long)this.GetHashCode(), "PostModernGroupItemBase.MoveItemToInbox: Execute completed.");
			}
			else
			{
				ExTraceGlobals.ModernGroupsTracer.TraceError((long)this.GetHashCode(), "PostModernGroupItemBase.MoveItemToInbox: pre-execute failed.");
			}
			return result;
		}

		// Token: 0x06004423 RID: 17443 RVA: 0x000E88DC File Offset: 0x000E6ADC
		private bool SendMessageAfterPostingDraft(ItemId itemId)
		{
			SendItemRequest sendItemRequest = new SendItemRequest();
			ServiceResult<ServiceResultNone> serviceResult = null;
			sendItemRequest.Ids = new BaseItemId[]
			{
				itemId
			};
			sendItemRequest.SavedItemFolderId = this.GetGroupFolderId();
			sendItemRequest.SaveItemToFolder = true;
			SendItem sendItem = new SendItem(base.CallContext, sendItemRequest);
			sendItem.BeforeMessageDisposition += this.OnBeforeSend;
			if (sendItem.PreExecute())
			{
				serviceResult = sendItem.Execute();
				sendItem.BeforeMessageDisposition -= this.OnBeforeSend;
				ExTraceGlobals.ModernGroupsTracer.TraceDebug((long)this.GetHashCode(), "PostModernGroupItemBase.SendMessageAfterPostingDraft: Execute completed.");
				if (serviceResult.Error != null)
				{
					ExTraceGlobals.ModernGroupsTracer.TraceDebug<string>((long)this.GetHashCode(), "PostModernGroupItemBase.SendMessageAfterPostingDraft: Execute completed with error {0}", serviceResult.Error.MessageText);
				}
			}
			else
			{
				ExTraceGlobals.ModernGroupsTracer.TraceError((long)this.GetHashCode(), "PostModernGroupItemBase.SendMessageAfterPostingDraft: pre-execute failed.");
			}
			return serviceResult != null && serviceResult.Error == null;
		}

		// Token: 0x06004424 RID: 17444 RVA: 0x000E89BC File Offset: 0x000E6BBC
		private void EscalateIfNecessary(MessageItem messageItem)
		{
			if (!GroupEscalation.IsEscalationEnabled())
			{
				ExTraceGlobals.ModernGroupsTracer.Information((long)this.GetHashCode(), "COWGroupMessageEscalation.SkipItemOperation: skipping group message escalation as the feature is disabled.");
				return;
			}
			IGroupEscalationFlightInfo groupEscalationFlightInfo = new GroupEscalationFlightInfo(this.GroupSession.MailboxOwner.GetContext(null));
			GroupEscalation groupEscalation = new GroupEscalation(XSOFactory.Default, groupEscalationFlightInfo, new MailboxUrls(this.GroupSession.MailboxOwner, false));
			bool flag;
			groupEscalation.EscalateItem(messageItem, this.GroupSession, out flag, this.IsFromYammer);
		}

		// Token: 0x06004425 RID: 17445 RVA: 0x000E8A34 File Offset: 0x000E6C34
		private bool HasOtherRecipients(EmailAddressWrapper[] recipients)
		{
			if (recipients == null)
			{
				ExTraceGlobals.ModernGroupsTracer.TraceDebug((long)this.GetHashCode(), "PostModernGroupItem.HasOtherRecipients: recipient count is 0");
				return false;
			}
			for (int i = 0; i < recipients.Length; i++)
			{
				if (!this.IsGroupEmail(recipients[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004426 RID: 17446 RVA: 0x000E8A78 File Offset: 0x000E6C78
		private bool IsGroupInRecipients(RecipientCollection recipients)
		{
			bool result = false;
			if (recipients != null && recipients.Contains(this.GroupParticipant))
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06004427 RID: 17447 RVA: 0x000E8A9C File Offset: 0x000E6C9C
		private bool IsGroupEmail(EmailAddressWrapper emailAddressWrapper)
		{
			bool flag = true;
			ProxyAddress proxyAddress;
			Participant participant;
			if (ProxyAddress.TryParse(emailAddressWrapper.EmailAddress, out proxyAddress))
			{
				flag = false;
				ADRecipient adrecipient = base.CallContext.ADRecipientSessionContext.GetADRecipientSession().FindByProxyAddress(proxyAddress);
				if (adrecipient == null)
				{
					ExTraceGlobals.ModernGroupsTracer.TraceDebug<string>((long)this.GetHashCode(), "PostModernGroupItemBase.IsGroupEmail: {0} can't be found in AD or AD cache", proxyAddress.AddressString);
					return false;
				}
				participant = new Participant(adrecipient);
				ExTraceGlobals.ModernGroupsTracer.TraceDebug<bool>((long)this.GetHashCode(), "PostModernGroupItemBase.IsGroupEmail: recipient.IsCached is {0}", adrecipient.IsCached);
			}
			else
			{
				participant = new Participant(emailAddressWrapper.OriginalDisplayName, emailAddressWrapper.EmailAddress, emailAddressWrapper.RoutingType);
				ExTraceGlobals.ModernGroupsTracer.TraceDebug<string>((long)this.GetHashCode(), "PostModernGroupItemBase.IsGroupEmail: {0} can't be parsed as ProxyAddress", emailAddressWrapper.EmailAddress);
			}
			if (flag)
			{
				base.CallContext.ProtocolLog.Set(PostModernGroupItemMetadata.MissedAdCache, true);
			}
			bool flag2 = Participant.HasSameEmail(this.groupParticipant, participant, this.GroupSession, flag);
			ExTraceGlobals.ModernGroupsTracer.TraceDebug<string, string, bool>((long)this.GetHashCode(), "PostModernGroupItemBase.IsGroupEmail: passed in email is {0}, group email is {1}, isGroupEmail = {2}", emailAddressWrapper.ToString(), this.groupParticipant.ToString(), flag2);
			return flag2;
		}

		// Token: 0x06004428 RID: 17448 RVA: 0x000E8BB0 File Offset: 0x000E6DB0
		private Participant GetMeParticipant()
		{
			EmailAddressWrapper address = ResolveNames.EmailAddressWrapperFromRecipient(base.CallContext.AccessingADUser);
			return MailboxHelper.GetParticipantFromAddress(address);
		}

		// Token: 0x040027B0 RID: 10160
		private bool needSend;

		// Token: 0x040027B1 RID: 10161
		private bool onBeforeSaveOrSendCalled;

		// Token: 0x040027B2 RID: 10162
		private readonly DisposeTracker disposeTracker;

		// Token: 0x040027B3 RID: 10163
		private EmailAddressWrapper[] toRecipients;

		// Token: 0x040027B4 RID: 10164
		private EmailAddressWrapper[] ccRecipients;

		// Token: 0x040027B5 RID: 10165
		private Participant groupParticipant;

		// Token: 0x040027B6 RID: 10166
		private EmailAddressWrapper modernGroupEmailAddress;

		// Token: 0x040027B7 RID: 10167
		private ConversationId conversationId;

		// Token: 0x040027B8 RID: 10168
		private string conversationShapeName;
	}
}
