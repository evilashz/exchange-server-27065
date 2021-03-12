using System;

namespace Microsoft.Exchange.Common.HA
{
	// Token: 0x0200003D RID: 61
	internal class IoErrorInfo
	{
		// Token: 0x06000110 RID: 272 RVA: 0x00006906 File Offset: 0x00004B06
		internal IoErrorInfo()
		{
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000690E File Offset: 0x00004B0E
		internal IoErrorInfo(IoErrorCategory category, string fileName, long offset, long size)
		{
			this.Category = category;
			this.FileName = fileName;
			this.Offset = offset;
			this.Size = size;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00006934 File Offset: 0x00004B34
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (base.GetType() != obj.GetType())
			{
				return false;
			}
			IoErrorInfo ioErrorInfo = obj as IoErrorInfo;
			return this.Category.Equals(ioErrorInfo.Category) && string.Equals(this.FileName, ioErrorInfo.FileName, StringComparison.OrdinalIgnoreCase) && this.Offset.Equals(ioErrorInfo.Offset) && this.Size.Equals(ioErrorInfo.Size);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000069C8 File Offset: 0x00004BC8
		public override string ToString()
		{
			return string.Format("(Category={0}, FileName={1}, Offset={2}, Size={3})", new object[]
			{
				this.Category,
				this.FileName,
				this.Offset,
				this.Size
			});
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00006A1C File Offset: 0x00004C1C
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00006A24 File Offset: 0x00004C24
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00006A2C File Offset: 0x00004C2C
		internal IoErrorCategory Category
		{
			get
			{
				return this.category;
			}
			set
			{
				this.category = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00006A35 File Offset: 0x00004C35
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00006A3D File Offset: 0x00004C3D
		internal string FileName
		{
			get
			{
				return this.fileName;
			}
			set
			{
				this.fileName = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00006A46 File Offset: 0x00004C46
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00006A4E File Offset: 0x00004C4E
		internal long Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.offset = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00006A57 File Offset: 0x00004C57
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00006A5F File Offset: 0x00004C5F
		internal long Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x04000150 RID: 336
		private IoErrorCategory category;

		// Token: 0x04000151 RID: 337
		private string fileName;

		// Token: 0x04000152 RID: 338
		private long offset;

		// Token: 0x04000153 RID: 339
		private long size;
	}
}
