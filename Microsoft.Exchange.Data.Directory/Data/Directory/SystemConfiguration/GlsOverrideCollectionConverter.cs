using System;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000298 RID: 664
	public class GlsOverrideCollectionConverter : TypeConverter
	{
		// Token: 0x06001F09 RID: 7945 RVA: 0x0008AD17 File Offset: 0x00088F17
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x0008AD35 File Offset: 0x00088F35
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x0008AD53 File Offset: 0x00088F53
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null || value is string)
			{
				return new GlsOverrideCollection((string)value);
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x0008AD78 File Offset: 0x00088F78
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(GlsOverrideCollection))
			{
				return this.ConvertFrom(context, culture, value);
			}
			GlsOverrideCollection glsOverrideCollection = value as GlsOverrideCollection;
			if (glsOverrideCollection == null)
			{
				throw new ArgumentException("value");
			}
			if (destinationType == typeof(string))
			{
				return glsOverrideCollection.ToString();
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
