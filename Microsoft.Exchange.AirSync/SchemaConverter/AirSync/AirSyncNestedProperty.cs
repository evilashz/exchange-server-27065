using System;
using System.Collections;
using System.Xml;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x02000150 RID: 336
	[Serializable]
	internal abstract class AirSyncNestedProperty : AirSyncProperty, INestedProperty
	{
		// Token: 0x06000FE2 RID: 4066 RVA: 0x0005A4B0 File Offset: 0x000586B0
		public AirSyncNestedProperty(string xmlNodeNamespace, string airSyncTagName, INestedData nested, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncTagName, requiresClientSupport)
		{
			this.nestedData = nested;
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x0005A4C4 File Offset: 0x000586C4
		public INestedData NestedData
		{
			get
			{
				if (this.nestedData != null)
				{
					foreach (object obj in base.XmlNode.ChildNodes)
					{
						XmlNode xmlNode = (XmlNode)obj;
						this.nestedData.SubProperties[xmlNode.Name] = xmlNode.InnerText;
					}
				}
				return this.nestedData;
			}
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0005A548 File Offset: 0x00058748
		public override void Unbind()
		{
			base.Unbind();
			this.nestedData.Clear();
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0005A55C File Offset: 0x0005875C
		protected override void InternalCopyFrom(IProperty srcProperty)
		{
			INestedProperty nestedProperty = srcProperty as INestedProperty;
			if (nestedProperty == null)
			{
				throw new UnexpectedTypeException("INestedProperty", srcProperty);
			}
			if (PropertyState.Modified != srcProperty.State)
			{
				throw new ConversionException("Property only supports conversion from Modified property state");
			}
			INestedData nestedData = nestedProperty.NestedData;
			if (nestedData == null)
			{
				throw new ConversionException("nestedData is NULL");
			}
			base.XmlNode = base.XmlParentNode.OwnerDocument.CreateElement(base.AirSyncTagNames[0], base.Namespace);
			this.AppendToXmlNode(base.XmlNode, nestedData);
			base.XmlParentNode.AppendChild(base.XmlNode);
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0005A5EB File Offset: 0x000587EB
		protected void AppendToXmlNode(XmlNode parentNode, INestedData nestedData)
		{
			this.AppendToXmlNode(parentNode, nestedData, base.Namespace);
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0005A5FC File Offset: 0x000587FC
		protected void AppendToXmlNode(XmlNode parentNode, INestedData nestedData, string namespaceString)
		{
			IDictionaryEnumerator enumerator = nestedData.SubProperties.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Value != null && !string.IsNullOrEmpty(enumerator.Value as string))
				{
					XmlNode newChild = base.XmlParentNode.OwnerDocument.CreateTextNode(enumerator.Value.ToString());
					XmlNode xmlNode = base.XmlParentNode.OwnerDocument.CreateElement(enumerator.Key as string, namespaceString);
					xmlNode.AppendChild(newChild);
					parentNode.AppendChild(xmlNode);
				}
			}
		}

		// Token: 0x04000A7A RID: 2682
		private INestedData nestedData;
	}
}
