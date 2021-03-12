using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Diagnostics.Components.BackSync;

namespace Microsoft.Exchange.Management.BackSync.Processors
{
	// Token: 0x020000BB RID: 187
	internal class PropertyReferenceTargetMissingPropertyResolver : TargetMissingPropertyResolver<PropertyReference>
	{
		// Token: 0x060005F8 RID: 1528 RVA: 0x00019E80 File Offset: 0x00018080
		public PropertyReferenceTargetMissingPropertyResolver(IDataProcessor next, IPropertyLookup propertyReferenceTargetPropertyLookup) : base(next, propertyReferenceTargetPropertyLookup)
		{
			this.propertyReferenceProperties = new List<PropertyDefinition>();
			this.propertyReferenceProperties.Add(SyncUserSchema.CloudSiteMailboxOwners);
			this.directoryClassLookup = new Dictionary<string, DirectoryObjectClassAddressList>(StringComparer.OrdinalIgnoreCase);
			DirectoryObjectClassAddressList[] array = (DirectoryObjectClassAddressList[])Enum.GetValues(typeof(DirectoryObjectClassAddressList));
			foreach (DirectoryObjectClassAddressList directoryObjectClassAddressList in array)
			{
				this.directoryClassLookup[Enum.GetName(typeof(DirectoryObjectClassAddressList), directoryObjectClassAddressList)] = directoryObjectClassAddressList;
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00019F0A File Offset: 0x0001810A
		protected override List<PropertyDefinition> GetResolvingProperties()
		{
			return this.propertyReferenceProperties;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00019F12 File Offset: 0x00018112
		protected override ADObjectId GetTargetADObjectId(PropertyReference targetPropertyValue)
		{
			return targetPropertyValue.TargetADObjectId;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00019F1C File Offset: 0x0001811C
		protected override bool UpdateTargetMissingProperty(string sourceId, ADPropertyDefinition propertyDefinition, PropertyReference propertyReference, ADRawEntry target)
		{
			string text = (string)target[SyncObjectSchema.ObjectId];
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)target[ADObjectSchema.ObjectClass];
			DirectoryObjectClassAddressList? directoryObjectClassAddressList = null;
			foreach (string key in multiValuedProperty)
			{
				if (this.directoryClassLookup.ContainsKey(key))
				{
					directoryObjectClassAddressList = new DirectoryObjectClassAddressList?(this.directoryClassLookup[key]);
					break;
				}
			}
			if (directoryObjectClassAddressList == null)
			{
				ExTraceGlobals.BackSyncTracer.TraceError<string, string, string>((long)SyncConfiguration.TraceId, "PropertyReferenceTargetMissingPropertyResolver:: - Skipping property reference {0} -> {1}. Unsupported target class: {2}", sourceId, text, string.Join(",", multiValuedProperty.ToArray()));
				return false;
			}
			propertyReference.UpdateReferenceData(text, directoryObjectClassAddressList.Value);
			return true;
		}

		// Token: 0x040002E7 RID: 743
		private readonly Dictionary<string, DirectoryObjectClassAddressList> directoryClassLookup;

		// Token: 0x040002E8 RID: 744
		private readonly List<PropertyDefinition> propertyReferenceProperties;
	}
}
