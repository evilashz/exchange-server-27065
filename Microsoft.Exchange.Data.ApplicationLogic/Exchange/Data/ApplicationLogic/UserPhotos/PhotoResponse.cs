using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x0200020A RID: 522
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PhotoResponse
	{
		// Token: 0x060012D3 RID: 4819 RVA: 0x0004ECAE File Offset: 0x0004CEAE
		public PhotoResponse(Stream outputPhotoStream)
		{
			if (outputPhotoStream == null)
			{
				throw new ArgumentNullException("outputPhotoStream");
			}
			this.OutputPhotoStream = outputPhotoStream;
			this.Status = HttpStatusCode.NotFound;
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x0004ECD6 File Offset: 0x0004CED6
		// (set) Token: 0x060012D5 RID: 4821 RVA: 0x0004ECDE File Offset: 0x0004CEDE
		public int? Thumbprint { get; set; }

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x0004ECE7 File Offset: 0x0004CEE7
		// (set) Token: 0x060012D7 RID: 4823 RVA: 0x0004ED08 File Offset: 0x0004CF08
		public string ETag
		{
			get
			{
				if (!this.etagExplicitlyInitialized)
				{
					return PhotoThumbprinter.Default.FormatAsETag(this.Thumbprint);
				}
				return this.etag;
			}
			set
			{
				this.etagExplicitlyInitialized = true;
				this.etag = value;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x0004ED18 File Offset: 0x0004CF18
		// (set) Token: 0x060012D9 RID: 4825 RVA: 0x0004ED20 File Offset: 0x0004CF20
		public IDictionary<UserPhotoSize, byte[]> UploadedPhotos { get; set; }

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x0004ED29 File Offset: 0x0004CF29
		// (set) Token: 0x060012DB RID: 4827 RVA: 0x0004ED31 File Offset: 0x0004CF31
		public Stream OutputPhotoStream { get; private set; }

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x0004ED3A File Offset: 0x0004CF3A
		// (set) Token: 0x060012DD RID: 4829 RVA: 0x0004ED42 File Offset: 0x0004CF42
		public bool ServerCacheHit { get; set; }

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x060012DE RID: 4830 RVA: 0x0004ED4B File Offset: 0x0004CF4B
		// (set) Token: 0x060012DF RID: 4831 RVA: 0x0004ED53 File Offset: 0x0004CF53
		public bool IsPhotoFileOnFileSystem { get; set; }

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060012E0 RID: 4832 RVA: 0x0004ED5C File Offset: 0x0004CF5C
		// (set) Token: 0x060012E1 RID: 4833 RVA: 0x0004ED64 File Offset: 0x0004CF64
		public bool Served { get; set; }

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x0004ED6D File Offset: 0x0004CF6D
		// (set) Token: 0x060012E3 RID: 4835 RVA: 0x0004ED75 File Offset: 0x0004CF75
		public HttpStatusCode Status { get; set; }

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060012E4 RID: 4836 RVA: 0x0004ED7E File Offset: 0x0004CF7E
		// (set) Token: 0x060012E5 RID: 4837 RVA: 0x0004ED86 File Offset: 0x0004CF86
		public string PhotoFullPathOnFileSystem { get; set; }

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x0004ED8F File Offset: 0x0004CF8F
		// (set) Token: 0x060012E7 RID: 4839 RVA: 0x0004ED97 File Offset: 0x0004CF97
		public long ContentLength { get; set; }

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060012E8 RID: 4840 RVA: 0x0004EDA0 File Offset: 0x0004CFA0
		// (set) Token: 0x060012E9 RID: 4841 RVA: 0x0004EDA8 File Offset: 0x0004CFA8
		public string ContentType { get; set; }

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060012EA RID: 4842 RVA: 0x0004EDB1 File Offset: 0x0004CFB1
		// (set) Token: 0x060012EB RID: 4843 RVA: 0x0004EDB9 File Offset: 0x0004CFB9
		public bool FileSystemHandlerProcessed { get; set; }

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x0004EDC2 File Offset: 0x0004CFC2
		// (set) Token: 0x060012ED RID: 4845 RVA: 0x0004EDCA File Offset: 0x0004CFCA
		public bool MailboxHandlerProcessed { get; set; }

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x0004EDD3 File Offset: 0x0004CFD3
		// (set) Token: 0x060012EF RID: 4847 RVA: 0x0004EDDB File Offset: 0x0004CFDB
		public bool ADHandlerProcessed { get; set; }

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060012F0 RID: 4848 RVA: 0x0004EDE4 File Offset: 0x0004CFE4
		// (set) Token: 0x060012F1 RID: 4849 RVA: 0x0004EDEC File Offset: 0x0004CFEC
		public bool CachingHandlerProcessed { get; set; }

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x0004EDF5 File Offset: 0x0004CFF5
		// (set) Token: 0x060012F3 RID: 4851 RVA: 0x0004EDFD File Offset: 0x0004CFFD
		public bool PreviewUploadHandlerProcessed { get; set; }

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x0004EE06 File Offset: 0x0004D006
		// (set) Token: 0x060012F5 RID: 4853 RVA: 0x0004EE0E File Offset: 0x0004D00E
		public bool FileSystemUploadHandlerProcessed { get; set; }

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x0004EE17 File Offset: 0x0004D017
		// (set) Token: 0x060012F7 RID: 4855 RVA: 0x0004EE1F File Offset: 0x0004D01F
		public bool MailboxUploadHandlerProcessed { get; set; }

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x0004EE28 File Offset: 0x0004D028
		// (set) Token: 0x060012F9 RID: 4857 RVA: 0x0004EE30 File Offset: 0x0004D030
		public bool ADUploadHandlerProcessed { get; set; }

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x0004EE39 File Offset: 0x0004D039
		// (set) Token: 0x060012FB RID: 4859 RVA: 0x0004EE41 File Offset: 0x0004D041
		public bool HttpHandlerProcessed { get; set; }

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x0004EE4A File Offset: 0x0004D04A
		// (set) Token: 0x060012FD RID: 4861 RVA: 0x0004EE52 File Offset: 0x0004D052
		public bool PrivateHandlerProcessed { get; set; }

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x0004EE5B File Offset: 0x0004D05B
		// (set) Token: 0x060012FF RID: 4863 RVA: 0x0004EE63 File Offset: 0x0004D063
		public bool TargetNotFoundHandlerProcessed { get; set; }

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x0004EE6C File Offset: 0x0004D06C
		// (set) Token: 0x06001301 RID: 4865 RVA: 0x0004EE74 File Offset: 0x0004D074
		public bool TransparentImageHandlerProcessed { get; set; }

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x0004EE7D File Offset: 0x0004D07D
		// (set) Token: 0x06001303 RID: 4867 RVA: 0x0004EE85 File Offset: 0x0004D085
		public bool OrganizationalToPrivateHandlerTransitionProcessed { get; set; }

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x0004EE8E File Offset: 0x0004D08E
		// (set) Token: 0x06001305 RID: 4869 RVA: 0x0004EE96 File Offset: 0x0004D096
		public bool PhotoWrittenToFileSystem { get; set; }

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x0004EE9F File Offset: 0x0004D09F
		// (set) Token: 0x06001307 RID: 4871 RVA: 0x0004EEA7 File Offset: 0x0004D0A7
		public string HttpExpiresHeader { get; set; }

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x0004EEB0 File Offset: 0x0004D0B0
		// (set) Token: 0x06001309 RID: 4873 RVA: 0x0004EEB8 File Offset: 0x0004D0B8
		public string PhotoUrl { get; set; }

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600130A RID: 4874 RVA: 0x0004EEC1 File Offset: 0x0004D0C1
		// (set) Token: 0x0600130B RID: 4875 RVA: 0x0004EEC9 File Offset: 0x0004D0C9
		public bool RemoteForestHandlerProcessed { get; set; }

		// Token: 0x04000A6B RID: 2667
		private bool etagExplicitlyInitialized;

		// Token: 0x04000A6C RID: 2668
		private string etag;
	}
}
