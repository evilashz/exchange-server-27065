using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.PSDirectInvoke
{
	// Token: 0x02000006 RID: 6
	internal class PSLocalTaskIO<TOutputType> : ITaskIOPipeline
	{
		// Token: 0x06000065 RID: 101 RVA: 0x000031A1 File Offset: 0x000013A1
		public PSLocalTaskIO()
		{
			this.Objects = new List<TOutputType>();
			this.Errors = new List<TaskErrorInfo>();
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000031BF File Offset: 0x000013BF
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000031C7 File Offset: 0x000013C7
		public bool CaptureAdditionalIO { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000031D0 File Offset: 0x000013D0
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000031D8 File Offset: 0x000013D8
		public List<TaskErrorInfo> Errors { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000031E1 File Offset: 0x000013E1
		// (set) Token: 0x0600006B RID: 107 RVA: 0x000031E9 File Offset: 0x000013E9
		public List<TOutputType> Objects { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000031F2 File Offset: 0x000013F2
		// (set) Token: 0x0600006D RID: 109 RVA: 0x000031FA File Offset: 0x000013FA
		public List<PSLocalTaskIOData> AdditionalIO { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003203 File Offset: 0x00001403
		// (set) Token: 0x0600006F RID: 111 RVA: 0x0000320B File Offset: 0x0000140B
		public bool WhatIfMode { get; set; }

		// Token: 0x06000070 RID: 112 RVA: 0x00003214 File Offset: 0x00001414
		public bool WriteVerbose(LocalizedString input, out LocalizedString output)
		{
			this.HandleIO(PSLocalTaskIOType.Verbose, input);
			output = input;
			return false;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003226 File Offset: 0x00001426
		public bool WriteDebug(LocalizedString input, out LocalizedString output)
		{
			this.HandleIO(PSLocalTaskIOType.Debug, input);
			output = input;
			return false;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003238 File Offset: 0x00001438
		public bool WriteWarning(LocalizedString input, string helperUrl, out LocalizedString output)
		{
			this.HandleIO(PSLocalTaskIOType.Warning, input);
			output = input;
			return false;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000324C File Offset: 0x0000144C
		public bool WriteError(TaskErrorInfo input, out TaskErrorInfo output)
		{
			output = input;
			TaskErrorInfo taskErrorInfo = new TaskErrorInfo();
			taskErrorInfo.SetErrorInfo(input.Exception, input.ExchangeErrorCategory.Value, input.Target, input.HelpUrl, input.TerminatePipeline, input.IsKnownError);
			this.Errors.Add(taskErrorInfo);
			return false;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000032A1 File Offset: 0x000014A1
		public bool WriteObject(object input, out object output)
		{
			output = input;
			this.Objects.Add((TOutputType)((object)input));
			return false;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000032B8 File Offset: 0x000014B8
		public bool WriteProgress(ExProgressRecord input, out ExProgressRecord output)
		{
			output = input;
			return false;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000032BE File Offset: 0x000014BE
		public bool ShouldContinue(string query, string caption, ref bool yesToAll, ref bool noToAll, out bool? output)
		{
			output = new bool?(true);
			return false;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000032CE File Offset: 0x000014CE
		public bool ShouldProcess(string verboseDescription, string verboseWarning, string caption, out bool? output)
		{
			output = new bool?(!this.WhatIfMode);
			return false;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000032E8 File Offset: 0x000014E8
		private void HandleIO(PSLocalTaskIOType type, LocalizedString input)
		{
			if (this.CaptureAdditionalIO)
			{
				if (this.AdditionalIO == null)
				{
					this.AdditionalIO = new List<PSLocalTaskIOData>(10);
				}
				this.AdditionalIO.Add(new PSLocalTaskIOData(type, DateTime.UtcNow, input.ToString()));
			}
		}
	}
}
