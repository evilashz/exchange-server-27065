using System;

namespace Microsoft.Exchange.HostedServices.Archive.MetaReplication
{
	// Token: 0x0200004A RID: 74
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class MimeContentAttribute : Attribute
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x0000B7D8 File Offset: 0x000099D8
		public MimeContentAttribute()
		{
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000B7E0 File Offset: 0x000099E0
		public MimeContentAttribute(string contentDescription)
		{
			if (string.IsNullOrEmpty(contentDescription))
			{
				throw new ArgumentException("may not be null or empty", "contentDescription");
			}
			this.contentDescription = contentDescription;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000B807 File Offset: 0x00009A07
		public string ContentDescription
		{
			get
			{
				return this.contentDescription;
			}
		}

		// Token: 0x04000152 RID: 338
		private readonly string contentDescription;
	}
}
