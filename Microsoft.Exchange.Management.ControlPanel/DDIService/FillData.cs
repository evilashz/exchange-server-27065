using System;
using System.Data;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200012E RID: 302
	public class FillData : Activity
	{
		// Token: 0x060020B5 RID: 8373 RVA: 0x0006326E File Offset: 0x0006146E
		public FillData()
		{
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x00063276 File Offset: 0x00061476
		protected FillData(FillData activity) : base(activity)
		{
			this.LoadDataAction = activity.LoadDataAction;
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x0006328B File Offset: 0x0006148B
		public override Activity Clone()
		{
			return new FillData(this);
		}

		// Token: 0x17001A3B RID: 6715
		// (get) Token: 0x060020B8 RID: 8376 RVA: 0x00063293 File Offset: 0x00061493
		// (set) Token: 0x060020B9 RID: 8377 RVA: 0x0006329B File Offset: 0x0006149B
		[DDIValidCodeBehindMethod]
		public string LoadDataAction { get; set; }

		// Token: 0x060020BA RID: 8378 RVA: 0x000632A4 File Offset: 0x000614A4
		public override RunResult Run(DataRow input, DataTable dataTable, DataObjectStore store, Type codeBehind, Workflow.UpdateTableDelegate updateTableDelegate)
		{
			RunResult result = new RunResult();
			if (null != codeBehind && !string.IsNullOrEmpty(this.LoadDataAction))
			{
				DDIHelper.Trace("LoadDataAction: " + this.LoadDataAction);
				codeBehind.GetMethod(this.LoadDataAction).Invoke(null, new object[]
				{
					input,
					dataTable,
					store
				});
			}
			return result;
		}
	}
}
