using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BD6 RID: 3030
	internal interface INetworkConnection : IDisposable
	{
		// Token: 0x17001048 RID: 4168
		// (get) Token: 0x0600419C RID: 16796
		long ConnectionId { get; }

		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x0600419D RID: 16797
		// (set) Token: 0x0600419E RID: 16798
		int ReceiveTimeout { get; set; }

		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x0600419F RID: 16799
		// (set) Token: 0x060041A0 RID: 16800
		int SendTimeout { get; set; }

		// Token: 0x1700104B RID: 4171
		// (set) Token: 0x060041A1 RID: 16801
		int Timeout { set; }

		// Token: 0x1700104C RID: 4172
		// (get) Token: 0x060041A2 RID: 16802
		IPEndPoint LocalEndPoint { get; }

		// Token: 0x1700104D RID: 4173
		// (get) Token: 0x060041A3 RID: 16803
		IPEndPoint RemoteEndPoint { get; }

		// Token: 0x1700104E RID: 4174
		// (get) Token: 0x060041A4 RID: 16804
		// (set) Token: 0x060041A5 RID: 16805
		int MaxLineLength { get; set; }

		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x060041A6 RID: 16806
		long BytesReceived { get; }

		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x060041A7 RID: 16807
		long BytesSent { get; }

		// Token: 0x17001051 RID: 4177
		// (get) Token: 0x060041A8 RID: 16808
		bool IsLineAvailable { get; }

		// Token: 0x17001052 RID: 4178
		// (get) Token: 0x060041A9 RID: 16809
		bool IsTls { get; }

		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x060041AA RID: 16810
		byte[] TlsEapKey { get; }

		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x060041AB RID: 16811
		int TlsCipherKeySize { get; }

		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x060041AC RID: 16812
		ConnectionInfo TlsConnectionInfo { get; }

		// Token: 0x17001056 RID: 4182
		// (get) Token: 0x060041AD RID: 16813
		IX509Certificate2 RemoteCertificate { get; }

		// Token: 0x17001057 RID: 4183
		// (get) Token: 0x060041AE RID: 16814
		IX509Certificate2 LocalCertificate { get; }

		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x060041AF RID: 16815
		ChannelBindingToken ChannelBindingToken { get; }

		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x060041B0 RID: 16816
		// (set) Token: 0x060041B1 RID: 16817
		SchannelProtocols ServerTlsProtocols { get; set; }

		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x060041B2 RID: 16818
		// (set) Token: 0x060041B3 RID: 16819
		SchannelProtocols ClientTlsProtocols { get; set; }

		// Token: 0x060041B4 RID: 16820
		void Shutdown();

		// Token: 0x060041B5 RID: 16821
		void Shutdown(int waitSeconds);

		// Token: 0x060041B6 RID: 16822
		IAsyncResult BeginRead(AsyncCallback callback, object state);

		// Token: 0x060041B7 RID: 16823
		void EndRead(IAsyncResult asyncResult, out byte[] buffer, out int offset, out int size, out object errorCode);

		// Token: 0x060041B8 RID: 16824
		Task<NetworkConnection.LazyAsyncResultWithTimeout> ReadAsync();

		// Token: 0x060041B9 RID: 16825
		void PutBackReceivedBytes(int bytesUnconsumed);

		// Token: 0x060041BA RID: 16826
		IAsyncResult BeginReadLine(AsyncCallback callback, object state);

		// Token: 0x060041BB RID: 16827
		void EndReadLine(IAsyncResult asyncResult, out byte[] buffer, out int offset, out int size, out object errorCode);

		// Token: 0x060041BC RID: 16828
		Task<NetworkConnection.LazyAsyncResultWithTimeout> ReadLineAsync();

		// Token: 0x060041BD RID: 16829
		IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state);

		// Token: 0x060041BE RID: 16830
		IAsyncResult BeginWrite(Stream stream, AsyncCallback callback, object state);

		// Token: 0x060041BF RID: 16831
		void EndWrite(IAsyncResult asyncResult, out object errorCode);

		// Token: 0x060041C0 RID: 16832
		Task<object> WriteAsync(byte[] buffer, int offset, int size);

		// Token: 0x060041C1 RID: 16833
		IAsyncResult BeginNegotiateTlsAsClient(X509Certificate certificate, string targetName, AsyncCallback callback, object state);

		// Token: 0x060041C2 RID: 16834
		void EndNegotiateTlsAsClient(IAsyncResult asyncResult, out object errorCode);

		// Token: 0x060041C3 RID: 16835
		Task<object> NegotiateTlsAsClientAsync(IX509Certificate2 certificate, string targetName);

		// Token: 0x060041C4 RID: 16836
		IAsyncResult BeginNegotiateTlsAsServer(X509Certificate2 cert, bool requestClientCertificate, AsyncCallback callback, object state);

		// Token: 0x060041C5 RID: 16837
		void EndNegotiateTlsAsServer(IAsyncResult asyncResult, out object errorCode);

		// Token: 0x060041C6 RID: 16838
		Task<object> NegotiateTlsAsServerAsync(IX509Certificate2 certificate, bool requestClientCertificate);
	}
}
