using System;
using System.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200092F RID: 2351
	internal sealed class PostModernGroupItem : PostModernGroupItemBase<PostModernGroupItemRequest, ItemType[]>
	{
		// Token: 0x06004429 RID: 17449 RVA: 0x000E8BD4 File Offset: 0x000E6DD4
		public PostModernGroupItem(CallContext callContext, PostModernGroupItemRequest request) : base(callContext, request)
		{
			base.ModernGroupEmailAddress = request.ModernGroupEmailAddress;
			this.currentMessageType = (base.Request.Items.Items[base.CurrentStep] as MessageType);
			OwsLogRegistry.Register(PostModernGroupItem.PostModernGroupItemActionName, typeof(PostModernGroupItemMetadata), new Type[0]);
			if (this.currentMessageType is SmartResponseType)
			{
				this.isReplying = true;
				this.isReplyingUsingDraft = (((SmartResponseType)this.currentMessageType).UpdateResponseItemId != null);
			}
		}

		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x0600442A RID: 17450 RVA: 0x000E8C61 File Offset: 0x000E6E61
		internal override int StepCount
		{
			get
			{
				return base.Request.Items.Items.Length;
			}
		}

		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x0600442B RID: 17451 RVA: 0x000E8C75 File Offset: 0x000E6E75
		protected override bool IsReplying
		{
			get
			{
				return this.isReplying;
			}
		}

		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x0600442C RID: 17452 RVA: 0x000E8C7D File Offset: 0x000E6E7D
		private bool ShouldUseSaveAndSendDispositionForPost
		{
			get
			{
				return base.NeedSend && !this.isReplyingUsingDraft;
			}
		}

		// Token: 0x0600442D RID: 17453 RVA: 0x000E8C94 File Offset: 0x000E6E94
		internal override void PreExecuteCommand()
		{
			DateTime utcNow = DateTime.UtcNow;
			base.PreExecuteCommand();
			if (base.Request.ItemShape != null)
			{
				base.ConversationShapeName = base.Request.ItemShape.ConversationShapeName;
				base.Request.ItemShape.ConversationShapeName = null;
			}
			CreateItemRequest createItemRequest = new CreateItemRequest();
			createItemRequest.Items = this.CreateMessageItemsForPost();
			createItemRequest.SavedItemFolderId = base.GetGroupFolderId();
			createItemRequest.IsNonDraft = true;
			if (base.Request.ItemShape != null || base.Request.ShapeName != null)
			{
				createItemRequest.ItemShape = base.Request.ItemShape;
				createItemRequest.ShapeName = base.Request.ShapeName;
			}
			else
			{
				createItemRequest.ShapeName = "QuickComposeItemPart";
				createItemRequest.ItemShape = ServiceCommandBase.DefaultItemResponseShape;
			}
			this.createItemServiceCommand = new CreateItem(base.CallContext, createItemRequest);
			this.createItemServiceCommand.BeforeMessageDisposition += base.OnBeforeSaveOrSend;
			this.createItemServiceCommand.AfterMessageDisposition += base.OnAfterSaveOrSend;
			if (this.ShouldUseSaveAndSendDispositionForPost)
			{
				createItemRequest.MessageDisposition = "SendAndSaveCopy";
				this.createItemServiceCommand.BeforeMessageDisposition += base.OnBeforeSend;
			}
			else
			{
				createItemRequest.MessageDisposition = "SaveOnly";
			}
			DateTime utcNow2 = DateTime.UtcNow;
			base.CallContext.ProtocolLog.Set(PostModernGroupItemMetadata.PreExecuteCommandLatency, (utcNow2 - utcNow).Milliseconds);
			this.createItemServiceCommand.PreExecute();
		}

		// Token: 0x0600442E RID: 17454 RVA: 0x000E8E10 File Offset: 0x000E7010
		internal override ServiceResult<ItemType[]> Execute()
		{
			ServiceResult<ItemType[]> result;
			try
			{
				ServiceResult<ItemType[]> serviceResult = this.createItemServiceCommand.Execute();
				if (this.ShouldUseSaveAndSendDispositionForPost)
				{
					this.createItemServiceCommand.BeforeMessageDisposition -= base.OnBeforeSend;
				}
				this.createItemServiceCommand.BeforeMessageDisposition -= base.OnBeforeSaveOrSend;
				this.createItemServiceCommand.AfterMessageDisposition -= base.OnAfterSaveOrSend;
				if (serviceResult.Value != null && serviceResult.Value.Length > 0 && serviceResult.Value[0] != null)
				{
					if (this.isReplyingUsingDraft)
					{
						base.MoveToInboxAndSendIfNeeded(serviceResult.Value[0]);
					}
					base.AddConversationToResponseItem(serviceResult.Value[0]);
				}
				result = serviceResult;
			}
			catch (Exception)
			{
				this.LogExceptionContext();
				throw;
			}
			return result;
		}

		// Token: 0x0600442F RID: 17455 RVA: 0x000E8ED8 File Offset: 0x000E70D8
		internal override IExchangeWebMethodResponse GetResponse()
		{
			PostModernGroupItemResponse postModernGroupItemResponse = new PostModernGroupItemResponse();
			postModernGroupItemResponse.BuildForResults<ItemType[]>(base.Results);
			return postModernGroupItemResponse;
		}

		// Token: 0x06004430 RID: 17456 RVA: 0x000E8EF8 File Offset: 0x000E70F8
		protected override EmailAddressWrapper[] GetToRecipients()
		{
			return this.currentMessageType.ToRecipients;
		}

		// Token: 0x06004431 RID: 17457 RVA: 0x000E8F05 File Offset: 0x000E7105
		protected override EmailAddressWrapper[] GetCcRecipients()
		{
			return this.currentMessageType.CcRecipients;
		}

		// Token: 0x06004432 RID: 17458 RVA: 0x000E8F12 File Offset: 0x000E7112
		protected override void DisposeHelper()
		{
			if (this.createItemServiceCommand != null)
			{
				this.createItemServiceCommand.Dispose();
				this.createItemServiceCommand = null;
			}
		}

		// Token: 0x06004433 RID: 17459 RVA: 0x000E8F30 File Offset: 0x000E7130
		private NonEmptyArrayOfAllItemsType CreateMessageItemsForPost()
		{
			NonEmptyArrayOfAllItemsType nonEmptyArrayOfAllItemsType = new NonEmptyArrayOfAllItemsType();
			nonEmptyArrayOfAllItemsType.Add(this.currentMessageType);
			this.SetReferenceItemIdForYammerInterop();
			return nonEmptyArrayOfAllItemsType;
		}

		// Token: 0x06004434 RID: 17460 RVA: 0x000E8F58 File Offset: 0x000E7158
		private void SetReferenceItemIdForYammerInterop()
		{
			if (base.IsFromYammer)
			{
				SmartResponseType smartResponseType = this.currentMessageType as SmartResponseType;
				if (smartResponseType != null && smartResponseType.ReferenceItemId == null && !string.IsNullOrEmpty(smartResponseType.InReplyTo))
				{
					IStorePropertyBag[] array = AllItemsFolderHelper.FindItemsFromInternetId(base.GroupSession, smartResponseType.InReplyTo, new StorePropertyDefinition[]
					{
						CoreItemSchema.Id
					});
					if (array != null && array.Any<IStorePropertyBag>())
					{
						VersionedId valueOrDefault = array[0].GetValueOrDefault<VersionedId>(CoreItemSchema.Id, null);
						if (valueOrDefault != null && valueOrDefault.ObjectId != null)
						{
							ItemId itemIdFromStoreId = IdConverter.GetItemIdFromStoreId(valueOrDefault, new MailboxId(base.GroupSession));
							smartResponseType.ReferenceItemId = itemIdFromStoreId;
						}
					}
				}
			}
		}

		// Token: 0x06004435 RID: 17461 RVA: 0x000E8FF8 File Offset: 0x000E71F8
		private void LogExceptionContext()
		{
			base.CallContext.ProtocolLog.Set(PostModernGroupItemMetadata.ConversationTopic, this.currentMessageType.Subject);
			base.CallContext.ProtocolLog.Set(PostModernGroupItemMetadata.ConversationId, base.ConversationId);
			base.CallContext.ProtocolLog.Set(PostModernGroupItemMetadata.ToRecipientCount, (base.ToRecipients == null) ? 0 : base.ToRecipients.Count<EmailAddressWrapper>());
			base.CallContext.ProtocolLog.Set(PostModernGroupItemMetadata.CcRecipientCount, (base.CcRecipients == null) ? 0 : base.CcRecipients.Count<EmailAddressWrapper>());
			base.CallContext.ProtocolLog.Set(PostModernGroupItemMetadata.IsReplying, this.isReplying);
			base.CallContext.ProtocolLog.Set(PostModernGroupItemMetadata.IsReplyingUsingDraft, this.isReplyingUsingDraft);
		}

		// Token: 0x040027BA RID: 10170
		private const string DefaultShapeName = "QuickComposeItemPart";

		// Token: 0x040027BB RID: 10171
		private static readonly string PostModernGroupItemActionName = typeof(PostModernGroupItem).Name;

		// Token: 0x040027BC RID: 10172
		private readonly MessageType currentMessageType;

		// Token: 0x040027BD RID: 10173
		private readonly bool isReplyingUsingDraft;

		// Token: 0x040027BE RID: 10174
		private readonly bool isReplying;

		// Token: 0x040027BF RID: 10175
		private CreateItem createItemServiceCommand;
	}
}
