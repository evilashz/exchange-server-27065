using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.OData.Model;
using Microsoft.OData.Core;

namespace Microsoft.Exchange.Services.OData.Web
{
	// Token: 0x02000E09 RID: 3593
	internal abstract class DocumentPublisher
	{
		// Token: 0x06005D26 RID: 23846 RVA: 0x00122B00 File Offset: 0x00120D00
		public DocumentPublisher(HttpContext httpContext, ServiceModel serviceModel)
		{
			ArgumentValidator.ThrowIfNull("httpContext", httpContext);
			ArgumentValidator.ThrowIfNull("serviceModel", serviceModel);
			this.HttpContext = httpContext;
			this.ServiceModel = serviceModel;
		}

		// Token: 0x17001518 RID: 5400
		// (get) Token: 0x06005D27 RID: 23847 RVA: 0x00122B2C File Offset: 0x00120D2C
		// (set) Token: 0x06005D28 RID: 23848 RVA: 0x00122B34 File Offset: 0x00120D34
		public HttpContext HttpContext { get; private set; }

		// Token: 0x17001519 RID: 5401
		// (get) Token: 0x06005D29 RID: 23849 RVA: 0x00122B3D File Offset: 0x00120D3D
		// (set) Token: 0x06005D2A RID: 23850 RVA: 0x00122B45 File Offset: 0x00120D45
		public ServiceModel ServiceModel { get; private set; }

		// Token: 0x06005D2B RID: 23851 RVA: 0x00122B4E File Offset: 0x00120D4E
		public Task Publish()
		{
			return Task.Factory.StartNew(new Action(this.InternalPublish));
		}

		// Token: 0x06005D2C RID: 23852 RVA: 0x00122B68 File Offset: 0x00120D68
		private void InternalPublish()
		{
			ResponseMessageWriter responseMessageWriter = new ResponseMessageWriter(this.HttpContext, this.ServiceModel);
			using (ODataMessageWriter odataMessageWriter = responseMessageWriter.CreateODataMessageWriter(null, false))
			{
				try
				{
					this.WriteDocument(odataMessageWriter);
				}
				catch (ODataContentTypeException innerException)
				{
					throw new InvalidContentTypeException(innerException);
				}
			}
		}

		// Token: 0x06005D2D RID: 23853
		protected abstract void WriteDocument(ODataMessageWriter odataWriter);
	}
}
