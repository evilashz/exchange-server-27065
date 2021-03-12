using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001D0 RID: 464
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class UnifiedMessageServiceConfiguration : ServiceConfiguration
	{
		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x0600139D RID: 5021 RVA: 0x0002555F File Offset: 0x0002375F
		// (set) Token: 0x0600139E RID: 5022 RVA: 0x00025567 File Offset: 0x00023767
		public bool UmEnabled
		{
			get
			{
				return this.umEnabledField;
			}
			set
			{
				this.umEnabledField = value;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x0600139F RID: 5023 RVA: 0x00025570 File Offset: 0x00023770
		// (set) Token: 0x060013A0 RID: 5024 RVA: 0x00025578 File Offset: 0x00023778
		public string PlayOnPhoneDialString
		{
			get
			{
				return this.playOnPhoneDialStringField;
			}
			set
			{
				this.playOnPhoneDialStringField = value;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x060013A1 RID: 5025 RVA: 0x00025581 File Offset: 0x00023781
		// (set) Token: 0x060013A2 RID: 5026 RVA: 0x00025589 File Offset: 0x00023789
		public bool PlayOnPhoneEnabled
		{
			get
			{
				return this.playOnPhoneEnabledField;
			}
			set
			{
				this.playOnPhoneEnabledField = value;
			}
		}

		// Token: 0x04000D94 RID: 3476
		private bool umEnabledField;

		// Token: 0x04000D95 RID: 3477
		private string playOnPhoneDialStringField;

		// Token: 0x04000D96 RID: 3478
		private bool playOnPhoneEnabledField;
	}
}
