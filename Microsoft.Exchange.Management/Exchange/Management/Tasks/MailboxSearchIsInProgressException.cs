using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000F3F RID: 3903
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxSearchIsInProgressException : LocalizedException
	{
		// Token: 0x0600AB35 RID: 43829 RVA: 0x0028EBA0 File Offset: 0x0028CDA0
		public MailboxSearchIsInProgressException(string name) : base(Strings.MailboxSearchIsInProgress(name))
		{
			this.name = name;
		}

		// Token: 0x0600AB36 RID: 43830 RVA: 0x0028EBB5 File Offset: 0x0028CDB5
		public MailboxSearchIsInProgressException(string name, Exception innerException) : base(Strings.MailboxSearchIsInProgress(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600AB37 RID: 43831 RVA: 0x0028EBCB File Offset: 0x0028CDCB
		protected MailboxSearchIsInProgressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600AB38 RID: 43832 RVA: 0x0028EBF5 File Offset: 0x0028CDF5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x17003756 RID: 14166
		// (get) Token: 0x0600AB39 RID: 43833 RVA: 0x0028EC10 File Offset: 0x0028CE10
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040060BC RID: 24764
		private readonly string name;
	}
}
