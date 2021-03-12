using System;
using System.Collections;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000556 RID: 1366
	internal class EcpTraceHelper
	{
		// Token: 0x06003FDD RID: 16349 RVA: 0x000C0FF4 File Offset: 0x000BF1F4
		public static string GetTraceString(object obj)
		{
			if (obj == null)
			{
				return string.Empty;
			}
			string result;
			if (obj is Exception)
			{
				result = (obj as Exception).ToTraceString();
			}
			else if (obj is PSCommand)
			{
				result = (obj as PSCommand).ToTraceString();
			}
			else if (obj is ErrorRecord[])
			{
				result = (obj as ErrorRecord[]).ToTraceString();
			}
			else if (obj is PowerShellResults<JsonDictionary<object>>)
			{
				result = (obj as PowerShellResults<JsonDictionary<object>>).ToTraceString();
			}
			else if (obj is IDictionary)
			{
				result = (obj as IDictionary).ToTraceString();
			}
			else if (obj is IEnumerable)
			{
				result = (obj as IEnumerable).ToTraceString();
			}
			else
			{
				result = obj.ToString();
			}
			return result;
		}
	}
}
