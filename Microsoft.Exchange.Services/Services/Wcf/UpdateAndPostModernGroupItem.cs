using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200093D RID: 2365
	internal sealed class UpdateAndPostModernGroupItem : PostModernGroupItemBase<UpdateAndPostModernGroupItemRequest, UpdateItemResponseWrapper>
	{
		// Token: 0x06004472 RID: 17522 RVA: 0x000EB6AD File Offset: 0x000E98AD
		public UpdateAndPostModernGroupItem(CallContext callContext, UpdateAndPostModernGroupItemRequest request) : base(callContext, request)
		{
			base.ModernGroupEmailAddress = request.ModernGroupEmailAddress;
			OwsLogRegistry.Register(UpdateAndPostModernGroupItem.UpdateAndPostModernGroupItemActionName, typeof(UpdateAndPostModernGroupItemMetadata), new Type[0]);
		}

		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x06004473 RID: 17523 RVA: 0x000EB6DD File Offset: 0x000E98DD
		internal override int StepCount
		{
			get
			{
				if (base.Request.ItemChanges != null)
				{
					return base.Request.ItemChanges.Length;
				}
				return 0;
			}
		}

		// Token: 0x06004474 RID: 17524 RVA: 0x000EB6FC File Offset: 0x000E98FC
		internal override void PreExecuteCommand()
		{
			base.PreExecuteCommand();
			if (base.Request.ItemShape != null)
			{
				base.ConversationShapeName = base.Request.ItemShape.ConversationShapeName;
				base.Request.ItemShape.ConversationShapeName = null;
			}
			UpdateItemRequest updateItemRequest = new UpdateItemRequest();
			updateItemRequest.ItemChanges = base.Request.ItemChanges;
			updateItemRequest.MessageDisposition = "SaveOnly";
			updateItemRequest.InternetMessageId = base.Request.InternetMessageId;
			if (base.Request.ItemShape != null || base.Request.ShapeName != null)
			{
				updateItemRequest.ItemShape = base.Request.ItemShape;
				updateItemRequest.ShapeName = base.Request.ShapeName;
			}
			this.updateItemServiceCommand = new UpdateItem(base.CallContext, updateItemRequest);
			this.updateItemServiceCommand.BeforeMessageDisposition += base.OnBeforeSaveOrSend;
			this.updateItemServiceCommand.AfterMessageDisposition += base.OnAfterSaveOrSend;
			this.updateItemServiceCommand.PreExecute();
		}

		// Token: 0x06004475 RID: 17525 RVA: 0x000EB800 File Offset: 0x000E9A00
		internal override ServiceResult<UpdateItemResponseWrapper> Execute()
		{
			ServiceResult<UpdateItemResponseWrapper> result;
			try
			{
				ServiceResult<UpdateItemResponseWrapper> serviceResult = this.updateItemServiceCommand.Execute();
				this.updateItemServiceCommand.BeforeMessageDisposition -= base.OnBeforeSaveOrSend;
				this.updateItemServiceCommand.AfterMessageDisposition -= base.OnAfterSaveOrSend;
				if (serviceResult.Value != null && serviceResult.Value.Item != null)
				{
					base.MoveToInboxAndSendIfNeeded(serviceResult.Value.Item);
					base.AddConversationToResponseItem(serviceResult.Value.Item);
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

		// Token: 0x06004476 RID: 17526 RVA: 0x000EB89C File Offset: 0x000E9A9C
		internal override IExchangeWebMethodResponse GetResponse()
		{
			UpdateAndPostModernGroupItemResponse updateAndPostModernGroupItemResponse = new UpdateAndPostModernGroupItemResponse();
			updateAndPostModernGroupItemResponse.BuildForUpdateItemResults(base.Results);
			return updateAndPostModernGroupItemResponse;
		}

		// Token: 0x06004477 RID: 17527 RVA: 0x000EB8BC File Offset: 0x000E9ABC
		protected override void DisposeHelper()
		{
			if (this.updateItemServiceCommand != null)
			{
				this.updateItemServiceCommand = null;
			}
		}

		// Token: 0x06004478 RID: 17528 RVA: 0x000EB8D0 File Offset: 0x000E9AD0
		protected override EmailAddressWrapper[] GetToRecipients()
		{
			List<EmailAddressWrapper> list = new List<EmailAddressWrapper>();
			foreach (PropertyUpdate propertyUpdate in base.Request.ItemChanges[base.CurrentStep].PropertyUpdates)
			{
				SetItemPropertyUpdate setItemPropertyUpdate = propertyUpdate as SetItemPropertyUpdate;
				if (setItemPropertyUpdate != null)
				{
					MessageType messageType = setItemPropertyUpdate.Item as MessageType;
					if (messageType != null && messageType.ToRecipients != null)
					{
						list.AddRange(messageType.ToRecipients);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06004479 RID: 17529 RVA: 0x000EB948 File Offset: 0x000E9B48
		protected override EmailAddressWrapper[] GetCcRecipients()
		{
			List<EmailAddressWrapper> list = new List<EmailAddressWrapper>();
			foreach (PropertyUpdate propertyUpdate in base.Request.ItemChanges[base.CurrentStep].PropertyUpdates)
			{
				SetItemPropertyUpdate setItemPropertyUpdate = propertyUpdate as SetItemPropertyUpdate;
				if (setItemPropertyUpdate != null)
				{
					MessageType messageType = setItemPropertyUpdate.Item as MessageType;
					if (messageType != null && messageType.CcRecipients != null)
					{
						list.AddRange(messageType.CcRecipients);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600447A RID: 17530 RVA: 0x000EB9C0 File Offset: 0x000E9BC0
		private string GetConversationTopic()
		{
			foreach (PropertyUpdate propertyUpdate in base.Request.ItemChanges[base.CurrentStep].PropertyUpdates)
			{
				SetItemPropertyUpdate setItemPropertyUpdate = propertyUpdate as SetItemPropertyUpdate;
				if (setItemPropertyUpdate != null)
				{
					MessageType messageType = setItemPropertyUpdate.Item as MessageType;
					if (messageType != null && messageType.Subject != null)
					{
						return messageType.Subject;
					}
				}
			}
			return null;
		}

		// Token: 0x0600447B RID: 17531 RVA: 0x000EBA2C File Offset: 0x000E9C2C
		private void LogExceptionContext()
		{
			base.CallContext.ProtocolLog.Set(UpdateAndPostModernGroupItemMetadata.ConversationTopic, this.GetConversationTopic());
			base.CallContext.ProtocolLog.Set(UpdateAndPostModernGroupItemMetadata.ConversationId, base.ConversationId);
			base.CallContext.ProtocolLog.Set(UpdateAndPostModernGroupItemMetadata.ToRecipientCount, (base.ToRecipients == null) ? 0 : base.ToRecipients.Count<EmailAddressWrapper>());
			base.CallContext.ProtocolLog.Set(UpdateAndPostModernGroupItemMetadata.CcRecipientCount, (base.CcRecipients == null) ? 0 : base.CcRecipients.Count<EmailAddressWrapper>());
		}

		// Token: 0x040027E9 RID: 10217
		private static readonly string UpdateAndPostModernGroupItemActionName = typeof(UpdateAndPostModernGroupItem).Name;

		// Token: 0x040027EA RID: 10218
		private UpdateItem updateItemServiceCommand;
	}
}
