using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F6A RID: 3946
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotRemoveMailboxCatchAllRecipientException : LocalizedException
	{
		// Token: 0x0600AC01 RID: 44033 RVA: 0x0028FCC9 File Offset: 0x0028DEC9
		public CannotRemoveMailboxCatchAllRecipientException(string domain) : base(Strings.CannotRemoveMailboxCatchAllRecipient(domain))
		{
			this.domain = domain;
		}

		// Token: 0x0600AC02 RID: 44034 RVA: 0x0028FCDE File Offset: 0x0028DEDE
		public CannotRemoveMailboxCatchAllRecipientException(string domain, Exception innerException) : base(Strings.CannotRemoveMailboxCatchAllRecipient(domain), innerException)
		{
			this.domain = domain;
		}

		// Token: 0x0600AC03 RID: 44035 RVA: 0x0028FCF4 File Offset: 0x0028DEF4
		protected CannotRemoveMailboxCatchAllRecipientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (string)info.GetValue("domain", typeof(string));
		}

		// Token: 0x0600AC04 RID: 44036 RVA: 0x0028FD1E File Offset: 0x0028DF1E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
		}

		// Token: 0x17003776 RID: 14198
		// (get) Token: 0x0600AC05 RID: 44037 RVA: 0x0028FD39 File Offset: 0x0028DF39
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x040060DC RID: 24796
		private readonly string domain;
	}
}
