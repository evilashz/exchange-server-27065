using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E05 RID: 3589
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExForeignForestSGroupNotFoundException : LocalizedException
	{
		// Token: 0x0600A513 RID: 42259 RVA: 0x002855BD File Offset: 0x002837BD
		public ExForeignForestSGroupNotFoundException(string name, string domain) : base(Strings.ExForeignForestSGroupNotFoundException(name, domain))
		{
			this.name = name;
			this.domain = domain;
		}

		// Token: 0x0600A514 RID: 42260 RVA: 0x002855DA File Offset: 0x002837DA
		public ExForeignForestSGroupNotFoundException(string name, string domain, Exception innerException) : base(Strings.ExForeignForestSGroupNotFoundException(name, domain), innerException)
		{
			this.name = name;
			this.domain = domain;
		}

		// Token: 0x0600A515 RID: 42261 RVA: 0x002855F8 File Offset: 0x002837F8
		protected ExForeignForestSGroupNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.domain = (string)info.GetValue("domain", typeof(string));
		}

		// Token: 0x0600A516 RID: 42262 RVA: 0x0028564D File Offset: 0x0028384D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("domain", this.domain);
		}

		// Token: 0x1700361C RID: 13852
		// (get) Token: 0x0600A517 RID: 42263 RVA: 0x00285679 File Offset: 0x00283879
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700361D RID: 13853
		// (get) Token: 0x0600A518 RID: 42264 RVA: 0x00285681 File Offset: 0x00283881
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x04005F82 RID: 24450
		private readonly string name;

		// Token: 0x04005F83 RID: 24451
		private readonly string domain;
	}
}
