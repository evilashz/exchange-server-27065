using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BF2 RID: 3058
	internal class DnsClient : IDisposable
	{
		// Token: 0x06004309 RID: 17161 RVA: 0x000B3020 File Offset: 0x000B1220
		internal DnsClient(int id, IPAddress dnsServer)
		{
			this.dnsServer = dnsServer;
			this.id = id;
			ExTraceGlobals.DNSTracer.TraceDebug<int, IPAddress>((long)this.GetHashCode(), "Created({0}), server {1}", this.id, dnsServer);
		}

		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x0600430A RID: 17162 RVA: 0x000B306E File Offset: 0x000B126E
		internal int Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x0600430B RID: 17163 RVA: 0x000B3076 File Offset: 0x000B1276
		private bool UseTcpOnly
		{
			get
			{
				return (this.request.DnsQueryOptions & DnsQueryOptions.UseTcpOnly) != DnsQueryOptions.None;
			}
		}

		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x0600430C RID: 17164 RVA: 0x000B308B File Offset: 0x000B128B
		private bool AcceptTruncatedResponse
		{
			get
			{
				return (this.request.DnsQueryOptions & DnsQueryOptions.AcceptTruncatedResponse) != DnsQueryOptions.None;
			}
		}

		// Token: 0x0600430D RID: 17165 RVA: 0x000B30A0 File Offset: 0x000B12A0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600430E RID: 17166 RVA: 0x000B30B0 File Offset: 0x000B12B0
		internal bool Send(DnsAsyncRequest dnsAsyncRequest)
		{
			if (!this.TryEnterClient())
			{
				return false;
			}
			bool result;
			try
			{
				int num = Interlocked.CompareExchange(ref this.sending, 1, 0);
				if (num == 1)
				{
					ExTraceGlobals.DNSTracer.TraceDebug<int, ushort>((long)this.GetHashCode(), "Send({0}) deferred, (query id:{1})", this.id, dnsAsyncRequest.QueryIdentifier);
					result = false;
				}
				else
				{
					ExTraceGlobals.DNSTracer.TraceDebug<int, ushort>((long)this.GetHashCode(), "Send({0}), (query id:{1})", this.id, dnsAsyncRequest.QueryIdentifier);
					Socket socket = this.socket;
					bool flag = socket != null && socket.ProtocolType == ProtocolType.Udp;
					if (dnsAsyncRequest.UseTcpOnly && flag)
					{
						Request request = (Request)dnsAsyncRequest.AsyncObject;
						request.CloseSocketAndResendRequest(this);
						result = true;
					}
					else
					{
						DnsAsyncRequest dnsAsyncRequest2 = Interlocked.Exchange<DnsAsyncRequest>(ref this.request, dnsAsyncRequest);
						if (socket == null)
						{
							this.Connect();
						}
						else if (dnsAsyncRequest2 != dnsAsyncRequest || flag)
						{
							this.SendBuffer();
						}
						result = true;
					}
				}
			}
			finally
			{
				this.LeaveClient();
			}
			return result;
		}

		// Token: 0x0600430F RID: 17167 RVA: 0x000B31A8 File Offset: 0x000B13A8
		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				ExTraceGlobals.DNSTracer.TraceDebug<int>((long)this.GetHashCode(), "Disposing({0})", this.id);
				this.ShutdownClient();
			}
		}

		// Token: 0x06004310 RID: 17168 RVA: 0x000B31D0 File Offset: 0x000B13D0
		private unsafe static DnsStatus RecordsFromBytes(ushort queryId, byte[] dnsMessageBuffer, int offset, ushort messageLength, int answerCount, int authorityCount, WireDnsHeader.Response responseCode, out DnsRecordList dnsRecords)
		{
			DnsStatus result = DnsStatus.Success;
			dnsRecords = null;
			IntPtr zero = IntPtr.Zero;
			try
			{
				DnsNativeMethods.WinDnsStatus winDnsStatus;
				try
				{
					fixed (byte* ptr = &dnsMessageBuffer[offset])
					{
						winDnsStatus = DnsNativeMethods.DnsExtractRecordsFromMessage(ptr, messageLength, out zero);
					}
				}
				finally
				{
					byte* ptr = null;
				}
				DnsNativeMethods.WinDnsStatus winDnsStatus2 = winDnsStatus;
				if (winDnsStatus2 <= DnsNativeMethods.WinDnsStatus.ErrorInvalidName)
				{
					if (winDnsStatus2 == DnsNativeMethods.WinDnsStatus.Success)
					{
						dnsRecords = ((zero != IntPtr.Zero) ? DnsClient.MarshalRecordTypes(queryId, zero) : DnsRecordList.Empty);
						goto IL_D7;
					}
					if (winDnsStatus2 == DnsNativeMethods.WinDnsStatus.ErrorInvalidName)
					{
						result = DnsStatus.ErrorInvalidData;
						goto IL_D7;
					}
				}
				else
				{
					switch (winDnsStatus2)
					{
					case DnsNativeMethods.WinDnsStatus.ErrorServerFailure:
						result = DnsStatus.ServerFailure;
						goto IL_D7;
					case DnsNativeMethods.WinDnsStatus.ErrorRCodeNameError:
						result = DnsStatus.InfoDomainNonexistent;
						goto IL_D7;
					default:
						switch (winDnsStatus2)
						{
						case DnsNativeMethods.WinDnsStatus.InfoNoRecords:
							result = DnsStatus.InfoNoRecords;
							goto IL_D7;
						case DnsNativeMethods.WinDnsStatus.ErrorBadPacket:
							result = ((answerCount == 0 && authorityCount == 0 && responseCode == WireDnsHeader.Response.NoError) ? DnsStatus.InfoNoRecords : DnsStatus.ErrorInvalidData);
							goto IL_D7;
						}
						break;
					}
				}
				Trace dnstracer = ExTraceGlobals.DNSTracer;
				long num = 0L;
				string str = "DnsExtractRecordsFromMessage returned unexpected error code 0x";
				int num2 = (int)winDnsStatus;
				dnstracer.TraceError(num, str + num2.ToString("X", NumberFormatInfo.InvariantInfo));
				result = DnsStatus.InfoNoRecords;
				IL_D7:;
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					DnsNativeMethods.DnsRecordListFree(zero, FreeType.RecordList);
				}
			}
			return result;
		}

		// Token: 0x06004311 RID: 17169 RVA: 0x000B32E8 File Offset: 0x000B14E8
		private static DnsRecordList MarshalRecordTypes(ushort queryId, IntPtr results)
		{
			IntPtr intPtr = results;
			DnsRecordList dnsRecordList = new DnsRecordList();
			Win32DnsRecordHeader header = (Win32DnsRecordHeader)Marshal.PtrToStructure(intPtr, typeof(Win32DnsRecordHeader));
			for (;;)
			{
				IntPtr dataPointer = new IntPtr((long)intPtr + (long)Win32DnsRecordHeader.MarshalSize);
				DnsRecordType recordType = header.recordType;
				if (recordType <= DnsRecordType.TXT)
				{
					switch (recordType)
					{
					case DnsRecordType.A:
						dnsRecordList.Add(new DnsARecord(header, dataPointer));
						break;
					case DnsRecordType.NS:
						dnsRecordList.Add(new DnsNsRecord(header, dataPointer));
						break;
					case (DnsRecordType)3:
					case (DnsRecordType)4:
						break;
					case DnsRecordType.CNAME:
						dnsRecordList.Add(new DnsCNameRecord(header, dataPointer));
						break;
					case DnsRecordType.SOA:
						dnsRecordList.Add(new DnsSoaRecord(header, dataPointer));
						break;
					default:
						switch (recordType)
						{
						case DnsRecordType.PTR:
							dnsRecordList.Add(new DnsPtrRecord(header, dataPointer));
							break;
						case DnsRecordType.MX:
							dnsRecordList.Add(new DnsMxRecord(header, dataPointer));
							break;
						case DnsRecordType.TXT:
							dnsRecordList.Add(new DnsTxtRecord(header, dataPointer));
							break;
						}
						break;
					}
				}
				else if (recordType != DnsRecordType.AAAA)
				{
					if (recordType == DnsRecordType.SRV)
					{
						dnsRecordList.Add(new DnsSrvRecord(header, dataPointer));
					}
				}
				else
				{
					dnsRecordList.Add(new DnsAAAARecord(header, dataPointer));
				}
				if (header.nextRecord == IntPtr.Zero)
				{
					break;
				}
				intPtr = header.nextRecord;
				header = (Win32DnsRecordHeader)Marshal.PtrToStructure(intPtr, typeof(Win32DnsRecordHeader));
			}
			return dnsRecordList;
		}

		// Token: 0x06004312 RID: 17170 RVA: 0x000B3448 File Offset: 0x000B1648
		private void Connect()
		{
			if (this.UseTcpOnly)
			{
				this.useTcp = true;
			}
			ExTraceGlobals.DNSTracer.TraceDebug<string, int, IPAddress>((long)this.GetHashCode(), "Connect{0}({1}), server {2}", this.useTcp ? "Tcp" : "Udp", this.id, this.dnsServer);
			try
			{
				if (this.useTcp)
				{
					this.socket = new Socket(this.dnsServer.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
					this.SetRandomizeSocketOption();
					this.socket.BeginConnect(this.dnsServer, 53, new AsyncCallback(this.TcpConnectComplete), this.socket);
				}
				else
				{
					this.socket = new Socket(this.dnsServer.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
					this.SetRandomizeSocketOption();
					this.socket.Connect(this.dnsServer, 53);
					this.ConnectComplete();
				}
			}
			catch (SocketException)
			{
				this.CloseDnsSocket();
			}
			catch (ObjectDisposedException)
			{
			}
		}

		// Token: 0x06004313 RID: 17171 RVA: 0x000B354C File Offset: 0x000B174C
		private void SetRandomizeSocketOption()
		{
			try
			{
				this.socket.SetSocketOption(SocketOptionLevel.Socket, (SocketOptionName)12293, true);
			}
			catch (SocketException)
			{
				ExTraceGlobals.DNSTracer.TraceDebug<string, int, IPAddress>((long)this.GetHashCode(), "Failed to set randomize option - {0}({1}), server {2}", this.useTcp ? "Tcp" : "Udp", this.id, this.dnsServer);
			}
		}

		// Token: 0x06004314 RID: 17172 RVA: 0x000B35BC File Offset: 0x000B17BC
		private void CloseDnsSocket()
		{
			DnsAsyncRequest dnsAsyncRequest = this.request;
			if (dnsAsyncRequest != null)
			{
				Request request = (Request)dnsAsyncRequest.AsyncObject;
				request.CloseClient(this);
			}
		}

		// Token: 0x06004315 RID: 17173 RVA: 0x000B35E8 File Offset: 0x000B17E8
		private void TcpConnectComplete(IAsyncResult asyncResult)
		{
			Socket socket = (Socket)asyncResult.AsyncState;
			if (this.TryEnterClient())
			{
				try
				{
					socket.EndConnect(asyncResult);
					this.ConnectComplete();
				}
				catch (SocketException)
				{
					this.CloseDnsSocket();
				}
				catch (ObjectDisposedException)
				{
				}
				finally
				{
					this.LeaveClient();
				}
			}
		}

		// Token: 0x06004316 RID: 17174 RVA: 0x000B3658 File Offset: 0x000B1858
		private void ConnectComplete()
		{
			this.SendBuffer();
		}

		// Token: 0x06004317 RID: 17175 RVA: 0x000B3660 File Offset: 0x000B1860
		private void PendReceiveIfNecessary()
		{
			if (Interlocked.CompareExchange(ref this.receiving, 1, 0) == 0)
			{
				this.PendReceive();
			}
		}

		// Token: 0x06004318 RID: 17176 RVA: 0x000B3684 File Offset: 0x000B1884
		private void PendReceive()
		{
			Socket socket = this.socket;
			if (socket == null)
			{
				return;
			}
			SocketError socketError;
			try
			{
				ExTraceGlobals.DNSTracer.TraceDebug<int>((long)this.GetHashCode(), "PendReceive({0})", this.id);
				this.receiveBuffer.ShuffleBuffer();
				socket.BeginReceive(this.receiveBuffer.Buffer, this.receiveBuffer.UnusedStartOffset, this.receiveBuffer.Unused, SocketFlags.None, out socketError, new AsyncCallback(this.ReceiveComplete), socket);
			}
			catch (SocketException)
			{
				socketError = SocketError.SocketError;
			}
			catch (ObjectDisposedException)
			{
				socketError = SocketError.SocketError;
			}
			if (socketError != SocketError.Success)
			{
				this.CloseDnsSocket();
			}
		}

		// Token: 0x06004319 RID: 17177 RVA: 0x000B372C File Offset: 0x000B192C
		private void ReceiveComplete(IAsyncResult asyncResult)
		{
			if (this.TryEnterClient())
			{
				try
				{
					this.InternalReceiveComplete(asyncResult);
				}
				finally
				{
					this.LeaveClient();
				}
			}
		}

		// Token: 0x0600431A RID: 17178 RVA: 0x000B3764 File Offset: 0x000B1964
		private void InternalReceiveComplete(IAsyncResult asyncResult)
		{
			Socket socket = (Socket)asyncResult.AsyncState;
			int num = 0;
			SocketError socketError;
			try
			{
				num = socket.EndReceive(asyncResult, out socketError);
			}
			catch (ObjectDisposedException)
			{
				socketError = SocketError.SocketError;
			}
			if (socketError != SocketError.Success)
			{
				this.CloseDnsSocket();
				return;
			}
			if (num == 0)
			{
				this.CloseDnsSocket();
				return;
			}
			this.receiveBuffer.ReportBytesFilled(num);
			DnsAsyncRequest dnsAsyncRequest = this.request;
			if (dnsAsyncRequest == null)
			{
				return;
			}
			Request request = (Request)dnsAsyncRequest.AsyncObject;
			bool flag = socket.ProtocolType == ProtocolType.Udp;
			int num2;
			if (flag)
			{
				if (num > request.MaxWireDataSize)
				{
					this.receiveBuffer.EmptyBuffer();
					this.receiving = 0;
					this.CompleteRequest(dnsAsyncRequest, DnsStatus.ErrorExcessiveData, null);
					return;
				}
				if (num < WireDnsHeader.MarshalSize)
				{
					this.receiveBuffer.EmptyBuffer();
					this.PendReceive();
					return;
				}
				num2 = num;
				this.bytesExpected = num;
				this.recvNetworkBuffer = this.receiveBuffer.Buffer;
				this.startIndex = this.receiveBuffer.DataStartOffset;
			}
			else
			{
				if (this.bytesExpected == 0)
				{
					if (this.receiveBuffer.Remaining < 2)
					{
						this.PendReceive();
						return;
					}
					ushort num3 = BitConverter.ToUInt16(this.receiveBuffer.Buffer, this.receiveBuffer.DataStartOffset);
					num3 = (ushort)IPAddress.NetworkToHostOrder((short)num3);
					if ((int)num3 > request.MaxWireDataSize || (int)num3 < WireDnsHeader.MarshalSize)
					{
						this.CloseDnsSocket();
						if ((int)num3 > request.MaxWireDataSize)
						{
							this.CompleteRequest(dnsAsyncRequest, DnsStatus.ErrorExcessiveData, null);
							return;
						}
						this.CompleteRequest(dnsAsyncRequest, DnsStatus.ErrorInvalidData, null);
						return;
					}
					else
					{
						this.bytesExpected = (int)num3;
						this.receiveBuffer.ConsumeData(2);
					}
				}
				if (this.recvNetworkBuffer == null)
				{
					if (this.bytesExpected > this.receiveBuffer.Remaining)
					{
						this.recvNetworkBuffer = new byte[this.bytesExpected];
						Buffer.BlockCopy(this.receiveBuffer.Buffer, this.receiveBuffer.DataStartOffset, this.recvNetworkBuffer, 0, this.receiveBuffer.Remaining);
						this.startIndex = this.receiveBuffer.Remaining;
						this.receiveBuffer.ConsumeData(this.receiveBuffer.Remaining);
						this.PendReceive();
						return;
					}
					num2 = this.bytesExpected;
					this.recvNetworkBuffer = this.receiveBuffer.Buffer;
					this.startIndex = this.receiveBuffer.DataStartOffset;
				}
				else
				{
					num2 = Math.Min(this.receiveBuffer.Remaining, this.bytesExpected - this.startIndex);
					Buffer.BlockCopy(this.receiveBuffer.Buffer, this.receiveBuffer.DataStartOffset, this.recvNetworkBuffer, this.startIndex, num2);
					this.startIndex += num2;
					if (this.startIndex != this.bytesExpected)
					{
						this.receiveBuffer.ConsumeData(num2);
						this.PendReceive();
						return;
					}
					this.startIndex = 0;
				}
			}
			WireDnsHeader wireDnsHeader = WireDnsHeader.NetworkToHostOrder(this.recvNetworkBuffer, this.startIndex);
			ExTraceGlobals.DNSTracer.TraceDebug<int, ushort, ushort>((long)this.GetHashCode(), "ReceiveData({0}), (query id:{1}), wire query id {2}", this.id, dnsAsyncRequest.QueryIdentifier, wireDnsHeader.xid);
			if (wireDnsHeader.xid != dnsAsyncRequest.QueryIdentifier || !wireDnsHeader.IsResponse)
			{
				this.receiveBuffer.ConsumeData(num2);
				this.DiscardRecvNetworkBuffer();
				this.PendReceive();
				return;
			}
			bool flag2 = false;
			if (flag && wireDnsHeader.IsTruncated)
			{
				if (!this.AcceptTruncatedResponse)
				{
					dnsAsyncRequest.UseTcpOnly = true;
					request.CloseSocketAndResendRequest(this);
					return;
				}
				flag2 = true;
			}
			DnsRecordList list;
			DnsStatus dnsStatus = DnsClient.RecordsFromBytes(wireDnsHeader.xid, this.recvNetworkBuffer, this.startIndex, (ushort)this.bytesExpected, wireDnsHeader.Answers, wireDnsHeader.AuthorityRecords, wireDnsHeader.ResponseCode, out list);
			this.receiveBuffer.ConsumeData(num2);
			this.DiscardRecvNetworkBuffer();
			if (dnsStatus == DnsStatus.Success && flag2)
			{
				dnsStatus = DnsStatus.InfoTruncated;
			}
			else if (dnsStatus == DnsStatus.ErrorInvalidData || dnsStatus == DnsStatus.ServerFailure)
			{
				ExTraceGlobals.DNSTracer.TraceDebug<int, ushort, DnsStatus>((long)this.GetHashCode(), "ReceiveComplete({0}), (query id:{1}), Status:{2}", this.id, dnsAsyncRequest.QueryIdentifier, dnsStatus);
				dnsAsyncRequest.ErrorCode = (int)dnsStatus;
				dnsAsyncRequest.SetClientError(this.id);
				if (dnsAsyncRequest.ExceedsDnsErrorLimit)
				{
					ExTraceGlobals.DNSTracer.TraceDebug<DnsQuery, ushort>((long)this.GetHashCode(), "Query:{0}, with id:{1}) resulted in errors from all Dns servers", dnsAsyncRequest.Query, dnsAsyncRequest.QueryIdentifier);
					this.receiving = 0;
					this.CompleteRequest(dnsAsyncRequest, dnsStatus, list);
					return;
				}
				this.PendReceive();
				return;
			}
			ExTraceGlobals.DNSTracer.TraceDebug<int, ushort>((long)this.GetHashCode(), "ReceiveComplete({0}), (query id:{1})", this.id, dnsAsyncRequest.QueryIdentifier);
			this.receiving = 0;
			this.CompleteRequest(dnsAsyncRequest, dnsStatus, list);
		}

		// Token: 0x0600431B RID: 17179 RVA: 0x000B3BE8 File Offset: 0x000B1DE8
		private void DiscardRecvNetworkBuffer()
		{
			this.startIndex = 0;
			this.recvNetworkBuffer = null;
			this.bytesExpected = 0;
		}

		// Token: 0x0600431C RID: 17180 RVA: 0x000B3C00 File Offset: 0x000B1E00
		private void CompleteRequest(DnsAsyncRequest dnsAsyncRequest, DnsStatus status, DnsRecordList list)
		{
			DnsAsyncRequest dnsAsyncRequest2 = Interlocked.CompareExchange<DnsAsyncRequest>(ref this.request, null, dnsAsyncRequest);
			if (dnsAsyncRequest2 == dnsAsyncRequest)
			{
				ExTraceGlobals.DNSTracer.TraceDebug<int, ushort>((long)this.GetHashCode(), "CompleteRequest({0}), (query id:{1})", this.id, dnsAsyncRequest.QueryIdentifier);
				if (status == DnsStatus.ErrorTimeout && dnsAsyncRequest.ErrorCode != 0)
				{
					status = (DnsStatus)dnsAsyncRequest.ErrorCode;
				}
				TimeSpan timeSpan;
				switch (status)
				{
				case DnsStatus.Success:
					timeSpan = TimeSpan.MaxValue;
					if (list != null)
					{
						foreach (DnsRecord dnsRecord in list)
						{
							if (dnsRecord.TimeToLive < timeSpan)
							{
								timeSpan = dnsRecord.TimeToLive;
							}
						}
					}
					timeSpan = DnsResult.TimeToLiveWithinLimits(timeSpan);
					goto IL_E5;
				case DnsStatus.InfoNoRecords:
				case DnsStatus.InfoDomainNonexistent:
				case DnsStatus.InfoMxLoopback:
				case DnsStatus.ErrorInvalidData:
				case DnsStatus.ErrorExcessiveData:
				case DnsStatus.InfoTruncated:
					timeSpan = DnsResult.ErrorTimeToLive;
					goto IL_E5;
				}
				timeSpan = TimeSpan.Zero;
				IL_E5:
				dnsAsyncRequest.InvokeCallback(new DnsResult(status, this.dnsServer, list, timeSpan));
				return;
			}
			if (dnsAsyncRequest2 != null)
			{
				this.PendReceiveIfNecessary();
			}
		}

		// Token: 0x0600431D RID: 17181 RVA: 0x000B3D24 File Offset: 0x000B1F24
		private void SendBuffer()
		{
			Socket socket = this.socket;
			if (socket == null)
			{
				return;
			}
			DnsAsyncRequest dnsAsyncRequest = this.request;
			if (dnsAsyncRequest == null || !dnsAsyncRequest.IsValid)
			{
				return;
			}
			SocketError socketError = SocketError.Success;
			int num = 0;
			int num2 = dnsAsyncRequest.Buffer.Length;
			if (!this.useTcp)
			{
				num += 2;
				num2 -= 2;
			}
			try
			{
				socket.BeginSend(dnsAsyncRequest.Buffer, num, num2, SocketFlags.None, out socketError, new AsyncCallback(this.SendComplete), socket);
				if (socketError != SocketError.Success && socketError != SocketError.IOPending)
				{
					this.CloseDnsSocket();
				}
			}
			catch (SocketException)
			{
				this.CloseDnsSocket();
			}
			catch (ObjectDisposedException)
			{
			}
		}

		// Token: 0x0600431E RID: 17182 RVA: 0x000B3DCC File Offset: 0x000B1FCC
		private void SendComplete(IAsyncResult asyncResult)
		{
			Socket socket = (Socket)asyncResult.AsyncState;
			if (!this.TryEnterClient())
			{
				return;
			}
			try
			{
				SocketError socketError;
				try
				{
					socket.EndSend(asyncResult, out socketError);
					this.sending = 0;
					ExTraceGlobals.DNSTracer.TraceDebug<int>((long)this.GetHashCode(), "SendComplete({0})", this.id);
				}
				catch (ObjectDisposedException)
				{
					ExTraceGlobals.DNSTracer.TraceError<int>((long)this.GetHashCode(), "SendComplete({0}), Disposed", this.id);
					socketError = SocketError.SocketError;
				}
				DnsLog.Log(this.request, "Send completed. Error={0}; Details={1}. Server={2}, TCP={3}", new object[]
				{
					socketError,
					this.request,
					this.dnsServer,
					this.useTcp
				});
				if (socketError == SocketError.Success)
				{
					this.PendReceiveIfNecessary();
				}
				else
				{
					ExTraceGlobals.DNSTracer.TraceError<int, SocketError>((long)this.GetHashCode(), "SendComplete({0}), Error {1}", this.id, socketError);
					this.CloseDnsSocket();
				}
			}
			finally
			{
				this.LeaveClient();
			}
		}

		// Token: 0x0600431F RID: 17183 RVA: 0x000B3ED4 File Offset: 0x000B20D4
		private bool TryEnterClient()
		{
			int num;
			for (int i = this.executingThreads; i >= 0; i = num)
			{
				num = Interlocked.CompareExchange(ref this.executingThreads, i + 1, i);
				if (i == num)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004320 RID: 17184 RVA: 0x000B3F09 File Offset: 0x000B2109
		private void LeaveClient()
		{
			if (Interlocked.Decrement(ref this.executingThreads) == -2147483648)
			{
				this.ReleaseResources();
			}
		}

		// Token: 0x06004321 RID: 17185 RVA: 0x000B3F24 File Offset: 0x000B2124
		private void ShutdownClient()
		{
			int num = this.executingThreads;
			int num2;
			while ((num2 = Interlocked.CompareExchange(ref this.executingThreads, num | -2147483648, num)) != num)
			{
				num = num2;
			}
			if (num2 == 0)
			{
				this.ReleaseResources();
			}
		}

		// Token: 0x06004322 RID: 17186 RVA: 0x000B3F60 File Offset: 0x000B2160
		private void ReleaseResources()
		{
			this.executingThreads = -16777216;
			if (this.socket != null)
			{
				this.socket.Close();
				this.socket = null;
			}
			if (this.receiveBuffer != null)
			{
				this.receiveBuffer.Dispose();
				this.receiveBuffer = null;
			}
		}

		// Token: 0x04003919 RID: 14617
		private const int DnsPort = 53;

		// Token: 0x0400391A RID: 14618
		private const int SignBit = -2147483648;

		// Token: 0x0400391B RID: 14619
		private const int ShuttingDown = -16777216;

		// Token: 0x0400391C RID: 14620
		private const SocketOptionName RandomizePort = (SocketOptionName)12293;

		// Token: 0x0400391D RID: 14621
		private readonly IPAddress dnsServer;

		// Token: 0x0400391E RID: 14622
		private readonly int id;

		// Token: 0x0400391F RID: 14623
		private DnsAsyncRequest request;

		// Token: 0x04003920 RID: 14624
		private bool useTcp;

		// Token: 0x04003921 RID: 14625
		private Socket socket;

		// Token: 0x04003922 RID: 14626
		private NetworkBuffer receiveBuffer = new NetworkBuffer(4096);

		// Token: 0x04003923 RID: 14627
		private int bytesExpected;

		// Token: 0x04003924 RID: 14628
		private int sending;

		// Token: 0x04003925 RID: 14629
		private int receiving;

		// Token: 0x04003926 RID: 14630
		private volatile int executingThreads;

		// Token: 0x04003927 RID: 14631
		private byte[] recvNetworkBuffer;

		// Token: 0x04003928 RID: 14632
		private int startIndex;
	}
}
