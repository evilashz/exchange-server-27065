using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200107F RID: 4223
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskClusterNameIsNotDagNameException : LocalizedException
	{
		// Token: 0x0600B164 RID: 45412 RVA: 0x00297FE2 File Offset: 0x002961E2
		public DagTaskClusterNameIsNotDagNameException(string mailboxServer, string clusterName, string dagName) : base(Strings.DagTaskClusterNameIsNotDagNameException(mailboxServer, clusterName, dagName))
		{
			this.mailboxServer = mailboxServer;
			this.clusterName = clusterName;
			this.dagName = dagName;
		}

		// Token: 0x0600B165 RID: 45413 RVA: 0x00298007 File Offset: 0x00296207
		public DagTaskClusterNameIsNotDagNameException(string mailboxServer, string clusterName, string dagName, Exception innerException) : base(Strings.DagTaskClusterNameIsNotDagNameException(mailboxServer, clusterName, dagName), innerException)
		{
			this.mailboxServer = mailboxServer;
			this.clusterName = clusterName;
			this.dagName = dagName;
		}

		// Token: 0x0600B166 RID: 45414 RVA: 0x00298030 File Offset: 0x00296230
		protected DagTaskClusterNameIsNotDagNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailboxServer = (string)info.GetValue("mailboxServer", typeof(string));
			this.clusterName = (string)info.GetValue("clusterName", typeof(string));
			this.dagName = (string)info.GetValue("dagName", typeof(string));
		}

		// Token: 0x0600B167 RID: 45415 RVA: 0x002980A5 File Offset: 0x002962A5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailboxServer", this.mailboxServer);
			info.AddValue("clusterName", this.clusterName);
			info.AddValue("dagName", this.dagName);
		}

		// Token: 0x17003885 RID: 14469
		// (get) Token: 0x0600B168 RID: 45416 RVA: 0x002980E2 File Offset: 0x002962E2
		public string MailboxServer
		{
			get
			{
				return this.mailboxServer;
			}
		}

		// Token: 0x17003886 RID: 14470
		// (get) Token: 0x0600B169 RID: 45417 RVA: 0x002980EA File Offset: 0x002962EA
		public string ClusterName
		{
			get
			{
				return this.clusterName;
			}
		}

		// Token: 0x17003887 RID: 14471
		// (get) Token: 0x0600B16A RID: 45418 RVA: 0x002980F2 File Offset: 0x002962F2
		public string DagName
		{
			get
			{
				return this.dagName;
			}
		}

		// Token: 0x040061EB RID: 25067
		private readonly string mailboxServer;

		// Token: 0x040061EC RID: 25068
		private readonly string clusterName;

		// Token: 0x040061ED RID: 25069
		private readonly string dagName;
	}
}
