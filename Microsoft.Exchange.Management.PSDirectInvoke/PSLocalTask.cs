using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.PSDirectInvoke
{
	// Token: 0x02000005 RID: 5
	internal class PSLocalTask<T, TResult> : IDisposable where T : Task
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00002FB5 File Offset: 0x000011B5
		public PSLocalTask(T task)
		{
			this.Task = task;
			this.TaskIO = new PSLocalTaskIO<TResult>();
			task.PrependTaskIOPipelineHandler(this.TaskIO);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002FE2 File Offset: 0x000011E2
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002FEF File Offset: 0x000011EF
		public bool CaptureAdditionalIO
		{
			get
			{
				return this.TaskIO.CaptureAdditionalIO;
			}
			set
			{
				this.TaskIO.CaptureAdditionalIO = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002FFD File Offset: 0x000011FD
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00003005 File Offset: 0x00001205
		public T Task { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600005A RID: 90 RVA: 0x0000300E File Offset: 0x0000120E
		// (set) Token: 0x0600005B RID: 91 RVA: 0x0000301C File Offset: 0x0000121C
		public bool WhatIfMode
		{
			get
			{
				return this.TaskIO.WhatIfMode;
			}
			set
			{
				T task = this.Task;
				PropertyBag userSpecifiedParameters = task.CurrentTaskContext.InvocationInfo.UserSpecifiedParameters;
				if (value)
				{
					userSpecifiedParameters["WhatIf"] = SwitchParameter.Present;
				}
				else if (userSpecifiedParameters.Contains("WhatIf"))
				{
					userSpecifiedParameters.Remove("WhatIf");
				}
				this.TaskIO.WhatIfMode = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003088 File Offset: 0x00001288
		public TResult Result
		{
			get
			{
				if (this.TaskIO.Objects.Count > 0)
				{
					return this.TaskIO.Objects[0];
				}
				return default(TResult);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000030C3 File Offset: 0x000012C3
		public TaskErrorInfo Error
		{
			get
			{
				if (this.TaskIO.Errors.Count > 0)
				{
					return this.TaskIO.Errors[0];
				}
				return null;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000030EC File Offset: 0x000012EC
		public string ErrorMessage
		{
			get
			{
				if (this.TaskIO.Errors.Count > 0 && this.TaskIO.Errors[0].Exception != null)
				{
					return this.TaskIO.Errors[0].Exception.Message;
				}
				return string.Empty;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00003145 File Offset: 0x00001345
		public IList<TResult> AllResults
		{
			get
			{
				return this.TaskIO.Objects;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003152 File Offset: 0x00001352
		public List<TaskErrorInfo> AllErrors
		{
			get
			{
				return this.TaskIO.Errors;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000061 RID: 97 RVA: 0x0000315F File Offset: 0x0000135F
		public List<PSLocalTaskIOData> AdditionalIO
		{
			get
			{
				return this.TaskIO.AdditionalIO;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000062 RID: 98 RVA: 0x0000316C File Offset: 0x0000136C
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00003174 File Offset: 0x00001374
		private PSLocalTaskIO<TResult> TaskIO { get; set; }

		// Token: 0x06000064 RID: 100 RVA: 0x00003180 File Offset: 0x00001380
		public void Dispose()
		{
			T task = this.Task;
			task.Dispose();
		}

		// Token: 0x0400004C RID: 76
		private const string WhatIfParameter = "WhatIf";
	}
}
