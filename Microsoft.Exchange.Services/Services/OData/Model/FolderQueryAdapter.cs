using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.OData.Web;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EAB RID: 3755
	internal class FolderQueryAdapter : EwsQueryAdapter
	{
		// Token: 0x060061C9 RID: 25033 RVA: 0x00131641 File Offset: 0x0012F841
		public FolderQueryAdapter(FolderSchema entitySchema, ODataQueryOptions odataQueryOptions) : base(entitySchema, odataQueryOptions)
		{
		}

		// Token: 0x060061CA RID: 25034 RVA: 0x0013164B File Offset: 0x0012F84B
		public FolderResponseShape GetResponseShape()
		{
			return new FolderResponseShape(ShapeEnum.IdOnly, base.GetRequestedPropertyPaths());
		}

		// Token: 0x040034DA RID: 13530
		public static readonly FolderQueryAdapter Default = new FolderQueryAdapter(FolderSchema.SchemaInstance, ODataQueryOptions.Empty);
	}
}
