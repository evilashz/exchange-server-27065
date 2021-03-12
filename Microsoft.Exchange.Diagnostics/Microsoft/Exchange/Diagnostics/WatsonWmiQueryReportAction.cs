using System;
using System.Management;
using System.Text;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000DA RID: 218
	internal class WatsonWmiQueryReportAction : WatsonReportAction
	{
		// Token: 0x0600060D RID: 1549 RVA: 0x0001996D File Offset: 0x00017B6D
		public WatsonWmiQueryReportAction(string query) : base(query, false)
		{
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x00019977 File Offset: 0x00017B77
		public override string ActionName
		{
			get
			{
				return "WMI Query";
			}
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00019980 File Offset: 0x00017B80
		public override string Evaluate(WatsonReport watsonReport)
		{
			string text = string.Concat(new string[]
			{
				"--- ",
				base.Expression,
				" ---\r\n",
				WatsonWmiQueryReportAction.EvaluateWmiQuery(base.Expression),
				"--- END ---"
			});
			watsonReport.LogExtraData(text);
			return text;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x000199D4 File Offset: 0x00017BD4
		private static string EvaluateWmiQuery(string query)
		{
			string result;
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(query))
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
					{
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						using (managementObject)
						{
							stringBuilder.AppendLine(managementObject.ToString());
						}
					}
					result = stringBuilder.ToString();
				}
			}
			catch (Exception ex)
			{
				result = string.Concat(new string[]
				{
					"Error evaluating WMI query: ",
					ex.GetType().Name,
					" (",
					ex.Message,
					")\r\n"
				});
			}
			return result;
		}
	}
}
