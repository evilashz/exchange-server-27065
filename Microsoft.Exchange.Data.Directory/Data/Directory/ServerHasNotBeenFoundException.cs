using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A8E RID: 2702
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServerHasNotBeenFoundException : ADOperationException
	{
		// Token: 0x06007F96 RID: 32662 RVA: 0x001A4300 File Offset: 0x001A2500
		public ServerHasNotBeenFoundException(int versionNumber, string identifier, bool needsExactVersionMatch, string siteName) : base(DirectoryStrings.ServerHasNotBeenFound(versionNumber, identifier, needsExactVersionMatch, siteName))
		{
			this.versionNumber = versionNumber;
			this.identifier = identifier;
			this.needsExactVersionMatch = needsExactVersionMatch;
			this.siteName = siteName;
		}

		// Token: 0x06007F97 RID: 32663 RVA: 0x001A432F File Offset: 0x001A252F
		public ServerHasNotBeenFoundException(int versionNumber, string identifier, bool needsExactVersionMatch, string siteName, Exception innerException) : base(DirectoryStrings.ServerHasNotBeenFound(versionNumber, identifier, needsExactVersionMatch, siteName), innerException)
		{
			this.versionNumber = versionNumber;
			this.identifier = identifier;
			this.needsExactVersionMatch = needsExactVersionMatch;
			this.siteName = siteName;
		}

		// Token: 0x06007F98 RID: 32664 RVA: 0x001A4360 File Offset: 0x001A2560
		protected ServerHasNotBeenFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.versionNumber = (int)info.GetValue("versionNumber", typeof(int));
			this.identifier = (string)info.GetValue("identifier", typeof(string));
			this.needsExactVersionMatch = (bool)info.GetValue("needsExactVersionMatch", typeof(bool));
			this.siteName = (string)info.GetValue("siteName", typeof(string));
		}

		// Token: 0x06007F99 RID: 32665 RVA: 0x001A43F8 File Offset: 0x001A25F8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("versionNumber", this.versionNumber);
			info.AddValue("identifier", this.identifier);
			info.AddValue("needsExactVersionMatch", this.needsExactVersionMatch);
			info.AddValue("siteName", this.siteName);
		}

		// Token: 0x17002EB5 RID: 11957
		// (get) Token: 0x06007F9A RID: 32666 RVA: 0x001A4451 File Offset: 0x001A2651
		public int VersionNumber
		{
			get
			{
				return this.versionNumber;
			}
		}

		// Token: 0x17002EB6 RID: 11958
		// (get) Token: 0x06007F9B RID: 32667 RVA: 0x001A4459 File Offset: 0x001A2659
		public string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17002EB7 RID: 11959
		// (get) Token: 0x06007F9C RID: 32668 RVA: 0x001A4461 File Offset: 0x001A2661
		public bool NeedsExactVersionMatch
		{
			get
			{
				return this.needsExactVersionMatch;
			}
		}

		// Token: 0x17002EB8 RID: 11960
		// (get) Token: 0x06007F9D RID: 32669 RVA: 0x001A4469 File Offset: 0x001A2669
		public string SiteName
		{
			get
			{
				return this.siteName;
			}
		}

		// Token: 0x0400558F RID: 21903
		private readonly int versionNumber;

		// Token: 0x04005590 RID: 21904
		private readonly string identifier;

		// Token: 0x04005591 RID: 21905
		private readonly bool needsExactVersionMatch;

		// Token: 0x04005592 RID: 21906
		private readonly string siteName;
	}
}
