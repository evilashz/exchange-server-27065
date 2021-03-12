using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.RbacDefinition;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000030 RID: 48
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExOrgAdminSGroupNotFoundException : LocalizedException
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00049496 File Offset: 0x00047696
		public ExOrgAdminSGroupNotFoundException(Guid guid) : base(Strings.ExOrgAdminSGroupNotFoundException(guid))
		{
			this.guid = guid;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000494AB File Offset: 0x000476AB
		public ExOrgAdminSGroupNotFoundException(Guid guid, Exception innerException) : base(Strings.ExOrgAdminSGroupNotFoundException(guid), innerException)
		{
			this.guid = guid;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000494C1 File Offset: 0x000476C1
		protected ExOrgAdminSGroupNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.guid = (Guid)info.GetValue("guid", typeof(Guid));
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000494EB File Offset: 0x000476EB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("guid", this.guid);
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0004950B File Offset: 0x0004770B
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x04000056 RID: 86
		private readonly Guid guid;
	}
}
