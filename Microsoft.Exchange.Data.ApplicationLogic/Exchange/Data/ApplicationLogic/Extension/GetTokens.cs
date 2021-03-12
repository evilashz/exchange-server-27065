using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x02000124 RID: 292
	internal sealed class GetTokens
	{
		// Token: 0x06000BFE RID: 3070 RVA: 0x00031F08 File Offset: 0x00030108
		internal GetTokens(OmexWebServiceUrlsCache urlsCache, TokenRenewSubmitter tokenRenewSubmitter)
		{
			if (urlsCache == null)
			{
				throw new ArgumentNullException("urlsCache");
			}
			if (tokenRenewSubmitter == null)
			{
				throw new ArgumentNullException("tokenRenewSubmitter");
			}
			this.urlsCache = urlsCache;
			this.tokenRenewSubmitter = tokenRenewSubmitter;
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00031F74 File Offset: 0x00030174
		internal void Execute(TokenRenewQueryContext queryContext)
		{
			if (queryContext == null)
			{
				throw new ArgumentNullException("queryContext");
			}
			if (queryContext.TokenRenewRequestAssets == null)
			{
				throw new ArgumentNullException("QueryContext.TokenRenewRequestAssets");
			}
			if (queryContext.TokenRenewRequestAssets.Count == 0)
			{
				throw new ArgumentException("QueryContext.TokenRenewRequestAssets must include 1 or more extensions");
			}
			this.queryContext = queryContext;
			this.downloadQueue = new Queue<TokenRenewRequestAsset>(queryContext.TokenRenewRequestAssets.Count);
			foreach (TokenRenewRequestAsset item in queryContext.TokenRenewRequestAssets)
			{
				this.downloadQueue.Enqueue(item);
			}
			this.downloadToken = new DownloadToken(this.urlsCache);
			this.ExecuteDownload(this.downloadToken);
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00032040 File Offset: 0x00030240
		internal void ExecuteDownload(DownloadToken downloadToken)
		{
			if (this.downloadQueue.Count > 0)
			{
				this.tokensInRequest.Clear();
				while (this.tokensInRequest.Count < 20 && this.downloadQueue.Count > 0)
				{
					TokenRenewRequestAsset item = this.downloadQueue.Dequeue();
					this.tokensInRequest.Add(item);
				}
				downloadToken.Execute(this.tokensInRequest, this.queryContext.DeploymentId, new BaseAsyncCommand.GetLoggedMailboxIdentifierCallback(this.GetLoggedMailboxIdentifier), new DownloadToken.SuccessCallback(this.DownloadTokenSuccessCallback), new BaseAsyncCommand.FailureCallback(this.DownloadTokenFailureCallback));
				return;
			}
			GetTokens.Tracer.TraceDebug(0L, "GetTokens.ExecuteDownload: Downloads complete.");
			this.ExecuteNextTokenRenewQuery();
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x000320F0 File Offset: 0x000302F0
		private void DownloadTokenFailureCallback(Exception exception)
		{
			GetTokens.Tracer.TraceError<Exception>(0L, "GetTokens.DownloadTokenFailureCallback called with exception: {0}", exception);
			foreach (TokenRenewRequestAsset tokenRenewRequestAsset in this.tokensInRequest)
			{
				this.appStatuses[tokenRenewRequestAsset.ExtensionID] = "2.0";
			}
			this.ExecuteDownload(this.downloadToken);
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00032170 File Offset: 0x00030370
		private void DownloadTokenSuccessCallback(Dictionary<string, string> newTokens, Dictionary<string, string> appStatusCodes)
		{
			GetTokens.Tracer.TraceDebug<int>(0L, "GetTokens.DownloadTokenSuccessCallback called for {0} token", this.tokensInRequest.Count);
			foreach (KeyValuePair<string, string> keyValuePair in newTokens)
			{
				this.downloadedTokens.Add(keyValuePair.Key, keyValuePair.Value);
			}
			foreach (KeyValuePair<string, string> keyValuePair2 in appStatusCodes)
			{
				this.appStatuses.Add(keyValuePair2.Key, keyValuePair2.Value);
			}
			this.ExecuteDownload(this.downloadToken);
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x00032248 File Offset: 0x00030448
		private string GetLoggedMailboxIdentifier()
		{
			return ExtensionDiagnostics.GetLoggedMailboxIdentifier(this.queryContext.ExchangePrincipal);
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0003225A File Offset: 0x0003045A
		private void ExecuteNextTokenRenewQuery()
		{
			this.WriteTokensToMailbox();
			this.downloadedTokens.Clear();
			this.appStatuses.Clear();
			this.queryContext = null;
			this.downloadQueue = null;
			this.downloadToken = null;
			this.tokenRenewSubmitter.ExecuteTokenRenewQuery(this);
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x000323D4 File Offset: 0x000305D4
		private void WriteTokensToMailbox()
		{
			if (this.downloadedTokens.Count == 0 && this.appStatuses.Count == 0)
			{
				return;
			}
			GetTokens.Tracer.TraceDebug<int, int>(0L, "GetTokens.WriteTokensToMailbox: Writing renewed tokens for {0} apps, failure error codes for {0} apps.", this.downloadedTokens.Count, this.appStatuses.Count);
			Exception ex = InstalledExtensionTable.RunClientExtensionAction(delegate
			{
				using (MailboxSession mailboxSession = MailboxSession.OpenAsSystemService(this.queryContext.ExchangePrincipal, this.queryContext.CultureInfo, this.queryContext.ClientInfoString))
				{
					using (InstalledExtensionTable installedExtensionTable = InstalledExtensionTable.CreateInstalledExtensionTable(this.queryContext.Domain, this.queryContext.IsUserScope, this.queryContext.OrgEmptyMasterTableCache, mailboxSession))
					{
						foreach (KeyValuePair<string, string> keyValuePair in this.downloadedTokens)
						{
							installedExtensionTable.ConfigureEtoken(keyValuePair.Key, keyValuePair.Value, true);
						}
						foreach (KeyValuePair<string, string> keyValuePair2 in this.appStatuses)
						{
							installedExtensionTable.ConfigureAppStatus(keyValuePair2.Key, keyValuePair2.Value);
						}
						installedExtensionTable.SaveXML();
					}
				}
			});
			if (ex != null)
			{
				GetTokens.Tracer.TraceError<Exception>(0L, "GetTokens.WriteTokensToMailbox: Writing renewed tokens failed. Exception: {0}", ex);
				ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_FailedToWritebackRenewedTokens, null, new object[]
				{
					"ProcessTokenRenew",
					ExtensionDiagnostics.GetLoggedMailboxIdentifier(this.queryContext.ExchangePrincipal),
					ExtensionDiagnostics.GetLoggedExceptionString(ex)
				});
				return;
			}
			ExtensionDiagnostics.LogToDatacenterOnly(ApplicationLogicEventLogConstants.Tuple_ProcessTokenRenewCompleted, null, new object[]
			{
				"ProcessTokenRenew",
				ExtensionDiagnostics.GetLoggedMailboxIdentifier(this.queryContext.ExchangePrincipal)
			});
		}

		// Token: 0x04000621 RID: 1569
		private const string ScenarioProcessTokens = "ProcessTokenRenew";

		// Token: 0x04000622 RID: 1570
		private const string ScenarioGetTokens = "GetTokens";

		// Token: 0x04000623 RID: 1571
		private const int MaxTokensInOneRequest = 20;

		// Token: 0x04000624 RID: 1572
		private static readonly Trace Tracer = ExTraceGlobals.ExtensionTracer;

		// Token: 0x04000625 RID: 1573
		private OmexWebServiceUrlsCache urlsCache;

		// Token: 0x04000626 RID: 1574
		private TokenRenewSubmitter tokenRenewSubmitter;

		// Token: 0x04000627 RID: 1575
		private TokenRenewQueryContext queryContext;

		// Token: 0x04000628 RID: 1576
		private Queue<TokenRenewRequestAsset> downloadQueue;

		// Token: 0x04000629 RID: 1577
		private DownloadToken downloadToken;

		// Token: 0x0400062A RID: 1578
		private Dictionary<string, string> downloadedTokens = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400062B RID: 1579
		private Dictionary<string, string> appStatuses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400062C RID: 1580
		private List<TokenRenewRequestAsset> tokensInRequest = new List<TokenRenewRequestAsset>(20);
	}
}
