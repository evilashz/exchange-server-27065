using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200009B RID: 155
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DeferredAction : IDisposable
	{
		// Token: 0x06000AE4 RID: 2788 RVA: 0x0004B911 File Offset: 0x00049B11
		private DeferredAction()
		{
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0004B91C File Offset: 0x00049B1C
		internal static DeferredAction Create(MailboxSession session, StoreObjectId ruleFolderId, string providerName)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(ruleFolderId, "ruleFolderId");
			Util.ThrowOnNullArgument(providerName, "providerName");
			if (!IdConverter.IsFolderId(ruleFolderId))
			{
				throw new ArgumentException(ServerStrings.InvalidFolderId(ruleFolderId.ToBase64String()));
			}
			DeferredAction deferredAction = new DeferredAction();
			deferredAction.actions = new List<RuleAction>();
			deferredAction.ruleIds = new List<long>();
			deferredAction.message = MessageItem.Create(session, session.GetDefaultFolderId(DefaultFolderType.DeferredActionFolder));
			deferredAction.message[InternalSchema.ItemClass] = "IPC.Microsoft Exchange 4.0.Deferred Action";
			deferredAction.message[InternalSchema.RuleFolderEntryId] = ruleFolderId.ProviderLevelItemId;
			deferredAction.message[InternalSchema.RuleProvider] = providerName;
			return deferredAction;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0004B9D5 File Offset: 0x00049BD5
		public void ClearActions()
		{
			this.CheckDisposed("ClearActions");
			this.actions.Clear();
			this.ruleIds.Clear();
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0004B9F8 File Offset: 0x00049BF8
		public void AddAction(long ruleId, RuleAction action)
		{
			this.CheckDisposed("AddAction");
			Util.ThrowOnNullArgument(action, "action");
			this.ruleIds.Add(ruleId);
			this.actions.Add(action);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0004BA28 File Offset: 0x00049C28
		public void SerializeActionsAndSave()
		{
			this.CheckDisposed("SerializeActionsAndSave");
			byte[] buffer = null;
			StoreSession session = this.message.Session;
			bool flag = false;
			try
			{
				if (session != null)
				{
					session.BeginMapiCall();
					session.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				buffer = this.message.Session.Mailbox.MapiStore.MapActionsToMDBActions(this.actions.ToArray());
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.ErrorSavingRules, ex, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("DeferredAction.SerializeActionsAndSave.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.ErrorSavingRules, ex2, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("DeferredAction.SerializeActionsAndSave.", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (session != null)
					{
						session.EndMapiCall();
						if (flag)
						{
							session.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			using (Stream stream = this.Message.OpenPropertyStream(InternalSchema.ClientActions, PropertyOpenMode.Create))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(stream))
				{
					binaryWriter.Write(buffer);
					binaryWriter.Flush();
				}
			}
			byte[] array = new byte[this.ruleIds.Count * 8];
			for (int i = 0; i < this.ruleIds.Count; i++)
			{
				Array.Copy(BitConverter.GetBytes(this.ruleIds[i]), 0, array, 8 * i, 8);
			}
			this.Message[InternalSchema.RuleIds] = array;
			this.message.Save(SaveMode.NoConflictResolution);
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x0004BC44 File Offset: 0x00049E44
		public MessageItem Message
		{
			get
			{
				this.CheckDisposed("Message::get");
				return this.message;
			}
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0004BC57 File Offset: 0x00049E57
		protected void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				StorageGlobals.TraceFailedCheckDisposed(this, methodName);
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0004BC79 File Offset: 0x00049E79
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0004BC88 File Offset: 0x00049E88
		private void Dispose(bool disposing)
		{
			StorageGlobals.TraceDispose(this, this.isDisposed, disposing);
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.InternalDispose(disposing);
			}
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0004BCAD File Offset: 0x00049EAD
		protected void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.message.Dispose();
			}
		}

		// Token: 0x040002C4 RID: 708
		private const string DamMsgClass = "IPC.Microsoft Exchange 4.0.Deferred Action";

		// Token: 0x040002C5 RID: 709
		private const int RuleIdSize = 8;

		// Token: 0x040002C6 RID: 710
		private MessageItem message;

		// Token: 0x040002C7 RID: 711
		private List<RuleAction> actions;

		// Token: 0x040002C8 RID: 712
		private List<long> ruleIds;

		// Token: 0x040002C9 RID: 713
		private bool isDisposed;
	}
}
