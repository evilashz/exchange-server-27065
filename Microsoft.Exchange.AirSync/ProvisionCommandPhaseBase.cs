using System;
using System.Xml;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000109 RID: 265
	internal abstract class ProvisionCommandPhaseBase
	{
		// Token: 0x06000E66 RID: 3686 RVA: 0x00050180 File Offset: 0x0004E380
		private static XmlNamespaceManager CreateNamespaceManager()
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(new NameTable());
			xmlNamespaceManager.AddNamespace("p", "Provision:");
			xmlNamespaceManager.AddNamespace("s", "Settings:");
			return xmlNamespaceManager;
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x000501B9 File Offset: 0x0004E3B9
		internal static bool IsValidPolicyType(string requestedPolicyType)
		{
			return string.Equals(requestedPolicyType, "MS-WAP-Provisioning-XML", StringComparison.OrdinalIgnoreCase) || string.Equals(requestedPolicyType, "MS-EAS-Provisioning-WBXML", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x000501D7 File Offset: 0x0004E3D7
		public ProvisionCommandPhaseBase(IProvisionCommandHost owningCommand)
		{
			if (owningCommand == null)
			{
				throw new ArgumentNullException("owningCommand");
			}
			this.owningCommand = owningCommand;
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x000501F4 File Offset: 0x0004E3F4
		protected XmlNode XmlRequest
		{
			get
			{
				return this.owningCommand.XmlRequest;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x00050201 File Offset: 0x0004E401
		protected XmlDocument XmlResponse
		{
			get
			{
				return this.owningCommand.XmlResponse;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x0005020E File Offset: 0x0004E40E
		protected IGlobalInfo GlobalInfo
		{
			get
			{
				return this.owningCommand.GlobalInfo;
			}
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x0005021C File Offset: 0x0004E41C
		internal static ProvisionCommandPhaseBase.ProvisionPhase DetermineCallPhase(XmlNode requestNode)
		{
			XmlNode xmlNode = requestNode.SelectSingleNode("/p:Provision/p:Policies/p:Policy", ProvisionCommandPhaseBase.namespaceManager);
			if (xmlNode != null)
			{
				XmlNode xmlNode2 = xmlNode["PolicyKey", "Provision:"];
				XmlNode xmlNode3 = xmlNode["Status", "Provision:"];
				if (xmlNode2 != null || xmlNode3 != null)
				{
					return ProvisionCommandPhaseBase.ProvisionPhase.PhaseTwo;
				}
			}
			xmlNode = requestNode.SelectSingleNode("/p:Provision/p:RemoteWipe/p:Status", ProvisionCommandPhaseBase.namespaceManager);
			if (xmlNode != null)
			{
				return ProvisionCommandPhaseBase.ProvisionPhase.PhaseTwo;
			}
			return ProvisionCommandPhaseBase.ProvisionPhase.PhaseOne;
		}

		// Token: 0x06000E6D RID: 3693
		internal abstract void Process(XmlNode provisionResponseNode);

		// Token: 0x06000E6E RID: 3694 RVA: 0x00050280 File Offset: 0x0004E480
		protected void GenerateRemoteWipeResponse(XmlNode provisionResponseNode, ProvisionCommand.ProvisionStatusCode statusCode)
		{
			if (provisionResponseNode["Status", "Provision:"] == null)
			{
				XmlNode xmlNode = this.owningCommand.XmlResponse.CreateElement("Status", "Provision:");
				XmlNode xmlNode2 = xmlNode;
				int num = (int)statusCode;
				xmlNode2.InnerText = num.ToString();
				provisionResponseNode.AppendChild(xmlNode);
			}
			XmlNode newChild = this.owningCommand.XmlResponse.CreateElement("RemoteWipe", "Provision:");
			provisionResponseNode.AppendChild(newChild);
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x000502F8 File Offset: 0x0004E4F8
		protected bool RemoteWipeRequired
		{
			get
			{
				ExDateTime? remoteWipeRequestedTime = this.owningCommand.GlobalInfo.RemoteWipeRequestedTime;
				if (remoteWipeRequestedTime != null)
				{
					ExDateTime? remoteWipeAckTime = this.owningCommand.GlobalInfo.RemoteWipeAckTime;
					return remoteWipeAckTime == null || !(remoteWipeAckTime.Value >= remoteWipeRequestedTime.Value);
				}
				return false;
			}
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00050354 File Offset: 0x0004E554
		protected bool GenerateRemoteWipeIfNeeded(XmlNode provisionResponseNode)
		{
			if (this.RemoteWipeRequired)
			{
				ExTraceGlobals.RequestsTracer.TraceDebug<ExDateTime?>((long)this.GetHashCode(), "[ProvisionCommandPhaseBase.GenerateRemoteWipeIfNeeded] WipeRequestTime '{0}' indicates we need to return a remote wipe response.", this.owningCommand.GlobalInfo.RemoteWipeRequestedTime);
				ExDateTime? remoteWipeSentTime = this.owningCommand.GlobalInfo.RemoteWipeSentTime;
				if (remoteWipeSentTime == null || remoteWipeSentTime.Value < this.owningCommand.GlobalInfo.RemoteWipeRequestedTime.Value)
				{
					this.owningCommand.GlobalInfo.RemoteWipeSentTime = new ExDateTime?(ExDateTime.UtcNow);
					this.owningCommand.ResetMobileServiceSelector();
				}
				this.GenerateRemoteWipeResponse(provisionResponseNode, ProvisionCommand.ProvisionStatusCode.Success);
				return true;
			}
			return false;
		}

		// Token: 0x040009BE RID: 2494
		internal static XmlNamespaceManager namespaceManager = ProvisionCommandPhaseBase.CreateNamespaceManager();

		// Token: 0x040009BF RID: 2495
		protected IProvisionCommandHost owningCommand;

		// Token: 0x0200010A RID: 266
		internal enum ProvisionPhase
		{
			// Token: 0x040009C1 RID: 2497
			Unknown,
			// Token: 0x040009C2 RID: 2498
			PhaseOne,
			// Token: 0x040009C3 RID: 2499
			PhaseTwo
		}
	}
}
