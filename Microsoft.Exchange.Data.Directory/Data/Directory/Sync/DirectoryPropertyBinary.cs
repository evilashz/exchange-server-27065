using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000869 RID: 2153
	[DesignerCategory("code")]
	[XmlInclude(typeof(DirectoryPropertyBinarySingle))]
	[XmlInclude(typeof(DirectoryPropertyBinarySingleLength1To102400))]
	[XmlInclude(typeof(DirectoryPropertyBinarySingleLength1To32000))]
	[XmlInclude(typeof(DirectoryPropertyBinarySingleLength1To12000))]
	[XmlInclude(typeof(DirectoryPropertyBinarySingleLength1To8000))]
	[XmlInclude(typeof(DirectoryPropertyBinarySingleLength1To4000))]
	[XmlInclude(typeof(DirectoryPropertyBinarySingleLength1To195))]
	[XmlInclude(typeof(DirectoryPropertyBinarySingleLength1To128))]
	[XmlInclude(typeof(DirectoryPropertyBinarySingleLength8))]
	[XmlInclude(typeof(DirectoryPropertyBinaryLength1To32768))]
	[XmlInclude(typeof(DirectoryPropertyBinaryLength1To2048))]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public abstract class DirectoryPropertyBinary : DirectoryProperty
	{
		// Token: 0x06006CE2 RID: 27874 RVA: 0x00174E45 File Offset: 0x00173045
		public override IList GetValues()
		{
			if (this.Value != null)
			{
				return this.Value;
			}
			return DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006CE3 RID: 27875 RVA: 0x00174E5B File Offset: 0x0017305B
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new byte[values.Count][];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x170026ED RID: 9965
		// (get) Token: 0x06006CE4 RID: 27876 RVA: 0x00174E8B File Offset: 0x0017308B
		// (set) Token: 0x06006CE5 RID: 27877 RVA: 0x00174E93 File Offset: 0x00173093
		[XmlElement("Value", DataType = "hexBinary", Order = 0)]
		public byte[][] Value
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

		// Token: 0x040046CA RID: 18122
		private byte[][] valueField;
	}
}
