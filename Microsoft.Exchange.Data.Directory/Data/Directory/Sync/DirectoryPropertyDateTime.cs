using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200086B RID: 2155
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlInclude(typeof(DirectoryPropertyDateTimeSingle))]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class DirectoryPropertyDateTime : DirectoryProperty
	{
		// Token: 0x06006CEC RID: 27884 RVA: 0x00174F03 File Offset: 0x00173103
		public override IList GetValues()
		{
			if (this.Value != null)
			{
				return this.Value;
			}
			return DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006CED RID: 27885 RVA: 0x00174F19 File Offset: 0x00173119
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new DateTime[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x170026EF RID: 9967
		// (get) Token: 0x06006CEE RID: 27886 RVA: 0x00174F49 File Offset: 0x00173149
		// (set) Token: 0x06006CEF RID: 27887 RVA: 0x00174F51 File Offset: 0x00173151
		[XmlElement("Value", Order = 0)]
		public DateTime[] Value
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

		// Token: 0x040046CC RID: 18124
		private DateTime[] valueField;
	}
}
