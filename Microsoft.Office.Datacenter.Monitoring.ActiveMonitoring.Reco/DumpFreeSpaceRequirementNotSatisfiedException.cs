using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000057 RID: 87
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DumpFreeSpaceRequirementNotSatisfiedException : RecoveryActionExceptionCommon
	{
		// Token: 0x06000370 RID: 880 RVA: 0x0000BEAD File Offset: 0x0000A0AD
		public DumpFreeSpaceRequirementNotSatisfiedException(string directory, double available, double required) : base(StringsRecovery.DumpFreeSpaceRequirementNotSatisfied(directory, available, required))
		{
			this.directory = directory;
			this.available = available;
			this.required = required;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000BED7 File Offset: 0x0000A0D7
		public DumpFreeSpaceRequirementNotSatisfiedException(string directory, double available, double required, Exception innerException) : base(StringsRecovery.DumpFreeSpaceRequirementNotSatisfied(directory, available, required), innerException)
		{
			this.directory = directory;
			this.available = available;
			this.required = required;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000BF04 File Offset: 0x0000A104
		protected DumpFreeSpaceRequirementNotSatisfiedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.directory = (string)info.GetValue("directory", typeof(string));
			this.available = (double)info.GetValue("available", typeof(double));
			this.required = (double)info.GetValue("required", typeof(double));
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000BF79 File Offset: 0x0000A179
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("directory", this.directory);
			info.AddValue("available", this.available);
			info.AddValue("required", this.required);
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000BFB6 File Offset: 0x0000A1B6
		public string Directory
		{
			get
			{
				return this.directory;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000BFBE File Offset: 0x0000A1BE
		public double Available
		{
			get
			{
				return this.available;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000BFC6 File Offset: 0x0000A1C6
		public double Required
		{
			get
			{
				return this.required;
			}
		}

		// Token: 0x04000210 RID: 528
		private readonly string directory;

		// Token: 0x04000211 RID: 529
		private readonly double available;

		// Token: 0x04000212 RID: 530
		private readonly double required;
	}
}
