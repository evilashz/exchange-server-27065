using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.FastTransfer
{
	// Token: 0x0200002C RID: 44
	internal class PerUserTableIterator : DisposableBase, IMessageIterator, IMessageIteratorClient, IDisposable
	{
		// Token: 0x060001BA RID: 442 RVA: 0x0000DE63 File Offset: 0x0000C063
		public PerUserTableIterator(FastTransferDownloadContext downloadContext, ExchangeId folderId)
		{
			if (downloadContext == null)
			{
				throw new ArgumentNullException("downloadContext");
			}
			this.context = downloadContext;
			this.readOnly = true;
			this.folderId = folderId;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000DE99 File Offset: 0x0000C099
		public PerUserTableIterator(FastTransferUploadContext uploadContext, ExchangeId folderId)
		{
			if (uploadContext == null)
			{
				throw new ArgumentNullException("uploadContext");
			}
			this.context = uploadContext;
			this.readOnly = false;
			this.folderId = folderId;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000DECF File Offset: 0x0000C0CF
		public bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000DED7 File Offset: 0x0000C0D7
		public FastTransferContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000E1DC File Offset: 0x0000C3DC
		public IEnumerator<IMessage> GetMessages()
		{
			if (this.folderId.IsZero)
			{
				foreach (PerUser perUserObject in PerUser.ResidentEntries(this.context.CurrentOperationContext, this.context.Logon.StoreMailbox))
				{
					IMessage result;
					using (LockManager.Lock(perUserObject, LockManager.LockType.PerUserShared))
					{
						result = new PerUserTableIterator.Record(this.Context, perUserObject);
					}
					yield return result;
				}
			}
			else
			{
				foreach (PerUser perUserObject2 in PerUser.ResidentEntriesForFolder(this.context.CurrentOperationContext, this.context.Logon.StoreMailbox, this.folderId))
				{
					IMessage result2;
					using (LockManager.Lock(perUserObject2, LockManager.LockType.PerUserShared))
					{
						result2 = new PerUserTableIterator.Record(this.Context, perUserObject2);
					}
					yield return result2;
				}
			}
			yield break;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000E1F8 File Offset: 0x0000C3F8
		public IMessage UploadMessage(bool isAssociatedMessage)
		{
			if (!this.oldEntriesDeleted)
			{
				if (this.folderId.IsZero)
				{
					PerUser.DeleteAllResidentEntries(this.context.CurrentOperationContext, this.context.Logon.StoreMailbox);
				}
				else
				{
					Folder folder = Folder.OpenFolder(this.context.CurrentOperationContext, this.context.Logon.StoreMailbox, this.folderId);
					PerUser.DeleteAllResidentEntriesForFolder(this.context.CurrentOperationContext, folder);
				}
				this.oldEntriesDeleted = true;
			}
			return new PerUserTableIterator.Record(this.context, null);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000E287 File Offset: 0x0000C487
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PerUserTableIterator>(this);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000E28F File Offset: 0x0000C48F
		protected override void InternalDispose(bool isCalledFromDispose)
		{
		}

		// Token: 0x040000CF RID: 207
		private readonly bool readOnly;

		// Token: 0x040000D0 RID: 208
		private ExchangeId folderId = ExchangeId.Zero;

		// Token: 0x040000D1 RID: 209
		private FastTransferContext context;

		// Token: 0x040000D2 RID: 210
		private bool oldEntriesDeleted;

		// Token: 0x0200002D RID: 45
		private sealed class MyMemoryPropertyBag : MemoryPropertyBag
		{
			// Token: 0x060001C2 RID: 450 RVA: 0x0000E291 File Offset: 0x0000C491
			public MyMemoryPropertyBag(ISession session) : base(session)
			{
			}

			// Token: 0x060001C3 RID: 451 RVA: 0x0000E29A File Offset: 0x0000C49A
			public override AnnotatedPropertyValue GetAnnotatedProperty(PropertyTag propertyTag)
			{
				if (propertyTag == PropertyTag.InstanceIdBin)
				{
					return base.GetAnnotatedProperty(PropertyTag.LongTermId);
				}
				return base.GetAnnotatedProperty(propertyTag);
			}
		}

		// Token: 0x0200002E RID: 46
		private class Record : IMessage, IDisposable
		{
			// Token: 0x060001C4 RID: 452 RVA: 0x0000E2BC File Offset: 0x0000C4BC
			public Record(FastTransferContext context, PerUser perUserObject)
			{
				this.context = context;
				this.propertyBag = new PerUserTableIterator.MyMemoryPropertyBag(context);
				if (perUserObject != null)
				{
					this.propertyBag.SetProperty(new PropertyValue(PerUser.MailboxGuidPropertyTag, perUserObject.Guid));
					this.propertyBag.SetProperty(new PropertyValue(PerUser.FolderIdPropertyTag, perUserObject.FolderId.To22ByteArray()));
					this.propertyBag.SetProperty(new PropertyValue(PerUser.CNSetPropertyTag, perUserObject.CNSetBytes));
					this.propertyBag.SetProperty(new PropertyValue(PerUser.LastModPropertyTag, (ExDateTime)perUserObject.LastModificationTime));
					this.propertyBag.SetProperty(new PropertyValue(PerUser.TypeTag, 0));
					this.propertyBag.SetProperty(new PropertyValue(PropertyTag.LongTermId, perUserObject.FolderId.To24ByteArray()));
					this.propertyBag.SetProperty(new PropertyValue(PropertyTag.InstanceIdBin, perUserObject.FolderId.To24ByteArray()));
				}
			}

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000E3CC File Offset: 0x0000C5CC
			public IPropertyBag PropertyBag
			{
				get
				{
					return this.propertyBag;
				}
			}

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000E3D4 File Offset: 0x0000C5D4
			public bool IsAssociated
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060001C7 RID: 455 RVA: 0x0000E478 File Offset: 0x0000C678
			public IEnumerable<IRecipient> GetRecipients()
			{
				yield break;
			}

			// Token: 0x060001C8 RID: 456 RVA: 0x0000E495 File Offset: 0x0000C695
			public IRecipient CreateRecipient()
			{
				throw new ExExceptionNoSupport((LID)49856U, "Recipients are not supported on the per user records");
			}

			// Token: 0x060001C9 RID: 457 RVA: 0x0000E4AB File Offset: 0x0000C6AB
			public void RemoveRecipient(int rowId)
			{
				throw new ExExceptionNoSupport((LID)42460U, "Recipient removal is not supported on the per user records");
			}

			// Token: 0x060001CA RID: 458 RVA: 0x0000E564 File Offset: 0x0000C764
			public IEnumerable<IAttachmentHandle> GetAttachments()
			{
				yield break;
			}

			// Token: 0x060001CB RID: 459 RVA: 0x0000E581 File Offset: 0x0000C781
			public IAttachment CreateAttachment()
			{
				throw new ExExceptionNoSupport((LID)48384U, "Attachments are not supported on the per user records");
			}

			// Token: 0x060001CC RID: 460 RVA: 0x0000E598 File Offset: 0x0000C798
			public void Save()
			{
				PropertyValue propertyValue = this.PropertyBag.GetAnnotatedProperty(PerUser.MailboxGuidPropertyTag).PropertyValue;
				Guid mailboxGuid = Guid.Empty;
				if (propertyValue.IsError || propertyValue.IsNotFound)
				{
					throw new ExExceptionCorruptData((LID)64768U, "Mailbox Guid is missing");
				}
				mailboxGuid = propertyValue.GetValue<Guid>();
				propertyValue = this.PropertyBag.GetAnnotatedProperty(PerUser.FolderIdPropertyTag).PropertyValue;
				ExchangeId folderId = ExchangeId.Zero;
				if (propertyValue.IsError || propertyValue.IsNotFound)
				{
					throw new ExExceptionCorruptData((LID)56576U, "FolderId is missing");
				}
				byte[] value = propertyValue.GetValue<byte[]>();
				if (value == null || value.Length <= 0)
				{
					throw new ExExceptionCorruptData((LID)40192U, "FolderId is corrupted");
				}
				folderId = ExchangeId.CreateFrom22ByteArray(this.context.CurrentOperationContext, this.context.Logon.StoreMailbox.ReplidGuidMap, value);
				propertyValue = this.PropertyBag.GetAnnotatedProperty(PerUser.CNSetPropertyTag).PropertyValue;
				if (propertyValue.IsError || propertyValue.IsNotFound)
				{
					throw new ExExceptionCorruptData((LID)60672U, "CNSet is missing");
				}
				byte[] value2 = propertyValue.GetValue<byte[]>();
				if (value2 == null || value2.Length <= 0)
				{
					throw new ExExceptionCorruptData((LID)44288U, "CNSet is corrupted");
				}
				IdSet cnSet = IdSet.Parse(this.context.CurrentOperationContext, value2);
				propertyValue = this.PropertyBag.GetAnnotatedProperty(PerUser.TypeTag).PropertyValue;
				if (propertyValue.IsError || propertyValue.IsNotFound)
				{
					throw new ExExceptionCorruptData((LID)36096U, "Type is missing");
				}
				int value3 = propertyValue.GetValue<int>();
				if (value3 != 0)
				{
					throw new ExExceptionNoSupport((LID)52480U, "Foreign PerUser entry is not supported");
				}
				propertyValue = this.PropertyBag.GetAnnotatedProperty(PerUser.LastModPropertyTag).PropertyValue;
				ExDateTime? value4 = new ExDateTime?(ExDateTime.MinValue);
				if (!propertyValue.IsError && !propertyValue.IsNotFound)
				{
					value4 = propertyValue.GetValue<ExDateTime?>();
				}
				PerUser.CreateResidentAndSave(this.context.CurrentOperationContext, this.context.Logon.StoreMailbox, mailboxGuid, folderId, cnSet, (DateTime?)value4);
			}

			// Token: 0x060001CD RID: 461 RVA: 0x0000E7CA File Offset: 0x0000C9CA
			public void SetLongTermId(StoreLongTermId longTermId)
			{
			}

			// Token: 0x060001CE RID: 462 RVA: 0x0000E7CC File Offset: 0x0000C9CC
			public void Dispose()
			{
			}

			// Token: 0x040000D3 RID: 211
			private FastTransferContext context;

			// Token: 0x040000D4 RID: 212
			private PerUserTableIterator.MyMemoryPropertyBag propertyBag;
		}
	}
}
