using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.OData.Web;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EB4 RID: 3764
	internal class ContactFolderQueryAdapter : EwsQueryAdapter
	{
		// Token: 0x060061FD RID: 25085 RVA: 0x001331F8 File Offset: 0x001313F8
		public ContactFolderQueryAdapter(ContactFolderSchema entitySchema, ODataQueryOptions odataQueryOptions) : base(entitySchema, odataQueryOptions)
		{
		}

		// Token: 0x060061FE RID: 25086 RVA: 0x00133202 File Offset: 0x00131402
		public FolderResponseShape GetResponseShape(bool findOnly = false)
		{
			return new FolderResponseShape(ShapeEnum.IdOnly, base.GetRequestedPropertyPaths());
		}

		// Token: 0x040034E9 RID: 13545
		public static readonly ContactFolderQueryAdapter Default = new ContactFolderQueryAdapter(ContactFolderSchema.SchemaInstance, ODataQueryOptions.Empty);
	}
}
