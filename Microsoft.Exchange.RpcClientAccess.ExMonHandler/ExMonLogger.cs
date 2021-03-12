using System;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Isam.Esent.Interop.Vista;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000002 RID: 2
	internal class ExMonLogger : BaseObject
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002110 File Offset: 0x00000310
		internal ExMonLogger(bool enableTestMode, string clientAddress, string serviceName, ExMonLogger.CreateExmonRpcInstanceId delegateCreateInstanceId, ExMonLogger.ExmonRpcTraceEventInstance delegateTraceEventInstance)
		{
			this.header = new ExMonLogger.EventInstanceHeader(this);
			this.clientAddress = clientAddress;
			this.ServiceName = serviceName;
			this.enableTestMode = enableTestMode;
			if (!this.enableTestMode)
			{
				this.hookableCreateInstance = Hookable<ExMonLogger.CreateExmonRpcInstanceId>.Create(true, delegateCreateInstanceId);
				this.hookableTraceInstance = Hookable<ExMonLogger.ExmonRpcTraceEventInstance>.Create(true, delegateTraceEventInstance);
				return;
			}
			this.hookableCreateInstance = Hookable<ExMonLogger.CreateExmonRpcInstanceId>.Create(true, () => default(DiagnosticsNativeMethods.EventInstanceInfo));
			this.hookableTraceInstance = Hookable<ExMonLogger.ExmonRpcTraceEventInstance>.Create(true, delegate(byte[] buffer, ref DiagnosticsNativeMethods.EventInstanceInfo instanceInfo, ref DiagnosticsNativeMethods.EventInstanceInfo parentInstanceInfo)
			{
				return 0U;
			});
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000021BB File Offset: 0x000003BB
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000021C3 File Offset: 0x000003C3
		public string ServiceName
		{
			get
			{
				return this.serviceName;
			}
			set
			{
				this.serviceName = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000021CC File Offset: 0x000003CC
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000021D4 File Offset: 0x000003D4
		public string UserName
		{
			get
			{
				return this.userName;
			}
			set
			{
				this.userName = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000021DD File Offset: 0x000003DD
		public string ClientAddress
		{
			get
			{
				return this.clientAddress;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000021E5 File Offset: 0x000003E5
		internal ExMonLogger.EventInstanceHeader Header
		{
			get
			{
				return this.header;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000021ED File Offset: 0x000003ED
		internal byte[] Buffer
		{
			get
			{
				if (this.buffer == null)
				{
					this.buffer = ExMonLogger.bufferPool.Acquire();
				}
				return this.buffer;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000220D File Offset: 0x0000040D
		internal bool IsTracingEnabled
		{
			get
			{
				return this.enableTestMode || ETWTrace.IsExmonRpcEnabled;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000221E File Offset: 0x0000041E
		internal IDisposable SetCreateInstanceHook(ExMonLogger.CreateExmonRpcInstanceId hookFunction)
		{
			return this.hookableCreateInstance.SetTestHook(hookFunction);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000222C File Offset: 0x0000042C
		internal IDisposable SetTraceInstanceHook(ExMonLogger.ExmonRpcTraceEventInstance hookFunction)
		{
			return this.hookableTraceInstance.SetTestHook(hookFunction);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000223A File Offset: 0x0000043A
		internal void SetClassType(byte classType)
		{
			this.header.ClassType = classType;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002248 File Offset: 0x00000448
		internal void SetTraceSize(int bufferSize)
		{
			this.bytesWrittenToBuffer = bufferSize;
			this.Clear();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002257 File Offset: 0x00000457
		internal void WriteByte(int offset, byte value)
		{
			this.Buffer[offset] = value;
			this.UpdateBytesWritten(offset, 1);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000226C File Offset: 0x0000046C
		internal void WriteUShort(int offset, ushort value)
		{
			int bytesWritten = ExBitConverter.Write(value, this.Buffer, offset);
			this.UpdateBytesWritten(offset, bytesWritten);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002290 File Offset: 0x00000490
		internal void WriteUInt32(int offset, uint value)
		{
			int bytesWritten = ExBitConverter.Write(value, this.Buffer, offset);
			this.UpdateBytesWritten(offset, bytesWritten);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022B4 File Offset: 0x000004B4
		internal void WriteByteArray(int offset, byte[] value)
		{
			Array.Copy(value, 0, this.Buffer, offset, value.Length);
			int bytesWritten = value.Length;
			this.UpdateBytesWritten(offset, bytesWritten);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022E0 File Offset: 0x000004E0
		internal void WriteUInt64(int offset, ulong value)
		{
			int bytesWritten = ExBitConverter.Write(value, this.Buffer, offset);
			this.UpdateBytesWritten(offset, bytesWritten);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002304 File Offset: 0x00000504
		internal void WriteGuid(int offset, Guid value)
		{
			int bytesWritten = ExBitConverter.Write(value, this.Buffer, offset);
			this.UpdateBytesWritten(offset, bytesWritten);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002328 File Offset: 0x00000528
		internal void WriteUserAddressApplication(int offset, string user, string address, string application)
		{
			this.WriteUserAddressApplication(offset, user, address, application, null, null, null, null);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002344 File Offset: 0x00000544
		internal void WriteUserAddressApplication(int offset, string user, string address, string application, string activityId, string component, string protocol, string action)
		{
			int num = 0;
			num += ExBitConverter.Write(string.IsNullOrEmpty(user) ? "<none>" : user, 32, true, this.Buffer, offset + num);
			num += ExBitConverter.Write(string.IsNullOrEmpty(address) ? "<none>" : address, ExMonLogger.MaximumIpAddressCharacterLength, true, this.Buffer, offset + num);
			num += ExBitConverter.Write(string.IsNullOrEmpty(application) ? "<none>" : application, 32, false, this.Buffer, offset + num);
			num += ExBitConverter.Write(string.IsNullOrEmpty(activityId) ? "<none>" : activityId, 32, false, this.Buffer, offset + num);
			num += ExBitConverter.Write(string.IsNullOrEmpty(component) ? "<none>" : component, 32, false, this.Buffer, offset + num);
			num += ExBitConverter.Write(string.IsNullOrEmpty(protocol) ? "<none>" : protocol, 32, false, this.Buffer, offset + num);
			num += ExBitConverter.Write(string.IsNullOrEmpty(action) ? "<none>" : action, 32, false, this.Buffer, offset + num);
			this.UpdateBytesWritten(offset, num);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002464 File Offset: 0x00000664
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ExMonLogger>(this);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000246C File Offset: 0x0000066C
		protected override void InternalDispose()
		{
			this.ReleaseBuffer();
			base.InternalDispose();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000247A File Offset: 0x0000067A
		protected void ReleaseBuffer()
		{
			if (this.buffer != null)
			{
				ExMonLogger.bufferPool.Release(this.buffer);
				this.buffer = null;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000249B File Offset: 0x0000069B
		protected void UpdateBytesWritten(int offset, int bytesWritten)
		{
			if (this.bytesWrittenToBuffer < offset + bytesWritten)
			{
				this.bytesWrittenToBuffer = offset + bytesWritten;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024B1 File Offset: 0x000006B1
		protected void GetNewInstanceId()
		{
			this.instanceInfo = this.hookableCreateInstance.Value();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024C9 File Offset: 0x000006C9
		protected uint Submit()
		{
			this.header.Config(this.bytesWrittenToBuffer);
			return this.hookableTraceInstance.Value(this.Buffer, ref this.instanceInfo, ref this.instanceInfo);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024FE File Offset: 0x000006FE
		private void Clear()
		{
			Array.Clear(this.Buffer, 0, this.bytesWrittenToBuffer);
		}

		// Token: 0x04000001 RID: 1
		private const int MaximumUserNameCharacterLength = 32;

		// Token: 0x04000002 RID: 2
		private const int MaximumApplicationNameCharacterLength = 32;

		// Token: 0x04000003 RID: 3
		private const string MaximumIpAddressString = "ABCD:ABCD:ABCD:ABCD:ABCD:ABCD.XXX.XXX.XXX.XXX";

		// Token: 0x04000004 RID: 4
		private static readonly int MaximumIpAddressCharacterLength = "ABCD:ABCD:ABCD:ABCD:ABCD:ABCD.XXX.XXX.XXX.XXX".Length + 1;

		// Token: 0x04000005 RID: 5
		private static readonly BufferPool bufferPool = BufferPoolCollection.AutoCleanupCollection.Acquire(BufferPoolCollection.BufferSize.Size1K);

		// Token: 0x04000006 RID: 6
		private readonly bool enableTestMode;

		// Token: 0x04000007 RID: 7
		private readonly string clientAddress;

		// Token: 0x04000008 RID: 8
		private readonly Hookable<ExMonLogger.CreateExmonRpcInstanceId> hookableCreateInstance;

		// Token: 0x04000009 RID: 9
		private readonly Hookable<ExMonLogger.ExmonRpcTraceEventInstance> hookableTraceInstance;

		// Token: 0x0400000A RID: 10
		private string serviceName;

		// Token: 0x0400000B RID: 11
		private string userName;

		// Token: 0x0400000C RID: 12
		private byte[] buffer;

		// Token: 0x0400000D RID: 13
		private int bytesWrittenToBuffer;

		// Token: 0x0400000E RID: 14
		private DiagnosticsNativeMethods.EventInstanceInfo instanceInfo;

		// Token: 0x0400000F RID: 15
		private ExMonLogger.EventInstanceHeader header;

		// Token: 0x02000003 RID: 3
		// (Invoke) Token: 0x06000021 RID: 33
		internal delegate DiagnosticsNativeMethods.EventInstanceInfo CreateExmonRpcInstanceId();

		// Token: 0x02000004 RID: 4
		// (Invoke) Token: 0x06000025 RID: 37
		internal delegate uint ExmonRpcTraceEventInstance(byte[] buffer, ref DiagnosticsNativeMethods.EventInstanceInfo instanceInfo, ref DiagnosticsNativeMethods.EventInstanceInfo parentInstanceInfo);

		// Token: 0x02000005 RID: 5
		internal struct JetThreadStats
		{
			// Token: 0x06000028 RID: 40 RVA: 0x00002512 File Offset: 0x00000712
			internal JetThreadStats(ExMonLogger exmonLogger, int baseOffset)
			{
				this.exmonLogger = exmonLogger;
				this.baseOffset = baseOffset;
			}

			// Token: 0x17000007 RID: 7
			// (set) Token: 0x06000029 RID: 41 RVA: 0x00002524 File Offset: 0x00000724
			public JET_THREADSTATS ThreadStats
			{
				set
				{
					this.exmonLogger.WriteUInt32(this.baseOffset, 32U);
					this.exmonLogger.WriteUInt32(4 + this.baseOffset, (uint)value.cPageReferenced);
					this.exmonLogger.WriteUInt32(8 + this.baseOffset, (uint)value.cPageRead);
					this.exmonLogger.WriteUInt32(12 + this.baseOffset, (uint)value.cPagePreread);
					this.exmonLogger.WriteUInt32(16 + this.baseOffset, (uint)value.cPageDirtied);
					this.exmonLogger.WriteUInt32(20 + this.baseOffset, (uint)value.cPageRedirtied);
					this.exmonLogger.WriteUInt32(24 + this.baseOffset, (uint)value.cLogRecord);
					this.exmonLogger.WriteUInt32(28 + this.baseOffset, (uint)value.cbLogRecord);
				}
			}

			// Token: 0x04000012 RID: 18
			internal const int StructSize = 32;

			// Token: 0x04000013 RID: 19
			private ExMonLogger exmonLogger;

			// Token: 0x04000014 RID: 20
			private int baseOffset;

			// Token: 0x02000006 RID: 6
			private enum Offsets
			{
				// Token: 0x04000016 RID: 22
				Size,
				// Token: 0x04000017 RID: 23
				PagesReferenced = 4,
				// Token: 0x04000018 RID: 24
				PagesRead = 8,
				// Token: 0x04000019 RID: 25
				PagesPreread = 12,
				// Token: 0x0400001A RID: 26
				PagesDirtied = 16,
				// Token: 0x0400001B RID: 27
				PagesRedirtied = 20,
				// Token: 0x0400001C RID: 28
				LogRecordsGenerated = 24,
				// Token: 0x0400001D RID: 29
				BytesLogged = 28,
				// Token: 0x0400001E RID: 30
				MaxOffset = 32
			}
		}

		// Token: 0x02000007 RID: 7
		internal struct EventInstanceHeader
		{
			// Token: 0x0600002A RID: 42 RVA: 0x000025FF File Offset: 0x000007FF
			internal EventInstanceHeader(ExMonLogger exmonLogger)
			{
				this.exmonLogger = exmonLogger;
			}

			// Token: 0x17000008 RID: 8
			// (set) Token: 0x0600002B RID: 43 RVA: 0x00002608 File Offset: 0x00000808
			internal ushort Size
			{
				set
				{
					this.exmonLogger.WriteUShort(0, value);
				}
			}

			// Token: 0x17000009 RID: 9
			// (set) Token: 0x0600002C RID: 44 RVA: 0x00002617 File Offset: 0x00000817
			internal ushort FieldTypeFlags
			{
				set
				{
					this.exmonLogger.WriteUShort(2, value);
				}
			}

			// Token: 0x1700000A RID: 10
			// (set) Token: 0x0600002D RID: 45 RVA: 0x00002626 File Offset: 0x00000826
			internal byte HeaderType
			{
				set
				{
					this.exmonLogger.WriteByte(2, value);
				}
			}

			// Token: 0x1700000B RID: 11
			// (set) Token: 0x0600002E RID: 46 RVA: 0x00002635 File Offset: 0x00000835
			internal byte MarkerFlags
			{
				set
				{
					this.exmonLogger.WriteByte(3, value);
				}
			}

			// Token: 0x1700000C RID: 12
			// (set) Token: 0x0600002F RID: 47 RVA: 0x00002644 File Offset: 0x00000844
			internal uint Version
			{
				set
				{
					this.exmonLogger.WriteUInt32(4, value);
				}
			}

			// Token: 0x1700000D RID: 13
			// (set) Token: 0x06000030 RID: 48 RVA: 0x00002653 File Offset: 0x00000853
			internal byte ClassType
			{
				set
				{
					this.exmonLogger.WriteByte(4, value);
				}
			}

			// Token: 0x1700000E RID: 14
			// (set) Token: 0x06000031 RID: 49 RVA: 0x00002662 File Offset: 0x00000862
			internal byte ClassLevel
			{
				set
				{
					this.exmonLogger.WriteByte(5, value);
				}
			}

			// Token: 0x1700000F RID: 15
			// (set) Token: 0x06000032 RID: 50 RVA: 0x00002671 File Offset: 0x00000871
			internal ushort ClassVersion
			{
				set
				{
					this.exmonLogger.WriteUShort(6, value);
				}
			}

			// Token: 0x17000010 RID: 16
			// (set) Token: 0x06000033 RID: 51 RVA: 0x00002680 File Offset: 0x00000880
			internal uint ThreadId
			{
				set
				{
					this.exmonLogger.WriteUInt32(8, value);
				}
			}

			// Token: 0x17000011 RID: 17
			// (set) Token: 0x06000034 RID: 52 RVA: 0x0000268F File Offset: 0x0000088F
			internal uint ProcessId
			{
				set
				{
					this.exmonLogger.WriteUInt32(12, value);
				}
			}

			// Token: 0x17000012 RID: 18
			// (set) Token: 0x06000035 RID: 53 RVA: 0x0000269F File Offset: 0x0000089F
			internal ulong TimeStamp
			{
				set
				{
					this.exmonLogger.WriteUInt64(16, value);
				}
			}

			// Token: 0x17000013 RID: 19
			// (set) Token: 0x06000036 RID: 54 RVA: 0x000026AF File Offset: 0x000008AF
			internal ulong RegHandle
			{
				set
				{
					this.exmonLogger.WriteUInt64(24, value);
				}
			}

			// Token: 0x17000014 RID: 20
			// (set) Token: 0x06000037 RID: 55 RVA: 0x000026BF File Offset: 0x000008BF
			internal ulong ParentRegHandle
			{
				set
				{
					this.exmonLogger.WriteUInt64(48, value);
				}
			}

			// Token: 0x17000015 RID: 21
			// (set) Token: 0x06000038 RID: 56 RVA: 0x000026CF File Offset: 0x000008CF
			internal uint InstanceId
			{
				set
				{
					this.exmonLogger.WriteUInt32(32, value);
				}
			}

			// Token: 0x17000016 RID: 22
			// (set) Token: 0x06000039 RID: 57 RVA: 0x000026DF File Offset: 0x000008DF
			internal uint ParentInstanceId
			{
				set
				{
					this.exmonLogger.WriteUInt32(36, value);
				}
			}

			// Token: 0x17000017 RID: 23
			// (set) Token: 0x0600003A RID: 58 RVA: 0x000026EF File Offset: 0x000008EF
			internal ulong ProcessorTime
			{
				set
				{
					this.exmonLogger.WriteUInt64(40, value);
				}
			}

			// Token: 0x17000018 RID: 24
			// (set) Token: 0x0600003B RID: 59 RVA: 0x000026FF File Offset: 0x000008FF
			internal uint KernelTime
			{
				set
				{
					this.exmonLogger.WriteUInt32(40, value);
				}
			}

			// Token: 0x17000019 RID: 25
			// (set) Token: 0x0600003C RID: 60 RVA: 0x0000270F File Offset: 0x0000090F
			internal uint UserTime
			{
				set
				{
					this.exmonLogger.WriteUInt32(44, value);
				}
			}

			// Token: 0x1700001A RID: 26
			// (set) Token: 0x0600003D RID: 61 RVA: 0x0000271F File Offset: 0x0000091F
			internal uint EventId
			{
				set
				{
					this.exmonLogger.WriteUInt32(40, value);
				}
			}

			// Token: 0x1700001B RID: 27
			// (set) Token: 0x0600003E RID: 62 RVA: 0x0000272F File Offset: 0x0000092F
			internal uint Flags
			{
				set
				{
					this.exmonLogger.WriteUInt32(44, value);
				}
			}

			// Token: 0x0600003F RID: 63 RVA: 0x0000273F File Offset: 0x0000093F
			internal void Config(int bytesWrittenToBuffer)
			{
				this.Flags = 131072U;
				this.ClassVersion = 2;
				this.Size = Convert.ToUInt16(bytesWrittenToBuffer);
			}

			// Token: 0x0400001F RID: 31
			internal const int StructSize = 56;

			// Token: 0x04000020 RID: 32
			internal const int HeaderClassTypeOffset = 4;

			// Token: 0x04000021 RID: 33
			private ExMonLogger exmonLogger;

			// Token: 0x02000008 RID: 8
			private enum Offsets
			{
				// Token: 0x04000023 RID: 35
				HeaderSize,
				// Token: 0x04000024 RID: 36
				HeaderFieldTypeFlags = 2,
				// Token: 0x04000025 RID: 37
				HeaderHeaderType = 2,
				// Token: 0x04000026 RID: 38
				HeaderMarkerFlags,
				// Token: 0x04000027 RID: 39
				HeaderVersion,
				// Token: 0x04000028 RID: 40
				HeaderClassType = 4,
				// Token: 0x04000029 RID: 41
				HeaderClassLevel,
				// Token: 0x0400002A RID: 42
				HeaderClassVersion,
				// Token: 0x0400002B RID: 43
				HeaderThreadId = 8,
				// Token: 0x0400002C RID: 44
				HeaderProcessId = 12,
				// Token: 0x0400002D RID: 45
				HeaderTimeStamp = 16,
				// Token: 0x0400002E RID: 46
				HeaderRegHandle = 24,
				// Token: 0x0400002F RID: 47
				HeaderInstanceId = 32,
				// Token: 0x04000030 RID: 48
				HeaderParentInstanceId = 36,
				// Token: 0x04000031 RID: 49
				HeaderProcessorTime = 40,
				// Token: 0x04000032 RID: 50
				HeaderKernelTime = 40,
				// Token: 0x04000033 RID: 51
				HeaderUserTime = 44,
				// Token: 0x04000034 RID: 52
				HeaderEventId = 40,
				// Token: 0x04000035 RID: 53
				HeaderFlags = 44,
				// Token: 0x04000036 RID: 54
				HeaderParentRegHandle = 48,
				// Token: 0x04000037 RID: 55
				MaxOffset = 56
			}
		}
	}
}
