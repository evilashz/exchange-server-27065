using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000010 RID: 16
	[Serializable]
	public class ExchangeDiagnosticInfoResult : ConfigurableObject
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x00004660 File Offset: 0x00002860
		internal ExchangeDiagnosticInfoResult(string result) : base(new SimpleProviderPropertyBag())
		{
			this.Result = result;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004674 File Offset: 0x00002874
		public ExchangeDiagnosticInfoResult() : base(new SimpleProviderPropertyBag())
		{
			this.Result = string.Empty;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000468C File Offset: 0x0000288C
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x000046A3 File Offset: 0x000028A3
		public string Result
		{
			get
			{
				return (string)this.propertyBag[ExchangeDiagnosticInfoResult.Schema.Result];
			}
			internal set
			{
				this.propertyBag[ExchangeDiagnosticInfoResult.Schema.Result] = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000046B6 File Offset: 0x000028B6
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000046BD File Offset: 0x000028BD
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ExchangeDiagnosticInfoResult.SchemaInstance;
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000046C4 File Offset: 0x000028C4
		public override string ToString()
		{
			return this.Result;
		}

		// Token: 0x0400004E RID: 78
		private static readonly ExchangeDiagnosticInfoResult.Schema SchemaInstance = ObjectSchema.GetInstance<ExchangeDiagnosticInfoResult.Schema>();

		// Token: 0x02000011 RID: 17
		internal class Schema : SimpleProviderObjectSchema
		{
			// Token: 0x0400004F RID: 79
			internal static readonly SimpleProviderPropertyDefinition Result = new SimpleProviderPropertyDefinition("Result", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}
	}
}
