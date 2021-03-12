using System;
using System.Collections;
using System.Net;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.Background
{
	// Token: 0x0200004D RID: 77
	internal class ProxyTest
	{
		// Token: 0x0600024D RID: 589 RVA: 0x0000F9D8 File Offset: 0x0000DBD8
		public override string ToString()
		{
			return this.proxyChainConfig[this.proxyChainConfig.Length - 2].Endpoint.ToString();
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000F9F5 File Offset: 0x0000DBF5
		public ProxyTest(StsWorkItem.EndOPDetectionCallback endOPDetectionCallback, IEnumerator proxyEnumerator)
		{
			this.endOPDetectionCallback = endOPDetectionCallback;
			this.detectionResult = OPDetectionResult.Unknown;
			this.m_proxyEnumerator = proxyEnumerator;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000FA14 File Offset: 0x0000DC14
		public OPDetectionResult BeginOPDetection(ProxyEndPoint[] path, ProxyType lastType, NetworkCredential lastAuth, IPEndPoint hostEndpoint, IPAddress target)
		{
			this.sender = target;
			this.proxyChainConfig = new ProxyEndPoint[(path == null) ? 2 : (path.Length + 2)];
			if (path != null)
			{
				for (int i = 0; i < path.Length; i++)
				{
					this.proxyChainConfig[i] = path[i];
				}
			}
			this.proxyChainConfig[this.proxyChainConfig.Length - 2] = new ProxyEndPoint(target, 0, lastType, lastAuth);
			this.proxyChainConfig[this.proxyChainConfig.Length - 1] = new ProxyEndPoint(hostEndpoint, ProxyType.None, new NetworkCredential());
			return this.StartNextDetection();
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000FA9C File Offset: 0x0000DC9C
		public void DetectionChainResult(OPDetectionResult result, ProxyType type, int port)
		{
			switch (result)
			{
			case OPDetectionResult.IsOpenProxy:
				this.positiveProxyType = type;
				this.positivePort = port;
				this.detectionResult = result;
				break;
			case OPDetectionResult.NotOpenProxy:
				if (this.detectionResult != OPDetectionResult.IsOpenProxy)
				{
					this.detectionResult = result;
				}
				break;
			}
			this.StartNextDetection();
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000FAF0 File Offset: 0x0000DCF0
		private void DetectOpenProxy(ushort port, ProxyType targetType)
		{
			this.proxyChainConfig[this.proxyChainConfig.Length - 2].Endpoint.Port = (int)port;
			this.proxyChainConfig[this.proxyChainConfig.Length - 1].Type = targetType;
			this.chain = new ProxyChain(this.proxyChainConfig, this, "220 ");
			this.chain.DetectOpenProxy(10000);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000FB58 File Offset: 0x0000DD58
		private OPDetectionResult StartNextDetection()
		{
			if (this.detectionResult != OPDetectionResult.IsOpenProxy && !ProtocolAnalysisBgAgent.ShutDown)
			{
				while (this.m_portEnumerator == null || !this.m_portEnumerator.MoveNext())
				{
					this.m_portEnumerator = null;
					if (!this.m_proxyEnumerator.MoveNext())
					{
						goto IL_A7;
					}
					this.proxyType = (ProxyType)this.m_proxyEnumerator.Current;
					this.portList = ProtocolAnalysisBgAgent.GetProxyPortList(this.proxyType);
					this.m_portEnumerator = this.portList.GetEnumerator();
					this.m_portEnumerator.Reset();
				}
				ushort port = (ushort)this.m_portEnumerator.Current;
				this.DetectOpenProxy(port, this.proxyType);
				return OPDetectionResult.Pending;
			}
			IL_A7:
			string message = (this.detectionResult == OPDetectionResult.IsOpenProxy) ? (this.positiveProxyType.ToString() + ":" + this.positivePort) : string.Empty;
			this.endOPDetectionCallback(this.detectionResult, this.positiveProxyType, message, this.sender);
			return this.detectionResult;
		}

		// Token: 0x040001A6 RID: 422
		private OPDetectionResult detectionResult;

		// Token: 0x040001A7 RID: 423
		private ProxyType positiveProxyType;

		// Token: 0x040001A8 RID: 424
		private int positivePort;

		// Token: 0x040001A9 RID: 425
		private ProxyEndPoint[] proxyChainConfig;

		// Token: 0x040001AA RID: 426
		private ProxyChain chain;

		// Token: 0x040001AB RID: 427
		private ushort[] portList;

		// Token: 0x040001AC RID: 428
		private IEnumerator m_portEnumerator;

		// Token: 0x040001AD RID: 429
		private IEnumerator m_proxyEnumerator;

		// Token: 0x040001AE RID: 430
		private ProxyType proxyType;

		// Token: 0x040001AF RID: 431
		private IPAddress sender;

		// Token: 0x040001B0 RID: 432
		private StsWorkItem.EndOPDetectionCallback endOPDetectionCallback;
	}
}
