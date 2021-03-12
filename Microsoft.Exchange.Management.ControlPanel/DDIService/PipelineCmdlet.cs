using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000129 RID: 297
	public abstract class PipelineCmdlet : CmdletActivity
	{
		// Token: 0x0600209B RID: 8347 RVA: 0x00062CDD File Offset: 0x00060EDD
		public PipelineCmdlet()
		{
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x00062CE5 File Offset: 0x00060EE5
		protected PipelineCmdlet(PipelineCmdlet activity) : base(activity)
		{
			this.SingletonObject = activity.SingletonObject;
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x00062CFC File Offset: 0x00060EFC
		protected override void DoPreRunCore(DataRow input, DataTable dataTable, DataObjectStore store, Type codeBehind)
		{
			if (!this.SingletonObject && !(input[base.IdentityVariable] is Identity[]))
			{
				Parameter item = new Parameter
				{
					Name = base.IdentityName,
					Reference = base.IdentityVariable,
					Type = ParameterType.Mandatory
				};
				if (!base.Parameters.Contains(item))
				{
					base.Parameters.Add(item);
				}
			}
			base.DoPreRunCore(input, dataTable, store, codeBehind);
		}

		// Token: 0x17001A3A RID: 6714
		// (get) Token: 0x0600209E RID: 8350 RVA: 0x00062D72 File Offset: 0x00060F72
		// (set) Token: 0x0600209F RID: 8351 RVA: 0x00062D7A File Offset: 0x00060F7A
		[DefaultValue(false)]
		public bool SingletonObject
		{
			get
			{
				return this.singletonObject;
			}
			set
			{
				if (value)
				{
					base.IdentityVariable = string.Empty;
				}
				this.singletonObject = value;
			}
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x00062D94 File Offset: 0x00060F94
		public override RunResult Run(DataRow input, DataTable dataTable, DataObjectStore store, Type codeBehind, Workflow.UpdateTableDelegate updateTableDelegate)
		{
			Identity[] array = this.PreparePipelineExecution(input, dataTable);
			if (array == null || this.SingletonObject)
			{
				return base.Run(input, dataTable, store, codeBehind, updateTableDelegate);
			}
			if (array.Length == 0)
			{
				throw new InvalidOperationException();
			}
			RunResult runResult = new RunResult();
			PowerShellResults<PSObject> powerShellResults;
			base.ExecuteCmdlet(array, runResult, out powerShellResults, false);
			return runResult;
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x00062DF4 File Offset: 0x00060FF4
		protected Identity[] PreparePipelineExecution(DataRow input, DataTable dataTable)
		{
			Identity[] array = null;
			if (!this.SingletonObject)
			{
				array = (input[base.IdentityVariable] as Identity[]);
				if (array != null)
				{
					IEnumerable<CommandParameter> source = from c in base.Command.Commands[0].Parameters
					where c.Name == base.IdentityName
					select c;
					if (source.Count<CommandParameter>() > 0)
					{
						base.Command.Commands[0].Parameters.Remove(source.First<CommandParameter>());
					}
				}
			}
			return array;
		}

		// Token: 0x04001CED RID: 7405
		private bool singletonObject;
	}
}
