using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.EventLogs;

namespace Microsoft.Exchange.Services.OData.Web
{
	// Token: 0x02000DFC RID: 3580
	internal class HttpHandler : IHttpAsyncHandler, IHttpHandler
	{
		// Token: 0x170014EE RID: 5358
		// (get) Token: 0x06005C96 RID: 23702 RVA: 0x00120A12 File Offset: 0x0011EC12
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005C97 RID: 23703 RVA: 0x00120A15 File Offset: 0x0011EC15
		public void ProcessRequest(HttpContext context)
		{
			throw new InvalidOperationException("Should never hit here!");
		}

		// Token: 0x06005C98 RID: 23704 RVA: 0x00120A24 File Offset: 0x0011EC24
		public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
		{
			this.asyncCallback = cb;
			this.requestBroker = new RequestBroker(context);
			Task task = this.requestBroker.Process();
			return task.ContinueWith(new Action<Task>(this.BrokerProcessingComplete));
		}

		// Token: 0x06005C99 RID: 23705 RVA: 0x00120A62 File Offset: 0x0011EC62
		public void EndProcessRequest(IAsyncResult result)
		{
			this.requestBroker.Dispose();
			if (this.asyncException != null)
			{
				throw new HttpException(500, this.asyncException.Message, this.asyncException);
			}
		}

		// Token: 0x06005C9A RID: 23706 RVA: 0x00120A93 File Offset: 0x0011EC93
		private void BrokerProcessingComplete(Task brokerTask)
		{
			if (brokerTask.Exception != null)
			{
				this.asyncException = brokerTask.Exception.InnerException;
				ServiceDiagnostics.ReportException(this.asyncException, ServicesEventLogConstants.Tuple_InternalServerError, null, "Unhandled OData service exception: {0}");
			}
			this.asyncCallback(brokerTask);
		}

		// Token: 0x0400323D RID: 12861
		private RequestBroker requestBroker;

		// Token: 0x0400323E RID: 12862
		private Exception asyncException;

		// Token: 0x0400323F RID: 12863
		private AsyncCallback asyncCallback;
	}
}
