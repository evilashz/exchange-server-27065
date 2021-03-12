using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200056C RID: 1388
	[Serializable]
	public class RoutingGroupConnector : SendConnector
	{
		// Token: 0x170013FC RID: 5116
		// (get) Token: 0x06003E38 RID: 15928 RVA: 0x000EC323 File Offset: 0x000EA523
		// (set) Token: 0x06003E39 RID: 15929 RVA: 0x000EC335 File Offset: 0x000EA535
		public ADObjectId TargetRoutingGroup
		{
			get
			{
				return (ADObjectId)this[RoutingGroupConnectorSchema.TargetRoutingGroup];
			}
			internal set
			{
				this[RoutingGroupConnectorSchema.TargetRoutingGroup] = value;
			}
		}

		// Token: 0x170013FD RID: 5117
		// (get) Token: 0x06003E3A RID: 15930 RVA: 0x000EC343 File Offset: 0x000EA543
		// (set) Token: 0x06003E3B RID: 15931 RVA: 0x000EC355 File Offset: 0x000EA555
		[Parameter]
		public int Cost
		{
			get
			{
				return (int)this[RoutingGroupConnectorSchema.Cost];
			}
			set
			{
				this[RoutingGroupConnectorSchema.Cost] = value;
			}
		}

		// Token: 0x170013FE RID: 5118
		// (get) Token: 0x06003E3C RID: 15932 RVA: 0x000EC368 File Offset: 0x000EA568
		// (set) Token: 0x06003E3D RID: 15933 RVA: 0x000EC37A File Offset: 0x000EA57A
		public MultiValuedProperty<ADObjectId> TargetTransportServers
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[RoutingGroupConnectorSchema.TargetTransportServers];
			}
			set
			{
				this[RoutingGroupConnectorSchema.TargetTransportServers] = value;
			}
		}

		// Token: 0x170013FF RID: 5119
		// (get) Token: 0x06003E3E RID: 15934 RVA: 0x000EC388 File Offset: 0x000EA588
		// (set) Token: 0x06003E3F RID: 15935 RVA: 0x000EC39A File Offset: 0x000EA59A
		public string ExchangeLegacyDN
		{
			get
			{
				return (string)this[RoutingGroupConnectorSchema.ExchangeLegacyDN];
			}
			internal set
			{
				this[RoutingGroupConnectorSchema.ExchangeLegacyDN] = value;
			}
		}

		// Token: 0x17001400 RID: 5120
		// (get) Token: 0x06003E40 RID: 15936 RVA: 0x000EC3A8 File Offset: 0x000EA5A8
		// (set) Token: 0x06003E41 RID: 15937 RVA: 0x000EC3BA File Offset: 0x000EA5BA
		[Parameter]
		public bool PublicFolderReferralsEnabled
		{
			get
			{
				return (bool)this[RoutingGroupConnectorSchema.PublicFolderReferralsEnabled];
			}
			set
			{
				this[RoutingGroupConnectorSchema.PublicFolderReferralsEnabled] = value;
			}
		}

		// Token: 0x17001401 RID: 5121
		// (get) Token: 0x06003E42 RID: 15938 RVA: 0x000EC3CD File Offset: 0x000EA5CD
		internal override ADObjectSchema Schema
		{
			get
			{
				return RoutingGroupConnector.schema;
			}
		}

		// Token: 0x17001402 RID: 5122
		// (get) Token: 0x06003E43 RID: 15939 RVA: 0x000EC3D4 File Offset: 0x000EA5D4
		internal override string MostDerivedObjectClass
		{
			get
			{
				return RoutingGroupConnector.mostDerivedClass;
			}
		}

		// Token: 0x06003E44 RID: 15940 RVA: 0x000EC3DB File Offset: 0x000EA5DB
		internal static object TargetTransportServersGetter(IPropertyBag propertyBag)
		{
			return SendConnector.VsiIdsToServerIds(propertyBag, RoutingGroupConnectorSchema.TargetTransportServerVsis, RoutingGroupConnectorSchema.TargetTransportServers);
		}

		// Token: 0x06003E45 RID: 15941 RVA: 0x000EC3ED File Offset: 0x000EA5ED
		internal static void TargetTransportServersSetter(object value, IPropertyBag propertyBag)
		{
			SendConnector.ServerIdsToVsiIds(propertyBag, value, RoutingGroupConnectorSchema.TargetTransportServerVsis, RoutingGroupConnectorSchema.TargetTransportServers);
		}

		// Token: 0x06003E46 RID: 15942 RVA: 0x000EC400 File Offset: 0x000EA600
		internal static object PFReferralsEnabledGetter(IPropertyBag propertyBag)
		{
			return !(bool)propertyBag[RoutingGroupConnectorSchema.PublicFolderReferralsDisabled];
		}

		// Token: 0x06003E47 RID: 15943 RVA: 0x000EC41A File Offset: 0x000EA61A
		internal static void PFReferralsEnabledSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[RoutingGroupConnectorSchema.PublicFolderReferralsDisabled] = !(bool)value;
		}

		// Token: 0x04002A23 RID: 10787
		private static RoutingGroupConnectorSchema schema = ObjectSchema.GetInstance<RoutingGroupConnectorSchema>();

		// Token: 0x04002A24 RID: 10788
		private static string mostDerivedClass = "msExchRoutingGroupConnector";
	}
}
