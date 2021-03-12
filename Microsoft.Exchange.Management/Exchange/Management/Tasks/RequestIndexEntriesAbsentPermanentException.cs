using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EC8 RID: 3784
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RequestIndexEntriesAbsentPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A8C0 RID: 43200 RVA: 0x0028A7E4 File Offset: 0x002889E4
		public RequestIndexEntriesAbsentPermanentException(string name) : base(Strings.ErrorCouldNotFindIndexEntriesForRequest(name))
		{
			this.name = name;
		}

		// Token: 0x0600A8C1 RID: 43201 RVA: 0x0028A7F9 File Offset: 0x002889F9
		public RequestIndexEntriesAbsentPermanentException(string name, Exception innerException) : base(Strings.ErrorCouldNotFindIndexEntriesForRequest(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600A8C2 RID: 43202 RVA: 0x0028A80F File Offset: 0x00288A0F
		protected RequestIndexEntriesAbsentPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600A8C3 RID: 43203 RVA: 0x0028A839 File Offset: 0x00288A39
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x170036BD RID: 14013
		// (get) Token: 0x0600A8C4 RID: 43204 RVA: 0x0028A854 File Offset: 0x00288A54
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04006023 RID: 24611
		private readonly string name;
	}
}
