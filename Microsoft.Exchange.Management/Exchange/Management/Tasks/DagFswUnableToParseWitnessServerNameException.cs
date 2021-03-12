using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001057 RID: 4183
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagFswUnableToParseWitnessServerNameException : LocalizedException
	{
		// Token: 0x0600B083 RID: 45187 RVA: 0x00296505 File Offset: 0x00294705
		public DagFswUnableToParseWitnessServerNameException(Exception ex) : base(Strings.DagFswUnableToParseWitnessServerNameException(ex))
		{
			this.ex = ex;
		}

		// Token: 0x0600B084 RID: 45188 RVA: 0x0029651A File Offset: 0x0029471A
		public DagFswUnableToParseWitnessServerNameException(Exception ex, Exception innerException) : base(Strings.DagFswUnableToParseWitnessServerNameException(ex), innerException)
		{
			this.ex = ex;
		}

		// Token: 0x0600B085 RID: 45189 RVA: 0x00296530 File Offset: 0x00294730
		protected DagFswUnableToParseWitnessServerNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ex = (Exception)info.GetValue("ex", typeof(Exception));
		}

		// Token: 0x0600B086 RID: 45190 RVA: 0x0029655A File Offset: 0x0029475A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ex", this.ex);
		}

		// Token: 0x17003844 RID: 14404
		// (get) Token: 0x0600B087 RID: 45191 RVA: 0x00296575 File Offset: 0x00294775
		public Exception Ex
		{
			get
			{
				return this.ex;
			}
		}

		// Token: 0x040061AA RID: 25002
		private readonly Exception ex;
	}
}
