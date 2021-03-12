using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Management.BackSync.Processors
{
	// Token: 0x020000B4 RID: 180
	internal sealed class NeverSetAttributesFilter : PipelineProcessor
	{
		// Token: 0x060005D6 RID: 1494 RVA: 0x00019334 File Offset: 0x00017534
		public NeverSetAttributesFilter(IDataProcessor next) : base(next)
		{
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00019340 File Offset: 0x00017540
		protected override bool ProcessInternal(PropertyBag propertyBag)
		{
			MultiValuedProperty<AttributeMetadata> multiValuedProperty = (MultiValuedProperty<AttributeMetadata>)propertyBag[ADRecipientSchema.AttributeMetadata];
			if (multiValuedProperty.Count == 0 && ProcessorHelper.IsDeletedObject(propertyBag))
			{
				return true;
			}
			propertyBag.SetIsReadOnly(false);
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			foreach (AttributeMetadata attributeMetadata in multiValuedProperty)
			{
				hashSet.Add(attributeMetadata.AttributeName);
			}
			foreach (SyncPropertyDefinition property in SyncSchema.Instance.AllBackSyncBaseProperties)
			{
				NeverSetAttributesFilter.FilterProperty(propertyBag, hashSet, property);
			}
			foreach (SyncPropertyDefinition property2 in SyncSchema.Instance.AllBackSyncShadowBaseProperties)
			{
				NeverSetAttributesFilter.FilterProperty(propertyBag, hashSet, property2);
			}
			propertyBag.SetIsReadOnly(true);
			return true;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00019464 File Offset: 0x00017664
		private static void FilterProperty(PropertyBag propertyBag, HashSet<string> attributesEverSet, SyncPropertyDefinition property)
		{
			bool flag = false;
			if (property.IsCalculated || !string.IsNullOrEmpty(property.LdapDisplayName))
			{
				if (property.IsCalculated)
				{
					using (IEnumerator<ADPropertyDefinition> enumerator = property.DependentProperties.Cast<ADPropertyDefinition>().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ADPropertyDefinition adpropertyDefinition = enumerator.Current;
							if (!string.IsNullOrEmpty(property.LdapDisplayName) && attributesEverSet.Contains(adpropertyDefinition.LdapDisplayName))
							{
								flag = true;
								break;
							}
						}
						goto IL_79;
					}
				}
				flag = attributesEverSet.Contains(property.LdapDisplayName);
				IL_79:
				if (!flag)
				{
					propertyBag.Remove(property);
				}
			}
		}
	}
}
