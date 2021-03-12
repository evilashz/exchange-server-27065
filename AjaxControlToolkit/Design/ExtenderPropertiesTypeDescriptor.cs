using System;
using System.ComponentModel;
using System.Globalization;

namespace AjaxControlToolkit.Design
{
	// Token: 0x02000009 RID: 9
	internal class ExtenderPropertiesTypeDescriptor : ExpandableObjectConverter
	{
		// Token: 0x06000030 RID: 48 RVA: 0x000024F0 File Offset: 0x000006F0
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				return string.Empty;
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
