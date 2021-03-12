using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200086C RID: 2156
	[XmlInclude(typeof(DirectoryPropertyGuidSingle))]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class DirectoryPropertyGuid : DirectoryProperty
	{
		// Token: 0x06006CF1 RID: 27889 RVA: 0x00174F62 File Offset: 0x00173162
		public override IList GetValues()
		{
			if (this.Value != null)
			{
				return this.Value;
			}
			return DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006CF2 RID: 27890 RVA: 0x00174F78 File Offset: 0x00173178
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new string[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x170026F0 RID: 9968
		// (get) Token: 0x06006CF3 RID: 27891 RVA: 0x00174FA8 File Offset: 0x001731A8
		// (set) Token: 0x06006CF4 RID: 27892 RVA: 0x00174FB0 File Offset: 0x001731B0
		[XmlElement("Value", Order = 0)]
		public string[] Value
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

		// Token: 0x040046CD RID: 18125
		private string[] valueField;
	}
}
