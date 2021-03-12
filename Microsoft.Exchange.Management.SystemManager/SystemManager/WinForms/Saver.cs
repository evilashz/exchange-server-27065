using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Exchange.Configuration.MonadDataProvider;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000059 RID: 89
	public abstract class Saver : RunnerBase
	{
		// Token: 0x0600039F RID: 927 RVA: 0x0000D160 File Offset: 0x0000B360
		public Saver(string workUnitTextColumn, string workUnitIconColumn)
		{
			this.workUnitTextColumn = workUnitTextColumn;
			this.workUnitIconColumn = workUnitIconColumn;
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000D176 File Offset: 0x0000B376
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x0000D17E File Offset: 0x0000B37E
		[DefaultValue(null)]
		[DDIDataColumnExist]
		public string WorkUnitTextColumn
		{
			get
			{
				return this.workUnitTextColumn;
			}
			set
			{
				this.workUnitTextColumn = value;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000D187 File Offset: 0x0000B387
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x0000D18F File Offset: 0x0000B38F
		[DDIDataColumnExist]
		[DefaultValue(null)]
		public string WorkUnitIconColumn
		{
			get
			{
				return this.workUnitIconColumn;
			}
			set
			{
				this.workUnitIconColumn = value;
			}
		}

		// Token: 0x060003A4 RID: 932
		public abstract void UpdateWorkUnits(DataRow row);

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003A5 RID: 933
		public abstract object WorkUnits { get; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003A6 RID: 934
		public abstract List<object> SavedResults { get; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003A7 RID: 935
		public abstract string CommandToRun { get; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003A8 RID: 936
		public abstract string ModifiedParametersDescription { get; }

		// Token: 0x060003A9 RID: 937 RVA: 0x0000D198 File Offset: 0x0000B398
		public virtual Saver CreateBulkSaver(WorkUnit[] workunits)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000D19F File Offset: 0x0000B39F
		public virtual bool HasPermission(string propertyName, IList<ParameterProfile> parameters)
		{
			return true;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000D1A2 File Offset: 0x0000B3A2
		public virtual string GetConsumedDataObjectName()
		{
			return null;
		}

		// Token: 0x040000F3 RID: 243
		private string workUnitTextColumn;

		// Token: 0x040000F4 RID: 244
		private string workUnitIconColumn;
	}
}
