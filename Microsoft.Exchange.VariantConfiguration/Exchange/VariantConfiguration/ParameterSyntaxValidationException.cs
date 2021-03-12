using System;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x0200012E RID: 302
	public sealed class ParameterSyntaxValidationException : OverrideValidationException
	{
		// Token: 0x06000E54 RID: 3668 RVA: 0x00022B63 File Offset: 0x00020D63
		public ParameterSyntaxValidationException(VariantConfigurationOverride o, string parameterLine, Exception innerException = null) : base(string.Format("The syntax in parameter override '{0}' is incorrect.", parameterLine), o, innerException)
		{
			this.ParameterLine = parameterLine;
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x00022B7F File Offset: 0x00020D7F
		// (set) Token: 0x06000E56 RID: 3670 RVA: 0x00022B87 File Offset: 0x00020D87
		public string ParameterLine { get; private set; }
	}
}
