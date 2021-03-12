using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ManagementEndpoint
{
	// Token: 0x020010AA RID: 4266
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorRedirectionEntryFailedToAddException : LocalizedException
	{
		// Token: 0x0600B249 RID: 45641 RVA: 0x00299905 File Offset: 0x00297B05
		public ErrorRedirectionEntryFailedToAddException(string domainName) : base(Strings.ErrorRedirectionEntryFailedToAdd(domainName))
		{
			this.domainName = domainName;
		}

		// Token: 0x0600B24A RID: 45642 RVA: 0x0029991A File Offset: 0x00297B1A
		public ErrorRedirectionEntryFailedToAddException(string domainName, Exception innerException) : base(Strings.ErrorRedirectionEntryFailedToAdd(domainName), innerException)
		{
			this.domainName = domainName;
		}

		// Token: 0x0600B24B RID: 45643 RVA: 0x00299930 File Offset: 0x00297B30
		protected ErrorRedirectionEntryFailedToAddException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domainName = (string)info.GetValue("domainName", typeof(string));
		}

		// Token: 0x0600B24C RID: 45644 RVA: 0x0029995A File Offset: 0x00297B5A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domainName", this.domainName);
		}

		// Token: 0x170038BE RID: 14526
		// (get) Token: 0x0600B24D RID: 45645 RVA: 0x00299975 File Offset: 0x00297B75
		public string DomainName
		{
			get
			{
				return this.domainName;
			}
		}

		// Token: 0x04006224 RID: 25124
		private readonly string domainName;
	}
}
