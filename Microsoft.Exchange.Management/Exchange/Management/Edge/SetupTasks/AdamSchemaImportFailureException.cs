using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x02001223 RID: 4643
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AdamSchemaImportFailureException : LocalizedException
	{
		// Token: 0x0600BB0B RID: 47883 RVA: 0x002A9A05 File Offset: 0x002A7C05
		public AdamSchemaImportFailureException(string processName, int exitCode) : base(Strings.AdamSchemaImportProcessFailure(processName, exitCode))
		{
			this.processName = processName;
			this.exitCode = exitCode;
		}

		// Token: 0x0600BB0C RID: 47884 RVA: 0x002A9A22 File Offset: 0x002A7C22
		public AdamSchemaImportFailureException(string processName, int exitCode, Exception innerException) : base(Strings.AdamSchemaImportProcessFailure(processName, exitCode), innerException)
		{
			this.processName = processName;
			this.exitCode = exitCode;
		}

		// Token: 0x0600BB0D RID: 47885 RVA: 0x002A9A40 File Offset: 0x002A7C40
		protected AdamSchemaImportFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.processName = (string)info.GetValue("processName", typeof(string));
			this.exitCode = (int)info.GetValue("exitCode", typeof(int));
		}

		// Token: 0x0600BB0E RID: 47886 RVA: 0x002A9A95 File Offset: 0x002A7C95
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("processName", this.processName);
			info.AddValue("exitCode", this.exitCode);
		}

		// Token: 0x17003ADF RID: 15071
		// (get) Token: 0x0600BB0F RID: 47887 RVA: 0x002A9AC1 File Offset: 0x002A7CC1
		public string ProcessName
		{
			get
			{
				return this.processName;
			}
		}

		// Token: 0x17003AE0 RID: 15072
		// (get) Token: 0x0600BB10 RID: 47888 RVA: 0x002A9AC9 File Offset: 0x002A7CC9
		public int ExitCode
		{
			get
			{
				return this.exitCode;
			}
		}

		// Token: 0x04006567 RID: 25959
		private readonly string processName;

		// Token: 0x04006568 RID: 25960
		private readonly int exitCode;
	}
}
