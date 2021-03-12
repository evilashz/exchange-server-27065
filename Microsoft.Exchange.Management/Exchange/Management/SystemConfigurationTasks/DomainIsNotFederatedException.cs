using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020010B4 RID: 4276
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DomainIsNotFederatedException : FederationException
	{
		// Token: 0x0600B274 RID: 45684 RVA: 0x00299B9E File Offset: 0x00297D9E
		public DomainIsNotFederatedException(string domain) : base(Strings.ErrorDomainIsNotFederated(domain))
		{
			this.domain = domain;
		}

		// Token: 0x0600B275 RID: 45685 RVA: 0x00299BB3 File Offset: 0x00297DB3
		public DomainIsNotFederatedException(string domain, Exception innerException) : base(Strings.ErrorDomainIsNotFederated(domain), innerException)
		{
			this.domain = domain;
		}

		// Token: 0x0600B276 RID: 45686 RVA: 0x00299BC9 File Offset: 0x00297DC9
		protected DomainIsNotFederatedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (string)info.GetValue("domain", typeof(string));
		}

		// Token: 0x0600B277 RID: 45687 RVA: 0x00299BF3 File Offset: 0x00297DF3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
		}

		// Token: 0x170038C1 RID: 14529
		// (get) Token: 0x0600B278 RID: 45688 RVA: 0x00299C0E File Offset: 0x00297E0E
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x04006227 RID: 25127
		private readonly string domain;
	}
}
