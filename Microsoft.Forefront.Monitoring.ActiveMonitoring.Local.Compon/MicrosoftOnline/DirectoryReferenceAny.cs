using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000112 RID: 274
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryReferenceAny : DirectoryReference
	{
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x000201B4 File Offset: 0x0001E3B4
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x000201BC File Offset: 0x0001E3BC
		[XmlAttribute]
		public DirectoryObjectClass TargetClass
		{
			get
			{
				return this.targetClassField;
			}
			set
			{
				this.targetClassField = value;
			}
		}

		// Token: 0x0400043A RID: 1082
		private DirectoryObjectClass targetClassField;
	}
}
