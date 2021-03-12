using System;
using System.Diagnostics;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000622 RID: 1570
	[Cmdlet("Remove", "PerfCounters")]
	[LocDescription(Strings.IDs.RemovePerfCounters)]
	public class RemovePerfCounters : ManagePerfCounters
	{
		// Token: 0x060037A0 RID: 14240 RVA: 0x000E6C14 File Offset: 0x000E4E14
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
			}
			catch (CorruptedPerformanceCountersException)
			{
			}
			TaskLogger.LogExit();
		}

		// Token: 0x02000623 RID: 1571
		private enum Checkpoints
		{
			// Token: 0x040025A7 RID: 9639
			Started,
			// Token: 0x040025A8 RID: 9640
			UnloadedLocalizedText,
			// Token: 0x040025A9 RID: 9641
			Finished
		}
	}
}
