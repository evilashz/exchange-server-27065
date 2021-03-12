using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000872 RID: 2162
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[XmlInclude(typeof(DirectoryPropertyReferenceAnySingle))]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryPropertyReferenceAny : DirectoryPropertyReference
	{
		// Token: 0x06006D06 RID: 27910 RVA: 0x001750E6 File Offset: 0x001732E6
		public override IList GetValues()
		{
			if (this.Value != null)
			{
				return this.Value;
			}
			return DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D07 RID: 27911 RVA: 0x001750FC File Offset: 0x001732FC
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new DirectoryReferenceAny[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x170026F4 RID: 9972
		// (get) Token: 0x06006D08 RID: 27912 RVA: 0x0017512C File Offset: 0x0017332C
		// (set) Token: 0x06006D09 RID: 27913 RVA: 0x00175134 File Offset: 0x00173334
		[XmlElement("Value", Order = 0)]
		public DirectoryReferenceAny[] Value
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

		// Token: 0x04004766 RID: 18278
		private DirectoryReferenceAny[] valueField;
	}
}
