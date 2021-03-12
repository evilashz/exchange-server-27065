using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x0200000F RID: 15
	public sealed class FlightNameValidationException : OverrideValidationException
	{
		// Token: 0x06000053 RID: 83 RVA: 0x000034E4 File Offset: 0x000016E4
		public FlightNameValidationException(VariantConfigurationOverride o, IEnumerable<string> allowedValues, Exception innerException = null) : base(string.Format("A flight with the name '{0}' does not exist. Available flight names are {1}.", o.SectionName, string.Join(", ", allowedValues)), o, innerException)
		{
			this.AllowedValues = allowedValues;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00003510 File Offset: 0x00001710
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00003518 File Offset: 0x00001718
		public IEnumerable<string> AllowedValues { get; private set; }
	}
}
