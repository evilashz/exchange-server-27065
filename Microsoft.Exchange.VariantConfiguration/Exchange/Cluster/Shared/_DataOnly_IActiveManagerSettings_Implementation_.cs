using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x02000018 RID: 24
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DataOnly_IActiveManagerSettings_Implementation_ : IActiveManagerSettings, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003684 File Offset: 0x00001884
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003687 File Offset: 0x00001887
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006E RID: 110 RVA: 0x0000368A File Offset: 0x0000188A
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003692 File Offset: 0x00001892
		public DxStoreMode DxStoreRunMode
		{
			get
			{
				return this._DxStoreRunMode_MaterializedValue_;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000369A File Offset: 0x0000189A
		public bool DxStoreIsUseHttpForInstanceCommunication
		{
			get
			{
				return this._DxStoreIsUseHttpForInstanceCommunication_MaterializedValue_;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000036A2 File Offset: 0x000018A2
		public bool DxStoreIsUseHttpForClientCommunication
		{
			get
			{
				return this._DxStoreIsUseHttpForClientCommunication_MaterializedValue_;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000036AA File Offset: 0x000018AA
		public bool DxStoreIsEncryptionEnabled
		{
			get
			{
				return this._DxStoreIsEncryptionEnabled_MaterializedValue_;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000036B2 File Offset: 0x000018B2
		public bool DxStoreIsPeriodicFixupEnabled
		{
			get
			{
				return this._DxStoreIsPeriodicFixupEnabled_MaterializedValue_;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000036BA File Offset: 0x000018BA
		public bool DxStoreIsUseBinarySerializerForClientCommunication
		{
			get
			{
				return this._DxStoreIsUseBinarySerializerForClientCommunication_MaterializedValue_;
			}
		}

		// Token: 0x0400004C RID: 76
		internal string _Name_MaterializedValue_;

		// Token: 0x0400004D RID: 77
		internal DxStoreMode _DxStoreRunMode_MaterializedValue_;

		// Token: 0x0400004E RID: 78
		internal bool _DxStoreIsUseHttpForInstanceCommunication_MaterializedValue_;

		// Token: 0x0400004F RID: 79
		internal bool _DxStoreIsUseHttpForClientCommunication_MaterializedValue_;

		// Token: 0x04000050 RID: 80
		internal bool _DxStoreIsEncryptionEnabled_MaterializedValue_;

		// Token: 0x04000051 RID: 81
		internal bool _DxStoreIsPeriodicFixupEnabled_MaterializedValue_;

		// Token: 0x04000052 RID: 82
		internal bool _DxStoreIsUseBinarySerializerForClientCommunication_MaterializedValue_;
	}
}
