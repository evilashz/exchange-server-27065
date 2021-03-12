using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000169 RID: 361
	[XmlInclude(typeof(DirectoryPropertyInt32SingleMin0Max65535))]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[XmlInclude(typeof(DirectoryPropertyInt32SingleMin0Max2))]
	[XmlInclude(typeof(DirectoryPropertyInt32Single))]
	[XmlInclude(typeof(DirectoryPropertyInt32SingleMax1))]
	[XmlInclude(typeof(DirectoryPropertyInt32SingleMin0Max3))]
	[XmlInclude(typeof(DirectoryPropertyInt32SingleMin0))]
	[XmlInclude(typeof(DirectoryPropertyInt32SingleMin0Max1))]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryPropertyInt32 : DirectoryProperty
	{
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x0002077A File Offset: 0x0001E97A
		// (set) Token: 0x060008BA RID: 2234 RVA: 0x00020782 File Offset: 0x0001E982
		[XmlElement("Value")]
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

		// Token: 0x04000468 RID: 1128
		private int[] valueField;
	}
}
