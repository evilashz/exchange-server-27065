using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02001143 RID: 4419
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidSiteForOrganizationException : LocalizedException
	{
		// Token: 0x0600B532 RID: 46386 RVA: 0x0029DCD4 File Offset: 0x0029BED4
		public InvalidSiteForOrganizationException(string organization, string redirectionUri) : base(Strings.InvalidSiteForOrganizationMessage(organization, redirectionUri))
		{
			this.organization = organization;
			this.redirectionUri = redirectionUri;
		}

		// Token: 0x0600B533 RID: 46387 RVA: 0x0029DCF1 File Offset: 0x0029BEF1
		public InvalidSiteForOrganizationException(string organization, string redirectionUri, Exception innerException) : base(Strings.InvalidSiteForOrganizationMessage(organization, redirectionUri), innerException)
		{
			this.organization = organization;
			this.redirectionUri = redirectionUri;
		}

		// Token: 0x0600B534 RID: 46388 RVA: 0x0029DD10 File Offset: 0x0029BF10
		protected InvalidSiteForOrganizationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.organization = (string)info.GetValue("organization", typeof(string));
			this.redirectionUri = (string)info.GetValue("redirectionUri", typeof(string));
		}

		// Token: 0x0600B535 RID: 46389 RVA: 0x0029DD65 File Offset: 0x0029BF65
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("organization", this.organization);
			info.AddValue("redirectionUri", this.redirectionUri);
		}

		// Token: 0x17003943 RID: 14659
		// (get) Token: 0x0600B536 RID: 46390 RVA: 0x0029DD91 File Offset: 0x0029BF91
		public string Organization
		{
			get
			{
				return this.organization;
			}
		}

		// Token: 0x17003944 RID: 14660
		// (get) Token: 0x0600B537 RID: 46391 RVA: 0x0029DD99 File Offset: 0x0029BF99
		public string RedirectionUri
		{
			get
			{
				return this.redirectionUri;
			}
		}

		// Token: 0x040062A9 RID: 25257
		private readonly string organization;

		// Token: 0x040062AA RID: 25258
		private readonly string redirectionUri;
	}
}
