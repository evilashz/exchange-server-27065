using System;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x0200014D RID: 333
	public sealed class VariantValueValidationException : OverrideValidationException
	{
		// Token: 0x06000F47 RID: 3911 RVA: 0x00026C1C File Offset: 0x00024E1C
		public VariantValueValidationException(VariantConfigurationOverride o, VariantType variant, string value, string format, Exception innerException = null) : base(string.Format("The value '{0}' specified for variant '{1}' does not match its type. Only values of type '{2}' in format '{3}' are allowed for this variant.", new object[]
		{
			value,
			variant.Name,
			variant.Type,
			format
		}), o, innerException)
		{
			this.Variant = variant;
			this.Value = value;
			this.Format = format;
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06000F48 RID: 3912 RVA: 0x00026C75 File Offset: 0x00024E75
		// (set) Token: 0x06000F49 RID: 3913 RVA: 0x00026C7D File Offset: 0x00024E7D
		public VariantType Variant { get; private set; }

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06000F4A RID: 3914 RVA: 0x00026C86 File Offset: 0x00024E86
		// (set) Token: 0x06000F4B RID: 3915 RVA: 0x00026C8E File Offset: 0x00024E8E
		public string Value { get; private set; }

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x00026C97 File Offset: 0x00024E97
		// (set) Token: 0x06000F4D RID: 3917 RVA: 0x00026C9F File Offset: 0x00024E9F
		public string Format { get; private set; }
	}
}
