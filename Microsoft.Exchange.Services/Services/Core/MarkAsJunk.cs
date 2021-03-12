using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000349 RID: 841
	internal class MarkAsJunk : MultiStepServiceCommand<MarkAsJunkRequest, ItemId>
	{
		// Token: 0x060017B9 RID: 6073 RVA: 0x0007F120 File Offset: 0x0007D320
		public MarkAsJunk(CallContext callContext, MarkAsJunkRequest request) : base(callContext, request)
		{
			this.session = callContext.SessionCache.GetMailboxIdentityMailboxSession();
			this.itemIds = request.ItemIds;
			this.isJunk = request.IsJunk;
			this.moveItem = request.MoveItem;
			this.sendCopy = request.SendCopy;
			this.junkMessageHeader = request.JunkMessageHeader;
			this.junkMessageBody = request.JunkMessageBody;
			ServiceCommandBase.ThrowIfNullOrEmpty<ItemId>(this.itemIds, "itemIds", "MarkAsJunk::Constructor");
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060017BA RID: 6074 RVA: 0x0007F1A3 File Offset: 0x0007D3A3
		internal override int StepCount
		{
			get
			{
				return this.itemIds.Count;
			}
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x0007F1B0 File Offset: 0x0007D3B0
		internal override IExchangeWebMethodResponse GetResponse()
		{
			MarkAsJunkResponse markAsJunkResponse = new MarkAsJunkResponse();
			markAsJunkResponse.AddResponses(base.Results);
			return markAsJunkResponse;
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x0007F1D0 File Offset: 0x0007D3D0
		internal override ServiceResult<ItemId> Execute()
		{
			ItemId value = this.InternalMarkAsJunk(this.itemIds[base.CurrentStep]);
			return new ServiceResult<ItemId>(value);
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x0007F1FC File Offset: 0x0007D3FC
		private ItemId InternalMarkAsJunk(ItemId itemId)
		{
			ItemId result = null;
			JunkEmailRule junkEmailRule = this.session.JunkEmailRule;
			bool flag = false;
			PropertyDefinition[] propsToReturn = new PropertyDefinition[]
			{
				MessageItemSchema.TransportMessageHeaders
			};
			using (MessageItem messageItem = MessageItem.Bind(this.session, IdConverter.EwsIdToMessageStoreObjectId(itemId.Id), propsToReturn))
			{
				if (ServiceCommandBase.IsAssociated(messageItem))
				{
					throw new ServiceInvalidOperationException((CoreResources.IDs)3859804741U);
				}
				string text = (string)messageItem[MessageItemSchema.SenderSmtpAddress];
				JunkEmailCollection.ValidationProblem validationProblem = JunkEmailCollection.ValidationProblem.NoError;
				try
				{
					if (this.isJunk)
					{
						validationProblem = junkEmailRule.BlockedSenderEmailCollection.TryAdd(text);
						junkEmailRule.TrustedSenderEmailCollection.Remove(text);
					}
					else
					{
						junkEmailRule.BlockedSenderEmailCollection.Remove(text);
						validationProblem = junkEmailRule.TrustedSenderEmailCollection.TryAdd(text);
					}
				}
				catch (JunkEmailValidationException ex)
				{
					validationProblem = ex.Problem;
				}
				finally
				{
					switch (validationProblem)
					{
					case JunkEmailCollection.ValidationProblem.NoError:
					case JunkEmailCollection.ValidationProblem.Duplicate:
					case JunkEmailCollection.ValidationProblem.Empty:
						flag = true;
						break;
					}
				}
				if (flag)
				{
					junkEmailRule.Save();
					if (this.moveItem)
					{
						DefaultFolderType defaultFolderType = this.isJunk ? DefaultFolderType.JunkEmail : DefaultFolderType.Inbox;
						using (Folder folder = Folder.Bind(this.session, defaultFolderType))
						{
							AggregateOperationResult aggregateOperationResult = messageItem.Session.Move(folder.Session, folder.Id, true, new StoreId[]
							{
								messageItem.Id
							});
							if (aggregateOperationResult != null && aggregateOperationResult.GroupOperationResults != null && aggregateOperationResult.GroupOperationResults.Length == 1 && aggregateOperationResult.GroupOperationResults[0].OperationResult == OperationResult.Succeeded && aggregateOperationResult.GroupOperationResults[0].ResultObjectIds != null && aggregateOperationResult.GroupOperationResults[0].ResultObjectIds.Count == 1)
							{
								StoreId storeItemId = IdConverter.CombineStoreObjectIdWithChangeKey(aggregateOperationResult.GroupOperationResults[0].ResultObjectIds[0], aggregateOperationResult.GroupOperationResults[0].ResultChangeKeys[0]);
								result = IdConverter.ConvertStoreItemIdToItemId(storeItemId, this.session);
							}
						}
					}
					if (this.sendCopy)
					{
						string value = (string)messageItem[MessageItemSchema.TransportMessageHeaders];
						EmailAddressWrapper emailAddressWrapper = new EmailAddressWrapper();
						if (this.isJunk)
						{
							emailAddressWrapper.EmailAddress = Global.JunkMailReportingAddress;
						}
						else
						{
							emailAddressWrapper.EmailAddress = Global.NotJunkMailReportingAddress;
						}
						emailAddressWrapper.RoutingType = "SMTP";
						Participant participant = new Participant(emailAddressWrapper.Name, emailAddressWrapper.EmailAddress, emailAddressWrapper.RoutingType);
						try
						{
							using (MessageItem messageItem2 = MessageItem.Create(this.session, Folder.Bind(this.session, DefaultFolderType.Drafts).Id))
							{
								if (this.junkMessageHeader != null)
								{
									messageItem2.Subject = "[" + this.junkMessageHeader + "] " + messageItem.Subject;
								}
								else
								{
									messageItem2.Subject = messageItem.Subject;
								}
								using (ItemAttachment itemAttachment = messageItem2.AttachmentCollection.AddExistingItem(messageItem))
								{
									itemAttachment[AttachmentSchema.DisplayName] = messageItem.Subject;
									itemAttachment.Save();
								}
								using (TextWriter textWriter = messageItem2.Body.OpenTextWriter(BodyFormat.TextPlain))
								{
									if (this.junkMessageBody != null)
									{
										textWriter.WriteLine(this.junkMessageBody);
									}
									textWriter.Write(value);
								}
								messageItem2.Recipients.Add(participant);
								messageItem2.SendWithoutSavingMessage();
							}
						}
						catch (Exception arg)
						{
							ExTraceGlobals.ExceptionTracer.TraceError<Exception>((long)this.GetHashCode(), "MarkAsJunk.InternalMarkAsJunk called for exception: {0}", arg);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x04000FF2 RID: 4082
		private readonly bool isJunk;

		// Token: 0x04000FF3 RID: 4083
		private readonly bool moveItem;

		// Token: 0x04000FF4 RID: 4084
		private readonly bool sendCopy;

		// Token: 0x04000FF5 RID: 4085
		private readonly string junkMessageHeader;

		// Token: 0x04000FF6 RID: 4086
		private readonly string junkMessageBody;

		// Token: 0x04000FF7 RID: 4087
		private readonly MailboxSession session;

		// Token: 0x04000FF8 RID: 4088
		private IList<ItemId> itemIds;
	}
}
