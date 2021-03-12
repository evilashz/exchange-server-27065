using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x02000149 RID: 329
	public sealed class VariantNameValidationException : OverrideValidationException
	{
		// Token: 0x06000F2E RID: 3886 RVA: 0x000264D6 File Offset: 0x000246D6
		public VariantNameValidationException(VariantConfigurationOverride o, string variantName, IEnumerable<string> allowedValues, Exception innerException = null) : base(string.Format("The variant name '{0}' is not recognized. Available variant names are {1}.", variantName, string.Join(", ", allowedValues)), o, innerException)
		{
			this.VariantName = variantName;
			this.AllowedValues = allowedValues;
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x00026505 File Offset: 0x00024705
		// (set) Token: 0x06000F30 RID: 3888 RVA: 0x0002650D File Offset: 0x0002470D
		public string VariantName { get; private set; }

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x00026516 File Offset: 0x00024716
		// (set) Token: 0x06000F32 RID: 3890 RVA: 0x0002651E File Offset: 0x0002471E
		public IEnumerable<string> AllowedValues { get; private set; }
	}
}
