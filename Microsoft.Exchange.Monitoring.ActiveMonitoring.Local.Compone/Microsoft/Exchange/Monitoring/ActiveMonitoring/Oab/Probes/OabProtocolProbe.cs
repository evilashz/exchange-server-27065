using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Oab.Probes
{
	// Token: 0x0200023F RID: 575
	public class OabProtocolProbe : OabBaseLocalProbe
	{
		// Token: 0x0600100A RID: 4106 RVA: 0x0006BAB4 File Offset: 0x00069CB4
		protected override void DoWork(CancellationToken cancellationToken)
		{
			HttpWebRequest request = base.GetRequest();
			request.Headers.Add("X-WLID-MemberName", base.Definition.Account);
			Task<WebResponse> task = base.WebRequestUtil.SendRequest(request);
			try
			{
				WebResponse result = task.Result;
			}
			catch (AggregateException ex)
			{
				WebException ex2 = ex.Flatten().InnerException as WebException;
				HttpWebResponse httpWebResponse = (HttpWebResponse)ex2.Response;
				if (httpWebResponse.StatusCode != HttpStatusCode.Forbidden)
				{
					base.Result.StateAttribute5 = httpWebResponse.StatusCode.ToString();
					throw ex2;
				}
			}
		}
	}
}
