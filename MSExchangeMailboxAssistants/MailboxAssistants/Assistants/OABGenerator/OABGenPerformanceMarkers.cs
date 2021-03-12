using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.OABGenerator
{
	// Token: 0x020001F8 RID: 504
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OABGenPerformanceMarkers
	{
		// Token: 0x04000BD1 RID: 3025
		public const string Total = "Total";

		// Token: 0x04000BD2 RID: 3026
		public const string PrepareFilesForOABGeneration = "PrepareFilesForOABGeneration";

		// Token: 0x04000BD3 RID: 3027
		public const string DownloadFilesFromMailbox = "PrepareFilesForOABGeneration.DownloadFilesFromMailbox";

		// Token: 0x04000BD4 RID: 3028
		public const string GenerateOrLinkTemplateFiles = "GenerateOrLinkTemplateFiles";

		// Token: 0x04000BD5 RID: 3029
		public const string GenerateTemplateFiles = "GenerateOrLinkTemplateFiles.GenerateTemplateFiles";

		// Token: 0x04000BD6 RID: 3030
		public const string BeginGeneratingAddressListFiles = "BeginGeneratingAddressListFiles";

		// Token: 0x04000BD7 RID: 3031
		public const string GetPreviousFiles = "BeginGeneratingAddressListFiles.GetPreviousFiles";

		// Token: 0x04000BD8 RID: 3032
		public const string ProcessOnePageOfADResults = "ProcessOnePageOfADResults";

		// Token: 0x04000BD9 RID: 3033
		public const string ADQuery = "ProcessOnePageOfADResults.ADQuery";

		// Token: 0x04000BDA RID: 3034
		public const string SortADResults = "ProcessOnePageOfADResults.SortADResults";

		// Token: 0x04000BDB RID: 3035
		public const string ResolveLinks = "ProcessOnePageOfADResults.ResolveLinks";

		// Token: 0x04000BDC RID: 3036
		public const string WriteTempFiles = "ProcessOnePageOfADResults.WriteTempFiles";

		// Token: 0x04000BDD RID: 3037
		public const string ProduceSortedFlatFile = "ProduceSortedFlatFile";

		// Token: 0x04000BDE RID: 3038
		public const string FinishGeneratingAddressListFiles = "FinishGeneratingAddressListFiles";

		// Token: 0x04000BDF RID: 3039
		public const string CompressGeneratedFiles = "FinishGeneratingAddressListFiles.CompressGeneratedFiles";

		// Token: 0x04000BE0 RID: 3040
		public const string GenerateDiffFiles = "FinishGeneratingAddressListFiles.GenerateDiffFiles";

		// Token: 0x04000BE1 RID: 3041
		public const string Publish = "Publish";

		// Token: 0x04000BE2 RID: 3042
		public const string PublishToDistribPoint = "Publish.PublishToDistribPoint";

		// Token: 0x04000BE3 RID: 3043
		public const string UploadFilesToMailbox = "Publish.UploadFilesToMailbox";
	}
}
