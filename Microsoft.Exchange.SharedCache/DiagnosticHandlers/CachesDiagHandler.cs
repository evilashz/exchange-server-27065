using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.SharedCache.DiagnosticHandlers
{
	// Token: 0x02000012 RID: 18
	internal sealed class CachesDiagHandler : ExchangeDiagnosableWrapper<List<CacheOperationResult>>
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003616 File Offset: 0x00001816
		protected override string ComponentName
		{
			get
			{
				return "Caches";
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000361D File Offset: 0x0000181D
		protected override string UsageSample
		{
			get
			{
				return "This handler allows the querying and removal of cache entries. Three operations are supported: GET, FIND, and DELETE.  Get and Delete operations require the exact key name while find can do a wildcard search (minimum of 3 chars).";
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003624 File Offset: 0x00001824
		protected override string UsageText
		{
			get
			{
				return " Example 1: Returns the cache entry for the key \"LiveIdMemberName~admin@rifuller.net\" from the AnchorMailboxCache\r\n                        Get-ExchangeDiagnosticInfo -Process Microsoft.Exchange.SharedCache -Component Caches -Argument \"GET LiveIdMemberName~admin@rifuller.net FROM AnchorMailboxCache\"\r\n\r\n                        Example 2: Returns cache entries that match the partial key \"admin@rifuller.net\" from the AnchorMailboxCache (maximum returned: " + CacheSettings.MaxResults + ")\r\n                        Get-ExchangeDiagnosticInfo -Process Microsoft.Exchange.SharedCache -Component Caches -Argument \"FIND admin@rifuller.net FROM AnchorMailboxCache\"\r\n\r\n                        Example 2: Returns and removes the cache entry for the key \" \" from the MailboxServerLocator cache\r\n                        Get-ExchangeDiagnosticInfo -Process Microsoft.Exchange.SharedCache -Component Caches -Argument \"DELETE MailboxGuid~3aa8258c-339c-430f-b2dc-f0c7ba5f5e83 FROM MailboxServerLocator\"";
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000363C File Offset: 0x0000183C
		public static CachesDiagHandler GetInstance()
		{
			if (CachesDiagHandler.instance == null)
			{
				lock (CachesDiagHandler.lockObject)
				{
					if (CachesDiagHandler.instance == null)
					{
						CachesDiagHandler.instance = new CachesDiagHandler();
					}
				}
			}
			return CachesDiagHandler.instance;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003694 File Offset: 0x00001894
		internal override List<CacheOperationResult> GetExchangeDiagnosticsInfoData(DiagnosableParameters arguments)
		{
			List<CacheOperationResult> list = new List<CacheOperationResult>();
			OperationQuery operationQuery;
			CacheOperationResult item;
			if (!OperationQuery.TryCreate(arguments.Argument, out operationQuery, out item))
			{
				list.Add(item);
			}
			else
			{
				list = operationQuery.Execute();
			}
			return list;
		}

		// Token: 0x0400003B RID: 59
		private static CachesDiagHandler instance;

		// Token: 0x0400003C RID: 60
		private static object lockObject = new object();
	}
}
