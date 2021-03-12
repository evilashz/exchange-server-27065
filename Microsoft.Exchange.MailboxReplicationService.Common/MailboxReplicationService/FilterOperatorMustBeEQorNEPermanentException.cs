using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002CA RID: 714
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FilterOperatorMustBeEQorNEPermanentException : ContentFilterPermanentException
	{
		// Token: 0x060023B6 RID: 9142 RVA: 0x0004EF78 File Offset: 0x0004D178
		public FilterOperatorMustBeEQorNEPermanentException(string propertyName) : base(MrsStrings.FilterOperatorMustBeEQorNE(propertyName))
		{
			this.propertyName = propertyName;
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x0004EF8D File Offset: 0x0004D18D
		public FilterOperatorMustBeEQorNEPermanentException(string propertyName, Exception innerException) : base(MrsStrings.FilterOperatorMustBeEQorNE(propertyName), innerException)
		{
			this.propertyName = propertyName;
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x0004EFA3 File Offset: 0x0004D1A3
		protected FilterOperatorMustBeEQorNEPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.propertyName = (string)info.GetValue("propertyName", typeof(string));
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x0004EFCD File Offset: 0x0004D1CD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("propertyName", this.propertyName);
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x060023BA RID: 9146 RVA: 0x0004EFE8 File Offset: 0x0004D1E8
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x04000FD7 RID: 4055
		private readonly string propertyName;
	}
}
