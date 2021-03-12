using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Data.Common;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000AB RID: 171
	internal class SourceMailboxWrapper : MailboxWrapper, ISourceMailbox, IMailbox, IDisposable
	{
		// Token: 0x060008C3 RID: 2243 RVA: 0x0003B9C7 File Offset: 0x00039BC7
		public SourceMailboxWrapper(IMailbox mailbox, MailboxWrapperFlags flags, LocalizedString tracingId) : base(mailbox, flags, tracingId)
		{
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x0003B9D2 File Offset: 0x00039BD2
		public ISourceMailbox SourceMailbox
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x0003B9D5 File Offset: 0x00039BD5
		protected override OperationSideDataContext SideOperationContext
		{
			get
			{
				return OperationSideDataContext.Source;
			}
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0003B9DC File Offset: 0x00039BDC
		public override IFolder GetFolder(byte[] folderId)
		{
			return this.SourceMailbox.GetFolder(folderId);
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0003BA18 File Offset: 0x00039C18
		byte[] ISourceMailbox.GetMailboxBasicInfo(MailboxSignatureFlags flags)
		{
			byte[] result = null;
			base.CreateContext("ISourceMailbox.GetMailboxBasicInfo", new DataContext[]
			{
				new SimpleValueDataContext("Flags", flags)
			}).Execute(delegate
			{
				result = ((ISourceMailbox)this.WrappedObject).GetMailboxBasicInfo(flags);
			}, true);
			return result;
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0003BAB4 File Offset: 0x00039CB4
		ISourceFolder ISourceMailbox.GetFolder(byte[] entryId)
		{
			ISourceFolder result = null;
			base.CreateContext("ISourceMailbox.GetFolder", new DataContext[]
			{
				new EntryIDsDataContext(entryId)
			}).Execute(delegate
			{
				result = ((ISourceMailbox)this.WrappedObject).GetFolder(entryId);
			}, true);
			if (result == null)
			{
				return null;
			}
			return new SourceFolderWrapper(result, base.CreateContext, base.ProviderInfo);
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0003BB60 File Offset: 0x00039D60
		void ISourceMailbox.CopyTo(IFxProxy destMailbox, PropTag[] excludeProps)
		{
			base.CreateContext("ISourceMailbox.CopyTo", new DataContext[]
			{
				new PropTagsDataContext(excludeProps)
			}).Execute(delegate
			{
				((ISourceMailbox)this.WrappedObject).CopyTo(destMailbox, excludeProps);
			}, true);
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0003BBE8 File Offset: 0x00039DE8
		void ISourceMailbox.SetMailboxSyncState(string syncStateStr)
		{
			base.CreateContext("ISourceMailbox.SetMailboxSyncState", new DataContext[]
			{
				new SimpleValueDataContext("StateLen", (syncStateStr != null) ? syncStateStr.Length : 0)
			}).Execute(delegate
			{
				((ISourceMailbox)this.WrappedObject).SetMailboxSyncState(syncStateStr);
			}, true);
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0003BC80 File Offset: 0x00039E80
		string ISourceMailbox.GetMailboxSyncState()
		{
			string result = null;
			base.CreateContext("ISourceMailbox.GetMailboxSyncState", new DataContext[0]).Execute(delegate
			{
				result = ((ISourceMailbox)this.WrappedObject).GetMailboxSyncState();
			}, true);
			return result;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0003BD00 File Offset: 0x00039F00
		MailboxChangesManifest ISourceMailbox.EnumerateHierarchyChanges(EnumerateHierarchyChangesFlags flags, int maxChanges)
		{
			MailboxChangesManifest result = null;
			base.CreateContext("ISourceMailbox.EnumerateHierarchyChanges", new DataContext[]
			{
				new SimpleValueDataContext("flags", flags),
				new SimpleValueDataContext("maxChanges", maxChanges)
			}).Execute(delegate
			{
				result = ((ISourceMailbox)this.WrappedObject).EnumerateHierarchyChanges(flags, maxChanges);
			}, true);
			return result;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0003BE24 File Offset: 0x0003A024
		void ISourceMailbox.ExportMessages(List<MessageRec> messages, IFxProxyPool destProxies, ExportMessagesFlags flags, PropTag[] propsToCopyExplicitly, PropTag[] excludeProps)
		{
			string text = "ISourceMailbox.ExportMessages";
			TimeSpan targetDuration = TimeSpan.Zero;
			Stopwatch stopwatch = Stopwatch.StartNew();
			base.CreateContext(text, new DataContext[]
			{
				new SimpleValueDataContext("Flags", flags),
				new PropTagsDataContext(excludeProps)
			}).Execute(delegate
			{
				using (FxProxyPoolFxCallbackWrapper fxProxyPoolFxCallbackWrapper = new FxProxyPoolFxCallbackWrapper(destProxies, true, delegate(TimeSpan duration)
				{
					targetDuration += duration;
				}))
				{
					((ISourceMailbox)this.WrappedObject).ExportMessages(messages, fxProxyPoolFxCallbackWrapper, flags, propsToCopyExplicitly, excludeProps);
				}
			}, false);
			base.UpdateDuration(text, stopwatch.Elapsed.Subtract(targetDuration));
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0003BF8C File Offset: 0x0003A18C
		void ISourceMailbox.ExportFolders(List<byte[]> folderIds, IFxProxyPool proxyPool, ExportFoldersDataToCopyFlags exportFoldersDataToCopyFlags, GetFolderRecFlags folderRecFlags, PropTag[] additionalFolderRecProps, CopyPropertiesFlags copyPropertiesFlags, PropTag[] excludeProps, AclFlags extendedAclFlags)
		{
			string text = "ISourceMailbox.ExportFolders";
			TimeSpan targetDuration = TimeSpan.Zero;
			Stopwatch stopwatch = Stopwatch.StartNew();
			base.CreateContext(text, new DataContext[]
			{
				new EntryIDsDataContext(folderIds),
				new SimpleValueDataContext("exportFoldersDataToCopyFlags", exportFoldersDataToCopyFlags),
				new SimpleValueDataContext("folderRecFlags", folderRecFlags),
				new PropTagsDataContext(additionalFolderRecProps),
				new SimpleValueDataContext("copyPropertiesFlags", copyPropertiesFlags),
				new PropTagsDataContext(excludeProps),
				new SimpleValueDataContext("extendedAclFlags", extendedAclFlags)
			}).Execute(delegate
			{
				using (FxProxyPoolFxCallbackWrapper fxProxyPoolFxCallbackWrapper = new FxProxyPoolFxCallbackWrapper(proxyPool, true, delegate(TimeSpan duration)
				{
					targetDuration += duration;
				}))
				{
					((ISourceMailbox)this.WrappedObject).ExportFolders(folderIds, fxProxyPoolFxCallbackWrapper, exportFoldersDataToCopyFlags, folderRecFlags, additionalFolderRecProps, copyPropertiesFlags, excludeProps, extendedAclFlags);
				}
			}, false);
			base.UpdateDuration(text, stopwatch.Elapsed.Subtract(targetDuration));
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0003C0F4 File Offset: 0x0003A2F4
		List<ReplayActionResult> ISourceMailbox.ReplayActions(List<ReplayAction> actions)
		{
			List<ReplayActionResult> result = null;
			base.CreateContext("ISourceMailbox.ReplayActions", new DataContext[0]).Execute(delegate
			{
				result = ((ISourceMailbox)this.WrappedObject).ReplayActions(actions);
			}, true);
			return result;
		}
	}
}
