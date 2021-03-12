using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Web.Services.Protocols;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;
using Microsoft.Exchange.Diagnostics.Performance;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.SoapWebClient;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200004F RID: 79
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UpdateGroupMailboxEwsBinding : PrivateExchangeServiceBinding
	{
		// Token: 0x060004EA RID: 1258 RVA: 0x00015C28 File Offset: 0x00013E28
		internal UpdateGroupMailboxEwsBinding(ADUser group, Uri ewsEndpoint) : base("GroupMailboxCmdlet", new RemoteCertificateValidationCallback(CommonCertificateValidationCallbacks.InternalServerToServer))
		{
			ArgumentValidator.ThrowIfNull("group", group);
			ArgumentValidator.ThrowIfNull("ewsEndpointUrl", ewsEndpoint);
			base.Url = ewsEndpoint.ToString();
			base.HttpHeaders[WellKnownHeader.EWSTargetVersion] = EwsTargetVersion.V2_14;
			base.UserAgent = "GroupMailboxCmdlet";
			base.Proxy = new WebProxy();
			base.SetClientRequestIdHeaderFromActivityId();
			using (new StopwatchPerformanceTracker("UpdateGroupMailboxEwsBinding.CreateNetworkService", GenericCmdletInfoDataLogger.Instance))
			{
				base.Authenticator = SoapHttpClientAuthenticator.CreateNetworkService();
			}
			using (new StopwatchPerformanceTracker("UpdateGroupMailboxEwsBinding.InitNetworkServiceImpersonator", GenericCmdletInfoDataLogger.Instance))
			{
				NetworkServiceImpersonator.Initialize();
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00015D0C File Offset: 0x00013F0C
		private UpdateGroupMailboxEwsBinding() : base("GroupMailboxCmdlet", new RemoteCertificateValidationCallback(CommonCertificateValidationCallbacks.InternalServerToServer))
		{
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00015D28 File Offset: 0x00013F28
		public static void InitializeXmlSerializer()
		{
			using (new UpdateGroupMailboxEwsBinding())
			{
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00015E48 File Offset: 0x00014048
		public static void ExecuteEwsOperationWithRetry(string taskName, Action function)
		{
			UpdateGroupMailboxEwsBinding.<>c__DisplayClass6 CS$<>8__locals1 = new UpdateGroupMailboxEwsBinding.<>c__DisplayClass6();
			CS$<>8__locals1.taskName = taskName;
			CS$<>8__locals1.function = function;
			CS$<>8__locals1.retryCount = 0;
			CS$<>8__locals1.taskCompleted = false;
			while (!CS$<>8__locals1.taskCompleted)
			{
				ILUtil.DoTryFilterCatch(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<ExecuteEwsOperationWithRetry>b__0)), new FilterDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<ExecuteEwsOperationWithRetry>b__1)), new CatchDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<ExecuteEwsOperationWithRetry>b__2)));
			}
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00015EE0 File Offset: 0x000140E0
		public UpdateGroupMailboxResponseType ExecuteUpdateGroupMailboxWithRetry(UpdateGroupMailboxType request)
		{
			UpdateGroupMailboxResponseType response = null;
			UpdateGroupMailboxEwsBinding.ExecuteEwsOperationWithRetry("ExecuteUpdateGroupMailboxWithRetry", delegate
			{
				response = this.UpdateGroupMailbox(request);
			});
			return response;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00015F24 File Offset: 0x00014124
		private static bool IsTransientFailure(Exception e)
		{
			return e is TransientException || e is BackEndLocatorException || e is IOException || e is WebException || e is SoapException;
		}

		// Token: 0x0400013E RID: 318
		private const int MaximumTransientFailureRetries = 3;

		// Token: 0x0400013F RID: 319
		private const string ComponentId = "GroupMailboxCmdlet";

		// Token: 0x04000140 RID: 320
		private static readonly Trace Tracer = ExTraceGlobals.CmdletsTracer;
	}
}
