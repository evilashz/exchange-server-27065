using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000260 RID: 608
	internal class NetworkPackagingLayer
	{
		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x060017B4 RID: 6068 RVA: 0x00061F28 File Offset: 0x00060128
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.NetworkChannelTracer;
			}
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x00061F30 File Offset: 0x00060130
		private static void ReadCallback(IAsyncResult ar)
		{
			NetworkPackagingLayer networkPackagingLayer = (NetworkPackagingLayer)ar.AsyncState;
			NetworkPackagingLayer.Tracer.TraceDebug<bool, int>(0L, "ReadCallback completion was sync: {0} arhash={1}", ar.CompletedSynchronously, ar.GetHashCode());
			Exception ex = null;
			NetworkChannelCallback networkChannelCallback = null;
			object asyncState = null;
			bool flag = false;
			int num = 0;
			try
			{
				num = networkPackagingLayer.m_tcpChannel.Stream.EndRead(ar);
				ExTraceGlobals.NetworkChannelTracer.TraceDebug<int, string, string>((long)networkPackagingLayer.GetHashCode(), "AsyncReadCallback read {0} bytes from {1} on {2}", num, networkPackagingLayer.m_netChannel.RemoteEndPointString, networkPackagingLayer.m_netChannel.LocalEndPointString);
				flag = true;
			}
			catch (IOException ex2)
			{
				ex = ex2;
			}
			catch (Win32Exception ex3)
			{
				ex = ex3;
			}
			catch (ObjectDisposedException ex4)
			{
				NetworkPackagingLayer.Tracer.TraceError<bool, string>(0L, "ReadCallback has closed={0} ignoring exception {1}", networkPackagingLayer.m_closeCalled, ex4.Message);
			}
			catch (InvalidOperationException ex5)
			{
				NetworkPackagingLayer.Tracer.TraceError<string>(0L, "ReadCallback ignoring exception {0}", ex5.Message);
			}
			finally
			{
				lock (networkPackagingLayer)
				{
					networkChannelCallback = networkPackagingLayer.m_userCallback;
					asyncState = networkPackagingLayer.m_userCallbackCtx;
					networkPackagingLayer.m_asyncException = ex;
					if (flag)
					{
						networkPackagingLayer.m_asyncReadByteCount = num;
						if (num == 0)
						{
							networkPackagingLayer.m_asyncReadGotZeroBytes = true;
						}
					}
					else
					{
						networkPackagingLayer.m_asyncReadByteCount = 0;
					}
					networkPackagingLayer.m_asyncReadActive = false;
					networkPackagingLayer.m_asyncReadCompleteEvent.Set();
				}
			}
			if (networkChannelCallback != null)
			{
				networkChannelCallback(asyncState, num, ar.CompletedSynchronously, ex);
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x000620CC File Offset: 0x000602CC
		internal string PartnerNodeName
		{
			get
			{
				return this.m_tcpChannel.PartnerNodeName;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x060017B7 RID: 6071 RVA: 0x000620D9 File Offset: 0x000602D9
		// (set) Token: 0x060017B8 RID: 6072 RVA: 0x000620E1 File Offset: 0x000602E1
		internal NetworkChannel.DataEncodingScheme Encoding
		{
			get
			{
				return this.m_encoding;
			}
			set
			{
				this.m_encoding = value;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x000620EA File Offset: 0x000602EA
		// (set) Token: 0x060017BA RID: 6074 RVA: 0x000620F2 File Offset: 0x000602F2
		internal CompressionConfig CompressionConfig { get; private set; }

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x000620FB File Offset: 0x000602FB
		private CoconetConfig CoconetConfig
		{
			get
			{
				return this.CompressionConfig.CoconetConfig;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x00062108 File Offset: 0x00060308
		// (set) Token: 0x060017BD RID: 6077 RVA: 0x00062110 File Offset: 0x00060310
		internal long TotalDecompressedBytesReceived { get; private set; }

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x00062119 File Offset: 0x00060319
		// (set) Token: 0x060017BF RID: 6079 RVA: 0x00062121 File Offset: 0x00060321
		internal long TotalCompressedBytesReceived { get; private set; }

		// Token: 0x060017C0 RID: 6080 RVA: 0x0006212A File Offset: 0x0006032A
		internal void SetEncoding(CompressionConfig cfg)
		{
			if (cfg.Provider == CompressionConfig.CompressionProvider.Coconet)
			{
				this.Encoding = NetworkChannel.DataEncodingScheme.Coconet;
			}
			else if (cfg.Provider == CompressionConfig.CompressionProvider.Xpress)
			{
				this.Encoding = NetworkChannel.DataEncodingScheme.CompressedXpress;
			}
			else
			{
				this.Encoding = NetworkChannel.DataEncodingScheme.Uncompressed;
			}
			this.CompressionConfig = cfg;
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x0006215E File Offset: 0x0006035E
		internal NetworkPackagingLayer(NetworkChannel netChannel, TcpChannel tcpChannel)
		{
			this.m_netChannel = netChannel;
			this.m_tcpChannel = tcpChannel;
			this.m_readPacketHeaderBuf = new byte[Math.Max(Math.Max(5, 5), 9)];
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x00062190 File Offset: 0x00060390
		internal void Close()
		{
			this.m_closeCalled = true;
			this.m_tcpChannel.Close();
			if (this.m_coconetCompressor != null)
			{
				this.m_coconetCompressor.Dispose();
			}
			if (this.m_coconetDecompressor != null)
			{
				this.m_coconetDecompressor.Dispose();
			}
			int num = 0;
			while (!this.WaitForAsyncRead(1000))
			{
				num++;
				ExTraceGlobals.NetworkChannelTracer.TraceError<int>((long)this.GetHashCode(), "Timeout while aborting...still waiting for async read to complete after {0} secs", num);
				if (num > 30)
				{
					DiagCore.RetailAssert(false, "TCP stack is not responding properly", new object[0]);
				}
			}
			this.DisposeCompletionEvent();
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x00062220 File Offset: 0x00060420
		private void DisposeCompletionEvent()
		{
			lock (this)
			{
				if (this.m_asyncReadCompleteEvent != null)
				{
					((IDisposable)this.m_asyncReadCompleteEvent).Dispose();
					this.m_asyncReadCompleteEvent = null;
				}
			}
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x00062270 File Offset: 0x00060470
		private void WriteLeadingBytes(NetworkPackagingLayer.PackageEncoding packetType, int leadingContentLength, byte[] content, int offsetInContent, int totalContentLength, bool flush)
		{
			int num = 5 + leadingContentLength;
			byte[] array = new byte[num];
			array[0] = (byte)packetType;
			int num2 = 1;
			Serialization.SerializeUInt32(array, ref num2, (uint)totalContentLength);
			Array.Copy(content, offsetInContent, array, 5, leadingContentLength);
			if (flush)
			{
				this.m_tcpChannel.WriteAndFlush(array, 0, num);
				return;
			}
			this.m_tcpChannel.Write(array, 0, num);
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x000622C8 File Offset: 0x000604C8
		private void WriteInternal(byte[] buf, int off, int len, int minLeadingLength, bool flush)
		{
			if (this.Encoding == NetworkChannel.DataEncodingScheme.CompressedXpress && len > 265)
			{
				this.WriteXpress(buf, off, len);
				return;
			}
			if (this.Encoding == NetworkChannel.DataEncodingScheme.Coconet && len > 265)
			{
				this.WriteCoconet(buf, off, len);
				return;
			}
			int num = Math.Min(len, minLeadingLength);
			int num2 = len - num;
			this.WriteLeadingBytes(NetworkPackagingLayer.PackageEncoding.Raw, num, buf, off, len, flush);
			if (num2 > 0)
			{
				this.m_tcpChannel.Write(buf, off + num, num2);
			}
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x00062339 File Offset: 0x00060539
		internal void Write(byte[] buf, int off, int len)
		{
			this.WriteInternal(buf, off, len, 500, false);
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x0006234A File Offset: 0x0006054A
		internal void WriteMessage(byte[] buf, int off, int len)
		{
			this.WriteInternal(buf, off, len, len, true);
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x00062357 File Offset: 0x00060557
		internal void WriteMessage(byte[] buf, int off, int len, bool flush)
		{
			this.WriteInternal(buf, off, len, len, flush);
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x00062368 File Offset: 0x00060568
		internal void WriteMessageParts(byte[] buf1, int off1, int len1, byte[] buf2, int off2, int len2, bool flush)
		{
			int num = 5 + len1 + len2;
			byte[] array = new byte[num];
			array[0] = 1;
			int num2 = 1;
			Serialization.SerializeUInt32(array, ref num2, (uint)(len1 + len2));
			Array.Copy(buf1, off1, array, 5, len1);
			Array.Copy(buf2, off2, array, 5 + len1, len2);
			this.m_tcpChannel.Write(array, 0, num, flush);
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x000623C0 File Offset: 0x000605C0
		private void CheckBlockLen(int len)
		{
			if (len > 1052672 || len < 0)
			{
				this.m_netChannel.TraceError("Invalid block len:{0} max:{1}", new object[]
				{
					len,
					1052672
				});
				throw new NetworkCorruptDataException(this.PartnerNodeName);
			}
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x00062414 File Offset: 0x00060614
		internal void WriteException(Exception ex)
		{
			byte[] array = Serialization.ObjectToBytes(ex);
			if (array.Length > 1052672)
			{
				throw new NetworkDataOverflowGenericException();
			}
			this.WriteLeadingBytes(NetworkPackagingLayer.PackageEncoding.SerializedException, array.Length, array, 0, array.Length, true);
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x00062448 File Offset: 0x00060648
		internal void Flush()
		{
			this.m_tcpChannel.Flush();
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x00062458 File Offset: 0x00060658
		protected void WriteXpress(byte[] buf, int off, int len)
		{
			int i = len;
			while (i > 0)
			{
				int num = Math.Min(i, 65536);
				this.WriteXpressBlock(buf, off, num);
				i -= num;
				off += num;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x0006248C File Offset: 0x0006068C
		protected byte[] CompressBuf
		{
			get
			{
				if (this.m_compressBuf == null)
				{
					if (this.Encoding == NetworkChannel.DataEncodingScheme.CompressedXpress)
					{
						this.m_compressBuf = new byte[65541];
					}
					else if (this.Encoding == NetworkChannel.DataEncodingScheme.Coconet)
					{
						this.m_compressBuf = new byte[1048586];
					}
				}
				return this.m_compressBuf;
			}
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x000624DC File Offset: 0x000606DC
		protected void WriteXpressBlock(byte[] buf, int offset, int length)
		{
			int num;
			Xpress.Compress(buf, offset, length, this.CompressBuf, 5, 65536, out num);
			this.CompressBuf[0] = 2;
			int num2 = 1;
			ushort val = (ushort)(num - 1);
			Serialization.SerializeUInt16(this.CompressBuf, ref num2, val);
			val = (ushort)(length - 1);
			Serialization.SerializeUInt16(this.CompressBuf, ref num2, val);
			this.m_tcpChannel.Write(this.CompressBuf, 0, 5 + num);
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x00062548 File Offset: 0x00060748
		private void WriteCoconet(byte[] buf, int off, int len)
		{
			if (this.m_coconetCompressor == null)
			{
				this.m_coconetCompressor = new CoconetCompressor(this.CoconetConfig.DictionarySize, this.CoconetConfig.SampleRate, (CoconetCompressor.LzOption)this.CoconetConfig.LzOption);
			}
			int i = len;
			while (i > 0)
			{
				int num = Math.Min(i, 1048576);
				this.WriteCoconetBlock(buf, off, num);
				i -= num;
				off += num;
			}
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x000625B0 File Offset: 0x000607B0
		private bool Memcmp(byte[] b1, int off1, byte[] b2, int off2, int len)
		{
			for (int i = 0; i < len; i++)
			{
				if (b1[i + off1] != b2[i + off2])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x000625DC File Offset: 0x000607DC
		protected void WriteCoconetBlock(byte[] buf, int offset, int length)
		{
			byte[] compressBuf = this.CompressBuf;
			int num;
			this.m_coconetCompressor.Compress(buf, offset, length, compressBuf, 9, 1048577, out num);
			compressBuf[0] = 4;
			ExBitConverter.Write(num, compressBuf, 1);
			ExBitConverter.Write(length, compressBuf, 5);
			this.m_tcpChannel.Write(compressBuf, 0, 9 + num);
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x0006262F File Offset: 0x0006082F
		internal void ThrowEndOfData(int bytesExpected, int bytesRead)
		{
			ExTraceGlobals.NetworkChannelTracer.TraceError<int, int>((long)this.GetHashCode(), "End of data: Expected:{0} Read:{1}", bytesExpected, bytesRead);
			throw new NetworkEndOfDataException(this.PartnerNodeName, ReplayStrings.NetworkReadEOF);
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x00062660 File Offset: 0x00060860
		private void ReadChunk(byte[] buf, int off, int bytesToRead)
		{
			int num = this.m_tcpChannel.TryReadChunk(buf, off, bytesToRead);
			if (num != bytesToRead)
			{
				this.ThrowEndOfData(bytesToRead, num);
			}
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x00062688 File Offset: 0x00060888
		private bool WaitForAsyncRead(int timeoutInMs)
		{
			bool flag = false;
			lock (this)
			{
				if (this.m_asyncReadCompleteEvent != null && !this.m_asyncReadCompleteEvent.WaitOne(0, false))
				{
					flag = true;
				}
			}
			if (flag)
			{
				ExTraceGlobals.NetworkChannelTracer.TraceDebug<int, int>((long)this.GetHashCode(), "WaitForAsyncRead: waiting a max timeout={0}ms on thread {1}", timeoutInMs, DiagCore.GetThreadId());
				if (!this.m_asyncReadCompleteEvent.WaitOne(timeoutInMs, false))
				{
					ExTraceGlobals.NetworkChannelTracer.TraceError<int>((long)this.GetHashCode(), "WaitForAsyncRead: timed out on thread {0}", DiagCore.GetThreadId());
					return false;
				}
			}
			return true;
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x00062724 File Offset: 0x00060924
		internal void Read(byte[] buf, int off, int len)
		{
			if (!this.WaitForAsyncRead(this.m_tcpChannel.ReadTimeoutInMs))
			{
				this.m_tcpChannel.ThrowTimeoutException(ReplayStrings.NetworkReadTimeout(this.m_tcpChannel.ReadTimeoutInMs / 1000));
			}
			if (this.m_asyncReadByteCount <= 0)
			{
				if (this.m_asyncException != null)
				{
					Exception asyncException = this.m_asyncException;
					this.m_asyncException = null;
					throw asyncException;
				}
				if (this.m_asyncReadGotZeroBytes)
				{
					this.m_asyncReadGotZeroBytes = false;
					throw new NetworkEndOfDataException(this.PartnerNodeName, ReplayStrings.NetworkReadEOF);
				}
			}
			int num;
			for (int i = len; i > 0; i -= num)
			{
				num = this.ReadData(buf, off, i);
				if (num == 0)
				{
					this.ThrowEndOfData(i, num);
				}
				off += num;
			}
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x000627D8 File Offset: 0x000609D8
		internal void StartRead(NetworkChannelCallback asyncCallback, object asyncState)
		{
			bool flag = false;
			lock (this)
			{
				if (this.m_closeCalled)
				{
					throw new NetworkCancelledException();
				}
				if (this.m_asyncReadCompleteEvent == null)
				{
					this.m_asyncReadCompleteEvent = new ManualResetEvent(false);
				}
				else
				{
					this.m_asyncReadCompleteEvent.Reset();
				}
				this.m_asyncReadActive = true;
				this.m_asyncReadGotZeroBytes = false;
				this.m_asyncReadByteCount = 0;
				this.m_asyncException = null;
				this.m_userCallback = asyncCallback;
				this.m_userCallbackCtx = asyncState;
				try
				{
					IAsyncResult asyncResult = this.m_tcpChannel.Stream.BeginRead(this.m_readPacketHeaderBuf, 0, 1, new AsyncCallback(NetworkPackagingLayer.ReadCallback), this);
					NetworkPackagingLayer.Tracer.TraceDebug<bool, int>((long)this.GetHashCode(), "BeginRead completion was sync: {0} arhash={1}", asyncResult.CompletedSynchronously, asyncResult.GetHashCode());
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						this.m_asyncReadActive = false;
						if (this.m_asyncReadCompleteEvent != null)
						{
							this.m_asyncReadCompleteEvent.Set();
						}
					}
				}
			}
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x000628E0 File Offset: 0x00060AE0
		internal bool HasAsyncDataToRead()
		{
			return this.m_asyncReadByteCount > 0;
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x000628F0 File Offset: 0x00060AF0
		private int ReadData(byte[] buf, int off, int len)
		{
			if (this.m_bytesRemainingToReadInCurrentBlock > 0)
			{
				int num = Math.Min(len, this.m_bytesRemainingToReadInCurrentBlock);
				if (this.m_curBlockType == NetworkPackagingLayer.PackageEncoding.Raw)
				{
					int num2 = this.m_tcpChannel.TryReadChunk(buf, off, num);
					this.m_bytesRemainingToReadInCurrentBlock -= num2;
					return num2;
				}
				int sourceIndex = this.m_decompressBufContentLen - this.m_bytesRemainingToReadInCurrentBlock;
				Array.Copy(this.m_decompressBuf, sourceIndex, buf, off, num);
				this.m_bytesRemainingToReadInCurrentBlock -= num;
				return num;
			}
			else
			{
				if (this.m_asyncReadByteCount > 0)
				{
					this.m_asyncReadByteCount = 0;
				}
				else
				{
					this.ReadChunk(this.m_readPacketHeaderBuf, 0, 1);
				}
				this.m_curBlockType = (NetworkPackagingLayer.PackageEncoding)this.m_readPacketHeaderBuf[0];
				if (this.m_curBlockType == NetworkPackagingLayer.PackageEncoding.SerializedException)
				{
					NetworkPackagingLayer.Tracer.TraceDebug((long)this.GetHashCode(), "NetPkg.ReadData found incoming exception");
					this.ReadChunk(this.m_readPacketHeaderBuf, 0, 4);
					int num3 = BitConverter.ToInt32(this.m_readPacketHeaderBuf, 0);
					this.CheckBlockLen(num3);
					byte[] array = new byte[num3];
					this.ReadChunk(array, 0, num3);
					Exception ex;
					try
					{
						ex = (Exception)Serialization.BytesToObject(array);
					}
					catch (SerializationException ex2)
					{
						this.m_netChannel.TraceError("Exception deserialization failed:{0}", new object[]
						{
							ex2
						});
						ex = new NetworkRemoteExceptionUnknown(this.m_netChannel.PartnerNodeName, ex2.Message, ex2);
					}
					catch (TargetInvocationException ex3)
					{
						this.m_netChannel.TraceError("Exception deserialization failed:{0}", new object[]
						{
							ex3
						});
						if (ex3.InnerException == null || !(ex3.InnerException is SerializationException))
						{
							throw;
						}
						ex = new NetworkRemoteExceptionUnknown(this.m_netChannel.PartnerNodeName, ex3.Message, ex3);
					}
					throw new NetworkRemoteException(this.m_netChannel.PartnerNodeName, ex.Message, ex);
				}
				if (this.m_curBlockType == NetworkPackagingLayer.PackageEncoding.Raw)
				{
					this.ReadChunk(this.m_readPacketHeaderBuf, 0, 4);
					this.m_bytesRemainingToReadInCurrentBlock = BitConverter.ToInt32(this.m_readPacketHeaderBuf, 0);
					int num = Math.Min(len, this.m_bytesRemainingToReadInCurrentBlock);
					int num2 = this.m_tcpChannel.TryReadChunk(buf, off, num);
					this.m_bytesRemainingToReadInCurrentBlock -= num2;
					this.TotalDecompressedBytesReceived += (long)num2;
					this.TotalCompressedBytesReceived += (long)num2;
					return num2;
				}
				if (this.m_curBlockType == NetworkPackagingLayer.PackageEncoding.Xpress)
				{
					return this.ReadXpressBlock(buf, off, len);
				}
				if (this.m_curBlockType == NetworkPackagingLayer.PackageEncoding.Coconet)
				{
					return this.ReadCoconetBlock(buf, off, len);
				}
				this.m_netChannel.TraceError("Unknown packet encoding: {0}", new object[]
				{
					this.m_curBlockType
				});
				throw new NetworkCorruptDataException(this.PartnerNodeName);
			}
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x00062B9C File Offset: 0x00060D9C
		private int ReadXpressBlock(byte[] buf, int off, int len)
		{
			if (this.m_xpressReadBuf == null)
			{
				this.m_xpressReadBuf = new byte[65536];
			}
			this.ReadChunk(this.m_readPacketHeaderBuf, 0, 4);
			ushort num = BitConverter.ToUInt16(this.m_readPacketHeaderBuf, 0);
			int num2 = (int)(num + 1);
			num = BitConverter.ToUInt16(this.m_readPacketHeaderBuf, 2);
			int num3 = (int)(num + 1);
			this.ReadChunk(this.m_xpressReadBuf, 0, num2);
			bool flag;
			if (num3 <= len)
			{
				flag = Xpress.Decompress(this.m_xpressReadBuf, 0, num2, buf, off, num3);
			}
			else
			{
				if (this.m_decompressBuf == null)
				{
					this.m_decompressBuf = new byte[65536];
				}
				flag = Xpress.Decompress(this.m_xpressReadBuf, 0, num2, this.m_decompressBuf, 0, num3);
			}
			if (!flag)
			{
				NetworkPackagingLayer.Tracer.TraceError((long)this.GetHashCode(), "Decompression failed");
				throw new NetworkCorruptDataException(this.PartnerNodeName);
			}
			ExchangeNetworkPerfmonCounters perfCounters = this.m_netChannel.PerfCounters;
			if (perfCounters != null && this.m_netChannel.NetworkPath != null)
			{
				perfCounters.RecordCompressedDataReceived(num2, num3, this.m_netChannel.NetworkPath.Purpose);
			}
			this.TotalDecompressedBytesReceived += (long)num3;
			this.TotalCompressedBytesReceived += (long)num2;
			if (num3 <= len)
			{
				this.m_bytesRemainingToReadInCurrentBlock = 0;
				return num3;
			}
			this.m_decompressBufContentLen = num3;
			this.m_bytesRemainingToReadInCurrentBlock = num3 - len;
			Array.Copy(this.m_decompressBuf, 0, buf, off, len);
			NetworkPackagingLayer.Tracer.TraceError<int, int>((long)this.GetHashCode(), "NetPkg.ReadXpressBlock: Had to buffer 0x{0:X} uncompressed bytes for the next fetch since only 0x{1:X} were requested.", this.m_bytesRemainingToReadInCurrentBlock, len);
			return len;
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x00062D0C File Offset: 0x00060F0C
		private int ReadCoconetBlock(byte[] buf, int off, int len)
		{
			if (this.m_coconetDecompressor == null)
			{
				this.m_coconetDecompressor = new CoconetDecompressor(this.CoconetConfig.DictionarySize, (CoconetCompressor.LzOption)this.CoconetConfig.LzOption);
			}
			if (this.m_xpressReadBuf == null || this.m_xpressReadBuf.Length < 1048577)
			{
				this.m_xpressReadBuf = new byte[1048577];
			}
			this.ReadChunk(this.m_readPacketHeaderBuf, 0, 8);
			int num = BitConverter.ToInt32(this.m_readPacketHeaderBuf, 0);
			int num2 = BitConverter.ToInt32(this.m_readPacketHeaderBuf, 4);
			if (num2 > 1048577 || num2 <= 0)
			{
				throw new NetworkCorruptDataException(this.PartnerNodeName);
			}
			if (num > 1048577 || num <= 0)
			{
				throw new NetworkCorruptDataException(this.PartnerNodeName);
			}
			this.ReadChunk(this.m_xpressReadBuf, 0, num);
			ExTraceGlobals.NetShareTracer.TraceDebug<int, int>((long)this.GetHashCode(), "ReadCoconetBlock l={0} c={1}", num2, num);
			if (num2 <= len)
			{
				this.m_coconetDecompressor.Decompress(this.m_xpressReadBuf, 0, num, buf, off, num2);
			}
			else
			{
				if (this.m_decompressBuf == null || this.m_decompressBuf.Length < 1048577)
				{
					this.m_decompressBuf = new byte[1048577];
				}
				this.m_coconetDecompressor.Decompress(this.m_xpressReadBuf, 0, num, this.m_decompressBuf, 0, num2);
			}
			ExchangeNetworkPerfmonCounters perfCounters = this.m_netChannel.PerfCounters;
			if (perfCounters != null && this.m_netChannel.NetworkPath != null)
			{
				perfCounters.RecordCompressedDataReceived(num, num2, this.m_netChannel.NetworkPath.Purpose);
			}
			this.TotalDecompressedBytesReceived += (long)num2;
			this.TotalCompressedBytesReceived += (long)num;
			if (num2 <= len)
			{
				this.m_bytesRemainingToReadInCurrentBlock = 0;
				return num2;
			}
			this.m_decompressBufContentLen = num2;
			this.m_bytesRemainingToReadInCurrentBlock = num2 - len;
			Array.Copy(this.m_decompressBuf, 0, buf, off, len);
			NetworkPackagingLayer.Tracer.TraceError<int, int>((long)this.GetHashCode(), "NetPkg.ReadCoconetBlock: Had to buffer 0x{0:X} uncompressed bytes for the next fetch since only 0x{1:X} were requested.", this.m_bytesRemainingToReadInCurrentBlock, len);
			return len;
		}

		// Token: 0x0400094D RID: 2381
		public const int RawHeaderSize = 5;

		// Token: 0x0400094E RID: 2382
		private const int XpressHeaderSize = 5;

		// Token: 0x0400094F RID: 2383
		private const int CoconetHeaderSize = 9;

		// Token: 0x04000950 RID: 2384
		private const int CoconetMaxBlockSize = 1048577;

		// Token: 0x04000951 RID: 2385
		private const int MinInitialPackageLength = 500;

		// Token: 0x04000952 RID: 2386
		private const int minCocoSize = 265;

		// Token: 0x04000953 RID: 2387
		private const int MaxBlockLen = 1052672;

		// Token: 0x04000954 RID: 2388
		protected TcpChannel m_tcpChannel;

		// Token: 0x04000955 RID: 2389
		protected NetworkChannel m_netChannel;

		// Token: 0x04000956 RID: 2390
		protected NetworkChannel.DataEncodingScheme m_encoding;

		// Token: 0x04000957 RID: 2391
		private byte[] m_readPacketHeaderBuf;

		// Token: 0x04000958 RID: 2392
		private bool m_closeCalled;

		// Token: 0x04000959 RID: 2393
		protected byte[] m_compressBuf;

		// Token: 0x0400095A RID: 2394
		private CoconetCompressor m_coconetCompressor;

		// Token: 0x0400095B RID: 2395
		private CoconetDecompressor m_coconetDecompressor;

		// Token: 0x0400095C RID: 2396
		private NetworkPackagingLayer.PackageEncoding m_curBlockType;

		// Token: 0x0400095D RID: 2397
		private int m_bytesRemainingToReadInCurrentBlock;

		// Token: 0x0400095E RID: 2398
		private ManualResetEvent m_asyncReadCompleteEvent;

		// Token: 0x0400095F RID: 2399
		private bool m_asyncReadActive;

		// Token: 0x04000960 RID: 2400
		private NetworkChannelCallback m_userCallback;

		// Token: 0x04000961 RID: 2401
		private object m_userCallbackCtx;

		// Token: 0x04000962 RID: 2402
		private Exception m_asyncException;

		// Token: 0x04000963 RID: 2403
		private int m_asyncReadByteCount;

		// Token: 0x04000964 RID: 2404
		private bool m_asyncReadGotZeroBytes;

		// Token: 0x04000965 RID: 2405
		private byte[] m_decompressBuf;

		// Token: 0x04000966 RID: 2406
		private int m_decompressBufContentLen;

		// Token: 0x04000967 RID: 2407
		private byte[] m_xpressReadBuf;

		// Token: 0x02000261 RID: 609
		public enum PackageEncoding
		{
			// Token: 0x0400096C RID: 2412
			Invalid,
			// Token: 0x0400096D RID: 2413
			Raw,
			// Token: 0x0400096E RID: 2414
			Xpress,
			// Token: 0x0400096F RID: 2415
			SerializedException,
			// Token: 0x04000970 RID: 2416
			Coconet
		}
	}
}
