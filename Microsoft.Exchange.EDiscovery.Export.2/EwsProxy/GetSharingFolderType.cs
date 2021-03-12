using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000359 RID: 857
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetSharingFolderType : BaseRequestType
	{
		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06001B8C RID: 7052 RVA: 0x00029827 File Offset: 0x00027A27
		// (set) Token: 0x06001B8D RID: 7053 RVA: 0x0002982F File Offset: 0x00027A2F
		public string SmtpAddress
		{
			get
			{
				return this.smtpAddressField;
			}
			set
			{
				this.smtpAddressField = value;
			}
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06001B8E RID: 7054 RVA: 0x00029838 File Offset: 0x00027A38
		// (set) Token: 0x06001B8F RID: 7055 RVA: 0x00029840 File Offset: 0x00027A40
		public SharingDataType DataType
		{
			get
			{
				return this.dataTypeField;
			}
			set
			{
				this.dataTypeField = value;
			}
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06001B90 RID: 7056 RVA: 0x00029849 File Offset: 0x00027A49
		// (set) Token: 0x06001B91 RID: 7057 RVA: 0x00029851 File Offset: 0x00027A51
		[XmlIgnore]
		public bool DataTypeSpecified
		{
			get
			{
				return this.dataTypeFieldSpecified;
			}
			set
			{
				this.dataTypeFieldSpecified = value;
			}
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06001B92 RID: 7058 RVA: 0x0002985A File Offset: 0x00027A5A
		// (set) Token: 0x06001B93 RID: 7059 RVA: 0x00029862 File Offset: 0x00027A62
		public string SharedFolderId
		{
			get
			{
				return this.sharedFolderIdField;
			}
			set
			{
				this.sharedFolderIdField = value;
			}
		}

		// Token: 0x04001263 RID: 4707
		private string smtpAddressField;

		// Token: 0x04001264 RID: 4708
		private SharingDataType dataTypeField;

		// Token: 0x04001265 RID: 4709
		private bool dataTypeFieldSpecified;

		// Token: 0x04001266 RID: 4710
		private string sharedFolderIdField;
	}
}
