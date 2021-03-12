using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EB8 RID: 3768
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MissingArchiveParameterForRestorePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A872 RID: 43122 RVA: 0x0028A0DA File Offset: 0x002882DA
		public MissingArchiveParameterForRestorePermanentException(string identity) : base(Strings.ErrorCloudArchiveNeedsTargetIsArchiveSwitchForRestore(identity))
		{
			this.identity = identity;
		}

		// Token: 0x0600A873 RID: 43123 RVA: 0x0028A0EF File Offset: 0x002882EF
		public MissingArchiveParameterForRestorePermanentException(string identity, Exception innerException) : base(Strings.ErrorCloudArchiveNeedsTargetIsArchiveSwitchForRestore(identity), innerException)
		{
			this.identity = identity;
		}

		// Token: 0x0600A874 RID: 43124 RVA: 0x0028A105 File Offset: 0x00288305
		protected MissingArchiveParameterForRestorePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
		}

		// Token: 0x0600A875 RID: 43125 RVA: 0x0028A12F File Offset: 0x0028832F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x170036AF RID: 13999
		// (get) Token: 0x0600A876 RID: 43126 RVA: 0x0028A14A File Offset: 0x0028834A
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x04006015 RID: 24597
		private readonly string identity;
	}
}
