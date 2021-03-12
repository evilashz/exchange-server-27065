using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x02000013 RID: 19
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MissingDatabaseActivationPreferenceException : MailboxLoadBalancePermanentException
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00002B2D File Offset: 0x00000D2D
		public MissingDatabaseActivationPreferenceException(string databaseName) : base(MigrationWorkflowServiceStrings.ErrorMissingDatabaseActivationPreference(databaseName))
		{
			this.databaseName = databaseName;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002B42 File Offset: 0x00000D42
		public MissingDatabaseActivationPreferenceException(string databaseName, Exception innerException) : base(MigrationWorkflowServiceStrings.ErrorMissingDatabaseActivationPreference(databaseName), innerException)
		{
			this.databaseName = databaseName;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002B58 File Offset: 0x00000D58
		protected MissingDatabaseActivationPreferenceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseName = (string)info.GetValue("databaseName", typeof(string));
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002B82 File Offset: 0x00000D82
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseName", this.databaseName);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002B9D File Offset: 0x00000D9D
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
		}

		// Token: 0x0400002A RID: 42
		private readonly string databaseName;
	}
}
