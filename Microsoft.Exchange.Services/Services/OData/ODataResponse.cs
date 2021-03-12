using System;
using System.Collections.Specialized;
using System.Net;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.OData.Model;
using Microsoft.Exchange.Services.OData.Web;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E32 RID: 3634
	internal class ODataResponse
	{
		// Token: 0x06005DAE RID: 23982 RVA: 0x00123B45 File Offset: 0x00121D45
		public ODataResponse(ODataRequest request)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			this.Request = request;
		}

		// Token: 0x17001533 RID: 5427
		// (get) Token: 0x06005DAF RID: 23983 RVA: 0x00123B5F File Offset: 0x00121D5F
		// (set) Token: 0x06005DB0 RID: 23984 RVA: 0x00123B67 File Offset: 0x00121D67
		public ODataRequest Request { get; private set; }

		// Token: 0x06005DB1 RID: 23985 RVA: 0x00123B70 File Offset: 0x00121D70
		public void WriteHttpResponse()
		{
			HttpContext httpContext = this.Request.ODataContext.HttpContext;
			httpContext.Response.StatusCode = (int)this.HttpResponseCodeOnSuccess;
			this.ApplyResponseHeaders(httpContext.Response.Headers);
			if (this.InternalResult != null)
			{
				ResponseMessageWriter responseMessageWriter = new ResponseMessageWriter(httpContext, this.Request.ODataContext.ServiceModel);
				responseMessageWriter.WriteDataResult(this.Request.ODataContext, this.InternalResult);
			}
		}

		// Token: 0x17001534 RID: 5428
		// (get) Token: 0x06005DB2 RID: 23986 RVA: 0x00123BE6 File Offset: 0x00121DE6
		// (set) Token: 0x06005DB3 RID: 23987 RVA: 0x00123BEE File Offset: 0x00121DEE
		protected object InternalResult { get; set; }

		// Token: 0x17001535 RID: 5429
		// (get) Token: 0x06005DB4 RID: 23988 RVA: 0x00123BF7 File Offset: 0x00121DF7
		protected virtual HttpStatusCode HttpResponseCodeOnSuccess
		{
			get
			{
				return HttpStatusCode.OK;
			}
		}

		// Token: 0x06005DB5 RID: 23989 RVA: 0x00123C00 File Offset: 0x00121E00
		protected virtual void ApplyResponseHeaders(NameValueCollection responseHeaders)
		{
			string etag = this.GetEtag();
			if (!string.IsNullOrEmpty(etag))
			{
				responseHeaders["ETag"] = etag;
			}
		}

		// Token: 0x06005DB6 RID: 23990 RVA: 0x00123C28 File Offset: 0x00121E28
		protected string GetEntityLocation()
		{
			string result = null;
			Entity entity = this.InternalResult as Entity;
			if (entity != null)
			{
				result = entity.GetWebUri(this.Request.ODataContext).OriginalString;
			}
			return result;
		}

		// Token: 0x06005DB7 RID: 23991 RVA: 0x00123C60 File Offset: 0x00121E60
		protected string GetEtag()
		{
			string result = null;
			Entity entity = this.InternalResult as Entity;
			if (entity != null)
			{
				object arg = null;
				if (entity.PropertyBag.TryGetValue(ItemSchema.ChangeKey, out arg))
				{
					result = string.Format("W/\"{0}\"", arg);
				}
			}
			return result;
		}
	}
}
