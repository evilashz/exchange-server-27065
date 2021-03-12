using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200072D RID: 1837
	[Serializable]
	public sealed class TestDataCenterDKMAccessResult : ConfigurableObject
	{
		// Token: 0x170013D3 RID: 5075
		// (get) Token: 0x06004145 RID: 16709 RVA: 0x0010C11E File Offset: 0x0010A31E
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return TestDataCenterDKMAccessResult.Schema;
			}
		}

		// Token: 0x06004146 RID: 16710 RVA: 0x0010C125 File Offset: 0x0010A325
		public TestDataCenterDKMAccessResult(bool aclStateIsGood, string aclStateDetails) : base(new SimpleProviderPropertyBag())
		{
			this.AclStateIsGood = aclStateIsGood;
			this.AclStateDetails = aclStateDetails;
		}

		// Token: 0x170013D4 RID: 5076
		// (get) Token: 0x06004147 RID: 16711 RVA: 0x0010C140 File Offset: 0x0010A340
		// (set) Token: 0x06004148 RID: 16712 RVA: 0x0010C152 File Offset: 0x0010A352
		public bool AclStateIsGood
		{
			get
			{
				return (bool)this[TestDataCenterDKMAccessResultSchema.AclStateIsGood];
			}
			private set
			{
				this[TestDataCenterDKMAccessResultSchema.AclStateIsGood] = value;
			}
		}

		// Token: 0x170013D5 RID: 5077
		// (get) Token: 0x06004149 RID: 16713 RVA: 0x0010C165 File Offset: 0x0010A365
		// (set) Token: 0x0600414A RID: 16714 RVA: 0x0010C177 File Offset: 0x0010A377
		public string AclStateDetails
		{
			get
			{
				return (string)this[TestDataCenterDKMAccessResultSchema.AclStateDetails];
			}
			private set
			{
				this[TestDataCenterDKMAccessResultSchema.AclStateDetails] = value;
			}
		}

		// Token: 0x170013D6 RID: 5078
		// (get) Token: 0x0600414B RID: 16715 RVA: 0x0010C185 File Offset: 0x0010A385
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04002935 RID: 10549
		private static readonly TestDataCenterDKMAccessResultSchema Schema = ObjectSchema.GetInstance<TestDataCenterDKMAccessResultSchema>();
	}
}
