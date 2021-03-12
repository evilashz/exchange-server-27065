using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200002C RID: 44
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DataOnly_ICalendarRepairLoggerSettings_Implementation_ : ICalendarRepairLoggerSettings, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00003BBA File Offset: 0x00001DBA
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003BBD File Offset: 0x00001DBD
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00003BC0 File Offset: 0x00001DC0
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00003BC8 File Offset: 0x00001DC8
		public bool InsightLogEnabled
		{
			get
			{
				return this._InsightLogEnabled_MaterializedValue_;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003BD0 File Offset: 0x00001DD0
		public string InsightLogDirectoryName
		{
			get
			{
				return this._InsightLogDirectoryName_MaterializedValue_;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00003BD8 File Offset: 0x00001DD8
		public TimeSpan InsightLogFileAgeInDays
		{
			get
			{
				return this._InsightLogFileAgeInDays_MaterializedValue_;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00003BE0 File Offset: 0x00001DE0
		public ulong InsightLogDirectorySizeLimit
		{
			get
			{
				return this._InsightLogDirectorySizeLimit_MaterializedValue_;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public ulong InsightLogFileSize
		{
			get
			{
				return this._InsightLogFileSize_MaterializedValue_;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00003BF0 File Offset: 0x00001DF0
		public ulong InsightLogCacheSize
		{
			get
			{
				return this._InsightLogCacheSize_MaterializedValue_;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003BF8 File Offset: 0x00001DF8
		public TimeSpan InsightLogFlushIntervalInSeconds
		{
			get
			{
				return this._InsightLogFlushIntervalInSeconds_MaterializedValue_;
			}
		}

		// Token: 0x0400008C RID: 140
		internal string _Name_MaterializedValue_;

		// Token: 0x0400008D RID: 141
		internal bool _InsightLogEnabled_MaterializedValue_;

		// Token: 0x0400008E RID: 142
		internal string _InsightLogDirectoryName_MaterializedValue_;

		// Token: 0x0400008F RID: 143
		internal TimeSpan _InsightLogFileAgeInDays_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000090 RID: 144
		internal ulong _InsightLogDirectorySizeLimit_MaterializedValue_;

		// Token: 0x04000091 RID: 145
		internal ulong _InsightLogFileSize_MaterializedValue_;

		// Token: 0x04000092 RID: 146
		internal ulong _InsightLogCacheSize_MaterializedValue_;

		// Token: 0x04000093 RID: 147
		internal TimeSpan _InsightLogFlushIntervalInSeconds_MaterializedValue_ = default(TimeSpan);
	}
}
