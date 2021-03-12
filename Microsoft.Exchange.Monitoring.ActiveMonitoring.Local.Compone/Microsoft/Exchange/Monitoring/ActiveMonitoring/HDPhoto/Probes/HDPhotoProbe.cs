using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HDPhoto.Probes
{
	// Token: 0x02000184 RID: 388
	public abstract class HDPhotoProbe : ProbeWorkItem
	{
		// Token: 0x06000B47 RID: 2887 RVA: 0x00048084 File Offset: 0x00046284
		protected override void DoWork(CancellationToken cancellationToken)
		{
			Stopwatch stopwatch = new Stopwatch();
			Stopwatch stopwatch2 = new Stopwatch();
			base.Result.StateAttribute2 = string.Format("Requester = {0} \t Publisher = {1}", base.Definition.Account, base.Definition.SecondaryAccount);
			base.Result.StateAttribute3 = base.Definition.AccountDisplayName;
			base.Result.StateAttribute4 = base.Definition.SecondaryAccountDisplayName;
			try
			{
				this.ConfigureProbe();
				stopwatch.Start();
				NetworkCredential credential = this.GetCredential(base.Definition.Account, base.Definition.AccountPassword);
				NetworkCredential credential2 = this.GetCredential(base.Definition.SecondaryAccount, base.Definition.SecondaryAccountPassword);
				bool flag = false;
				try
				{
					flag = this.UploadPhoto(base.Definition.SecondaryEndpoint, credential2);
				}
				catch (Exception e)
				{
					this.LogExceptionInfoOnResult(e);
				}
				if (flag)
				{
					stopwatch2.Start();
					WTFDiagnostics.TraceInformation(ExTraceGlobals.HDPhotoTracer, base.TraceContext, string.Format("Requesting User photo from {0} to {1} ", base.Definition.Account, base.Definition.SecondaryAccount), null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HDPhoto\\hdphotoprobe.cs", 90);
					this.GetUserPhoto(credential, base.Definition.SecondaryAccount);
					stopwatch2.Stop();
					base.Result.SampleValue = (double)stopwatch2.ElapsedMilliseconds;
				}
			}
			catch (Exception e2)
			{
				this.LogExceptionInfoOnResult(e2);
				throw;
			}
			finally
			{
				stopwatch.Stop();
				base.Result.StateAttribute7 = (double)stopwatch.ElapsedMilliseconds;
			}
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00048224 File Offset: 0x00046424
		protected void LogExceptionInfoOnResult(Exception e)
		{
			base.Result.StateAttribute5 = e.GetType().Name;
			base.Result.Exception = ((e.InnerException != null) ? e.InnerException.GetType().Name : e.GetType().Name);
			base.Result.FailureContext = ((e.InnerException != null) ? e.InnerException.Message : e.Message);
		}

		// Token: 0x06000B49 RID: 2889
		protected abstract bool UploadPhoto(string endpoint, NetworkCredential credential, MemoryStream stream, string identity);

		// Token: 0x06000B4A RID: 2890
		protected abstract HttpWebRequest GetEwsRequest(NetworkCredential credential, string publisherEmail);

		// Token: 0x06000B4B RID: 2891 RVA: 0x0004829D File Offset: 0x0004649D
		protected virtual void ConfigureProbe()
		{
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x000482A0 File Offset: 0x000464A0
		private bool UploadPhoto(string endpoint, NetworkCredential credential)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.HDPhotoTracer, base.TraceContext, "Running Set-UserPhoto", null, "UploadPhoto", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HDPhoto\\hdphotoprobe.cs", 153);
			bool result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int width = 96;
				int height = 96;
				Bitmap bitmap = new Bitmap(width, height);
				Graphics.FromImage(bitmap).FillRectangle(new SolidBrush(Color.Blue), 0, 0, width, height);
				bitmap.Save(memoryStream, ImageFormat.Jpeg);
				string identity = string.Format("{0}\\{1}", credential.Domain, credential.UserName);
				result = this.UploadPhoto(endpoint, credential, memoryStream, identity);
			}
			return result;
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x00048508 File Offset: 0x00046708
		private void GetUserPhoto(NetworkCredential credential, string publisherEmail)
		{
			HttpWebRequest ewsRequest = this.GetEwsRequest(credential, publisherEmail);
			HDPhotoProbe.RequestState requestState = new HDPhotoProbe.RequestState
			{
				WebRequest = ewsRequest,
				SentTimeUtc = DateTime.UtcNow
			};
			ewsRequest.BeginGetResponse(delegate(IAsyncResult x)
			{
				HDPhotoProbe.RequestState requestState2 = (HDPhotoProbe.RequestState)x.AsyncState;
				try
				{
					using (HttpWebResponse httpWebResponse = (HttpWebResponse)requestState2.WebRequest.EndGetResponse(x))
					{
						requestState2.ResponseReceivedTimeUtc = DateTime.UtcNow;
						requestState2.StatusCode = httpWebResponse.StatusCode.ToString();
						string text = "X-Exchange-GetUserPhoto-Traces";
						if (httpWebResponse.Headers.AllKeys != null && httpWebResponse.Headers.AllKeys.Count<string>() != 0 && httpWebResponse.Headers.AllKeys.Contains(text))
						{
							base.Result.ExecutionContext = httpWebResponse.Headers[text];
						}
					}
				}
				catch (WebException ex)
				{
					requestState2.StatusCode = ex.Status.ToString();
					requestState2.ResponseReceivedTimeUtc = DateTime.UtcNow;
					requestState2.Exception = ex;
				}
				catch (Exception exception)
				{
					requestState2.StatusCode = WebExceptionStatus.UnknownError.ToString();
					requestState2.ResponseReceivedTimeUtc = DateTime.UtcNow;
					requestState2.Exception = exception;
				}
				finally
				{
					base.Result.StateAttribute12 = "Sent Time = " + requestState2.SentTimeUtc.ToString();
					base.Result.StateAttribute13 = "Received Time = " + requestState2.ResponseReceivedTimeUtc.ToString();
					base.Result.StateAttribute14 = "Status Code = " + requestState2.StatusCode.ToString();
					this.allDone.Set();
				}
			}, requestState);
			this.allDone.WaitOne();
			if (requestState.Exception != null)
			{
				throw requestState.Exception;
			}
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00048568 File Offset: 0x00046768
		private NetworkCredential GetCredential(string userName, string password)
		{
			NetworkCredential networkCredential = new NetworkCredential
			{
				UserName = userName,
				Password = password
			};
			if (userName.Contains("@"))
			{
				string[] array = userName.Split(new char[]
				{
					'@'
				});
				if (array.Length == 2)
				{
					networkCredential.UserName = array[0];
					networkCredential.Domain = array[1];
				}
			}
			return networkCredential;
		}

		// Token: 0x0400089E RID: 2206
		private ManualResetEvent allDone = new ManualResetEvent(false);

		// Token: 0x02000185 RID: 389
		private class RequestState
		{
			// Token: 0x1700025D RID: 605
			// (get) Token: 0x06000B51 RID: 2897 RVA: 0x000485D9 File Offset: 0x000467D9
			// (set) Token: 0x06000B52 RID: 2898 RVA: 0x000485E1 File Offset: 0x000467E1
			public DateTime SentTimeUtc { get; set; }

			// Token: 0x1700025E RID: 606
			// (get) Token: 0x06000B53 RID: 2899 RVA: 0x000485EA File Offset: 0x000467EA
			// (set) Token: 0x06000B54 RID: 2900 RVA: 0x000485F2 File Offset: 0x000467F2
			public DateTime ResponseReceivedTimeUtc { get; set; }

			// Token: 0x1700025F RID: 607
			// (get) Token: 0x06000B55 RID: 2901 RVA: 0x000485FB File Offset: 0x000467FB
			// (set) Token: 0x06000B56 RID: 2902 RVA: 0x00048603 File Offset: 0x00046803
			public HttpWebRequest WebRequest { get; set; }

			// Token: 0x17000260 RID: 608
			// (get) Token: 0x06000B57 RID: 2903 RVA: 0x0004860C File Offset: 0x0004680C
			// (set) Token: 0x06000B58 RID: 2904 RVA: 0x00048614 File Offset: 0x00046814
			public Exception Exception { get; set; }

			// Token: 0x17000261 RID: 609
			// (get) Token: 0x06000B59 RID: 2905 RVA: 0x0004861D File Offset: 0x0004681D
			// (set) Token: 0x06000B5A RID: 2906 RVA: 0x00048625 File Offset: 0x00046825
			public string StatusCode { get; set; }
		}
	}
}
