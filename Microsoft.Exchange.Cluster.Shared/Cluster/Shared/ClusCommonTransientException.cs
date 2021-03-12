using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000B8 RID: 184
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClusCommonTransientException : TransientException
	{
		// Token: 0x060006CA RID: 1738 RVA: 0x0001B07B File Offset: 0x0001927B
		public ClusCommonTransientException(string error) : base(Strings.ClusCommonTransientException(error))
		{
			this.error = error;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001B090 File Offset: 0x00019290
		public ClusCommonTransientException(string error, Exception innerException) : base(Strings.ClusCommonTransientException(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0001B0A6 File Offset: 0x000192A6
		protected ClusCommonTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0001B0D0 File Offset: 0x000192D0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x0001B0EB File Offset: 0x000192EB
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x0400070A RID: 1802
		private readonly string error;
	}
}
