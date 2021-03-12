using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AC2 RID: 2754
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotFindTargetTenantException : ADOperationException
	{
		// Token: 0x06008084 RID: 32900 RVA: 0x001A550D File Offset: 0x001A370D
		public CannotFindTargetTenantException(string oldTenant, string newPartition, string guid) : base(DirectoryStrings.NoMatchingTenantInTargetPartition(oldTenant, newPartition, guid))
		{
			this.oldTenant = oldTenant;
			this.newPartition = newPartition;
			this.guid = guid;
		}

		// Token: 0x06008085 RID: 32901 RVA: 0x001A5532 File Offset: 0x001A3732
		public CannotFindTargetTenantException(string oldTenant, string newPartition, string guid, Exception innerException) : base(DirectoryStrings.NoMatchingTenantInTargetPartition(oldTenant, newPartition, guid), innerException)
		{
			this.oldTenant = oldTenant;
			this.newPartition = newPartition;
			this.guid = guid;
		}

		// Token: 0x06008086 RID: 32902 RVA: 0x001A555C File Offset: 0x001A375C
		protected CannotFindTargetTenantException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.oldTenant = (string)info.GetValue("oldTenant", typeof(string));
			this.newPartition = (string)info.GetValue("newPartition", typeof(string));
			this.guid = (string)info.GetValue("guid", typeof(string));
		}

		// Token: 0x06008087 RID: 32903 RVA: 0x001A55D1 File Offset: 0x001A37D1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("oldTenant", this.oldTenant);
			info.AddValue("newPartition", this.newPartition);
			info.AddValue("guid", this.guid);
		}

		// Token: 0x17002ED3 RID: 11987
		// (get) Token: 0x06008088 RID: 32904 RVA: 0x001A560E File Offset: 0x001A380E
		public string OldTenant
		{
			get
			{
				return this.oldTenant;
			}
		}

		// Token: 0x17002ED4 RID: 11988
		// (get) Token: 0x06008089 RID: 32905 RVA: 0x001A5616 File Offset: 0x001A3816
		public string NewPartition
		{
			get
			{
				return this.newPartition;
			}
		}

		// Token: 0x17002ED5 RID: 11989
		// (get) Token: 0x0600808A RID: 32906 RVA: 0x001A561E File Offset: 0x001A381E
		public string Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x040055AD RID: 21933
		private readonly string oldTenant;

		// Token: 0x040055AE RID: 21934
		private readonly string newPartition;

		// Token: 0x040055AF RID: 21935
		private readonly string guid;
	}
}
