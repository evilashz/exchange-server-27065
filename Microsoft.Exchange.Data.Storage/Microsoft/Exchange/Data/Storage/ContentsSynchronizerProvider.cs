using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000838 RID: 2104
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ContentsSynchronizerProvider : SynchronizerProviderBase
	{
		// Token: 0x06004E47 RID: 20039 RVA: 0x0014843C File Offset: 0x0014663C
		public ContentsSynchronizerProvider(CoreFolder folder, SynchronizerConfigFlags flags, QueryFilter filter, StorageIcsState initialState, PropertyDefinition[] includeProperties, PropertyDefinition[] excludeProperties, short[] unspecifiedIncludeProperties, short[] unspecifiedExcludeProperties, int bufferSize) : base(folder, flags, filter, initialState, includeProperties, excludeProperties, unspecifiedIncludeProperties, unspecifiedExcludeProperties, bufferSize)
		{
		}

		// Token: 0x06004E48 RID: 20040 RVA: 0x00148460 File Offset: 0x00146660
		public ContentsSynchronizerProvider(CoreFolder folder, SynchronizerConfigFlags flags, QueryFilter filter, StorageIcsState initialState, PropertyDefinition[] includeProperties, PropertyDefinition[] excludeProperties, int bufferSize) : base(folder, flags, filter, initialState, includeProperties, excludeProperties, null, null, bufferSize)
		{
		}

		// Token: 0x06004E49 RID: 20041 RVA: 0x00148480 File Offset: 0x00146680
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				Util.DisposeIfPresent(this.synchronizer);
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x06004E4A RID: 20042 RVA: 0x00148497 File Offset: 0x00146697
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ContentsSynchronizerProvider>(this);
		}

		// Token: 0x06004E4B RID: 20043 RVA: 0x001484A0 File Offset: 0x001466A0
		protected override void MapiCreateSynchronizer(SynchronizerConfigFlags flags, Restriction restriction, StorageIcsState initialState, ICollection<PropTag> includePropertyTags, ICollection<PropTag> excludePropertyTags, int fastTransferBlockSize)
		{
			this.synchronizer = base.MapiFolder.CreateSynchronizerEx(initialState.StateIdsetGiven, initialState.StateCnsetSeen, initialState.StateCnsetSeenFAI, initialState.StateCnsetRead, SynchronizerProviderBase.ConvertSynchronizerConfigFlags(flags), restriction, includePropertyTags, excludePropertyTags, fastTransferBlockSize);
		}

		// Token: 0x06004E4C RID: 20044 RVA: 0x001484E8 File Offset: 0x001466E8
		protected override FastTransferBlock MapiGetBuffer(out int residualCacheSize, out bool doneInCache)
		{
			FastTransferBlock buffer = this.synchronizer.GetBuffer(out residualCacheSize, out doneInCache);
			if (buffer.Progress > 0U && !this.reportedMessagesWereDownloaded)
			{
				base.CoreFolder.Session.MessagesWereDownloaded = true;
				this.reportedMessagesWereDownloaded = true;
			}
			return buffer;
		}

		// Token: 0x06004E4D RID: 20045 RVA: 0x00148530 File Offset: 0x00146730
		protected override void MapiGetFinalState(ref StorageIcsState finalState)
		{
			byte[] stateIdsetGiven;
			byte[] stateCnsetSeen;
			byte[] stateCnsetSeenFAI;
			byte[] stateCnsetRead;
			this.synchronizer.GetState(out stateIdsetGiven, out stateCnsetSeen, out stateCnsetSeenFAI, out stateCnsetRead);
			finalState.StateIdsetGiven = stateIdsetGiven;
			finalState.StateCnsetSeen = stateCnsetSeen;
			finalState.StateCnsetSeenFAI = stateCnsetSeenFAI;
			finalState.StateCnsetRead = stateCnsetRead;
		}

		// Token: 0x04002ABB RID: 10939
		private MapiSynchronizerEx synchronizer;

		// Token: 0x04002ABC RID: 10940
		private bool reportedMessagesWereDownloaded;
	}
}
