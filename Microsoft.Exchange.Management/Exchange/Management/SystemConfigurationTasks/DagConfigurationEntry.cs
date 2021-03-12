using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000888 RID: 2184
	[Serializable]
	public sealed class DagConfigurationEntry : IConfigurable
	{
		// Token: 0x1700167B RID: 5755
		// (get) Token: 0x06004BD4 RID: 19412 RVA: 0x0013B426 File Offset: 0x00139626
		// (set) Token: 0x06004BD5 RID: 19413 RVA: 0x0013B42E File Offset: 0x0013962E
		public ObjectId Identity { get; set; }

		// Token: 0x1700167C RID: 5756
		// (get) Token: 0x06004BD6 RID: 19414 RVA: 0x0013B437 File Offset: 0x00139637
		// (set) Token: 0x06004BD7 RID: 19415 RVA: 0x0013B43F File Offset: 0x0013963F
		public string Name { get; set; }

		// Token: 0x1700167D RID: 5757
		// (get) Token: 0x06004BD8 RID: 19416 RVA: 0x0013B448 File Offset: 0x00139648
		// (set) Token: 0x06004BD9 RID: 19417 RVA: 0x0013B450 File Offset: 0x00139650
		public int ServersPerDag { get; set; }

		// Token: 0x1700167E RID: 5758
		// (get) Token: 0x06004BDA RID: 19418 RVA: 0x0013B459 File Offset: 0x00139659
		// (set) Token: 0x06004BDB RID: 19419 RVA: 0x0013B461 File Offset: 0x00139661
		public int DatabasesPerServer { get; set; }

		// Token: 0x1700167F RID: 5759
		// (get) Token: 0x06004BDC RID: 19420 RVA: 0x0013B46A File Offset: 0x0013966A
		// (set) Token: 0x06004BDD RID: 19421 RVA: 0x0013B472 File Offset: 0x00139672
		public int DatabasesPerVolume { get; set; }

		// Token: 0x17001680 RID: 5760
		// (get) Token: 0x06004BDE RID: 19422 RVA: 0x0013B47B File Offset: 0x0013967B
		// (set) Token: 0x06004BDF RID: 19423 RVA: 0x0013B483 File Offset: 0x00139683
		public int CopiesPerDatabase { get; set; }

		// Token: 0x17001681 RID: 5761
		// (get) Token: 0x06004BE0 RID: 19424 RVA: 0x0013B48C File Offset: 0x0013968C
		// (set) Token: 0x06004BE1 RID: 19425 RVA: 0x0013B494 File Offset: 0x00139694
		public int MinCopiesPerDatabaseForMonitoring { get; set; }

		// Token: 0x06004BE2 RID: 19426 RVA: 0x0013B49D File Offset: 0x0013969D
		public ValidationError[] Validate()
		{
			return new ValidationError[0];
		}

		// Token: 0x06004BE3 RID: 19427 RVA: 0x0013B4A5 File Offset: 0x001396A5
		public void CopyChangesFrom(IConfigurable source)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004BE4 RID: 19428 RVA: 0x0013B4AC File Offset: 0x001396AC
		public void ResetChangeTracking()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17001682 RID: 5762
		// (get) Token: 0x06004BE5 RID: 19429 RVA: 0x0013B4B3 File Offset: 0x001396B3
		internal bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001683 RID: 5763
		// (get) Token: 0x06004BE6 RID: 19430 RVA: 0x0013B4B6 File Offset: 0x001396B6
		bool IConfigurable.IsValid
		{
			get
			{
				return this.IsValid;
			}
		}

		// Token: 0x17001684 RID: 5764
		// (get) Token: 0x06004BE7 RID: 19431 RVA: 0x0013B4BE File Offset: 0x001396BE
		internal ObjectState ObjectState
		{
			get
			{
				return ObjectState.Unchanged;
			}
		}

		// Token: 0x17001685 RID: 5765
		// (get) Token: 0x06004BE8 RID: 19432 RVA: 0x0013B4C1 File Offset: 0x001396C1
		ObjectState IConfigurable.ObjectState
		{
			get
			{
				return this.ObjectState;
			}
		}
	}
}
