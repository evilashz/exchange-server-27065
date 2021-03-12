using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x0200013A RID: 314
	public sealed class SectionNameValidationException : OverrideValidationException
	{
		// Token: 0x06000E9B RID: 3739 RVA: 0x0002399E File Offset: 0x00021B9E
		public SectionNameValidationException(VariantConfigurationOverride o, IEnumerable<string> allowedValues, Exception innerException = null) : base(string.Format("A section with the name '{1}' does not exist in component '{0}'. Available section names in this component: {2}.", o.ComponentName, o.SectionName, string.Join(", ", allowedValues)), o, innerException)
		{
			this.AllowedValues = allowedValues;
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x000239D0 File Offset: 0x00021BD0
		// (set) Token: 0x06000E9D RID: 3741 RVA: 0x000239D8 File Offset: 0x00021BD8
		public IEnumerable<string> AllowedValues { get; private set; }
	}
}
