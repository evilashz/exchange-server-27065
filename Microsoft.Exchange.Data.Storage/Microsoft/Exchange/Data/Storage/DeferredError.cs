using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000BB0 RID: 2992
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DeferredError : IDisposable
	{
		// Token: 0x06006AF7 RID: 27383 RVA: 0x001C81E8 File Offset: 0x001C63E8
		private DeferredError()
		{
		}

		// Token: 0x06006AF8 RID: 27384 RVA: 0x001C81F0 File Offset: 0x001C63F0
		public static DeferredError Create(MailboxSession session, StoreObjectId folderId, string providerName, long ruleId, RuleAction.Type actionType, int actionNumber, DeferredError.RuleError ruleError)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(folderId, "folderId");
			Util.ThrowOnNullArgument(providerName, "providerName");
			EnumValidator.ThrowIfInvalid<RuleAction.Type>(actionType, "actionType");
			EnumValidator.ThrowIfInvalid<DeferredError.RuleError>(ruleError, "ruleError");
			if (!IdConverter.IsFolderId(folderId))
			{
				throw new ArgumentException(ServerStrings.InvalidFolderId(folderId.ToBase64String()));
			}
			DeferredError deferredError = new DeferredError();
			deferredError.message = MessageItem.Create(session, session.GetDefaultFolderId(DefaultFolderType.DeferredActionFolder));
			deferredError.message[InternalSchema.ItemClass] = "IPC.Microsoft Exchange 4.0.Deferred Error";
			deferredError.message[InternalSchema.RuleFolderEntryId] = folderId.ProviderLevelItemId;
			deferredError.message[InternalSchema.RuleId] = ruleId;
			deferredError.message[InternalSchema.RuleActionType] = (int)actionType;
			deferredError.message[InternalSchema.RuleActionNumber] = actionNumber;
			deferredError.message[InternalSchema.RuleError] = ruleError;
			deferredError.message[InternalSchema.RuleProvider] = providerName;
			return deferredError;
		}

		// Token: 0x06006AF9 RID: 27385 RVA: 0x001C8308 File Offset: 0x001C6508
		public byte[] Save()
		{
			this.CheckDisposed("Save");
			this.message.Save(SaveMode.NoConflictResolution);
			this.message.Load(DeferredError.EntryId);
			return this.message[InternalSchema.EntryId] as byte[];
		}

		// Token: 0x17001D1E RID: 7454
		// (get) Token: 0x06006AFA RID: 27386 RVA: 0x001C8354 File Offset: 0x001C6554
		public MessageItem Message
		{
			get
			{
				this.CheckDisposed("Message::get");
				return this.message;
			}
		}

		// Token: 0x06006AFB RID: 27387 RVA: 0x001C8367 File Offset: 0x001C6567
		protected void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				StorageGlobals.TraceFailedCheckDisposed(this, methodName);
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x06006AFC RID: 27388 RVA: 0x001C8389 File Offset: 0x001C6589
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06006AFD RID: 27389 RVA: 0x001C8398 File Offset: 0x001C6598
		private void Dispose(bool disposing)
		{
			StorageGlobals.TraceDispose(this, this.isDisposed, disposing);
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.InternalDispose(disposing);
			}
		}

		// Token: 0x06006AFE RID: 27390 RVA: 0x001C83BD File Offset: 0x001C65BD
		protected void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.message.Dispose();
			}
		}

		// Token: 0x04003CF5 RID: 15605
		private const string DaeMsgClass = "IPC.Microsoft Exchange 4.0.Deferred Error";

		// Token: 0x04003CF6 RID: 15606
		public static readonly PropertyDefinition[] EntryId = new PropertyDefinition[]
		{
			StoreObjectSchema.EntryId
		};

		// Token: 0x04003CF7 RID: 15607
		private MessageItem message;

		// Token: 0x04003CF8 RID: 15608
		private bool isDisposed;

		// Token: 0x02000BB1 RID: 2993
		public enum RuleError
		{
			// Token: 0x04003CFA RID: 15610
			Unknown = 1,
			// Token: 0x04003CFB RID: 15611
			Load,
			// Token: 0x04003CFC RID: 15612
			Delivery,
			// Token: 0x04003CFD RID: 15613
			Parsing,
			// Token: 0x04003CFE RID: 15614
			CreateDae,
			// Token: 0x04003CFF RID: 15615
			NoFolder,
			// Token: 0x04003D00 RID: 15616
			NoRights,
			// Token: 0x04003D01 RID: 15617
			CreateDam,
			// Token: 0x04003D02 RID: 15618
			NoSendAs,
			// Token: 0x04003D03 RID: 15619
			NoTemplateId,
			// Token: 0x04003D04 RID: 15620
			Execution,
			// Token: 0x04003D05 RID: 15621
			QuotaExceeded,
			// Token: 0x04003D06 RID: 15622
			TooManyRecips,
			// Token: 0x04003D07 RID: 15623
			FolderOverQuota
		}
	}
}
