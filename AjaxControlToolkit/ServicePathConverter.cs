using System;
using System.ComponentModel;
using System.Globalization;
using System.Web;

namespace AjaxControlToolkit
{
	// Token: 0x0200001F RID: 31
	public class ServicePathConverter : StringConverter
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00004324 File Offset: 0x00002524
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				string value2 = (string)value;
				if (string.IsNullOrEmpty(value2))
				{
					HttpContext httpContext = HttpContext.Current;
					if (httpContext != null)
					{
						return httpContext.Request.FilePath;
					}
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
