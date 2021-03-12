using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000CB RID: 203
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClusteringNotInstalledOnLHException : ClusCommonFailException
	{
		// Token: 0x0600072B RID: 1835 RVA: 0x0001BAA4 File Offset: 0x00019CA4
		public ClusteringNotInstalledOnLHException(string errorMessage) : base(Strings.ClusteringNotInstalledOnLHException(errorMessage))
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0001BABE File Offset: 0x00019CBE
		public ClusteringNotInstalledOnLHException(string errorMessage, Exception innerException) : base(Strings.ClusteringNotInstalledOnLHException(errorMessage), innerException)
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001BAD9 File Offset: 0x00019CD9
		protected ClusteringNotInstalledOnLHException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001BB03 File Offset: 0x00019D03
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x0001BB1E File Offset: 0x00019D1E
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x0400071F RID: 1823
		private readonly string errorMessage;
	}
}
