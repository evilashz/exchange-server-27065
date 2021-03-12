using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000883 RID: 2179
	[Serializable]
	public sealed class ClientAccessRulesEvaluationResult : ConfigurableObject
	{
		// Token: 0x17001671 RID: 5745
		// (get) Token: 0x06004B8E RID: 19342 RVA: 0x0013892E File Offset: 0x00136B2E
		// (set) Token: 0x06004B8F RID: 19343 RVA: 0x00138936 File Offset: 0x00136B36
		public new ObjectId Identity { get; set; }

		// Token: 0x17001672 RID: 5746
		// (get) Token: 0x06004B90 RID: 19344 RVA: 0x0013893F File Offset: 0x00136B3F
		// (set) Token: 0x06004B91 RID: 19345 RVA: 0x00138947 File Offset: 0x00136B47
		public string Name { get; set; }

		// Token: 0x17001673 RID: 5747
		// (get) Token: 0x06004B92 RID: 19346 RVA: 0x00138950 File Offset: 0x00136B50
		// (set) Token: 0x06004B93 RID: 19347 RVA: 0x00138958 File Offset: 0x00136B58
		public ClientAccessRulesAction Action { get; set; }

		// Token: 0x06004B94 RID: 19348 RVA: 0x00138961 File Offset: 0x00136B61
		public ClientAccessRulesEvaluationResult() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x17001674 RID: 5748
		// (get) Token: 0x06004B95 RID: 19349 RVA: 0x0013896E File Offset: 0x00136B6E
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ClientAccessRulesEvaluationResult.Schema;
			}
		}

		// Token: 0x17001675 RID: 5749
		// (get) Token: 0x06004B96 RID: 19350 RVA: 0x00138975 File Offset: 0x00136B75
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x04002D30 RID: 11568
		private static readonly ClientAccessRulesEvaluationResultSchema Schema = ObjectSchema.GetInstance<ClientAccessRulesEvaluationResultSchema>();
	}
}
