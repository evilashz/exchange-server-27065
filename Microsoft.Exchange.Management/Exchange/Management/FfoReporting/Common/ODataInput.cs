using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.FfoReporting.Common
{
	// Token: 0x020003D4 RID: 980
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	internal sealed class ODataInput : Attribute
	{
		// Token: 0x0600230E RID: 8974 RVA: 0x0008E63C File Offset: 0x0008C83C
		internal ODataInput(string propertyName)
		{
			this.propertyName = propertyName;
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x0008E64C File Offset: 0x0008C84C
		internal void SetCmdletProperty(object ffoTask, object value)
		{
			PropertyInfo property = ffoTask.GetType().GetProperty(this.propertyName);
			if (property == null)
			{
				throw new NullReferenceException("Property does not exist.");
			}
			if (typeof(IList).IsAssignableFrom(property.PropertyType))
			{
				IList list = ODataInput.ConvertToList(value);
				if (list == null || list.Count <= 0)
				{
					return;
				}
				IList list2 = (IList)property.GetValue(ffoTask, null);
				list2.Clear();
				using (IEnumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object value2 = enumerator.Current;
						list2.Add(value2);
					}
					return;
				}
			}
			object value3 = ValueConvertor.ConvertValue(value, property.PropertyType, null);
			property.SetValue(ffoTask, value3);
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x0008E720 File Offset: 0x0008C920
		private static IList ConvertToList(object value)
		{
			string text = value as string;
			IList list;
			if (!string.IsNullOrEmpty(text))
			{
				list = text.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries);
			}
			else
			{
				list = (value as IList);
				if (list == null)
				{
					list = new List<object>
					{
						value
					};
				}
			}
			return list;
		}

		// Token: 0x04001BA6 RID: 7078
		private readonly string propertyName;
	}
}
