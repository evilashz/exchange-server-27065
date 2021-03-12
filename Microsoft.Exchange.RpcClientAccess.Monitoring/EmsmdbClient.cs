using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MapiHttp;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.ExchangeClient;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200000F RID: 15
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class EmsmdbClient : BaseObject, IEmsmdbClient, IRpcClient, IDisposable
	{
		// Token: 0x0600005D RID: 93 RVA: 0x000029D7 File Offset: 0x00000BD7
		public EmsmdbClient(RpcBindingInfo bindingInfo)
		{
			this.protocolClient = new ExchangeAsyncRpcClient(bindingInfo);
			this.connectionUriString = bindingInfo.Uri.ToString();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002A08 File Offset: 0x00000C08
		public EmsmdbClient(MapiHttpBindingInfo bindingInfo)
		{
			EmsmdbHttpClient emsmdbHttpClient = new EmsmdbHttpClient(bindingInfo);
			this.protocolClient = emsmdbHttpClient;
			string vdirPath = emsmdbHttpClient.VdirPath;
			this.connectionUriString = bindingInfo.BuildRequestPath(ref vdirPath);
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002A4C File Offset: 0x00000C4C
		public string BindingString
		{
			get
			{
				RpcClientBase rpcClientBase = this.protocolClient as RpcClientBase;
				if (rpcClientBase != null)
				{
					return rpcClientBase.GetBindingString();
				}
				return "GetBindingString() not implemented";
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002A74 File Offset: 0x00000C74
		public IExchangeAsyncDispatch ProtocolClient
		{
			get
			{
				return this.protocolClient;
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002A7C File Offset: 0x00000C7C
		public IAsyncResult BeginConnect(string userDn, TimeSpan timeout, bool useMonitoringContext, AsyncCallback asyncCallback, object asyncState)
		{
			if (this.contextHandle != IntPtr.Zero)
			{
				throw new InvalidOperationException("BeginConnect cannot be issued more than once per client instance");
			}
			return new EmsmdbClient.ConnectCallContext(this.ProtocolClient, userDn, timeout, useMonitoringContext, asyncCallback, asyncState).Begin();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002AB2 File Offset: 0x00000CB2
		public ConnectCallResult EndConnect(IAsyncResult asyncResult)
		{
			return ((EmsmdbClient.ConnectCallContext)asyncResult).End(asyncResult, out this.contextHandle);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002AC6 File Offset: 0x00000CC6
		public IAsyncResult BeginLogon(string mailboxDn, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState)
		{
			if (this.contextHandle == IntPtr.Zero)
			{
				throw new InvalidOperationException("BeginLogon cannot be issued without a successful connection");
			}
			return new EmsmdbClient.PrivateLogonCallContext(this.ProtocolClient, this.contextHandle, mailboxDn, timeout, asyncCallback, asyncState).Begin();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002B00 File Offset: 0x00000D00
		public IAsyncResult BeginPublicLogon(string mailboxDn, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState)
		{
			if (this.contextHandle == IntPtr.Zero)
			{
				throw new InvalidOperationException("BeginPublicLogon cannot be issued without a successful connection");
			}
			EmsmdbClient.PublicLogonCallContext publicLogonCallContext = new EmsmdbClient.PublicLogonCallContext(this.ProtocolClient, this.contextHandle, mailboxDn, timeout, asyncCallback, asyncState);
			return publicLogonCallContext.Begin();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002B47 File Offset: 0x00000D47
		public LogonCallResult EndLogon(IAsyncResult asyncResult)
		{
			return ((EmsmdbClient.PrivateLogonCallContext)asyncResult).End(asyncResult, out this.contextHandle);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002B5B File Offset: 0x00000D5B
		public LogonCallResult EndPublicLogon(IAsyncResult asyncResult)
		{
			return ((EmsmdbClient.PublicLogonCallContext)asyncResult).End(asyncResult, out this.contextHandle);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002B6F File Offset: 0x00000D6F
		public IAsyncResult BeginDummy(TimeSpan timeout, AsyncCallback asyncCallback, object asyncState)
		{
			return new EmsmdbClient.DummyCallContext(this.protocolClient, timeout, asyncCallback, asyncState).Begin();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002B84 File Offset: 0x00000D84
		public DummyCallResult EndDummy(IAsyncResult asyncResult)
		{
			return ((EmsmdbClient.DummyCallContext)asyncResult).End(asyncResult);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002B92 File Offset: 0x00000D92
		public string GetConnectionUriString()
		{
			return this.connectionUriString;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002B9A File Offset: 0x00000D9A
		protected sealed override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<EmsmdbClient>(this);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002BA4 File Offset: 0x00000DA4
		protected sealed override void InternalDispose()
		{
			if (this.contextHandle != IntPtr.Zero)
			{
				this.ProtocolClient.EndDisconnect(this.ProtocolClient.BeginDisconnect(null, this.contextHandle, null, null), out this.contextHandle);
			}
			IDisposable disposable = this.ProtocolClient as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
			base.InternalDispose();
		}

		// Token: 0x04000021 RID: 33
		private readonly IExchangeAsyncDispatch protocolClient;

		// Token: 0x04000022 RID: 34
		private readonly string connectionUriString;

		// Token: 0x04000023 RID: 35
		private IntPtr contextHandle = IntPtr.Zero;

		// Token: 0x02000010 RID: 16
		private class ConnectCallContext : EmsmdbCallContext<ConnectCallResult>
		{
			// Token: 0x0600006C RID: 108 RVA: 0x00002C04 File Offset: 0x00000E04
			public ConnectCallContext(IExchangeAsyncDispatch protocolClient, string userDn, TimeSpan timeout, bool useMonitoringContext, AsyncCallback asyncCallback, object asyncState) : base(protocolClient, timeout, asyncCallback, asyncState)
			{
				Util.ThrowOnNullOrEmptyArgument(userDn, "userDn");
				this.userDn = userDn;
				this.useMonitoringContext = useMonitoringContext;
			}

			// Token: 0x0600006D RID: 109 RVA: 0x00002C37 File Offset: 0x00000E37
			public ConnectCallResult End(IAsyncResult asyncResult, out IntPtr contextHandle)
			{
				contextHandle = this.contextHandle;
				return base.GetResult();
			}

			// Token: 0x0600006E RID: 110 RVA: 0x00002C4C File Offset: 0x00000E4C
			internal static void ParseAuxOut(ArraySegment<byte> segmentExtendedAuxOut, out MonitoringActivityAuxiliaryBlock monitoringActivityAuxiliaryBlock, out ExceptionTraceAuxiliaryBlock exceptionTraceAuxiliaryBlock)
			{
				Util.ThrowOnNullArgument(segmentExtendedAuxOut, "segmentExtendedAuxOut");
				monitoringActivityAuxiliaryBlock = null;
				exceptionTraceAuxiliaryBlock = null;
				if (segmentExtendedAuxOut.Count > 0)
				{
					byte[] buffer = AsyncBufferPools.GetBuffer(EmsmdbConstants.MaxAuxBufferSize);
					ArraySegment<byte> arraySegment = new ArraySegment<byte>(buffer, 0, EmsmdbConstants.MaxAuxBufferSize);
					try
					{
						ArraySegment<byte>? arraySegment2 = null;
						arraySegment = ExtendedBufferHelper.Unwrap(CompressAndObfuscate.Instance, segmentExtendedAuxOut, arraySegment, out arraySegment2);
						if (arraySegment.Count > 0)
						{
							monitoringActivityAuxiliaryBlock = RopClient.ParseAuxiliaryBuffer(arraySegment).OfType<MonitoringActivityAuxiliaryBlock>().FirstOrDefault<MonitoringActivityAuxiliaryBlock>();
							exceptionTraceAuxiliaryBlock = RopClient.ParseAuxiliaryBuffer(arraySegment).OfType<ExceptionTraceAuxiliaryBlock>().FirstOrDefault<ExceptionTraceAuxiliaryBlock>();
						}
					}
					finally
					{
						if (buffer != null)
						{
							AsyncBufferPools.ReleaseBuffer(buffer);
						}
					}
				}
			}

			// Token: 0x0600006F RID: 111 RVA: 0x00002CF8 File Offset: 0x00000EF8
			protected override ICancelableAsyncResult OnBegin(CancelableAsyncCallback asyncCallback, object asyncState)
			{
				bool flag = false;
				ICancelableAsyncResult result;
				try
				{
					MapiVersion outlook = MapiVersion.Outlook15;
					this.auxIn = AsyncBufferPools.GetBuffer(EmsmdbConstants.MaxExtendedAuxBufferSize);
					this.auxOut = AsyncBufferPools.GetBuffer(EmsmdbConstants.MaxExtendedAuxBufferSize);
					ArraySegment<byte> segmentExtendedAuxIn = this.BuildAuxInBuffer(new ArraySegment<byte>(this.auxIn, 0, EmsmdbConstants.MaxExtendedAuxBufferSize));
					ArraySegment<byte> segmentExtendedAuxOut = new ArraySegment<byte>(this.auxOut, 0, EmsmdbConstants.MaxExtendedAuxBufferSize);
					ICancelableAsyncResult cancelableAsyncResult = base.ProtocolClient.BeginConnect(null, null, this.userDn, 0, 0, Encoding.ASCII.CodePage, 0, 0, (from x in outlook.ToQuartet()
					select (short)x).ToArray<short>(), segmentExtendedAuxIn, segmentExtendedAuxOut, asyncCallback, asyncState);
					base.StartRpcTimer();
					flag = true;
					result = cancelableAsyncResult;
				}
				finally
				{
					if (!flag)
					{
						this.Cleanup();
					}
				}
				return result;
			}

			// Token: 0x06000070 RID: 112 RVA: 0x00002DD8 File Offset: 0x00000FD8
			protected override ConnectCallResult OnEnd(ICancelableAsyncResult asyncResult)
			{
				ConnectCallResult result;
				try
				{
					TimeSpan zero = TimeSpan.Zero;
					int num = 0;
					TimeSpan zero2 = TimeSpan.Zero;
					string text;
					string text2;
					short[] array;
					ArraySegment<byte> segmentExtendedAuxOut;
					ErrorCode errorCode = (ErrorCode)base.ProtocolClient.EndConnect(asyncResult, out this.contextHandle, out zero, out num, out zero2, out text, out text2, out array, out segmentExtendedAuxOut);
					MonitoringActivityAuxiliaryBlock activityContext;
					ExceptionTraceAuxiliaryBlock remoteExceptionTrace;
					EmsmdbClient.ConnectCallContext.ParseAuxOut(segmentExtendedAuxOut, out activityContext, out remoteExceptionTrace);
					result = new ConnectCallResult(errorCode, remoteExceptionTrace, activityContext, new MapiVersion?(new MapiVersion((ushort)array[0], (ushort)array[1], (ushort)array[2], (ushort)array[3])), base.GetHttpResponseInformation());
				}
				finally
				{
					base.StopAndCleanupRpcTimer();
					this.Cleanup();
				}
				return result;
			}

			// Token: 0x06000071 RID: 113 RVA: 0x00002E70 File Offset: 0x00001070
			protected override ConnectCallResult OnRpcException(RpcException rpcException)
			{
				return new ConnectCallResult(rpcException);
			}

			// Token: 0x06000072 RID: 114 RVA: 0x00002E78 File Offset: 0x00001078
			protected override ConnectCallResult OnProtocolException(ProtocolException protocolException)
			{
				return new ConnectCallResult(protocolException, base.GetHttpInformationFromProtocolException(protocolException));
			}

			// Token: 0x06000073 RID: 115 RVA: 0x00002E88 File Offset: 0x00001088
			private ArraySegment<byte> BuildAuxInBuffer(ArraySegment<byte> extendedBuffer)
			{
				AuxiliaryBlock[] array = new AuxiliaryBlock[]
				{
					new PerfClientInfoAuxiliaryBlock(0U, 1, ComputerInformation.DnsPhysicalFullyQualifiedDomainName, string.Empty, Array<byte>.EmptySegment, Array<byte>.EmptySegment, string.Empty, Array<byte>.EmptySegment, ClientMode.Cached),
					new PerfProcessInfoAuxiliaryBlock(2, 1, Guid.NewGuid(), Assembly.GetExecutingAssembly().ManifestModule.Name)
				};
				if (this.useMonitoringContext)
				{
					array = array.Concat(new AuxiliaryBlock[]
					{
						new SetMonitoringContextAuxiliaryBlock()
					});
				}
				byte[] array2 = RopClient.CreateAuxiliaryBuffer(array);
				return ExtendedBufferHelper.Wrap(CompressAndObfuscate.Instance, extendedBuffer, new ArraySegment<byte>(array2), false, false);
			}

			// Token: 0x06000074 RID: 116 RVA: 0x00002F23 File Offset: 0x00001123
			private void Cleanup()
			{
				if (this.auxIn != null)
				{
					AsyncBufferPools.ReleaseBuffer(this.auxIn);
					this.auxIn = null;
				}
				if (this.auxOut != null)
				{
					AsyncBufferPools.ReleaseBuffer(this.auxOut);
					this.auxOut = null;
				}
			}

			// Token: 0x04000024 RID: 36
			private readonly string userDn;

			// Token: 0x04000025 RID: 37
			private readonly bool useMonitoringContext;

			// Token: 0x04000026 RID: 38
			private byte[] auxIn;

			// Token: 0x04000027 RID: 39
			private byte[] auxOut;

			// Token: 0x04000028 RID: 40
			private IntPtr contextHandle = IntPtr.Zero;
		}

		// Token: 0x02000011 RID: 17
		private abstract class BaseLogonCallContext : EmsmdbCallContext<LogonCallResult>
		{
			// Token: 0x06000076 RID: 118 RVA: 0x00002F59 File Offset: 0x00001159
			public BaseLogonCallContext(IExchangeAsyncDispatch protocolClient, IntPtr contextHandle, string mailboxDn, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState) : base(protocolClient, timeout, asyncCallback, asyncState)
			{
				Util.ThrowOnIntPtrZero(contextHandle, "contextHandle");
				this.contextHandle = contextHandle;
				this.mailboxDn = mailboxDn;
			}

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x06000077 RID: 119 RVA: 0x00002F8C File Offset: 0x0000118C
			// (set) Token: 0x06000078 RID: 120 RVA: 0x00002F94 File Offset: 0x00001194
			protected LogonFlags LogonFlags { get; set; }

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x06000079 RID: 121 RVA: 0x00002F9D File Offset: 0x0000119D
			// (set) Token: 0x0600007A RID: 122 RVA: 0x00002FA5 File Offset: 0x000011A5
			protected OpenFlags OpenFlags { get; set; }

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x0600007B RID: 123 RVA: 0x00002FAE File Offset: 0x000011AE
			// (set) Token: 0x0600007C RID: 124 RVA: 0x00002FB6 File Offset: 0x000011B6
			protected LogonExtendedRequestFlags ExtendedFlags { get; set; }

			// Token: 0x0600007D RID: 125 RVA: 0x00002FBF File Offset: 0x000011BF
			public LogonCallResult End(IAsyncResult asyncResult, out IntPtr contextHandle)
			{
				contextHandle = this.contextHandle;
				return base.GetResult();
			}

			// Token: 0x0600007E RID: 126 RVA: 0x00002FD4 File Offset: 0x000011D4
			protected override ICancelableAsyncResult OnBegin(CancelableAsyncCallback asyncCallback, object asyncState)
			{
				bool flag = false;
				ICancelableAsyncResult result;
				try
				{
					this.ropIn = AsyncBufferPools.GetBuffer(EmsmdbConstants.MaxExtendedRopBufferSize);
					this.ropOut = AsyncBufferPools.GetBuffer(EmsmdbConstants.MaxExtendedRopBufferSize);
					this.auxOut = AsyncBufferPools.GetBuffer(EmsmdbConstants.MaxExtendedAuxBufferSize);
					ArraySegment<byte> segmentExtendedRopIn = this.BuildLogonRequestBuffer(this.mailboxDn, new ArraySegment<byte>(this.ropIn, 0, EmsmdbConstants.MaxExtendedRopBufferSize));
					ArraySegment<byte> segmentExtendedRopOut = new ArraySegment<byte>(this.ropOut, 0, EmsmdbConstants.MaxExtendedRopBufferSize);
					ArraySegment<byte> emptySegment = Array<byte>.EmptySegment;
					ArraySegment<byte> segmentExtendedAuxOut = new ArraySegment<byte>(this.auxOut, 0, EmsmdbConstants.MaxExtendedAuxBufferSize);
					ICancelableAsyncResult cancelableAsyncResult = base.ProtocolClient.BeginExecute(null, this.contextHandle, 0, segmentExtendedRopIn, segmentExtendedRopOut, emptySegment, segmentExtendedAuxOut, asyncCallback, asyncState);
					base.StartRpcTimer();
					flag = true;
					result = cancelableAsyncResult;
				}
				finally
				{
					if (!flag)
					{
						this.Cleanup();
					}
				}
				return result;
			}

			// Token: 0x0600007F RID: 127 RVA: 0x000030A4 File Offset: 0x000012A4
			protected override LogonCallResult OnEnd(ICancelableAsyncResult asyncResult)
			{
				LogonCallResult result;
				try
				{
					ArraySegment<byte> segmentExtendedRopOut;
					ArraySegment<byte> segmentExtendedAuxOut;
					ErrorCode errorCode = (ErrorCode)base.ProtocolClient.EndExecute(asyncResult, out this.contextHandle, out segmentExtendedRopOut, out segmentExtendedAuxOut);
					ErrorCode logonErrorCode = (errorCode == ErrorCode.None) ? EmsmdbClient.BaseLogonCallContext.ParseLogonResponseBuffer(segmentExtendedRopOut) : ErrorCode.None;
					MonitoringActivityAuxiliaryBlock activityContext;
					ExceptionTraceAuxiliaryBlock remoteExceptionTrace;
					EmsmdbClient.ConnectCallContext.ParseAuxOut(segmentExtendedAuxOut, out activityContext, out remoteExceptionTrace);
					result = new LogonCallResult(errorCode, remoteExceptionTrace, activityContext, logonErrorCode, base.GetHttpResponseInformation());
				}
				finally
				{
					base.StopAndCleanupRpcTimer();
					this.Cleanup();
				}
				return result;
			}

			// Token: 0x06000080 RID: 128 RVA: 0x00003114 File Offset: 0x00001314
			protected override LogonCallResult OnRpcException(RpcException rpcException)
			{
				return new LogonCallResult(rpcException);
			}

			// Token: 0x06000081 RID: 129 RVA: 0x0000311C File Offset: 0x0000131C
			protected override LogonCallResult OnProtocolException(ProtocolException protocolException)
			{
				return new LogonCallResult(protocolException, base.GetHttpInformationFromProtocolException(protocolException));
			}

			// Token: 0x06000082 RID: 130 RVA: 0x0000312B File Offset: 0x0000132B
			protected virtual void BuildFlagsForLogonRequestBuffer()
			{
			}

			// Token: 0x06000083 RID: 131 RVA: 0x00003130 File Offset: 0x00001330
			private static ErrorCode ParseLogonResponseBuffer(ArraySegment<byte> segmentExtendedRopOut)
			{
				Util.ThrowOnNullArgument(segmentExtendedRopOut, "segmentExtendedRopOut");
				byte[] buffer = AsyncBufferPools.GetBuffer(EmsmdbConstants.MaxRopBufferSize);
				ArraySegment<byte> arraySegment = new ArraySegment<byte>(buffer, 0, EmsmdbConstants.MaxRopBufferSize);
				ErrorCode errorCode;
				try
				{
					ArraySegment<byte>? arraySegment2 = null;
					arraySegment = ExtendedBufferHelper.Unwrap(CompressAndObfuscate.Instance, segmentExtendedRopOut, arraySegment, out arraySegment2);
					List<RopResult> list = RopClient.ParseOneRop<RopLogon>(arraySegment);
					errorCode = list[0].ErrorCode;
				}
				finally
				{
					if (buffer != null)
					{
						AsyncBufferPools.ReleaseBuffer(buffer);
					}
				}
				return errorCode;
			}

			// Token: 0x06000084 RID: 132 RVA: 0x000031B0 File Offset: 0x000013B0
			private ArraySegment<byte> BuildLogonRequestBuffer(string mailboxDn, ArraySegment<byte> extendedBuffer)
			{
				this.BuildFlagsForLogonRequestBuffer();
				StoreState storeState = StoreState.None;
				RopLogon ropLogon = new RopLogon();
				ropLogon.SetInput(1, 0, this.LogonFlags, this.OpenFlags, storeState, this.ExtendedFlags, new MailboxId?(new MailboxId(mailboxDn)), null, "Client=Microsoft.Exchange.RpcClientAccess.Monitoring", null, null);
				byte[][] array = RopClient.CreateInputBuffer(new Rop[]
				{
					ropLogon
				});
				return ExtendedBufferHelper.Wrap(CompressAndObfuscate.Instance, extendedBuffer, new ArraySegment<byte>(array[0]), false, false);
			}

			// Token: 0x06000085 RID: 133 RVA: 0x00003230 File Offset: 0x00001430
			private void Cleanup()
			{
				if (this.ropIn != null)
				{
					AsyncBufferPools.ReleaseBuffer(this.ropIn);
					this.ropIn = null;
				}
				if (this.ropOut != null)
				{
					AsyncBufferPools.ReleaseBuffer(this.ropOut);
					this.ropOut = null;
				}
				if (this.auxOut != null)
				{
					AsyncBufferPools.ReleaseBuffer(this.auxOut);
					this.auxOut = null;
				}
			}

			// Token: 0x0400002A RID: 42
			private readonly string mailboxDn;

			// Token: 0x0400002B RID: 43
			private byte[] ropIn;

			// Token: 0x0400002C RID: 44
			private byte[] ropOut;

			// Token: 0x0400002D RID: 45
			private byte[] auxOut;

			// Token: 0x0400002E RID: 46
			private IntPtr contextHandle = IntPtr.Zero;
		}

		// Token: 0x02000012 RID: 18
		private class PrivateLogonCallContext : EmsmdbClient.BaseLogonCallContext
		{
			// Token: 0x06000086 RID: 134 RVA: 0x0000328B File Offset: 0x0000148B
			public PrivateLogonCallContext(IExchangeAsyncDispatch protocolClient, IntPtr contextHandle, string mailboxDn, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState) : base(protocolClient, contextHandle, mailboxDn, timeout, asyncCallback, asyncState)
			{
			}

			// Token: 0x06000087 RID: 135 RVA: 0x0000329C File Offset: 0x0000149C
			protected override void BuildFlagsForLogonRequestBuffer()
			{
				base.LogonFlags = (LogonFlags.Private | LogonFlags.Extended);
				base.OpenFlags = (OpenFlags.HomeLogon | OpenFlags.TakeOwnership | OpenFlags.NoMail | OpenFlags.CliWithPerMdbFix);
				base.ExtendedFlags = LogonExtendedRequestFlags.ApplicationId;
			}
		}

		// Token: 0x02000013 RID: 19
		private class PublicLogonCallContext : EmsmdbClient.BaseLogonCallContext
		{
			// Token: 0x06000088 RID: 136 RVA: 0x000032B8 File Offset: 0x000014B8
			public PublicLogonCallContext(IExchangeAsyncDispatch protocolClient, IntPtr contextHandle, string mailboxDn, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState) : base(protocolClient, contextHandle, mailboxDn, timeout, asyncCallback, asyncState)
			{
			}

			// Token: 0x06000089 RID: 137 RVA: 0x000032C9 File Offset: 0x000014C9
			protected override void BuildFlagsForLogonRequestBuffer()
			{
				base.LogonFlags = LogonFlags.None;
				base.OpenFlags = (OpenFlags.Public | OpenFlags.HomeLogon | OpenFlags.TakeOwnership | OpenFlags.NoMail | OpenFlags.CliWithPerMdbFix);
				base.ExtendedFlags = LogonExtendedRequestFlags.None;
			}
		}

		// Token: 0x02000014 RID: 20
		private class DummyCallContext : EmsmdbCallContext<DummyCallResult>
		{
			// Token: 0x0600008A RID: 138 RVA: 0x000032E4 File Offset: 0x000014E4
			public DummyCallContext(IExchangeAsyncDispatch protocolClient, TimeSpan timeout, AsyncCallback asyncCallback, object asyncState) : base(protocolClient, timeout, asyncCallback, asyncState)
			{
			}

			// Token: 0x0600008B RID: 139 RVA: 0x000032F1 File Offset: 0x000014F1
			public DummyCallResult End(IAsyncResult asyncResult)
			{
				return base.GetResult();
			}

			// Token: 0x0600008C RID: 140 RVA: 0x000032FC File Offset: 0x000014FC
			protected override ICancelableAsyncResult OnBegin(CancelableAsyncCallback asyncCallback, object asyncState)
			{
				ICancelableAsyncResult result = base.ProtocolClient.BeginDummy(null, null, asyncCallback, asyncState);
				base.StartRpcTimer();
				return result;
			}

			// Token: 0x0600008D RID: 141 RVA: 0x00003320 File Offset: 0x00001520
			protected override DummyCallResult OnEnd(ICancelableAsyncResult asyncResult)
			{
				DummyCallResult result;
				try
				{
					ErrorCode errorCode = (ErrorCode)base.ProtocolClient.EndDummy(asyncResult);
					result = new DummyCallResult(errorCode);
				}
				finally
				{
					base.StopAndCleanupRpcTimer();
				}
				return result;
			}

			// Token: 0x0600008E RID: 142 RVA: 0x0000335C File Offset: 0x0000155C
			protected override DummyCallResult OnRpcException(RpcException rpcException)
			{
				return new DummyCallResult(rpcException);
			}

			// Token: 0x0600008F RID: 143 RVA: 0x00003364 File Offset: 0x00001564
			protected override DummyCallResult OnProtocolException(ProtocolException protocolException)
			{
				return new DummyCallResult(protocolException);
			}
		}
	}
}
