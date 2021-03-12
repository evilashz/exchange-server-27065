using System;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000002 RID: 2
	internal sealed class StandardBudgetCacheHandler : ExchangeDiagnosableWrapper<BudgetCacheHandlerMetadata>
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		private StandardBudgetCacheHandler()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		protected override string UsageText
		{
			get
			{
				return "This handler returns metadata about the standard budget cache in a given process.";
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020DF File Offset: 0x000002DF
		protected override string UsageSample
		{
			get
			{
				return "Example 1: Return all budget entries in cache:\\r\\nGet-ExchangeDiagnosticInfo -Process MSExchangeServicesAppPool -Component StandardBudgetCache\r\n\r\nExample 2: Return all budget entries that are locked out:\r\nGet-ExchangeDiagnosticInfo -Process MSExchangeServicesAppPool -Component StandardBudgetCache -Argument \"IsLocked -eq 'true'\"\r\n\r\nExample 3: Return all budget entries johndoe (alias):\r\nGet-ExchangeDiagnosticInfo -Process MSExchangeServicesAppPool -Component StandardBudgetCache -Argument \"Name -like '*johndoe*'\"\r\n\r\nThe argument is a OPath query.  Supported fields:\r\nInCutoff (bool), InMicroDelay (bool), NotThrottled (bool), Connections (int), Balance (float), OutstandingActionCount (int)\r\nIsServiceAccount (bool), ThrottlingPolicy (string), LiveTime (TimeSpan), Name (string), IsGlobalPolicy (bool), IsOrgPolicy (bool)\r\nIsRegularPolicy (bool)\r\n\r\nNOTE: Name is the budget KEY value, not the smtp address.  This will typically map to either the NTAccount or the sid of the user.  You must \r\nuse the -like filter if you wish to be happy.";
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020E6 File Offset: 0x000002E6
		protected override string ComponentName
		{
			get
			{
				return "StandardBudgetCache";
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020F0 File Offset: 0x000002F0
		public static StandardBudgetCacheHandler GetInstance()
		{
			if (StandardBudgetCacheHandler.instance == null)
			{
				lock (StandardBudgetCacheHandler.lockObject)
				{
					if (StandardBudgetCacheHandler.instance == null)
					{
						StandardBudgetCacheHandler.instance = new StandardBudgetCacheHandler();
					}
				}
			}
			return StandardBudgetCacheHandler.instance;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002148 File Offset: 0x00000348
		internal override BudgetCacheHandlerMetadata GetExchangeDiagnosticsInfoData(DiagnosableParameters arguments)
		{
			return StandardBudgetCache.Singleton.GetMetadata((arguments.Argument == null) ? null : arguments.Argument);
		}

		// Token: 0x04000001 RID: 1
		private static StandardBudgetCacheHandler instance;

		// Token: 0x04000002 RID: 2
		private static object lockObject = new object();
	}
}
