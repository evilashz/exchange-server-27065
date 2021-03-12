using System;
using System.Xml;

namespace Microsoft.Exchange.Net.SharePoint
{
	// Token: 0x0200092D RID: 2349
	public class SharepointFileInfo
	{
		// Token: 0x06003258 RID: 12888 RVA: 0x0007BD02 File Offset: 0x00079F02
		private SharepointFileInfo()
		{
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x06003259 RID: 12889 RVA: 0x0007BD0A File Offset: 0x00079F0A
		// (set) Token: 0x0600325A RID: 12890 RVA: 0x0007BD12 File Offset: 0x00079F12
		public string IsFolderRaw { get; private set; }

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x0600325B RID: 12891 RVA: 0x0007BD1B File Offset: 0x00079F1B
		// (set) Token: 0x0600325C RID: 12892 RVA: 0x0007BD23 File Offset: 0x00079F23
		public string IsCollectionRaw { get; private set; }

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x0600325D RID: 12893 RVA: 0x0007BD2C File Offset: 0x00079F2C
		// (set) Token: 0x0600325E RID: 12894 RVA: 0x0007BD34 File Offset: 0x00079F34
		public string IsHiddenRaw { get; private set; }

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x0600325F RID: 12895 RVA: 0x0007BD3D File Offset: 0x00079F3D
		public bool IsFolder
		{
			get
			{
				return "t".Equals(this.IsFolderRaw, StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x06003260 RID: 12896 RVA: 0x0007BD50 File Offset: 0x00079F50
		public bool IsCollection
		{
			get
			{
				return "1".Equals(this.IsCollectionRaw, StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x06003261 RID: 12897 RVA: 0x0007BD63 File Offset: 0x00079F63
		public bool IsHidden
		{
			get
			{
				return "0".Equals(this.IsHiddenRaw, StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x06003262 RID: 12898 RVA: 0x0007BD76 File Offset: 0x00079F76
		// (set) Token: 0x06003263 RID: 12899 RVA: 0x0007BD7E File Offset: 0x00079F7E
		public string DisplayName { get; private set; }

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x06003264 RID: 12900 RVA: 0x0007BD87 File Offset: 0x00079F87
		// (set) Token: 0x06003265 RID: 12901 RVA: 0x0007BD8F File Offset: 0x00079F8F
		public string LastModifiedRaw { get; private set; }

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x06003266 RID: 12902 RVA: 0x0007BD98 File Offset: 0x00079F98
		// (set) Token: 0x06003267 RID: 12903 RVA: 0x0007BDA0 File Offset: 0x00079FA0
		public string CreationDateRaw { get; private set; }

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x06003268 RID: 12904 RVA: 0x0007BDA9 File Offset: 0x00079FA9
		// (set) Token: 0x06003269 RID: 12905 RVA: 0x0007BDB1 File Offset: 0x00079FB1
		public string Href { get; private set; }

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x0600326A RID: 12906 RVA: 0x0007BDBA File Offset: 0x00079FBA
		// (set) Token: 0x0600326B RID: 12907 RVA: 0x0007BDC2 File Offset: 0x00079FC2
		public string Status { get; private set; }

		// Token: 0x0600326C RID: 12908 RVA: 0x0007BDCC File Offset: 0x00079FCC
		public static SharepointFileInfo ParseNode(XmlNode node, XmlNamespaceManager nsmgr)
		{
			SharepointFileInfo sharepointFileInfo = new SharepointFileInfo();
			XmlNode xmlNode = node.SelectSingleNode("D:href", nsmgr);
			if (xmlNode != null)
			{
				sharepointFileInfo.Href = xmlNode.InnerText.Trim();
			}
			XmlNode xmlNode2 = node.SelectSingleNode("D:propstat/D:prop", nsmgr);
			if (xmlNode2 == null)
			{
				return sharepointFileInfo;
			}
			XmlNode xmlNode3 = xmlNode2.SelectSingleNode("D:displayname", nsmgr);
			if (xmlNode3 != null)
			{
				sharepointFileInfo.DisplayName = xmlNode3.InnerText.Trim();
			}
			xmlNode3 = xmlNode2.SelectSingleNode("D:isFolder", nsmgr);
			if (xmlNode3 != null)
			{
				sharepointFileInfo.IsFolderRaw = xmlNode3.InnerText;
			}
			xmlNode3 = xmlNode2.SelectSingleNode("D:iscollection", nsmgr);
			if (xmlNode3 != null)
			{
				sharepointFileInfo.IsCollectionRaw = xmlNode3.InnerText;
			}
			xmlNode3 = xmlNode2.SelectSingleNode("D:ishidden", nsmgr);
			if (xmlNode3 != null)
			{
				sharepointFileInfo.IsHiddenRaw = xmlNode3.InnerText;
			}
			xmlNode3 = xmlNode2.SelectSingleNode("D:getlastmodified", nsmgr);
			if (xmlNode3 != null)
			{
				sharepointFileInfo.LastModifiedRaw = xmlNode3.InnerText;
			}
			xmlNode3 = xmlNode2.SelectSingleNode("D:creationdate", nsmgr);
			if (xmlNode3 != null)
			{
				sharepointFileInfo.CreationDateRaw = xmlNode3.InnerText;
			}
			return sharepointFileInfo;
		}
	}
}
