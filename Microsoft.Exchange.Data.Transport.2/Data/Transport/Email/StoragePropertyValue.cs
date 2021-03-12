using System;
using System.IO;
using Microsoft.Exchange.Data.ContentTypes.Tnef;
using Microsoft.Exchange.Data.Internal;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x02000104 RID: 260
	internal class StoragePropertyValue
	{
		// Token: 0x060007EC RID: 2028 RVA: 0x0001CFF8 File Offset: 0x0001B1F8
		public StoragePropertyValue(TnefPropertyTag propertyTag, DataStorage storage, long start, long end)
		{
			if (storage != null)
			{
				storage.AddRef();
			}
			this.propertyTag = propertyTag;
			this.storage = storage;
			this.start = start;
			this.end = end;
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x0001D026 File Offset: 0x0001B226
		public TnefPropertyTag PropertyTag
		{
			get
			{
				return this.propertyTag;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x0001D02E File Offset: 0x0001B22E
		public DataStorage Storage
		{
			get
			{
				return this.storage;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x0001D036 File Offset: 0x0001B236
		public long Start
		{
			get
			{
				return this.start;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0001D03E File Offset: 0x0001B23E
		public long End
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0001D046 File Offset: 0x0001B246
		public Stream GetReadStream()
		{
			return this.storage.OpenReadStream(this.start, this.end);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0001D05F File Offset: 0x0001B25F
		public void SetUnicodePropertyTag()
		{
			this.propertyTag = new TnefPropertyTag(this.propertyTag.Id, TnefPropertyType.Unicode);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0001D079 File Offset: 0x0001B279
		public void SetBinaryPropertyTag()
		{
			this.propertyTag = new TnefPropertyTag(this.propertyTag.Id, TnefPropertyType.Binary);
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0001D096 File Offset: 0x0001B296
		public void SetStorage(DataStorage storage, long start, long end)
		{
			if (this.storage != null)
			{
				this.storage.Release();
			}
			if (storage != null)
			{
				storage.AddRef();
			}
			this.storage = storage;
			this.start = start;
			this.end = end;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001D0C9 File Offset: 0x0001B2C9
		public void DisposeStorage()
		{
			if (this.storage != null)
			{
				this.storage.Release();
				this.storage = null;
			}
		}

		// Token: 0x0400045A RID: 1114
		private TnefPropertyTag propertyTag;

		// Token: 0x0400045B RID: 1115
		private DataStorage storage;

		// Token: 0x0400045C RID: 1116
		private long start;

		// Token: 0x0400045D RID: 1117
		private long end;
	}
}
