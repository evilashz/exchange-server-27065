using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005FF RID: 1535
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MimePartHeaders
	{
		// Token: 0x06003F2C RID: 16172 RVA: 0x00106E20 File Offset: 0x00105020
		public MimePartHeaders(Charset charset)
		{
			this.headerDictionary = new Dictionary<string, Header>();
			this.headerByIdDictionary = new Dictionary<HeaderId, Header>();
			this.headerList = new List<Header>();
			this.charset = charset;
		}

		// Token: 0x06003F2D RID: 16173 RVA: 0x00106E50 File Offset: 0x00105050
		public void AddHeader(Header header)
		{
			this.headerList.Add(header);
			if (header.HeaderId != HeaderId.Received)
			{
				if (header.HeaderId != HeaderId.Unknown)
				{
					this.headerByIdDictionary[header.HeaderId] = header;
					return;
				}
				this.headerDictionary[header.Name.ToLowerInvariant()] = header;
			}
		}

		// Token: 0x170012DC RID: 4828
		public Header this[string headerName]
		{
			get
			{
				headerName = headerName.ToLowerInvariant();
				HeaderId headerId = Header.GetHeaderId(headerName);
				Header result;
				if (headerId == HeaderId.Unknown)
				{
					this.headerDictionary.TryGetValue(headerName, out result);
				}
				else
				{
					this.headerByIdDictionary.TryGetValue(headerId, out result);
				}
				return result;
			}
		}

		// Token: 0x170012DD RID: 4829
		public Header this[HeaderId id]
		{
			get
			{
				EnumValidator.ThrowIfInvalid<HeaderId>(id, "id");
				Header result;
				this.headerByIdDictionary.TryGetValue(id, out result);
				return result;
			}
		}

		// Token: 0x170012DE RID: 4830
		// (get) Token: 0x06003F30 RID: 16176 RVA: 0x00106F0C File Offset: 0x0010510C
		public int Count
		{
			get
			{
				return this.headerList.Count;
			}
		}

		// Token: 0x06003F31 RID: 16177 RVA: 0x00106F19 File Offset: 0x00105119
		public IEnumerator<Header> GetEnumerator()
		{
			return this.headerList.GetEnumerator();
		}

		// Token: 0x170012DF RID: 4831
		// (get) Token: 0x06003F32 RID: 16178 RVA: 0x00106F2B File Offset: 0x0010512B
		public Charset Charset
		{
			get
			{
				return this.charset;
			}
		}

		// Token: 0x040022C6 RID: 8902
		private Dictionary<string, Header> headerDictionary;

		// Token: 0x040022C7 RID: 8903
		private Dictionary<HeaderId, Header> headerByIdDictionary;

		// Token: 0x040022C8 RID: 8904
		private List<Header> headerList;

		// Token: 0x040022C9 RID: 8905
		private Charset charset;
	}
}
