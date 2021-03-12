using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.Cluster
{
	// Token: 0x02000131 RID: 305
	[Serializable]
	internal sealed class RpcSeederStatus
	{
		// Token: 0x06000856 RID: 2134 RVA: 0x00008358 File Offset: 0x00007758
		public RpcSeederStatus(RpcSeederStatus other)
		{
			this.m_bytesRead = other.m_bytesRead;
			this.m_bytesWritten = other.m_bytesWritten;
			this.m_bytesTotal = other.m_bytesTotal;
			this.m_bytesTotalDivisor = other.m_bytesTotalDivisor;
			this.m_state = other.m_state;
			RpcErrorExceptionInfo errorInfo = other.m_errorInfo;
			this.m_errorInfo = new RpcErrorExceptionInfo(errorInfo);
			base..ctor();
			if (other.m_fileFullPath != null)
			{
				string fileFullPath = other.m_fileFullPath;
				string text = fileFullPath;
				string text2 = fileFullPath;
				this.m_fileFullPath = new string(text2.ToCharArray(), 0, text.Length);
			}
			if (other.m_addressForData != null)
			{
				string addressForData = other.m_addressForData;
				string text3 = addressForData;
				string text4 = addressForData;
				this.m_addressForData = new string(text4.ToCharArray(), 0, text3.Length);
			}
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x000075E0 File Offset: 0x000069E0
		public RpcSeederStatus()
		{
			this.m_bytesRead = 0L;
			this.m_bytesWritten = 0L;
			this.m_bytesTotal = 0L;
			this.m_fileFullPath = string.Empty;
			this.m_addressForData = string.Empty;
			this.m_state = SeederState.Unknown;
			this.m_errorInfo = new RpcErrorExceptionInfo();
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x00007634 File Offset: 0x00006A34
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x00007648 File Offset: 0x00006A48
		public long BytesRead
		{
			get
			{
				return this.m_bytesRead;
			}
			set
			{
				this.m_bytesRead = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x0000765C File Offset: 0x00006A5C
		// (set) Token: 0x0600085B RID: 2139 RVA: 0x00007670 File Offset: 0x00006A70
		public long BytesWritten
		{
			get
			{
				return this.m_bytesWritten;
			}
			set
			{
				this.m_bytesWritten = value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x00007684 File Offset: 0x00006A84
		public long BytesRemaining
		{
			get
			{
				return Math.Max(this.m_bytesTotal - this.m_bytesRead, 0L);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x000076A8 File Offset: 0x00006AA8
		// (set) Token: 0x0600085E RID: 2142 RVA: 0x000076BC File Offset: 0x00006ABC
		public long BytesTotal
		{
			get
			{
				return this.m_bytesTotal;
			}
			set
			{
				this.m_bytesTotal = value;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x000076D0 File Offset: 0x00006AD0
		// (set) Token: 0x06000860 RID: 2144 RVA: 0x000076E4 File Offset: 0x00006AE4
		public long BytesTotalDivisor
		{
			get
			{
				return this.m_bytesTotalDivisor;
			}
			set
			{
				this.m_bytesTotalDivisor = value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x000076F8 File Offset: 0x00006AF8
		public int PercentComplete
		{
			get
			{
				long bytesTotal = this.m_bytesTotal;
				if (bytesTotal == 0L)
				{
					return 0;
				}
				long bytesTotalDivisor = this.m_bytesTotalDivisor;
				long num = (bytesTotalDivisor == 0L) ? bytesTotal : bytesTotalDivisor;
				return (int)(this.m_bytesWritten * 100L / num);
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x00007730 File Offset: 0x00006B30
		// (set) Token: 0x06000863 RID: 2147 RVA: 0x00007744 File Offset: 0x00006B44
		public string FileFullPath
		{
			get
			{
				return this.m_fileFullPath;
			}
			set
			{
				this.m_fileFullPath = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x00007758 File Offset: 0x00006B58
		// (set) Token: 0x06000865 RID: 2149 RVA: 0x0000776C File Offset: 0x00006B6C
		public string AddressForData
		{
			get
			{
				return this.m_addressForData;
			}
			set
			{
				this.m_addressForData = value;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x00007780 File Offset: 0x00006B80
		// (set) Token: 0x06000867 RID: 2151 RVA: 0x00007794 File Offset: 0x00006B94
		public SeederState State
		{
			get
			{
				return this.m_state;
			}
			set
			{
				this.m_state = value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x000077A8 File Offset: 0x00006BA8
		// (set) Token: 0x06000869 RID: 2153 RVA: 0x000077BC File Offset: 0x00006BBC
		public RpcErrorExceptionInfo ErrorInfo
		{
			get
			{
				return this.m_errorInfo;
			}
			set
			{
				this.m_errorInfo = value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x000077D0 File Offset: 0x00006BD0
		// (set) Token: 0x0600086B RID: 2155 RVA: 0x000077E4 File Offset: 0x00006BE4
		public uint FileNumber
		{
			get
			{
				return this.m_fileNumber;
			}
			set
			{
				this.m_fileNumber = value;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x000077F8 File Offset: 0x00006BF8
		// (set) Token: 0x0600086D RID: 2157 RVA: 0x0000780C File Offset: 0x00006C0C
		public uint FileCount
		{
			get
			{
				return this.m_fileCount;
			}
			set
			{
				this.m_fileCount = value;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x00007820 File Offset: 0x00006C20
		// (set) Token: 0x0600086F RID: 2159 RVA: 0x00007834 File Offset: 0x00006C34
		public long BytesTotalDirectory
		{
			get
			{
				return this.m_bytesTotalDirectory;
			}
			set
			{
				this.m_bytesTotalDirectory = value;
			}
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00007848 File Offset: 0x00006C48
		[return: MarshalAs(UnmanagedType.U1)]
		public bool IsSeederStatusDataAvailable()
		{
			return this.PercentComplete >= 0 && this.m_fileFullPath != null && this.m_state != SeederState.Unknown;
		}

		// Token: 0x04000A4E RID: 2638
		private long m_bytesRead;

		// Token: 0x04000A4F RID: 2639
		private long m_bytesWritten;

		// Token: 0x04000A50 RID: 2640
		private long m_bytesTotal;

		// Token: 0x04000A51 RID: 2641
		private long m_bytesTotalDivisor;

		// Token: 0x04000A52 RID: 2642
		private string m_fileFullPath;

		// Token: 0x04000A53 RID: 2643
		private string m_addressForData;

		// Token: 0x04000A54 RID: 2644
		private SeederState m_state;

		// Token: 0x04000A55 RID: 2645
		private RpcErrorExceptionInfo m_errorInfo;

		// Token: 0x04000A56 RID: 2646
		private uint m_fileNumber;

		// Token: 0x04000A57 RID: 2647
		private uint m_fileCount;

		// Token: 0x04000A58 RID: 2648
		private long m_bytesTotalDirectory;
	}
}
