using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000138 RID: 312
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ArchiveNotAvailable : LocalizedException
	{
		// Token: 0x06000CEE RID: 3310 RVA: 0x00051A08 File Offset: 0x0004FC08
		public ArchiveNotAvailable(string nameOfUser) : base(Strings.descArchiveNotAvailable(nameOfUser))
		{
			this.nameOfUser = nameOfUser;
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00051A1D File Offset: 0x0004FC1D
		public ArchiveNotAvailable(string nameOfUser, Exception innerException) : base(Strings.descArchiveNotAvailable(nameOfUser), innerException)
		{
			this.nameOfUser = nameOfUser;
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00051A33 File Offset: 0x0004FC33
		protected ArchiveNotAvailable(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.nameOfUser = (string)info.GetValue("nameOfUser", typeof(string));
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00051A5D File Offset: 0x0004FC5D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("nameOfUser", this.nameOfUser);
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x00051A78 File Offset: 0x0004FC78
		public string NameOfUser
		{
			get
			{
				return this.nameOfUser;
			}
		}

		// Token: 0x04000834 RID: 2100
		private readonly string nameOfUser;
	}
}
