using System;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x02000007 RID: 7
	internal class BufferResponseItem : IResponseItem
	{
		// Token: 0x060000BF RID: 191 RVA: 0x000042C4 File Offset: 0x000024C4
		public BufferResponseItem() : this(null, 0, 0, null)
		{
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000042D0 File Offset: 0x000024D0
		public BufferResponseItem(byte[] buf) : this(buf, 0, buf.Length, null)
		{
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000042DE File Offset: 0x000024DE
		public BufferResponseItem(byte[] buf, BaseSession.SendCompleteDelegate sendCompleteDelegate) : this(buf, 0, buf.Length, sendCompleteDelegate)
		{
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000042EC File Offset: 0x000024EC
		public BufferResponseItem(byte[] buf, int offset, int size) : this(buf, offset, size, null)
		{
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000042F8 File Offset: 0x000024F8
		public BufferResponseItem(byte[] buf, int offset, int size, BaseSession.SendCompleteDelegate sendCompleteDelegate)
		{
			this.sendCompleteDelegate = sendCompleteDelegate;
			this.responseBuf = buf;
			this.index = offset;
			this.size = size;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x0000431D File Offset: 0x0000251D
		public BaseSession.SendCompleteDelegate SendCompleteDelegate
		{
			get
			{
				return this.sendCompleteDelegate;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00004325 File Offset: 0x00002525
		protected bool DataSent
		{
			get
			{
				return this.dataSent;
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004330 File Offset: 0x00002530
		public virtual int GetNextChunk(BaseSession session, out byte[] buffer, out int offset)
		{
			if (this.dataSent)
			{
				buffer = null;
				offset = 0;
				return 0;
			}
			ProtocolSession protocolSession = session as ProtocolSession;
			if (protocolSession != null)
			{
				if (protocolSession.ProxySession != null)
				{
					protocolSession.LogSend("Proxy {0} bytes", this.size);
				}
				else
				{
					protocolSession.LogSend(this.responseBuf, this.index, this.size);
				}
			}
			this.dataSent = true;
			buffer = this.responseBuf;
			offset = this.index;
			return this.size;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000043A7 File Offset: 0x000025A7
		public void ClearData()
		{
			if (this.responseBuf != null)
			{
				Array.Clear(this.responseBuf, this.index, this.size);
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000043C8 File Offset: 0x000025C8
		public void BindData(byte[] buf, int offset, int size, BaseSession.SendCompleteDelegate sendCompleteDelegate)
		{
			this.responseBuf = buf;
			this.index = offset;
			this.size = size;
			this.dataSent = false;
			this.sendCompleteDelegate = sendCompleteDelegate;
		}

		// Token: 0x0400004F RID: 79
		private byte[] responseBuf;

		// Token: 0x04000050 RID: 80
		private int size;

		// Token: 0x04000051 RID: 81
		private int index;

		// Token: 0x04000052 RID: 82
		private bool dataSent;

		// Token: 0x04000053 RID: 83
		private BaseSession.SendCompleteDelegate sendCompleteDelegate;
	}
}
