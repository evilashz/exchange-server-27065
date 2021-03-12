using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001053 RID: 4179
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagFswUnableToCreateWitnessShareException : LocalizedException
	{
		// Token: 0x0600B066 RID: 45158 RVA: 0x0029604A File Offset: 0x0029424A
		public DagFswUnableToCreateWitnessShareException(string fswMachine, string share, Exception ex) : base(Strings.DagFswUnableToCreateWitnessShareException(fswMachine, share, ex))
		{
			this.fswMachine = fswMachine;
			this.share = share;
			this.ex = ex;
		}

		// Token: 0x0600B067 RID: 45159 RVA: 0x0029606F File Offset: 0x0029426F
		public DagFswUnableToCreateWitnessShareException(string fswMachine, string share, Exception ex, Exception innerException) : base(Strings.DagFswUnableToCreateWitnessShareException(fswMachine, share, ex), innerException)
		{
			this.fswMachine = fswMachine;
			this.share = share;
			this.ex = ex;
		}

		// Token: 0x0600B068 RID: 45160 RVA: 0x00296098 File Offset: 0x00294298
		protected DagFswUnableToCreateWitnessShareException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fswMachine = (string)info.GetValue("fswMachine", typeof(string));
			this.share = (string)info.GetValue("share", typeof(string));
			this.ex = (Exception)info.GetValue("ex", typeof(Exception));
		}

		// Token: 0x0600B069 RID: 45161 RVA: 0x0029610D File Offset: 0x0029430D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fswMachine", this.fswMachine);
			info.AddValue("share", this.share);
			info.AddValue("ex", this.ex);
		}

		// Token: 0x17003837 RID: 14391
		// (get) Token: 0x0600B06A RID: 45162 RVA: 0x0029614A File Offset: 0x0029434A
		public string FswMachine
		{
			get
			{
				return this.fswMachine;
			}
		}

		// Token: 0x17003838 RID: 14392
		// (get) Token: 0x0600B06B RID: 45163 RVA: 0x00296152 File Offset: 0x00294352
		public string Share
		{
			get
			{
				return this.share;
			}
		}

		// Token: 0x17003839 RID: 14393
		// (get) Token: 0x0600B06C RID: 45164 RVA: 0x0029615A File Offset: 0x0029435A
		public Exception Ex
		{
			get
			{
				return this.ex;
			}
		}

		// Token: 0x0400619D RID: 24989
		private readonly string fswMachine;

		// Token: 0x0400619E RID: 24990
		private readonly string share;

		// Token: 0x0400619F RID: 24991
		private readonly Exception ex;
	}
}
