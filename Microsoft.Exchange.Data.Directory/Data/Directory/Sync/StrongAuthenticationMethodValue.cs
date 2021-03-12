using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000912 RID: 2322
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class StrongAuthenticationMethodValue
	{
		// Token: 0x06006F2F RID: 28463 RVA: 0x001769AE File Offset: 0x00174BAE
		public StrongAuthenticationMethodValue()
		{
			this.defaultField = false;
		}

		// Token: 0x17002780 RID: 10112
		// (get) Token: 0x06006F30 RID: 28464 RVA: 0x001769BD File Offset: 0x00174BBD
		// (set) Token: 0x06006F31 RID: 28465 RVA: 0x001769C5 File Offset: 0x00174BC5
		[XmlAttribute]
		public int MethodType
		{
			get
			{
				return this.methodTypeField;
			}
			set
			{
				this.methodTypeField = value;
			}
		}

		// Token: 0x17002781 RID: 10113
		// (get) Token: 0x06006F32 RID: 28466 RVA: 0x001769CE File Offset: 0x00174BCE
		// (set) Token: 0x06006F33 RID: 28467 RVA: 0x001769D6 File Offset: 0x00174BD6
		[DefaultValue(false)]
		[XmlAttribute]
		public bool Default
		{
			get
			{
				return this.defaultField;
			}
			set
			{
				this.defaultField = value;
			}
		}

		// Token: 0x0400482C RID: 18476
		private int methodTypeField;

		// Token: 0x0400482D RID: 18477
		private bool defaultField;
	}
}
