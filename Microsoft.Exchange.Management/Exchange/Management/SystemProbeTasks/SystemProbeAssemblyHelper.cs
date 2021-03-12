using System;
using System.IO;
using System.Management.Automation;
using System.Reflection;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemProbeTasks
{
	// Token: 0x02000DA4 RID: 3492
	internal static class SystemProbeAssemblyHelper
	{
		// Token: 0x060085D6 RID: 34262 RVA: 0x00223A90 File Offset: 0x00221C90
		public static void Invoke(Task task, Action<Type> action)
		{
			try
			{
				Assembly assembly = Assembly.Load("Microsoft.Exchange.Hygiene.Data");
				Type type = assembly.GetType("Microsoft.Exchange.Hygiene.Data.SystemProbe.SystemProbeData");
				action(type);
			}
			catch (IOException ex)
			{
				task.WriteError(new FailedToSystemProbeCmdletException(ex.ToString()), ErrorCategory.ObjectNotFound, null);
			}
			catch (SecurityException ex2)
			{
				task.WriteError(new FailedToSystemProbeCmdletException(ex2.ToString()), ExchangeErrorCategory.Authorization, null);
			}
			catch (UnauthorizedAccessException ex3)
			{
				task.WriteError(new FailedToSystemProbeCmdletException(ex3.ToString()), ExchangeErrorCategory.Authorization, null);
			}
		}
	}
}
