using System;
using System.Diagnostics;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200061F RID: 1567
	[Cmdlet("New", "PerfCounters")]
	[LocDescription(Strings.IDs.NewPerfCounters)]
	public class NewPerfCounters : ManagePerfCounters
	{
		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x06003799 RID: 14233 RVA: 0x000E6A94 File Offset: 0x000E4C94
		// (set) Token: 0x0600379A RID: 14234 RVA: 0x000E6A9C File Offset: 0x000E4C9C
		[ValidateRange(0, 2147483647)]
		[Parameter(Mandatory = false)]
		public int FileMappingSize
		{
			get
			{
				return this.fileMappingSize;
			}
			set
			{
				this.fileMappingSize = value;
			}
		}

		// Token: 0x0600379B RID: 14235 RVA: 0x000E6AA8 File Offset: 0x000E4CA8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				if (new PerfCounterCategoryExistsCondition(this.CategoryName).Verify())
				{
					base.DeletePerfCounterCategory();
					PerformanceCounter.CloseSharedResources();
				}
				base.CreatePerfCounterCategory();
			}
			catch (CorruptedPerformanceCountersException)
			{
			}
			PerformanceCounter.CloseSharedResources();
			TaskLogger.LogExit();
		}

		// Token: 0x02000620 RID: 1568
		private enum Checkpoints
		{
			// Token: 0x040025A2 RID: 9634
			Started,
			// Token: 0x040025A3 RID: 9635
			RegistryKeysCreated,
			// Token: 0x040025A4 RID: 9636
			Finished
		}
	}
}
