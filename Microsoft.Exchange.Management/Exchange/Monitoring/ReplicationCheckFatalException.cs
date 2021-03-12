using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F33 RID: 3891
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplicationCheckFatalException : ReplicationCheckException
	{
		// Token: 0x0600AAF6 RID: 43766 RVA: 0x0028E4F5 File Offset: 0x0028C6F5
		public ReplicationCheckFatalException(string checkTitle, string errorMessage) : base(Strings.ReplicationCheckFatalException(checkTitle, errorMessage))
		{
			this.checkTitle = checkTitle;
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600AAF7 RID: 43767 RVA: 0x0028E512 File Offset: 0x0028C712
		public ReplicationCheckFatalException(string checkTitle, string errorMessage, Exception innerException) : base(Strings.ReplicationCheckFatalException(checkTitle, errorMessage), innerException)
		{
			this.checkTitle = checkTitle;
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600AAF8 RID: 43768 RVA: 0x0028E530 File Offset: 0x0028C730
		protected ReplicationCheckFatalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.checkTitle = (string)info.GetValue("checkTitle", typeof(string));
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x0600AAF9 RID: 43769 RVA: 0x0028E585 File Offset: 0x0028C785
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("checkTitle", this.checkTitle);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17003747 RID: 14151
		// (get) Token: 0x0600AAFA RID: 43770 RVA: 0x0028E5B1 File Offset: 0x0028C7B1
		public string CheckTitle
		{
			get
			{
				return this.checkTitle;
			}
		}

		// Token: 0x17003748 RID: 14152
		// (get) Token: 0x0600AAFB RID: 43771 RVA: 0x0028E5B9 File Offset: 0x0028C7B9
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x040060AD RID: 24749
		private readonly string checkTitle;

		// Token: 0x040060AE RID: 24750
		private readonly string errorMessage;
	}
}
