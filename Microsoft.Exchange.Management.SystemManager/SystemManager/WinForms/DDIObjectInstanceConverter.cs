using System;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000C3 RID: 195
	public class DDIObjectInstanceConverter : TypeConverter
	{
		// Token: 0x06000649 RID: 1609 RVA: 0x000168AC File Offset: 0x00014AAC
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			Type type = (Type)new DDIObjectTypeConverter().ConvertFrom(value);
			return type.GetConstructor(new Type[0]).Invoke(new object[0]);
		}
	}
}
