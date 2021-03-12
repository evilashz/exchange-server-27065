using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000125 RID: 293
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class BodyType
	{
		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x00021E85 File Offset: 0x00020085
		// (set) Token: 0x06000D22 RID: 3362 RVA: 0x00021E8D File Offset: 0x0002008D
		[XmlAttribute("BodyType")]
		public BodyTypeType BodyType1
		{
			get
			{
				return this.bodyType1Field;
			}
			set
			{
				this.bodyType1Field = value;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x00021E96 File Offset: 0x00020096
		// (set) Token: 0x06000D24 RID: 3364 RVA: 0x00021E9E File Offset: 0x0002009E
		[XmlAttribute]
		public bool IsTruncated
		{
			get
			{
				return this.isTruncatedField;
			}
			set
			{
				this.isTruncatedField = value;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x00021EA7 File Offset: 0x000200A7
		// (set) Token: 0x06000D26 RID: 3366 RVA: 0x00021EAF File Offset: 0x000200AF
		[XmlIgnore]
		public bool IsTruncatedSpecified
		{
			get
			{
				return this.isTruncatedFieldSpecified;
			}
			set
			{
				this.isTruncatedFieldSpecified = value;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x00021EB8 File Offset: 0x000200B8
		// (set) Token: 0x06000D28 RID: 3368 RVA: 0x00021EC0 File Offset: 0x000200C0
		[XmlText]
		public string Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		// Token: 0x04000921 RID: 2337
		private BodyTypeType bodyType1Field;

		// Token: 0x04000922 RID: 2338
		private bool isTruncatedField;

		// Token: 0x04000923 RID: 2339
		private bool isTruncatedFieldSpecified;

		// Token: 0x04000924 RID: 2340
		private string valueField;
	}
}
