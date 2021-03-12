using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.Autodiscover
{
	// Token: 0x02000028 RID: 40
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class TryUnauthenticatedGet : AutodiscoverStep
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00004250 File Offset: 0x00002450
		protected internal TryUnauthenticatedGet(EasConnectionSettings easConnectionSettings) : base(easConnectionSettings, Step.TryDnsLookupOfSrvRecord)
		{
			this.WebRequestTimeout = new TimeSpan(0, 0, 10).Milliseconds;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000EC RID: 236 RVA: 0x0000427D File Offset: 0x0000247D
		internal override bool UseSsl
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004280 File Offset: 0x00002480
		protected override string RequestMethodName
		{
			get
			{
				return "GET";
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00004287 File Offset: 0x00002487
		// (set) Token: 0x060000EF RID: 239 RVA: 0x0000428F File Offset: 0x0000248F
		private int WebRequestTimeout { get; set; }

		// Token: 0x060000F0 RID: 240 RVA: 0x00004298 File Offset: 0x00002498
		public override Step ExecuteStep(StepContext stepContext)
		{
			string autodiscoverDomain = base.GetAutodiscoverDomain(base.EasConnectionSettings.EasEndpointSettings.Domain);
			stepContext.ProbeStack.Push(autodiscoverDomain);
			return base.PrimitiveExecuteStep(stepContext);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000042CF File Offset: 0x000024CF
		protected override bool IsStepAllowable(StepContext stepContext)
		{
			return stepContext.Request.AutodiscoverOption != AutodiscoverOption.ExistingEndpoint;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000042E2 File Offset: 0x000024E2
		protected override HttpWebRequest ConditionWebRequest(HttpWebRequest webRequest)
		{
			webRequest.Method = this.RequestMethodName;
			webRequest.UserAgent = base.EasConnectionSettings.UserAgent;
			webRequest.AllowAutoRedirect = false;
			webRequest.PreAuthenticate = false;
			webRequest.Timeout = 10000;
			return webRequest;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000431B File Offset: 0x0000251B
		protected override void AddWebRequestBody(HttpWebRequest webRequest, AutodiscoverRequest easRequest)
		{
		}
	}
}
