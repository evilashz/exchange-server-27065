using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Handler;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x02000078 RID: 120
	internal class MessageIterator : ServerObject, IMessageIterator, IDisposable, WatsonHelper.IProvideWatsonReportData
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x0002140A File Offset: 0x0001F60A
		internal MessageIterator(Logon logon, StoreId folderId, StoreId[] messageIds, FastTransferCopyMessagesFlag flags, FastTransferSendOption sendOptions) : base(logon)
		{
			this.folderId = folderId;
			this.messageIds = messageIds;
			this.flags = flags;
			this.sendOptions = sendOptions;
			if (Activity.Current != null)
			{
				this.watsonReportActionGuard = Activity.Current.RegisterWatsonReportDataProviderAndGetGuard(WatsonReportActionType.MessageAdaptor, this);
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x000216B0 File Offset: 0x0001F8B0
		public IEnumerator<IMessage> GetMessages()
		{
			MessageAdaptor.Options options = new MessageAdaptor.Options
			{
				IsReadOnly = true,
				IsEmbedded = false,
				SendEntryId = ((byte)(this.flags & FastTransferCopyMessagesFlag.SendEntryId) == 32),
				DownloadBodyOption = (((byte)(this.flags & FastTransferCopyMessagesFlag.BestBody) == 16) ? DownloadBodyOption.BestBodyOnly : DownloadBodyOption.RtfOnly),
				IsUpload = this.sendOptions.IsUpload()
			};
			int messagesPreread = 0;
			int messagesTillNextPreread = 25;
			this.PrereadMessages(ref messagesPreread);
			foreach (StoreId messageId in this.messageIds)
			{
				IMessage message = null;
				if (this.TryGetMessage(messageId, options, out message))
				{
					yield return message;
				}
				else
				{
					yield return null;
				}
				messagesTillNextPreread--;
				if (messagesTillNextPreread == 0)
				{
					this.PrereadMessages(ref messagesPreread);
					messagesTillNextPreread = 50;
				}
			}
			yield break;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x000216CC File Offset: 0x0001F8CC
		private static string StoreIdsToString(StoreId[] ids)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (StoreId storeId in ids)
			{
				stringBuilder.Append("[");
				stringBuilder.Append(storeId.ToString());
				stringBuilder.Append("] ");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00021730 File Offset: 0x0001F930
		private bool TryGetMessage(StoreId messageId, MessageAdaptor.Options options, out IMessage message)
		{
			message = null;
			bool result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				CoreItem coreItem = null;
				try
				{
					coreItem = CoreItem.Bind(base.LogonObject.Session, base.LogonObject.Session.IdConverter.CreateMessageId(this.folderId, messageId), CoreObjectSchema.AllPropertiesOnStore);
				}
				catch (ObjectNotFoundException)
				{
				}
				catch (AccessDeniedException)
				{
				}
				if (coreItem == null)
				{
					result = false;
				}
				else
				{
					disposeGuard.Add<CoreItem>(coreItem);
					if (options.SkipMessagesInConflict && coreItem.PropertyBag.GetValueAsNullable<bool>(MessageItemSchema.MessageInConflict) == true)
					{
						result = false;
					}
					else
					{
						ReferenceCount<CoreItem> referenceCount = new ReferenceCount<CoreItem>(coreItem);
						try
						{
							IMessage message2 = new MessageAdaptor(referenceCount, options, base.LogonObject.LogonString8Encoding, this.sendOptions.IsUpload(), null);
							disposeGuard.Add<IMessage>(message2);
							PropertyValue propertyValue = message2.PropertyBag.GetAnnotatedProperty(PropertyTag.Subject).PropertyValue;
							this.lastMessageForTracing = (propertyValue.IsError ? null : propertyValue.GetValueAssert<string>());
							disposeGuard.Success();
							message = message2;
							result = true;
						}
						finally
						{
							referenceCount.Release();
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x000218C4 File Offset: 0x0001FAC4
		private void PrereadMessages(ref int messagesPreread)
		{
			PrivateLogon privateLogon = base.LogonObject as PrivateLogon;
			if (privateLogon != null && messagesPreread < this.messageIds.Length)
			{
				int num = Math.Min(this.messageIds.Length - messagesPreread, 50);
				if (num > 1)
				{
					StoreId[] array = new StoreId[num];
					for (int i = 0; i < num; i++)
					{
						array[i] = base.LogonObject.Session.IdConverter.CreateMessageId(this.folderId, this.messageIds[messagesPreread + i]);
					}
					privateLogon.MailboxSession.PrereadMessages(array);
					messagesPreread += num;
				}
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00021969 File Offset: 0x0001FB69
		protected override void InternalDispose()
		{
			base.InternalDispose();
			Util.DisposeIfPresent(this.watsonReportActionGuard);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0002197C File Offset: 0x0001FB7C
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MessageIterator>(this);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00021984 File Offset: 0x0001FB84
		string WatsonHelper.IProvideWatsonReportData.GetWatsonReportString()
		{
			base.CheckDisposed();
			return string.Format("MessageIterator: MessageIds: {0}\r\nLast Message = \"{1}\".", MessageIterator.StoreIdsToString(this.messageIds), this.lastMessageForTracing);
		}

		// Token: 0x040001BD RID: 445
		private const int MaximumMessagePrereadSize = 50;

		// Token: 0x040001BE RID: 446
		private readonly StoreId folderId;

		// Token: 0x040001BF RID: 447
		private readonly StoreId[] messageIds;

		// Token: 0x040001C0 RID: 448
		private readonly FastTransferCopyMessagesFlag flags;

		// Token: 0x040001C1 RID: 449
		private readonly FastTransferSendOption sendOptions;

		// Token: 0x040001C2 RID: 450
		private readonly IDisposable watsonReportActionGuard;

		// Token: 0x040001C3 RID: 451
		private string lastMessageForTracing;
	}
}
