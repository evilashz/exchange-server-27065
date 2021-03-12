using System;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200007D RID: 125
	internal sealed class EasMailboxSessionCacheHandler : ExchangeDiagnosableWrapper<EasMailboxSessionCacheResult>
	{
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x00025C19 File Offset: 0x00023E19
		protected override string UsageText
		{
			get
			{
				return "The mailbox session cache handler is a diagnostics handler that returns information about the current state of the mailbox session cache. \r\n                        The handler supports dumpcache argument. Below are examples for using this diagnostics handler: ";
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x00025C20 File Offset: 0x00023E20
		protected override string UsageSample
		{
			get
			{
				return "Example 1: Metadata only.\r\n                        Get-ExchangeDiagnosticInfo Process MSExchangeSync Component EASMailboxSessionCache\r\n\r\n                        Example 2: Dump Cache.\r\n                        Get-ExchangeDiagnosticInfo Process MSExchangeSync Component EASMailboxSessionCache -Argument dumpcache";
			}
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00025C28 File Offset: 0x00023E28
		public static EasMailboxSessionCacheHandler GetInstance()
		{
			if (EasMailboxSessionCacheHandler.instance == null)
			{
				lock (EasMailboxSessionCacheHandler.lockObject)
				{
					if (EasMailboxSessionCacheHandler.instance == null)
					{
						EasMailboxSessionCacheHandler.instance = new EasMailboxSessionCacheHandler();
					}
				}
			}
			return EasMailboxSessionCacheHandler.instance;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00025C80 File Offset: 0x00023E80
		private EasMailboxSessionCacheHandler()
		{
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x00025C88 File Offset: 0x00023E88
		protected override string ComponentName
		{
			get
			{
				return "EasMailboxSessionCache";
			}
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00025C8F File Offset: 0x00023E8F
		internal override EasMailboxSessionCacheResult GetExchangeDiagnosticsInfoData(DiagnosableParameters argument)
		{
			if (argument.Argument.ToLower() == "dumpcache")
			{
				return new EasMailboxSessionCacheResult(MailboxSessionCache.GetCacheEntries());
			}
			return new EasMailboxSessionCacheResult(MailboxSessionCache.Count);
		}

		// Token: 0x040004B0 RID: 1200
		private const string DumpCacheArgument = "dumpcache";

		// Token: 0x040004B1 RID: 1201
		private static EasMailboxSessionCacheHandler instance;

		// Token: 0x040004B2 RID: 1202
		private static object lockObject = new object();
	}
}
