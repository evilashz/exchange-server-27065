using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Linq;
using Microsoft.Exchange.DxStore.Common;

namespace Microsoft.Exchange.DxStore.Server
{
	// Token: 0x0200003F RID: 63
	[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
	[Serializable]
	public class KeyContainer
	{
		// Token: 0x06000208 RID: 520 RVA: 0x00003C61 File Offset: 0x00001E61
		public KeyContainer()
		{
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00003C6C File Offset: 0x00001E6C
		public KeyContainer(string name, KeyContainer parent)
		{
			this.Name = name;
			this.Parent = parent;
			this.FullName = name;
			if (parent != null)
			{
				this.FullName = Utils.CombinePathNullSafe(parent.FullName, name);
			}
			this.SubKeys = new Dictionary<string, KeyContainer>();
			this.Properties = new Dictionary<string, PropertyValue>();
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00003CBF File Offset: 0x00001EBF
		// (set) Token: 0x0600020B RID: 523 RVA: 0x00003CC7 File Offset: 0x00001EC7
		[DataMember]
		public string Name { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00003CD0 File Offset: 0x00001ED0
		// (set) Token: 0x0600020D RID: 525 RVA: 0x00003CD8 File Offset: 0x00001ED8
		[DataMember]
		public Dictionary<string, KeyContainer> SubKeys { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00003CE1 File Offset: 0x00001EE1
		// (set) Token: 0x0600020F RID: 527 RVA: 0x00003CE9 File Offset: 0x00001EE9
		[DataMember]
		public Dictionary<string, PropertyValue> Properties { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00003CF2 File Offset: 0x00001EF2
		// (set) Token: 0x06000211 RID: 529 RVA: 0x00003CFA File Offset: 0x00001EFA
		[IgnoreDataMember]
		public string FullName { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00003D03 File Offset: 0x00001F03
		// (set) Token: 0x06000213 RID: 531 RVA: 0x00003D0B File Offset: 0x00001F0B
		[IgnoreDataMember]
		public KeyContainer Parent { get; set; }

		// Token: 0x06000214 RID: 532 RVA: 0x00003D14 File Offset: 0x00001F14
		public static KeyContainer Create(XElement element, KeyContainer parent = null)
		{
			string value = element.Attribute("Name").Value;
			KeyContainer keyContainer = new KeyContainer(value, parent);
			if (parent != null)
			{
				parent.SubKeys[value] = keyContainer;
			}
			List<XElement> list = new List<XElement>();
			if (element.HasElements)
			{
				foreach (XElement xelement in element.Elements())
				{
					string localName = xelement.Name.LocalName;
					if (Utils.IsEqual(localName, "Key", StringComparison.OrdinalIgnoreCase))
					{
						list.Add(xelement);
					}
					else if (Utils.IsEqual(localName, "Value", StringComparison.OrdinalIgnoreCase))
					{
						string value2 = xelement.Attribute("Name").Value;
						PropertyValue value3 = PropertyValue.Parse(xelement);
						keyContainer.Properties[value2] = value3;
					}
				}
			}
			foreach (XElement element2 in list)
			{
				KeyContainer.Create(element2, keyContainer);
			}
			return keyContainer;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00003E5C File Offset: 0x0000205C
		public XElement GetSnapshot(bool isRootKey = false)
		{
			XName name = "Key";
			object[] array = new object[2];
			array[0] = new XAttribute("Name", isRootKey ? "\\" : this.Name);
			array[1] = from kvp in this.Properties
			select kvp.Value.ToXElement(kvp.Key);
			XElement xelement = new XElement(name, array);
			foreach (KeyContainer keyContainer in this.SubKeys.Values)
			{
				XElement snapshot = keyContainer.GetSnapshot(false);
				xelement.Add(snapshot);
			}
			return xelement;
		}
	}
}
