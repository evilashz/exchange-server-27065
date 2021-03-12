using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Exchange.Configuration.MonadDataProvider;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000B2 RID: 178
	public abstract class TaskProfileBase
	{
		// Token: 0x060005BD RID: 1469 RVA: 0x00015B71 File Offset: 0x00013D71
		public TaskProfileBase()
		{
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00015B84 File Offset: 0x00013D84
		public TaskProfileBase(string name, RunnerBase runner)
		{
			this.name = name;
			this.runner = runner;
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x00015BA5 File Offset: 0x00013DA5
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x00015BAD File Offset: 0x00013DAD
		public ParameterProfileList ParameterProfileList
		{
			get
			{
				return this.parameterProfileList;
			}
			set
			{
				this.parameterProfileList = value;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x00015BB6 File Offset: 0x00013DB6
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x00015BBE File Offset: 0x00013DBE
		[DDIMandatoryValue]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x00015BC7 File Offset: 0x00013DC7
		// (set) Token: 0x060005C4 RID: 1476 RVA: 0x00015BCF File Offset: 0x00013DCF
		public RunnerBase Runner
		{
			get
			{
				return this.runner;
			}
			set
			{
				this.runner = value;
			}
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00015BD8 File Offset: 0x00013DD8
		public void AddParameterProfile(ParameterProfile profile)
		{
			this.parameterProfileList.Add(profile);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00015BE6 File Offset: 0x00013DE6
		public bool IsRunnable(DataRow row, DataObjectStore store)
		{
			return this.runner.IsRunnable(row, store);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00015BF5 File Offset: 0x00013DF5
		public void BuildParameters(DataRow row, DataObjectStore store)
		{
			this.runner.BuildParameters(row, store, this.GetEffectiveParameters());
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x00015C0A File Offset: 0x00013E0A
		// (set) Token: 0x060005C9 RID: 1481 RVA: 0x00015C12 File Offset: 0x00013E12
		[DefaultValue(false)]
		public bool IgnoreException { get; set; }

		// Token: 0x060005CA RID: 1482
		internal abstract void Run(CommandInteractionHandler interactionHandler, DataRow row, DataObjectStore store);

		// Token: 0x060005CB RID: 1483 RVA: 0x00015C1C File Offset: 0x00013E1C
		public IList<ParameterProfile> GetEffectiveParameters()
		{
			IList<ParameterProfile> list = new List<ParameterProfile>();
			foreach (ParameterProfile parameterProfile in this.ParameterProfileList)
			{
				if (WinformsHelper.IsCurrentOrganizationAllowed(parameterProfile.OrganizationTypes))
				{
					list.Add(parameterProfile);
				}
			}
			return list;
		}

		// Token: 0x040001DB RID: 475
		private ParameterProfileList parameterProfileList = new ParameterProfileList();

		// Token: 0x040001DC RID: 476
		private RunnerBase runner;

		// Token: 0x040001DD RID: 477
		private string name;
	}
}
