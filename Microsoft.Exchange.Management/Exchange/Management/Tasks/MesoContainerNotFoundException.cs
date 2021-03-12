using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000DFF RID: 3583
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MesoContainerNotFoundException : LocalizedException
	{
		// Token: 0x0600A4F3 RID: 42227 RVA: 0x00285238 File Offset: 0x00283438
		public MesoContainerNotFoundException(string domain) : base(Strings.MesoContainerNotFoundException(domain))
		{
			this.domain = domain;
		}

		// Token: 0x0600A4F4 RID: 42228 RVA: 0x0028524D File Offset: 0x0028344D
		public MesoContainerNotFoundException(string domain, Exception innerException) : base(Strings.MesoContainerNotFoundException(domain), innerException)
		{
			this.domain = domain;
		}

		// Token: 0x0600A4F5 RID: 42229 RVA: 0x00285263 File Offset: 0x00283463
		protected MesoContainerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (string)info.GetValue("domain", typeof(string));
		}

		// Token: 0x0600A4F6 RID: 42230 RVA: 0x0028528D File Offset: 0x0028348D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
		}

		// Token: 0x17003614 RID: 13844
		// (get) Token: 0x0600A4F7 RID: 42231 RVA: 0x002852A8 File Offset: 0x002834A8
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x04005F7A RID: 24442
		private readonly string domain;
	}
}
