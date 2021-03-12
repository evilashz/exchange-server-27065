using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000183 RID: 387
	[ClassAccessLevel(AccessLevel.Consumer)]
	public abstract class TaskInfo
	{
		// Token: 0x06000E70 RID: 3696 RVA: 0x0004178E File Offset: 0x0003F98E
		public TaskInfo()
		{
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x000417A1 File Offset: 0x0003F9A1
		// (set) Token: 0x06000E72 RID: 3698 RVA: 0x000417A9 File Offset: 0x0003F9A9
		[XmlIgnore]
		public string FileId { get; set; }

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x000417B2 File Offset: 0x0003F9B2
		// (set) Token: 0x06000E74 RID: 3700 RVA: 0x000417BA File Offset: 0x0003F9BA
		[XmlAttribute]
		public string Id { get; set; }

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x000417C3 File Offset: 0x0003F9C3
		// (set) Token: 0x06000E76 RID: 3702 RVA: 0x000417CB File Offset: 0x0003F9CB
		[XmlAttribute]
		public string Component { get; set; }

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x000417D4 File Offset: 0x0003F9D4
		// (set) Token: 0x06000E78 RID: 3704 RVA: 0x000417DC File Offset: 0x0003F9DC
		[XmlAttribute]
		public bool ExcludeInDatacenterDedicated { get; set; }

		// Token: 0x06000E79 RID: 3705 RVA: 0x000417E8 File Offset: 0x0003F9E8
		public string GetTask(InstallationModes mode, InstallationCircumstances circumstance)
		{
			TaskInfoBlock taskInfoBlock = null;
			if (this.blocks.TryGetValue(mode, out taskInfoBlock) && (mode == InstallationModes.BuildToBuildUpgrade || mode == InstallationModes.DisasterRecovery) && taskInfoBlock.UseInstallTasks)
			{
				this.blocks.TryGetValue(InstallationModes.Install, out taskInfoBlock);
			}
			if (taskInfoBlock != null)
			{
				return taskInfoBlock.GetTask(circumstance);
			}
			return string.Empty;
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00041838 File Offset: 0x0003FA38
		public string GetDescription(InstallationModes mode)
		{
			string text = string.Empty;
			TaskInfoBlock taskInfoBlock = null;
			if (this.blocks.TryGetValue(mode, out taskInfoBlock))
			{
				text = taskInfoBlock.DescriptionId;
				if ((mode == InstallationModes.BuildToBuildUpgrade || mode == InstallationModes.DisasterRecovery) && taskInfoBlock.UseInstallTasks && string.IsNullOrEmpty(text) && this.blocks.TryGetValue(InstallationModes.Install, out taskInfoBlock))
				{
					text = taskInfoBlock.DescriptionId;
				}
			}
			return text ?? string.Empty;
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x000418A0 File Offset: 0x0003FAA0
		public int GetWeight(InstallationModes mode)
		{
			int num = 1;
			TaskInfoBlock taskInfoBlock = null;
			if (this.blocks.TryGetValue(mode, out taskInfoBlock))
			{
				num = taskInfoBlock.Weight;
				if ((mode == InstallationModes.BuildToBuildUpgrade || mode == InstallationModes.DisasterRecovery) && taskInfoBlock.UseInstallTasks && num == 1 && this.blocks.TryGetValue(InstallationModes.Install, out taskInfoBlock))
				{
					num = taskInfoBlock.Weight;
				}
			}
			return num;
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x000418F4 File Offset: 0x0003FAF4
		public bool IsFatal(InstallationModes mode)
		{
			bool flag = true;
			TaskInfoBlock taskInfoBlock = null;
			if (this.blocks.TryGetValue(mode, out taskInfoBlock))
			{
				flag = taskInfoBlock.IsFatal;
				if ((mode == InstallationModes.BuildToBuildUpgrade || mode == InstallationModes.DisasterRecovery) && taskInfoBlock.UseInstallTasks && flag && this.blocks.TryGetValue(InstallationModes.Install, out taskInfoBlock))
				{
					flag = taskInfoBlock.IsFatal;
				}
			}
			return flag;
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00041948 File Offset: 0x0003FB48
		public virtual string GetID()
		{
			if (string.IsNullOrEmpty(this.Id))
			{
				string text = this.GetTask(InstallationModes.Install, InstallationCircumstances.Standalone) + this.GetTask(InstallationModes.BuildToBuildUpgrade, InstallationCircumstances.Standalone) + this.GetTask(InstallationModes.DisasterRecovery, InstallationCircumstances.Standalone) + this.GetTask(InstallationModes.Uninstall, InstallationCircumstances.Standalone);
				return text.GetHashCode().ToString();
			}
			return string.Format("{0}__{1}", this.FileId, this.Id);
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x000419B0 File Offset: 0x0003FBB0
		internal TTaskInfoBlock GetBlock<TTaskInfoBlock>(InstallationModes mode) where TTaskInfoBlock : TaskInfoBlock, new()
		{
			TaskInfoBlock taskInfoBlock = null;
			this.blocks.TryGetValue(mode, out taskInfoBlock);
			switch (mode)
			{
			case InstallationModes.Install:
			case InstallationModes.Uninstall:
				if (taskInfoBlock == null)
				{
					taskInfoBlock = Activator.CreateInstance<TTaskInfoBlock>();
					this.blocks[mode] = taskInfoBlock;
				}
				break;
			case InstallationModes.BuildToBuildUpgrade:
			case InstallationModes.DisasterRecovery:
				if (taskInfoBlock == null)
				{
					taskInfoBlock = Activator.CreateInstance<TTaskInfoBlock>();
					this.blocks[mode] = taskInfoBlock;
				}
				else if (taskInfoBlock.UseInstallTasks)
				{
					taskInfoBlock = this.GetBlock<TTaskInfoBlock>(InstallationModes.Install);
				}
				break;
			}
			return (TTaskInfoBlock)((object)taskInfoBlock);
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00041A3F File Offset: 0x0003FC3F
		internal void SetBlock(InstallationModes mode, TaskInfoBlock infoBlock)
		{
			this.blocks[mode] = infoBlock;
		}

		// Token: 0x040006C7 RID: 1735
		private Dictionary<InstallationModes, TaskInfoBlock> blocks = new Dictionary<InstallationModes, TaskInfoBlock>();
	}
}
