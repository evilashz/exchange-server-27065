using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001051 RID: 4177
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagFswUnableToCreateWitnessDirectoryException : LocalizedException
	{
		// Token: 0x0600B058 RID: 45144 RVA: 0x00295E1A File Offset: 0x0029401A
		public DagFswUnableToCreateWitnessDirectoryException(string fswMachine, string directory, Exception ex) : base(Strings.DagFswUnableToCreateWitnessDirectoryException(fswMachine, directory, ex))
		{
			this.fswMachine = fswMachine;
			this.directory = directory;
			this.ex = ex;
		}

		// Token: 0x0600B059 RID: 45145 RVA: 0x00295E3F File Offset: 0x0029403F
		public DagFswUnableToCreateWitnessDirectoryException(string fswMachine, string directory, Exception ex, Exception innerException) : base(Strings.DagFswUnableToCreateWitnessDirectoryException(fswMachine, directory, ex), innerException)
		{
			this.fswMachine = fswMachine;
			this.directory = directory;
			this.ex = ex;
		}

		// Token: 0x0600B05A RID: 45146 RVA: 0x00295E68 File Offset: 0x00294068
		protected DagFswUnableToCreateWitnessDirectoryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fswMachine = (string)info.GetValue("fswMachine", typeof(string));
			this.directory = (string)info.GetValue("directory", typeof(string));
			this.ex = (Exception)info.GetValue("ex", typeof(Exception));
		}

		// Token: 0x0600B05B RID: 45147 RVA: 0x00295EDD File Offset: 0x002940DD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fswMachine", this.fswMachine);
			info.AddValue("directory", this.directory);
			info.AddValue("ex", this.ex);
		}

		// Token: 0x17003831 RID: 14385
		// (get) Token: 0x0600B05C RID: 45148 RVA: 0x00295F1A File Offset: 0x0029411A
		public string FswMachine
		{
			get
			{
				return this.fswMachine;
			}
		}

		// Token: 0x17003832 RID: 14386
		// (get) Token: 0x0600B05D RID: 45149 RVA: 0x00295F22 File Offset: 0x00294122
		public string Directory
		{
			get
			{
				return this.directory;
			}
		}

		// Token: 0x17003833 RID: 14387
		// (get) Token: 0x0600B05E RID: 45150 RVA: 0x00295F2A File Offset: 0x0029412A
		public Exception Ex
		{
			get
			{
				return this.ex;
			}
		}

		// Token: 0x04006197 RID: 24983
		private readonly string fswMachine;

		// Token: 0x04006198 RID: 24984
		private readonly string directory;

		// Token: 0x04006199 RID: 24985
		private readonly Exception ex;
	}
}
