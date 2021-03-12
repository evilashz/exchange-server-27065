using System;
using Microsoft.Exchange.Rpc.SharedCache;

namespace Microsoft.Exchange.SharedCache.Exceptions
{
	// Token: 0x02000017 RID: 23
	public abstract class SharedCacheExceptionBase : Exception
	{
		// Token: 0x0600009D RID: 157 RVA: 0x00003D07 File Offset: 0x00001F07
		protected SharedCacheExceptionBase(string errorMessage) : base(errorMessage)
		{
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003D10 File Offset: 0x00001F10
		protected SharedCacheExceptionBase(ResponseCode responseCode, string errorMessage) : this(responseCode, errorMessage, null)
		{
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003D1B File Offset: 0x00001F1B
		protected SharedCacheExceptionBase(ResponseCode responseCode, string errorMessage, Exception innerException) : base(errorMessage, innerException)
		{
			this.ResponseCode = responseCode;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003D2C File Offset: 0x00001F2C
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00003D34 File Offset: 0x00001F34
		public ResponseCode ResponseCode { get; private set; }

		// Token: 0x060000A2 RID: 162 RVA: 0x00003D3D File Offset: 0x00001F3D
		public override string ToString()
		{
			return "Error=" + this.ResponseCode.ToString() + ". " + base.ToString();
		}
	}
}
