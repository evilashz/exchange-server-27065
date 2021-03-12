using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.DirSync;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Management.BackSync.Processors
{
	// Token: 0x020000B1 RID: 177
	internal class LinkTargetPropertyLookup : PropertyCache, IPropertyLookup
	{
		// Token: 0x060005CB RID: 1483 RVA: 0x00018E40 File Offset: 0x00017040
		public LinkTargetPropertyLookup(Func<ADObjectId[], PropertyDefinition[], Result<ADRawEntry>[]> getProperties, IDictionary<ADObjectId, ADRawEntry> propertyCache) : base(getProperties, SyncObject.BackSyncProperties.Cast<PropertyDefinition>().ToArray<PropertyDefinition>(), propertyCache)
		{
			this.linkProperties = new List<PropertyDefinition>(SyncSchema.Instance.AllBackSyncLinkedProperties.Cast<PropertyDefinition>());
			this.linkProperties.AddRange(SyncSchema.Instance.AllBackSyncShadowLinkedProperties.Cast<PropertyDefinition>());
			this.linkProperties.Add(SyncUserSchema.CloudSiteMailboxOwners);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00018EA8 File Offset: 0x000170A8
		public LinkTargetPropertyLookup(Func<ADObjectId[], PropertyDefinition[], Result<ADRawEntry>[]> getProperties) : this(getProperties, new Dictionary<ADObjectId, ADRawEntry>())
		{
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00018EB8 File Offset: 0x000170B8
		public override IEnumerable<ADObjectId> GetObjectIds(PropertyBag propertyBag)
		{
			HashSet<ADObjectId> hashSet = new HashSet<ADObjectId>();
			IDictionary<ADPropertyDefinition, object> changedProperties = ADDirSyncHelper.GetChangedProperties(this.linkProperties, propertyBag);
			foreach (KeyValuePair<ADPropertyDefinition, object> keyValuePair in changedProperties)
			{
				if (keyValuePair.Value != null)
				{
					if (keyValuePair.Key.Type == typeof(PropertyReference))
					{
						MultiValuedProperty<PropertyReference> multiValuedProperty = (MultiValuedProperty<PropertyReference>)keyValuePair.Value;
						using (MultiValuedProperty<PropertyReference>.Enumerator enumerator2 = multiValuedProperty.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								PropertyReference propertyReference = enumerator2.Current;
								hashSet.Add(propertyReference.TargetADObjectId);
							}
							continue;
						}
					}
					MultiValuedProperty<SyncLink> multiValuedProperty2 = (MultiValuedProperty<SyncLink>)keyValuePair.Value;
					foreach (SyncLink syncLink in multiValuedProperty2)
					{
						hashSet.Add(syncLink.Link);
					}
				}
			}
			return hashSet;
		}

		// Token: 0x040002D6 RID: 726
		private readonly List<PropertyDefinition> linkProperties;
	}
}
