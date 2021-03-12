using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000032 RID: 50
	internal class NetworkPath
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00006C41 File Offset: 0x00004E41
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00006C49 File Offset: 0x00004E49
		public DagNetRoute[] AlternateRoutes { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00006C52 File Offset: 0x00004E52
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00006C5A File Offset: 0x00004E5A
		public bool IgnoreMutualAuth { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00006C63 File Offset: 0x00004E63
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00006C6B File Offset: 0x00004E6B
		public bool UseNullSpn { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00006C74 File Offset: 0x00004E74
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00006C7C File Offset: 0x00004E7C
		public bool UseSocketStream { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00006C85 File Offset: 0x00004E85
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00006C8D File Offset: 0x00004E8D
		public ISimpleBufferPool SocketStreamBufferPool { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00006C96 File Offset: 0x00004E96
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00006C9E File Offset: 0x00004E9E
		public IPool<SocketStreamAsyncArgs> SocketStreamAsyncArgPool { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00006CA7 File Offset: 0x00004EA7
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00006CAF File Offset: 0x00004EAF
		public SocketStream.ISocketStreamPerfCounters SocketStreamPerfCounters { get; set; }

		// Token: 0x0600018F RID: 399 RVA: 0x00006CB8 File Offset: 0x00004EB8
		internal NetworkPath(string targetNodeName, IPAddress targetAddr, int targetPort, IPAddress sourceAddr)
		{
			this.m_targetNodeName = targetNodeName;
			this.m_targetEndPoint = new IPEndPoint(targetAddr, targetPort);
			if (sourceAddr != null)
			{
				this.m_sourceEndPoint = new IPEndPoint(sourceAddr, 0);
			}
			NetworkPath.Tracer.TraceDebug(0L, "NetworkPath for {0} mapped to {1}:{2} from {3}", new object[]
			{
				targetNodeName,
				targetAddr,
				targetPort,
				(sourceAddr == null) ? "default" : sourceAddr.ToString()
			});
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00006D37 File Offset: 0x00004F37
		public string TargetNodeName
		{
			get
			{
				return this.m_targetNodeName;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00006D3F File Offset: 0x00004F3F
		public IPEndPoint TargetEndPoint
		{
			get
			{
				return this.m_targetEndPoint;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00006D47 File Offset: 0x00004F47
		public IPEndPoint SourceEndPoint
		{
			get
			{
				return this.m_sourceEndPoint;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00006D4F File Offset: 0x00004F4F
		// (set) Token: 0x06000194 RID: 404 RVA: 0x00006D57 File Offset: 0x00004F57
		public bool CrossSubnet
		{
			get
			{
				return this.m_isCrossSubnet;
			}
			set
			{
				this.m_isCrossSubnet = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00006D60 File Offset: 0x00004F60
		// (set) Token: 0x06000196 RID: 406 RVA: 0x00006D68 File Offset: 0x00004F68
		public bool Compress
		{
			get
			{
				return this.m_compression;
			}
			set
			{
				this.m_compression = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00006D71 File Offset: 0x00004F71
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00006D79 File Offset: 0x00004F79
		public bool Encrypt
		{
			get
			{
				return this.m_encryption;
			}
			set
			{
				this.m_encryption = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00006D82 File Offset: 0x00004F82
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00006D8A File Offset: 0x00004F8A
		public bool NetworkChoiceIsMandatory
		{
			get
			{
				return this.m_networkChoiceIsMandatory;
			}
			set
			{
				this.m_networkChoiceIsMandatory = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00006D93 File Offset: 0x00004F93
		// (set) Token: 0x0600019C RID: 412 RVA: 0x00006D9B File Offset: 0x00004F9B
		public NetworkPath.ConnectionPurpose Purpose
		{
			get
			{
				return this.m_connectionPurpose;
			}
			set
			{
				this.m_connectionPurpose = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00006DA4 File Offset: 0x00004FA4
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00006DAC File Offset: 0x00004FAC
		internal string NetworkName
		{
			get
			{
				return this.m_networkName;
			}
			set
			{
				this.m_networkName = value;
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00006DB5 File Offset: 0x00004FB5
		public bool HasSourceEndpoint()
		{
			return this.m_sourceEndPoint != null;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00006DC4 File Offset: 0x00004FC4
		public void ApplyNetworkPolicy(DagNetConfig dagConfig)
		{
			this.Encrypt = false;
			switch (dagConfig.NetworkEncryption)
			{
			case NetworkOption.Disabled:
				this.Encrypt = false;
				break;
			case NetworkOption.Enabled:
				this.Encrypt = true;
				break;
			case NetworkOption.InterSubnetOnly:
				this.Encrypt = this.CrossSubnet;
				break;
			case NetworkOption.SeedOnly:
				this.Encrypt = (this.Purpose == NetworkPath.ConnectionPurpose.Seeding);
				break;
			}
			this.Compress = false;
			if (!Parameters.CurrentValues.LogShipCompressionDisable)
			{
				switch (dagConfig.NetworkCompression)
				{
				case NetworkOption.Disabled:
					this.Compress = false;
					return;
				case NetworkOption.Enabled:
					this.Compress = true;
					return;
				case NetworkOption.InterSubnetOnly:
					this.Compress = this.CrossSubnet;
					return;
				case NetworkOption.SeedOnly:
					this.Compress = (this.Purpose == NetworkPath.ConnectionPurpose.Seeding);
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x040000FB RID: 251
		private static readonly Trace Tracer = ExTraceGlobals.NetPathTracer;

		// Token: 0x040000FC RID: 252
		private IPEndPoint m_targetEndPoint;

		// Token: 0x040000FD RID: 253
		private IPEndPoint m_sourceEndPoint;

		// Token: 0x040000FE RID: 254
		private readonly string m_targetNodeName;

		// Token: 0x040000FF RID: 255
		private bool m_isCrossSubnet = true;

		// Token: 0x04000100 RID: 256
		private bool m_encryption;

		// Token: 0x04000101 RID: 257
		private bool m_compression;

		// Token: 0x04000102 RID: 258
		private string m_networkName;

		// Token: 0x04000103 RID: 259
		private bool m_networkChoiceIsMandatory;

		// Token: 0x04000104 RID: 260
		private NetworkPath.ConnectionPurpose m_connectionPurpose;

		// Token: 0x02000033 RID: 51
		public enum ConnectionPurpose
		{
			// Token: 0x0400010D RID: 269
			General,
			// Token: 0x0400010E RID: 270
			TestHealth,
			// Token: 0x0400010F RID: 271
			Seeding,
			// Token: 0x04000110 RID: 272
			LogCopy
		}
	}
}
