using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000C2 RID: 194
	public class DDIObjectTypeConverter : TypeConverter
	{
		// Token: 0x06000646 RID: 1606 RVA: 0x000167B8 File Offset: 0x000149B8
		static DDIObjectTypeConverter()
		{
			DDIObjectTypeConverter.unHandledTypeList["System.Net.IPAddress"] = typeof(IPAddress);
			DDIObjectTypeConverter.unHandledTypeList["Microsoft.Exchange.Data.Common.LocalizedString, Microsoft.Exchange.Data.Common"] = typeof(LocalizedString);
			DDIObjectTypeConverter.unHandledTypeList["Microsoft.Exchange.Data.Common.LocalizedString"] = typeof(LocalizedString);
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00016824 File Offset: 0x00014A24
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value.ToString();
			if (DDIObjectTypeConverter.unHandledTypeList.ContainsKey(text))
			{
				return DDIObjectTypeConverter.unHandledTypeList[text];
			}
			object result;
			lock (DDIObjectTypeConverter.syncInstance)
			{
				Type type = Type.GetType(text);
				if (!DDIObjectTypeConverter.unHandledTypeList.ContainsKey(text))
				{
					DDIObjectTypeConverter.unHandledTypeList.Add(text, type);
				}
				result = type;
			}
			return result;
		}

		// Token: 0x0400020E RID: 526
		private static object syncInstance = new object();

		// Token: 0x0400020F RID: 527
		private static Dictionary<string, Type> unHandledTypeList = new Dictionary<string, Type>();
	}
}
