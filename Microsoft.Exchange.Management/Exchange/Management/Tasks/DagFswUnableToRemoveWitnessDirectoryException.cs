using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001052 RID: 4178
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagFswUnableToRemoveWitnessDirectoryException : LocalizedException
	{
		// Token: 0x0600B05F RID: 45151 RVA: 0x00295F32 File Offset: 0x00294132
		public DagFswUnableToRemoveWitnessDirectoryException(string fswMachine, string directory, Exception ex) : base(Strings.DagFswUnableToRemoveWitnessDirectoryException(fswMachine, directory, ex))
		{
			this.fswMachine = fswMachine;
			this.directory = directory;
			this.ex = ex;
		}

		// Token: 0x0600B060 RID: 45152 RVA: 0x00295F57 File Offset: 0x00294157
		public DagFswUnableToRemoveWitnessDirectoryException(string fswMachine, string directory, Exception ex, Exception innerException) : base(Strings.DagFswUnableToRemoveWitnessDirectoryException(fswMachine, directory, ex), innerException)
		{
			this.fswMachine = fswMachine;
			this.directory = directory;
			this.ex = ex;
		}

		// Token: 0x0600B061 RID: 45153 RVA: 0x00295F80 File Offset: 0x00294180
		protected DagFswUnableToRemoveWitnessDirectoryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fswMachine = (string)info.GetValue("fswMachine", typeof(string));
			this.directory = (string)info.GetValue("directory", typeof(string));
			this.ex = (Exception)info.GetValue("ex", typeof(Exception));
		}

		// Token: 0x0600B062 RID: 45154 RVA: 0x00295FF5 File Offset: 0x002941F5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fswMachine", this.fswMachine);
			info.AddValue("directory", this.directory);
			info.AddValue("ex", this.ex);
		}

		// Token: 0x17003834 RID: 14388
		// (get) Token: 0x0600B063 RID: 45155 RVA: 0x00296032 File Offset: 0x00294232
		public string FswMachine
		{
			get
			{
				return this.fswMachine;
			}
		}

		// Token: 0x17003835 RID: 14389
		// (get) Token: 0x0600B064 RID: 45156 RVA: 0x0029603A File Offset: 0x0029423A
		public string Directory
		{
			get
			{
				return this.directory;
			}
		}

		// Token: 0x17003836 RID: 14390
		// (get) Token: 0x0600B065 RID: 45157 RVA: 0x00296042 File Offset: 0x00294242
		public Exception Ex
		{
			get
			{
				return this.ex;
			}
		}

		// Token: 0x0400619A RID: 24986
		private readonly string fswMachine;

		// Token: 0x0400619B RID: 24987
		private readonly string directory;

		// Token: 0x0400619C RID: 24988
		private readonly Exception ex;
	}
}
