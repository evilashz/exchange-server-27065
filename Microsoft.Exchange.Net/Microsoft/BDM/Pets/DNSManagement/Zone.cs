using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.BDM.Pets.DNSManagement
{
	// Token: 0x02000BC8 RID: 3016
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "Zone", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.DNSManagement")]
	public class Zone : IExtensibleDataObject
	{
		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x06004114 RID: 16660 RVA: 0x000AD3B2 File Offset: 0x000AB5B2
		// (set) Token: 0x06004115 RID: 16661 RVA: 0x000AD3BA File Offset: 0x000AB5BA
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

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x06004116 RID: 16662 RVA: 0x000AD3C3 File Offset: 0x000AB5C3
		// (set) Token: 0x06004117 RID: 16663 RVA: 0x000AD3CB File Offset: 0x000AB5CB
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

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x06004118 RID: 16664 RVA: 0x000AD3D4 File Offset: 0x000AB5D4
		// (set) Token: 0x06004119 RID: 16665 RVA: 0x000AD3DC File Offset: 0x000AB5DC
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

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x0600411A RID: 16666 RVA: 0x000AD3E5 File Offset: 0x000AB5E5
		// (set) Token: 0x0600411B RID: 16667 RVA: 0x000AD3ED File Offset: 0x000AB5ED
		[DataMember]
		public bool IsDisabled
		{
			get
			{
				return this.IsDisabledField;
			}
			set
			{
				this.IsDisabledField = value;
			}
		}

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x0600411C RID: 16668 RVA: 0x000AD3F6 File Offset: 0x000AB5F6
		// (set) Token: 0x0600411D RID: 16669 RVA: 0x000AD3FE File Offset: 0x000AB5FE
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

		// Token: 0x0400384A RID: 14410
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400384B RID: 14411
		private string DomainNameField;

		// Token: 0x0400384C RID: 14412
		private bool IsDeletedField;

		// Token: 0x0400384D RID: 14413
		private bool IsDisabledField;

		// Token: 0x0400384E RID: 14414
		private Guid ZoneGUIDField;
	}
}
