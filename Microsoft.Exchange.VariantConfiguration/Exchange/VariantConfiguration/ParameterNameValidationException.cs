using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x0200012F RID: 303
	public sealed class ParameterNameValidationException : OverrideValidationException
	{
		// Token: 0x06000E57 RID: 3671 RVA: 0x00022B90 File Offset: 0x00020D90
		public ParameterNameValidationException(VariantConfigurationOverride o, string parameterName, IEnumerable<string> allowedValues, Exception innerException = null) : base(string.Format("A parameter with the name '{2}' does not exist in component '{0}', section '{1}'. Available parameter names are {3}.", new object[]
		{
			o.ComponentName,
			o.SectionName,
			parameterName,
			string.Join(", ", allowedValues)
		}), o, innerException)
		{
			this.ParameterName = parameterName;
			this.AllowedValues = allowedValues;
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x00022BEA File Offset: 0x00020DEA
		// (set) Token: 0x06000E59 RID: 3673 RVA: 0x00022BF2 File Offset: 0x00020DF2
		public string ParameterName { get; private set; }

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x00022BFB File Offset: 0x00020DFB
		// (set) Token: 0x06000E5B RID: 3675 RVA: 0x00022C03 File Offset: 0x00020E03
		public IEnumerable<string> AllowedValues { get; private set; }
	}
}
