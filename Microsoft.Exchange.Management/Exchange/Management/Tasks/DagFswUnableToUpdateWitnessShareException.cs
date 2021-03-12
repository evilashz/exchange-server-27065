using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001054 RID: 4180
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagFswUnableToUpdateWitnessShareException : LocalizedException
	{
		// Token: 0x0600B06D RID: 45165 RVA: 0x00296162 File Offset: 0x00294362
		public DagFswUnableToUpdateWitnessShareException(string fswMachine, string share, Exception ex) : base(Strings.DagFswUnableToUpdateWitnessShareException(fswMachine, share, ex))
		{
			this.fswMachine = fswMachine;
			this.share = share;
			this.ex = ex;
		}

		// Token: 0x0600B06E RID: 45166 RVA: 0x00296187 File Offset: 0x00294387
		public DagFswUnableToUpdateWitnessShareException(string fswMachine, string share, Exception ex, Exception innerException) : base(Strings.DagFswUnableToUpdateWitnessShareException(fswMachine, share, ex), innerException)
		{
			this.fswMachine = fswMachine;
			this.share = share;
			this.ex = ex;
		}

		// Token: 0x0600B06F RID: 45167 RVA: 0x002961B0 File Offset: 0x002943B0
		protected DagFswUnableToUpdateWitnessShareException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fswMachine = (string)info.GetValue("fswMachine", typeof(string));
			this.share = (string)info.GetValue("share", typeof(string));
			this.ex = (Exception)info.GetValue("ex", typeof(Exception));
		}

		// Token: 0x0600B070 RID: 45168 RVA: 0x00296225 File Offset: 0x00294425
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fswMachine", this.fswMachine);
			info.AddValue("share", this.share);
			info.AddValue("ex", this.ex);
		}

		// Token: 0x1700383A RID: 14394
		// (get) Token: 0x0600B071 RID: 45169 RVA: 0x00296262 File Offset: 0x00294462
		public string FswMachine
		{
			get
			{
				return this.fswMachine;
			}
		}

		// Token: 0x1700383B RID: 14395
		// (get) Token: 0x0600B072 RID: 45170 RVA: 0x0029626A File Offset: 0x0029446A
		public string Share
		{
			get
			{
				return this.share;
			}
		}

		// Token: 0x1700383C RID: 14396
		// (get) Token: 0x0600B073 RID: 45171 RVA: 0x00296272 File Offset: 0x00294472
		public Exception Ex
		{
			get
			{
				return this.ex;
			}
		}

		// Token: 0x040061A0 RID: 24992
		private readonly string fswMachine;

		// Token: 0x040061A1 RID: 24993
		private readonly string share;

		// Token: 0x040061A2 RID: 24994
		private readonly Exception ex;
	}
}
