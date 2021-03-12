using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.AnchorService.Storage;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Anchor;
using Microsoft.Exchange.MailboxLoadBalance.Providers;

namespace Microsoft.Exchange.MailboxLoadBalance.Band
{
	// Token: 0x02000017 RID: 23
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class BandSettingsStorage : DisposeTrackableBase, IBandSettingsProvider, IDisposable
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x000054C7 File Offset: 0x000036C7
		public BandSettingsStorage(IAnchorDataProvider anchorDataProvider, LoadBalanceAnchorContext anchorContext)
		{
			this.anchorDataProvider = anchorDataProvider;
			this.logger = anchorContext.Logger;
			this.loadBalanceAnchorContext = anchorContext;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000054FC File Offset: 0x000036FC
		public IEnumerable<Band> GetBandSettings()
		{
			base.CheckDisposed();
			IList<PersistedBandDefinition> list = this.GetPersistedBands().ToList<PersistedBandDefinition>();
			if (list.Count == 0)
			{
				return BandSettingsStorage.DefaultBandSettings;
			}
			return from persistedBand in list
			where persistedBand.IsEnabled
			select persistedBand.ToBand();
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005580 File Offset: 0x00003780
		public PersistedBandDefinition PersistBand(Band band, bool enabled)
		{
			base.CheckDisposed();
			PersistedBandDefinition persistedBandDefinition = new PersistedBandDefinition(band, enabled);
			List<PersistedBandDefinition> source = this.GetPersistedBands().ToList<PersistedBandDefinition>();
			if (enabled)
			{
				if (source.Any((PersistedBandDefinition pbd) => pbd.IsEnabled))
				{
					this.CheckNewBandDoesntConflict(persistedBandDefinition, from pb in source
					where pb.IsEnabled
					select pb);
				}
			}
			new AnchorXmlSerializableObject<PersistedBandDefinition>(this.loadBalanceAnchorContext)
			{
				PersistedObject = persistedBandDefinition
			}.CreateInStore(this.anchorDataProvider, null);
			return persistedBandDefinition;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005622 File Offset: 0x00003822
		public IEnumerable<PersistedBandDefinition> GetPersistedBands()
		{
			base.CheckDisposed();
			return from persistedBandDefinition in this.GetBandDefinitionXmlSerializable()
			select persistedBandDefinition.PersistedObject;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005654 File Offset: 0x00003854
		public PersistedBandDefinition DisableBand(Band band)
		{
			base.CheckDisposed();
			AnchorXmlSerializableObject<PersistedBandDefinition> bandPersistedMessage = this.GetBandPersistedMessage(band);
			bandPersistedMessage.PersistedObject.IsEnabled = false;
			this.UpdatePersistedObject(bandPersistedMessage);
			return bandPersistedMessage.PersistedObject;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005690 File Offset: 0x00003890
		public PersistedBandDefinition EnableBand(Band band)
		{
			base.CheckDisposed();
			AnchorXmlSerializableObject<PersistedBandDefinition> bandPersistedMessage = this.GetBandPersistedMessage(band);
			this.CheckNewBandDoesntConflict(bandPersistedMessage.PersistedObject, from pb in this.GetPersistedBands()
			where pb.IsEnabled
			select pb);
			bandPersistedMessage.PersistedObject.IsEnabled = true;
			this.UpdatePersistedObject(bandPersistedMessage);
			return bandPersistedMessage.PersistedObject;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000056F8 File Offset: 0x000038F8
		public PersistedBandDefinition RemoveBand(Band band)
		{
			base.CheckDisposed();
			AnchorXmlSerializableObject<PersistedBandDefinition> bandPersistedMessage = this.GetBandPersistedMessage(band);
			this.anchorDataProvider.RemoveMessage(bandPersistedMessage.StoreObjectId);
			return bandPersistedMessage.PersistedObject;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000572A File Offset: 0x0000392A
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<BandSettingsStorage>(this);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005732 File Offset: 0x00003932
		protected override void InternalDispose(bool disposing)
		{
			if (this.anchorDataProvider != null)
			{
				this.anchorDataProvider.Dispose();
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005764 File Offset: 0x00003964
		private AnchorXmlSerializableObject<PersistedBandDefinition> GetBandPersistedMessage(Band band)
		{
			AnchorXmlSerializableObject<PersistedBandDefinition> anchorXmlSerializableObject = this.GetBandDefinitionXmlSerializable().FirstOrDefault((AnchorXmlSerializableObject<PersistedBandDefinition> persisted) => persisted.PersistedObject.Matches(band));
			if (anchorXmlSerializableObject == null)
			{
				throw new BandDefinitionNotFoundException(band.ToString());
			}
			return anchorXmlSerializableObject;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000057CC File Offset: 0x000039CC
		private void CheckNewBandDoesntConflict(PersistedBandDefinition incomingBand, IEnumerable<PersistedBandDefinition> existingBands)
		{
			Band incoming = incomingBand.ToBand();
			Band band2 = (from persistedBand in existingBands
			select persistedBand.ToBand()).FirstOrDefault((Band band) => band.IsOverlap(incoming));
			if (band2 != null)
			{
				this.logger.LogError(null, "Band {0} conflicts with defined band {1}.", new object[]
				{
					incoming,
					band2
				});
				throw new OverlappingBandDefinitionException(incoming.ToString(), band2.ToString());
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005AB4 File Offset: 0x00003CB4
		private IEnumerable<AnchorXmlSerializableObject<PersistedBandDefinition>> GetBandDefinitionXmlSerializable()
		{
			QueryFilter messageType = new ComparisonFilter(ComparisonOperator.Equal, InternalSchema.ItemClass, AnchorXmlSerializableObject<PersistedBandDefinition>.GetItemClass());
			IEnumerable<StoreObjectId> bandDefinitionMessageIds = this.anchorDataProvider.FindMessageIds(messageType, BandSettingsStorage.ItemClassPropertyDefinitions, BandSettingsStorage.SortByItemClassAscending, new AnchorRowSelector(AnchorXmlSerializableObject<PersistedBandDefinition>.SelectByItemClassAndStopProcessing), null);
			foreach (StoreObjectId bandDefinitionMessageId in bandDefinitionMessageIds)
			{
				AnchorXmlSerializableObject<PersistedBandDefinition> xmlSerializable = new AnchorXmlSerializableObject<PersistedBandDefinition>(this.loadBalanceAnchorContext);
				if (!xmlSerializable.TryLoad(this.anchorDataProvider, bandDefinitionMessageId))
				{
					this.logger.LogWarning("Could not load band definition from message {0}.", new object[]
					{
						bandDefinitionMessageId.ToBase64String()
					});
				}
				else
				{
					yield return xmlSerializable;
				}
			}
			yield break;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005AD4 File Offset: 0x00003CD4
		private void UpdatePersistedObject(AnchorXmlSerializableObject<PersistedBandDefinition> persisted)
		{
			using (IAnchorStoreObject anchorStoreObject = persisted.FindStoreObject(this.anchorDataProvider))
			{
				anchorStoreObject.OpenAsReadWrite();
				persisted.WriteToMessageItem(anchorStoreObject, true);
				anchorStoreObject.Save(SaveMode.NoConflictResolution);
			}
		}

		// Token: 0x0400004F RID: 79
		private static readonly Band[] DefaultBandSettings = new Band[]
		{
			new Band(Band.BandProfile.SizeBased, ByteQuantifiedSize.FromMB(100UL), ByteQuantifiedSize.FromGB(512UL), 30.0, true, null, null)
		};

		// Token: 0x04000050 RID: 80
		private static readonly PropertyDefinition[] ItemClassPropertyDefinitions = new PropertyDefinition[]
		{
			StoreObjectSchema.ItemClass
		};

		// Token: 0x04000051 RID: 81
		private static readonly SortBy[] SortByItemClassAscending = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending)
		};

		// Token: 0x04000052 RID: 82
		private readonly IAnchorDataProvider anchorDataProvider;

		// Token: 0x04000053 RID: 83
		private readonly ILogger logger;

		// Token: 0x04000054 RID: 84
		private readonly LoadBalanceAnchorContext loadBalanceAnchorContext;
	}
}
