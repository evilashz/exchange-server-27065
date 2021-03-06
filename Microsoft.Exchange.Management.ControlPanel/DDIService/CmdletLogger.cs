using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;
using System.Web;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000048 RID: 72
	public static class CmdletLogger
	{
		// Token: 0x060019B4 RID: 6580 RVA: 0x000526E0 File Offset: 0x000508E0
		public static CmdExecuteInfo CaculateLogAndSaveToContext(PowerShell ps, DateTime utcStartTime, Microsoft.Exchange.Management.ControlPanel.ErrorRecord[] errorRecords)
		{
			if (HttpContext.Current.IsCmdletLogEnabled())
			{
				string exception = null;
				CmdletStatus status;
				if (errorRecords != null && errorRecords.Length > 0)
				{
					status = CmdletStatus.Failed;
					StringBuilder stringBuilder = new StringBuilder();
					foreach (Microsoft.Exchange.Management.ControlPanel.ErrorRecord errorRecord in errorRecords)
					{
						stringBuilder = stringBuilder.AppendLine(errorRecord.Message);
					}
					exception = stringBuilder.ToString();
				}
				else if (ps.InvocationStateInfo.State == PSInvocationState.Failed)
				{
					status = CmdletStatus.Failed;
				}
				else if (ps.InvocationStateInfo.State == PSInvocationState.Stopped)
				{
					status = CmdletStatus.Stopped;
				}
				else
				{
					status = CmdletStatus.Completed;
				}
				CmdExecuteInfo cmdExecuteInfo = new CmdExecuteInfo
				{
					LogId = ps.InstanceId.ToString(),
					Status = status,
					StartTime = utcStartTime.UtcToUserDateTimeString(),
					CommandText = ps.Commands.ToTraceString(),
					Exception = exception
				};
				if (HttpContext.Current != null && HttpContext.Current.Items != null)
				{
					List<CmdExecuteInfo> list = HttpContext.Current.Items["Eac_CmdletLogs"] as List<CmdExecuteInfo>;
					if (list == null)
					{
						list = new List<CmdExecuteInfo>();
						HttpContext.Current.Items["Eac_CmdletLogs"] = list;
					}
					list.Add(cmdExecuteInfo);
				}
				return cmdExecuteInfo;
			}
			return null;
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x0005281C File Offset: 0x00050A1C
		public static CmdExecuteInfo[] GetCmdletLogs(this HttpContext context)
		{
			if (context != null && context.Items != null)
			{
				List<CmdExecuteInfo> list = context.Items["Eac_CmdletLogs"] as List<CmdExecuteInfo>;
				if (list != null)
				{
					return list.ToArray();
				}
			}
			return null;
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x00052858 File Offset: 0x00050A58
		private static bool IsCmdletLogEnabled(this HttpContext context)
		{
			return context != null && context.Request != null && context.Request.Cookies["Eac_CmdletLogging"] != null && context.Request.Cookies["Eac_CmdletLogging"].Value == "true";
		}

		// Token: 0x04001ADB RID: 6875
		private const string CmdletLogContextProperty = "Eac_CmdletLogs";

		// Token: 0x04001ADC RID: 6876
		internal const string EnableCmdletLoggingCookie = "Eac_CmdletLogging";
	}
}
