using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008B2 RID: 2226
	[Serializable]
	public sealed class DatabaseMoveResult : IConfigurable
	{
		// Token: 0x17001785 RID: 6021
		// (get) Token: 0x06004EB3 RID: 20147 RVA: 0x00147487 File Offset: 0x00145687
		// (set) Token: 0x06004EB4 RID: 20148 RVA: 0x0014748F File Offset: 0x0014568F
		internal Guid Guid { get; set; }

		// Token: 0x17001786 RID: 6022
		// (get) Token: 0x06004EB5 RID: 20149 RVA: 0x00147498 File Offset: 0x00145698
		// (set) Token: 0x06004EB6 RID: 20150 RVA: 0x001474A0 File Offset: 0x001456A0
		public ObjectId Identity { get; internal set; }

		// Token: 0x17001787 RID: 6023
		// (get) Token: 0x06004EB7 RID: 20151 RVA: 0x001474A9 File Offset: 0x001456A9
		// (set) Token: 0x06004EB8 RID: 20152 RVA: 0x001474B1 File Offset: 0x001456B1
		public string ActiveServerAtStart { get; internal set; }

		// Token: 0x17001788 RID: 6024
		// (get) Token: 0x06004EB9 RID: 20153 RVA: 0x001474BA File Offset: 0x001456BA
		// (set) Token: 0x06004EBA RID: 20154 RVA: 0x001474C2 File Offset: 0x001456C2
		public string ActiveServerAtEnd { get; internal set; }

		// Token: 0x17001789 RID: 6025
		// (get) Token: 0x06004EBB RID: 20155 RVA: 0x001474CB File Offset: 0x001456CB
		// (set) Token: 0x06004EBC RID: 20156 RVA: 0x001474D3 File Offset: 0x001456D3
		public MoveStatus Status { get; internal set; }

		// Token: 0x1700178A RID: 6026
		// (get) Token: 0x06004EBD RID: 20157 RVA: 0x001474DC File Offset: 0x001456DC
		// (set) Token: 0x06004EBE RID: 20158 RVA: 0x001474E4 File Offset: 0x001456E4
		public string ErrorMessage { get; internal set; }

		// Token: 0x1700178B RID: 6027
		// (get) Token: 0x06004EBF RID: 20159 RVA: 0x001474ED File Offset: 0x001456ED
		// (set) Token: 0x06004EC0 RID: 20160 RVA: 0x001474F5 File Offset: 0x001456F5
		public Exception Exception { get; internal set; }

		// Token: 0x1700178C RID: 6028
		// (get) Token: 0x06004EC1 RID: 20161 RVA: 0x001474FE File Offset: 0x001456FE
		// (set) Token: 0x06004EC2 RID: 20162 RVA: 0x00147506 File Offset: 0x00145706
		public MountStatus MountStatusAtMoveStart { get; internal set; }

		// Token: 0x1700178D RID: 6029
		// (get) Token: 0x06004EC3 RID: 20163 RVA: 0x0014750F File Offset: 0x0014570F
		// (set) Token: 0x06004EC4 RID: 20164 RVA: 0x00147517 File Offset: 0x00145717
		public MountStatus MountStatusAtMoveEnd { get; internal set; }

		// Token: 0x1700178E RID: 6030
		// (get) Token: 0x06004EC5 RID: 20165 RVA: 0x00147520 File Offset: 0x00145720
		// (set) Token: 0x06004EC6 RID: 20166 RVA: 0x00147528 File Offset: 0x00145728
		public long? NumberOfLogsLost { get; internal set; }

		// Token: 0x1700178F RID: 6031
		// (get) Token: 0x06004EC7 RID: 20167 RVA: 0x00147531 File Offset: 0x00145731
		// (set) Token: 0x06004EC8 RID: 20168 RVA: 0x00147539 File Offset: 0x00145739
		public DateTime? RecoveryPointObjective { get; internal set; }

		// Token: 0x17001790 RID: 6032
		// (get) Token: 0x06004EC9 RID: 20169 RVA: 0x00147542 File Offset: 0x00145742
		internal bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001791 RID: 6033
		// (get) Token: 0x06004ECA RID: 20170 RVA: 0x00147545 File Offset: 0x00145745
		bool IConfigurable.IsValid
		{
			get
			{
				return this.IsValid;
			}
		}

		// Token: 0x17001792 RID: 6034
		// (get) Token: 0x06004ECB RID: 20171 RVA: 0x0014754D File Offset: 0x0014574D
		internal ObjectState ObjectState
		{
			get
			{
				return ObjectState.Unchanged;
			}
		}

		// Token: 0x17001793 RID: 6035
		// (get) Token: 0x06004ECC RID: 20172 RVA: 0x00147550 File Offset: 0x00145750
		ObjectState IConfigurable.ObjectState
		{
			get
			{
				return this.ObjectState;
			}
		}

		// Token: 0x06004ECD RID: 20173 RVA: 0x00147558 File Offset: 0x00145758
		public ValidationError[] Validate()
		{
			return new ValidationError[0];
		}

		// Token: 0x06004ECE RID: 20174 RVA: 0x00147560 File Offset: 0x00145760
		public void CopyChangesFrom(IConfigurable source)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004ECF RID: 20175 RVA: 0x00147567 File Offset: 0x00145767
		public void ResetChangeTracking()
		{
			throw new NotImplementedException();
		}
	}
}
