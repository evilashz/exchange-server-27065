using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.CalendarSharing.Probes
{
	// Token: 0x02000059 RID: 89
	public class OWACalendarAppPoolProbe : ProbeWorkItem
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00013194 File Offset: 0x00011394
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x0001319C File Offset: 0x0001139C
		public HttpWebRequestUtility WebRequestUtil { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x000131A5 File Offset: 0x000113A5
		// (set) Token: 0x060002DA RID: 730 RVA: 0x000131AD File Offset: 0x000113AD
		public string ExpectedStatusCode { get; private set; }

		// Token: 0x060002DB RID: 731 RVA: 0x000131B8 File Offset: 0x000113B8
		protected override void DoWork(CancellationToken cancellationToken)
		{
			this.Configure();
			HttpWebResponse httpWebResponse = null;
			try
			{
				HttpWebRequest request = this.GetRequest();
				Task<WebResponse> task = this.WebRequestUtil.SendRequest(request);
				httpWebResponse = (HttpWebResponse)task.Result;
				if (!(httpWebResponse.StatusCode.ToString() == this.ExpectedStatusCode))
				{
					throw new AggregateException(new Exception[]
					{
						new WebException("Non-242 response received", new Exception(string.Format("Unexpected response received. Expected 242 received {0}", httpWebResponse.StatusCode)), WebExceptionStatus.UnknownError, httpWebResponse)
					});
				}
				base.Result.StateAttribute5 = 0.ToString();
			}
			catch (AggregateException ex)
			{
				WebException ex2 = ex.Flatten().InnerException as WebException;
				if (ex2 != null)
				{
					base.Result.StateAttribute5 = ex2.GetType().ToString();
					httpWebResponse = (HttpWebResponse)ex2.Response;
					string text = httpWebResponse.StatusCode.ToString();
					base.Result.StateAttribute4 = text;
					if (this.ErrorCodesToBeIgnored.Contains(text))
					{
						base.Result.StateAttribute5 = "KnownErrorWasIgnored";
						return;
					}
					ProbeResult result = base.Result;
					result.ExecutionContext += ex.Message;
				}
				throw;
			}
			finally
			{
				if (httpWebResponse != null)
				{
					httpWebResponse.Close();
				}
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0001335C File Offset: 0x0001155C
		private void Configure()
		{
			this.ExpectedStatusCode = this.ReadAttribute("ExpectedStatusCode", "242");
			string text = this.ReadAttribute("KnownErrorCodes", string.Empty);
			if (!string.IsNullOrEmpty(text))
			{
				char[] separator = new char[]
				{
					','
				};
				text.Split(separator, 100).ToList<string>().ForEach(delegate(string r)
				{
					this.ErrorCodesToBeIgnored.Add(r.Trim());
				});
				this.ErrorCodesToBeIgnored.RemoveAll((string r) => string.IsNullOrWhiteSpace(r));
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x000133F8 File Offset: 0x000115F8
		protected HttpWebRequest GetRequest()
		{
			this.WebRequestUtil = new HttpWebRequestUtility(base.TraceContext);
			HttpWebRequest httpWebRequest = this.WebRequestUtil.CreateBasicHttpWebRequest(base.Definition.Endpoint, false);
			httpWebRequest.ContentType = "text/xml";
			httpWebRequest.Method = "GET";
			return httpWebRequest;
		}

		// Token: 0x04000223 RID: 547
		public const string KnownErrorCodesKey = "KnownErrorCodes";

		// Token: 0x04000224 RID: 548
		public const string ExpectedStatusCodeKey = "ExpectedStatusCode";

		// Token: 0x04000225 RID: 549
		public const string KnownErrorWasIgnored = "KnownErrorWasIgnored";

		// Token: 0x04000226 RID: 550
		private const string DefaultExpectedStatusCode = "242";

		// Token: 0x04000227 RID: 551
		protected List<string> ErrorCodesToBeIgnored = new List<string>
		{
			HttpStatusCode.OK.ToString()
		};
	}
}
