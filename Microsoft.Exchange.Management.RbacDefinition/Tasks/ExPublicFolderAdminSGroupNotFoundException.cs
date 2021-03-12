using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.RbacDefinition;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000031 RID: 49
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExPublicFolderAdminSGroupNotFoundException : LocalizedException
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00049513 File Offset: 0x00047713
		public ExPublicFolderAdminSGroupNotFoundException(Guid guid) : base(Strings.ExPublicFolderAdminSGroupNotFoundException(guid))
		{
			this.guid = guid;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00049528 File Offset: 0x00047728
		public ExPublicFolderAdminSGroupNotFoundException(Guid guid, Exception innerException) : base(Strings.ExPublicFolderAdminSGroupNotFoundException(guid), innerException)
		{
			this.guid = guid;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0004953E File Offset: 0x0004773E
		protected ExPublicFolderAdminSGroupNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.guid = (Guid)info.GetValue("guid", typeof(Guid));
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00049568 File Offset: 0x00047768
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("guid", this.guid);
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00049588 File Offset: 0x00047788
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x04000057 RID: 87
		private readonly Guid guid;
	}
}
