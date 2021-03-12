using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000218 RID: 536
	[ClassAccessLevel(AccessLevel.Consumer)]
	public class Parameter
	{
		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x00050ECA File Offset: 0x0004F0CA
		// (set) Token: 0x06001262 RID: 4706 RVA: 0x00050ED2 File Offset: 0x0004F0D2
		[XmlAttribute]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x00050EDB File Offset: 0x0004F0DB
		// (set) Token: 0x06001264 RID: 4708 RVA: 0x00050EE3 File Offset: 0x0004F0E3
		[XmlAttribute]
		public string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				if (this.values.Count > 0)
				{
					throw new ArgumentException(Strings.ParameterValueIsAlreadySet(this.Name), this.Name);
				}
				this.value = value;
				this.isSingleValue = true;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001265 RID: 4709 RVA: 0x00050F1D File Offset: 0x0004F11D
		[XmlArray]
		[XmlArrayItem(ElementName = "Value", Type = typeof(string))]
		public List<string> Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001266 RID: 4710 RVA: 0x00050F28 File Offset: 0x0004F128
		internal object EffectiveValue
		{
			get
			{
				if (!this.isSingleValue)
				{
					return this.Values.ToArray();
				}
				if (this.Value.ToLower() == "true")
				{
					return true;
				}
				if (this.Value.ToLower() == "false")
				{
					return false;
				}
				return this.Value;
			}
		}

		// Token: 0x040007D7 RID: 2007
		private string name;

		// Token: 0x040007D8 RID: 2008
		private string value;

		// Token: 0x040007D9 RID: 2009
		private bool isSingleValue;

		// Token: 0x040007DA RID: 2010
		private List<string> values = new List<string>();
	}
}
