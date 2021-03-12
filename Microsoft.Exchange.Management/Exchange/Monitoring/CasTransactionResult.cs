using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000522 RID: 1314
	[Serializable]
	public class CasTransactionResult : ConfigurableObject
	{
		// Token: 0x06002F4E RID: 12110 RVA: 0x000BE7F5 File Offset: 0x000BC9F5
		internal CasTransactionResult(CasTransactionResultEnum result) : base(new CasTransactionPropertyBag())
		{
			this.Value = result;
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06002F4F RID: 12111 RVA: 0x000BE809 File Offset: 0x000BCA09
		// (set) Token: 0x06002F50 RID: 12112 RVA: 0x000BE820 File Offset: 0x000BCA20
		public CasTransactionResultEnum Value
		{
			get
			{
				return (CasTransactionResultEnum)this.propertyBag[CasTransactionResultSchema.Value];
			}
			internal set
			{
				this.propertyBag[CasTransactionResultSchema.Value] = value;
			}
		}

		// Token: 0x06002F51 RID: 12113 RVA: 0x000BE838 File Offset: 0x000BCA38
		public override string ToString()
		{
			string result = string.Empty;
			switch (this.Value)
			{
			case CasTransactionResultEnum.Undefined:
				result = Strings.CasTransactionResultUndefined;
				break;
			case CasTransactionResultEnum.Success:
				result = Strings.CasTransactionResultSuccess;
				break;
			case CasTransactionResultEnum.Failure:
				result = Strings.CasTransactionResultFailure;
				break;
			case CasTransactionResultEnum.Skipped:
				result = Strings.CasTransactionResultSkipped;
				break;
			default:
				throw new CasTransactionResultToStringCaseNotHandledException(this.Value);
			}
			return result;
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06002F52 RID: 12114 RVA: 0x000BE8AB File Offset: 0x000BCAAB
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x06002F53 RID: 12115 RVA: 0x000BE8B2 File Offset: 0x000BCAB2
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return CasTransactionResult.schema;
			}
		}

		// Token: 0x040021DA RID: 8666
		private static CasTransactionResultSchema schema = ObjectSchema.GetInstance<CasTransactionResultSchema>();
	}
}
