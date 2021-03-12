using System;

namespace Microsoft.Exchange.Rpc.SharedCache
{
	// Token: 0x020003ED RID: 1005
	[Serializable]
	public sealed class CacheResponse
	{
		// Token: 0x06001139 RID: 4409 RVA: 0x0005667C File Offset: 0x00055A7C
		private CacheResponse(ResponseCode responseCode, byte[] value, string diagnostics)
		{
			this.m_responseCode = responseCode;
			this.m_value = value;
			this.m_diagnostics = diagnostics;
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x00056668 File Offset: 0x00055A68
		private CacheResponse()
		{
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x000566A4 File Offset: 0x00055AA4
		public static CacheResponse Create(ResponseCode responseCode, byte[] value)
		{
			return new CacheResponse(responseCode, value, null);
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x000567B4 File Offset: 0x00055BB4
		public static CacheResponse Create(ResponseCode responseCode)
		{
			return new CacheResponse(responseCode, null, null);
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x000566BC File Offset: 0x00055ABC
		public ResponseCode ResponseCode
		{
			get
			{
				return this.m_responseCode;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x000566D0 File Offset: 0x00055AD0
		public byte[] Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x000566E4 File Offset: 0x00055AE4
		public string Diagnostics
		{
			get
			{
				return this.m_diagnostics;
			}
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x000566F8 File Offset: 0x00055AF8
		public void AppendDiagInfo(string diagKeyName, string diagData)
		{
			string text = string.Format("{0}={1}", diagKeyName, diagData);
			if (string.IsNullOrEmpty(this.m_diagnostics))
			{
				this.m_diagnostics = text;
			}
			else
			{
				this.m_diagnostics += "|" + text;
			}
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x00056744 File Offset: 0x00055B44
		public sealed override string ToString()
		{
			string arg;
			if (this.m_diagnostics != null)
			{
				arg = this.m_diagnostics;
			}
			else
			{
				arg = "<null>";
			}
			byte[] value = this.m_value;
			string arg2;
			if (value != null)
			{
				arg2 = value.Length.ToString();
			}
			else
			{
				arg2 = "<null>";
			}
			return string.Format("Code={0}; BlobSize={1} bytes; DiagInfo={2}", ((ResponseCode)this.m_responseCode).ToString(), arg2, arg);
		}

		// Token: 0x04001012 RID: 4114
		private ResponseCode m_responseCode;

		// Token: 0x04001013 RID: 4115
		private byte[] m_value;

		// Token: 0x04001014 RID: 4116
		private string m_diagnostics;
	}
}
