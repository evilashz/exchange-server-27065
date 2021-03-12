using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x02000003 RID: 3
	public sealed class ComponentNameValidationException : OverrideValidationException
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020F2 File Offset: 0x000002F2
		public ComponentNameValidationException(VariantConfigurationOverride o, IEnumerable<string> allowedValues, Exception innerException = null) : base(string.Format("A component with the name '{0}' does not exist. Available component names are {1}.", o.ComponentName, string.Join(", ", allowedValues)), o, innerException)
		{
			this.AllowedValues = allowedValues;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000211E File Offset: 0x0000031E
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002126 File Offset: 0x00000326
		public IEnumerable<string> AllowedValues { get; private set; }
	}
}
