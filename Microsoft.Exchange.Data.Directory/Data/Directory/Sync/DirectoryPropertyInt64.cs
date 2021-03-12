using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200086E RID: 2158
	[XmlInclude(typeof(DirectoryPropertyInt64Single))]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class DirectoryPropertyInt64 : DirectoryProperty
	{
		// Token: 0x06006CFB RID: 27899 RVA: 0x00175020 File Offset: 0x00173220
		public override IList GetValues()
		{
			if (this.Value != null)
			{
				return this.Value;
			}
			return DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006CFC RID: 27900 RVA: 0x00175036 File Offset: 0x00173236
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new long[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x170026F2 RID: 9970
		// (get) Token: 0x06006CFD RID: 27901 RVA: 0x00175066 File Offset: 0x00173266
		// (set) Token: 0x06006CFE RID: 27902 RVA: 0x0017506E File Offset: 0x0017326E
		[XmlElement("Value", Order = 0)]
		public long[] Value
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

		// Token: 0x040046CF RID: 18127
		private long[] valueField;
	}
}
