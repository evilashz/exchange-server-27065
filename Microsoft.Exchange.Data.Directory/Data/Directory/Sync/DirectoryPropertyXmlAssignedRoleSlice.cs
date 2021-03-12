using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000878 RID: 2168
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class DirectoryPropertyXmlAssignedRoleSlice : DirectoryPropertyXml
	{
		// Token: 0x06006D20 RID: 27936 RVA: 0x001752A0 File Offset: 0x001734A0
		public override IList GetValues()
		{
			if (this.Value != null)
			{
				return this.Value;
			}
			return DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D21 RID: 27937 RVA: 0x001752B6 File Offset: 0x001734B6
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
			}
		}

		// Token: 0x170026F9 RID: 9977
		// (get) Token: 0x06006D22 RID: 27938 RVA: 0x001752C7 File Offset: 0x001734C7
		// (set) Token: 0x06006D23 RID: 27939 RVA: 0x001752CF File Offset: 0x001734CF
		[XmlElement("Value", Order = 0)]
		public XmlValueAssignedRoleSlice[] Value
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

		// Token: 0x0400476B RID: 18283
		private XmlValueAssignedRoleSlice[] valueField;
	}
}
