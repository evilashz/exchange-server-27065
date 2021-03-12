using System;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Runtime.Serialization;
using System.Xml.Linq;
using Microsoft.Win32;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000044 RID: 68
	[KnownType(typeof(ulong))]
	[KnownType(typeof(long))]
	[KnownType(typeof(string[]))]
	[KnownType(typeof(uint))]
	[KnownType(typeof(byte[]))]
	[KnownType(typeof(string))]
	[KnownType(typeof(int))]
	[Serializable]
	public class PropertyValue
	{
		// Token: 0x06000238 RID: 568 RVA: 0x00004132 File Offset: 0x00002332
		public PropertyValue()
		{
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000413A File Offset: 0x0000233A
		public PropertyValue(object value)
		{
			this.Value = value;
			this.Kind = (int)Utils.GetValueKind(value);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00004155 File Offset: 0x00002355
		public PropertyValue(object value, RegistryValueKind kind)
		{
			this.Value = value;
			if (kind == RegistryValueKind.Unknown)
			{
				this.Kind = (int)Utils.GetValueKind(value);
				return;
			}
			this.Kind = (int)kind;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000417B File Offset: 0x0000237B
		// (set) Token: 0x0600023C RID: 572 RVA: 0x00004183 File Offset: 0x00002383
		[DataMember]
		public object Value { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000418C File Offset: 0x0000238C
		// (set) Token: 0x0600023E RID: 574 RVA: 0x00004194 File Offset: 0x00002394
		[DataMember]
		public int Kind { get; set; }

		// Token: 0x0600023F RID: 575 RVA: 0x000041A0 File Offset: 0x000023A0
		public static PropertyValue Parse(XElement element)
		{
			string value = element.Attribute("Kind").Value;
			RegistryValueKind registryValueKind;
			if (!Enum.TryParse<RegistryValueKind>(value, true, out registryValueKind))
			{
				registryValueKind = RegistryValueKind.Unknown;
			}
			string value2 = element.Value;
			RegistryValueKind registryValueKind2 = registryValueKind;
			object value3;
			switch (registryValueKind2)
			{
			case RegistryValueKind.String:
				value3 = value2;
				goto IL_93;
			case RegistryValueKind.ExpandString:
			case (RegistryValueKind)5:
			case (RegistryValueKind)6:
				break;
			case RegistryValueKind.Binary:
				value3 = SoapHexBinary.Parse(value2).Value;
				goto IL_93;
			case RegistryValueKind.DWord:
				value3 = int.Parse(value2);
				goto IL_93;
			case RegistryValueKind.MultiString:
				value3 = Utils.GetMultistring(element);
				goto IL_93;
			default:
				if (registryValueKind2 == RegistryValueKind.QWord)
				{
					value3 = long.Parse(value2);
					goto IL_93;
				}
				break;
			}
			value3 = value2;
			IL_93:
			return new PropertyValue(value3, registryValueKind);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000424C File Offset: 0x0000244C
		public string GetDebugString()
		{
			RegistryValueKind kind = (RegistryValueKind)this.Kind;
			string empty = string.Empty;
			RegistryValueKind registryValueKind = kind;
			switch (registryValueKind)
			{
			case RegistryValueKind.String:
			{
				string text = (string)this.Value;
				int length = text.Length;
				if (length > 15)
				{
					text = text.Substring(0, 12) + "...";
				}
				return string.Format("kind={0}, len={1}, value={2}", RegistryValueKind.String, length, text);
			}
			case RegistryValueKind.ExpandString:
			case (RegistryValueKind)5:
			case (RegistryValueKind)6:
				goto IL_EC;
			case RegistryValueKind.Binary:
				return string.Format("kind={0}, size={1}", RegistryValueKind.Binary, ((byte[])this.Value).Length);
			case RegistryValueKind.DWord:
				break;
			case RegistryValueKind.MultiString:
				return string.Format("kind={0}, count={1}", RegistryValueKind.MultiString, ((string[])this.Value).Length);
			default:
				if (registryValueKind != RegistryValueKind.QWord)
				{
					goto IL_EC;
				}
				break;
			}
			return string.Format("kind={0}, value={1}", kind, this.Value);
			IL_EC:
			return string.Format("kind={0}, isKnown=false", kind);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00004357 File Offset: 0x00002557
		public PropertyValue Clone()
		{
			return (PropertyValue)base.MemberwiseClone();
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000436C File Offset: 0x0000256C
		public XElement ToXElement(string propertyName)
		{
			if (this.Kind == 0)
			{
				this.Kind = (int)Utils.GetValueKind(this.Value);
			}
			XElement xelement = new XElement("Value", new object[]
			{
				new XAttribute("Name", propertyName),
				new XAttribute("Kind", this.Kind.ToString())
			});
			if (this.Kind == 7)
			{
				string[] array = (from o in (object[])this.Value
				select o.ToString()).ToArray<string>();
				foreach (string value in array)
				{
					XElement content = new XElement("String")
					{
						Value = value
					};
					xelement.Add(content);
				}
			}
			else if (this.Kind == 3)
			{
				SoapHexBinary soapHexBinary = new SoapHexBinary((byte[])this.Value);
				xelement.Value = soapHexBinary.ToString();
			}
			else
			{
				xelement.Value = this.Value.ToString();
			}
			return xelement;
		}
	}
}
