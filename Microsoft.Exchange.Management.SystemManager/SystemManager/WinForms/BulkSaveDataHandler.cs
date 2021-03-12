using System;
using System.Text;
using Microsoft.Exchange.Configuration.MonadDataProvider;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000069 RID: 105
	internal class BulkSaveDataHandler : SingleTaskDataHandler
	{
		// Token: 0x060003FD RID: 1021 RVA: 0x0000EFCC File Offset: 0x0000D1CC
		public BulkSaveDataHandler(WorkUnit[] workUnits, string saveCommand) : base(saveCommand)
		{
			base.WorkUnits.AddRange(workUnits);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000EFE4 File Offset: 0x0000D1E4
		public override void UpdateWorkUnits()
		{
			base.UpdateWorkUnits();
			foreach (WorkUnit workUnit in base.WorkUnits)
			{
				workUnit.ResetStatus();
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000F038 File Offset: 0x0000D238
		internal override string CommandToRun
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = true;
				foreach (WorkUnit workUnit in base.WorkUnits)
				{
					if (!flag)
					{
						stringBuilder.Append(",");
					}
					flag = false;
					stringBuilder.Append(MonadCommand.FormatParameterValue(workUnit.Target));
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" | ");
				}
				stringBuilder.Append(base.CommandToRun);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000F0D4 File Offset: 0x0000D2D4
		protected override void HandleIdentityParameter()
		{
		}
	}
}
