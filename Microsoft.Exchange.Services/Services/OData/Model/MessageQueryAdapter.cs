using System;
using System.Linq;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.OData.Web;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EAE RID: 3758
	internal class MessageQueryAdapter : EwsQueryAdapter
	{
		// Token: 0x060061DB RID: 25051 RVA: 0x00132135 File Offset: 0x00130335
		public MessageQueryAdapter(MessageSchema entitySchema, ODataQueryOptions odataQueryOptions) : base(entitySchema, odataQueryOptions)
		{
		}

		// Token: 0x060061DC RID: 25052 RVA: 0x00132140 File Offset: 0x00130340
		public ItemResponseShape GetResponseShape(bool findOnly = false)
		{
			if (findOnly && this.FindNeedsReread)
			{
				return MessageQueryAdapter.IdOnlyResponseType;
			}
			bool includeMimeContent = false;
			PropertyPath[] array = base.GetRequestedPropertyPaths();
			if (base.ODataQueryOptions.Expands(ItemSchema.Attachments.Name) && !base.RequestedProperties.Contains(ItemSchema.HasAttachments))
			{
				array = array.Concat(new PropertyPath[]
				{
					ItemSchema.HasAttachments.EwsPropertyProvider.GetEwsPropertyProvider(base.EntitySchema).PropertyInformation.PropertyPath
				}).ToArray<PropertyPath>();
			}
			return new ItemResponseShape(ShapeEnum.IdOnly, BodyResponseType.Best, includeMimeContent, array);
		}

		// Token: 0x17001671 RID: 5745
		// (get) Token: 0x060061DD RID: 25053 RVA: 0x001321CF File Offset: 0x001303CF
		public bool FindNeedsReread
		{
			get
			{
				return base.RequestedProperties.Intersect(MessageQueryAdapter.PropertiesSkippedForFind).Count<PropertyDefinition>() > 0;
			}
		}

		// Token: 0x040034E1 RID: 13537
		public static readonly MessageQueryAdapter Default = new MessageQueryAdapter(MessageSchema.SchemaInstance, ODataQueryOptions.Empty);

		// Token: 0x040034E2 RID: 13538
		public static readonly ItemResponseShape IdOnlyResponseType = new ItemResponseShape(ShapeEnum.IdOnly, BodyResponseType.Best, false, null);

		// Token: 0x040034E3 RID: 13539
		public static readonly PropertyDefinition[] PropertiesSkippedForFind = new PropertyDefinition[]
		{
			ItemSchema.Body,
			MessageSchema.UniqueBody,
			MessageSchema.ToRecipients,
			MessageSchema.CcRecipients,
			MessageSchema.BccRecipients,
			MessageSchema.EventId
		};
	}
}
