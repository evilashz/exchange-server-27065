using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200086A RID: 2154
	[XmlInclude(typeof(DirectoryPropertyBooleanSingle))]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryPropertyBoolean : DirectoryProperty
	{
		// Token: 0x06006CE7 RID: 27879 RVA: 0x00174EA4 File Offset: 0x001730A4
		public override IList GetValues()
		{
			if (this.Value != null)
			{
				return this.Value;
			}
			return DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006CE8 RID: 27880 RVA: 0x00174EBA File Offset: 0x001730BA
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new bool[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x170026EE RID: 9966
		// (get) Token: 0x06006CE9 RID: 27881 RVA: 0x00174EEA File Offset: 0x001730EA
		// (set) Token: 0x06006CEA RID: 27882 RVA: 0x00174EF2 File Offset: 0x001730F2
		[XmlElement("Value", Order = 0)]
		public bool[] Value
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

		// Token: 0x040046CB RID: 18123
		private bool[] valueField;
	}
}
