using System;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.ServicesServerTasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000EB RID: 235
	internal class Globals
	{
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x0001CC5F File Offset: 0x0001AE5F
		public static ExchangeResourceManager ResourceManager
		{
			get
			{
				return Globals.resourceManager;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0001CC66 File Offset: 0x0001AE66
		public static Guid ComponentGuid
		{
			get
			{
				return ExTraceGlobals.TaskTracer.Category;
			}
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001CC74 File Offset: 0x0001AE74
		public static string PowerShellStringFromMultivaluedParameter(object[] values)
		{
			if (values == null || values.Length == 0)
			{
				return "$null";
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < values.Length; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(',');
				}
				string text = (values[i] == null) ? null : values[i].ToString();
				if (text != null)
				{
					text = text.Replace("`", "``");
					text = text.Replace("$", "`$");
					text = text.Replace("\"", "`\"");
					stringBuilder.AppendFormat("\"{0}\"", text);
				}
				else
				{
					stringBuilder.Append("");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001CD18 File Offset: 0x0001AF18
		public static string PowerShellArrayFromStringArray(string[] values)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("@(");
			bool flag = true;
			foreach (string text in values)
			{
				string arg = text.Replace("'", "''");
				if (flag)
				{
					stringBuilder.AppendFormat("'{0}'", arg);
				}
				else
				{
					stringBuilder.AppendFormat(",'{0}'", arg);
				}
				flag = false;
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x0400034D RID: 845
		private static readonly ExchangeResourceManager resourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Management.Tasks.Strings", Assembly.GetExecutingAssembly());
	}
}
