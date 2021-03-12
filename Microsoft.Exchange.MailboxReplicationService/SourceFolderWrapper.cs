using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000AA RID: 170
	internal class SourceFolderWrapper : FolderWrapper, ISourceFolder, IFolder, IDisposable
	{
		// Token: 0x060008BD RID: 2237 RVA: 0x0003B613 File Offset: 0x00039813
		public SourceFolderWrapper(ISourceFolder folder, CommonUtils.CreateContextDelegate createContext, ProviderInfo mailboxProviderInfo) : base(folder, createContext)
		{
			base.ProviderInfo = mailboxProviderInfo;
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0003B6AC File Offset: 0x000398AC
		void ISourceFolder.CopyTo(IFxProxy destFolder, CopyPropertiesFlags flags, PropTag[] excludeTags)
		{
			string text = "ISourceFolder.CopyTo";
			TimeSpan targetDuration = TimeSpan.Zero;
			Stopwatch stopwatch = Stopwatch.StartNew();
			base.CreateContext(text, new DataContext[]
			{
				new PropTagsDataContext(excludeTags)
			}).Execute(delegate
			{
				using (FxProxyCallbackWrapper fxProxyCallbackWrapper = new FxProxyCallbackWrapper(destFolder, true, delegate(TimeSpan duration)
				{
					targetDuration += duration;
				}))
				{
					((ISourceFolder)this.WrappedObject).CopyTo(fxProxyCallbackWrapper, flags, excludeTags);
				}
			}, false);
			base.UpdateDuration(text, stopwatch.Elapsed.Subtract(targetDuration));
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0003B774 File Offset: 0x00039974
		void ISourceFolder.ExportMessages(IFxProxy destFolderProxy, CopyMessagesFlags flags, byte[][] entryIds)
		{
			base.CreateContext("ISourceFolder.ExportMessages", new DataContext[]
			{
				new SimpleValueDataContext("Flags", flags),
				new EntryIDsDataContext(entryIds)
			}).Execute(delegate
			{
				((ISourceFolder)this.WrappedObject).ExportMessages(destFolderProxy, flags, entryIds);
			}, true);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0003B824 File Offset: 0x00039A24
		FolderChangesManifest ISourceFolder.EnumerateChanges(EnumerateContentChangesFlags flags, int maxChanges)
		{
			FolderChangesManifest result = null;
			base.CreateContext("ISourceFolder.EnumerateChanges", new DataContext[]
			{
				new SimpleValueDataContext("flags", flags),
				new SimpleValueDataContext("maxChanges", maxChanges)
			}).Execute(delegate
			{
				result = ((ISourceFolder)this.WrappedObject).EnumerateChanges(flags, maxChanges);
			}, true);
			return result;
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0003B8E0 File Offset: 0x00039AE0
		List<MessageRec> ISourceFolder.EnumerateMessagesPaged(int maxPageSize)
		{
			List<MessageRec> result = null;
			base.CreateContext("ISourceFolder.EnumerateMessagesPaged", new DataContext[]
			{
				new SimpleValueDataContext("maxPageSize", maxPageSize)
			}).Execute(delegate
			{
				result = ((ISourceFolder)this.WrappedObject).EnumerateMessagesPaged(maxPageSize);
			}, true);
			return result;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0003B978 File Offset: 0x00039B78
		int ISourceFolder.GetEstimatedItemCount()
		{
			int result = 0;
			base.CreateContext("ISourceFolder.GetEstimatedItemCount", new DataContext[0]).Execute(delegate
			{
				result = ((ISourceFolder)this.WrappedObject).GetEstimatedItemCount();
			}, true);
			return result;
		}
	}
}
