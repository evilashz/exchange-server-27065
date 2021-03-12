using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E00 RID: 3584
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class USGContainerNotFoundException : LocalizedException
	{
		// Token: 0x0600A4F8 RID: 42232 RVA: 0x002852B0 File Offset: 0x002834B0
		public USGContainerNotFoundException(string ou, string domain) : base(Strings.USGContainerNotFoundException(ou, domain))
		{
			this.ou = ou;
			this.domain = domain;
		}

		// Token: 0x0600A4F9 RID: 42233 RVA: 0x002852CD File Offset: 0x002834CD
		public USGContainerNotFoundException(string ou, string domain, Exception innerException) : base(Strings.USGContainerNotFoundException(ou, domain), innerException)
		{
			this.ou = ou;
			this.domain = domain;
		}

		// Token: 0x0600A4FA RID: 42234 RVA: 0x002852EC File Offset: 0x002834EC
		protected USGContainerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ou = (string)info.GetValue("ou", typeof(string));
			this.domain = (string)info.GetValue("domain", typeof(string));
		}

		// Token: 0x0600A4FB RID: 42235 RVA: 0x00285341 File Offset: 0x00283541
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ou", this.ou);
			info.AddValue("domain", this.domain);
		}

		// Token: 0x17003615 RID: 13845
		// (get) Token: 0x0600A4FC RID: 42236 RVA: 0x0028536D File Offset: 0x0028356D
		public string Ou
		{
			get
			{
				return this.ou;
			}
		}

		// Token: 0x17003616 RID: 13846
		// (get) Token: 0x0600A4FD RID: 42237 RVA: 0x00285375 File Offset: 0x00283575
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x04005F7B RID: 24443
		private readonly string ou;

		// Token: 0x04005F7C RID: 24444
		private readonly string domain;
	}
}
