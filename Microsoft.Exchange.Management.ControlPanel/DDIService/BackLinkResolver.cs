using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000113 RID: 275
	public class BackLinkResolver : CmdletActivity
	{
		// Token: 0x06001FC3 RID: 8131 RVA: 0x0005FA0A File Offset: 0x0005DC0A
		public BackLinkResolver()
		{
			this.EnableFilter = true;
			this.FilterOperator = "eq";
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x0005FA24 File Offset: 0x0005DC24
		protected BackLinkResolver(BackLinkResolver activity) : base(activity)
		{
			this.LinkProperty = activity.LinkProperty;
			this.OutputVariable = activity.OutputVariable;
			this.EnableFilter = activity.EnableFilter;
			this.FilterOperator = activity.FilterOperator;
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x0005FA5D File Offset: 0x0005DC5D
		public override Activity Clone()
		{
			return new BackLinkResolver(this);
		}

		// Token: 0x17001A24 RID: 6692
		// (get) Token: 0x06001FC6 RID: 8134 RVA: 0x0005FA65 File Offset: 0x0005DC65
		// (set) Token: 0x06001FC7 RID: 8135 RVA: 0x0005FA6D File Offset: 0x0005DC6D
		[DDIMandatoryValue]
		public string LinkProperty { get; set; }

		// Token: 0x17001A25 RID: 6693
		// (get) Token: 0x06001FC8 RID: 8136 RVA: 0x0005FA76 File Offset: 0x0005DC76
		// (set) Token: 0x06001FC9 RID: 8137 RVA: 0x0005FA7E File Offset: 0x0005DC7E
		[DDIMandatoryValue]
		[DDIVariableNameExist]
		[DDIVaraibleWithoutDataObject]
		public string OutputVariable { get; set; }

		// Token: 0x17001A26 RID: 6694
		// (get) Token: 0x06001FCA RID: 8138 RVA: 0x0005FA87 File Offset: 0x0005DC87
		// (set) Token: 0x06001FCB RID: 8139 RVA: 0x0005FA8F File Offset: 0x0005DC8F
		public bool EnableFilter { get; set; }

		// Token: 0x17001A27 RID: 6695
		// (get) Token: 0x06001FCC RID: 8140 RVA: 0x0005FA98 File Offset: 0x0005DC98
		// (set) Token: 0x06001FCD RID: 8141 RVA: 0x0005FAA0 File Offset: 0x0005DCA0
		public string FilterOperator { get; set; }

		// Token: 0x06001FCE RID: 8142 RVA: 0x0005FAAC File Offset: 0x0005DCAC
		public override RunResult Run(DataRow input, DataTable dataTable, DataObjectStore store, Type codeBehind, Workflow.UpdateTableDelegate updateTableDelegate)
		{
			DDIHelper.CheckDataTableForSingleObject(dataTable);
			DataRow dataRow = dataTable.Rows[0];
			if (!(dataRow[base.IdentityVariable] is ADObjectId))
			{
				throw new NotSupportedException("Currently we don't support Back-Link look up based on the UMC type Identity!");
			}
			if (this.EnableFilter)
			{
				base.Command.AddParameter("Filter", string.Format("{0} -{1} '{2}'", this.LinkProperty, this.FilterOperator, DDIHelper.ToQuotationEscapedString(((ADObjectId)dataRow[base.IdentityVariable]).DistinguishedName)));
			}
			RunResult runResult = new RunResult();
			PowerShellResults<PSObject> powerShellResults;
			base.ExecuteCmdlet(null, runResult, out powerShellResults, false);
			if (!runResult.ErrorOccur)
			{
				List<ADObjectId> list = new List<ADObjectId>();
				foreach (PSObject psobject in powerShellResults.Output)
				{
					if (!this.EnableFilter)
					{
						ADObjectId adobjectId = (ADObjectId)psobject.Properties[this.LinkProperty].Value;
						if (adobjectId != null && adobjectId.Equals(dataRow[base.IdentityVariable]))
						{
							list.Add(psobject.Properties["Identity"].Value as ADObjectId);
						}
					}
					else
					{
						list.Add(psobject.Properties["Identity"].Value as ADObjectId);
					}
				}
				dataRow[this.OutputVariable] = list;
			}
			return runResult;
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x0005FC0F File Offset: 0x0005DE0F
		protected override bool IsToExecuteCmdlet(DataRow input, DataTable dataTable, DataObjectStore store, List<Parameter> parameters)
		{
			return dataTable.Rows[0][base.IdentityVariable] is ADObjectId;
		}

		// Token: 0x06001FD0 RID: 8144 RVA: 0x0005FC38 File Offset: 0x0005DE38
		internal override bool HasPermission(DataRow input, DataTable dataTable, DataObjectStore store, Variable updatingVariable)
		{
			List<string> list = (from c in this.GetEffectiveParameters(input, dataTable, store)
			select c.Name).ToList<string>();
			if (this.EnableFilter && !list.Contains("Filter", StringComparer.OrdinalIgnoreCase))
			{
				list.Add("Filter");
			}
			return this.CheckPermission(store, list, updatingVariable);
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x0005FCA5 File Offset: 0x0005DEA5
		internal override bool HasOutputVariable(string variable)
		{
			return this.OutputVariable == variable;
		}

		// Token: 0x04001C90 RID: 7312
		private const string Filter = "Filter";
	}
}
