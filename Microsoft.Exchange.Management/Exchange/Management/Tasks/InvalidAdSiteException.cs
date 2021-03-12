using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001043 RID: 4163
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidAdSiteException : LocalizedException
	{
		// Token: 0x0600B00B RID: 45067 RVA: 0x00295535 File Offset: 0x00293735
		public InvalidAdSiteException(string adSite) : base(Strings.InvalidAdSite(adSite))
		{
			this.adSite = adSite;
		}

		// Token: 0x0600B00C RID: 45068 RVA: 0x0029554A File Offset: 0x0029374A
		public InvalidAdSiteException(string adSite, Exception innerException) : base(Strings.InvalidAdSite(adSite), innerException)
		{
			this.adSite = adSite;
		}

		// Token: 0x0600B00D RID: 45069 RVA: 0x00295560 File Offset: 0x00293760
		protected InvalidAdSiteException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.adSite = (string)info.GetValue("adSite", typeof(string));
		}

		// Token: 0x0600B00E RID: 45070 RVA: 0x0029558A File Offset: 0x0029378A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("adSite", this.adSite);
		}

		// Token: 0x1700381C RID: 14364
		// (get) Token: 0x0600B00F RID: 45071 RVA: 0x002955A5 File Offset: 0x002937A5
		public string AdSite
		{
			get
			{
				return this.adSite;
			}
		}

		// Token: 0x04006182 RID: 24962
		private readonly string adSite;
	}
}
