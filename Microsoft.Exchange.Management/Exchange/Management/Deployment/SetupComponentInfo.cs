using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000191 RID: 401
	[XmlInclude(typeof(SetupComponentInfo))]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public class SetupComponentInfo
	{
		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x00041D12 File Offset: 0x0003FF12
		// (set) Token: 0x06000EBC RID: 3772 RVA: 0x00041D2D File Offset: 0x0003FF2D
		[XmlAttribute]
		public string DescriptionId
		{
			get
			{
				if (this.descriptionId == null)
				{
					this.descriptionId = string.Empty;
				}
				return this.descriptionId;
			}
			set
			{
				this.descriptionId = value;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x00041D36 File Offset: 0x0003FF36
		// (set) Token: 0x06000EBE RID: 3774 RVA: 0x00041D51 File Offset: 0x0003FF51
		public ServerTaskInfoCollection ServerTasks
		{
			get
			{
				if (this.serverTasks == null)
				{
					this.serverTasks = new ServerTaskInfoCollection();
				}
				return this.serverTasks;
			}
			set
			{
				this.serverTasks = value;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x00041D5A File Offset: 0x0003FF5A
		// (set) Token: 0x06000EC0 RID: 3776 RVA: 0x00041D75 File Offset: 0x0003FF75
		public OrgTaskInfoCollection OrgTasks
		{
			get
			{
				if (this.orgTasks == null)
				{
					this.orgTasks = new OrgTaskInfoCollection();
				}
				return this.orgTasks;
			}
			set
			{
				this.orgTasks = value;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x00041D7E File Offset: 0x0003FF7E
		// (set) Token: 0x06000EC2 RID: 3778 RVA: 0x00041D99 File Offset: 0x0003FF99
		public ServicePlanTaskInfoCollection ServicePlanOrgTasks
		{
			get
			{
				if (this.servicePlanOrgTasks == null)
				{
					this.servicePlanOrgTasks = new ServicePlanTaskInfoCollection();
				}
				return this.servicePlanOrgTasks;
			}
			set
			{
				this.servicePlanOrgTasks = value;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x00041DA2 File Offset: 0x0003FFA2
		public TaskInfoCollection Tasks
		{
			get
			{
				if (this.tasks == null)
				{
					this.tasks = new TaskInfoCollection();
				}
				return this.tasks;
			}
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x00041DC0 File Offset: 0x0003FFC0
		internal void PopulateTasksProperty(string fileId)
		{
			if (this.tasks == null)
			{
				this.tasks = new TaskInfoCollection();
			}
			if (this.tasks.Count == 0)
			{
				foreach (ServerTaskInfo serverTaskInfo in this.ServerTasks)
				{
					serverTaskInfo.FileId = fileId;
					this.tasks.Add(serverTaskInfo);
				}
				foreach (OrgTaskInfo orgTaskInfo in this.OrgTasks)
				{
					orgTaskInfo.FileId = fileId;
					this.tasks.Add(orgTaskInfo);
				}
				foreach (ServicePlanTaskInfo item in this.ServicePlanOrgTasks)
				{
					this.tasks.Add(item);
				}
			}
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x00041EDC File Offset: 0x000400DC
		internal void ValidateDatacenterAttributes()
		{
			if (this.DatacenterMode == DatacenterMode.ExO && !this.IsDatacenterOnly)
			{
				throw new LocalizedException(Strings.ErrorCannotBeExOWithoutDatacenter(this.Name), null);
			}
			if (this.DatacenterMode == DatacenterMode.Ffo && !this.IsDatacenterOnly)
			{
				throw new LocalizedException(Strings.ErrorCannotBeFfoWithoutDatacenter(this.Name), null);
			}
		}

		// Token: 0x040006D8 RID: 1752
		[XmlAttribute]
		public string Name;

		// Token: 0x040006D9 RID: 1753
		private string descriptionId;

		// Token: 0x040006DA RID: 1754
		[XmlAttribute]
		public bool AlwaysExecute;

		// Token: 0x040006DB RID: 1755
		[XmlAttribute]
		public bool IsDatacenterOnly;

		// Token: 0x040006DC RID: 1756
		[XmlAttribute]
		public bool IsPartnerHostedOnly;

		// Token: 0x040006DD RID: 1757
		[DefaultValue(DatacenterMode.Common)]
		[XmlAttribute]
		public DatacenterMode DatacenterMode;

		// Token: 0x040006DE RID: 1758
		[XmlAttribute]
		public bool IsDatacenterDedicatedOnly;

		// Token: 0x040006DF RID: 1759
		private ServerTaskInfoCollection serverTasks;

		// Token: 0x040006E0 RID: 1760
		private OrgTaskInfoCollection orgTasks;

		// Token: 0x040006E1 RID: 1761
		private ServicePlanTaskInfoCollection servicePlanOrgTasks;

		// Token: 0x040006E2 RID: 1762
		private TaskInfoCollection tasks;
	}
}
