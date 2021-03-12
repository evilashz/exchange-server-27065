using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F34 RID: 3892
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplicationCheckHighPriorityFailedException : ReplicationCheckException
	{
		// Token: 0x0600AAFC RID: 43772 RVA: 0x0028E5C1 File Offset: 0x0028C7C1
		public ReplicationCheckHighPriorityFailedException(string checkTitle, string errorMessage) : base(Strings.ReplicationCheckHighPriorityFailedException(checkTitle, errorMessage))
		{
			this.checkTitle = checkTitle;
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600AAFD RID: 43773 RVA: 0x0028E5DE File Offset: 0x0028C7DE
		public ReplicationCheckHighPriorityFailedException(string checkTitle, string errorMessage, Exception innerException) : base(Strings.ReplicationCheckHighPriorityFailedException(checkTitle, errorMessage), innerException)
		{
			this.checkTitle = checkTitle;
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600AAFE RID: 43774 RVA: 0x0028E5FC File Offset: 0x0028C7FC
		protected ReplicationCheckHighPriorityFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.checkTitle = (string)info.GetValue("checkTitle", typeof(string));
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x0600AAFF RID: 43775 RVA: 0x0028E651 File Offset: 0x0028C851
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("checkTitle", this.checkTitle);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17003749 RID: 14153
		// (get) Token: 0x0600AB00 RID: 43776 RVA: 0x0028E67D File Offset: 0x0028C87D
		public string CheckTitle
		{
			get
			{
				return this.checkTitle;
			}
		}

		// Token: 0x1700374A RID: 14154
		// (get) Token: 0x0600AB01 RID: 43777 RVA: 0x0028E685 File Offset: 0x0028C885
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x040060AF RID: 24751
		private readonly string checkTitle;

		// Token: 0x040060B0 RID: 24752
		private readonly string errorMessage;
	}
}
