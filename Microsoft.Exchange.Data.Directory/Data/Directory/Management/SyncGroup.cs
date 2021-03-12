using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Provisioning;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200076B RID: 1899
	[ProvisioningObjectTag("SyncGroup")]
	[Serializable]
	public class SyncGroup : WindowsGroup
	{
		// Token: 0x170020B1 RID: 8369
		// (get) Token: 0x06005D53 RID: 23891 RVA: 0x00142316 File Offset: 0x00140516
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return SyncGroup.schema;
			}
		}

		// Token: 0x06005D54 RID: 23892 RVA: 0x0014231D File Offset: 0x0014051D
		public SyncGroup()
		{
			base.SetObjectClass("group");
		}

		// Token: 0x06005D55 RID: 23893 RVA: 0x00142330 File Offset: 0x00140530
		public SyncGroup(ADGroup dataObject) : base(dataObject)
		{
		}

		// Token: 0x06005D56 RID: 23894 RVA: 0x00142339 File Offset: 0x00140539
		internal new static SyncGroup FromDataObject(ADGroup dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new SyncGroup(dataObject);
		}

		// Token: 0x170020B2 RID: 8370
		// (get) Token: 0x06005D57 RID: 23895 RVA: 0x00142346 File Offset: 0x00140546
		// (set) Token: 0x06005D58 RID: 23896 RVA: 0x00142358 File Offset: 0x00140558
		[Parameter(Mandatory = false)]
		public bool IsDirSynced
		{
			get
			{
				return (bool)this[SyncGroupSchema.IsDirSynced];
			}
			set
			{
				this[SyncGroupSchema.IsDirSynced] = value;
			}
		}

		// Token: 0x170020B3 RID: 8371
		// (get) Token: 0x06005D59 RID: 23897 RVA: 0x0014236B File Offset: 0x0014056B
		// (set) Token: 0x06005D5A RID: 23898 RVA: 0x0014237D File Offset: 0x0014057D
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> DirSyncAuthorityMetadata
		{
			get
			{
				return (MultiValuedProperty<string>)this[SyncGroupSchema.DirSyncAuthorityMetadata];
			}
			set
			{
				this[SyncGroupSchema.DirSyncAuthorityMetadata] = value;
			}
		}

		// Token: 0x170020B4 RID: 8372
		// (get) Token: 0x06005D5B RID: 23899 RVA: 0x0014238B File Offset: 0x0014058B
		public string ExternalDirectoryObjectId
		{
			get
			{
				return (string)this[SyncGroupSchema.ExternalDirectoryObjectId];
			}
		}

		// Token: 0x170020B5 RID: 8373
		// (get) Token: 0x06005D5C RID: 23900 RVA: 0x0014239D File Offset: 0x0014059D
		// (set) Token: 0x06005D5D RID: 23901 RVA: 0x001423AF File Offset: 0x001405AF
		[Parameter(Mandatory = false)]
		public string OnPremisesObjectId
		{
			get
			{
				return (string)this[SyncGroupSchema.OnPremisesObjectId];
			}
			set
			{
				this[SyncGroupSchema.OnPremisesObjectId] = value;
			}
		}

		// Token: 0x04003F1B RID: 16155
		private static SyncGroupSchema schema = ObjectSchema.GetInstance<SyncGroupSchema>();
	}
}
