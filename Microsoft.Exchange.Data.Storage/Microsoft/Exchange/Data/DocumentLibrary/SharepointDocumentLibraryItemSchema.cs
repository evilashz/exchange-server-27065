using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006E3 RID: 1763
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SharepointDocumentLibraryItemSchema : SharepointObjectSchema
	{
		// Token: 0x0400264D RID: 9805
		public static readonly DocumentLibraryPropertyDefinition DocIcon = new SharepointPropertyDefinition("DocIcon", typeof(string), DocumentLibraryPropertyId.DocIcon, "DocIcon", SharepointFieldType.Computed, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.ObjectToString), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.NoOp), null);

		// Token: 0x0400264E RID: 9806
		public static readonly DocumentLibraryPropertyDefinition FileSystemObjectType = new SharepointPropertyDefinition("File Type", typeof(bool), DocumentLibraryPropertyId.IsFolder, "FSObjType", SharepointFieldType.Lookup, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.ObjectToString), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.SharepointIsFolderToBool), null);

		// Token: 0x0400264F RID: 9807
		public static readonly DocumentLibraryPropertyDefinition ServerUri = new SharepointPropertyDefinition("Server related Uri", typeof(Uri), DocumentLibraryPropertyId.ServerUri, "ServerUrl", SharepointFieldType.Computed, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.ObjectToString), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.NoOp), null);

		// Token: 0x04002650 RID: 9808
		public static readonly DocumentLibraryPropertyDefinition EncodedAbsoluteUri = new SharepointPropertyDefinition("Encoded Absolute Uri", typeof(Uri), DocumentLibraryPropertyId.Uri, "EncodedAbsUrl", SharepointFieldType.Computed, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.ObjectToString), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.SharepointToAbsoluteUri), null);

		// Token: 0x04002651 RID: 9809
		public static readonly DocumentLibraryPropertyDefinition BaseName = new SharepointPropertyDefinition("Base Name", typeof(string), DocumentLibraryPropertyId.BaseName, "BaseName", SharepointFieldType.Computed, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.ObjectToString), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.NoOp), null);

		// Token: 0x04002652 RID: 9810
		public static readonly DocumentLibraryPropertyDefinition Name = new SharepointPropertyDefinition("Name", typeof(string), DocumentLibraryPropertyId.Title, "FileLeafRef", SharepointFieldType.File, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.ObjectToString), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.SecondJoinedValue), null);

		// Token: 0x04002653 RID: 9811
		public static readonly DocumentLibraryPropertyDefinition ID = new SharepointPropertyDefinition("ID", typeof(string), DocumentLibraryPropertyId.Id, "ID", SharepointFieldType.Counter, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.ObjectToString), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.NoOp), null);

		// Token: 0x04002654 RID: 9812
		public static readonly DocumentLibraryPropertyDefinition LastModifiedTime = new SharepointPropertyDefinition("Last Modified", typeof(ExDateTime), DocumentLibraryPropertyId.LastModifiedTime, "Modified", SharepointFieldType.DateTime, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.DateTimeToSharepoint), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.SharepointToDateTime), null);

		// Token: 0x04002655 RID: 9813
		public static readonly DocumentLibraryPropertyDefinition Editor = new SharepointPropertyDefinition("Modified By", typeof(string), DocumentLibraryPropertyId.Editor, "Editor", SharepointFieldType.User, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.ObjectToString), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.SecondJoinedValue), null);

		// Token: 0x04002656 RID: 9814
		public static readonly DocumentLibraryPropertyDefinition CreationTime = new SharepointPropertyDefinition("Created Date", typeof(ExDateTime), DocumentLibraryPropertyId.CreationTime, "Created_x0020_Date", SharepointFieldType.Lookup, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.DateTimeToSharepoint), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.SharepointJoinedToDateTime), null);

		// Token: 0x04002657 RID: 9815
		public static readonly DocumentLibraryPropertyDefinition Author = new SharepointPropertyDefinition("Author", typeof(string), DocumentLibraryPropertyId.None, "Author", SharepointFieldType.User, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.ObjectToString), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.SecondJoinedValue), null);
	}
}
