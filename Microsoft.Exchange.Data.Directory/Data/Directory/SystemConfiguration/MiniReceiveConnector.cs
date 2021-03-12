using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004E5 RID: 1253
	[Serializable]
	public class MiniReceiveConnector : MiniObject
	{
		// Token: 0x06003806 RID: 14342 RVA: 0x000D983C File Offset: 0x000D7A3C
		static MiniReceiveConnector()
		{
			ReceiveConnector receiveConnector = new ReceiveConnector();
			MiniReceiveConnector.mostDerivedClass = receiveConnector.MostDerivedObjectClass;
			MiniReceiveConnector.schema = ObjectSchema.GetInstance<MiniReceiveConnectorSchema>();
		}

		// Token: 0x17001135 RID: 4405
		// (get) Token: 0x06003807 RID: 14343 RVA: 0x000D9864 File Offset: 0x000D7A64
		public bool IsSmtp
		{
			get
			{
				return base.ObjectClass.Contains("msExchSmtpReceiveConnector");
			}
		}

		// Token: 0x17001136 RID: 4406
		// (get) Token: 0x06003808 RID: 14344 RVA: 0x000D9876 File Offset: 0x000D7A76
		public ADObjectId Server
		{
			get
			{
				return (ADObjectId)this[ReceiveConnectorSchema.Server];
			}
		}

		// Token: 0x17001137 RID: 4407
		// (get) Token: 0x06003809 RID: 14345 RVA: 0x000D9888 File Offset: 0x000D7A88
		public AuthMechanisms AuthMechanism
		{
			get
			{
				return (AuthMechanisms)this[ReceiveConnectorSchema.SecurityFlags];
			}
		}

		// Token: 0x17001138 RID: 4408
		// (get) Token: 0x0600380A RID: 14346 RVA: 0x000D989A File Offset: 0x000D7A9A
		public MultiValuedProperty<IPBinding> Bindings
		{
			get
			{
				return (MultiValuedProperty<IPBinding>)this[ReceiveConnectorSchema.Bindings];
			}
		}

		// Token: 0x17001139 RID: 4409
		// (get) Token: 0x0600380B RID: 14347 RVA: 0x000D98AC File Offset: 0x000D7AAC
		public bool AdvertiseClientSettings
		{
			get
			{
				return (bool)this[ReceiveConnectorSchema.AdvertiseClientSettings];
			}
		}

		// Token: 0x1700113A RID: 4410
		// (get) Token: 0x0600380C RID: 14348 RVA: 0x000D98BE File Offset: 0x000D7ABE
		public Fqdn ServiceDiscoveryFqdn
		{
			get
			{
				return (Fqdn)this[ReceiveConnectorSchema.ServiceDiscoveryFqdn];
			}
		}

		// Token: 0x1700113B RID: 4411
		// (get) Token: 0x0600380D RID: 14349 RVA: 0x000D98D0 File Offset: 0x000D7AD0
		public Fqdn Fqdn
		{
			get
			{
				return (Fqdn)this[ReceiveConnectorSchema.Fqdn];
			}
		}

		// Token: 0x1700113C RID: 4412
		// (get) Token: 0x0600380E RID: 14350 RVA: 0x000D98E2 File Offset: 0x000D7AE2
		internal override ADObjectSchema Schema
		{
			get
			{
				return MiniReceiveConnector.schema;
			}
		}

		// Token: 0x1700113D RID: 4413
		// (get) Token: 0x0600380F RID: 14351 RVA: 0x000D98E9 File Offset: 0x000D7AE9
		internal override string MostDerivedObjectClass
		{
			get
			{
				return MiniReceiveConnector.mostDerivedClass;
			}
		}

		// Token: 0x040025D8 RID: 9688
		private static ADObjectSchema schema;

		// Token: 0x040025D9 RID: 9689
		private static string mostDerivedClass;
	}
}
