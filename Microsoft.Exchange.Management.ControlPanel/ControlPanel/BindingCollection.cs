using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000591 RID: 1425
	public class BindingCollection : Collection<Binding>
	{
		// Token: 0x060041AB RID: 16811 RVA: 0x000C7E04 File Offset: 0x000C6004
		public string ToJavaScript(IControlResolver resolver)
		{
			StringBuilder stringBuilder = new StringBuilder("[");
			stringBuilder.Append(string.Join(",", (from o in this
			select o.ToJavaScript(resolver)).ToArray<string>()));
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}
	}
}
