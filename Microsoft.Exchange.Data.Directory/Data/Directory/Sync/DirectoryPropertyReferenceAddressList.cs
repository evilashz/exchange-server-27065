using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000871 RID: 2161
	[DebuggerStepThrough]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[XmlInclude(typeof(DirectoryPropertyReferenceAddressListSingle))]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryPropertyReferenceAddressList : DirectoryPropertyReference
	{
		// Token: 0x06006D01 RID: 27905 RVA: 0x00175087 File Offset: 0x00173287
		public override IList GetValues()
		{
			if (this.Value != null)
			{
				return this.Value;
			}
			return DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D02 RID: 27906 RVA: 0x0017509D File Offset: 0x0017329D
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new DirectoryReferenceAddressList[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x170026F3 RID: 9971
		// (get) Token: 0x06006D03 RID: 27907 RVA: 0x001750CD File Offset: 0x001732CD
		// (set) Token: 0x06006D04 RID: 27908 RVA: 0x001750D5 File Offset: 0x001732D5
		[XmlElement("Value", Order = 0)]
		public DirectoryReferenceAddressList[] Value
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

		// Token: 0x04004765 RID: 18277
		private DirectoryReferenceAddressList[] valueField;
	}
}
