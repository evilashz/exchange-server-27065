using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020010B5 RID: 4277
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotRemoveAccountNamespaceException : FederationException
	{
		// Token: 0x0600B279 RID: 45689 RVA: 0x00299C16 File Offset: 0x00297E16
		public CannotRemoveAccountNamespaceException(string domain) : base(Strings.ErrorCannotRemoveAccountNamespace(domain))
		{
			this.domain = domain;
		}

		// Token: 0x0600B27A RID: 45690 RVA: 0x00299C2B File Offset: 0x00297E2B
		public CannotRemoveAccountNamespaceException(string domain, Exception innerException) : base(Strings.ErrorCannotRemoveAccountNamespace(domain), innerException)
		{
			this.domain = domain;
		}

		// Token: 0x0600B27B RID: 45691 RVA: 0x00299C41 File Offset: 0x00297E41
		protected CannotRemoveAccountNamespaceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (string)info.GetValue("domain", typeof(string));
		}

		// Token: 0x0600B27C RID: 45692 RVA: 0x00299C6B File Offset: 0x00297E6B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
		}

		// Token: 0x170038C2 RID: 14530
		// (get) Token: 0x0600B27D RID: 45693 RVA: 0x00299C86 File Offset: 0x00297E86
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x04006228 RID: 25128
		private readonly string domain;
	}
}
