using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EBE RID: 3774
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NonUNCFilePathPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A88F RID: 43151 RVA: 0x0028A36F File Offset: 0x0028856F
		public NonUNCFilePathPermanentException(string pathName) : base(Strings.ErrorFilePathMustBeUNC(pathName))
		{
			this.pathName = pathName;
		}

		// Token: 0x0600A890 RID: 43152 RVA: 0x0028A384 File Offset: 0x00288584
		public NonUNCFilePathPermanentException(string pathName, Exception innerException) : base(Strings.ErrorFilePathMustBeUNC(pathName), innerException)
		{
			this.pathName = pathName;
		}

		// Token: 0x0600A891 RID: 43153 RVA: 0x0028A39A File Offset: 0x0028859A
		protected NonUNCFilePathPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.pathName = (string)info.GetValue("pathName", typeof(string));
		}

		// Token: 0x0600A892 RID: 43154 RVA: 0x0028A3C4 File Offset: 0x002885C4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("pathName", this.pathName);
		}

		// Token: 0x170036B4 RID: 14004
		// (get) Token: 0x0600A893 RID: 43155 RVA: 0x0028A3DF File Offset: 0x002885DF
		public string PathName
		{
			get
			{
				return this.pathName;
			}
		}

		// Token: 0x0400601A RID: 24602
		private readonly string pathName;
	}
}
