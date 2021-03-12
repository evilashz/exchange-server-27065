using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x02000126 RID: 294
	internal class AirSync14AttachmentsProperty : AirSync12AttachmentsProperty
	{
		// Token: 0x06000F49 RID: 3913 RVA: 0x00057438 File Offset: 0x00055638
		public AirSync14AttachmentsProperty(string xmlNodeNamespace, string airSyncTagName, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncTagName, requiresClientSupport)
		{
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x00057664 File Offset: 0x00055864
		public override IEnumerator<Attachment12Data> GetEnumerator()
		{
			int nodeIndex = 0;
			foreach (Attachment12Data attachment12Data in this)
			{
				Attachment14Data attachment14 = (Attachment14Data)attachment12Data;
				XmlNodeList childNodes = base.XmlNode.ChildNodes;
				int i;
				nodeIndex = (i = nodeIndex) + 1;
				XmlNode attachmentNode = childNodes[i];
				foreach (object obj in attachmentNode)
				{
					XmlNode xmlNode = (XmlNode)obj;
					string name;
					if ((name = xmlNode.Name) != null)
					{
						if (!(name == "UmAttOrder"))
						{
							if (name == "UmAttDuration")
							{
								attachment14.Duration = Convert.ToInt32(xmlNode.InnerText);
							}
						}
						else
						{
							attachment14.Order = Convert.ToInt32(xmlNode.InnerText);
						}
					}
				}
				yield return attachment14;
			}
			yield break;
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x00057680 File Offset: 0x00055880
		protected override void CopyAttachment(XmlNode attachmentNode, Attachment12Data attachment)
		{
			base.CopyAttachment(attachmentNode, attachment);
			Attachment14Data attachment14Data = attachment as Attachment14Data;
			if (attachment14Data == null)
			{
				throw new UnexpectedTypeException("Attachment14Data", attachment);
			}
			if (attachment14Data.Order != -1)
			{
				XmlNode xmlNode = base.XmlParentNode.OwnerDocument.CreateElement("UmAttOrder", "Email2:");
				xmlNode.InnerText = attachment14Data.Order.ToString(CultureInfo.InvariantCulture);
				attachmentNode.AppendChild(xmlNode);
			}
			if (attachment14Data.Duration != -1)
			{
				XmlNode xmlNode2 = base.XmlParentNode.OwnerDocument.CreateElement("UmAttDuration", "Email2:");
				xmlNode2.InnerText = attachment14Data.Duration.ToString(CultureInfo.InvariantCulture);
				attachmentNode.AppendChild(xmlNode2);
			}
		}
	}
}
