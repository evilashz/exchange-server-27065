using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200031F RID: 799
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnexpectedFilterTypePermanentException : ContentFilterPermanentException
	{
		// Token: 0x06002554 RID: 9556 RVA: 0x00051582 File Offset: 0x0004F782
		public UnexpectedFilterTypePermanentException(string filterTypeName) : base(MrsStrings.UnexpectedFilterType(filterTypeName))
		{
			this.filterTypeName = filterTypeName;
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x00051597 File Offset: 0x0004F797
		public UnexpectedFilterTypePermanentException(string filterTypeName, Exception innerException) : base(MrsStrings.UnexpectedFilterType(filterTypeName), innerException)
		{
			this.filterTypeName = filterTypeName;
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x000515AD File Offset: 0x0004F7AD
		protected UnexpectedFilterTypePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filterTypeName = (string)info.GetValue("filterTypeName", typeof(string));
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x000515D7 File Offset: 0x0004F7D7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filterTypeName", this.filterTypeName);
		}

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x06002558 RID: 9560 RVA: 0x000515F2 File Offset: 0x0004F7F2
		public string FilterTypeName
		{
			get
			{
				return this.filterTypeName;
			}
		}

		// Token: 0x04001021 RID: 4129
		private readonly string filterTypeName;
	}
}
