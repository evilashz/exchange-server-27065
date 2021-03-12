using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001086 RID: 4230
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NewDagServerIsAlreadyManuallyConfiguredForClusteringButIsNotInDagException : LocalizedException
	{
		// Token: 0x0600B18D RID: 45453 RVA: 0x00298516 File Offset: 0x00296716
		public NewDagServerIsAlreadyManuallyConfiguredForClusteringButIsNotInDagException(string mailboxServer, string dagName) : base(Strings.NewDagServerIsAlreadyManuallyConfiguredForClusteringButIsNotInDagException(mailboxServer, dagName))
		{
			this.mailboxServer = mailboxServer;
			this.dagName = dagName;
		}

		// Token: 0x0600B18E RID: 45454 RVA: 0x00298533 File Offset: 0x00296733
		public NewDagServerIsAlreadyManuallyConfiguredForClusteringButIsNotInDagException(string mailboxServer, string dagName, Exception innerException) : base(Strings.NewDagServerIsAlreadyManuallyConfiguredForClusteringButIsNotInDagException(mailboxServer, dagName), innerException)
		{
			this.mailboxServer = mailboxServer;
			this.dagName = dagName;
		}

		// Token: 0x0600B18F RID: 45455 RVA: 0x00298554 File Offset: 0x00296754
		protected NewDagServerIsAlreadyManuallyConfiguredForClusteringButIsNotInDagException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailboxServer = (string)info.GetValue("mailboxServer", typeof(string));
			this.dagName = (string)info.GetValue("dagName", typeof(string));
		}

		// Token: 0x0600B190 RID: 45456 RVA: 0x002985A9 File Offset: 0x002967A9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailboxServer", this.mailboxServer);
			info.AddValue("dagName", this.dagName);
		}

		// Token: 0x17003892 RID: 14482
		// (get) Token: 0x0600B191 RID: 45457 RVA: 0x002985D5 File Offset: 0x002967D5
		public string MailboxServer
		{
			get
			{
				return this.mailboxServer;
			}
		}

		// Token: 0x17003893 RID: 14483
		// (get) Token: 0x0600B192 RID: 45458 RVA: 0x002985DD File Offset: 0x002967DD
		public string DagName
		{
			get
			{
				return this.dagName;
			}
		}

		// Token: 0x040061F8 RID: 25080
		private readonly string mailboxServer;

		// Token: 0x040061F9 RID: 25081
		private readonly string dagName;
	}
}
