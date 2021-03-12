using System;
using Microsoft.Exchange.Diagnostics.Service.Common;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service
{
	// Token: 0x02000021 RID: 33
	public sealed class SingleStreamJob : WatermarkedJob
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00007A58 File Offset: 0x00005C58
		public SingleStreamJob(string jobName, LogSource logSource, string extensionName, OutputStream stream, Watermark watermark, string watermarkDirectory) : base(jobName, logSource, extensionName, watermark, watermarkDirectory)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.multipleStream = (stream as IMultipleOutputStream);
			this.stream = stream;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00007A8B File Offset: 0x00005C8B
		public override void CloseOutputStream(OutputStream stream)
		{
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00007A90 File Offset: 0x00005C90
		protected override OutputStream InternalOpenOutputStream(LogAnalyzer analyzer, string name)
		{
			if (this.multipleStream != null)
			{
				string name2 = analyzer.GetType().Name;
				string text;
				if (!base.OutputFormats.TryGetValue(name2, out text) || string.IsNullOrEmpty(text))
				{
					text = ((name == "MachineInformation") ? "MachineInformation" : "PerformanceCounter");
				}
				return this.multipleStream.OpenOutputStream(name2, text, name);
			}
			return this.stream;
		}

		// Token: 0x04000075 RID: 117
		private IMultipleOutputStream multipleStream;

		// Token: 0x04000076 RID: 118
		private OutputStream stream;
	}
}
