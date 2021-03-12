using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.TextProcessing.Boomerang
{
	// Token: 0x0200001F RID: 31
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IBoomerangSettings_Implementation_ : IBoomerangSettings, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IBoomerangSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000037C7 File Offset: 0x000019C7
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000037CF File Offset: 0x000019CF
		_DynamicStorageSelection_IBoomerangSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IBoomerangSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000037D7 File Offset: 0x000019D7
		void IDataAccessorBackedObject<_DynamicStorageSelection_IBoomerangSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_IBoomerangSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000037E7 File Offset: 0x000019E7
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000037F4 File Offset: 0x000019F4
		public bool Enabled
		{
			get
			{
				if (this.dataAccessor._Enabled_ValueProvider_ != null)
				{
					return this.dataAccessor._Enabled_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._Enabled_MaterializedValue_;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003825 File Offset: 0x00001A25
		public string MasterKeyRegistryPath
		{
			get
			{
				if (this.dataAccessor._MasterKeyRegistryPath_ValueProvider_ != null)
				{
					return this.dataAccessor._MasterKeyRegistryPath_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MasterKeyRegistryPath_MaterializedValue_;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003856 File Offset: 0x00001A56
		public string MasterKeyRegistryKeyName
		{
			get
			{
				if (this.dataAccessor._MasterKeyRegistryKeyName_ValueProvider_ != null)
				{
					return this.dataAccessor._MasterKeyRegistryKeyName_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MasterKeyRegistryKeyName_MaterializedValue_;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003887 File Offset: 0x00001A87
		public uint NumberOfValidIntervalsInDays
		{
			get
			{
				if (this.dataAccessor._NumberOfValidIntervalsInDays_ValueProvider_ != null)
				{
					return this.dataAccessor._NumberOfValidIntervalsInDays_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._NumberOfValidIntervalsInDays_MaterializedValue_;
			}
		}

		// Token: 0x04000066 RID: 102
		private _DynamicStorageSelection_IBoomerangSettings_DataAccessor_ dataAccessor;

		// Token: 0x04000067 RID: 103
		private VariantContextSnapshot context;
	}
}
