using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EDA RID: 3802
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotMovePrimaryAndArchiveToOpposingMrsVersions : MailboxReplicationPermanentException
	{
		// Token: 0x0600A91E RID: 43294 RVA: 0x0028B1C2 File Offset: 0x002893C2
		public CannotMovePrimaryAndArchiveToOpposingMrsVersions(string srcPrimary, string targetPrimary, string srcArchive, string targetArchive) : base(Strings.ErrorCannotMovePrimaryAndArchiveInDifferentDirections(srcPrimary, targetPrimary, srcArchive, targetArchive))
		{
			this.srcPrimary = srcPrimary;
			this.targetPrimary = targetPrimary;
			this.srcArchive = srcArchive;
			this.targetArchive = targetArchive;
		}

		// Token: 0x0600A91F RID: 43295 RVA: 0x0028B1F1 File Offset: 0x002893F1
		public CannotMovePrimaryAndArchiveToOpposingMrsVersions(string srcPrimary, string targetPrimary, string srcArchive, string targetArchive, Exception innerException) : base(Strings.ErrorCannotMovePrimaryAndArchiveInDifferentDirections(srcPrimary, targetPrimary, srcArchive, targetArchive), innerException)
		{
			this.srcPrimary = srcPrimary;
			this.targetPrimary = targetPrimary;
			this.srcArchive = srcArchive;
			this.targetArchive = targetArchive;
		}

		// Token: 0x0600A920 RID: 43296 RVA: 0x0028B224 File Offset: 0x00289424
		protected CannotMovePrimaryAndArchiveToOpposingMrsVersions(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.srcPrimary = (string)info.GetValue("srcPrimary", typeof(string));
			this.targetPrimary = (string)info.GetValue("targetPrimary", typeof(string));
			this.srcArchive = (string)info.GetValue("srcArchive", typeof(string));
			this.targetArchive = (string)info.GetValue("targetArchive", typeof(string));
		}

		// Token: 0x0600A921 RID: 43297 RVA: 0x0028B2BC File Offset: 0x002894BC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("srcPrimary", this.srcPrimary);
			info.AddValue("targetPrimary", this.targetPrimary);
			info.AddValue("srcArchive", this.srcArchive);
			info.AddValue("targetArchive", this.targetArchive);
		}

		// Token: 0x170036D3 RID: 14035
		// (get) Token: 0x0600A922 RID: 43298 RVA: 0x0028B315 File Offset: 0x00289515
		public string SrcPrimary
		{
			get
			{
				return this.srcPrimary;
			}
		}

		// Token: 0x170036D4 RID: 14036
		// (get) Token: 0x0600A923 RID: 43299 RVA: 0x0028B31D File Offset: 0x0028951D
		public string TargetPrimary
		{
			get
			{
				return this.targetPrimary;
			}
		}

		// Token: 0x170036D5 RID: 14037
		// (get) Token: 0x0600A924 RID: 43300 RVA: 0x0028B325 File Offset: 0x00289525
		public string SrcArchive
		{
			get
			{
				return this.srcArchive;
			}
		}

		// Token: 0x170036D6 RID: 14038
		// (get) Token: 0x0600A925 RID: 43301 RVA: 0x0028B32D File Offset: 0x0028952D
		public string TargetArchive
		{
			get
			{
				return this.targetArchive;
			}
		}

		// Token: 0x04006039 RID: 24633
		private readonly string srcPrimary;

		// Token: 0x0400603A RID: 24634
		private readonly string targetPrimary;

		// Token: 0x0400603B RID: 24635
		private readonly string srcArchive;

		// Token: 0x0400603C RID: 24636
		private readonly string targetArchive;
	}
}
