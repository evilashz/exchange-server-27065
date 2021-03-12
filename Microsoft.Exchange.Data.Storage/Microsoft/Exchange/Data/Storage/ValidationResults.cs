using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DF4 RID: 3572
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ValidationResults
	{
		// Token: 0x06007ABE RID: 31422 RVA: 0x0021F08F File Offset: 0x0021D28F
		public ValidationResults(ValidationResult result, string reason)
		{
			EnumValidator.ThrowIfInvalid<ValidationResult>(result, "result");
			this.result = result;
			this.reason = reason;
		}

		// Token: 0x170020D1 RID: 8401
		// (get) Token: 0x06007ABF RID: 31423 RVA: 0x0021F0B0 File Offset: 0x0021D2B0
		public ValidationResult Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x170020D2 RID: 8402
		// (get) Token: 0x06007AC0 RID: 31424 RVA: 0x0021F0B8 File Offset: 0x0021D2B8
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x06007AC1 RID: 31425 RVA: 0x0021F0C0 File Offset: 0x0021D2C0
		public override string ToString()
		{
			return "Validation result=" + this.result.ToString() + ", reason=" + this.reason;
		}

		// Token: 0x06007AC2 RID: 31426 RVA: 0x0021F0E7 File Offset: 0x0021D2E7
		private ValidationResults(ValidationResult result)
		{
			this.result = result;
			this.reason = null;
		}

		// Token: 0x0400548E RID: 21646
		internal static readonly ValidationResults Success = new ValidationResults(ValidationResult.Success);

		// Token: 0x0400548F RID: 21647
		private ValidationResult result;

		// Token: 0x04005490 RID: 21648
		private string reason;
	}
}
