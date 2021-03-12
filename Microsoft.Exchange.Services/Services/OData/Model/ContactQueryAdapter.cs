using System;
using System.Linq;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.OData.Web;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EB2 RID: 3762
	internal class ContactQueryAdapter : EwsQueryAdapter
	{
		// Token: 0x060061F4 RID: 25076 RVA: 0x00132F18 File Offset: 0x00131118
		public ContactQueryAdapter(ContactSchema entitySchema, ODataQueryOptions odataQueryOptions) : base(entitySchema, odataQueryOptions)
		{
		}

		// Token: 0x17001672 RID: 5746
		// (get) Token: 0x060061F5 RID: 25077 RVA: 0x00132F22 File Offset: 0x00131122
		public bool FindNeedsReread
		{
			get
			{
				return base.RequestedProperties.Intersect(ContactQueryAdapter.PropertiesSkippedForFind).Count<PropertyDefinition>() > 0;
			}
		}

		// Token: 0x060061F6 RID: 25078 RVA: 0x00132F3C File Offset: 0x0013113C
		public ItemResponseShape GetResponseShape(bool findOnly = false)
		{
			if (findOnly && this.FindNeedsReread)
			{
				return ContactQueryAdapter.IdOnlyResponseType;
			}
			PropertyPath[] requestedPropertyPaths = base.GetRequestedPropertyPaths();
			return new ItemResponseShape(ShapeEnum.IdOnly, BodyResponseType.Best, false, requestedPropertyPaths);
		}

		// Token: 0x040034E6 RID: 13542
		public static readonly ContactQueryAdapter Default = new ContactQueryAdapter(ContactSchema.SchemaInstance, ODataQueryOptions.Empty);

		// Token: 0x040034E7 RID: 13543
		public static readonly ItemResponseShape IdOnlyResponseType = new ItemResponseShape(ShapeEnum.IdOnly, BodyResponseType.Best, false, null);

		// Token: 0x040034E8 RID: 13544
		public static readonly PropertyDefinition[] PropertiesSkippedForFind = new PropertyDefinition[]
		{
			ItemSchema.Body
		};
	}
}
