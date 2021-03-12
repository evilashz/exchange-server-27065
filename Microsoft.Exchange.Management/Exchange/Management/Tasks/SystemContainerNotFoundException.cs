using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E19 RID: 3609
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SystemContainerNotFoundException : LocalizedException
	{
		// Token: 0x0600A57F RID: 42367 RVA: 0x002861E8 File Offset: 0x002843E8
		public SystemContainerNotFoundException(string domain, Guid guid) : base(Strings.SystemContainerNotFoundException(domain, guid))
		{
			this.domain = domain;
			this.guid = guid;
		}

		// Token: 0x0600A580 RID: 42368 RVA: 0x00286205 File Offset: 0x00284405
		public SystemContainerNotFoundException(string domain, Guid guid, Exception innerException) : base(Strings.SystemContainerNotFoundException(domain, guid), innerException)
		{
			this.domain = domain;
			this.guid = guid;
		}

		// Token: 0x0600A581 RID: 42369 RVA: 0x00286224 File Offset: 0x00284424
		protected SystemContainerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (string)info.GetValue("domain", typeof(string));
			this.guid = (Guid)info.GetValue("guid", typeof(Guid));
		}

		// Token: 0x0600A582 RID: 42370 RVA: 0x00286279 File Offset: 0x00284479
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
			info.AddValue("guid", this.guid);
		}

		// Token: 0x17003638 RID: 13880
		// (get) Token: 0x0600A583 RID: 42371 RVA: 0x002862AA File Offset: 0x002844AA
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x17003639 RID: 13881
		// (get) Token: 0x0600A584 RID: 42372 RVA: 0x002862B2 File Offset: 0x002844B2
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x04005F9E RID: 24478
		private readonly string domain;

		// Token: 0x04005F9F RID: 24479
		private readonly Guid guid;
	}
}
