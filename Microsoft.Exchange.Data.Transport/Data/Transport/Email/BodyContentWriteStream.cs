using System;
using Microsoft.Exchange.Data.Internal;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000C4 RID: 196
	internal class BodyContentWriteStream : AppendStreamOnDataStorage
	{
		// Token: 0x06000484 RID: 1156 RVA: 0x0000A290 File Offset: 0x00008490
		public BodyContentWriteStream(IBody body) : base(new TemporaryDataStorage())
		{
			this.body = body;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000A2A4 File Offset: 0x000084A4
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.body != null)
			{
				ReadableDataStorage readableWritableStorage = base.ReadableWritableStorage;
				readableWritableStorage.AddRef();
				this.body.SetNewContent(readableWritableStorage, 0L, readableWritableStorage.Length);
				readableWritableStorage.Release();
				this.body = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0400027E RID: 638
		private IBody body;
	}
}
