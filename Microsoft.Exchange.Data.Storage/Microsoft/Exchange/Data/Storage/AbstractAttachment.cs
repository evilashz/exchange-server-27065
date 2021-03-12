using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002BD RID: 701
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AbstractAttachment : AbstractStorePropertyBag, IAttachment, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06001D55 RID: 7509 RVA: 0x00085BB5 File Offset: 0x00083DB5
		public AttachmentType AttachmentType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06001D56 RID: 7510 RVA: 0x00085BBC File Offset: 0x00083DBC
		public virtual bool IsContactPhoto
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06001D57 RID: 7511 RVA: 0x00085BC3 File Offset: 0x00083DC3
		// (set) Token: 0x06001D58 RID: 7512 RVA: 0x00085BCA File Offset: 0x00083DCA
		public virtual AttachmentId Id
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06001D59 RID: 7513 RVA: 0x00085BD1 File Offset: 0x00083DD1
		// (set) Token: 0x06001D5A RID: 7514 RVA: 0x00085BD8 File Offset: 0x00083DD8
		public virtual string ContentType
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06001D5B RID: 7515 RVA: 0x00085BDF File Offset: 0x00083DDF
		public virtual string CalculatedContentType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06001D5C RID: 7516 RVA: 0x00085BE6 File Offset: 0x00083DE6
		public virtual string DisplayName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06001D5D RID: 7517 RVA: 0x00085BED File Offset: 0x00083DED
		// (set) Token: 0x06001D5E RID: 7518 RVA: 0x00085BF4 File Offset: 0x00083DF4
		public virtual string FileExtension
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06001D5F RID: 7519 RVA: 0x00085BFB File Offset: 0x00083DFB
		// (set) Token: 0x06001D60 RID: 7520 RVA: 0x00085C02 File Offset: 0x00083E02
		public virtual string FileName
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06001D61 RID: 7521 RVA: 0x00085C09 File Offset: 0x00083E09
		// (set) Token: 0x06001D62 RID: 7522 RVA: 0x00085C10 File Offset: 0x00083E10
		public virtual bool IsInline
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06001D63 RID: 7523 RVA: 0x00085C17 File Offset: 0x00083E17
		public virtual ExDateTime LastModifiedTime
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06001D64 RID: 7524 RVA: 0x00085C1E File Offset: 0x00083E1E
		public virtual long Size
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x00085C25 File Offset: 0x00083E25
		public virtual void Save()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x00085C2C File Offset: 0x00083E2C
		public virtual void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
