using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000268 RID: 616
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[Serializable]
	public class RightsManagementLicenseDataType
	{
		// Token: 0x04000FC3 RID: 4035
		public int RightsManagedMessageDecryptionStatus;

		// Token: 0x04000FC4 RID: 4036
		[XmlIgnore]
		public bool RightsManagedMessageDecryptionStatusSpecified;

		// Token: 0x04000FC5 RID: 4037
		public string RmsTemplateId;

		// Token: 0x04000FC6 RID: 4038
		public string TemplateName;

		// Token: 0x04000FC7 RID: 4039
		public string TemplateDescription;

		// Token: 0x04000FC8 RID: 4040
		public bool EditAllowed;

		// Token: 0x04000FC9 RID: 4041
		[XmlIgnore]
		public bool EditAllowedSpecified;

		// Token: 0x04000FCA RID: 4042
		public bool ReplyAllowed;

		// Token: 0x04000FCB RID: 4043
		[XmlIgnore]
		public bool ReplyAllowedSpecified;

		// Token: 0x04000FCC RID: 4044
		public bool ReplyAllAllowed;

		// Token: 0x04000FCD RID: 4045
		[XmlIgnore]
		public bool ReplyAllAllowedSpecified;

		// Token: 0x04000FCE RID: 4046
		public bool ForwardAllowed;

		// Token: 0x04000FCF RID: 4047
		[XmlIgnore]
		public bool ForwardAllowedSpecified;

		// Token: 0x04000FD0 RID: 4048
		public bool ModifyRecipientsAllowed;

		// Token: 0x04000FD1 RID: 4049
		[XmlIgnore]
		public bool ModifyRecipientsAllowedSpecified;

		// Token: 0x04000FD2 RID: 4050
		public bool ExtractAllowed;

		// Token: 0x04000FD3 RID: 4051
		[XmlIgnore]
		public bool ExtractAllowedSpecified;

		// Token: 0x04000FD4 RID: 4052
		public bool PrintAllowed;

		// Token: 0x04000FD5 RID: 4053
		[XmlIgnore]
		public bool PrintAllowedSpecified;

		// Token: 0x04000FD6 RID: 4054
		public bool ExportAllowed;

		// Token: 0x04000FD7 RID: 4055
		[XmlIgnore]
		public bool ExportAllowedSpecified;

		// Token: 0x04000FD8 RID: 4056
		public bool ProgrammaticAccessAllowed;

		// Token: 0x04000FD9 RID: 4057
		[XmlIgnore]
		public bool ProgrammaticAccessAllowedSpecified;

		// Token: 0x04000FDA RID: 4058
		public bool IsOwner;

		// Token: 0x04000FDB RID: 4059
		[XmlIgnore]
		public bool IsOwnerSpecified;

		// Token: 0x04000FDC RID: 4060
		public string ContentOwner;

		// Token: 0x04000FDD RID: 4061
		public string ContentExpiryDate;
	}
}
