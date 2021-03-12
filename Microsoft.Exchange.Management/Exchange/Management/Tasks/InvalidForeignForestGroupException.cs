using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E04 RID: 3588
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidForeignForestGroupException : LocalizedException
	{
		// Token: 0x0600A50D RID: 42253 RVA: 0x002854EF File Offset: 0x002836EF
		public InvalidForeignForestGroupException(string name, string domain) : base(Strings.InvalidForeignForestGroupException(name, domain))
		{
			this.name = name;
			this.domain = domain;
		}

		// Token: 0x0600A50E RID: 42254 RVA: 0x0028550C File Offset: 0x0028370C
		public InvalidForeignForestGroupException(string name, string domain, Exception innerException) : base(Strings.InvalidForeignForestGroupException(name, domain), innerException)
		{
			this.name = name;
			this.domain = domain;
		}

		// Token: 0x0600A50F RID: 42255 RVA: 0x0028552C File Offset: 0x0028372C
		protected InvalidForeignForestGroupException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.domain = (string)info.GetValue("domain", typeof(string));
		}

		// Token: 0x0600A510 RID: 42256 RVA: 0x00285581 File Offset: 0x00283781
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("domain", this.domain);
		}

		// Token: 0x1700361A RID: 13850
		// (get) Token: 0x0600A511 RID: 42257 RVA: 0x002855AD File Offset: 0x002837AD
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700361B RID: 13851
		// (get) Token: 0x0600A512 RID: 42258 RVA: 0x002855B5 File Offset: 0x002837B5
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x04005F80 RID: 24448
		private readonly string name;

		// Token: 0x04005F81 RID: 24449
		private readonly string domain;
	}
}
