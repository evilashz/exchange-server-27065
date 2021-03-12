using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200010C RID: 268
	[XmlInclude(typeof(DirectoryReferenceContact))]
	[DebuggerStepThrough]
	[XmlInclude(typeof(DirectoryReferenceAny))]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[XmlInclude(typeof(DirectoryReferenceAddressList))]
	[DesignerCategory("code")]
	[XmlInclude(typeof(DirectoryReferenceServicePlan))]
	[Serializable]
	public abstract class DirectoryReference
	{
		// Token: 0x060007F4 RID: 2036 RVA: 0x00020107 File Offset: 0x0001E307
		public DirectoryReference()
		{
			this.targetDeletedField = false;
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x00020116 File Offset: 0x0001E316
		// (set) Token: 0x060007F6 RID: 2038 RVA: 0x0002011E File Offset: 0x0001E31E
		[DefaultValue(false)]
		[XmlAttribute]
		public bool TargetDeleted
		{
			get
			{
				return this.targetDeletedField;
			}
			set
			{
				this.targetDeletedField = value;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x00020127 File Offset: 0x0001E327
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x0002012F File Offset: 0x0001E32F
		[XmlText]
		public string Value
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

		// Token: 0x04000416 RID: 1046
		private bool targetDeletedField;

		// Token: 0x04000417 RID: 1047
		private string valueField;
	}
}
