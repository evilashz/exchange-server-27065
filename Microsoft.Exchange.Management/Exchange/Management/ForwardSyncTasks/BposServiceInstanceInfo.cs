using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000344 RID: 836
	[Serializable]
	public class BposServiceInstanceInfo : ConfigurableObject
	{
		// Token: 0x06001CD7 RID: 7383 RVA: 0x0007F7EF File Offset: 0x0007D9EF
		internal BposServiceInstanceInfo(ServiceInstanceId serviceInstanceId, string endpointName, Uri backSyncUrl, bool authorityTransferIsSupported) : this()
		{
			this.Identity = serviceInstanceId;
			this.EndpointName = endpointName;
			this.BackSyncUrl = backSyncUrl;
			this.AuthorityTransferIsSupported = authorityTransferIsSupported;
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x0007F814 File Offset: 0x0007DA14
		internal BposServiceInstanceInfo() : base(new SimpleProviderPropertyBag())
		{
			this.propertyBag.SetField(this.propertyBag.ObjectVersionPropertyDefinition, ExchangeObjectVersion.Exchange2010);
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06001CD9 RID: 7385 RVA: 0x0007F83D File Offset: 0x0007DA3D
		// (set) Token: 0x06001CDA RID: 7386 RVA: 0x0007F84F File Offset: 0x0007DA4F
		public new ServiceInstanceId Identity
		{
			get
			{
				return (ServiceInstanceId)this[BposServiceInstanceInfoSchema.Identity];
			}
			internal set
			{
				this[BposServiceInstanceInfoSchema.Identity] = value;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06001CDB RID: 7387 RVA: 0x0007F85D File Offset: 0x0007DA5D
		// (set) Token: 0x06001CDC RID: 7388 RVA: 0x0007F86F File Offset: 0x0007DA6F
		public Uri BackSyncUrl
		{
			get
			{
				return (Uri)base[BposServiceInstanceInfoSchema.BackSyncUrl];
			}
			private set
			{
				base[BposServiceInstanceInfoSchema.BackSyncUrl] = value;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06001CDD RID: 7389 RVA: 0x0007F87D File Offset: 0x0007DA7D
		// (set) Token: 0x06001CDE RID: 7390 RVA: 0x0007F88F File Offset: 0x0007DA8F
		public string EndpointName
		{
			get
			{
				return (string)base[BposServiceInstanceInfoSchema.EndpointName];
			}
			private set
			{
				base[BposServiceInstanceInfoSchema.EndpointName] = value;
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06001CDF RID: 7391 RVA: 0x0007F89D File Offset: 0x0007DA9D
		// (set) Token: 0x06001CE0 RID: 7392 RVA: 0x0007F8AF File Offset: 0x0007DAAF
		public bool AuthorityTransferIsSupported
		{
			get
			{
				return (bool)base[BposServiceInstanceInfoSchema.AuthorityTransferIsSupported];
			}
			private set
			{
				base[BposServiceInstanceInfoSchema.AuthorityTransferIsSupported] = value;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06001CE1 RID: 7393 RVA: 0x0007F8C2 File Offset: 0x0007DAC2
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return BposServiceInstanceInfo.schema;
			}
		}

		// Token: 0x04001862 RID: 6242
		public const string BackSyncUrlEndpointName = "BackSyncPSConnectionURI";

		// Token: 0x04001863 RID: 6243
		public const string SupportsAuthorityTransfer = "SupportsAuthorityTransfer";

		// Token: 0x04001864 RID: 6244
		private static BposServiceInstanceInfoSchema schema = ObjectSchema.GetInstance<BposServiceInstanceInfoSchema>();
	}
}
