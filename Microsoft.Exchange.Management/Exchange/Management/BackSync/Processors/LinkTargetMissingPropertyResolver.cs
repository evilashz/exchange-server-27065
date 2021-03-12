using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Diagnostics.Components.BackSync;

namespace Microsoft.Exchange.Management.BackSync.Processors
{
	// Token: 0x020000AF RID: 175
	internal class LinkTargetMissingPropertyResolver : TargetMissingPropertyResolver<SyncLink>
	{
		// Token: 0x060005BC RID: 1468 RVA: 0x00018B44 File Offset: 0x00016D44
		public LinkTargetMissingPropertyResolver(IDataProcessor next, IPropertyLookup linkTargetPropertyLookup) : base(next, linkTargetPropertyLookup)
		{
			this.linkProperties = new List<PropertyDefinition>(SyncSchema.Instance.AllBackSyncLinkedProperties.Cast<PropertyDefinition>());
			this.linkProperties.AddRange(SyncSchema.Instance.AllBackSyncShadowLinkedProperties.Cast<PropertyDefinition>());
			this.directoryClassLookup = new Dictionary<string, DirectoryObjectClass>(StringComparer.OrdinalIgnoreCase);
			DirectoryObjectClass[] array = (DirectoryObjectClass[])Enum.GetValues(typeof(DirectoryObjectClass));
			foreach (DirectoryObjectClass directoryObjectClass in array)
			{
				this.directoryClassLookup[Enum.GetName(typeof(DirectoryObjectClass), directoryObjectClass)] = directoryObjectClass;
			}
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00018BE7 File Offset: 0x00016DE7
		protected override List<PropertyDefinition> GetResolvingProperties()
		{
			return this.linkProperties;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00018BEF File Offset: 0x00016DEF
		protected override ADObjectId GetTargetADObjectId(SyncLink targetPropertyValue)
		{
			return targetPropertyValue.Link;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00018BF8 File Offset: 0x00016DF8
		protected override bool UpdateTargetMissingProperty(string sourceId, ADPropertyDefinition propertyDefinition, SyncLink link, ADRawEntry target)
		{
			string text = (string)target[SyncObjectSchema.ObjectId];
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)target[ADObjectSchema.ObjectClass];
			DirectoryObjectClass? directoryObjectClass = null;
			foreach (string key in multiValuedProperty)
			{
				if (this.directoryClassLookup.ContainsKey(key))
				{
					directoryObjectClass = new DirectoryObjectClass?(this.directoryClassLookup[key]);
					break;
				}
			}
			if (directoryObjectClass == null)
			{
				ExTraceGlobals.BackSyncTracer.TraceError<string, string, string>((long)SyncConfiguration.TraceId, "LinkTargetMissingPropertyResolver:: - Skipping link {0} -> {1}. Unsupported link target class: {2}", sourceId, text, string.Join(",", multiValuedProperty.ToArray()));
				return false;
			}
			if (propertyDefinition.LdapDisplayName == SyncGroupSchema.ManagedBy.LdapDisplayName && directoryObjectClass.Value == DirectoryObjectClass.Group)
			{
				ExTraceGlobals.BackSyncTracer.TraceError<string, string>((long)SyncConfiguration.TraceId, "LinkTargetMissingPropertyResolver:: - Skipping link {0} -> {1}. ManagedBy for groups pointing to groups need to be filtered out.", sourceId, text);
				return false;
			}
			link.UpdateSyncData(text, directoryObjectClass.Value);
			return true;
		}

		// Token: 0x040002D0 RID: 720
		private readonly Dictionary<string, DirectoryObjectClass> directoryClassLookup;

		// Token: 0x040002D1 RID: 721
		private readonly List<PropertyDefinition> linkProperties;
	}
}
