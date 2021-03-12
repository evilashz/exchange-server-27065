using System;
using System.Collections.Generic;
using System.Text;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000592 RID: 1426
	public class BindingDictionary : Dictionary<string, Binding>
	{
		// Token: 0x060041AD RID: 16813 RVA: 0x000C7E6C File Offset: 0x000C606C
		public string ToJavaScript(IControlResolver resolver)
		{
			StringBuilder stringBuilder = new StringBuilder("{");
			foreach (KeyValuePair<string, Binding> keyValuePair in this)
			{
				stringBuilder.Append('"');
				stringBuilder.Append(keyValuePair.Key);
				stringBuilder.Append("\":");
				stringBuilder.Append(keyValuePair.Value.ToJavaScript(resolver));
				stringBuilder.Append(",");
			}
			if (stringBuilder.Length > 1)
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}
	}
}
