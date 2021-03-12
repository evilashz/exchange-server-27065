using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200002F RID: 47
	internal class AmbiguousRecipientToResolve
	{
		// Token: 0x06000349 RID: 841 RVA: 0x00013588 File Offset: 0x00011788
		internal AmbiguousRecipientToResolve(string name)
		{
			this.Name = name;
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00013597 File Offset: 0x00011797
		// (set) Token: 0x0600034B RID: 843 RVA: 0x0001359F File Offset: 0x0001179F
		internal bool CompleteList { get; set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600034C RID: 844 RVA: 0x000135A8 File Offset: 0x000117A8
		// (set) Token: 0x0600034D RID: 845 RVA: 0x000135B0 File Offset: 0x000117B0
		internal bool ExactMatchFound { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600034E RID: 846 RVA: 0x000135B9 File Offset: 0x000117B9
		// (set) Token: 0x0600034F RID: 847 RVA: 0x000135C1 File Offset: 0x000117C1
		internal string Name { get; private set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000350 RID: 848 RVA: 0x000135CA File Offset: 0x000117CA
		// (set) Token: 0x06000351 RID: 849 RVA: 0x000135D2 File Offset: 0x000117D2
		internal int ResolvedNamesCount { get; set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000352 RID: 850 RVA: 0x000135DB File Offset: 0x000117DB
		// (set) Token: 0x06000353 RID: 851 RVA: 0x000135E3 File Offset: 0x000117E3
		internal List<ResolvedRecipient> ResolvedTo { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000354 RID: 852 RVA: 0x000135EC File Offset: 0x000117EC
		// (set) Token: 0x06000355 RID: 853 RVA: 0x000135F4 File Offset: 0x000117F4
		internal StatusCode Status { get; set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000356 RID: 854 RVA: 0x000135FD File Offset: 0x000117FD
		// (set) Token: 0x06000357 RID: 855 RVA: 0x00013605 File Offset: 0x00011805
		internal PictureOptions PictureOptions { get; set; }

		// Token: 0x06000358 RID: 856 RVA: 0x00013610 File Offset: 0x00011810
		internal void BuildXmlResponse(XmlDocument xmlResponse, XmlNode parentNode)
		{
			XmlNode xmlNode = xmlResponse.CreateElement("Response", "ResolveRecipients:");
			parentNode.AppendChild(xmlNode);
			XmlNode xmlNode2 = xmlResponse.CreateElement("To", "ResolveRecipients:");
			xmlNode2.InnerText = this.Name;
			xmlNode.AppendChild(xmlNode2);
			XmlNode xmlNode3 = xmlResponse.CreateElement("Status", "ResolveRecipients:");
			xmlNode3.InnerText = ((int)this.Status).ToString(CultureInfo.InvariantCulture);
			xmlNode.AppendChild(xmlNode3);
			if (this.Status != StatusCode.Sync_ProtocolError)
			{
				XmlNode xmlNode4 = xmlResponse.CreateElement("RecipientCount", "ResolveRecipients:");
				xmlNode4.InnerText = this.ResolvedNamesCount.ToString(CultureInfo.InvariantCulture);
				xmlNode.AppendChild(xmlNode4);
				int num = 0;
				foreach (ResolvedRecipient resolvedRecipient in this.ResolvedTo)
				{
					resolvedRecipient.PictureOptions = this.PictureOptions;
					bool flag;
					resolvedRecipient.BuildXmlResponse(xmlResponse, xmlNode, this.PictureOptions == null || num >= this.PictureOptions.MaxPictures, out flag);
					if (flag)
					{
						num++;
					}
				}
			}
		}
	}
}
