using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006EC RID: 1772
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SharepointListSchema : SharepointObjectSchema
	{
		// Token: 0x17001478 RID: 5240
		// (get) Token: 0x06004662 RID: 18018 RVA: 0x0012BC5B File Offset: 0x00129E5B
		public new static SharepointListSchema Instance
		{
			get
			{
				if (SharepointListSchema.instance == null)
				{
					SharepointListSchema.instance = new SharepointListSchema();
				}
				return SharepointListSchema.instance;
			}
		}

		// Token: 0x04002683 RID: 9859
		private static SharepointListSchema instance = null;

		// Token: 0x04002684 RID: 9860
		public static readonly DocumentLibraryPropertyDefinition ID = new SharepointPropertyDefinition("ID", typeof(DocumentLibraryObjectId), DocumentLibraryPropertyId.Id, "ID", SharepointFieldType.Guid, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.ObjectToString), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.NoOp), null);

		// Token: 0x04002685 RID: 9861
		public static DocumentLibraryPropertyDefinition Name = new SharepointPropertyDefinition("Name", typeof(string), DocumentLibraryPropertyId.None, "Name", SharepointFieldType.Guid, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.ObjectToString), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.NoOp), null);

		// Token: 0x04002686 RID: 9862
		public static DocumentLibraryPropertyDefinition Title = new SharepointPropertyDefinition("Title", typeof(string), DocumentLibraryPropertyId.Title, "Title", SharepointFieldType.Text, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.ObjectToString), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.NoOp), string.Empty);

		// Token: 0x04002687 RID: 9863
		public static DocumentLibraryPropertyDefinition Description = new SharepointPropertyDefinition("Description", typeof(string), DocumentLibraryPropertyId.Description, "Description", SharepointFieldType.Text, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.ObjectToString), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.NoOp), string.Empty);

		// Token: 0x04002688 RID: 9864
		public static DocumentLibraryPropertyDefinition DefaultViewUri = new SharepointPropertyDefinition("DefaultViewUri", typeof(Uri), DocumentLibraryPropertyId.None, "DefaultViewUrl", SharepointFieldType.URL, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.ObjectToString), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.SharepointToRelateiveUri), null);

		// Token: 0x04002689 RID: 9865
		public static DocumentLibraryPropertyDefinition ListType = new SharepointPropertyDefinition("ListType", typeof(int), DocumentLibraryPropertyId.None, "BaseType", SharepointFieldType.Integer, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.ObjectToString), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.SharepointToInt), null);

		// Token: 0x0400268A RID: 9866
		public static DocumentLibraryPropertyDefinition DefaultTemplateUri = new SharepointPropertyDefinition("DefaultTemplateUri", typeof(Uri), DocumentLibraryPropertyId.None, "DefaultTemplateUrl", SharepointFieldType.URL, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.ObjectToString), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.SharepointToRelateiveUri), null);

		// Token: 0x0400268B RID: 9867
		public static DocumentLibraryPropertyDefinition DocTemplateUri = new SharepointPropertyDefinition("DocTemplateUri", typeof(Uri), DocumentLibraryPropertyId.None, "DocTemplateUrl", SharepointFieldType.URL, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.ObjectToString), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.SharepointToRelateiveUri), null);

		// Token: 0x0400268C RID: 9868
		public static DocumentLibraryPropertyDefinition ImageUri = new SharepointPropertyDefinition("ImageUri", typeof(Uri), DocumentLibraryPropertyId.None, "ImageUrl", SharepointFieldType.URL, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.ObjectToString), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.SharepointToRelateiveUri), null);

		// Token: 0x0400268D RID: 9869
		public static DocumentLibraryPropertyDefinition PredefinedListType = new SharepointPropertyDefinition("Predefined ListType", typeof(int), DocumentLibraryPropertyId.None, "ServerTemplate", SharepointFieldType.Integer, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.ObjectToString), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.SharepointToInt), null);

		// Token: 0x0400268E RID: 9870
		public static DocumentLibraryPropertyDefinition LastModifiedTime = new SharepointPropertyDefinition("Last modified time", typeof(ExDateTime), DocumentLibraryPropertyId.LastModifiedTime, "Modified", SharepointFieldType.DateTime, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.ObjectToString), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.SharepoinToListDateTime), null);

		// Token: 0x0400268F RID: 9871
		public static DocumentLibraryPropertyDefinition CreationTime = new SharepointPropertyDefinition("Created Date", typeof(ExDateTime), DocumentLibraryPropertyId.CreationTime, "Created", SharepointFieldType.DateTime, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.ObjectToString), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.SharepoinToListDateTime), null);

		// Token: 0x04002690 RID: 9872
		public static DocumentLibraryPropertyDefinition ItemCount = new SharepointPropertyDefinition("Item Count", typeof(int), DocumentLibraryPropertyId.None, "ItemCount", SharepointFieldType.DateTime, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.ObjectToString), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.SharepointToInt), null);

		// Token: 0x04002691 RID: 9873
		internal static DocumentLibraryPropertyDefinition IsHidden = new SharepointPropertyDefinition("IsHidden", typeof(bool), DocumentLibraryPropertyId.IsHidden, "Hidden", SharepointFieldType.DateTime, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.ObjectToString), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.SharepointToBool), null);

		// Token: 0x04002692 RID: 9874
		public static DocumentLibraryPropertyDefinition Uri = DocumentLibraryItemSchema.Uri;
	}
}
