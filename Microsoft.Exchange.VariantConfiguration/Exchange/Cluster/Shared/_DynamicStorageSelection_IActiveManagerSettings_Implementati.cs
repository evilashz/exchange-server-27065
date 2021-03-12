using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x02000017 RID: 23
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IActiveManagerSettings_Implementation_ : IActiveManagerSettings, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IActiveManagerSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003529 File Offset: 0x00001729
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003531 File Offset: 0x00001731
		_DynamicStorageSelection_IActiveManagerSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IActiveManagerSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003539 File Offset: 0x00001739
		void IDataAccessorBackedObject<_DynamicStorageSelection_IActiveManagerSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_IActiveManagerSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003549 File Offset: 0x00001749
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003556 File Offset: 0x00001756
		public DxStoreMode DxStoreRunMode
		{
			get
			{
				if (this.dataAccessor._DxStoreRunMode_ValueProvider_ != null)
				{
					return this.dataAccessor._DxStoreRunMode_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._DxStoreRunMode_MaterializedValue_;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003587 File Offset: 0x00001787
		public bool DxStoreIsUseHttpForInstanceCommunication
		{
			get
			{
				if (this.dataAccessor._DxStoreIsUseHttpForInstanceCommunication_ValueProvider_ != null)
				{
					return this.dataAccessor._DxStoreIsUseHttpForInstanceCommunication_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._DxStoreIsUseHttpForInstanceCommunication_MaterializedValue_;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000035B8 File Offset: 0x000017B8
		public bool DxStoreIsUseHttpForClientCommunication
		{
			get
			{
				if (this.dataAccessor._DxStoreIsUseHttpForClientCommunication_ValueProvider_ != null)
				{
					return this.dataAccessor._DxStoreIsUseHttpForClientCommunication_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._DxStoreIsUseHttpForClientCommunication_MaterializedValue_;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000035E9 File Offset: 0x000017E9
		public bool DxStoreIsEncryptionEnabled
		{
			get
			{
				if (this.dataAccessor._DxStoreIsEncryptionEnabled_ValueProvider_ != null)
				{
					return this.dataAccessor._DxStoreIsEncryptionEnabled_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._DxStoreIsEncryptionEnabled_MaterializedValue_;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000361A File Offset: 0x0000181A
		public bool DxStoreIsPeriodicFixupEnabled
		{
			get
			{
				if (this.dataAccessor._DxStoreIsPeriodicFixupEnabled_ValueProvider_ != null)
				{
					return this.dataAccessor._DxStoreIsPeriodicFixupEnabled_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._DxStoreIsPeriodicFixupEnabled_MaterializedValue_;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000364B File Offset: 0x0000184B
		public bool DxStoreIsUseBinarySerializerForClientCommunication
		{
			get
			{
				if (this.dataAccessor._DxStoreIsUseBinarySerializerForClientCommunication_ValueProvider_ != null)
				{
					return this.dataAccessor._DxStoreIsUseBinarySerializerForClientCommunication_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._DxStoreIsUseBinarySerializerForClientCommunication_MaterializedValue_;
			}
		}

		// Token: 0x0400004A RID: 74
		private _DynamicStorageSelection_IActiveManagerSettings_DataAccessor_ dataAccessor;

		// Token: 0x0400004B RID: 75
		private VariantContextSnapshot context;
	}
}
