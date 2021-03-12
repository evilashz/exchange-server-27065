using System;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x02000034 RID: 52
	internal sealed class EwsBudgetCacheHandler : ExchangeDiagnosableWrapper<BudgetCacheHandlerMetadata>
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x0000A601 File Offset: 0x00008801
		private EwsBudgetCacheHandler()
		{
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000A609 File Offset: 0x00008809
		protected override string UsageText
		{
			get
			{
				return "This handler returns metadata about the EWS budget cache in a given process.";
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000A610 File Offset: 0x00008810
		protected override string UsageSample
		{
			get
			{
				return "Example 1: Return all budget entries in cache:\\r\\nGet-ExchangeDiagnosticInfo -Process MSExchangeServicesAppPool -Component EwsBudgetCache\r\n\r\nExample 2: Return all budget entries that are locked out:\r\nGet-ExchangeDiagnosticInfo -Process MSExchangeServicesAppPool -Component EwsBudgetCache -Argument \"IsLocked -eq 'true'\"\r\n\r\nExample 3: Return all budget entries johndoe (alias):\r\nGet-ExchangeDiagnosticInfo -Process MSExchangeServicesAppPool -Component EwsBudgetCache -Argument \"Name -like '*johndoe*'\"\r\n\r\nThe argument is a OPath query.  Supported fields:\r\nInCutoff (bool), InMicroDelay (bool), NotThrottled (bool), Connections (int), Balance (float), OutstandingActionCount (int)\r\nIsServiceAccount (bool), ThrottlingPolicy (string), LiveTime (TimeSpan), Name (string), IsGlobalPolicy (bool), IsOrgPolicy (bool)\r\nIsRegularPolicy (bool)\r\n\r\nNOTE: Name is the budget KEY value, not the smtp address.  This will typically map to either the NTAccount or the sid of the user.  You must \r\nuse the -like filter if you wish to be happy.";
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000A617 File Offset: 0x00008817
		protected override string ComponentName
		{
			get
			{
				return "EwsBudgetCache";
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000A620 File Offset: 0x00008820
		public static EwsBudgetCacheHandler GetInstance()
		{
			if (EwsBudgetCacheHandler.instance == null)
			{
				lock (EwsBudgetCacheHandler.lockObject)
				{
					if (EwsBudgetCacheHandler.instance == null)
					{
						EwsBudgetCacheHandler.instance = new EwsBudgetCacheHandler();
					}
				}
			}
			return EwsBudgetCacheHandler.instance;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000A678 File Offset: 0x00008878
		internal override BudgetCacheHandlerMetadata GetExchangeDiagnosticsInfoData(DiagnosableParameters arguments)
		{
			return EwsBudgetCache.Singleton.GetMetadata((arguments.Argument == null) ? null : arguments.Argument);
		}

		// Token: 0x0400022A RID: 554
		private static EwsBudgetCacheHandler instance;

		// Token: 0x0400022B RID: 555
		private static object lockObject = new object();
	}
}
