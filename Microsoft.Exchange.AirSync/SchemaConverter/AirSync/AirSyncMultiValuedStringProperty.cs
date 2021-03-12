using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x0200013A RID: 314
	[Serializable]
	internal class AirSyncMultiValuedStringProperty : AirSyncProperty, IMultivaluedProperty<string>, IProperty, IEnumerable<string>, IEnumerable
	{
		// Token: 0x06000F8A RID: 3978 RVA: 0x00058E01 File Offset: 0x00057001
		public AirSyncMultiValuedStringProperty(string xmlNodeNamespace, string airSyncTagName, string airSyncChildTagName, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncTagName, requiresClientSupport)
		{
			this.airSyncChildTagName = airSyncChildTagName;
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x00058E14 File Offset: 0x00057014
		public int Count
		{
			get
			{
				return base.XmlNode.ChildNodes.Count;
			}
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x00058F78 File Offset: 0x00057178
		public IEnumerator<string> GetEnumerator()
		{
			foreach (object obj in base.XmlNode.ChildNodes)
			{
				XmlNode childNode = (XmlNode)obj;
				yield return childNode.InnerText;
			}
			yield break;
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x00058F94 File Offset: 0x00057194
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x00058F9C File Offset: 0x0005719C
		protected override void InternalCopyFrom(IProperty srcProperty)
		{
			IMultivaluedProperty<string> multivaluedProperty = srcProperty as IMultivaluedProperty<string>;
			if (multivaluedProperty == null)
			{
				throw new UnexpectedTypeException("IMultivaluedProperty<string>", srcProperty);
			}
			base.XmlNode = base.XmlParentNode.OwnerDocument.CreateElement(base.AirSyncTagNames[0], base.Namespace);
			foreach (string text in multivaluedProperty)
			{
				if (!string.IsNullOrEmpty(text))
				{
					XmlNode xmlNode = base.XmlParentNode.OwnerDocument.CreateElement(this.airSyncChildTagName, base.Namespace);
					xmlNode.InnerText = text;
					base.XmlNode.AppendChild(xmlNode);
				}
			}
			if (base.XmlNode.HasChildNodes)
			{
				base.XmlParentNode.AppendChild(base.XmlNode);
			}
		}

		// Token: 0x04000A67 RID: 2663
		private string airSyncChildTagName;
	}
}
