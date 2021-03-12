using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003F4 RID: 1012
	[Serializable]
	public abstract class SendConnector : ADLegacyVersionableObject
	{
		// Token: 0x06002E3A RID: 11834 RVA: 0x000BC36F File Offset: 0x000BA56F
		public SendConnector()
		{
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x06002E3B RID: 11835 RVA: 0x000BC377 File Offset: 0x000BA577
		public ADObjectId SourceRoutingGroup
		{
			get
			{
				return (ADObjectId)this[SendConnectorSchema.SourceRoutingGroup];
			}
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06002E3C RID: 11836 RVA: 0x000BC389 File Offset: 0x000BA589
		// (set) Token: 0x06002E3D RID: 11837 RVA: 0x000BC39B File Offset: 0x000BA59B
		public MultiValuedProperty<ADObjectId> SourceTransportServers
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[SendConnectorSchema.SourceTransportServers];
			}
			set
			{
				this[SendConnectorSchema.SourceTransportServers] = value;
			}
		}

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06002E3E RID: 11838 RVA: 0x000BC3A9 File Offset: 0x000BA5A9
		// (set) Token: 0x06002E3F RID: 11839 RVA: 0x000BC3BB File Offset: 0x000BA5BB
		public ADObjectId HomeMTA
		{
			get
			{
				return (ADObjectId)this[SendConnectorSchema.HomeMTA];
			}
			internal set
			{
				this[SendConnectorSchema.HomeMTA] = value;
			}
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06002E40 RID: 11840 RVA: 0x000BC3C9 File Offset: 0x000BA5C9
		public ADObjectId HomeMtaServerId
		{
			get
			{
				return (ADObjectId)this[SendConnectorSchema.HomeMtaServerId];
			}
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06002E41 RID: 11841 RVA: 0x000BC3DB File Offset: 0x000BA5DB
		// (set) Token: 0x06002E42 RID: 11842 RVA: 0x000BC3ED File Offset: 0x000BA5ED
		[Parameter]
		public Unlimited<ByteQuantifiedSize> MaxMessageSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[SendConnectorSchema.MaxMessageSize];
			}
			set
			{
				this[SendConnectorSchema.MaxMessageSize] = value;
			}
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06002E43 RID: 11843 RVA: 0x000BC400 File Offset: 0x000BA600
		internal ulong AbsoluteMaxMessageSize
		{
			get
			{
				Unlimited<ByteQuantifiedSize> unlimited = (Unlimited<ByteQuantifiedSize>)this[SendConnectorSchema.MaxMessageSize];
				if (!unlimited.IsUnlimited)
				{
					return unlimited.Value.ToBytes();
				}
				return 2147483647UL;
			}
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x000BC440 File Offset: 0x000BA640
		internal static MultiValuedProperty<ADObjectId> VsiIdsToServerIds(IPropertyBag propertyBag, ADPropertyDefinition vsisProperty, ADPropertyDefinition serversProperty)
		{
			MultiValuedProperty<ADObjectId> multiValuedProperty = (MultiValuedProperty<ADObjectId>)propertyBag[vsisProperty];
			if (multiValuedProperty.Count == 0)
			{
				return new MultiValuedProperty<ADObjectId>(false, serversProperty, new ADObjectId[0]);
			}
			List<ADObjectId> list = new List<ADObjectId>(multiValuedProperty.Count);
			foreach (ADObjectId adobjectId in multiValuedProperty)
			{
				ADObjectId adobjectId2;
				try
				{
					adobjectId2 = adobjectId.DescendantDN(8);
				}
				catch (InvalidOperationException ex)
				{
					throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty(serversProperty.Name, ex.Message), serversProperty, propertyBag[ADObjectSchema.Id]), ex);
				}
				if (adobjectId2 != null)
				{
					bool flag = false;
					foreach (ADObjectId adobjectId3 in list)
					{
						if (adobjectId3.DistinguishedName.Equals(adobjectId2.DistinguishedName, StringComparison.OrdinalIgnoreCase))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						list.Add(adobjectId2);
					}
				}
			}
			return new MultiValuedProperty<ADObjectId>(false, serversProperty, list);
		}

		// Token: 0x06002E45 RID: 11845 RVA: 0x000BC56C File Offset: 0x000BA76C
		internal static void ServerIdsToVsiIds(IPropertyBag propertyBag, object serverIdsObject, ADPropertyDefinition vsisProperty, ADPropertyDefinition serversProperty)
		{
			MultiValuedProperty<ADObjectId> serverIds;
			if (serverIdsObject == null)
			{
				serverIds = new MultiValuedProperty<ADObjectId>(false, serversProperty, new ADObjectId[0]);
			}
			else
			{
				serverIds = (MultiValuedProperty<ADObjectId>)serverIdsObject;
			}
			SendConnector.ServerIdsToVsiIds(propertyBag, serverIds, vsisProperty);
		}

		// Token: 0x06002E46 RID: 11846 RVA: 0x000BC59C File Offset: 0x000BA79C
		internal static void ServerIdsToVsiIds(IPropertyBag propertyBag, MultiValuedProperty<ADObjectId> serverIds, ADPropertyDefinition vsisProperty)
		{
			if (serverIds == null || serverIds.Count == 0)
			{
				propertyBag[vsisProperty] = new MultiValuedProperty<ADObjectId>(false, vsisProperty, new ADObjectId[0]);
				return;
			}
			List<ADObjectId> list = new List<ADObjectId>(serverIds.Count);
			foreach (ADObjectId adobjectId in serverIds)
			{
				ADObjectId childId = adobjectId.GetChildId("Protocols").GetChildId("SMTP").GetChildId("1");
				list.Add(childId);
			}
			propertyBag[vsisProperty] = new MultiValuedProperty<ADObjectId>(false, vsisProperty, list);
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x000BC648 File Offset: 0x000BA848
		internal static object SourceRoutingGroupGetter(IPropertyBag propertyBag)
		{
			object result;
			try
			{
				ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
				if (adobjectId == null)
				{
					throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("SourceRoutingGroup", string.Empty), SendConnectorSchema.SourceRoutingGroup, propertyBag[ADObjectSchema.Id]), null);
				}
				result = adobjectId.DescendantDN(8);
			}
			catch (InvalidOperationException ex)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("SourceRoutingGroup", ex.Message), SendConnectorSchema.SourceRoutingGroup, propertyBag[ADObjectSchema.Id]), ex);
			}
			return result;
		}

		// Token: 0x06002E48 RID: 11848 RVA: 0x000BC6DC File Offset: 0x000BA8DC
		internal static object SourceTransportServersGetter(IPropertyBag propertyBag)
		{
			return SendConnector.VsiIdsToServerIds(propertyBag, SendConnectorSchema.SourceTransportServerVsis, SendConnectorSchema.SourceTransportServers);
		}

		// Token: 0x06002E49 RID: 11849 RVA: 0x000BC6EE File Offset: 0x000BA8EE
		internal static void SourceTransportServersSetter(object value, IPropertyBag propertyBag)
		{
			SendConnector.ServerIdsToVsiIds(propertyBag, value, SendConnectorSchema.SourceTransportServerVsis, SendConnectorSchema.SourceTransportServers);
		}

		// Token: 0x04001F18 RID: 7960
		internal const ulong UnlimitedMaxMessageSize = 2147483647UL;
	}
}
