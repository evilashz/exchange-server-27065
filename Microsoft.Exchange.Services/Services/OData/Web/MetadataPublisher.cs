using System;
using System.Web;
using Microsoft.Exchange.Services.OData.Model;
using Microsoft.OData.Core;

namespace Microsoft.Exchange.Services.OData.Web
{
	// Token: 0x02000E0A RID: 3594
	internal class MetadataPublisher : DocumentPublisher
	{
		// Token: 0x06005D2E RID: 23854 RVA: 0x00122BCC File Offset: 0x00120DCC
		public MetadataPublisher(HttpContext httpContext, ServiceModel serviceModel) : base(httpContext, serviceModel)
		{
		}

		// Token: 0x06005D2F RID: 23855 RVA: 0x00122BD6 File Offset: 0x00120DD6
		protected override void WriteDocument(ODataMessageWriter odataWriter)
		{
			odataWriter.WriteMetadataDocument();
		}
	}
}
