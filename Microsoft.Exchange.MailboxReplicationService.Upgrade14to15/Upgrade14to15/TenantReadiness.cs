using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200007A RID: 122
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "TenantReadiness", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.Common.DataContract")]
	public class TenantReadiness : IExtensibleDataObject
	{
		// Token: 0x060002F1 RID: 753 RVA: 0x00003F23 File Offset: 0x00002123
		public TenantReadiness(string[] constraints, string groupName, bool isReady, Guid tenantId, int upgradeUnits, bool useDefaultCapacity)
		{
			this.ConstraintsField = constraints;
			this.GroupNameField = groupName;
			this.IsReadyField = isReady;
			this.TenantIdField = tenantId;
			this.UpgradeUnitsField = upgradeUnits;
			this.UseDefaultCapacityField = useDefaultCapacity;
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00003F58 File Offset: 0x00002158
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x00003F60 File Offset: 0x00002160
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

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00003F69 File Offset: 0x00002169
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x00003F71 File Offset: 0x00002171
		[DataMember]
		public string[] Constraints
		{
			get
			{
				return this.ConstraintsField;
			}
			set
			{
				this.ConstraintsField = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00003F7A File Offset: 0x0000217A
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x00003F82 File Offset: 0x00002182
		[DataMember]
		public string GroupName
		{
			get
			{
				return this.GroupNameField;
			}
			set
			{
				this.GroupNameField = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00003F8B File Offset: 0x0000218B
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x00003F93 File Offset: 0x00002193
		[DataMember]
		public bool IsReady
		{
			get
			{
				return this.IsReadyField;
			}
			set
			{
				this.IsReadyField = value;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00003F9C File Offset: 0x0000219C
		// (set) Token: 0x060002FB RID: 763 RVA: 0x00003FA4 File Offset: 0x000021A4
		[DataMember]
		public Guid TenantId
		{
			get
			{
				return this.TenantIdField;
			}
			set
			{
				this.TenantIdField = value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060002FC RID: 764 RVA: 0x00003FAD File Offset: 0x000021AD
		// (set) Token: 0x060002FD RID: 765 RVA: 0x00003FB5 File Offset: 0x000021B5
		[DataMember]
		public int UpgradeUnits
		{
			get
			{
				return this.UpgradeUnitsField;
			}
			set
			{
				this.UpgradeUnitsField = value;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00003FBE File Offset: 0x000021BE
		// (set) Token: 0x060002FF RID: 767 RVA: 0x00003FC6 File Offset: 0x000021C6
		[DataMember]
		public bool UseDefaultCapacity
		{
			get
			{
				return this.UseDefaultCapacityField;
			}
			set
			{
				this.UseDefaultCapacityField = value;
			}
		}

		// Token: 0x04000152 RID: 338
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000153 RID: 339
		private string[] ConstraintsField;

		// Token: 0x04000154 RID: 340
		private string GroupNameField;

		// Token: 0x04000155 RID: 341
		private bool IsReadyField;

		// Token: 0x04000156 RID: 342
		private Guid TenantIdField;

		// Token: 0x04000157 RID: 343
		private int UpgradeUnitsField;

		// Token: 0x04000158 RID: 344
		private bool UseDefaultCapacityField;
	}
}
