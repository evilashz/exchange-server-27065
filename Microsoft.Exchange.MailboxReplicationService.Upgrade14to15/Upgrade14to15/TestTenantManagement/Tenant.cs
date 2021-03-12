using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15.TestTenantManagement
{
	// Token: 0x020000AD RID: 173
	[DataContract(Name = "Tenant", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.SyntheticSvc.Contracts")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class Tenant : IExtensibleDataObject
	{
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00008F13 File Offset: 0x00007113
		// (set) Token: 0x060004F7 RID: 1271 RVA: 0x00008F1B File Offset: 0x0000711B
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00008F24 File Offset: 0x00007124
		// (set) Token: 0x060004F9 RID: 1273 RVA: 0x00008F2C File Offset: 0x0000712C
		[DataMember]
		public string AdminPassword
		{
			get
			{
				return this.AdminPasswordField;
			}
			set
			{
				this.AdminPasswordField = value;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x00008F35 File Offset: 0x00007135
		// (set) Token: 0x060004FB RID: 1275 RVA: 0x00008F3D File Offset: 0x0000713D
		[DataMember]
		public string AdminUPN
		{
			get
			{
				return this.AdminUPNField;
			}
			set
			{
				this.AdminUPNField = value;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x00008F46 File Offset: 0x00007146
		// (set) Token: 0x060004FD RID: 1277 RVA: 0x00008F4E File Offset: 0x0000714E
		[DataMember]
		public string ExchangeServiceInstance
		{
			get
			{
				return this.ExchangeServiceInstanceField;
			}
			set
			{
				this.ExchangeServiceInstanceField = value;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x00008F57 File Offset: 0x00007157
		// (set) Token: 0x060004FF RID: 1279 RVA: 0x00008F5F File Offset: 0x0000715F
		[DataMember]
		public string ForefrontServiceInstance
		{
			get
			{
				return this.ForefrontServiceInstanceField;
			}
			set
			{
				this.ForefrontServiceInstanceField = value;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x00008F68 File Offset: 0x00007168
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x00008F70 File Offset: 0x00007170
		[DataMember]
		public string LyncServiceInstance
		{
			get
			{
				return this.LyncServiceInstanceField;
			}
			set
			{
				this.LyncServiceInstanceField = value;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x00008F79 File Offset: 0x00007179
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x00008F81 File Offset: 0x00007181
		[DataMember]
		public string SharepointServiceInstance
		{
			get
			{
				return this.SharepointServiceInstanceField;
			}
			set
			{
				this.SharepointServiceInstanceField = value;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x00008F8A File Offset: 0x0000718A
		// (set) Token: 0x06000505 RID: 1285 RVA: 0x00008F92 File Offset: 0x00007192
		[DataMember]
		public Guid TenantId
		{
			get
			{
				return this.TenantIdField;
			}
			set
			{
				this.TenantIdField = value;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00008F9B File Offset: 0x0000719B
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x00008FA3 File Offset: 0x000071A3
		[DataMember]
		public TenantOffer[] TenantOffers
		{
			get
			{
				return this.TenantOffersField;
			}
			set
			{
				this.TenantOffersField = value;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x00008FAC File Offset: 0x000071AC
		// (set) Token: 0x06000509 RID: 1289 RVA: 0x00008FB4 File Offset: 0x000071B4
		[DataMember]
		public string TestPartnerScenario
		{
			get
			{
				return this.TestPartnerScenarioField;
			}
			set
			{
				this.TestPartnerScenarioField = value;
			}
		}

		// Token: 0x04000268 RID: 616
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000269 RID: 617
		private string AdminPasswordField;

		// Token: 0x0400026A RID: 618
		private string AdminUPNField;

		// Token: 0x0400026B RID: 619
		private string ExchangeServiceInstanceField;

		// Token: 0x0400026C RID: 620
		private string ForefrontServiceInstanceField;

		// Token: 0x0400026D RID: 621
		private string LyncServiceInstanceField;

		// Token: 0x0400026E RID: 622
		private string SharepointServiceInstanceField;

		// Token: 0x0400026F RID: 623
		private Guid TenantIdField;

		// Token: 0x04000270 RID: 624
		private TenantOffer[] TenantOffersField;

		// Token: 0x04000271 RID: 625
		private string TestPartnerScenarioField;
	}
}
