using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x02000011 RID: 17
	internal sealed class PopImapServiceHealthHandler : ExchangeDiagnosableWrapper<PopImapServiceHealth>
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00005034 File Offset: 0x00003234
		protected override string UsageText
		{
			get
			{
				return "This diagnostics handler returns info about PopImap health. The handler supports \"ShowError\" argument to return error details. Below are examples for using this diagnostics handler: ";
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0000503C File Offset: 0x0000323C
		protected override string UsageSample
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder("Example 1: Returns POp3 Service health without error details.");
				stringBuilder.AppendLine("Get-ExchangeDiagnosticInfo -Process Microsoft.Exchange.Pop3 -Component PopImapServiceHealth");
				stringBuilder.AppendLine();
				stringBuilder.AppendLine("Example 2: Returns Pop3 Service health with error details.");
				stringBuilder.AppendLine("Get-ExchangeDiagnosticInfo -Process Microsoft.Exchange.Pop3 -Component PopImapServiceHealth -Argument ShowError");
				stringBuilder.AppendLine();
				stringBuilder.AppendLine("Returns IMAP4 Service health without error details.");
				stringBuilder.AppendLine("Get-ExchangeDiagnosticInfo -Process Microsoft.Exchange.Imap4 -Component PopImapServiceHealth");
				stringBuilder.AppendLine();
				stringBuilder.AppendLine("Example 2: Returns IMAP4 Service health with error details.");
				stringBuilder.AppendLine("Get-ExchangeDiagnosticInfo -Process Microsoft.Exchange.Imap4 -Component PopImapServiceHealth -Argument ShowError");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000050C4 File Offset: 0x000032C4
		public static PopImapServiceHealthHandler GetInstance()
		{
			if (PopImapServiceHealthHandler.instance == null)
			{
				lock (PopImapServiceHealthHandler.lockObject)
				{
					if (PopImapServiceHealthHandler.instance == null)
					{
						PopImapServiceHealthHandler.instance = new PopImapServiceHealthHandler();
					}
				}
			}
			return PopImapServiceHealthHandler.instance;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000511C File Offset: 0x0000331C
		private PopImapServiceHealthHandler()
		{
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00005124 File Offset: 0x00003324
		protected override string ComponentName
		{
			get
			{
				return "PopImapServiceHealth";
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005198 File Offset: 0x00003398
		internal override PopImapServiceHealth GetExchangeDiagnosticsInfoData(DiagnosableParameters arguments)
		{
			PopImapServiceHealth serviceHealth = new PopImapServiceHealth();
			serviceHealth.ServerName = Environment.MachineName;
			if (PopImapRequestCache.Instance.Values.Count == 0)
			{
				return serviceHealth;
			}
			serviceHealth.AverageLdapLatency = PopImapRequestCache.Instance.Values.Average((PopImapRequestData request) => request.LdapLatency);
			serviceHealth.AverageRpcLatency = PopImapRequestCache.Instance.Values.Average((PopImapRequestData request) => request.RpcLatency);
			serviceHealth.NumberOfErroredRequests = (from request in PopImapRequestCache.Instance.Values
			where request.HasErrors || (request.ResponseType != null && !string.Equals(request.ResponseType, "OK", StringComparison.InvariantCultureIgnoreCase))
			select request).Count<PopImapRequestData>();
			serviceHealth.NumberOfRequests = (long)PopImapRequestCache.Instance.Values.Count;
			serviceHealth.AverageRequestTime = PopImapRequestCache.Instance.Values.Average((PopImapRequestData request) => request.RequestTime);
			serviceHealth.OKResponseRatio = ((serviceHealth.NumberOfRequests > 0L) ? ((double)(serviceHealth.NumberOfRequests - (long)serviceHealth.NumberOfErroredRequests) / (double)serviceHealth.NumberOfRequests) : 0.0);
			if (arguments.Argument.Equals("ShowError", StringComparison.InvariantCultureIgnoreCase))
			{
				serviceHealth.ErrorDetails = new List<ErrorDetail>();
				PopImapRequestCache.Instance.Values.ForEach(delegate(PopImapRequestData request)
				{
					if (request.ErrorDetails != null)
					{
						serviceHealth.ErrorDetails.AddRange(request.ErrorDetails);
					}
				});
			}
			return serviceHealth;
		}

		// Token: 0x04000080 RID: 128
		private const string ShowErrorArgument = "ShowError";

		// Token: 0x04000081 RID: 129
		private static PopImapServiceHealthHandler instance;

		// Token: 0x04000082 RID: 130
		private static object lockObject = new object();
	}
}
