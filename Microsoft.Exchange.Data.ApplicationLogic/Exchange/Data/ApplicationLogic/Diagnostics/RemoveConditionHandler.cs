using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.Diagnostics
{
	// Token: 0x020000E2 RID: 226
	internal sealed class RemoveConditionHandler : ExchangeDiagnosableWrapper<RemoveConditionResult>
	{
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x00025593 File Offset: 0x00023793
		protected override string UsageText
		{
			get
			{
				return "The RemoveConditional handler does what its name suggests – it “unregisters” or removes active conditionals.  The component or “method name” to use here is “RemoveCondition”.. Below are examples for using this diagnostics handler: ";
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x0002559A File Offset: 0x0002379A
		protected override string UsageSample
		{
			get
			{
				return "Example 1: To remove a single conditional, the argument must be in the format: Cookie=<GUID>\r\n                                    Get-ExchangeDiagnosticInfo -Process MSExchangeSyncAppPool -Component RemoveCondition –Argument \"cookie=0d341cfc-8577-4677-aa73-187b9ba6cc5c\"\r\n\r\n                                    Example 2: To remove ALL conditionals, pass in “removeall” as the argument.  The response will include all of the conditionals that were removed.\r\n                                    Get-ExchangeDiagnosticInfo -Process MSExchangeSyncAppPool -Component RemoveCondition –Argument removeall";
			}
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x000255A4 File Offset: 0x000237A4
		public static RemoveConditionHandler GetInstance()
		{
			if (RemoveConditionHandler.instance == null)
			{
				lock (RemoveConditionHandler.lockObject)
				{
					if (RemoveConditionHandler.instance == null)
					{
						RemoveConditionHandler.instance = new RemoveConditionHandler();
					}
				}
			}
			return RemoveConditionHandler.instance;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x000255FC File Offset: 0x000237FC
		private RemoveConditionHandler()
		{
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x00025604 File Offset: 0x00023804
		protected override string ComponentName
		{
			get
			{
				return "RemoveCondition";
			}
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0002560C File Offset: 0x0002380C
		internal override RemoveConditionResult GetExchangeDiagnosticsInfoData(DiagnosableParameters argument)
		{
			if (string.IsNullOrEmpty(argument.Argument))
			{
				throw new ArgumentException("RemoveCondition requires an argument with either 'cookie=[guid]' or 'removeall'.");
			}
			if (argument.Argument.Trim().ToLower().StartsWith("cookie="))
			{
				return this.GetRemove(argument.Argument);
			}
			if (argument.Argument.Trim().ToLower().StartsWith("removeall"))
			{
				return this.GetAllRemove();
			}
			throw new ArgumentException("RemoveCondition requires an argument with either 'cookie=[guid]' or 'removeall'.  Encountered: " + argument.Argument);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00025698 File Offset: 0x00023898
		internal RemoveConditionResult GetRemove(string argument)
		{
			int num = argument.IndexOf('=');
			string cookie = argument.Substring(num + 1);
			ConditionalRegistration conditionalRegistration = ConditionalRegistrationCache.Singleton.GetRegistration(cookie) as ConditionalRegistration;
			bool removed = false;
			if (conditionalRegistration != null)
			{
				removed = ConditionalRegistrationCache.Singleton.Remove(cookie);
			}
			return new RemoveConditionResult(cookie, removed);
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x000256E4 File Offset: 0x000238E4
		internal RemoveConditionResult GetAllRemove()
		{
			List<string> allKeys = ConditionalRegistrationCache.Singleton.GetAllKeys();
			for (int i = allKeys.Count - 1; i >= 0; i--)
			{
				string cookie = allKeys[i];
				ConditionalRegistration conditionalRegistration = ConditionalRegistrationCache.Singleton.GetRegistration(cookie) as ConditionalRegistration;
				if (conditionalRegistration != null)
				{
					ConditionalRegistrationCache.Singleton.Remove(cookie);
				}
				else
				{
					allKeys.RemoveAt(i);
				}
			}
			return new RemoveConditionResult(allKeys);
		}

		// Token: 0x0400047A RID: 1146
		private const string CookieArg = "cookie=";

		// Token: 0x0400047B RID: 1147
		private const string RemoveAllArg = "removeall";

		// Token: 0x0400047C RID: 1148
		private static RemoveConditionHandler instance;

		// Token: 0x0400047D RID: 1149
		private static object lockObject = new object();
	}
}
