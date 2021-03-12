using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000131 RID: 305
	public class GetCmdlet : OutputObjectCmdlet, INotAccessChecker
	{
		// Token: 0x060020D3 RID: 8403 RVA: 0x0006377D File Offset: 0x0006197D
		public GetCmdlet()
		{
			base.AllowExceuteThruHttpGetRequest = true;
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x0006378C File Offset: 0x0006198C
		protected GetCmdlet(GetCmdlet activity) : base(activity)
		{
			this.SingletonObject = activity.SingletonObject;
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x000637A1 File Offset: 0x000619A1
		public override Activity Clone()
		{
			return new GetCmdlet(this);
		}

		// Token: 0x17001A42 RID: 6722
		// (get) Token: 0x060020D6 RID: 8406 RVA: 0x000637A9 File Offset: 0x000619A9
		// (set) Token: 0x060020D7 RID: 8407 RVA: 0x000637B1 File Offset: 0x000619B1
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

		// Token: 0x060020D8 RID: 8408 RVA: 0x000637D0 File Offset: 0x000619D0
		internal override bool HasPermission(DataRow input, DataTable dataTable, DataObjectStore store, Variable updatingVariable)
		{
			List<string> list = (from c in this.GetEffectiveParameters(input, dataTable, store)
			select c.Name).ToList<string>();
			if (!this.SingletonObject && !list.Contains(base.IdentityName, StringComparer.OrdinalIgnoreCase))
			{
				list.Add(base.IdentityName);
			}
			return this.CheckPermission(store, list, updatingVariable);
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x0006383F File Offset: 0x00061A3F
		protected override string GetVerb()
		{
			return "Get-";
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x00063848 File Offset: 0x00061A48
		protected override void DoPreRunCore(DataRow input, DataTable dataTable, DataObjectStore store, Type codeBehind)
		{
			if (!this.SingletonObject)
			{
				DDIHelper.Trace("Map identity parameter : {0} - {1}", new object[]
				{
					base.IdentityName,
					base.IdentityVariable
				});
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

		// Token: 0x060020DB RID: 8411 RVA: 0x000638D0 File Offset: 0x00061AD0
		public override IList<Parameter> GetEffectiveParameters(DataRow input, DataTable dataTable, DataObjectStore store)
		{
			IList<Parameter> list = base.GetEffectiveParameters(input, dataTable, store) ?? new List<Parameter>();
			string commandText = base.GetCommandText(store);
			if (!this.SingletonObject && GetCmdlet.SupportReadFromDcCmdlets.Contains(commandText, StringComparer.OrdinalIgnoreCase) && !DDIHelper.IsFFO())
			{
				Parameter item = new Parameter
				{
					Name = "ReadFromDomainController",
					Type = ParameterType.Switch
				};
				if (!list.Contains(item))
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x04001D01 RID: 7425
		private static readonly ICollection<string> SupportReadFromDcCmdlets = new string[]
		{
			"Get-Contact",
			"Get-DistributionGroup",
			"Get-DynamicDistributionGroup",
			"Get-Group",
			"Get-Mailbox",
			"Get-MailContact",
			"Get-MailUser",
			"Get-Recipient",
			"Get-SiteMailbox",
			"Get-User"
		};

		// Token: 0x04001D02 RID: 7426
		private bool singletonObject;
	}
}
