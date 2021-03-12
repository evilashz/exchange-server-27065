using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000882 RID: 2178
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class DirectoryPropertyXmlDirSyncStatus : DirectoryPropertyXml
	{
		// Token: 0x06006D52 RID: 27986 RVA: 0x0017562D File Offset: 0x0017382D
		public override IList GetValues()
		{
			if (this.Value != null)
			{
				return this.Value;
			}
			return DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D53 RID: 27987 RVA: 0x00175643 File Offset: 0x00173843
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new XmlValueDirSyncStatus[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x17002703 RID: 9987
		// (get) Token: 0x06006D54 RID: 27988 RVA: 0x00175673 File Offset: 0x00173873
		// (set) Token: 0x06006D55 RID: 27989 RVA: 0x0017567B File Offset: 0x0017387B
		[XmlElement("Value", Order = 0)]
		public XmlValueDirSyncStatus[] Value
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

		// Token: 0x04004775 RID: 18293
		private XmlValueDirSyncStatus[] valueField;
	}
}
