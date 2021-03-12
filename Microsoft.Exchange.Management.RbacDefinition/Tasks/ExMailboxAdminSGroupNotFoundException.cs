using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.RbacDefinition;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000032 RID: 50
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExMailboxAdminSGroupNotFoundException : LocalizedException
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x00049590 File Offset: 0x00047790
		public ExMailboxAdminSGroupNotFoundException(Guid guid) : base(Strings.ExMailboxAdminSGroupNotFoundException(guid))
		{
			this.guid = guid;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000495A5 File Offset: 0x000477A5
		public ExMailboxAdminSGroupNotFoundException(Guid guid, Exception innerException) : base(Strings.ExMailboxAdminSGroupNotFoundException(guid), innerException)
		{
			this.guid = guid;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000495BB File Offset: 0x000477BB
		protected ExMailboxAdminSGroupNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.guid = (Guid)info.GetValue("guid", typeof(Guid));
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000495E5 File Offset: 0x000477E5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("guid", this.guid);
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00049605 File Offset: 0x00047805
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x04000058 RID: 88
		private readonly Guid guid;
	}
}
