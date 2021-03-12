using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000937 RID: 2359
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class ValidationErrorValue
	{
		// Token: 0x170027BB RID: 10171
		// (get) Token: 0x06006FC4 RID: 28612 RVA: 0x00176E9F File Offset: 0x0017509F
		// (set) Token: 0x06006FC5 RID: 28613 RVA: 0x00176EA7 File Offset: 0x001750A7
		[XmlElement(Order = 0)]
		public XmlElement ErrorDetail
		{
			get
			{
				return this.errorDetailField;
			}
			set
			{
				this.errorDetailField = value;
			}
		}

		// Token: 0x170027BC RID: 10172
		// (get) Token: 0x06006FC6 RID: 28614 RVA: 0x00176EB0 File Offset: 0x001750B0
		// (set) Token: 0x06006FC7 RID: 28615 RVA: 0x00176EB8 File Offset: 0x001750B8
		[XmlAttribute]
		public bool Resolved
		{
			get
			{
				return this.resolvedField;
			}
			set
			{
				this.resolvedField = value;
			}
		}

		// Token: 0x170027BD RID: 10173
		// (get) Token: 0x06006FC8 RID: 28616 RVA: 0x00176EC1 File Offset: 0x001750C1
		// (set) Token: 0x06006FC9 RID: 28617 RVA: 0x00176EC9 File Offset: 0x001750C9
		[XmlAttribute]
		public string ServiceInstance
		{
			get
			{
				return this.serviceInstanceField;
			}
			set
			{
				this.serviceInstanceField = value;
			}
		}

		// Token: 0x170027BE RID: 10174
		// (get) Token: 0x06006FCA RID: 28618 RVA: 0x00176ED2 File Offset: 0x001750D2
		// (set) Token: 0x06006FCB RID: 28619 RVA: 0x00176EDA File Offset: 0x001750DA
		[XmlAttribute]
		public DateTime Timestamp
		{
			get
			{
				return this.timestampField;
			}
			set
			{
				this.timestampField = value;
			}
		}

		// Token: 0x0400488D RID: 18573
		private XmlElement errorDetailField;

		// Token: 0x0400488E RID: 18574
		private bool resolvedField;

		// Token: 0x0400488F RID: 18575
		private string serviceInstanceField;

		// Token: 0x04004890 RID: 18576
		private DateTime timestampField;
	}
}
