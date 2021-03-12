using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F32 RID: 3890
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplicationCheckWarningException : ReplicationCheckException
	{
		// Token: 0x0600AAF0 RID: 43760 RVA: 0x0028E429 File Offset: 0x0028C629
		public ReplicationCheckWarningException(string checkTitle, string warningMessage) : base(Strings.ReplicationCheckWarningException(checkTitle, warningMessage))
		{
			this.checkTitle = checkTitle;
			this.warningMessage = warningMessage;
		}

		// Token: 0x0600AAF1 RID: 43761 RVA: 0x0028E446 File Offset: 0x0028C646
		public ReplicationCheckWarningException(string checkTitle, string warningMessage, Exception innerException) : base(Strings.ReplicationCheckWarningException(checkTitle, warningMessage), innerException)
		{
			this.checkTitle = checkTitle;
			this.warningMessage = warningMessage;
		}

		// Token: 0x0600AAF2 RID: 43762 RVA: 0x0028E464 File Offset: 0x0028C664
		protected ReplicationCheckWarningException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.checkTitle = (string)info.GetValue("checkTitle", typeof(string));
			this.warningMessage = (string)info.GetValue("warningMessage", typeof(string));
		}

		// Token: 0x0600AAF3 RID: 43763 RVA: 0x0028E4B9 File Offset: 0x0028C6B9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("checkTitle", this.checkTitle);
			info.AddValue("warningMessage", this.warningMessage);
		}

		// Token: 0x17003745 RID: 14149
		// (get) Token: 0x0600AAF4 RID: 43764 RVA: 0x0028E4E5 File Offset: 0x0028C6E5
		public string CheckTitle
		{
			get
			{
				return this.checkTitle;
			}
		}

		// Token: 0x17003746 RID: 14150
		// (get) Token: 0x0600AAF5 RID: 43765 RVA: 0x0028E4ED File Offset: 0x0028C6ED
		public string WarningMessage
		{
			get
			{
				return this.warningMessage;
			}
		}

		// Token: 0x040060AB RID: 24747
		private readonly string checkTitle;

		// Token: 0x040060AC RID: 24748
		private readonly string warningMessage;
	}
}
