using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000600 RID: 1536
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MimePartInfo
	{
		// Token: 0x170012E0 RID: 4832
		// (get) Token: 0x06003F33 RID: 16179 RVA: 0x00106F33 File Offset: 0x00105133
		public Charset Charset
		{
			get
			{
				return this.charset;
			}
		}

		// Token: 0x170012E1 RID: 4833
		// (get) Token: 0x06003F34 RID: 16180 RVA: 0x00106F3B File Offset: 0x0010513B
		public List<MimePartInfo> Children
		{
			get
			{
				return this.children;
			}
		}

		// Token: 0x170012E2 RID: 4834
		// (get) Token: 0x06003F35 RID: 16181 RVA: 0x00106F43 File Offset: 0x00105143
		// (set) Token: 0x06003F36 RID: 16182 RVA: 0x00106F4B File Offset: 0x0010514B
		public MimePartInfo AttachedItemStructure
		{
			get
			{
				return this.attachedItem;
			}
			set
			{
				this.attachedItem = value;
			}
		}

		// Token: 0x170012E3 RID: 4835
		// (get) Token: 0x06003F37 RID: 16183 RVA: 0x00106F54 File Offset: 0x00105154
		internal AttachmentId AttachmentId
		{
			get
			{
				return this.attachmentId;
			}
		}

		// Token: 0x170012E4 RID: 4836
		// (get) Token: 0x06003F38 RID: 16184 RVA: 0x00106F5C File Offset: 0x0010515C
		// (set) Token: 0x06003F39 RID: 16185 RVA: 0x00106F64 File Offset: 0x00105164
		public MimePartHeaders Headers
		{
			get
			{
				return this.headers;
			}
			set
			{
				this.headers = value;
			}
		}

		// Token: 0x170012E5 RID: 4837
		// (get) Token: 0x06003F3A RID: 16186 RVA: 0x00106F6D File Offset: 0x0010516D
		public int BodySize
		{
			get
			{
				return this.bodyBytes;
			}
		}

		// Token: 0x170012E6 RID: 4838
		// (get) Token: 0x06003F3B RID: 16187 RVA: 0x00106F75 File Offset: 0x00105175
		public int BodyLineCount
		{
			get
			{
				return this.bodyLines;
			}
		}

		// Token: 0x06003F3C RID: 16188 RVA: 0x00106F7D File Offset: 0x0010517D
		public MimePartInfo(Charset charset, MimePartInfo.Callback writerCallback, MimePartContentType contentType, ref int partIndex) : this(charset, writerCallback, contentType, null, null, null, ref partIndex)
		{
			EnumValidator.ThrowIfInvalid<MimePartContentType>(contentType, "contentType");
		}

		// Token: 0x06003F3D RID: 16189 RVA: 0x00106F98 File Offset: 0x00105198
		internal MimePartInfo(Charset charset, MimePartInfo.Callback writerCallback, MimePartContentType contentType, AttachmentId attachmentId, ref int partIndex) : this(charset, writerCallback, contentType, attachmentId, null, null, ref partIndex)
		{
		}

		// Token: 0x06003F3E RID: 16190 RVA: 0x00106FAC File Offset: 0x001051AC
		internal MimePartInfo(Charset charset, MimePartInfo.Callback writerCallback, MimePartContentType contentType, AttachmentId attachmentId, MimePart skeletonPart, MimeDocument skeleton, ref int partIndex) : this(charset, writerCallback, contentType, attachmentId, skeletonPart, skeleton, null, null, ref partIndex)
		{
		}

		// Token: 0x06003F3F RID: 16191 RVA: 0x00106FCC File Offset: 0x001051CC
		internal MimePartInfo(Charset charset, MimePartInfo.Callback writerCallback, MimePartContentType contentType, AttachmentId attachmentId, MimePart skeletonPart, MimeDocument skeleton, MimePart smimePart, MimeDocument smimeDocument, ref int partIndex)
		{
			this.charset = charset;
			this.contentType = contentType;
			this.writerCallback = writerCallback;
			this.partIndex = partIndex++;
			this.attachmentId = attachmentId;
			this.skeletonPart = skeletonPart;
			this.skeleton = skeleton;
			this.smimePart = smimePart;
			this.smimeDocument = smimeDocument;
			this.bodyLines = -1;
			this.bodyBytes = -1;
		}

		// Token: 0x170012E7 RID: 4839
		// (get) Token: 0x06003F40 RID: 16192 RVA: 0x0010703A File Offset: 0x0010523A
		// (set) Token: 0x06003F41 RID: 16193 RVA: 0x00107042 File Offset: 0x00105242
		internal MimePartContentType ContentType
		{
			get
			{
				return this.contentType;
			}
			set
			{
				this.contentType = value;
			}
		}

		// Token: 0x170012E8 RID: 4840
		// (get) Token: 0x06003F42 RID: 16194 RVA: 0x0010704B File Offset: 0x0010524B
		// (set) Token: 0x06003F43 RID: 16195 RVA: 0x00107053 File Offset: 0x00105253
		internal bool IsBodyToRemoveFromSkeleton
		{
			get
			{
				return this.isBodyToRemoveFromSkeleton;
			}
			set
			{
				this.isBodyToRemoveFromSkeleton = value;
			}
		}

		// Token: 0x170012E9 RID: 4841
		// (get) Token: 0x06003F44 RID: 16196 RVA: 0x0010705C File Offset: 0x0010525C
		public bool IsMultipart
		{
			get
			{
				return this.contentType >= MimePartContentType.FirstMultipartType;
			}
		}

		// Token: 0x170012EA RID: 4842
		// (get) Token: 0x06003F45 RID: 16197 RVA: 0x0010706B File Offset: 0x0010526B
		internal bool IsAttachment
		{
			get
			{
				return this.attachmentId != null;
			}
		}

		// Token: 0x170012EB RID: 4843
		// (get) Token: 0x06003F46 RID: 16198 RVA: 0x00107079 File Offset: 0x00105279
		internal string TypeName
		{
			get
			{
				return MimePartInfo.GetContentTypeName(this.contentType);
			}
		}

		// Token: 0x06003F47 RID: 16199 RVA: 0x00107088 File Offset: 0x00105288
		internal static string GetContentTypeName(MimePartContentType contentType)
		{
			switch (contentType)
			{
			case MimePartContentType.TextPlain:
				return "text/plain";
			case MimePartContentType.TextHtml:
				return "text/html";
			case MimePartContentType.TextEnriched:
				return "text/enriched";
			case MimePartContentType.Tnef:
				return "application/ms-tnef";
			case MimePartContentType.Calendar:
				return "text/calendar";
			case MimePartContentType.FirstMultipartType:
				return "multipart/alternative";
			case MimePartContentType.MultipartRelated:
				return "multipart/related";
			case MimePartContentType.MultipartMixed:
				return "multipart/mixed";
			case MimePartContentType.MultipartReportDsn:
				return "multipart/report";
			case MimePartContentType.MultipartReportMdn:
				return "multipart/report";
			}
			return null;
		}

		// Token: 0x06003F48 RID: 16200 RVA: 0x00107124 File Offset: 0x00105324
		public static MimePartContentType GetContentType(string contentTypeName)
		{
			if (contentTypeName.Equals("text/plain", StringComparison.OrdinalIgnoreCase))
			{
				return MimePartContentType.TextPlain;
			}
			if (contentTypeName.Equals("text/html", StringComparison.OrdinalIgnoreCase))
			{
				return MimePartContentType.TextHtml;
			}
			if (contentTypeName.Equals("text/enriched", StringComparison.OrdinalIgnoreCase))
			{
				return MimePartContentType.TextEnriched;
			}
			if (contentTypeName.Equals("application/ms-tnef", StringComparison.OrdinalIgnoreCase))
			{
				return MimePartContentType.Tnef;
			}
			if (contentTypeName.Equals("text/calendar", StringComparison.OrdinalIgnoreCase))
			{
				return MimePartContentType.Calendar;
			}
			if (contentTypeName.Equals("multipart/alternative", StringComparison.OrdinalIgnoreCase))
			{
				return MimePartContentType.FirstMultipartType;
			}
			if (contentTypeName.Equals("multipart/related", StringComparison.OrdinalIgnoreCase))
			{
				return MimePartContentType.MultipartRelated;
			}
			if (contentTypeName.Equals("multipart/mixed", StringComparison.OrdinalIgnoreCase))
			{
				return MimePartContentType.MultipartMixed;
			}
			if (contentTypeName.Equals("multipart/report", StringComparison.OrdinalIgnoreCase))
			{
				return MimePartContentType.MultipartReportDsn;
			}
			return MimePartContentType.Attachment;
		}

		// Token: 0x170012EC RID: 4844
		// (get) Token: 0x06003F49 RID: 16201 RVA: 0x001071C6 File Offset: 0x001053C6
		internal string SubpartContentType
		{
			get
			{
				if (this.children != null && this.children.Count != 0)
				{
					return this.children[0].TypeName;
				}
				return null;
			}
		}

		// Token: 0x170012ED RID: 4845
		// (get) Token: 0x06003F4A RID: 16202 RVA: 0x001071F0 File Offset: 0x001053F0
		internal int PartIndex
		{
			get
			{
				return this.partIndex;
			}
		}

		// Token: 0x170012EE RID: 4846
		// (get) Token: 0x06003F4B RID: 16203 RVA: 0x001071F8 File Offset: 0x001053F8
		internal MimePartInfo.Callback WriterCallback
		{
			get
			{
				return this.writerCallback;
			}
		}

		// Token: 0x06003F4C RID: 16204 RVA: 0x00107200 File Offset: 0x00105400
		public void AddChild(MimePartInfo newChild)
		{
			if (this.children == null)
			{
				this.children = new List<MimePartInfo>();
			}
			this.children.Add(newChild);
		}

		// Token: 0x06003F4D RID: 16205 RVA: 0x00107221 File Offset: 0x00105421
		internal void AddChildren(List<MimePartInfo> children)
		{
			this.children.AddRange(children);
		}

		// Token: 0x06003F4E RID: 16206 RVA: 0x0010722F File Offset: 0x0010542F
		internal void AddHeader(Header header)
		{
			if (this.headers == null)
			{
				this.headers = new MimePartHeaders(this.charset);
			}
			this.headers.AddHeader(header);
		}

		// Token: 0x06003F4F RID: 16207 RVA: 0x00107256 File Offset: 0x00105456
		public void SetBodySize(int bodySize, int lineCount)
		{
			this.bodyBytes = bodySize;
			this.bodyLines = lineCount;
		}

		// Token: 0x06003F50 RID: 16208 RVA: 0x00107266 File Offset: 0x00105466
		internal void ChildrenWrittenOut()
		{
			if (this.IsMultipart)
			{
				this.bodyBytes = 0;
				this.bodyLines = 0;
			}
		}

		// Token: 0x170012EF RID: 4847
		// (get) Token: 0x06003F51 RID: 16209 RVA: 0x00107286 File Offset: 0x00105486
		internal bool IsBodySizeComputed
		{
			get
			{
				if (this.bodyBytes == -1)
				{
					return false;
				}
				if (this.Children != null)
				{
					return this.Children.TrueForAll((MimePartInfo info) => info.IsBodySizeComputed);
				}
				return true;
			}
		}

		// Token: 0x170012F0 RID: 4848
		// (get) Token: 0x06003F52 RID: 16210 RVA: 0x001072C5 File Offset: 0x001054C5
		internal MimePart SkeletonPart
		{
			get
			{
				return this.skeletonPart;
			}
		}

		// Token: 0x170012F1 RID: 4849
		// (get) Token: 0x06003F53 RID: 16211 RVA: 0x001072CD File Offset: 0x001054CD
		internal MimeDocument Skeleton
		{
			get
			{
				return this.skeleton;
			}
		}

		// Token: 0x170012F2 RID: 4850
		// (get) Token: 0x06003F54 RID: 16212 RVA: 0x001072D5 File Offset: 0x001054D5
		internal MimePart SmimePart
		{
			get
			{
				return this.smimePart;
			}
		}

		// Token: 0x170012F3 RID: 4851
		// (get) Token: 0x06003F55 RID: 16213 RVA: 0x001072DD File Offset: 0x001054DD
		internal MimeDocument SmimeDocument
		{
			get
			{
				return this.smimeDocument;
			}
		}

		// Token: 0x040022CA RID: 8906
		private Charset charset;

		// Token: 0x040022CB RID: 8907
		private MimePartContentType contentType;

		// Token: 0x040022CC RID: 8908
		private List<MimePartInfo> children;

		// Token: 0x040022CD RID: 8909
		private MimePartInfo attachedItem;

		// Token: 0x040022CE RID: 8910
		private MimePartHeaders headers;

		// Token: 0x040022CF RID: 8911
		private AttachmentId attachmentId;

		// Token: 0x040022D0 RID: 8912
		private MimePartInfo.Callback writerCallback;

		// Token: 0x040022D1 RID: 8913
		private int partIndex;

		// Token: 0x040022D2 RID: 8914
		private int bodyLines;

		// Token: 0x040022D3 RID: 8915
		private int bodyBytes;

		// Token: 0x040022D4 RID: 8916
		private bool isBodyToRemoveFromSkeleton;

		// Token: 0x040022D5 RID: 8917
		private MimePart skeletonPart;

		// Token: 0x040022D6 RID: 8918
		private MimeDocument skeleton;

		// Token: 0x040022D7 RID: 8919
		private MimePart smimePart;

		// Token: 0x040022D8 RID: 8920
		private MimeDocument smimeDocument;

		// Token: 0x02000601 RID: 1537
		// (Invoke) Token: 0x06003F58 RID: 16216
		internal delegate ConversionResult Callback(ItemToMimeConverter converter, MimePartInfo partInfo, ItemToMimeConverter.MimeFlags flags);
	}
}
