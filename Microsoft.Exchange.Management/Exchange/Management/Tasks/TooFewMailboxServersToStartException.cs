using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200105E RID: 4190
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TooFewMailboxServersToStartException : LocalizedException
	{
		// Token: 0x0600B0A6 RID: 45222 RVA: 0x00296858 File Offset: 0x00294A58
		public TooFewMailboxServersToStartException(string dag, int number) : base(Strings.TooFewMailboxServersToStart(dag, number))
		{
			this.dag = dag;
			this.number = number;
		}

		// Token: 0x0600B0A7 RID: 45223 RVA: 0x00296875 File Offset: 0x00294A75
		public TooFewMailboxServersToStartException(string dag, int number, Exception innerException) : base(Strings.TooFewMailboxServersToStart(dag, number), innerException)
		{
			this.dag = dag;
			this.number = number;
		}

		// Token: 0x0600B0A8 RID: 45224 RVA: 0x00296894 File Offset: 0x00294A94
		protected TooFewMailboxServersToStartException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dag = (string)info.GetValue("dag", typeof(string));
			this.number = (int)info.GetValue("number", typeof(int));
		}

		// Token: 0x0600B0A9 RID: 45225 RVA: 0x002968E9 File Offset: 0x00294AE9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dag", this.dag);
			info.AddValue("number", this.number);
		}

		// Token: 0x1700384B RID: 14411
		// (get) Token: 0x0600B0AA RID: 45226 RVA: 0x00296915 File Offset: 0x00294B15
		public string Dag
		{
			get
			{
				return this.dag;
			}
		}

		// Token: 0x1700384C RID: 14412
		// (get) Token: 0x0600B0AB RID: 45227 RVA: 0x0029691D File Offset: 0x00294B1D
		public int Number
		{
			get
			{
				return this.number;
			}
		}

		// Token: 0x040061B1 RID: 25009
		private readonly string dag;

		// Token: 0x040061B2 RID: 25010
		private readonly int number;
	}
}
