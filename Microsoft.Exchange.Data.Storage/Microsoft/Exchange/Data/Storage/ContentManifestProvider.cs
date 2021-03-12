using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000837 RID: 2103
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ContentManifestProvider : ManifestProviderBase<MapiManifestEx, ContentSyncPhase>, IMapiManifestExCallback
	{
		// Token: 0x06004E3A RID: 20026 RVA: 0x00148261 File Offset: 0x00146461
		internal ContentManifestProvider(CoreFolder folder, ManifestConfigFlags flags, QueryFilter filter, StorageIcsState initialState, PropertyDefinition[] includeProperties) : base(folder, flags, filter, initialState, includeProperties, Array<NativeStorePropertyDefinition>.Empty)
		{
		}

		// Token: 0x06004E3B RID: 20027 RVA: 0x00148275 File Offset: 0x00146475
		protected override bool IsValidTransition(ContentSyncPhase oldPhase, ContentSyncPhase newPhase)
		{
			return oldPhase <= newPhase;
		}

		// Token: 0x06004E3C RID: 20028 RVA: 0x00148280 File Offset: 0x00146480
		protected override MapiManifestEx MapiCreateManifest(ManifestConfigFlags flags, Restriction restriction, StorageIcsState initialState, ICollection<PropTag> includePropertyTags, ICollection<PropTag> excludePropertyTags)
		{
			return base.MapiFolder.CreateExportManifestEx(ContentManifestProvider.ConvertManifestConfigFlags(flags), restriction, initialState.StateIdsetGiven, initialState.StateCnsetSeen, initialState.StateCnsetSeenFAI, initialState.StateCnsetRead, this, includePropertyTags);
		}

		// Token: 0x06004E3D RID: 20029 RVA: 0x001482C0 File Offset: 0x001464C0
		protected override void MapiGetFinalState(ref StorageIcsState finalState)
		{
			byte[] stateIdsetGiven;
			byte[] stateCnsetSeen;
			byte[] stateCnsetSeenFAI;
			byte[] stateCnsetRead;
			base.MapiManifest.GetState(out stateIdsetGiven, out stateCnsetSeen, out stateCnsetSeenFAI, out stateCnsetRead);
			finalState.StateIdsetGiven = stateIdsetGiven;
			finalState.StateCnsetSeen = stateCnsetSeen;
			finalState.StateCnsetSeenFAI = stateCnsetSeenFAI;
			finalState.StateCnsetRead = stateCnsetRead;
		}

		// Token: 0x06004E3E RID: 20030 RVA: 0x001482FC File Offset: 0x001464FC
		protected override ManifestStatus MapiSynchronize()
		{
			return base.MapiManifest.Synchronize();
		}

		// Token: 0x06004E3F RID: 20031 RVA: 0x00148309 File Offset: 0x00146509
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ContentManifestProvider>(this);
		}

		// Token: 0x06004E40 RID: 20032 RVA: 0x00148311 File Offset: 0x00146511
		public bool TryGetNextChange(out ManifestItemChange change)
		{
			this.CheckDisposed(null);
			return base.TryGetChange<ManifestItemChange>(ContentSyncPhase.Change, out change);
		}

		// Token: 0x06004E41 RID: 20033 RVA: 0x00148322 File Offset: 0x00146522
		public bool TryGetDeletion(out ManifestItemDeletion deletion)
		{
			this.CheckDisposed(null);
			return base.TryGetChange<ManifestItemDeletion>(ContentSyncPhase.Delete, out deletion);
		}

		// Token: 0x06004E42 RID: 20034 RVA: 0x00148333 File Offset: 0x00146533
		public bool TryGetReadUnread(out ManifestItemReadUnread readUnread)
		{
			this.CheckDisposed(null);
			return base.TryGetChange<ManifestItemReadUnread>(ContentSyncPhase.ReadUnread, out readUnread);
		}

		// Token: 0x06004E43 RID: 20035 RVA: 0x00148344 File Offset: 0x00146544
		ManifestCallbackStatus IMapiManifestExCallback.Change(bool newMessage, PropValue[] headerPropertyValues, PropValue[] propertyValues)
		{
			if (headerPropertyValues == null || headerPropertyValues.Length == 0)
			{
				throw new ArgumentNullException("headerPropertyValues");
			}
			Util.ThrowOnNullArgument(propertyValues, "propertyValues");
			base.SetChange(ContentSyncPhase.Change, new ManifestItemChange(newMessage, base.FromMapiPropValueToXsoPropValue(headerPropertyValues), base.FromMapiPropValueToXsoPropValue(propertyValues)));
			return ManifestCallbackStatus.Yield;
		}

		// Token: 0x06004E44 RID: 20036 RVA: 0x00148380 File Offset: 0x00146580
		ManifestCallbackStatus IMapiManifestExCallback.Delete(byte[] idsetDeleted, bool softDeleted, bool expired)
		{
			Util.ThrowOnNullArgument(idsetDeleted, "idsetDeleted");
			base.SetChange(ContentSyncPhase.Delete, new ManifestItemDeletion(idsetDeleted, softDeleted, expired));
			return ManifestCallbackStatus.Yield;
		}

		// Token: 0x06004E45 RID: 20037 RVA: 0x0014839D File Offset: 0x0014659D
		ManifestCallbackStatus IMapiManifestExCallback.ReadUnread(byte[] idsetReadUnread, bool read)
		{
			Util.ThrowOnNullArgument(idsetReadUnread, "idsetReadUnread");
			base.SetChange(ContentSyncPhase.ReadUnread, new ManifestItemReadUnread(idsetReadUnread, read));
			return ManifestCallbackStatus.Yield;
		}

		// Token: 0x06004E46 RID: 20038 RVA: 0x001483BC File Offset: 0x001465BC
		private static SyncConfigFlags ConvertManifestConfigFlags(ManifestConfigFlags syncFlag)
		{
			SyncConfigFlags result = SyncConfigFlags.None;
			EnumValidator.ThrowIfInvalid<ManifestConfigFlags>(syncFlag);
			ManifestProviderBase<MapiManifestEx, ContentSyncPhase>.TranslateFlag(ManifestConfigFlags.NoDeletions, SyncConfigFlags.NoDeletions, syncFlag, ref result);
			ManifestProviderBase<MapiManifestEx, ContentSyncPhase>.TranslateFlag(ManifestConfigFlags.NoSoftDeletions, SyncConfigFlags.NoSoftDeletions, syncFlag, ref result);
			ManifestProviderBase<MapiManifestEx, ContentSyncPhase>.TranslateFlag(ManifestConfigFlags.ReadState, SyncConfigFlags.ReadState, syncFlag, ref result);
			ManifestProviderBase<MapiManifestEx, ContentSyncPhase>.TranslateFlag(ManifestConfigFlags.Associated, SyncConfigFlags.Associated, syncFlag, ref result);
			ManifestProviderBase<MapiManifestEx, ContentSyncPhase>.TranslateFlag(ManifestConfigFlags.Normal, SyncConfigFlags.Normal, syncFlag, ref result);
			ManifestProviderBase<MapiManifestEx, ContentSyncPhase>.TranslateFlag(ManifestConfigFlags.Catchup, SyncConfigFlags.Catchup, syncFlag, ref result);
			ManifestProviderBase<MapiManifestEx, ContentSyncPhase>.TranslateFlag(ManifestConfigFlags.NoChanges, SyncConfigFlags.NoChanges, syncFlag, ref result);
			ManifestProviderBase<MapiManifestEx, ContentSyncPhase>.TranslateFlag(ManifestConfigFlags.OrderByDeliveryTime, SyncConfigFlags.OrderByDeliveryTime, syncFlag, ref result);
			return result;
		}
	}
}
