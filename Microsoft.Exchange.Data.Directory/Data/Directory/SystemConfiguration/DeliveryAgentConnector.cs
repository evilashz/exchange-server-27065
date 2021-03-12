using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003F7 RID: 1015
	[Serializable]
	public class DeliveryAgentConnector : MailGateway
	{
		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06002E5D RID: 11869 RVA: 0x000BC8AF File Offset: 0x000BAAAF
		internal override ADObjectSchema Schema
		{
			get
			{
				return DeliveryAgentConnector.schema;
			}
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06002E5E RID: 11870 RVA: 0x000BC8B6 File Offset: 0x000BAAB6
		internal override string MostDerivedObjectClass
		{
			get
			{
				return DeliveryAgentConnector.MostDerivedClass;
			}
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x06002E5F RID: 11871 RVA: 0x000BC8BD File Offset: 0x000BAABD
		// (set) Token: 0x06002E60 RID: 11872 RVA: 0x000BC8CF File Offset: 0x000BAACF
		[Parameter]
		public override bool Enabled
		{
			get
			{
				return (bool)this[DeliveryAgentConnectorSchema.Enabled];
			}
			set
			{
				this[DeliveryAgentConnectorSchema.Enabled] = value;
			}
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06002E61 RID: 11873 RVA: 0x000BC8E2 File Offset: 0x000BAAE2
		// (set) Token: 0x06002E62 RID: 11874 RVA: 0x000BC8F4 File Offset: 0x000BAAF4
		[Parameter]
		public string DeliveryProtocol
		{
			get
			{
				return (string)this[DeliveryAgentConnectorSchema.DeliveryProtocol];
			}
			set
			{
				this[DeliveryAgentConnectorSchema.DeliveryProtocol] = value;
			}
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06002E63 RID: 11875 RVA: 0x000BC902 File Offset: 0x000BAB02
		// (set) Token: 0x06002E64 RID: 11876 RVA: 0x000BC914 File Offset: 0x000BAB14
		[Parameter]
		public int MaxConcurrentConnections
		{
			get
			{
				return (int)this[DeliveryAgentConnectorSchema.MaxConcurrentConnections];
			}
			set
			{
				this[DeliveryAgentConnectorSchema.MaxConcurrentConnections] = value;
			}
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06002E65 RID: 11877 RVA: 0x000BC927 File Offset: 0x000BAB27
		// (set) Token: 0x06002E66 RID: 11878 RVA: 0x000BC939 File Offset: 0x000BAB39
		[Parameter]
		public int MaxMessagesPerConnection
		{
			get
			{
				return (int)this[DeliveryAgentConnectorSchema.MaxMessagesPerConnection];
			}
			set
			{
				this[DeliveryAgentConnectorSchema.MaxMessagesPerConnection] = value;
			}
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06002E67 RID: 11879 RVA: 0x000BC94C File Offset: 0x000BAB4C
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x04001F1C RID: 7964
		public new static string MostDerivedClass = "msExchDeliveryAgentConnector";

		// Token: 0x04001F1D RID: 7965
		private static DeliveryAgentConnectorSchema schema = ObjectSchema.GetInstance<DeliveryAgentConnectorSchema>();
	}
}
