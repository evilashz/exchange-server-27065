using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F6B RID: 3947
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotRemoveMailContactCatchAllRecipientException : LocalizedException
	{
		// Token: 0x0600AC06 RID: 44038 RVA: 0x0028FD41 File Offset: 0x0028DF41
		public CannotRemoveMailContactCatchAllRecipientException(string domain) : base(Strings.CannotRemoveMailContactCatchAllRecipient(domain))
		{
			this.domain = domain;
		}

		// Token: 0x0600AC07 RID: 44039 RVA: 0x0028FD56 File Offset: 0x0028DF56
		public CannotRemoveMailContactCatchAllRecipientException(string domain, Exception innerException) : base(Strings.CannotRemoveMailContactCatchAllRecipient(domain), innerException)
		{
			this.domain = domain;
		}

		// Token: 0x0600AC08 RID: 44040 RVA: 0x0028FD6C File Offset: 0x0028DF6C
		protected CannotRemoveMailContactCatchAllRecipientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (string)info.GetValue("domain", typeof(string));
		}

		// Token: 0x0600AC09 RID: 44041 RVA: 0x0028FD96 File Offset: 0x0028DF96
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
		}

		// Token: 0x17003777 RID: 14199
		// (get) Token: 0x0600AC0A RID: 44042 RVA: 0x0028FDB1 File Offset: 0x0028DFB1
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x040060DD RID: 24797
		private readonly string domain;
	}
}
