using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000565 RID: 1381
	public static class VirtualDirectoryService
	{
		// Token: 0x0600405E RID: 16478 RVA: 0x000C44B0 File Offset: 0x000C26B0
		public static string GetWebSiteNameFromVDirName(string vdirName)
		{
			MatchCollection matchCollection = Regex.Matches(vdirName, "\\([\\w\\s]+\\)");
			return matchCollection[matchCollection.Count - 1].Value.Trim(new char[]
			{
				'(',
				')'
			});
		}

		// Token: 0x0600405F RID: 16479 RVA: 0x000C44F4 File Offset: 0x000C26F4
		public static string AuthenticationMethodsToText(object authen)
		{
			ADMultiValuedProperty<AuthenticationMethod> admultiValuedProperty = (ADMultiValuedProperty<AuthenticationMethod>)authen;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (AuthenticationMethod authenticationMethod in admultiValuedProperty)
			{
				stringBuilder.AppendFormat("{0}, ", LocalizedDescriptionAttribute.FromEnum(typeof(AuthenticationMethod), authenticationMethod));
			}
			return stringBuilder.ToString().TrimEnd(new char[]
			{
				',',
				' '
			});
		}

		// Token: 0x06004060 RID: 16480 RVA: 0x000C459C File Offset: 0x000C279C
		public static void ResetGetPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store, PowerShellResults[] psResults)
		{
			string name = (string)inputRow["DataObjectName"];
			object dataObject = store.GetDataObject(name);
			StringBuilder stringBuilder = new StringBuilder();
			PropertyInfo[] properties = dataObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.GetProperty);
			int num = properties.Max((PropertyInfo p) => p.Name.Length);
			foreach (PropertyInfo propertyInfo in properties)
			{
				stringBuilder.Append(string.Format("{0}{1} : ", propertyInfo.Name, new string(' ', num - propertyInfo.Name.Length)));
				bool flag = true;
				object value = propertyInfo.GetValue(dataObject, null);
				if (value != null && value is ICollection && value.GetType().GetMethod("ToString", new Type[0]).DeclaringType == typeof(object))
				{
					foreach (object arg in ((IEnumerable)value))
					{
						stringBuilder.AppendLine(string.Format("{0}{1}", flag ? string.Empty : new string(' ', num + 3), arg));
						flag = false;
					}
					if (flag)
					{
						stringBuilder.AppendLine();
					}
				}
				else
				{
					stringBuilder.AppendLine((value == null) ? string.Empty : value.ToString());
				}
			}
			inputRow["FileContent"] = stringBuilder.ToString();
			store.SetModifiedColumns(new List<string>
			{
				"FileContent"
			});
		}
	}
}
