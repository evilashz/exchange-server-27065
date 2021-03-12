using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000FB RID: 251
	public static class DAGUtility
	{
		// Token: 0x06001EE1 RID: 7905 RVA: 0x0005C9FC File Offset: 0x0005ABFC
		public static string ExtractDAGNameFromDAGNetworkIdentity(object rawIdentity)
		{
			if (rawIdentity == null)
			{
				return string.Empty;
			}
			string text = rawIdentity as string;
			string[] array = text.Split(new char[]
			{
				'\\'
			});
			if (array.Length == 2 && !string.IsNullOrEmpty(array[0]) && !string.IsNullOrEmpty(array[1]))
			{
				return array[0];
			}
			return string.Empty;
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x0005CA50 File Offset: 0x0005AC50
		public static Identity ComposeDAGNetworkIdentity(object dagName, object dagNetworkName)
		{
			string rawIdentity = dagName + "\\" + dagNetworkName;
			return new Identity(rawIdentity, (string)dagNetworkName);
		}
	}
}
