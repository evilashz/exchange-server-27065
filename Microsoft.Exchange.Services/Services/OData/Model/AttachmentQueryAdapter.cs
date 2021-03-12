using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.OData.Web;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EB0 RID: 3760
	internal class AttachmentQueryAdapter : EwsQueryAdapter
	{
		// Token: 0x060061EA RID: 25066 RVA: 0x0013293F File Offset: 0x00130B3F
		public AttachmentQueryAdapter(AttachmentSchema entitySchema, ODataQueryOptions odataQueryOptions) : base(entitySchema, odataQueryOptions)
		{
		}

		// Token: 0x040034E4 RID: 13540
		public static readonly AttachmentQueryAdapter Default = new AttachmentQueryAdapter(AttachmentSchema.SchemaInstance, ODataQueryOptions.Empty);

		// Token: 0x040034E5 RID: 13541
		public static readonly AttachmentResponseShape AttachmentResponseShape = new AttachmentResponseShape();
	}
}
