using System;
using System.Configuration;
using System.Threading;
using Microsoft.Exchange.LogUploaderProxy;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000004 RID: 4
	internal static class ExchangeComponent
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600008C RID: 140 RVA: 0x0000302A File Offset: 0x0000122A
		public static string Name
		{
			get
			{
				return ExchangeComponent.name ?? ExchangeComponent.ReadName();
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000303C File Offset: 0x0000123C
		private static string ReadName()
		{
			try
			{
				ExchangeComponent.name = ConfigurationManager.AppSettings["ExchangeComponent"];
			}
			catch (ConfigurationErrorsException arg)
			{
				string text = string.Format("Fail to read config value, default values are used. The error is {0}", arg);
				EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_ConfigSettingNotFound, Thread.CurrentThread.Name, new object[]
				{
					text
				});
				ExchangeComponent.name = "MessageTracing";
			}
			bool flag = string.IsNullOrWhiteSpace(ExchangeComponent.name);
			if (flag)
			{
				string text2 = string.Format("Fail to read Exchange Component name, default values is used.", new object[0]);
				EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_ConfigSettingNotFound, Thread.CurrentThread.Name, new object[]
				{
					text2
				});
				ExchangeComponent.name = "MessageTracing";
			}
			return ExchangeComponent.name;
		}

		// Token: 0x04000030 RID: 48
		private const string MessageTracingComponentName = "MessageTracing";

		// Token: 0x04000031 RID: 49
		private static string name;
	}
}
