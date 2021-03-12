using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.Net.ExSmtpClient
{
	// Token: 0x02000709 RID: 1801
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SmtpSslStream : Stream, IDisposeTrackable, IDisposable
	{
		// Token: 0x060021CE RID: 8654 RVA: 0x000441E6 File Offset: 0x000423E6
		internal SmtpSslStream(Stream innerStream, ISmtpClientDebugOutput smtpClientDebugOutput)
		{
			this.innerStream = innerStream;
			this.disposeTracker = this.GetDisposeTracker();
			this.smtpClientDebugOutput = smtpClientDebugOutput;
			this.smtpSsl = new SmtpSslStream.SmtpSslHelper(smtpClientDebugOutput);
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x060021CF RID: 8655 RVA: 0x00044214 File Offset: 0x00042414
		public override bool CanRead
		{
			get
			{
				return this.innerStream.CanRead;
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x060021D0 RID: 8656 RVA: 0x00044221 File Offset: 0x00042421
		public override bool CanSeek
		{
			get
			{
				return this.innerStream.CanSeek;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x060021D1 RID: 8657 RVA: 0x0004422E File Offset: 0x0004242E
		public override bool CanWrite
		{
			get
			{
				return this.innerStream.CanWrite;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x060021D2 RID: 8658 RVA: 0x0004423B File Offset: 0x0004243B
		public override long Length
		{
			get
			{
				return this.innerStream.Length;
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x060021D3 RID: 8659 RVA: 0x00044248 File Offset: 0x00042448
		// (set) Token: 0x060021D4 RID: 8660 RVA: 0x00044255 File Offset: 0x00042455
		public override long Position
		{
			get
			{
				return this.innerStream.Position;
			}
			set
			{
				this.innerStream.Position = value;
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x060021D5 RID: 8661 RVA: 0x00044263 File Offset: 0x00042463
		internal byte[] SessionKey
		{
			get
			{
				return this.smtpSsl.GetSessionKey();
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x060021D6 RID: 8662 RVA: 0x00044270 File Offset: 0x00042470
		internal byte[] CertificatePublicKey
		{
			get
			{
				return this.smtpSsl.GetCertificatePublicKey();
			}
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x0004427D File Offset: 0x0004247D
		public override void Close()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
			this.smtpSsl.Dispose();
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x000442A4 File Offset: 0x000424A4
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SmtpSslStream>(this);
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x000442AC File Offset: 0x000424AC
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x000442C4 File Offset: 0x000424C4
		public override void Write(byte[] buffer, int offset, int size)
		{
			byte[] array = new byte[size];
			Array.Copy(buffer, offset, array, 0, array.Length);
			byte[] array2 = this.smtpSsl.Encrypt(array);
			this.innerStream.Write(array2, 0, array2.Length);
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x00044304 File Offset: 0x00042504
		public override int Read(byte[] buffer, int offset, int size)
		{
			if (this.decryptedBytes == null)
			{
				bool flag = true;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					byte[] array = new byte[SmtpConstants.BufferSize];
					int num;
					do
					{
						num = this.innerStream.Read(array, 0, array.Length);
						this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), "Read {0} off the encrypted network stream", new object[]
						{
							num
						});
						memoryStream.Write(array, 0, num);
						this.decryptedBytes = this.smtpSsl.Decrypt(memoryStream.GetBuffer(), (int)memoryStream.Length, out flag);
					}
					while (flag && num > 0);
				}
			}
			int num2 = Math.Min(size, this.decryptedBytes.Length - this.decryptedBytesIndex);
			Array.Copy(this.decryptedBytes, this.decryptedBytesIndex, buffer, offset, num2);
			this.decryptedBytesIndex += num2;
			if (this.decryptedBytesIndex == this.decryptedBytes.Length)
			{
				this.decryptedBytes = null;
				this.decryptedBytesIndex = 0;
			}
			return num2;
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x00044420 File Offset: 0x00042620
		public override void Flush()
		{
			this.innerStream.Flush();
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x0004442D File Offset: 0x0004262D
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.innerStream.Seek(offset, origin);
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x0004443C File Offset: 0x0004263C
		public override void SetLength(long value)
		{
			this.innerStream.SetLength(value);
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x0004444C File Offset: 0x0004264C
		internal void Handshake()
		{
			byte[] array = new byte[SmtpConstants.BufferSize];
			byte[] array2 = this.smtpSsl.GetInitialBlob(null, SmtpSspiMechanism.TLS);
			for (int i = 0; i < 100; i++)
			{
				this.innerStream.Write(array2, 0, array2.Length);
				bool flag = true;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					while (flag)
					{
						int count = this.innerStream.Read(array, 0, array.Length);
						memoryStream.Write(array, 0, count);
						array2 = this.smtpSsl.GetNextBlob(memoryStream.GetBuffer(), (int)memoryStream.Length, out flag);
					}
				}
				if (array2.Length == 0)
				{
					return;
				}
			}
		}

		// Token: 0x04002097 RID: 8343
		private Stream innerStream;

		// Token: 0x04002098 RID: 8344
		private SmtpSslStream.SmtpSslHelper smtpSsl;

		// Token: 0x04002099 RID: 8345
		private byte[] decryptedBytes;

		// Token: 0x0400209A RID: 8346
		private int decryptedBytesIndex;

		// Token: 0x0400209B RID: 8347
		private DisposeTracker disposeTracker;

		// Token: 0x0400209C RID: 8348
		private ISmtpClientDebugOutput smtpClientDebugOutput;

		// Token: 0x0200070A RID: 1802
		internal class SmtpSslHelper : DisposeTrackableBase
		{
			// Token: 0x060021E0 RID: 8672 RVA: 0x000444FC File Offset: 0x000426FC
			internal SmtpSslHelper(ISmtpClientDebugOutput smtpClientDebugOutput)
			{
				this.smtpClientDebugOutput = smtpClientDebugOutput;
			}

			// Token: 0x060021E1 RID: 8673 RVA: 0x0004452F File Offset: 0x0004272F
			internal static IntPtr Add(IntPtr a, int b)
			{
				return (IntPtr)((long)a + (long)b);
			}

			// Token: 0x060021E2 RID: 8674 RVA: 0x00044540 File Offset: 0x00042740
			internal byte[] GetInitialBlob(NetworkCredential creds, SmtpSspiMechanism sspiMechanism)
			{
				base.CheckDisposed();
				IntPtr intPtr = (IntPtr)0;
				IntPtr intPtr2 = (IntPtr)0;
				byte[] result = new byte[0];
				try
				{
					this.safeCredHandle = this.GetClientCredentials();
					this.safeContextHandle = default(NativeMethods.CredHandle);
					this.streamSizes = default(NativeMethods.SecurityPackageContextStreamSizes);
					NativeMethods.SecBuffer secBuffer = default(NativeMethods.SecBuffer);
					secBuffer.cbBuffer = 65536;
					secBuffer.BufferType = 2;
					intPtr2 = Marshal.AllocHGlobal(secBuffer.cbBuffer);
					secBuffer.buffer = intPtr2;
					NativeMethods.SecBufferDesc secBufferDesc = new NativeMethods.SecBufferDesc();
					secBufferDesc.cBuffers = 1;
					secBufferDesc.ulVersion = 0;
					intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(secBuffer));
					secBufferDesc.pBuffer = intPtr;
					Marshal.StructureToPtr(secBuffer, secBufferDesc.pBuffer, false);
					long num = 0L;
					int num2 = 0;
					int fContextReq = 573596;
					NativeMethods.SEC_STATUS secStatus = NativeMethods.InitializeSecurityContext(ref this.safeCredHandle, IntPtr.Zero, "MSExchangeTransport", fContextReq, 0, NativeMethods.Endianness.Native, IntPtr.Zero, 0, ref this.safeContextHandle, secBufferDesc, ref num2, out num);
					if (590610 != secStatus)
					{
						this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), "InitializeSecurityContext failed with " + secStatus.ToString(), new object[0]);
						throw new TlsApiFailureException(secStatus.ToString());
					}
					secBuffer = (NativeMethods.SecBuffer)Marshal.PtrToStructure(secBufferDesc.pBuffer, typeof(NativeMethods.SecBuffer));
					byte[] array = new byte[secBuffer.cbBuffer];
					Marshal.Copy(secBuffer.buffer, array, 0, secBuffer.cbBuffer);
					result = array;
				}
				finally
				{
					if (intPtr != (IntPtr)0)
					{
						Marshal.FreeHGlobal(intPtr);
					}
					if (intPtr2 != (IntPtr)0)
					{
						Marshal.FreeHGlobal(intPtr2);
					}
				}
				return result;
			}

			// Token: 0x060021E3 RID: 8675 RVA: 0x00044728 File Offset: 0x00042928
			internal NativeMethods.CredHandle GetClientCredentials()
			{
				base.CheckDisposed();
				NativeMethods.CredHandle result = default(NativeMethods.CredHandle);
				NativeMethods.AuthDataForSchannel authDataForSchannel = NativeMethods.AuthDataForSchannel.GetAuthDataForSchannel();
				long num = 0L;
				NativeMethods.SEC_STATUS secStatus = NativeMethods.AcquireCredentialsHandleForSchannel(null, "Microsoft Unified Security Protocol Provider", 2, IntPtr.Zero, ref authDataForSchannel, IntPtr.Zero, IntPtr.Zero, ref result, out num);
				if (secStatus != 0)
				{
					this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), "AcquireCredentialsHandleForSchannel failed " + secStatus.ToString(), new object[0]);
					throw new TlsApiFailureException(secStatus.ToString());
				}
				return result;
			}

			// Token: 0x060021E4 RID: 8676 RVA: 0x000447C8 File Offset: 0x000429C8
			internal byte[] GetNextBlob(byte[] serverBlob, int bytesReceived, out bool incomplete)
			{
				base.CheckDisposed();
				incomplete = false;
				byte[] result = new byte[0];
				IntPtr intPtr = (IntPtr)0;
				IntPtr intPtr2 = (IntPtr)0;
				IntPtr intPtr3 = (IntPtr)0;
				IntPtr intPtr4 = (IntPtr)0;
				try
				{
					NativeMethods.SecBuffer secBuffer = default(NativeMethods.SecBuffer);
					secBuffer.cbBuffer = bytesReceived;
					secBuffer.BufferType = 2;
					intPtr2 = Marshal.AllocHGlobal(bytesReceived);
					secBuffer.buffer = intPtr2;
					Marshal.Copy(serverBlob, 0, secBuffer.buffer, bytesReceived);
					NativeMethods.SecBufferDesc secBufferDesc = new NativeMethods.SecBufferDesc();
					secBufferDesc.ulVersion = 0;
					secBufferDesc.cBuffers = 1;
					intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(secBuffer));
					secBufferDesc.pBuffer = intPtr;
					Marshal.StructureToPtr(secBuffer, secBufferDesc.pBuffer, false);
					NativeMethods.SecBuffer secBuffer2 = default(NativeMethods.SecBuffer);
					secBuffer2.cbBuffer = 65536;
					secBuffer2.BufferType = 2;
					intPtr3 = Marshal.AllocHGlobal(65536);
					secBuffer2.buffer = intPtr3;
					NativeMethods.SecBufferDesc secBufferDesc2 = new NativeMethods.SecBufferDesc();
					secBufferDesc2.ulVersion = 0;
					secBufferDesc2.cBuffers = 1;
					intPtr4 = Marshal.AllocHGlobal(Marshal.SizeOf(secBuffer2));
					Marshal.StructureToPtr(secBuffer2, intPtr4, false);
					secBufferDesc2.pBuffer = intPtr4;
					long num = 0L;
					int num2 = 0;
					NativeMethods.SEC_STATUS secStatus = NativeMethods.InitializeSecurityContext_NextSslBlob(ref this.safeCredHandle, ref this.safeContextHandle, "MSExchangeTransport", 0, 0, NativeMethods.Endianness.Network, secBufferDesc, 0, ref this.safeContextHandle, secBufferDesc2, ref num2, out num);
					if (secStatus == "0x80090318")
					{
						incomplete = true;
					}
					else if (secStatus == 590610)
					{
						secBuffer2 = (NativeMethods.SecBuffer)Marshal.PtrToStructure(intPtr4, typeof(NativeMethods.SecBuffer));
						byte[] array = new byte[secBuffer2.cbBuffer];
						Marshal.Copy(intPtr3, array, 0, secBuffer2.cbBuffer);
						result = array;
					}
					else
					{
						if (secStatus != 0)
						{
							this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), "InitializeSecurityContext failed " + secStatus.ToString(), new object[0]);
							throw new TlsApiFailureException(secStatus.ToString());
						}
						byte[] array2 = new byte[NativeMethods.SecurityPackageContextStreamSizes.SizeOf];
						secStatus = NativeMethods.QueryContextAttributes(ref this.safeContextHandle, NativeMethods.ContextAttribute.StreamSizes, array2);
						if (secStatus != 0)
						{
							this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), "QueryContextAttributes_StreamSizes failed " + secStatus.ToString(), new object[0]);
							throw new TlsApiFailureException(secStatus.ToString());
						}
						this.streamSizes = new NativeMethods.SecurityPackageContextStreamSizes(array2);
						string message = string.Concat(new string[]
						{
							"SSL context attributes - Block size: ",
							this.streamSizes.cbBlockSize.ToString(CultureInfo.InvariantCulture),
							", Cb header: ",
							this.streamSizes.cbHeader.ToString(CultureInfo.InvariantCulture),
							", Max msg: ",
							this.streamSizes.cbMaximumMessage.ToString(CultureInfo.InvariantCulture),
							", Trailer: ",
							this.streamSizes.cbTrailer.ToString(CultureInfo.InvariantCulture),
							", Cb buffers: ",
							this.streamSizes.cBuffers.ToString(CultureInfo.InvariantCulture)
						});
						this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), message, new object[0]);
					}
				}
				finally
				{
					if (intPtr != (IntPtr)0)
					{
						Marshal.FreeHGlobal(intPtr);
					}
					if (intPtr2 != (IntPtr)0)
					{
						Marshal.FreeHGlobal(intPtr2);
					}
					if (intPtr3 != (IntPtr)0)
					{
						Marshal.FreeHGlobal(intPtr3);
					}
					if (intPtr4 != (IntPtr)0)
					{
						Marshal.FreeHGlobal(intPtr4);
					}
				}
				return result;
			}

			// Token: 0x060021E5 RID: 8677 RVA: 0x00044BB8 File Offset: 0x00042DB8
			internal byte[] Decrypt(byte[] serverBlob, int serverBlobLength, out bool incomplete)
			{
				base.CheckDisposed();
				byte[] result;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					int num = serverBlobLength;
					byte[] array = serverBlob;
					SmtpSslStream.SmtpSslHelper.SslFragment sslFragment;
					for (;;)
					{
						sslFragment = this.CrackSsl(array, serverBlobLength);
						sslFragment.DecodedFragment.WriteTo(memoryStream);
						if (sslFragment.LastFragment || sslFragment.Incomplete)
						{
							break;
						}
						array = new byte[sslFragment.BytesRemaining];
						Array.Copy(serverBlob, num - sslFragment.BytesRemaining, array, 0, sslFragment.BytesRemaining);
						serverBlobLength = sslFragment.BytesRemaining;
					}
					incomplete = sslFragment.Incomplete;
					result = memoryStream.ToArray();
				}
				return result;
			}

			// Token: 0x060021E6 RID: 8678 RVA: 0x00044C58 File Offset: 0x00042E58
			internal byte[] Encrypt(byte[] bytesToEncrypt)
			{
				base.CheckDisposed();
				byte[] result;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					int num;
					for (int i = 0; i < bytesToEncrypt.Length; i += num)
					{
						num = Math.Min(this.streamSizes.cbMaximumMessage, bytesToEncrypt.Length - i);
						byte[] array = this.Encrypt(bytesToEncrypt, i, num);
						memoryStream.Write(array, 0, array.Length);
					}
					result = memoryStream.ToArray();
				}
				return result;
			}

			// Token: 0x060021E7 RID: 8679 RVA: 0x00044CD0 File Offset: 0x00042ED0
			internal byte[] GetSessionKey()
			{
				base.CheckDisposed();
				NativeMethods.EapKeyBlock eapKeyBlock = default(NativeMethods.EapKeyBlock);
				NativeMethods.SEC_STATUS secStatus = NativeMethods.QueryContextAttributes(ref this.safeContextHandle, NativeMethods.ContextAttribute.EapKeyBlock, ref eapKeyBlock);
				if (secStatus == 0)
				{
					return eapKeyBlock.rgbKeys;
				}
				throw new TlsApiFailureException(secStatus.ToString());
			}

			// Token: 0x060021E8 RID: 8680 RVA: 0x00044D24 File Offset: 0x00042F24
			internal byte[] GetCertificatePublicKey()
			{
				base.CheckDisposed();
				IntPtr zero = IntPtr.Zero;
				SafeCertContextHandle safeCertContextHandle;
				NativeMethods.SEC_STATUS secStatus = NativeMethods.QueryContextAttributes(ref this.safeContextHandle, NativeMethods.ContextAttribute.RemoteCertificate, out safeCertContextHandle);
				byte[] publicKey;
				using (safeCertContextHandle)
				{
					if (secStatus != 0)
					{
						throw new Win32Exception(secStatus, "QueryContextAttributes for SECPKG_ATTR_REMOTE_CERT_CONTEXT failed");
					}
					X509Certificate x509Certificate = new X509Certificate(safeCertContextHandle.DangerousGetHandle());
					publicKey = x509Certificate.GetPublicKey();
				}
				return publicKey;
			}

			// Token: 0x060021E9 RID: 8681 RVA: 0x00044DA0 File Offset: 0x00042FA0
			protected override void InternalDispose(bool disposing)
			{
				if (disposing)
				{
					this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), "SmtpSsl::Dispose()", new object[0]);
					if (!this.safeContextHandle.IsZero)
					{
						int num = NativeMethods.DeleteSecurityContext(ref this.safeContextHandle);
						this.safeContextHandle.SetToInvalid();
						this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), "SmtpSsl::Dispose() DeleteSecurityContext returned {0:x}", new object[]
						{
							num
						});
					}
					if (!this.safeCredHandle.IsZero)
					{
						int num = NativeMethods.FreeCredentialsHandle(ref this.safeCredHandle);
						this.safeCredHandle.SetToInvalid();
						this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), "SmtpSsl::Dispose() FreeCredentialsHandle returned {0:x}", new object[]
						{
							num
						});
					}
				}
			}

			// Token: 0x060021EA RID: 8682 RVA: 0x00044E85 File Offset: 0x00043085
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<SmtpSslStream.SmtpSslHelper>(this);
			}

			// Token: 0x060021EB RID: 8683 RVA: 0x00044E90 File Offset: 0x00043090
			private SmtpSslStream.SmtpSslHelper.SslFragment CrackSsl(byte[] serverBlob, int serverBlobLength)
			{
				IntPtr intPtr = (IntPtr)0;
				IntPtr intPtr2 = (IntPtr)0;
				string text = string.Empty;
				MemoryStream memoryStream = new MemoryStream();
				bool flag = true;
				int bytesRemaining = 0;
				GCHandle gchandle = default(GCHandle);
				SmtpSslStream.SmtpSslHelper.SslFragment result;
				try
				{
					intPtr = Marshal.AllocHGlobal(serverBlobLength);
					Marshal.Copy(serverBlob, 0, intPtr, serverBlobLength);
					NativeMethods.SecBuffer[] array = new NativeMethods.SecBuffer[4];
					array[0].buffer = intPtr;
					array[0].BufferType = 1;
					array[0].cbBuffer = serverBlobLength;
					array[1].buffer = IntPtr.Zero;
					array[1].BufferType = 0;
					array[1].cbBuffer = 0;
					array[2].buffer = IntPtr.Zero;
					array[2].BufferType = 0;
					array[2].cbBuffer = 0;
					array[3].buffer = IntPtr.Zero;
					array[3].BufferType = 0;
					array[3].cbBuffer = 0;
					NativeMethods.SecBufferDesc secBufferDesc = new NativeMethods.SecBufferDesc();
					secBufferDesc.ulVersion = 0;
					secBufferDesc.cBuffers = 4;
					gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
					secBufferDesc.pBuffer = Marshal.UnsafeAddrOfPinnedArrayElement(array, 0);
					uint num = 0U;
					NativeMethods.SEC_STATUS secStatus = NativeMethods.DecryptMessage(ref this.safeContextHandle, secBufferDesc, 0U, out num);
					if ("0x80090318" == secStatus)
					{
						this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), "DecryptMessage returned NativeMethods.SEC_E_INCOMPLETE_MESSAGE", new object[0]);
						result = new SmtpSslStream.SmtpSslHelper.SslFragment(false, true, 0, memoryStream);
					}
					else
					{
						if (secStatus != 0)
						{
							this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), "DecryptMessage failed " + secStatus.ToString(), new object[0]);
							throw new TlsApiFailureException(secStatus.ToString());
						}
						if (array[1].BufferType != 1)
						{
							this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), "Unexpected, decryptBuffers.DataBuffer should be the data buffer!", new object[0]);
							throw new TlsProtocolFailureException();
						}
						if (array[0].BufferType != 7)
						{
							this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), "Unexpected, decryptBuffers.HeaderBuffer should be the data buffer!", new object[0]);
							throw new TlsProtocolFailureException();
						}
						if (array[2].BufferType != 6)
						{
							this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), "Unexpected, decryptBuffers.TrailerBuffer should be the data buffer!", new object[0]);
							throw new TlsProtocolFailureException();
						}
						int bufferType = array[3].BufferType;
						if (bufferType != 0)
						{
							if (bufferType != 5)
							{
								string message = "(" + array[3].BufferType.ToString(CultureInfo.InvariantCulture) + ") is an unexpected decryptBuffers.ExtraBuffer.BufferType";
								this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), message, new object[0]);
								throw new TlsProtocolFailureException();
							}
							flag = false;
							bytesRemaining = array[3].cbBuffer;
						}
						byte[] array2 = new byte[array[1].cbBuffer];
						Marshal.Copy(array[1].buffer, array2, 0, array[1].cbBuffer);
						Encoding @default = Encoding.Default;
						text = @default.GetString(array2, 0, array2.Length);
						memoryStream.Write(array2, 0, array2.Length);
						string message2 = string.Concat(new string[]
						{
							"Decrypted (",
							serverBlobLength.ToString(CultureInfo.InvariantCulture),
							") bytes into (",
							text,
							")",
							flag ? "" : ", more text remains in this TCP packet to be decoded"
						});
						this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), message2, new object[0]);
						result = new SmtpSslStream.SmtpSslHelper.SslFragment(flag, false, bytesRemaining, memoryStream);
					}
				}
				finally
				{
					if (intPtr != (IntPtr)0)
					{
						Marshal.FreeHGlobal(intPtr);
					}
					if (intPtr2 != (IntPtr)0)
					{
						Marshal.FreeHGlobal(intPtr2);
					}
					if (gchandle.IsAllocated)
					{
						gchandle.Free();
					}
				}
				return result;
			}

			// Token: 0x060021EC RID: 8684 RVA: 0x000452E4 File Offset: 0x000434E4
			private byte[] Encrypt(byte[] bytesToEncrypt, int offset, int numberOfBytesToEncrypt)
			{
				byte[] array = null;
				NativeMethods.SecBuffer[] array2 = new NativeMethods.SecBuffer[4];
				NativeMethods.SecBufferDesc secBufferDesc = new NativeMethods.SecBufferDesc();
				int num = 0;
				IntPtr intPtr = IntPtr.Zero;
				GCHandle gchandle = default(GCHandle);
				int cb = numberOfBytesToEncrypt + this.streamSizes.cbHeader + this.streamSizes.cbTrailer;
				intPtr = Marshal.AllocHGlobal(cb);
				array2[0].BufferType = 7;
				array2[0].cbBuffer = this.streamSizes.cbHeader;
				array2[0].buffer = intPtr;
				num++;
				array2[1].BufferType = 1;
				array2[1].cbBuffer = numberOfBytesToEncrypt;
				array2[1].buffer = SmtpSslStream.SmtpSslHelper.Add(array2[0].buffer, array2[0].cbBuffer);
				num++;
				try
				{
					Marshal.Copy(bytesToEncrypt, offset, array2[1].buffer, numberOfBytesToEncrypt);
					array2[2].BufferType = 6;
					array2[2].cbBuffer = this.streamSizes.cbTrailer;
					array2[2].buffer = SmtpSslStream.SmtpSslHelper.Add(array2[1].buffer, array2[1].cbBuffer);
					num++;
					array2[3].BufferType = 0;
					array2[3].cbBuffer = 0;
					array2[3].buffer = SmtpSslStream.SmtpSslHelper.Add(array2[2].buffer, array2[2].cbBuffer);
					num++;
					secBufferDesc.ulVersion = 0;
					secBufferDesc.cBuffers = num;
					gchandle = GCHandle.Alloc(array2, GCHandleType.Pinned);
					secBufferDesc.pBuffer = Marshal.UnsafeAddrOfPinnedArrayElement(array2, 0);
					NativeMethods.SEC_STATUS secStatus = NativeMethods.EncryptMessage(ref this.safeContextHandle, 0U, secBufferDesc, 0U);
					if (secStatus != 0)
					{
						this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), "EncryptMessage failed " + secStatus.ToString(), new object[0]);
						throw new TlsApiFailureException(secStatus.ToString());
					}
					int num2 = array2[0].cbBuffer + array2[1].cbBuffer + array2[2].cbBuffer;
					array = new byte[num2];
					Marshal.Copy(intPtr, array, 0, array.Length);
				}
				finally
				{
					if (intPtr != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(intPtr);
					}
					if (gchandle.IsAllocated)
					{
						gchandle.Free();
					}
				}
				return array;
			}

			// Token: 0x0400209D RID: 8349
			private const int BufferSize = 65536;

			// Token: 0x0400209E RID: 8350
			private NativeMethods.CredHandle safeCredHandle = default(NativeMethods.CredHandle);

			// Token: 0x0400209F RID: 8351
			private NativeMethods.CredHandle safeContextHandle = default(NativeMethods.CredHandle);

			// Token: 0x040020A0 RID: 8352
			private NativeMethods.SecurityPackageContextStreamSizes streamSizes = default(NativeMethods.SecurityPackageContextStreamSizes);

			// Token: 0x040020A1 RID: 8353
			private ISmtpClientDebugOutput smtpClientDebugOutput;

			// Token: 0x0200070B RID: 1803
			private class SslFragment
			{
				// Token: 0x060021ED RID: 8685 RVA: 0x00045578 File Offset: 0x00043778
				internal SslFragment(bool lastFragment, bool incomplete, int bytesRemaining, MemoryStream decodedFragment)
				{
					this.lastFragment = lastFragment;
					this.incomplete = incomplete;
					this.bytesRemaining = bytesRemaining;
					this.decodedFragment = decodedFragment;
				}

				// Token: 0x170008C0 RID: 2240
				// (get) Token: 0x060021EE RID: 8686 RVA: 0x000455A4 File Offset: 0x000437A4
				internal bool LastFragment
				{
					get
					{
						return this.lastFragment;
					}
				}

				// Token: 0x170008C1 RID: 2241
				// (get) Token: 0x060021EF RID: 8687 RVA: 0x000455AC File Offset: 0x000437AC
				internal int BytesRemaining
				{
					get
					{
						return this.bytesRemaining;
					}
				}

				// Token: 0x170008C2 RID: 2242
				// (get) Token: 0x060021F0 RID: 8688 RVA: 0x000455B4 File Offset: 0x000437B4
				internal MemoryStream DecodedFragment
				{
					get
					{
						return this.decodedFragment;
					}
				}

				// Token: 0x170008C3 RID: 2243
				// (get) Token: 0x060021F1 RID: 8689 RVA: 0x000455BC File Offset: 0x000437BC
				internal bool Incomplete
				{
					get
					{
						return this.incomplete;
					}
				}

				// Token: 0x040020A2 RID: 8354
				private bool lastFragment = true;

				// Token: 0x040020A3 RID: 8355
				private int bytesRemaining;

				// Token: 0x040020A4 RID: 8356
				private MemoryStream decodedFragment;

				// Token: 0x040020A5 RID: 8357
				private bool incomplete;
			}
		}
	}
}
