using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000008 RID: 8
	[DebuggerStepThrough]
	[XmlInclude(typeof(DomainInfoEx))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://domains.live.com/Service/DomainServices/V1.0")]
	[Serializable]
	public class DomainInfo
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000691F File Offset: 0x00004B1F
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00006927 File Offset: 0x00004B27
		public string DomainName
		{
			get
			{
				return this.domainNameField;
			}
			set
			{
				this.domainNameField = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00006930 File Offset: 0x00004B30
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00006938 File Offset: 0x00004B38
		public int PartnerId
		{
			get
			{
				return this.partnerIdField;
			}
			set
			{
				this.partnerIdField = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00006941 File Offset: 0x00004B41
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00006949 File Offset: 0x00004B49
		public string DomainConfigId
		{
			get
			{
				return this.domainConfigIdField;
			}
			set
			{
				this.domainConfigIdField = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00006952 File Offset: 0x00004B52
		// (set) Token: 0x0600019B RID: 411 RVA: 0x0000695A File Offset: 0x00004B5A
		public PermissionFlags PermissionFlags
		{
			get
			{
				return this.permissionFlagsField;
			}
			set
			{
				this.permissionFlagsField = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00006963 File Offset: 0x00004B63
		// (set) Token: 0x0600019D RID: 413 RVA: 0x0000696B File Offset: 0x00004B6B
		public bool IsPendingProcessing
		{
			get
			{
				return this.isPendingProcessingField;
			}
			set
			{
				this.isPendingProcessingField = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00006974 File Offset: 0x00004B74
		// (set) Token: 0x0600019F RID: 415 RVA: 0x0000697C File Offset: 0x00004B7C
		public bool IsPendingRelease
		{
			get
			{
				return this.isPendingReleaseField;
			}
			set
			{
				this.isPendingReleaseField = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00006985 File Offset: 0x00004B85
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x0000698D File Offset: 0x00004B8D
		public bool IsMembershipEditable
		{
			get
			{
				return this.isMembershipEditableField;
			}
			set
			{
				this.isMembershipEditableField = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00006996 File Offset: 0x00004B96
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x0000699E File Offset: 0x00004B9E
		public bool IsEmailActive
		{
			get
			{
				return this.isEmailActiveField;
			}
			set
			{
				this.isEmailActiveField = value;
			}
		}

		// Token: 0x0400006B RID: 107
		private string domainNameField;

		// Token: 0x0400006C RID: 108
		private int partnerIdField;

		// Token: 0x0400006D RID: 109
		private string domainConfigIdField;

		// Token: 0x0400006E RID: 110
		private PermissionFlags permissionFlagsField;

		// Token: 0x0400006F RID: 111
		private bool isPendingProcessingField;

		// Token: 0x04000070 RID: 112
		private bool isPendingReleaseField;

		// Token: 0x04000071 RID: 113
		private bool isMembershipEditableField;

		// Token: 0x04000072 RID: 114
		private bool isEmailActiveField;
	}
}
