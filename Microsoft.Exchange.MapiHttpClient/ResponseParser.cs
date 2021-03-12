using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000023 RID: 35
	internal abstract class ResponseParser : BaseObject
	{
		// Token: 0x0600011C RID: 284 RVA: 0x00007812 File Offset: 0x00005A12
		protected ResponseParser(HttpStatusCode httpStatusCode, ResponseCode responseCode, int maxResponseSize)
		{
			this.httpStatusCode = httpStatusCode;
			this.responseCode = responseCode;
			this.maxResponseSize = maxResponseSize;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600011D RID: 285 RVA: 0x0000783B File Offset: 0x00005A3B
		public bool IsSuccessful
		{
			get
			{
				base.CheckDisposed();
				return this.httpStatusCode == HttpStatusCode.OK && this.responseCode == ResponseCode.Success;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600011E RID: 286 RVA: 0x0000785B File Offset: 0x00005A5B
		public HttpStatusCode HttpStatusCode
		{
			get
			{
				base.CheckDisposed();
				return this.httpStatusCode;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00007869 File Offset: 0x00005A69
		public ResponseCode ResponseCode
		{
			get
			{
				base.CheckDisposed();
				return this.responseCode;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00007877 File Offset: 0x00005A77
		public TimeSpan? ElapsedTime
		{
			get
			{
				base.CheckDisposed();
				return this.elapsedTime;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00007885 File Offset: 0x00005A85
		public Dictionary<string, string> AdditionalHeaders
		{
			get
			{
				base.CheckDisposed();
				return this.additionalHeaders;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00007893 File Offset: 0x00005A93
		public ArraySegment<byte> ResponseData
		{
			get
			{
				base.CheckDisposed();
				if (this.responseBufferSize > 0)
				{
					return new ArraySegment<byte>(this.responseBuffer.Array, this.responseBuffer.Offset, this.responseBufferSize);
				}
				return Array<byte>.EmptySegment;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000078CC File Offset: 0x00005ACC
		public string FailureInfo
		{
			get
			{
				base.CheckDisposed();
				if (this.failureStream != null)
				{
					byte[] bytes = this.failureStream.ToArray();
					try
					{
						return Encoding.UTF8.GetString(bytes);
					}
					catch (Exception ex)
					{
						return string.Format("[Unable to convert response body to string; exception={0}:{1}]\r\n", ex.GetType().ToString(), ex.Message);
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x06000124 RID: 292
		public abstract void PutData(ArraySegment<byte> dataFragment);

		// Token: 0x06000125 RID: 293
		public abstract void Done();

		// Token: 0x06000126 RID: 294
		public abstract void AppendParserDiagnosticInformation(StringBuilder stringBuilder);

		// Token: 0x06000127 RID: 295 RVA: 0x00007938 File Offset: 0x00005B38
		protected void WriteResponseData(ArraySegment<byte> dataFragment)
		{
			if (!this.IsSuccessful)
			{
				if (this.failureStream == null)
				{
					this.failureStream = new MemoryStream();
				}
				this.failureStream.Write(dataFragment.Array, dataFragment.Offset, dataFragment.Count);
				return;
			}
			if (dataFragment.Count + this.responseBufferSize > this.maxResponseSize)
			{
				throw ProtocolException.FromResponseCode((LID)52768, string.Format("Response data larger than requested; size={0}, expected={1}", dataFragment.Count + this.responseBufferSize, this.maxResponseSize), ResponseCode.InvalidPayload, null);
			}
			if (this.responseBuffer == null)
			{
				int i;
				for (i = 8192; i < dataFragment.Count; i *= 2)
				{
				}
				this.responseBuffer = new WorkBuffer(Math.Min(i, this.maxResponseSize));
			}
			else if (this.responseBuffer.MaxSize - this.responseBufferSize < dataFragment.Count)
			{
				int num = this.responseBuffer.MaxSize;
				while (num - this.responseBufferSize < dataFragment.Count)
				{
					num *= 2;
				}
				WorkBuffer workBuffer = new WorkBuffer(Math.Min(num, this.maxResponseSize));
				Array.Copy(this.responseBuffer.Array, this.responseBuffer.Offset, workBuffer.Array, workBuffer.Offset, this.responseBufferSize);
				Util.DisposeIfPresent(this.responseBuffer);
				this.responseBuffer = workBuffer;
			}
			Array.Copy(dataFragment.Array, dataFragment.Offset, this.responseBuffer.Array, this.responseBuffer.Offset + this.responseBufferSize, dataFragment.Count);
			this.responseBufferSize += dataFragment.Count;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00007AE4 File Offset: 0x00005CE4
		protected void SetHeader(string header, string headerValue)
		{
			if (this.additionalHeaders == null)
			{
				this.additionalHeaders = new Dictionary<string, string>();
			}
			this.additionalHeaders.Add(header, headerValue);
			if (string.Compare("X-ResponseCode", header, true) != 0)
			{
				if (string.Compare("X-ElapsedTime", header, true) == 0)
				{
					int num;
					if (!int.TryParse(headerValue, out num) || num < 0)
					{
						this.elapsedTime = null;
					}
					this.elapsedTime = new TimeSpan?(TimeSpan.FromMilliseconds((double)num));
				}
				return;
			}
			int num2;
			if (!int.TryParse(headerValue, out num2))
			{
				throw ProtocolException.FromResponseCode((LID)36384, string.Format("Unable to parse a value from additional {1} header; found={0}", headerValue, "X-ResponseCode"), ResponseCode.InvalidHeader, null);
			}
			this.responseCode = (ResponseCode)num2;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00007B88 File Offset: 0x00005D88
		protected override void InternalDispose()
		{
			if (this.failureStream != null)
			{
				this.failureStream.Close();
			}
			Util.DisposeIfPresent(this.responseBuffer);
			base.InternalDispose();
		}

		// Token: 0x04000050 RID: 80
		private readonly int maxResponseSize;

		// Token: 0x04000051 RID: 81
		private readonly HttpStatusCode httpStatusCode;

		// Token: 0x04000052 RID: 82
		private WorkBuffer responseBuffer;

		// Token: 0x04000053 RID: 83
		private int responseBufferSize;

		// Token: 0x04000054 RID: 84
		private MemoryStream failureStream;

		// Token: 0x04000055 RID: 85
		private ResponseCode responseCode;

		// Token: 0x04000056 RID: 86
		private TimeSpan? elapsedTime = null;

		// Token: 0x04000057 RID: 87
		private Dictionary<string, string> additionalHeaders;
	}
}
