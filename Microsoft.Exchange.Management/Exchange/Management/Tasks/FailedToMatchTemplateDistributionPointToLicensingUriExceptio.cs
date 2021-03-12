using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010E5 RID: 4325
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToMatchTemplateDistributionPointToLicensingUriException : LocalizedException
	{
		// Token: 0x0600B366 RID: 45926 RVA: 0x0029B27B File Offset: 0x0029947B
		public FailedToMatchTemplateDistributionPointToLicensingUriException(Guid templateGuid, Uri templateDp, Uri tpdDp) : base(Strings.FailedToMatchTemplateDistributionPointToLicensingUri(templateGuid, templateDp, tpdDp))
		{
			this.templateGuid = templateGuid;
			this.templateDp = templateDp;
			this.tpdDp = tpdDp;
		}

		// Token: 0x0600B367 RID: 45927 RVA: 0x0029B2A0 File Offset: 0x002994A0
		public FailedToMatchTemplateDistributionPointToLicensingUriException(Guid templateGuid, Uri templateDp, Uri tpdDp, Exception innerException) : base(Strings.FailedToMatchTemplateDistributionPointToLicensingUri(templateGuid, templateDp, tpdDp), innerException)
		{
			this.templateGuid = templateGuid;
			this.templateDp = templateDp;
			this.tpdDp = tpdDp;
		}

		// Token: 0x0600B368 RID: 45928 RVA: 0x0029B2C8 File Offset: 0x002994C8
		protected FailedToMatchTemplateDistributionPointToLicensingUriException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.templateGuid = (Guid)info.GetValue("templateGuid", typeof(Guid));
			this.templateDp = (Uri)info.GetValue("templateDp", typeof(Uri));
			this.tpdDp = (Uri)info.GetValue("tpdDp", typeof(Uri));
		}

		// Token: 0x0600B369 RID: 45929 RVA: 0x0029B340 File Offset: 0x00299540
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("templateGuid", this.templateGuid);
			info.AddValue("templateDp", this.templateDp);
			info.AddValue("tpdDp", this.tpdDp);
		}

		// Token: 0x170038EF RID: 14575
		// (get) Token: 0x0600B36A RID: 45930 RVA: 0x0029B38D File Offset: 0x0029958D
		public Guid TemplateGuid
		{
			get
			{
				return this.templateGuid;
			}
		}

		// Token: 0x170038F0 RID: 14576
		// (get) Token: 0x0600B36B RID: 45931 RVA: 0x0029B395 File Offset: 0x00299595
		public Uri TemplateDp
		{
			get
			{
				return this.templateDp;
			}
		}

		// Token: 0x170038F1 RID: 14577
		// (get) Token: 0x0600B36C RID: 45932 RVA: 0x0029B39D File Offset: 0x0029959D
		public Uri TpdDp
		{
			get
			{
				return this.tpdDp;
			}
		}

		// Token: 0x04006255 RID: 25173
		private readonly Guid templateGuid;

		// Token: 0x04006256 RID: 25174
		private readonly Uri templateDp;

		// Token: 0x04006257 RID: 25175
		private readonly Uri tpdDp;
	}
}
