using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200083B RID: 2107
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class HierarchySynchronizerProvider : SynchronizerProviderBase
	{
		// Token: 0x06004E5A RID: 20058 RVA: 0x001486D8 File Offset: 0x001468D8
		public HierarchySynchronizerProvider(CoreFolder folder, SynchronizerConfigFlags flags, QueryFilter filter, StorageIcsState initialState, PropertyDefinition[] includeProperties, PropertyDefinition[] excludeProperties, short[] unspecifiedIncludeProperties, short[] unspecifiedExcludeProperties, int bufferSize) : base(folder, flags, filter, initialState, includeProperties, excludeProperties, unspecifiedIncludeProperties, unspecifiedExcludeProperties, bufferSize)
		{
		}

		// Token: 0x06004E5B RID: 20059 RVA: 0x001486FC File Offset: 0x001468FC
		public HierarchySynchronizerProvider(CoreFolder folder, SynchronizerConfigFlags flags, QueryFilter filter, StorageIcsState initialState, PropertyDefinition[] includeProperties, PropertyDefinition[] excludeProperties, int bufferSize) : base(folder, flags, filter, initialState, includeProperties, excludeProperties, null, null, bufferSize)
		{
		}

		// Token: 0x06004E5C RID: 20060 RVA: 0x0014871C File Offset: 0x0014691C
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				Util.DisposeIfPresent(this.synchronizer);
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x06004E5D RID: 20061 RVA: 0x00148733 File Offset: 0x00146933
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<HierarchySynchronizerProvider>(this);
		}

		// Token: 0x06004E5E RID: 20062 RVA: 0x0014873C File Offset: 0x0014693C
		protected override void MapiCreateSynchronizer(SynchronizerConfigFlags flags, Restriction restriction, StorageIcsState initialState, ICollection<PropTag> includePropertyTags, ICollection<PropTag> excludePropertyTags, int fastTransferBlockSize)
		{
			this.synchronizer = base.MapiFolder.CreateHierarchySynchronizerEx(initialState.StateIdsetGiven, initialState.StateCnsetSeen, SynchronizerProviderBase.ConvertSynchronizerConfigFlags(flags), restriction, includePropertyTags, excludePropertyTags, fastTransferBlockSize);
		}

		// Token: 0x06004E5F RID: 20063 RVA: 0x00148775 File Offset: 0x00146975
		protected override FastTransferBlock MapiGetBuffer(out int residualCacheSize, out bool doneInCache)
		{
			return this.synchronizer.GetBuffer(out residualCacheSize, out doneInCache);
		}

		// Token: 0x06004E60 RID: 20064 RVA: 0x00148784 File Offset: 0x00146984
		protected override void MapiGetFinalState(ref StorageIcsState finalState)
		{
			byte[] stateIdsetGiven;
			byte[] stateCnsetSeen;
			this.synchronizer.GetState(out stateIdsetGiven, out stateCnsetSeen);
			finalState.StateIdsetGiven = stateIdsetGiven;
			finalState.StateCnsetSeen = stateCnsetSeen;
		}

		// Token: 0x04002AC2 RID: 10946
		private MapiHierarchySynchronizerEx synchronizer;
	}
}
