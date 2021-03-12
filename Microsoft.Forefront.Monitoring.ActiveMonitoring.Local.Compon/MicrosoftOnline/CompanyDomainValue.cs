using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000102 RID: 258
	[XmlInclude(typeof(CompanyVerifiedDomainValue))]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlInclude(typeof(CompanyUnverifiedDomainValue))]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class CompanyDomainValue
	{
		// Token: 0x060007C3 RID: 1987 RVA: 0x0001FF3C File Offset: 0x0001E13C
		public CompanyDomainValue()
		{
			this.liveTypeField = LiveNamespaceType.None;
			this.capabilitiesField = 0;
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x0001FF52 File Offset: 0x0001E152
		// (set) Token: 0x060007C5 RID: 1989 RVA: 0x0001FF5A File Offset: 0x0001E15A
		[XmlAttribute]
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x0001FF63 File Offset: 0x0001E163
		// (set) Token: 0x060007C7 RID: 1991 RVA: 0x0001FF6B File Offset: 0x0001E16B
		[DefaultValue(LiveNamespaceType.None)]
		[XmlAttribute]
		public LiveNamespaceType LiveType
		{
			get
			{
				return this.liveTypeField;
			}
			set
			{
				this.liveTypeField = value;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x0001FF74 File Offset: 0x0001E174
		// (set) Token: 0x060007C9 RID: 1993 RVA: 0x0001FF7C File Offset: 0x0001E17C
		[XmlAttribute(DataType = "hexBinary")]
		public byte[] LiveNetId
		{
			get
			{
				return this.liveNetIdField;
			}
			set
			{
				this.liveNetIdField = value;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x0001FF85 File Offset: 0x0001E185
		// (set) Token: 0x060007CB RID: 1995 RVA: 0x0001FF8D File Offset: 0x0001E18D
		[XmlAttribute]
		[DefaultValue(0)]
		public int Capabilities
		{
			get
			{
				return this.capabilitiesField;
			}
			set
			{
				this.capabilitiesField = value;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x0001FF96 File Offset: 0x0001E196
		// (set) Token: 0x060007CD RID: 1997 RVA: 0x0001FF9E File Offset: 0x0001E19E
		[XmlAttribute]
		public string MailTargetKey
		{
			get
			{
				return this.mailTargetKeyField;
			}
			set
			{
				this.mailTargetKeyField = value;
			}
		}

		// Token: 0x040003FE RID: 1022
		private string nameField;

		// Token: 0x040003FF RID: 1023
		private LiveNamespaceType liveTypeField;

		// Token: 0x04000400 RID: 1024
		private byte[] liveNetIdField;

		// Token: 0x04000401 RID: 1025
		private int capabilitiesField;

		// Token: 0x04000402 RID: 1026
		private string mailTargetKeyField;
	}
}
