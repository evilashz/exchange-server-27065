using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200015E RID: 350
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UserDuplicateInCSVException : MigrationPermanentException
	{
		// Token: 0x0600163B RID: 5691 RVA: 0x0006F0E8 File Offset: 0x0006D2E8
		public UserDuplicateInCSVException(string alias) : base(Strings.UserDuplicateInCSV(alias))
		{
			this.alias = alias;
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x0006F0FD File Offset: 0x0006D2FD
		public UserDuplicateInCSVException(string alias, Exception innerException) : base(Strings.UserDuplicateInCSV(alias), innerException)
		{
			this.alias = alias;
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x0006F113 File Offset: 0x0006D313
		protected UserDuplicateInCSVException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.alias = (string)info.GetValue("alias", typeof(string));
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x0006F13D File Offset: 0x0006D33D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("alias", this.alias);
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x0006F158 File Offset: 0x0006D358
		public string Alias
		{
			get
			{
				return this.alias;
			}
		}

		// Token: 0x04000AEB RID: 2795
		private readonly string alias;
	}
}
