using System;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace Microsoft.Exchange.Common
{
	// Token: 0x0200002E RID: 46
	public class CommonReflectionHelper
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00005180 File Offset: 0x00003380
		internal static XElement[] GetXmlProperties(object obj)
		{
			Type type = obj.GetType();
			return (from property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty)
			select new XElement(property.Name, property.GetValue(obj, null))).ToArray<XElement>();
		}
	}
}
