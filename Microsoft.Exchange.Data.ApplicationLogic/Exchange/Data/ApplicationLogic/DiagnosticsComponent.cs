using System;
using System.Configuration;
using System.Xml;
using System.Xml.Linq;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x020000EF RID: 239
	public class DiagnosticsComponent : ConfigurationElement
	{
		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x0002693F File Offset: 0x00024B3F
		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get
			{
				return base["name"] as string;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x00026951 File Offset: 0x00024B51
		[ConfigurationProperty("methodName", IsRequired = true)]
		public string MethodName
		{
			get
			{
				return base["methodName"] as string;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x00026963 File Offset: 0x00024B63
		[ConfigurationProperty("implementation", IsRequired = true)]
		public string Implementation
		{
			get
			{
				return base["implementation"] as string;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x00026975 File Offset: 0x00024B75
		[ConfigurationProperty("argument", IsRequired = false)]
		public string Argument
		{
			get
			{
				return base["argument"] as string;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x00026987 File Offset: 0x00024B87
		// (set) Token: 0x060009E3 RID: 2531 RVA: 0x0002698F File Offset: 0x00024B8F
		public XElement Data { get; private set; }

		// Token: 0x060009E4 RID: 2532 RVA: 0x00026998 File Offset: 0x00024B98
		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			if (!string.Equals(elementName, "Data", StringComparison.OrdinalIgnoreCase))
			{
				return base.OnDeserializeUnrecognizedElement(elementName, reader);
			}
			this.Data = (XNode.ReadFrom(reader) as XElement);
			return true;
		}
	}
}
