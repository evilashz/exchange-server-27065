using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Connections.Pop;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000007 RID: 7
	internal class PopSourceMailbox : PopMailbox, ISourceMailbox, IMailbox, IDisposable, ISupportMime
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00002EB4 File Offset: 0x000010B4
		public PopSourceMailbox(ConnectionParameters connectionParameters, Pop3AuthenticationParameters authenticationParameters, Pop3ServerParameters serverParameters, SmtpServerParameters smtpParameters) : base(connectionParameters, authenticationParameters, serverParameters, smtpParameters)
		{
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002EC1 File Offset: 0x000010C1
		internal override bool SupportsSavingSyncState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002EC4 File Offset: 0x000010C4
		Stream ISupportMime.GetMimeStream(MessageRec message, out PropValueData[] extraPropValues)
		{
			extraPropValues = null;
			string messageUid = PopEntryId.ParseUid(message.EntryId);
			Stream result;
			ExDateTime? exDateTime;
			this.FetchMessage(messageUid, out result, out exDateTime);
			if (exDateTime != null)
			{
				extraPropValues = new PropValueData[]
				{
					new PropValueData(PropTag.MessageDeliveryTime, exDateTime.Value.ToUtc())
				};
			}
			return result;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002F21 File Offset: 0x00001121
		byte[] ISourceMailbox.GetMailboxBasicInfo(MailboxSignatureFlags flags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002F28 File Offset: 0x00001128
		ISourceFolder ISourceMailbox.GetFolder(byte[] entryId)
		{
			MrsTracer.Provider.Function("PopSourceMailbox.GetFolder({0})", new object[]
			{
				TraceUtils.DumpEntryId(entryId)
			});
			return base.GetFolder<PopSourceFolder>(entryId);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002F5C File Offset: 0x0000115C
		void ISourceMailbox.CopyTo(IFxProxy destMailboxProxy, PropTag[] excludeTags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002F63 File Offset: 0x00001163
		void ISourceMailbox.SetMailboxSyncState(string syncStateStr)
		{
			base.SetMailboxSyncState(syncStateStr);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002F6C File Offset: 0x0000116C
		string ISourceMailbox.GetMailboxSyncState()
		{
			return base.GetMailboxSyncState();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002F90 File Offset: 0x00001190
		MailboxChangesManifest ISourceMailbox.EnumerateHierarchyChanges(EnumerateHierarchyChangesFlags flags, int maxChanges)
		{
			bool catchup = flags.HasFlag(EnumerateHierarchyChangesFlags.Catchup);
			return this.EnumerateHierarchyChanges(catchup, (SyncHierarchyManifestState hierarchyData) => this.RunManualHierarchySync(catchup, hierarchyData));
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002FDC File Offset: 0x000011DC
		void ISourceMailbox.ExportMessages(List<MessageRec> messages, IFxProxyPool proxyPool, ExportMessagesFlags flags, PropTag[] propsToCopyExplicitly, PropTag[] excludeProps)
		{
			MrsTracer.Provider.Function("PopSourceMailbox.ExportMessages({0} messages)", new object[]
			{
				messages.Count
			});
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			this.CopyMessagesOneByOne(messages, proxyPool, propsToCopyExplicitly, excludeProps, delegate(MessageRec curMsg)
			{
			});
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000303E File Offset: 0x0000123E
		void ISourceMailbox.ExportFolders(List<byte[]> folderIds, IFxProxyPool proxyPool, ExportFoldersDataToCopyFlags exportFoldersDataToCopyFlags, GetFolderRecFlags folderRecFlags, PropTag[] additionalFolderRecProps, CopyPropertiesFlags copyPropertiesFlags, PropTag[] excludeProps, AclFlags extendedAclFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003048 File Offset: 0x00001248
		List<ReplayActionResult> ISourceMailbox.ReplayActions(List<ReplayAction> actions)
		{
			MrsTracer.Provider.Function("PopSourceMailbox.ReplayActions({0} actions)", new object[]
			{
				actions.Count
			});
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			List<ReplayActionResult> list = new List<ReplayActionResult>(actions.Count);
			foreach (ReplayAction replayAction in actions)
			{
				ActionId id = replayAction.Id;
				if (id != ActionId.Send)
				{
					throw new ActionNotSupportedException();
				}
				SmtpClientHelper.Submit((SendAction)replayAction, base.SmtpParameters.Server, base.SmtpParameters.Port, base.AuthenticationParameters.NetworkCredential);
				list.Add(null);
			}
			return list;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003134 File Offset: 0x00001334
		protected override void CopySingleMessage(MessageRec message, IFolderProxy folderProxy, PropTag[] propTagsToExclude, PropTag[] excludeProps)
		{
			ExecutionContext.Create(new DataContext[]
			{
				new OperationDataContext("PopSourceMailbox.CopySingleMessage", OperationType.None),
				new EntryIDsDataContext(message.EntryId)
			}).Execute(delegate
			{
				SyncEmailUtils.CopyMimeStream(this, message, folderProxy);
			});
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000319C File Offset: 0x0000139C
		private void FetchMessage(string messageUid, out Stream mimeStream, out ExDateTime? internalDate)
		{
			base.CheckDisposed();
			internalDate = null;
			mimeStream = null;
			if (!base.UniqueIdMap.ContainsKey(messageUid))
			{
				this.UpdateMessageMap();
			}
			if (!base.UniqueIdMap.ContainsKey(messageUid))
			{
				return;
			}
			Pop3ResultData email = base.PopConnection.GetEmail(base.UniqueIdMap[messageUid]);
			if (email.Email == null || email.Email.MimeStream == null)
			{
				throw new UnableToFetchMimeStreamException(messageUid);
			}
			mimeStream = email.Email.MimeStream;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003220 File Offset: 0x00001420
		private void UpdateMessageMap()
		{
			base.UniqueIdMap.Clear();
			Pop3ResultData uniqueIds = base.PopConnection.GetUniqueIds();
			if (uniqueIds == null)
			{
				return;
			}
			for (int i = 1; i <= uniqueIds.EmailDropCount; i++)
			{
				string uniqueId = uniqueIds.GetUniqueId(i);
				if (uniqueId != null)
				{
					base.UniqueIdMap[uniqueId] = i;
				}
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003271 File Offset: 0x00001471
		ResourceHealthTracker ISupportMime.get_RHTracker()
		{
			return base.RHTracker;
		}
	}
}
