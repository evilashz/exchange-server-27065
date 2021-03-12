using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200086D RID: 2157
	[XmlInclude(typeof(DirectoryPropertyInt32Single))]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[XmlInclude(typeof(DirectoryPropertyInt32SingleMin0))]
	[XmlInclude(typeof(DirectoryPropertyInt32SingleMin0Max4))]
	[XmlInclude(typeof(DirectoryPropertyInt32SingleMin0Max3))]
	[XmlInclude(typeof(DirectoryPropertyInt32SingleMin0Max2))]
	[XmlInclude(typeof(DirectoryPropertyInt32SingleMin0Max1))]
	[DesignerCategory("code")]
	[XmlInclude(typeof(DirectoryPropertyInt32SingleMin1Max86400))]
	[DebuggerStepThrough]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlInclude(typeof(DirectoryPropertyInt32SingleMax1))]
	[XmlInclude(typeof(DirectoryPropertyInt32SingleMin0Max65535))]
	[Serializable]
	public class DirectoryPropertyInt32 : DirectoryProperty
	{
		// Token: 0x06006CF6 RID: 27894 RVA: 0x00174FC1 File Offset: 0x001731C1
		public override IList GetValues()
		{
			if (this.Value != null)
			{
				return this.Value;
			}
			return DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006CF7 RID: 27895 RVA: 0x00174FD7 File Offset: 0x001731D7
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new int[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x170026F1 RID: 9969
		// (get) Token: 0x06006CF8 RID: 27896 RVA: 0x00175007 File Offset: 0x00173207
		// (set) Token: 0x06006CF9 RID: 27897 RVA: 0x0017500F File Offset: 0x0017320F
		[XmlElement("Value", Order = 0)]
		public int[] Value
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

		// Token: 0x040046CE RID: 18126
		private int[] valueField;
	}
}
