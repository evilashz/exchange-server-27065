using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x02001224 RID: 4644
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AdamSchemaImportProcessFailureException : LocalizedException
	{
		// Token: 0x0600BB11 RID: 47889 RVA: 0x002A9AD1 File Offset: 0x002A7CD1
		public AdamSchemaImportProcessFailureException(string processName, int exitCode) : base(Strings.AdamSchemaImportProcessFailure(processName, exitCode))
		{
			this.processName = processName;
			this.exitCode = exitCode;
		}

		// Token: 0x0600BB12 RID: 47890 RVA: 0x002A9AEE File Offset: 0x002A7CEE
		public AdamSchemaImportProcessFailureException(string processName, int exitCode, Exception innerException) : base(Strings.AdamSchemaImportProcessFailure(processName, exitCode), innerException)
		{
			this.processName = processName;
			this.exitCode = exitCode;
		}

		// Token: 0x0600BB13 RID: 47891 RVA: 0x002A9B0C File Offset: 0x002A7D0C
		protected AdamSchemaImportProcessFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.processName = (string)info.GetValue("processName", typeof(string));
			this.exitCode = (int)info.GetValue("exitCode", typeof(int));
		}

		// Token: 0x0600BB14 RID: 47892 RVA: 0x002A9B61 File Offset: 0x002A7D61
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("processName", this.processName);
			info.AddValue("exitCode", this.exitCode);
		}

		// Token: 0x17003AE1 RID: 15073
		// (get) Token: 0x0600BB15 RID: 47893 RVA: 0x002A9B8D File Offset: 0x002A7D8D
		public string ProcessName
		{
			get
			{
				return this.processName;
			}
		}

		// Token: 0x17003AE2 RID: 15074
		// (get) Token: 0x0600BB16 RID: 47894 RVA: 0x002A9B95 File Offset: 0x002A7D95
		public int ExitCode
		{
			get
			{
				return this.exitCode;
			}
		}

		// Token: 0x04006569 RID: 25961
		private readonly string processName;

		// Token: 0x0400656A RID: 25962
		private readonly int exitCode;
	}
}
