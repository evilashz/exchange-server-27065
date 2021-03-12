using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Management.SnapIn;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000C4 RID: 196
	public class OrganizationTypesConverter : TypeConverter
	{
		// Token: 0x0600064B RID: 1611 RVA: 0x0001690C File Offset: 0x00014B0C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value.ToString();
			return (from c in text.Split(new char[]
			{
				','
			})
			where !string.IsNullOrEmpty(c)
			select (OrganizationType)Enum.Parse(typeof(OrganizationType), c)).ToArray<OrganizationType>();
		}
	}
}
