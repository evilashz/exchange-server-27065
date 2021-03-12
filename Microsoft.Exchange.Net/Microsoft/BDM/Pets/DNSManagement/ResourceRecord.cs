using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;
using Microsoft.BDM.Pets.SharedLibrary.Enums;

namespace Microsoft.BDM.Pets.DNSManagement
{
	// Token: 0x02000BC7 RID: 3015
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "ResourceRecord", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.DNSManagement")]
	public class ResourceRecord : IExtensibleDataObject
	{
		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x06004103 RID: 16643 RVA: 0x000AD322 File Offset: 0x000AB522
		// (set) Token: 0x06004104 RID: 16644 RVA: 0x000AD32A File Offset: 0x000AB52A
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

		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x06004105 RID: 16645 RVA: 0x000AD333 File Offset: 0x000AB533
		// (set) Token: 0x06004106 RID: 16646 RVA: 0x000AD33B File Offset: 0x000AB53B
		[DataMember]
		public string DomainName
		{
			get
			{
				return this.DomainNameField;
			}
			set
			{
				this.DomainNameField = value;
			}
		}

		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x06004107 RID: 16647 RVA: 0x000AD344 File Offset: 0x000AB544
		// (set) Token: 0x06004108 RID: 16648 RVA: 0x000AD34C File Offset: 0x000AB54C
		[DataMember]
		public bool IsDeleted
		{
			get
			{
				return this.IsDeletedField;
			}
			set
			{
				this.IsDeletedField = value;
			}
		}

		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x06004109 RID: 16649 RVA: 0x000AD355 File Offset: 0x000AB555
		// (set) Token: 0x0600410A RID: 16650 RVA: 0x000AD35D File Offset: 0x000AB55D
		[DataMember]
		public ResourceRecordType RecordType
		{
			get
			{
				return this.RecordTypeField;
			}
			set
			{
				this.RecordTypeField = value;
			}
		}

		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x0600410B RID: 16651 RVA: 0x000AD366 File Offset: 0x000AB566
		// (set) Token: 0x0600410C RID: 16652 RVA: 0x000AD36E File Offset: 0x000AB56E
		[DataMember]
		public Guid ResourceRecordId
		{
			get
			{
				return this.ResourceRecordIdField;
			}
			set
			{
				this.ResourceRecordIdField = value;
			}
		}

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x0600410D RID: 16653 RVA: 0x000AD377 File Offset: 0x000AB577
		// (set) Token: 0x0600410E RID: 16654 RVA: 0x000AD37F File Offset: 0x000AB57F
		[DataMember]
		public int TTL
		{
			get
			{
				return this.TTLField;
			}
			set
			{
				this.TTLField = value;
			}
		}

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x0600410F RID: 16655 RVA: 0x000AD388 File Offset: 0x000AB588
		// (set) Token: 0x06004110 RID: 16656 RVA: 0x000AD390 File Offset: 0x000AB590
		[DataMember]
		public string Value
		{
			get
			{
				return this.ValueField;
			}
			set
			{
				this.ValueField = value;
			}
		}

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x06004111 RID: 16657 RVA: 0x000AD399 File Offset: 0x000AB599
		// (set) Token: 0x06004112 RID: 16658 RVA: 0x000AD3A1 File Offset: 0x000AB5A1
		[DataMember]
		public Guid ZoneGUID
		{
			get
			{
				return this.ZoneGUIDField;
			}
			set
			{
				this.ZoneGUIDField = value;
			}
		}

		// Token: 0x04003842 RID: 14402
		private ExtensionDataObject extensionDataField;

		// Token: 0x04003843 RID: 14403
		private string DomainNameField;

		// Token: 0x04003844 RID: 14404
		private bool IsDeletedField;

		// Token: 0x04003845 RID: 14405
		private ResourceRecordType RecordTypeField;

		// Token: 0x04003846 RID: 14406
		private Guid ResourceRecordIdField;

		// Token: 0x04003847 RID: 14407
		private int TTLField;

		// Token: 0x04003848 RID: 14408
		private string ValueField;

		// Token: 0x04003849 RID: 14409
		private Guid ZoneGUIDField;
	}
}
