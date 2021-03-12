using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000167 RID: 359
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[XmlInclude(typeof(DirectoryPropertyInt64Single))]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class DirectoryPropertyInt64 : DirectoryProperty
	{
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x00020759 File Offset: 0x0001E959
		// (set) Token: 0x060008B6 RID: 2230 RVA: 0x00020761 File Offset: 0x0001E961
		[XmlElement("Value")]
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

		// Token: 0x04000467 RID: 1127
		private long[] valueField;
	}
}
