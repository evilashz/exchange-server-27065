using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AA7 RID: 2727
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class OscContactSourcesForContactUpdateRule
	{
		// Token: 0x0600637B RID: 25467 RVA: 0x001A3744 File Offset: 0x001A1944
		public OscContactSourcesForContactUpdateRule() : this(OscContactSourcesForContactParser.Instance)
		{
		}

		// Token: 0x0600637C RID: 25468 RVA: 0x001A3751 File Offset: 0x001A1951
		public OscContactSourcesForContactUpdateRule(IOscContactSourcesForContactParser parser)
		{
			this.oscParser = parser;
		}

		// Token: 0x0600637D RID: 25469 RVA: 0x001A3760 File Offset: 0x001A1960
		public bool UpdatePartnerNetworkProperties(ICorePropertyBag propertyBag)
		{
			if (!propertyBag.IsPropertyDirty(ContactSchema.OscContactSourcesForContact))
			{
				return false;
			}
			byte[] valueOrDefault = propertyBag.GetValueOrDefault<byte[]>(ContactSchema.OscContactSourcesForContact, null);
			if (valueOrDefault == null || valueOrDefault.Length == 0)
			{
				return OscContactSourcesForContactUpdateRule.UpdateDependentProperties(propertyBag, null, null);
			}
			try
			{
				return OscContactSourcesForContactUpdateRule.UpdateDependentProperties(propertyBag, valueOrDefault, this.oscParser.ReadOscContactSource(valueOrDefault));
			}
			catch (OscContactSourcesForContactParseException arg)
			{
				OscContactSourcesForContactUpdateRule.Tracer.TraceError<OscContactSourcesForContactParseException>((long)this.GetHashCode(), "Encountered exception when parsing: {0}", arg);
			}
			return false;
		}

		// Token: 0x0600637E RID: 25470 RVA: 0x001A37E0 File Offset: 0x001A19E0
		private static bool UpdateDependentProperties(ICorePropertyBag propertyBag, byte[] oscContactSources, OscNetworkProperties networkProperties)
		{
			bool flag = false;
			flag |= OscContactSourcesForContactUpdateRule.UpdatePartnerNetworkIdAndUserId(propertyBag, oscContactSources, networkProperties);
			return flag | OscContactSourcesForContactUpdateRule.CopyCloudIdFromOscContactSourcesIfCurrentCloudIdIsBlank(propertyBag, networkProperties);
		}

		// Token: 0x0600637F RID: 25471 RVA: 0x001A3808 File Offset: 0x001A1A08
		private static bool UpdatePartnerNetworkIdAndUserId(ICorePropertyBag propertyBag, byte[] oscContactSources, OscNetworkProperties networkProperties)
		{
			if (propertyBag.IsPropertyDirty(ContactSchema.PartnerNetworkId) || propertyBag.IsPropertyDirty(ContactSchema.PartnerNetworkUserId))
			{
				return false;
			}
			if (oscContactSources == null)
			{
				propertyBag.Delete(ContactSchema.PartnerNetworkId);
				propertyBag.Delete(ContactSchema.PartnerNetworkUserId);
				return true;
			}
			if (networkProperties != null)
			{
				propertyBag[ContactSchema.PartnerNetworkId] = networkProperties.NetworkId;
				propertyBag[ContactSchema.PartnerNetworkUserId] = networkProperties.NetworkUserId;
				return true;
			}
			return false;
		}

		// Token: 0x06006380 RID: 25472 RVA: 0x001A3874 File Offset: 0x001A1A74
		private static bool CopyCloudIdFromOscContactSourcesIfCurrentCloudIdIsBlank(ICorePropertyBag propertyBag, OscNetworkProperties networkProperties)
		{
			if (propertyBag.IsPropertyDirty(ItemSchema.CloudId))
			{
				return false;
			}
			if (!string.IsNullOrEmpty(propertyBag.GetValueOrDefault<string>(ItemSchema.CloudId)))
			{
				return false;
			}
			if (networkProperties != null && !string.IsNullOrEmpty(networkProperties.NetworkUserId))
			{
				propertyBag[ItemSchema.CloudId] = networkProperties.NetworkUserId;
				return true;
			}
			return false;
		}

		// Token: 0x04003836 RID: 14390
		private static readonly Trace Tracer = ExTraceGlobals.OutlookSocialConnectorInteropTracer;

		// Token: 0x04003837 RID: 14391
		private readonly IOscContactSourcesForContactParser oscParser;

		// Token: 0x04003838 RID: 14392
		public static readonly PropertyReference[] UpdateProperties = new PropertyReference[]
		{
			new PropertyReference(InternalSchema.OscContactSourcesForContact, PropertyAccess.Read),
			new PropertyReference(InternalSchema.PartnerNetworkId, PropertyAccess.ReadWrite),
			new PropertyReference(InternalSchema.PartnerNetworkUserId, PropertyAccess.ReadWrite),
			new PropertyReference(InternalSchema.CloudId, PropertyAccess.ReadWrite)
		};
	}
}
