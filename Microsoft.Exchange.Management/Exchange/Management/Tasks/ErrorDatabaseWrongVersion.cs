using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001098 RID: 4248
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorDatabaseWrongVersion : LocalizedException
	{
		// Token: 0x0600B1F9 RID: 45561 RVA: 0x00299356 File Offset: 0x00297556
		public ErrorDatabaseWrongVersion(string dbName) : base(Strings.ErrorDatabaseWrongVersion(dbName))
		{
			this.dbName = dbName;
		}

		// Token: 0x0600B1FA RID: 45562 RVA: 0x0029936B File Offset: 0x0029756B
		public ErrorDatabaseWrongVersion(string dbName, Exception innerException) : base(Strings.ErrorDatabaseWrongVersion(dbName), innerException)
		{
			this.dbName = dbName;
		}

		// Token: 0x0600B1FB RID: 45563 RVA: 0x00299381 File Offset: 0x00297581
		protected ErrorDatabaseWrongVersion(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
		}

		// Token: 0x0600B1FC RID: 45564 RVA: 0x002993AB File Offset: 0x002975AB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
		}

		// Token: 0x170038B6 RID: 14518
		// (get) Token: 0x0600B1FD RID: 45565 RVA: 0x002993C6 File Offset: 0x002975C6
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x0400621C RID: 25116
		private readonly string dbName;
	}
}
