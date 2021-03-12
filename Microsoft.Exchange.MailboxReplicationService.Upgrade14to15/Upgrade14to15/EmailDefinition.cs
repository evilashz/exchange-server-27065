using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000009 RID: 9
	[DebuggerStepThrough]
	[DataContract(Name = "EmailDefinition", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.Email.Platform")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class EmailDefinition : IExtensibleDataObject
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002732 File Offset: 0x00000932
		// (set) Token: 0x06000022 RID: 34 RVA: 0x0000273A File Offset: 0x0000093A
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002743 File Offset: 0x00000943
		// (set) Token: 0x06000024 RID: 36 RVA: 0x0000274B File Offset: 0x0000094B
		[DataMember]
		public EmailAttribute[] Attributes
		{
			get
			{
				return this.AttributesField;
			}
			set
			{
				this.AttributesField = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002754 File Offset: 0x00000954
		// (set) Token: 0x06000026 RID: 38 RVA: 0x0000275C File Offset: 0x0000095C
		[DataMember]
		public string[] BCCList
		{
			get
			{
				return this.BCCListField;
			}
			set
			{
				this.BCCListField = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002765 File Offset: 0x00000965
		// (set) Token: 0x06000028 RID: 40 RVA: 0x0000276D File Offset: 0x0000096D
		[DataMember]
		public string[] CClist
		{
			get
			{
				return this.CClistField;
			}
			set
			{
				this.CClistField = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002776 File Offset: 0x00000976
		// (set) Token: 0x0600002A RID: 42 RVA: 0x0000277E File Offset: 0x0000097E
		[DataMember]
		public string EmailAddress
		{
			get
			{
				return this.EmailAddressField;
			}
			set
			{
				this.EmailAddressField = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002787 File Offset: 0x00000987
		// (set) Token: 0x0600002C RID: 44 RVA: 0x0000278F File Offset: 0x0000098F
		[DataMember]
		public string EmailId
		{
			get
			{
				return this.EmailIdField;
			}
			set
			{
				this.EmailIdField = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002798 File Offset: 0x00000998
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000027A0 File Offset: 0x000009A0
		[DataMember]
		public string LocaleId
		{
			get
			{
				return this.LocaleIdField;
			}
			set
			{
				this.LocaleIdField = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000027A9 File Offset: 0x000009A9
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000027B1 File Offset: 0x000009B1
		[DataMember]
		public Guid TrackingId
		{
			get
			{
				return this.TrackingIdField;
			}
			set
			{
				this.TrackingIdField = value;
			}
		}

		// Token: 0x04000004 RID: 4
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000005 RID: 5
		private EmailAttribute[] AttributesField;

		// Token: 0x04000006 RID: 6
		private string[] BCCListField;

		// Token: 0x04000007 RID: 7
		private string[] CClistField;

		// Token: 0x04000008 RID: 8
		private string EmailAddressField;

		// Token: 0x04000009 RID: 9
		private string EmailIdField;

		// Token: 0x0400000A RID: 10
		private string LocaleIdField;

		// Token: 0x0400000B RID: 11
		private Guid TrackingIdField;
	}
}
