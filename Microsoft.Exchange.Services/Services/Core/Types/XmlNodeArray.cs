using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008C6 RID: 2246
	public class XmlNodeArray
	{
		// Token: 0x06003F8E RID: 16270 RVA: 0x000DBA58 File Offset: 0x000D9C58
		internal void Normalize()
		{
			for (int i = this.Nodes.Count - 1; i >= 0; i--)
			{
				XmlNode xmlNode = this.Nodes[i];
				switch (ServiceXml.GetNormalizationAction(xmlNode))
				{
				case ServiceXml.NormalizationAction.Normalize:
					ServiceXml.NormalizeXml(xmlNode);
					break;
				case ServiceXml.NormalizationAction.Remove:
					this.Nodes.Remove(xmlNode);
					break;
				}
			}
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06003F8F RID: 16271 RVA: 0x000DBABA File Offset: 0x000D9CBA
		// (set) Token: 0x06003F90 RID: 16272 RVA: 0x000DBAC2 File Offset: 0x000D9CC2
		[XmlAnyElement]
		public List<XmlNode> Nodes
		{
			get
			{
				return this.nodes;
			}
			set
			{
				this.nodes = value;
			}
		}

		// Token: 0x04002462 RID: 9314
		private List<XmlNode> nodes = new List<XmlNode>();
	}
}
