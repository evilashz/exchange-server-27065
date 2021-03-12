using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020010BD RID: 4285
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AcceptedDomainsInvalidTypeException : FederationException
	{
		// Token: 0x0600B29D RID: 45725 RVA: 0x00299EB2 File Offset: 0x002980B2
		public AcceptedDomainsInvalidTypeException(string domain) : base(Strings.ErrorAcceptedDomainsInvalidType(domain))
		{
			this.domain = domain;
		}

		// Token: 0x0600B29E RID: 45726 RVA: 0x00299EC7 File Offset: 0x002980C7
		public AcceptedDomainsInvalidTypeException(string domain, Exception innerException) : base(Strings.ErrorAcceptedDomainsInvalidType(domain), innerException)
		{
			this.domain = domain;
		}

		// Token: 0x0600B29F RID: 45727 RVA: 0x00299EDD File Offset: 0x002980DD
		protected AcceptedDomainsInvalidTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (string)info.GetValue("domain", typeof(string));
		}

		// Token: 0x0600B2A0 RID: 45728 RVA: 0x00299F07 File Offset: 0x00298107
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
		}

		// Token: 0x170038C6 RID: 14534
		// (get) Token: 0x0600B2A1 RID: 45729 RVA: 0x00299F22 File Offset: 0x00298122
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x0400622C RID: 25132
		private readonly string domain;
	}
}
