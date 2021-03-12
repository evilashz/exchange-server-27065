using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200019B RID: 411
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class SearchableMailboxType
	{
		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x0002433B File Offset: 0x0002253B
		// (set) Token: 0x06001179 RID: 4473 RVA: 0x00024343 File Offset: 0x00022543
		public string Guid
		{
			get
			{
				return this.guidField;
			}
			set
			{
				this.guidField = value;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x0002434C File Offset: 0x0002254C
		// (set) Token: 0x0600117B RID: 4475 RVA: 0x00024354 File Offset: 0x00022554
		public string PrimarySmtpAddress
		{
			get
			{
				return this.primarySmtpAddressField;
			}
			set
			{
				this.primarySmtpAddressField = value;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x0002435D File Offset: 0x0002255D
		// (set) Token: 0x0600117D RID: 4477 RVA: 0x00024365 File Offset: 0x00022565
		public bool IsExternalMailbox
		{
			get
			{
				return this.isExternalMailboxField;
			}
			set
			{
				this.isExternalMailboxField = value;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x0002436E File Offset: 0x0002256E
		// (set) Token: 0x0600117F RID: 4479 RVA: 0x00024376 File Offset: 0x00022576
		public string ExternalEmailAddress
		{
			get
			{
				return this.externalEmailAddressField;
			}
			set
			{
				this.externalEmailAddressField = value;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001180 RID: 4480 RVA: 0x0002437F File Offset: 0x0002257F
		// (set) Token: 0x06001181 RID: 4481 RVA: 0x00024387 File Offset: 0x00022587
		public string DisplayName
		{
			get
			{
				return this.displayNameField;
			}
			set
			{
				this.displayNameField = value;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x00024390 File Offset: 0x00022590
		// (set) Token: 0x06001183 RID: 4483 RVA: 0x00024398 File Offset: 0x00022598
		public bool IsMembershipGroup
		{
			get
			{
				return this.isMembershipGroupField;
			}
			set
			{
				this.isMembershipGroupField = value;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x000243A1 File Offset: 0x000225A1
		// (set) Token: 0x06001185 RID: 4485 RVA: 0x000243A9 File Offset: 0x000225A9
		public string ReferenceId
		{
			get
			{
				return this.referenceIdField;
			}
			set
			{
				this.referenceIdField = value;
			}
		}

		// Token: 0x04000C03 RID: 3075
		private string guidField;

		// Token: 0x04000C04 RID: 3076
		private string primarySmtpAddressField;

		// Token: 0x04000C05 RID: 3077
		private bool isExternalMailboxField;

		// Token: 0x04000C06 RID: 3078
		private string externalEmailAddressField;

		// Token: 0x04000C07 RID: 3079
		private string displayNameField;

		// Token: 0x04000C08 RID: 3080
		private bool isMembershipGroupField;

		// Token: 0x04000C09 RID: 3081
		private string referenceIdField;
	}
}
