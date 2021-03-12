using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000514 RID: 1300
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InsufficientSparesForExtraCopiesException : DatabaseCopyLayoutException
	{
		// Token: 0x06002F79 RID: 12153 RVA: 0x000C5A01 File Offset: 0x000C3C01
		public InsufficientSparesForExtraCopiesException(int spares, int copies) : base(ReplayStrings.InsufficientSparesForExtraCopiesException(spares, copies))
		{
			this.spares = spares;
			this.copies = copies;
		}

		// Token: 0x06002F7A RID: 12154 RVA: 0x000C5A23 File Offset: 0x000C3C23
		public InsufficientSparesForExtraCopiesException(int spares, int copies, Exception innerException) : base(ReplayStrings.InsufficientSparesForExtraCopiesException(spares, copies), innerException)
		{
			this.spares = spares;
			this.copies = copies;
		}

		// Token: 0x06002F7B RID: 12155 RVA: 0x000C5A48 File Offset: 0x000C3C48
		protected InsufficientSparesForExtraCopiesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.spares = (int)info.GetValue("spares", typeof(int));
			this.copies = (int)info.GetValue("copies", typeof(int));
		}

		// Token: 0x06002F7C RID: 12156 RVA: 0x000C5A9D File Offset: 0x000C3C9D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("spares", this.spares);
			info.AddValue("copies", this.copies);
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x06002F7D RID: 12157 RVA: 0x000C5AC9 File Offset: 0x000C3CC9
		public int Spares
		{
			get
			{
				return this.spares;
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x06002F7E RID: 12158 RVA: 0x000C5AD1 File Offset: 0x000C3CD1
		public int Copies
		{
			get
			{
				return this.copies;
			}
		}

		// Token: 0x040015D0 RID: 5584
		private readonly int spares;

		// Token: 0x040015D1 RID: 5585
		private readonly int copies;
	}
}
