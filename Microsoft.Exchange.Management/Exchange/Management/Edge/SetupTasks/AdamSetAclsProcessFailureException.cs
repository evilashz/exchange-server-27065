using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x02001225 RID: 4645
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AdamSetAclsProcessFailureException : LocalizedException
	{
		// Token: 0x0600BB17 RID: 47895 RVA: 0x002A9B9D File Offset: 0x002A7D9D
		public AdamSetAclsProcessFailureException(string processName, int exitCode, string dn) : base(Strings.AdamSetAclsProcessFailure(processName, exitCode, dn))
		{
			this.processName = processName;
			this.exitCode = exitCode;
			this.dn = dn;
		}

		// Token: 0x0600BB18 RID: 47896 RVA: 0x002A9BC2 File Offset: 0x002A7DC2
		public AdamSetAclsProcessFailureException(string processName, int exitCode, string dn, Exception innerException) : base(Strings.AdamSetAclsProcessFailure(processName, exitCode, dn), innerException)
		{
			this.processName = processName;
			this.exitCode = exitCode;
			this.dn = dn;
		}

		// Token: 0x0600BB19 RID: 47897 RVA: 0x002A9BEC File Offset: 0x002A7DEC
		protected AdamSetAclsProcessFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.processName = (string)info.GetValue("processName", typeof(string));
			this.exitCode = (int)info.GetValue("exitCode", typeof(int));
			this.dn = (string)info.GetValue("dn", typeof(string));
		}

		// Token: 0x0600BB1A RID: 47898 RVA: 0x002A9C61 File Offset: 0x002A7E61
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("processName", this.processName);
			info.AddValue("exitCode", this.exitCode);
			info.AddValue("dn", this.dn);
		}

		// Token: 0x17003AE3 RID: 15075
		// (get) Token: 0x0600BB1B RID: 47899 RVA: 0x002A9C9E File Offset: 0x002A7E9E
		public string ProcessName
		{
			get
			{
				return this.processName;
			}
		}

		// Token: 0x17003AE4 RID: 15076
		// (get) Token: 0x0600BB1C RID: 47900 RVA: 0x002A9CA6 File Offset: 0x002A7EA6
		public int ExitCode
		{
			get
			{
				return this.exitCode;
			}
		}

		// Token: 0x17003AE5 RID: 15077
		// (get) Token: 0x0600BB1D RID: 47901 RVA: 0x002A9CAE File Offset: 0x002A7EAE
		public string Dn
		{
			get
			{
				return this.dn;
			}
		}

		// Token: 0x0400656B RID: 25963
		private readonly string processName;

		// Token: 0x0400656C RID: 25964
		private readonly int exitCode;

		// Token: 0x0400656D RID: 25965
		private readonly string dn;
	}
}
