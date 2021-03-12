using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x0200121F RID: 4639
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AdamUninstallProcessFailureException : LocalizedException
	{
		// Token: 0x0600BAF6 RID: 47862 RVA: 0x002A97D1 File Offset: 0x002A79D1
		public AdamUninstallProcessFailureException(string processName, int exitCode) : base(Strings.AdamUninstallProcessFailure(processName, exitCode))
		{
			this.processName = processName;
			this.exitCode = exitCode;
		}

		// Token: 0x0600BAF7 RID: 47863 RVA: 0x002A97EE File Offset: 0x002A79EE
		public AdamUninstallProcessFailureException(string processName, int exitCode, Exception innerException) : base(Strings.AdamUninstallProcessFailure(processName, exitCode), innerException)
		{
			this.processName = processName;
			this.exitCode = exitCode;
		}

		// Token: 0x0600BAF8 RID: 47864 RVA: 0x002A980C File Offset: 0x002A7A0C
		protected AdamUninstallProcessFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.processName = (string)info.GetValue("processName", typeof(string));
			this.exitCode = (int)info.GetValue("exitCode", typeof(int));
		}

		// Token: 0x0600BAF9 RID: 47865 RVA: 0x002A9861 File Offset: 0x002A7A61
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("processName", this.processName);
			info.AddValue("exitCode", this.exitCode);
		}

		// Token: 0x17003ADA RID: 15066
		// (get) Token: 0x0600BAFA RID: 47866 RVA: 0x002A988D File Offset: 0x002A7A8D
		public string ProcessName
		{
			get
			{
				return this.processName;
			}
		}

		// Token: 0x17003ADB RID: 15067
		// (get) Token: 0x0600BAFB RID: 47867 RVA: 0x002A9895 File Offset: 0x002A7A95
		public int ExitCode
		{
			get
			{
				return this.exitCode;
			}
		}

		// Token: 0x04006562 RID: 25954
		private readonly string processName;

		// Token: 0x04006563 RID: 25955
		private readonly int exitCode;
	}
}
