using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006E9 RID: 1769
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SharepointDocumentSchema : SharepointDocumentLibraryItemSchema
	{
		// Token: 0x17001477 RID: 5239
		// (get) Token: 0x06004647 RID: 17991 RVA: 0x0012B400 File Offset: 0x00129600
		public new static SharepointDocumentSchema Instance
		{
			get
			{
				if (SharepointDocumentSchema.instance == null)
				{
					SharepointDocumentSchema.instance = new SharepointDocumentSchema();
				}
				return SharepointDocumentSchema.instance;
			}
		}

		// Token: 0x0400265F RID: 9823
		private static SharepointDocumentSchema instance = null;

		// Token: 0x04002660 RID: 9824
		public static readonly DocumentLibraryPropertyDefinition VersionControl = new DocumentLibraryPropertyDefinition("Version control", typeof(VersionControl), null, DocumentLibraryPropertyId.None);

		// Token: 0x04002661 RID: 9825
		public static readonly DocumentLibraryPropertyDefinition CheckedOutUserId = new SharepointPropertyDefinition("Checked out user", typeof(string), DocumentLibraryPropertyId.None, "CheckedOutTitle", SharepointFieldType.Lookup, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.ObjectToString), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.SecondJoinedValue), null);

		// Token: 0x04002662 RID: 9826
		internal static readonly DocumentLibraryPropertyDefinition VersionId = new SharepointPropertyDefinition("Document version number", typeof(int), DocumentLibraryPropertyId.None, "owshiddenversion", SharepointFieldType.Integer, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.ObjectToString), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.SharepointToInt), null);

		// Token: 0x04002663 RID: 9827
		public static readonly DocumentLibraryPropertyDefinition FileSize = new SharepointPropertyDefinition("File Size", typeof(long), DocumentLibraryPropertyId.FileSize, "File_x0020_Size", SharepointFieldType.Lookup, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.ObjectToString), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.SharepointJoinedToLong), null);

		// Token: 0x04002664 RID: 9828
		public static readonly DocumentLibraryPropertyDefinition FileType = new SharepointPropertyDefinition("FileType", typeof(string), DocumentLibraryPropertyId.FileType, "File_x0020_Type", SharepointFieldType.Text, new SharepointPropertyDefinition.MarshalTypeToSharepoint(SharepointHelpers.ObjectToString), new SharepointPropertyDefinition.MarshalTypeFromSharepoint(SharepointHelpers.ExtensionToContentType), "application/octet-stream");
	}
}
