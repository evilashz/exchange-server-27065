using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C6E RID: 3182
	internal class SspiContext : DisposeTrackableBase
	{
		// Token: 0x0600467E RID: 18046 RVA: 0x000BCB9A File Offset: 0x000BAD9A
		public SspiContext()
		{
			this.ServerTlsProtocols = SspiContext.DefaultServerTlsProtocols;
			this.ClientTlsProtocols = SspiContext.DefaultClientTlsProtocols;
		}

		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x0600467F RID: 18047 RVA: 0x000BCBC3 File Offset: 0x000BADC3
		// (set) Token: 0x06004680 RID: 18048 RVA: 0x000BCBCB File Offset: 0x000BADCB
		public SchannelProtocols ServerTlsProtocols { get; set; }

		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x06004681 RID: 18049 RVA: 0x000BCBD4 File Offset: 0x000BADD4
		public static SchannelProtocols DefaultServerTlsProtocols
		{
			get
			{
				return SchannelProtocols.Zero;
			}
		}

		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x06004682 RID: 18050 RVA: 0x000BCBD7 File Offset: 0x000BADD7
		// (set) Token: 0x06004683 RID: 18051 RVA: 0x000BCBDF File Offset: 0x000BADDF
		public SchannelProtocols ClientTlsProtocols { get; set; }

		// Token: 0x170011C7 RID: 4551
		// (get) Token: 0x06004684 RID: 18052 RVA: 0x000BCBE8 File Offset: 0x000BADE8
		public static SchannelProtocols DefaultClientTlsProtocols
		{
			get
			{
				return SchannelProtocols.Zero;
			}
		}

		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x06004685 RID: 18053 RVA: 0x000BCBEB File Offset: 0x000BADEB
		public ContextState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x06004686 RID: 18054 RVA: 0x000BCBF3 File Offset: 0x000BADF3
		public ContextFlags RequestedContextFlags
		{
			get
			{
				return this.requestedContextFlags;
			}
		}

		// Token: 0x170011CA RID: 4554
		// (get) Token: 0x06004687 RID: 18055 RVA: 0x000BCBFB File Offset: 0x000BADFB
		public ContextFlags ReturnedContextFlags
		{
			get
			{
				return this.returnedContextFlags;
			}
		}

		// Token: 0x170011CB RID: 4555
		// (get) Token: 0x06004688 RID: 18056 RVA: 0x000BCC03 File Offset: 0x000BAE03
		public int HeaderSize
		{
			get
			{
				return this.headerSize;
			}
		}

		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x06004689 RID: 18057 RVA: 0x000BCC0B File Offset: 0x000BAE0B
		public int MaxMessageSize
		{
			get
			{
				return this.maxMessageSize;
			}
		}

		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x0600468A RID: 18058 RVA: 0x000BCC13 File Offset: 0x000BAE13
		public int MaxStreamSize
		{
			get
			{
				return this.headerSize + this.maxMessageSize + this.trailerSize;
			}
		}

		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x0600468B RID: 18059 RVA: 0x000BCC29 File Offset: 0x000BAE29
		public int MaxTokenSize
		{
			get
			{
				return this.maxTokenSize;
			}
		}

		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x0600468C RID: 18060 RVA: 0x000BCC31 File Offset: 0x000BAE31
		public int TrailerSize
		{
			get
			{
				return this.trailerSize;
			}
		}

		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x0600468D RID: 18061 RVA: 0x000BCC39 File Offset: 0x000BAE39
		public CredentialUse CredentialUse
		{
			get
			{
				return this.credentialUse;
			}
		}

		// Token: 0x0600468E RID: 18062 RVA: 0x000BCC41 File Offset: 0x000BAE41
		public static IntPtr IntPtrAdd(IntPtr a, int b)
		{
			return (IntPtr)((long)a + (long)b);
		}

		// Token: 0x0600468F RID: 18063 RVA: 0x000BCC51 File Offset: 0x000BAE51
		public static int IntPtrDiff(IntPtr a, IntPtr b)
		{
			return (int)((long)a - (long)b);
		}

		// Token: 0x06004690 RID: 18064 RVA: 0x000BCC64 File Offset: 0x000BAE64
		public virtual SecurityStatus InitializeForInboundAuthentication(string packageName, ExtendedProtectionConfig config, ChannelBindingToken token)
		{
			if (this.credHandle != null)
			{
				throw new InvalidOperationException();
			}
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			this.requestedContextFlags = (ContextFlags.AcceptExtendedError | ContextFlags.AcceptAllowNonUserLogons);
			if (config.PolicySetting != ExtendedProtectionPolicySetting.None)
			{
				if (config.PolicySetting == ExtendedProtectionPolicySetting.Allow)
				{
					this.requestedContextFlags |= ContextFlags.AllowMissingBindings;
				}
				if (config.ExtendedProtectionTlsTerminatedAtProxyScenario)
				{
					this.requestedContextFlags |= ContextFlags.ProxyBindings;
					token = null;
				}
				this.extendedProtectionConfig = config;
			}
			else
			{
				token = null;
			}
			this.credentialUse = CredentialUse.Inbound;
			return this.InitializeAuthenticationCommon(packageName, null, AuthIdentity.Default, token);
		}

		// Token: 0x06004691 RID: 18065 RVA: 0x000BCCF9 File Offset: 0x000BAEF9
		public SecurityStatus InitializeForOutboundAuthentication(string packageName, string target, AuthIdentity authIdentity, bool requestMutualAuth, ChannelBindingToken token)
		{
			if (this.credHandle != null)
			{
				throw new InvalidOperationException();
			}
			if (requestMutualAuth)
			{
				this.requestedContextFlags = (ContextFlags.MutualAuth | ContextFlags.InitExtendedError);
			}
			else
			{
				this.requestedContextFlags = ContextFlags.InitExtendedError;
			}
			this.credentialUse = CredentialUse.Outbound;
			return this.InitializeAuthenticationCommon(packageName, target, authIdentity, token);
		}

		// Token: 0x06004692 RID: 18066 RVA: 0x000BCD38 File Offset: 0x000BAF38
		public SecurityStatus InitializeAuthenticationCommon(string packageName, string target, AuthIdentity authIdentity, ChannelBindingToken token)
		{
			this.targetName = target;
			this.channelBindingToken = token;
			SecurityStatus securityStatus = this.DetermineMaxToken(packageName);
			if (securityStatus != SecurityStatus.OK)
			{
				return securityStatus;
			}
			this.credHandle = new SafeCredentialsHandle();
			long num;
			securityStatus = SspiNativeMethods.AcquireCredentialsHandle(null, packageName, this.credentialUse, null, ref authIdentity, null, null, ref this.credHandle.SspiHandle, out num);
			if (securityStatus == SecurityStatus.OK)
			{
				this.state = ContextState.Initialized;
			}
			return securityStatus;
		}

		// Token: 0x06004693 RID: 18067 RVA: 0x000BCDA4 File Offset: 0x000BAFA4
		public unsafe SecurityStatus InitializeForTls(CredentialUse use, bool requestClientCertificate, X509Certificate cert, string target)
		{
			if (this.credHandle != null)
			{
				throw new InvalidOperationException();
			}
			this.isTls = true;
			SchannelProtocols schannelProtocols;
			SchannelCredential.Flags flags;
			switch (use)
			{
			case CredentialUse.Inbound:
				schannelProtocols = this.ServerTlsProtocols;
				this.requestedContextFlags = (ContextFlags.ReplayDetect | ContextFlags.SequenceDetect | ContextFlags.Confidentiality | ContextFlags.AcceptExtendedError | ContextFlags.AcceptStream);
				flags = SchannelCredential.Flags.Zero;
				if (requestClientCertificate)
				{
					this.requestedContextFlags |= ContextFlags.MutualAuth;
				}
				SspiContext.tlsInboundCredHandleCache.TryGetValue(Tuple.Create<SchannelProtocols, X509Certificate>(schannelProtocols, cert), out this.credHandle);
				break;
			case CredentialUse.Outbound:
				schannelProtocols = this.ClientTlsProtocols;
				this.requestedContextFlags = (ContextFlags.ReplayDetect | ContextFlags.SequenceDetect | ContextFlags.Confidentiality | ContextFlags.InitExtendedError | ContextFlags.AcceptExtendedError | ContextFlags.InitManualCredValidation | ContextFlags.InitUseSuppliedCreds);
				flags = (SchannelCredential.Flags.ValidateManual | SchannelCredential.Flags.NoDefaultCred);
				this.credHandle = TlsCredentialCache.Find(cert, target);
				break;
			default:
				throw new ArgumentOutOfRangeException("use");
			}
			this.credentialUse = use;
			this.certificate = cert;
			this.targetName = target;
			SecurityStatus securityStatus = this.DetermineMaxToken("Microsoft Unified Security Protocol Provider");
			if (securityStatus != SecurityStatus.OK)
			{
				return securityStatus;
			}
			if (this.credHandle == null)
			{
				SchannelCredential schannelCredential = new SchannelCredential(4, cert, flags, schannelProtocols);
				IntPtr certContextArray = schannelCredential.certContextArray;
				IntPtr certContextArray2 = new IntPtr((void*)(&certContextArray));
				if (certContextArray != IntPtr.Zero)
				{
					schannelCredential.certContextArray = certContextArray2;
				}
				this.credHandle = new SafeCredentialsHandle();
				long num;
				securityStatus = SspiNativeMethods.AcquireCredentialsHandle(null, "Microsoft Unified Security Protocol Provider", use, null, ref schannelCredential, null, null, ref this.credHandle.SspiHandle, out num);
			}
			if (securityStatus == SecurityStatus.OK)
			{
				this.state = ContextState.Initialized;
			}
			if (use == CredentialUse.Inbound && SspiContext.tlsInboundCredHandleCache != null)
			{
				SspiContext.tlsInboundCredHandleCache.AddOrUpdate(Tuple.Create<SchannelProtocols, X509Certificate>(schannelProtocols, cert), this.credHandle, (Tuple<SchannelProtocols, X509Certificate> key, SafeCredentialsHandle oldValue) => this.credHandle);
			}
			return securityStatus;
		}

		// Token: 0x06004694 RID: 18068 RVA: 0x000BCF28 File Offset: 0x000BB128
		public SecurityStatus NegotiateSecurityContext(NetworkBuffer inputBuffer, NetworkBuffer outputBuffer)
		{
			outputBuffer.EmptyBuffer();
			int num;
			int num2;
			SecurityStatus result;
			if (inputBuffer != null)
			{
				result = this.NegotiateSecurityContext(inputBuffer.Buffer, inputBuffer.DataStartOffset, inputBuffer.Remaining, outputBuffer.Buffer, outputBuffer.BufferStartOffset, outputBuffer.Length, out num, out num2);
			}
			else
			{
				result = this.NegotiateSecurityContext(null, 0, 0, outputBuffer.Buffer, outputBuffer.BufferStartOffset, outputBuffer.Length, out num, out num2);
			}
			if (num != 0 && inputBuffer != null)
			{
				inputBuffer.ConsumeData(num);
			}
			if (num2 != 0)
			{
				outputBuffer.ReportBytesFilled(num2);
			}
			return result;
		}

		// Token: 0x06004695 RID: 18069 RVA: 0x000BCFA8 File Offset: 0x000BB1A8
		public SecurityStatus NegotiateSecurityContext(byte[] inputBuffer, int inputOffset, int inputLength, byte[] outputBuffer, int outputOffset, int outputLength, out int inputConsumed, out int outputFilled)
		{
			if (this.state == ContextState.Initialized)
			{
				this.state = ContextState.Negotiating;
			}
			else if (this.state != ContextState.Negotiating)
			{
				throw new InvalidOperationException();
			}
			if (this.contextHandle == null)
			{
				this.contextHandle = new SafeContextHandle();
			}
			SecurityStatus securityStatus = this.NegotiateSecurityContextInternal(inputBuffer, inputOffset, inputLength, outputBuffer, outputOffset, outputLength, out inputConsumed, out outputFilled);
			if (securityStatus == SecurityStatus.OK)
			{
				this.state = ContextState.NegotiationComplete;
				StreamSizes streamSizes;
				if (this.QueryStreamSizes(out streamSizes) == SecurityStatus.OK)
				{
					this.headerSize = streamSizes.Header;
					this.maxMessageSize = streamSizes.MaxMessage;
					this.trailerSize = streamSizes.Trailer;
				}
				SecSizes secSizes;
				if (this.QuerySizes(out secSizes) == SecurityStatus.OK)
				{
					this.maxTokenSize = secSizes.MaxToken;
					this.blockSize = secSizes.BlockSize;
					this.securityTrailerSize = secSizes.SecurityTrailer;
				}
			}
			return securityStatus;
		}

		// Token: 0x06004696 RID: 18070 RVA: 0x000BD064 File Offset: 0x000BB264
		protected unsafe virtual SecurityStatus NegotiateSecurityContextInternal(byte[] inputBuffer, int inputOffset, int inputLength, byte[] outputBuffer, int outputOffset, int outputLength, out int inputConsumed, out int outputFilled)
		{
			GCHandle gchandle = default(GCHandle);
			GCHandle gchandle2 = default(GCHandle);
			GCHandle gchandle3 = default(GCHandle);
			int num = 0;
			if (inputBuffer != null)
			{
				num += 2;
			}
			if (this.channelBindingToken != null)
			{
				num++;
			}
			SecurityBufferDescriptor securityBufferDescriptor = new SecurityBufferDescriptor(num);
			SecurityBufferDescriptor securityBufferDescriptor2 = new SecurityBufferDescriptor(1);
			SecurityBuffer[] array = (num > 0) ? new SecurityBuffer[num] : null;
			SecurityBuffer[] array2 = new SecurityBuffer[1];
			inputConsumed = 0;
			outputFilled = 0;
			SecurityStatus securityStatus;
			try
			{
				num = 0;
				if (inputBuffer != null && array != null)
				{
					gchandle = GCHandle.Alloc(inputBuffer, GCHandleType.Pinned);
					array[num].count = inputLength;
					array[num].type = BufferType.Token;
					array[num].token = Marshal.UnsafeAddrOfPinnedArrayElement(inputBuffer, inputOffset);
					num++;
					array[num].count = 0;
					array[num].type = BufferType.Empty;
					array[num].token = IntPtr.Zero;
					num++;
				}
				if (this.channelBindingToken != null && array != null)
				{
					gchandle2 = GCHandle.Alloc(this.channelBindingToken.Buffer, GCHandleType.Pinned);
					array[num].count = this.channelBindingToken.Buffer.Length;
					array[num].type = BufferType.ChannelBindings;
					array[num].token = Marshal.UnsafeAddrOfPinnedArrayElement(this.channelBindingToken.Buffer, 0);
				}
				gchandle3 = GCHandle.Alloc(outputBuffer, GCHandleType.Pinned);
				array2[0].count = outputLength;
				array2[0].type = BufferType.Token;
				array2[0].token = Marshal.UnsafeAddrOfPinnedArrayElement(outputBuffer, outputOffset);
				try
				{
					fixed (IntPtr* ptr = array)
					{
						securityBufferDescriptor.UnmanagedPointer = (void*)ptr;
						try
						{
							fixed (IntPtr* ptr2 = array2)
							{
								securityBufferDescriptor2.UnmanagedPointer = (void*)ptr2;
								SspiHandle sspiHandle = this.contextHandle.SspiHandle;
								if (this.credentialUse == CredentialUse.Outbound)
								{
									long num2;
									securityStatus = SspiNativeMethods.InitializeSecurityContext(ref this.credHandle.SspiHandle, sspiHandle.IsZero ? null : ((void*)(&sspiHandle)), this.targetName, this.requestedContextFlags, 0, Endianness.Network, securityBufferDescriptor, 0, ref this.contextHandle.SspiHandle, securityBufferDescriptor2, ref this.returnedContextFlags, out num2);
								}
								else
								{
									long num2;
									securityStatus = SspiNativeMethods.AcceptSecurityContext(ref this.credHandle.SspiHandle, sspiHandle.IsZero ? null : ((void*)(&sspiHandle)), securityBufferDescriptor, this.requestedContextFlags, Endianness.Network, ref this.contextHandle.SspiHandle, securityBufferDescriptor2, ref this.returnedContextFlags, out num2);
								}
								outputFilled = ((!SspiContext.IsSecurityStatusFailure(securityStatus)) ? array2[0].count : 0);
								if (inputBuffer != null && securityStatus != SecurityStatus.IncompleteMessage && array != null)
								{
									inputConsumed = array[0].count - array[1].count;
								}
							}
						}
						finally
						{
							IntPtr* ptr2 = null;
						}
					}
				}
				finally
				{
					IntPtr* ptr = null;
				}
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
				if (gchandle2.IsAllocated)
				{
					gchandle2.Free();
				}
				if (gchandle3.IsAllocated)
				{
					gchandle3.Free();
				}
			}
			return securityStatus;
		}

		// Token: 0x06004697 RID: 18071 RVA: 0x000BD3AC File Offset: 0x000BB5AC
		public unsafe SecurityStatus EncryptMessage(byte[] buffer, int offset, int size, NetworkBuffer outBuffer)
		{
			if (this.state != ContextState.NegotiationComplete)
			{
				throw new InvalidOperationException();
			}
			if (size < 0 || size > this.maxMessageSize)
			{
				throw new ArgumentException("size");
			}
			GCHandle gchandle = default(GCHandle);
			SecurityBufferDescriptor securityBufferDescriptor = new SecurityBufferDescriptor(4);
			SecurityBuffer[] array = new SecurityBuffer[4];
			outBuffer.EmptyBuffer();
			Buffer.BlockCopy(buffer, offset, outBuffer.Buffer, outBuffer.BufferStartOffset + this.headerSize, size);
			SecurityStatus result;
			try
			{
				gchandle = GCHandle.Alloc(outBuffer.Buffer, GCHandleType.Pinned);
				array[0].count = this.headerSize;
				array[0].type = BufferType.Header;
				array[0].token = Marshal.UnsafeAddrOfPinnedArrayElement(outBuffer.Buffer, outBuffer.BufferStartOffset);
				array[1].count = size;
				array[1].type = BufferType.Data;
				array[1].token = SspiContext.IntPtrAdd(array[0].token, array[0].count);
				array[2].count = this.trailerSize;
				array[2].type = BufferType.Trailer;
				array[2].token = SspiContext.IntPtrAdd(array[1].token, array[1].count);
				array[3].count = 0;
				array[3].type = BufferType.Empty;
				array[3].token = IntPtr.Zero;
				try
				{
					fixed (IntPtr* ptr = array)
					{
						securityBufferDescriptor.UnmanagedPointer = (void*)ptr;
						result = SspiNativeMethods.EncryptMessage(ref this.contextHandle.SspiHandle, QualityOfProtection.None, securityBufferDescriptor, 0U);
					}
				}
				finally
				{
					IntPtr* ptr = null;
				}
				outBuffer.Filled = array[0].count + array[1].count + array[2].count;
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
			return result;
		}

		// Token: 0x06004698 RID: 18072 RVA: 0x000BD5D4 File Offset: 0x000BB7D4
		public unsafe SecurityStatus EncryptMessage(NetworkBuffer outBuffer)
		{
			if (this.state != ContextState.NegotiationComplete)
			{
				throw new InvalidOperationException();
			}
			if (this.headerSize > outBuffer.Consumed)
			{
				throw new InvalidOperationException();
			}
			if (this.trailerSize > outBuffer.Unused)
			{
				throw new InvalidOperationException();
			}
			GCHandle gchandle = default(GCHandle);
			SecurityBufferDescriptor securityBufferDescriptor = new SecurityBufferDescriptor(4);
			SecurityBuffer[] array = new SecurityBuffer[4];
			SecurityStatus result;
			try
			{
				gchandle = GCHandle.Alloc(outBuffer.Buffer, GCHandleType.Pinned);
				array[0].count = this.headerSize;
				array[0].type = BufferType.Header;
				array[0].token = Marshal.UnsafeAddrOfPinnedArrayElement(outBuffer.Buffer, outBuffer.DataStartOffset - this.headerSize);
				array[1].count = outBuffer.Remaining;
				array[1].type = BufferType.Data;
				array[1].token = SspiContext.IntPtrAdd(array[0].token, array[0].count);
				array[2].count = this.trailerSize;
				array[2].type = BufferType.Trailer;
				array[2].token = SspiContext.IntPtrAdd(array[1].token, array[1].count);
				array[3].count = 0;
				array[3].type = BufferType.Empty;
				array[3].token = IntPtr.Zero;
				try
				{
					fixed (IntPtr* ptr = array)
					{
						securityBufferDescriptor.UnmanagedPointer = (void*)ptr;
						result = SspiNativeMethods.EncryptMessage(ref this.contextHandle.SspiHandle, QualityOfProtection.None, securityBufferDescriptor, 0U);
					}
				}
				finally
				{
					IntPtr* ptr = null;
				}
				outBuffer.PutBackUnconsumedData(this.headerSize);
				outBuffer.Filled = outBuffer.Consumed + array[0].count + array[1].count + array[2].count;
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
			return result;
		}

		// Token: 0x06004699 RID: 18073 RVA: 0x000BD800 File Offset: 0x000BBA00
		public unsafe SecurityStatus DecryptMessage(NetworkBuffer outBuffer)
		{
			if (this.state != ContextState.NegotiationComplete)
			{
				throw new InvalidOperationException();
			}
			GCHandle gchandle = default(GCHandle);
			SecurityBufferDescriptor securityBufferDescriptor = new SecurityBufferDescriptor(4);
			SecurityBuffer[] array = new SecurityBuffer[4];
			bool flag = false;
			SecurityStatus result;
			try
			{
				gchandle = GCHandle.Alloc(outBuffer.Buffer, GCHandleType.Pinned);
				IntPtr intPtr = Marshal.UnsafeAddrOfPinnedArrayElement(outBuffer.Buffer, outBuffer.BufferStartOffset);
				SecurityStatus securityStatus;
				for (;;)
				{
					array[0].count = outBuffer.EncryptedDataLength;
					array[0].type = BufferType.Data;
					array[0].token = SspiContext.IntPtrAdd(intPtr, outBuffer.EncryptedDataOffset);
					try
					{
						fixed (IntPtr* ptr = array)
						{
							securityBufferDescriptor.UnmanagedPointer = (void*)ptr;
							QualityOfProtection qualityOfProtection;
							securityStatus = SspiNativeMethods.DecryptMessage(ref this.contextHandle.SspiHandle, securityBufferDescriptor, 0U, out qualityOfProtection);
						}
					}
					finally
					{
						IntPtr* ptr = null;
					}
					if (securityStatus == SecurityStatus.IncompleteMessage && flag)
					{
						break;
					}
					if (securityStatus != SecurityStatus.OK)
					{
						goto Block_7;
					}
					flag = true;
					outBuffer.EncryptedDataLength = 0;
					outBuffer.EncryptedDataOffset = 0;
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].type == BufferType.Data)
						{
							int count = array[i].count;
							if (count != 0)
							{
								int num = SspiContext.IntPtrDiff(array[i].token, intPtr);
								Buffer.BlockCopy(outBuffer.Buffer, outBuffer.BufferStartOffset + num, outBuffer.Buffer, outBuffer.BufferStartOffset + outBuffer.Filled, count);
								outBuffer.Filled += count;
							}
						}
						else if (array[i].type == BufferType.Extra)
						{
							outBuffer.EncryptedDataLength = array[i].count;
							outBuffer.EncryptedDataOffset = SspiContext.IntPtrDiff(array[i].token, intPtr);
						}
					}
					if (outBuffer.EncryptedDataLength == 0)
					{
						goto Block_12;
					}
					for (int j = 1; j < array.Length; j++)
					{
						array[j].count = 0;
						array[j].type = BufferType.Empty;
						array[j].token = IntPtr.Zero;
					}
				}
				return SecurityStatus.OK;
				Block_7:
				return securityStatus;
				Block_12:
				result = SecurityStatus.OK;
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
			return result;
		}

		// Token: 0x0600469A RID: 18074 RVA: 0x000BDA64 File Offset: 0x000BBC64
		public SecurityStatus WrapMessage(byte[] inputBuffer, bool encryptMessage, out byte[] outputBuffer)
		{
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			return this.WrapMessage(inputBuffer, 0, inputBuffer.Length, encryptMessage, out outputBuffer);
		}

		// Token: 0x0600469B RID: 18075 RVA: 0x000BDA84 File Offset: 0x000BBC84
		public unsafe SecurityStatus WrapMessage(byte[] buffer, int offset, int size, bool encryptMessage, out byte[] outputBuffer)
		{
			if (this.state != ContextState.NegotiationComplete)
			{
				throw new InvalidOperationException();
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (size < 0 || size > this.maxTokenSize)
			{
				throw new ArgumentException("size");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentException("offset");
			}
			SecurityBufferDescriptor securityBufferDescriptor = new SecurityBufferDescriptor(3);
			SecurityBuffer[] array = new SecurityBuffer[3];
			outputBuffer = null;
			int cb = this.securityTrailerSize + size + this.blockSize;
			SafeHGlobalHandle safeHGlobalHandle = new SafeHGlobalHandle(Marshal.AllocHGlobal(cb));
			array[0].count = this.securityTrailerSize;
			array[0].type = BufferType.Token;
			array[0].token = safeHGlobalHandle.DangerousGetHandle();
			array[1].count = size;
			array[1].type = BufferType.Data;
			array[1].token = SspiContext.IntPtrAdd(array[0].token, array[0].count);
			Marshal.Copy(buffer, offset, array[1].token, size);
			array[2].count = this.blockSize;
			array[2].type = BufferType.Padding;
			array[2].token = SspiContext.IntPtrAdd(array[1].token, array[1].count);
			fixed (IntPtr* ptr = array)
			{
				securityBufferDescriptor.UnmanagedPointer = (void*)ptr;
				SecurityStatus securityStatus = this.SspiEncryptMessage(ref this.contextHandle.SspiHandle, encryptMessage ? QualityOfProtection.None : QualityOfProtection.WrapNoEncrypt, securityBufferDescriptor, 0U);
				if (securityStatus != SecurityStatus.OK)
				{
					safeHGlobalHandle.Dispose();
					return securityStatus;
				}
			}
			int num = array[0].count + array[1].count + array[2].count;
			outputBuffer = new byte[num];
			Marshal.Copy(array[0].token, outputBuffer, 0, array[0].count);
			Marshal.Copy(array[1].token, outputBuffer, array[0].count, array[1].count);
			Marshal.Copy(array[2].token, outputBuffer, array[0].count + array[1].count, array[2].count);
			safeHGlobalHandle.Dispose();
			return SecurityStatus.OK;
		}

		// Token: 0x0600469C RID: 18076 RVA: 0x000BDCF9 File Offset: 0x000BBEF9
		protected virtual SecurityStatus SspiEncryptMessage(ref SspiHandle handlePtr, QualityOfProtection qualityOfProtection, SecurityBufferDescriptor inOut, uint sequenceNumber)
		{
			return SspiNativeMethods.EncryptMessage(ref handlePtr, qualityOfProtection, inOut, sequenceNumber);
		}

		// Token: 0x0600469D RID: 18077 RVA: 0x000BDD05 File Offset: 0x000BBF05
		public SecurityStatus UnwrapMessage(byte[] inputBuffer, out byte[] outputBuffer)
		{
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			return this.UnwrapMessage(inputBuffer, 0, inputBuffer.Length, out outputBuffer);
		}

		// Token: 0x0600469E RID: 18078 RVA: 0x000BDD24 File Offset: 0x000BBF24
		public unsafe SecurityStatus UnwrapMessage(byte[] inputBuffer, int offset, int size, out byte[] outputBuffer)
		{
			outputBuffer = null;
			if (this.state != ContextState.NegotiationComplete)
			{
				throw new InvalidOperationException();
			}
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (size < 0 || size > this.maxTokenSize)
			{
				throw new ArgumentException("size");
			}
			if (offset < 0 || offset > inputBuffer.Length)
			{
				throw new ArgumentException("offset");
			}
			SecurityBufferDescriptor securityBufferDescriptor = new SecurityBufferDescriptor(2);
			SecurityBuffer[] array = new SecurityBuffer[2];
			SecurityStatus securityStatus;
			using (SafeHGlobalHandle safeHGlobalHandle = new SafeHGlobalHandle(Marshal.AllocHGlobal(size)))
			{
				Marshal.Copy(inputBuffer, offset, safeHGlobalHandle.DangerousGetHandle(), size);
				array[0].count = size;
				array[0].type = BufferType.Stream;
				array[0].token = safeHGlobalHandle.DangerousGetHandle();
				array[1].count = 0;
				array[1].type = BufferType.Data;
				array[1].token = IntPtr.Zero;
				try
				{
					fixed (IntPtr* ptr = array)
					{
						securityBufferDescriptor.UnmanagedPointer = (void*)ptr;
						QualityOfProtection qualityOfProtection;
						securityStatus = SspiNativeMethods.DecryptMessage(ref this.contextHandle.SspiHandle, securityBufferDescriptor, 0U, out qualityOfProtection);
						if (securityStatus == SecurityStatus.OK)
						{
							outputBuffer = new byte[array[1].count];
							Marshal.Copy(array[1].token, outputBuffer, 0, array[1].count);
						}
					}
				}
				finally
				{
					IntPtr* ptr = null;
				}
			}
			return securityStatus;
		}

		// Token: 0x0600469F RID: 18079 RVA: 0x000BDEA4 File Offset: 0x000BC0A4
		public SecurityStatus DetermineMaxToken(string packageName)
		{
			SafeContextBuffer safeContextBuffer;
			SecurityStatus securityStatus = SspiNativeMethods.QuerySecurityPackageInfo(packageName, out safeContextBuffer);
			if (securityStatus == SecurityStatus.OK)
			{
				SecurityPackageInfo securityPackageInfo = new SecurityPackageInfo(safeContextBuffer.DangerousGetHandle());
				this.maxTokenSize = securityPackageInfo.MaxToken;
			}
			safeContextBuffer.Close();
			return securityStatus;
		}

		// Token: 0x060046A0 RID: 18080 RVA: 0x000BDEDE File Offset: 0x000BC0DE
		public virtual SecurityStatus QuerySecurityContextToken(out SafeTokenHandle token)
		{
			return SspiNativeMethods.QuerySecurityContextToken(ref this.contextHandle.SspiHandle, out token);
		}

		// Token: 0x060046A1 RID: 18081 RVA: 0x000BDEF4 File Offset: 0x000BC0F4
		public SecurityStatus QueryStreamSizes(out StreamSizes sizes)
		{
			object obj;
			SecurityStatus result = this.QueryContextAttributes(ContextAttribute.StreamSizes, out obj);
			sizes = (StreamSizes)obj;
			return result;
		}

		// Token: 0x060046A2 RID: 18082 RVA: 0x000BDF14 File Offset: 0x000BC114
		public SecurityStatus QueryEapKeyBlock(out EapKeyBlock block)
		{
			object obj;
			SecurityStatus result = this.QueryContextAttributes(ContextAttribute.EapKeyBlock, out obj);
			block = (EapKeyBlock)obj;
			return result;
		}

		// Token: 0x060046A3 RID: 18083 RVA: 0x000BDF38 File Offset: 0x000BC138
		public virtual SecurityStatus QuerySessionKey(out SessionKey block)
		{
			object obj;
			SecurityStatus result = this.QueryContextAttributes(ContextAttribute.SessionKey, out obj);
			block = (SessionKey)obj;
			return result;
		}

		// Token: 0x060046A4 RID: 18084 RVA: 0x000BDF5C File Offset: 0x000BC15C
		public SecurityStatus QueryConnectionInfo(out ConnectionInfo connectionInfo)
		{
			object obj;
			SecurityStatus result = this.QueryContextAttributes(ContextAttribute.ConnectionInfo, out obj);
			connectionInfo = (ConnectionInfo)obj;
			return result;
		}

		// Token: 0x060046A5 RID: 18085 RVA: 0x000BDF80 File Offset: 0x000BC180
		public SecurityStatus QuerySizes(out SecSizes sizes)
		{
			object obj;
			SecurityStatus result = this.QueryContextAttributes(ContextAttribute.Sizes, out obj);
			sizes = (SecSizes)obj;
			return result;
		}

		// Token: 0x060046A6 RID: 18086 RVA: 0x000BDFA0 File Offset: 0x000BC1A0
		public SecurityStatus QueryRemoteCertificate(out X509Certificate2 cert)
		{
			object obj;
			SecurityStatus result = this.QueryContextAttributes(ContextAttribute.RemoteCertificate, out obj);
			cert = (X509Certificate2)obj;
			return result;
		}

		// Token: 0x060046A7 RID: 18087 RVA: 0x000BDFC4 File Offset: 0x000BC1C4
		public SecurityStatus QueryLocalCertificate(out X509Certificate2 cert)
		{
			object obj;
			SecurityStatus result = this.QueryContextAttributes(ContextAttribute.LocalCertificate, out obj);
			cert = (X509Certificate2)obj;
			return result;
		}

		// Token: 0x060046A8 RID: 18088 RVA: 0x000BDFE8 File Offset: 0x000BC1E8
		public SecurityStatus QueryCredentialsNames(out string userName)
		{
			object obj;
			SecurityStatus result = this.QueryCredentialsAttributes(CredentialsAttribute.Names, out obj);
			userName = ((CredentialsNames)obj).UserName;
			return result;
		}

		// Token: 0x060046A9 RID: 18089 RVA: 0x000BE010 File Offset: 0x000BC210
		public SecurityStatus QueryPackageInfo(out SecurityPackageInfo info)
		{
			object obj;
			SecurityStatus result = this.QueryContextAttributes(ContextAttribute.PackageInfo, out obj);
			info = (SecurityPackageInfo)obj;
			return result;
		}

		// Token: 0x060046AA RID: 18090 RVA: 0x000BE038 File Offset: 0x000BC238
		public SecurityStatus QueryContextAttributes(ContextAttribute attribute, out object returnedAttributes)
		{
			returnedAttributes = null;
			int size;
			if (attribute <= ContextAttribute.PackageInfo)
			{
				if (attribute == ContextAttribute.Sizes)
				{
					size = SecSizes.Size;
					goto IL_B7;
				}
				if (attribute == ContextAttribute.StreamSizes)
				{
					size = StreamSizes.Size;
					goto IL_B7;
				}
				switch (attribute)
				{
				case ContextAttribute.SessionKey:
					size = SessionKey.Size;
					goto IL_B7;
				case ContextAttribute.PackageInfo:
					size = IntPtr.Size;
					goto IL_B7;
				}
			}
			else
			{
				switch (attribute)
				{
				case ContextAttribute.UniqueBindings:
				case ContextAttribute.EndpointBindings:
					size = ChannelBindingToken.Size;
					goto IL_B7;
				case ContextAttribute.ClientSpecifiedTarget:
					size = IntPtr.Size;
					goto IL_B7;
				default:
					switch (attribute)
					{
					case ContextAttribute.RemoteCertificate:
					case ContextAttribute.LocalCertificate:
						size = IntPtr.Size;
						goto IL_B7;
					default:
						switch (attribute)
						{
						case ContextAttribute.ConnectionInfo:
							size = ConnectionInfo.Size;
							goto IL_B7;
						case ContextAttribute.EapKeyBlock:
							size = EapKeyBlock.Size;
							goto IL_B7;
						}
						break;
					}
					break;
				}
			}
			return SecurityStatus.Unsupported;
			IL_B7:
			byte[] array = new byte[size];
			SecurityStatus securityStatus = SspiNativeMethods.QueryContextAttributes(ref this.contextHandle.SspiHandle, attribute, array);
			if (securityStatus != SecurityStatus.OK)
			{
				return securityStatus;
			}
			if (attribute <= ContextAttribute.PackageInfo)
			{
				if (attribute == ContextAttribute.Sizes)
				{
					returnedAttributes = new SecSizes(array);
					return securityStatus;
				}
				if (attribute == ContextAttribute.StreamSizes)
				{
					returnedAttributes = new StreamSizes(array);
					return securityStatus;
				}
				switch (attribute)
				{
				case ContextAttribute.SessionKey:
					returnedAttributes = new SessionKey(array);
					return securityStatus;
				case ContextAttribute.PackageInfo:
				{
					SecurityPackageInfo securityPackageInfo = new SecurityPackageInfo(array);
					returnedAttributes = securityPackageInfo;
					return securityStatus;
				}
				}
			}
			else
			{
				switch (attribute)
				{
				case ContextAttribute.UniqueBindings:
				case ContextAttribute.EndpointBindings:
				{
					ChannelBindingToken channelBindingToken = new ChannelBindingToken(array);
					returnedAttributes = ((channelBindingToken.Buffer.Length > 0) ? channelBindingToken : null);
					return securityStatus;
				}
				case ContextAttribute.ClientSpecifiedTarget:
				{
					ClientSpecifiedTarget clientSpecifiedTarget = new ClientSpecifiedTarget(array);
					returnedAttributes = clientSpecifiedTarget.ToString();
					return securityStatus;
				}
				default:
					switch (attribute)
					{
					case ContextAttribute.RemoteCertificate:
					case ContextAttribute.LocalCertificate:
						using (SafeCertificateContext safeCertificateContext = new SafeCertificateContext(array))
						{
							returnedAttributes = new X509Certificate2(safeCertificateContext.DangerousGetHandle());
							return securityStatus;
						}
						break;
					default:
						switch (attribute)
						{
						case ContextAttribute.ConnectionInfo:
							break;
						case ContextAttribute.EapKeyBlock:
							returnedAttributes = new EapKeyBlock(array);
							return securityStatus;
						default:
							return SecurityStatus.Unsupported;
						}
						break;
					}
					returnedAttributes = new ConnectionInfo(array);
					return securityStatus;
				}
			}
			return SecurityStatus.Unsupported;
		}

		// Token: 0x060046AB RID: 18091 RVA: 0x000BE238 File Offset: 0x000BC438
		public SecurityStatus QueryCredentialsAttributes(CredentialsAttribute attribute, out object returnedAttributes)
		{
			returnedAttributes = null;
			if (attribute != CredentialsAttribute.Names)
			{
				return SecurityStatus.Unsupported;
			}
			int size = CredentialsNames.Size;
			byte[] array = new byte[size];
			SecurityStatus securityStatus = SspiNativeMethods.QueryCredentialsAttributes(ref this.credHandle.SspiHandle, attribute, array);
			if (securityStatus != SecurityStatus.OK)
			{
				return securityStatus;
			}
			if (attribute == CredentialsAttribute.Names)
			{
				returnedAttributes = new CredentialsNames(array);
				return securityStatus;
			}
			return SecurityStatus.Unsupported;
		}

		// Token: 0x060046AC RID: 18092 RVA: 0x000BE299 File Offset: 0x000BC499
		public static bool IsSecurityStatusFailure(SecurityStatus status)
		{
			return (status & SecurityStatus.ErrorMask) == SecurityStatus.ErrorMask;
		}

		// Token: 0x060046AD RID: 18093 RVA: 0x000BE2AC File Offset: 0x000BC4AC
		public SecurityStatus VerifyServiceBindings()
		{
			if (this.extendedProtectionConfig.PolicySetting == ExtendedProtectionPolicySetting.None)
			{
				return SecurityStatus.OK;
			}
			if (this.channelBindingToken != null && !this.extendedProtectionConfig.ExtendedProtectionTlsTerminatedAtProxyScenario)
			{
				return SecurityStatus.OK;
			}
			SecurityPackageInfo securityPackageInfo;
			if (this.QueryPackageInfo(out securityPackageInfo) == SecurityStatus.OK && securityPackageInfo.Name.Equals("Kerberos"))
			{
				return SecurityStatus.OK;
			}
			string text;
			SecurityStatus securityStatus = this.QueryClientSpecifiedTarget(out text);
			if (securityStatus == SecurityStatus.Unsupported)
			{
				return SecurityStatus.ExtendedProtectionOSNotPatched;
			}
			if (securityStatus == SecurityStatus.TargetUnknown)
			{
				if (this.extendedProtectionConfig.PolicySetting == ExtendedProtectionPolicySetting.Allow)
				{
					return SecurityStatus.OK;
				}
				return SecurityStatus.ExtendedProtectionMissingSpn;
			}
			else
			{
				if (securityStatus != SecurityStatus.OK)
				{
					return securityStatus;
				}
				if (this.extendedProtectionConfig.IsValidTargetName(text))
				{
					return SecurityStatus.OK;
				}
				return SecurityStatus.ExtendedProtectionWrongSpn;
			}
		}

		// Token: 0x060046AE RID: 18094 RVA: 0x000BE344 File Offset: 0x000BC544
		public SecurityStatus CaptureChannelBindingToken(ChannelBindingType tokenType, out ChannelBindingToken token)
		{
			ContextAttribute attribute;
			switch (tokenType)
			{
			case ChannelBindingType.Unique:
				attribute = ContextAttribute.UniqueBindings;
				break;
			case ChannelBindingType.Endpoint:
				attribute = ContextAttribute.EndpointBindings;
				break;
			default:
				throw new ArgumentException("tokenType");
			}
			object obj;
			SecurityStatus result = this.QueryContextAttributes(attribute, out obj);
			token = (obj as ChannelBindingToken);
			return result;
		}

		// Token: 0x060046AF RID: 18095 RVA: 0x000BE38B File Offset: 0x000BC58B
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SspiContext>(this);
		}

		// Token: 0x060046B0 RID: 18096 RVA: 0x000BE394 File Offset: 0x000BC594
		protected override void InternalDispose(bool disposing)
		{
			if (this.contextHandle != null && !this.contextHandle.IsClosed)
			{
				this.contextHandle.Close();
				this.contextHandle = null;
			}
			if (this.credHandle != null && !this.credHandle.IsClosed)
			{
				if (this.isTls && this.credentialUse == CredentialUse.Outbound && this.state == ContextState.NegotiationComplete)
				{
					TlsCredentialCache.Add(this.certificate, this.targetName, this.credHandle);
				}
				else if (!this.isTls)
				{
					this.credHandle.Close();
				}
				this.credHandle = null;
			}
		}

		// Token: 0x060046B1 RID: 18097 RVA: 0x000BE42C File Offset: 0x000BC62C
		private SecurityStatus QueryClientSpecifiedTarget(out string target)
		{
			object obj;
			SecurityStatus result = this.QueryContextAttributes(ContextAttribute.ClientSpecifiedTarget, out obj);
			target = (obj as string);
			return result;
		}

		// Token: 0x04003ACA RID: 15050
		private static readonly ConcurrentDictionary<Tuple<SchannelProtocols, X509Certificate>, SafeCredentialsHandle> tlsInboundCredHandleCache = new ConcurrentDictionary<Tuple<SchannelProtocols, X509Certificate>, SafeCredentialsHandle>();

		// Token: 0x04003ACB RID: 15051
		private SafeCredentialsHandle credHandle;

		// Token: 0x04003ACC RID: 15052
		private SafeContextHandle contextHandle;

		// Token: 0x04003ACD RID: 15053
		private CredentialUse credentialUse;

		// Token: 0x04003ACE RID: 15054
		protected ContextState state;

		// Token: 0x04003ACF RID: 15055
		private ContextFlags requestedContextFlags;

		// Token: 0x04003AD0 RID: 15056
		private ContextFlags returnedContextFlags;

		// Token: 0x04003AD1 RID: 15057
		private string targetName;

		// Token: 0x04003AD2 RID: 15058
		private ChannelBindingToken channelBindingToken;

		// Token: 0x04003AD3 RID: 15059
		private ExtendedProtectionConfig extendedProtectionConfig = ExtendedProtectionConfig.NoExtendedProtection;

		// Token: 0x04003AD4 RID: 15060
		private X509Certificate certificate;

		// Token: 0x04003AD5 RID: 15061
		private int headerSize;

		// Token: 0x04003AD6 RID: 15062
		private int maxMessageSize;

		// Token: 0x04003AD7 RID: 15063
		private int maxTokenSize;

		// Token: 0x04003AD8 RID: 15064
		private int trailerSize;

		// Token: 0x04003AD9 RID: 15065
		private int securityTrailerSize;

		// Token: 0x04003ADA RID: 15066
		private int blockSize;

		// Token: 0x04003ADB RID: 15067
		private bool isTls;
	}
}
