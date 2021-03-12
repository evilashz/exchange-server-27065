using System;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000030 RID: 48
	public struct DecodingResults
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000095BA File Offset: 0x000077BA
		// (set) Token: 0x06000215 RID: 533 RVA: 0x000095C2 File Offset: 0x000077C2
		public string CharsetName
		{
			get
			{
				return this.charsetName;
			}
			internal set
			{
				this.charsetName = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000216 RID: 534 RVA: 0x000095CB File Offset: 0x000077CB
		// (set) Token: 0x06000217 RID: 535 RVA: 0x000095D3 File Offset: 0x000077D3
		public string CultureName
		{
			get
			{
				return this.cultureName;
			}
			internal set
			{
				this.cultureName = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000218 RID: 536 RVA: 0x000095DC File Offset: 0x000077DC
		// (set) Token: 0x06000219 RID: 537 RVA: 0x000095E4 File Offset: 0x000077E4
		public EncodingScheme EncodingScheme
		{
			get
			{
				return this.encodingScheme;
			}
			internal set
			{
				this.encodingScheme = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600021A RID: 538 RVA: 0x000095ED File Offset: 0x000077ED
		// (set) Token: 0x0600021B RID: 539 RVA: 0x000095F5 File Offset: 0x000077F5
		public bool DecodingFailed
		{
			get
			{
				return this.decodingFailed;
			}
			internal set
			{
				this.decodingFailed = value;
			}
		}

		// Token: 0x04000125 RID: 293
		private string charsetName;

		// Token: 0x04000126 RID: 294
		private string cultureName;

		// Token: 0x04000127 RID: 295
		private EncodingScheme encodingScheme;

		// Token: 0x04000128 RID: 296
		private bool decodingFailed;
	}
}
