using System;
using System.Xml;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000056 RID: 86
	internal class ChangeTrackingNode
	{
		// Token: 0x060004B4 RID: 1204 RVA: 0x0001D399 File Offset: 0x0001B599
		internal ChangeTrackingNode(string namespaceUri, string name)
		{
			this.qualifiedName = namespaceUri + name;
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0001D3AE File Offset: 0x0001B5AE
		internal static ChangeTrackingNode AllOtherNodes
		{
			get
			{
				return ChangeTrackingNode.allOtherNodes;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0001D3B5 File Offset: 0x0001B5B5
		internal static ChangeTrackingNode AllNodes
		{
			get
			{
				return ChangeTrackingNode.allNodes;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0001D3BC File Offset: 0x0001B5BC
		internal string QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001D3C4 File Offset: 0x0001B5C4
		internal static string GetQualifiedName(XmlNode node)
		{
			return node.NamespaceURI + node.Name;
		}

		// Token: 0x04000395 RID: 917
		private static ChangeTrackingNode allOtherNodes = new ChangeTrackingNode("AirSync:", "ApplicationData");

		// Token: 0x04000396 RID: 918
		private static ChangeTrackingNode allNodes = ChangeTrackingNode.AllOtherNodes;

		// Token: 0x04000397 RID: 919
		private string qualifiedName;
	}
}
