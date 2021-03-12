using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F6C RID: 3948
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotRemoveMailUserCatchAllRecipientException : LocalizedException
	{
		// Token: 0x0600AC0B RID: 44043 RVA: 0x0028FDB9 File Offset: 0x0028DFB9
		public CannotRemoveMailUserCatchAllRecipientException(string domain) : base(Strings.CannotRemoveMailUserCatchAllRecipient(domain))
		{
			this.domain = domain;
		}

		// Token: 0x0600AC0C RID: 44044 RVA: 0x0028FDCE File Offset: 0x0028DFCE
		public CannotRemoveMailUserCatchAllRecipientException(string domain, Exception innerException) : base(Strings.CannotRemoveMailUserCatchAllRecipient(domain), innerException)
		{
			this.domain = domain;
		}

		// Token: 0x0600AC0D RID: 44045 RVA: 0x0028FDE4 File Offset: 0x0028DFE4
		protected CannotRemoveMailUserCatchAllRecipientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (string)info.GetValue("domain", typeof(string));
		}

		// Token: 0x0600AC0E RID: 44046 RVA: 0x0028FE0E File Offset: 0x0028E00E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
		}

		// Token: 0x17003778 RID: 14200
		// (get) Token: 0x0600AC0F RID: 44047 RVA: 0x0028FE29 File Offset: 0x0028E029
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x040060DE RID: 24798
		private readonly string domain;
	}
}
