using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.DirSync;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Management.BackSync.Processors
{
	// Token: 0x020000B2 RID: 178
	internal sealed class Metadata2LinkTranslator : PipelineProcessor
	{
		// Token: 0x060005CE RID: 1486 RVA: 0x00018FEC File Offset: 0x000171EC
		public Metadata2LinkTranslator(IDataProcessor next) : base(next)
		{
			this.InitializePropertiesToTranslate();
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00018FFC File Offset: 0x000171FC
		protected override bool ProcessInternal(PropertyBag propertyBag)
		{
			Dictionary<SyncPropertyDefinition, List<SyncLink>> dictionary = new Dictionary<SyncPropertyDefinition, List<SyncLink>>();
			MultiValuedProperty<LinkMetadata> multiValuedProperty = (MultiValuedProperty<LinkMetadata>)propertyBag[ADRecipientSchema.LinkMetadata];
			foreach (LinkMetadata linkMetadata in multiValuedProperty)
			{
				if (this.propertiesToTranslate.ContainsKey(linkMetadata.AttributeName))
				{
					SyncPropertyDefinition key = this.propertiesToTranslate[linkMetadata.AttributeName];
					if (!dictionary.ContainsKey(key))
					{
						dictionary[key] = new List<SyncLink>();
					}
					dictionary[key].Add(Metadata2LinkTranslator.ConvertLinkMetadataToSyncLink(linkMetadata));
				}
			}
			foreach (KeyValuePair<SyncPropertyDefinition, List<SyncLink>> keyValuePair in dictionary)
			{
				propertyBag.SetField(keyValuePair.Key, new MultiValuedProperty<SyncLink>(true, keyValuePair.Key, keyValuePair.Value));
			}
			return true;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00019104 File Offset: 0x00017304
		private static SyncLink ConvertLinkMetadataToSyncLink(LinkMetadata metadata)
		{
			return new SyncLink(new ADObjectId(metadata.TargetDistinguishedName), metadata.IsDeleted ? LinkState.Removed : LinkState.Added);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00019124 File Offset: 0x00017324
		private void InitializePropertiesToTranslate()
		{
			this.propertiesToTranslate = new Dictionary<string, SyncPropertyDefinition>();
			foreach (SyncPropertyDefinition syncPropertyDefinition in SyncSchema.Instance.AllBackSyncLinkedProperties)
			{
				this.propertiesToTranslate[syncPropertyDefinition.LdapDisplayName] = syncPropertyDefinition;
			}
			foreach (SyncPropertyDefinition syncPropertyDefinition2 in SyncSchema.Instance.AllBackSyncShadowLinkedProperties)
			{
				this.propertiesToTranslate[syncPropertyDefinition2.LdapDisplayName] = syncPropertyDefinition2;
			}
		}

		// Token: 0x040002D7 RID: 727
		private Dictionary<string, SyncPropertyDefinition> propertiesToTranslate;
	}
}
