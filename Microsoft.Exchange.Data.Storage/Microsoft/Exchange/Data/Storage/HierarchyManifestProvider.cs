using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200083A RID: 2106
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class HierarchyManifestProvider : ManifestProviderBase<MapiHierarchyManifestEx, HierarchySyncPhase>, IMapiHierarchyManifestCallback
	{
		// Token: 0x06004E4E RID: 20046 RVA: 0x0014856C File Offset: 0x0014676C
		internal HierarchyManifestProvider(CoreFolder folder, ManifestConfigFlags flags, StorageIcsState initialState, PropertyDefinition[] includeProperties, PropertyDefinition[] excludeProperties) : base(folder, flags, null, initialState, includeProperties, excludeProperties)
		{
		}

		// Token: 0x06004E4F RID: 20047 RVA: 0x0014857C File Offset: 0x0014677C
		protected override bool IsValidTransition(HierarchySyncPhase oldPhase, HierarchySyncPhase newPhase)
		{
			return (oldPhase == HierarchySyncPhase.None && newPhase != HierarchySyncPhase.None) || (oldPhase == HierarchySyncPhase.Change && (newPhase == HierarchySyncPhase.Change || newPhase == HierarchySyncPhase.Delete));
		}

		// Token: 0x06004E50 RID: 20048 RVA: 0x00148596 File Offset: 0x00146796
		protected override MapiHierarchyManifestEx MapiCreateManifest(ManifestConfigFlags flags, Restriction restriction, StorageIcsState initialState, ICollection<PropTag> includePropertyTags, ICollection<PropTag> excludePropertyTags)
		{
			return base.MapiFolder.CreateExportHierarchyManifestEx(HierarchyManifestProvider.ConvertManifestConfigFlags(flags), initialState.StateIdsetGiven, initialState.StateCnsetSeen, this, includePropertyTags, excludePropertyTags);
		}

		// Token: 0x06004E51 RID: 20049 RVA: 0x001485BC File Offset: 0x001467BC
		protected override void MapiGetFinalState(ref StorageIcsState finalState)
		{
			byte[] stateIdsetGiven;
			byte[] stateCnsetSeen;
			base.MapiManifest.GetState(out stateIdsetGiven, out stateCnsetSeen);
			finalState.StateIdsetGiven = stateIdsetGiven;
			finalState.StateCnsetSeen = stateCnsetSeen;
		}

		// Token: 0x06004E52 RID: 20050 RVA: 0x001485E6 File Offset: 0x001467E6
		protected override ManifestStatus MapiSynchronize()
		{
			return base.MapiManifest.Synchronize();
		}

		// Token: 0x06004E53 RID: 20051 RVA: 0x001485F3 File Offset: 0x001467F3
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<HierarchyManifestProvider>(this);
		}

		// Token: 0x06004E54 RID: 20052 RVA: 0x001485FB File Offset: 0x001467FB
		public bool TryGetNextChange(out ManifestFolderChange change)
		{
			this.CheckDisposed(null);
			return base.TryGetChange<ManifestFolderChange>(HierarchySyncPhase.Change, out change);
		}

		// Token: 0x06004E55 RID: 20053 RVA: 0x0014860C File Offset: 0x0014680C
		public bool TryGetDeletion(out ManifestFolderDeletion deletion)
		{
			this.CheckDisposed(null);
			return base.TryGetChange<ManifestFolderDeletion>(HierarchySyncPhase.Delete, out deletion);
		}

		// Token: 0x06004E56 RID: 20054 RVA: 0x0014861D File Offset: 0x0014681D
		ManifestCallbackStatus IMapiHierarchyManifestCallback.Change(PropValue[] propertyValues)
		{
			if (propertyValues == null || propertyValues.Length == 0)
			{
				throw new ArgumentNullException("propertyValues");
			}
			base.SetChange(HierarchySyncPhase.Change, new ManifestFolderChange(base.FromMapiPropValueToXsoPropValue(propertyValues)));
			return ManifestCallbackStatus.Yield;
		}

		// Token: 0x06004E57 RID: 20055 RVA: 0x00148646 File Offset: 0x00146846
		ManifestCallbackStatus IMapiHierarchyManifestCallback.Delete(byte[] idsetDeleted)
		{
			Util.ThrowOnNullArgument(idsetDeleted, "idsetDeleted");
			base.SetChange(HierarchySyncPhase.Delete, new ManifestFolderDeletion(idsetDeleted));
			return ManifestCallbackStatus.Yield;
		}

		// Token: 0x06004E58 RID: 20056 RVA: 0x00148664 File Offset: 0x00146864
		private static SyncConfigFlags ConvertManifestConfigFlags(ManifestConfigFlags syncFlag)
		{
			EnumValidator.ThrowIfInvalid<ManifestConfigFlags>(syncFlag, HierarchyManifestProvider.validConvertOptions);
			SyncConfigFlags result = SyncConfigFlags.None;
			ManifestProviderBase<MapiHierarchyManifestEx, HierarchySyncPhase>.TranslateFlag(ManifestConfigFlags.NoDeletions, SyncConfigFlags.NoDeletions, syncFlag, ref result);
			ManifestProviderBase<MapiHierarchyManifestEx, HierarchySyncPhase>.TranslateFlag(ManifestConfigFlags.Catchup, SyncConfigFlags.Catchup, syncFlag, ref result);
			ManifestProviderBase<MapiHierarchyManifestEx, HierarchySyncPhase>.TranslateFlag(ManifestConfigFlags.NoChanges, SyncConfigFlags.NoChanges, syncFlag, ref result);
			return result;
		}

		// Token: 0x04002AC1 RID: 10945
		private static ManifestConfigFlags[] validConvertOptions = new ManifestConfigFlags[]
		{
			ManifestConfigFlags.None,
			ManifestConfigFlags.NoDeletions,
			ManifestConfigFlags.Catchup,
			ManifestConfigFlags.NoChanges
		};
	}
}
