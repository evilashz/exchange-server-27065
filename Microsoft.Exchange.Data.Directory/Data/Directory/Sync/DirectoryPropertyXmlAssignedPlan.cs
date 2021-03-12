using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200087C RID: 2172
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class DirectoryPropertyXmlAssignedPlan : DirectoryPropertyXml
	{
		// Token: 0x06006D34 RID: 27956 RVA: 0x001753F8 File Offset: 0x001735F8
		public override IList GetValues()
		{
			if (this.Value != null)
			{
				return this.Value;
			}
			return DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D35 RID: 27957 RVA: 0x0017540E File Offset: 0x0017360E
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new XmlValueAssignedPlan[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x170026FD RID: 9981
		// (get) Token: 0x06006D36 RID: 27958 RVA: 0x0017543E File Offset: 0x0017363E
		// (set) Token: 0x06006D37 RID: 27959 RVA: 0x00175446 File Offset: 0x00173646
		[XmlElement("Value", Order = 0)]
		public XmlValueAssignedPlan[] Value
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

		// Token: 0x0400476F RID: 18287
		private XmlValueAssignedPlan[] valueField;
	}
}
