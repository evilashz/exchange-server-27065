using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200002B RID: 43
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_ICalendarRepairLoggerSettings_Implementation_ : ICalendarRepairLoggerSettings, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_ICalendarRepairLoggerSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003A2E File Offset: 0x00001C2E
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00003A36 File Offset: 0x00001C36
		_DynamicStorageSelection_ICalendarRepairLoggerSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_ICalendarRepairLoggerSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003A3E File Offset: 0x00001C3E
		void IDataAccessorBackedObject<_DynamicStorageSelection_ICalendarRepairLoggerSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_ICalendarRepairLoggerSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003A4E File Offset: 0x00001C4E
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003A5B File Offset: 0x00001C5B
		public bool InsightLogEnabled
		{
			get
			{
				if (this.dataAccessor._InsightLogEnabled_ValueProvider_ != null)
				{
					return this.dataAccessor._InsightLogEnabled_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._InsightLogEnabled_MaterializedValue_;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003A8C File Offset: 0x00001C8C
		public string InsightLogDirectoryName
		{
			get
			{
				if (this.dataAccessor._InsightLogDirectoryName_ValueProvider_ != null)
				{
					return this.dataAccessor._InsightLogDirectoryName_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._InsightLogDirectoryName_MaterializedValue_;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003ABD File Offset: 0x00001CBD
		public TimeSpan InsightLogFileAgeInDays
		{
			get
			{
				if (this.dataAccessor._InsightLogFileAgeInDays_ValueProvider_ != null)
				{
					return this.dataAccessor._InsightLogFileAgeInDays_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._InsightLogFileAgeInDays_MaterializedValue_;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00003AEE File Offset: 0x00001CEE
		public ulong InsightLogDirectorySizeLimit
		{
			get
			{
				if (this.dataAccessor._InsightLogDirectorySizeLimit_ValueProvider_ != null)
				{
					return this.dataAccessor._InsightLogDirectorySizeLimit_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._InsightLogDirectorySizeLimit_MaterializedValue_;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003B1F File Offset: 0x00001D1F
		public ulong InsightLogFileSize
		{
			get
			{
				if (this.dataAccessor._InsightLogFileSize_ValueProvider_ != null)
				{
					return this.dataAccessor._InsightLogFileSize_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._InsightLogFileSize_MaterializedValue_;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003B50 File Offset: 0x00001D50
		public ulong InsightLogCacheSize
		{
			get
			{
				if (this.dataAccessor._InsightLogCacheSize_ValueProvider_ != null)
				{
					return this.dataAccessor._InsightLogCacheSize_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._InsightLogCacheSize_MaterializedValue_;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00003B81 File Offset: 0x00001D81
		public TimeSpan InsightLogFlushIntervalInSeconds
		{
			get
			{
				if (this.dataAccessor._InsightLogFlushIntervalInSeconds_ValueProvider_ != null)
				{
					return this.dataAccessor._InsightLogFlushIntervalInSeconds_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._InsightLogFlushIntervalInSeconds_MaterializedValue_;
			}
		}

		// Token: 0x0400008A RID: 138
		private _DynamicStorageSelection_ICalendarRepairLoggerSettings_DataAccessor_ dataAccessor;

		// Token: 0x0400008B RID: 139
		private VariantContextSnapshot context;
	}
}
