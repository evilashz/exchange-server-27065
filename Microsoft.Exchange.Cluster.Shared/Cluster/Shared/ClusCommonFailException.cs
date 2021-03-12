using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000B7 RID: 183
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClusCommonFailException : ClusterException
	{
		// Token: 0x060006C5 RID: 1733 RVA: 0x0001AFF9 File Offset: 0x000191F9
		public ClusCommonFailException(string error) : base(Strings.ClusCommonFailException(error))
		{
			this.error = error;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001B013 File Offset: 0x00019213
		public ClusCommonFailException(string error, Exception innerException) : base(Strings.ClusCommonFailException(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001B02E File Offset: 0x0001922E
		protected ClusCommonFailException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001B058 File Offset: 0x00019258
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0001B073 File Offset: 0x00019273
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04000709 RID: 1801
		private readonly string error;
	}
}
