using System;
using System.IO;
using System.Net;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000079 RID: 121
	internal class RpcHttpOutDataResponseStreamProxy : StreamProxy
	{
		// Token: 0x060003AE RID: 942 RVA: 0x00015E0C File Offset: 0x0001400C
		internal RpcHttpOutDataResponseStreamProxy(StreamProxy.StreamProxyType streamProxyType, Stream source, Stream target, byte[] buffer, IRequestContext requestContext) : base(streamProxyType, source, target, buffer, requestContext)
		{
			this.connectTimeout = RpcHttpOutDataResponseStreamProxy.RpcHttpOutConnectingTimeoutInSeconds.Value;
			this.isConnecting = (this.connectTimeout != TimeSpan.Zero);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00015E4C File Offset: 0x0001404C
		internal RpcHttpOutDataResponseStreamProxy(StreamProxy.StreamProxyType streamProxyType, Stream source, Stream target, BufferPoolCollection.BufferSize maxBufferSize, BufferPoolCollection.BufferSize minBufferSize, IRequestContext requestContext) : base(streamProxyType, source, target, maxBufferSize, minBufferSize, requestContext)
		{
			this.connectTimeout = RpcHttpOutDataResponseStreamProxy.RpcHttpOutConnectingTimeoutInSeconds.Value;
			this.isConnecting = (this.connectTimeout != TimeSpan.Zero);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00015E9C File Offset: 0x0001409C
		protected override byte[] GetUpdatedBufferToSend(ArraySegment<byte> buffer)
		{
			if (!this.isConnecting)
			{
				return null;
			}
			if (RpcHttpPackets.IsConnA3PacketInBuffer(buffer))
			{
				this.endTime = new ExDateTime?(ExDateTime.Now + this.connectTimeout);
			}
			if (RpcHttpPackets.IsConnC2PacketInBuffer(buffer))
			{
				this.isConnecting = false;
				this.endTime = null;
			}
			if (RpcHttpPackets.IsPingPacket(buffer) && this.endTime != null && ExDateTime.Now >= this.endTime.Value)
			{
				Exception ex = new HttpProxyException(HttpStatusCode.InternalServerError, HttpProxySubErrorCode.RpcHttpConnectionEstablishmentTimeout, "Outbound proxy connection timed out");
				throw ex;
			}
			return null;
		}

		// Token: 0x040002AE RID: 686
		private static readonly TimeSpanAppSettingsEntry RpcHttpOutConnectingTimeoutInSeconds = new TimeSpanAppSettingsEntry(HttpProxySettings.Prefix("RpcHttpOutConnectingTimeoutInSeconds"), TimeSpanUnit.Seconds, TimeSpan.FromSeconds(0.0), ExTraceGlobals.VerboseTracer);

		// Token: 0x040002AF RID: 687
		private readonly TimeSpan connectTimeout = TimeSpan.Zero;

		// Token: 0x040002B0 RID: 688
		private bool isConnecting;

		// Token: 0x040002B1 RID: 689
		private ExDateTime? endTime;
	}
}
