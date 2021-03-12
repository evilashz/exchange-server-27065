using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Win32;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders
{
	// Token: 0x02000203 RID: 515
	internal class MemoryRestartResponderChecker : RestartResponderChecker
	{
		// Token: 0x06000E5F RID: 3679 RVA: 0x00060246 File Offset: 0x0005E446
		internal MemoryRestartResponderChecker(ResponderDefinition definition) : base(definition)
		{
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x0006024F File Offset: 0x0005E44F
		internal override string SkipReasonOrException
		{
			get
			{
				return this.skipReasonOrException;
			}
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00060258 File Offset: 0x0005E458
		protected override bool IsWithinThreshold()
		{
			this.skipReasonOrException = null;
			try
			{
				if (this.memoryThreshold > 0)
				{
					uint memoryLoad = MemoryRestartResponderChecker.GetMemoryLoad();
					if ((ulong)memoryLoad < (ulong)((long)this.memoryThreshold))
					{
						this.skipReasonOrException = string.Format("Skipped Due to Low Memory. Real value = {0}, Threshold = {1}.", memoryLoad, this.memoryThreshold);
						return false;
					}
				}
			}
			catch (Exception ex)
			{
				this.skipReasonOrException = ex.ToString();
			}
			return true;
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000E62 RID: 3682 RVA: 0x000602D0 File Offset: 0x0005E4D0
		internal override string KeyOfEnabled
		{
			get
			{
				return "MemoryRestartResponderCheckerEnabled";
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x000602D7 File Offset: 0x0005E4D7
		internal override string KeyOfSetting
		{
			get
			{
				return "MemoryLoadThreshold";
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x000602E0 File Offset: 0x0005E4E0
		internal override string DefaultSetting
		{
			get
			{
				return 4.ToString();
			}
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x000602F8 File Offset: 0x0005E4F8
		protected override bool OnSettingChange(string newSetting)
		{
			try
			{
				this.memoryThreshold = int.Parse(newSetting);
			}
			catch (Exception ex)
			{
				this.skipReasonOrException = ex.ToString();
				return false;
			}
			return true;
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00060338 File Offset: 0x0005E538
		private static uint GetMemoryLoad()
		{
			NativeMethods.MemoryStatusEx memoryStatusEx = default(NativeMethods.MemoryStatusEx);
			memoryStatusEx.Length = (uint)Marshal.SizeOf(typeof(NativeMethods.MemoryStatusEx));
			if (!NativeMethods.GlobalMemoryStatusEx(out memoryStatusEx))
			{
				return 100U;
			}
			return memoryStatusEx.MemoryLoad;
		}

		// Token: 0x04000AC5 RID: 2757
		private const string ReasonToSkipFormat = "Skipped Due to Low Memory. Real value = {0}, Threshold = {1}.";

		// Token: 0x04000AC6 RID: 2758
		private const int DefaultMinimumMemoryThreshold = 4;

		// Token: 0x04000AC7 RID: 2759
		private const string MemoryRestartResponderCheckerEnabled = "MemoryRestartResponderCheckerEnabled";

		// Token: 0x04000AC8 RID: 2760
		private const string MemoryLoadThreshold = "MemoryLoadThreshold";

		// Token: 0x04000AC9 RID: 2761
		private string skipReasonOrException;

		// Token: 0x04000ACA RID: 2762
		private int memoryThreshold;
	}
}
