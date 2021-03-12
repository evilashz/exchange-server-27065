using System;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x0200013B RID: 315
	public sealed class SyntaxValidationException : OverrideValidationException
	{
		// Token: 0x06000E9E RID: 3742 RVA: 0x000239F4 File Offset: 0x00021BF4
		public SyntaxValidationException(VariantConfigurationOverride o, Exception innerException = null) : base(string.Format("The override parameters @(\"{0}\") contain syntax error: {1}", string.Join("\", \"", Array.ConvertAll<string, string>(o.Parameters, (string parameter) => parameter.Replace("\"", "^\""))), (innerException != null) ? innerException.Message : string.Empty), o, innerException)
		{
		}
	}
}
