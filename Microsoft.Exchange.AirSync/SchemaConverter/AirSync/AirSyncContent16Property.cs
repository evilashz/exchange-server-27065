using System;
using System.Xml;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x02000145 RID: 325
	internal class AirSyncContent16Property : AirSyncContent14Property, IContent16Property, IContent14Property, IContentProperty, IMIMEDataProperty, IMIMERelatedProperty, IProperty
	{
		// Token: 0x06000FC0 RID: 4032 RVA: 0x00059992 File Offset: 0x00057B92
		public AirSyncContent16Property(string xmlNodeNamespace, string airSyncTagName, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncTagName, requiresClientSupport)
		{
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x000599A0 File Offset: 0x00057BA0
		public string BodyString
		{
			get
			{
				XmlNode xmlNode = base.XmlNode.SelectSingleNode("X:Data", base.NamespaceManager);
				if (xmlNode == null)
				{
					return null;
				}
				return xmlNode.InnerText;
			}
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x000599D0 File Offset: 0x00057BD0
		protected override bool ClientAccepts(IContentProperty srcProperty, BodyType type, out long estimatedDataSize, out long truncationSize)
		{
			IContent16Property content16Property = srcProperty as IContent16Property;
			if (content16Property == null)
			{
				throw new UnexpectedTypeException("IContent16Property", content16Property);
			}
			estimatedDataSize = 0L;
			bool flag;
			if (!base.IsAcceptable(type, out truncationSize, out flag))
			{
				return false;
			}
			this.dataString = content16Property.BodyString;
			estimatedDataSize = (long)this.dataString.Length;
			if (truncationSize < 0L || estimatedDataSize <= truncationSize)
			{
				base.Truncated = false;
			}
			else
			{
				if (flag)
				{
					this.dataString = null;
					return false;
				}
				this.dataString = this.dataString.Remove((int)truncationSize);
				base.Truncated = true;
			}
			return true;
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x00059A60 File Offset: 0x00057C60
		protected override void CopyData()
		{
			if (!string.IsNullOrEmpty(this.dataString))
			{
				base.AppendChildNode(base.XmlNode, "Data", this.dataString);
				return;
			}
			base.CopyData();
		}

		// Token: 0x04000A6F RID: 2671
		private string dataString;
	}
}
