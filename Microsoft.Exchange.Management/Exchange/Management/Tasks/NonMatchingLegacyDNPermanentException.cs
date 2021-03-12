using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EB6 RID: 3766
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NonMatchingLegacyDNPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A866 RID: 43110 RVA: 0x00289F49 File Offset: 0x00288149
		public NonMatchingLegacyDNPermanentException(string sourceMailboxLegDN, string targetMailbox, string parameterName) : base(Strings.ErrorNonMatchingLegacyDNs(sourceMailboxLegDN, targetMailbox, parameterName))
		{
			this.sourceMailboxLegDN = sourceMailboxLegDN;
			this.targetMailbox = targetMailbox;
			this.parameterName = parameterName;
		}

		// Token: 0x0600A867 RID: 43111 RVA: 0x00289F6E File Offset: 0x0028816E
		public NonMatchingLegacyDNPermanentException(string sourceMailboxLegDN, string targetMailbox, string parameterName, Exception innerException) : base(Strings.ErrorNonMatchingLegacyDNs(sourceMailboxLegDN, targetMailbox, parameterName), innerException)
		{
			this.sourceMailboxLegDN = sourceMailboxLegDN;
			this.targetMailbox = targetMailbox;
			this.parameterName = parameterName;
		}

		// Token: 0x0600A868 RID: 43112 RVA: 0x00289F98 File Offset: 0x00288198
		protected NonMatchingLegacyDNPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.sourceMailboxLegDN = (string)info.GetValue("sourceMailboxLegDN", typeof(string));
			this.targetMailbox = (string)info.GetValue("targetMailbox", typeof(string));
			this.parameterName = (string)info.GetValue("parameterName", typeof(string));
		}

		// Token: 0x0600A869 RID: 43113 RVA: 0x0028A00D File Offset: 0x0028820D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("sourceMailboxLegDN", this.sourceMailboxLegDN);
			info.AddValue("targetMailbox", this.targetMailbox);
			info.AddValue("parameterName", this.parameterName);
		}

		// Token: 0x170036AB RID: 13995
		// (get) Token: 0x0600A86A RID: 43114 RVA: 0x0028A04A File Offset: 0x0028824A
		public string SourceMailboxLegDN
		{
			get
			{
				return this.sourceMailboxLegDN;
			}
		}

		// Token: 0x170036AC RID: 13996
		// (get) Token: 0x0600A86B RID: 43115 RVA: 0x0028A052 File Offset: 0x00288252
		public string TargetMailbox
		{
			get
			{
				return this.targetMailbox;
			}
		}

		// Token: 0x170036AD RID: 13997
		// (get) Token: 0x0600A86C RID: 43116 RVA: 0x0028A05A File Offset: 0x0028825A
		public string ParameterName
		{
			get
			{
				return this.parameterName;
			}
		}

		// Token: 0x04006011 RID: 24593
		private readonly string sourceMailboxLegDN;

		// Token: 0x04006012 RID: 24594
		private readonly string targetMailbox;

		// Token: 0x04006013 RID: 24595
		private readonly string parameterName;
	}
}
