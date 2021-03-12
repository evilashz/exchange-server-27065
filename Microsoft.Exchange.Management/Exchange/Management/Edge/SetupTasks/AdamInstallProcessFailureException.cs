using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x0200121D RID: 4637
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AdamInstallProcessFailureException : LocalizedException
	{
		// Token: 0x0600BAEB RID: 47851 RVA: 0x002A968C File Offset: 0x002A788C
		public AdamInstallProcessFailureException(string processName, int exitCode) : base(Strings.AdamInstallProcessFailure(processName, exitCode))
		{
			this.processName = processName;
			this.exitCode = exitCode;
		}

		// Token: 0x0600BAEC RID: 47852 RVA: 0x002A96A9 File Offset: 0x002A78A9
		public AdamInstallProcessFailureException(string processName, int exitCode, Exception innerException) : base(Strings.AdamInstallProcessFailure(processName, exitCode), innerException)
		{
			this.processName = processName;
			this.exitCode = exitCode;
		}

		// Token: 0x0600BAED RID: 47853 RVA: 0x002A96C8 File Offset: 0x002A78C8
		protected AdamInstallProcessFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.processName = (string)info.GetValue("processName", typeof(string));
			this.exitCode = (int)info.GetValue("exitCode", typeof(int));
		}

		// Token: 0x0600BAEE RID: 47854 RVA: 0x002A971D File Offset: 0x002A791D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("processName", this.processName);
			info.AddValue("exitCode", this.exitCode);
		}

		// Token: 0x17003AD7 RID: 15063
		// (get) Token: 0x0600BAEF RID: 47855 RVA: 0x002A9749 File Offset: 0x002A7949
		public string ProcessName
		{
			get
			{
				return this.processName;
			}
		}

		// Token: 0x17003AD8 RID: 15064
		// (get) Token: 0x0600BAF0 RID: 47856 RVA: 0x002A9751 File Offset: 0x002A7951
		public int ExitCode
		{
			get
			{
				return this.exitCode;
			}
		}

		// Token: 0x0400655F RID: 25951
		private readonly string processName;

		// Token: 0x04006560 RID: 25952
		private readonly int exitCode;
	}
}
