using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001049 RID: 4169
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagServerCantBeRemovedInDatacenterActivationMode : LocalizedException
	{
		// Token: 0x0600B02A RID: 45098 RVA: 0x00295864 File Offset: 0x00293A64
		public DagServerCantBeRemovedInDatacenterActivationMode(string mailbox, string dagName) : base(Strings.DagServerCantBeRemovedInDatacenterActivationMode(mailbox, dagName))
		{
			this.mailbox = mailbox;
			this.dagName = dagName;
		}

		// Token: 0x0600B02B RID: 45099 RVA: 0x00295881 File Offset: 0x00293A81
		public DagServerCantBeRemovedInDatacenterActivationMode(string mailbox, string dagName, Exception innerException) : base(Strings.DagServerCantBeRemovedInDatacenterActivationMode(mailbox, dagName), innerException)
		{
			this.mailbox = mailbox;
			this.dagName = dagName;
		}

		// Token: 0x0600B02C RID: 45100 RVA: 0x002958A0 File Offset: 0x00293AA0
		protected DagServerCantBeRemovedInDatacenterActivationMode(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailbox = (string)info.GetValue("mailbox", typeof(string));
			this.dagName = (string)info.GetValue("dagName", typeof(string));
		}

		// Token: 0x0600B02D RID: 45101 RVA: 0x002958F5 File Offset: 0x00293AF5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailbox", this.mailbox);
			info.AddValue("dagName", this.dagName);
		}

		// Token: 0x17003823 RID: 14371
		// (get) Token: 0x0600B02E RID: 45102 RVA: 0x00295921 File Offset: 0x00293B21
		public string Mailbox
		{
			get
			{
				return this.mailbox;
			}
		}

		// Token: 0x17003824 RID: 14372
		// (get) Token: 0x0600B02F RID: 45103 RVA: 0x00295929 File Offset: 0x00293B29
		public string DagName
		{
			get
			{
				return this.dagName;
			}
		}

		// Token: 0x04006189 RID: 24969
		private readonly string mailbox;

		// Token: 0x0400618A RID: 24970
		private readonly string dagName;
	}
}
