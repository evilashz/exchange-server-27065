using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.TextProcessing.Boomerang
{
	// Token: 0x02000020 RID: 32
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DataOnly_IBoomerangSettings_Implementation_ : IBoomerangSettings, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000038C0 File Offset: 0x00001AC0
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000038C3 File Offset: 0x00001AC3
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000038C6 File Offset: 0x00001AC6
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000038CE File Offset: 0x00001ACE
		public bool Enabled
		{
			get
			{
				return this._Enabled_MaterializedValue_;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000038D6 File Offset: 0x00001AD6
		public string MasterKeyRegistryPath
		{
			get
			{
				return this._MasterKeyRegistryPath_MaterializedValue_;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000038DE File Offset: 0x00001ADE
		public string MasterKeyRegistryKeyName
		{
			get
			{
				return this._MasterKeyRegistryKeyName_MaterializedValue_;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000038E6 File Offset: 0x00001AE6
		public uint NumberOfValidIntervalsInDays
		{
			get
			{
				return this._NumberOfValidIntervalsInDays_MaterializedValue_;
			}
		}

		// Token: 0x04000068 RID: 104
		internal string _Name_MaterializedValue_;

		// Token: 0x04000069 RID: 105
		internal bool _Enabled_MaterializedValue_;

		// Token: 0x0400006A RID: 106
		internal string _MasterKeyRegistryPath_MaterializedValue_;

		// Token: 0x0400006B RID: 107
		internal string _MasterKeyRegistryKeyName_MaterializedValue_;

		// Token: 0x0400006C RID: 108
		internal uint _NumberOfValidIntervalsInDays_MaterializedValue_;
	}
}
