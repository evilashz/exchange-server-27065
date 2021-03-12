using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000336 RID: 822
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetClientExtensionType : BaseRequestType
	{
		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06001A78 RID: 6776 RVA: 0x00028F0A File Offset: 0x0002710A
		// (set) Token: 0x06001A79 RID: 6777 RVA: 0x00028F12 File Offset: 0x00027112
		[XmlArrayItem("String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] RequestedExtensionIds
		{
			get
			{
				return this.requestedExtensionIdsField;
			}
			set
			{
				this.requestedExtensionIdsField = value;
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06001A7A RID: 6778 RVA: 0x00028F1B File Offset: 0x0002711B
		// (set) Token: 0x06001A7B RID: 6779 RVA: 0x00028F23 File Offset: 0x00027123
		public GetClientExtensionUserParametersType UserParameters
		{
			get
			{
				return this.userParametersField;
			}
			set
			{
				this.userParametersField = value;
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06001A7C RID: 6780 RVA: 0x00028F2C File Offset: 0x0002712C
		// (set) Token: 0x06001A7D RID: 6781 RVA: 0x00028F34 File Offset: 0x00027134
		public bool IsDebug
		{
			get
			{
				return this.isDebugField;
			}
			set
			{
				this.isDebugField = value;
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06001A7E RID: 6782 RVA: 0x00028F3D File Offset: 0x0002713D
		// (set) Token: 0x06001A7F RID: 6783 RVA: 0x00028F45 File Offset: 0x00027145
		[XmlIgnore]
		public bool IsDebugSpecified
		{
			get
			{
				return this.isDebugFieldSpecified;
			}
			set
			{
				this.isDebugFieldSpecified = value;
			}
		}

		// Token: 0x040011B6 RID: 4534
		private string[] requestedExtensionIdsField;

		// Token: 0x040011B7 RID: 4535
		private GetClientExtensionUserParametersType userParametersField;

		// Token: 0x040011B8 RID: 4536
		private bool isDebugField;

		// Token: 0x040011B9 RID: 4537
		private bool isDebugFieldSpecified;
	}
}
