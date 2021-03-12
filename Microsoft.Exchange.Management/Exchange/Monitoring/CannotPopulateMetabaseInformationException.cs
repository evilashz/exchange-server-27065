using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F25 RID: 3877
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotPopulateMetabaseInformationException : LocalizedException
	{
		// Token: 0x0600AAAD RID: 43693 RVA: 0x0028DD56 File Offset: 0x0028BF56
		public CannotPopulateMetabaseInformationException(string vDir, string errorMessage) : base(Strings.CasHealthFailedToPopulateFromMetabase(vDir, errorMessage))
		{
			this.vDir = vDir;
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600AAAE RID: 43694 RVA: 0x0028DD73 File Offset: 0x0028BF73
		public CannotPopulateMetabaseInformationException(string vDir, string errorMessage, Exception innerException) : base(Strings.CasHealthFailedToPopulateFromMetabase(vDir, errorMessage), innerException)
		{
			this.vDir = vDir;
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600AAAF RID: 43695 RVA: 0x0028DD94 File Offset: 0x0028BF94
		protected CannotPopulateMetabaseInformationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.vDir = (string)info.GetValue("vDir", typeof(string));
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x0600AAB0 RID: 43696 RVA: 0x0028DDE9 File Offset: 0x0028BFE9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("vDir", this.vDir);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17003736 RID: 14134
		// (get) Token: 0x0600AAB1 RID: 43697 RVA: 0x0028DE15 File Offset: 0x0028C015
		public string VDir
		{
			get
			{
				return this.vDir;
			}
		}

		// Token: 0x17003737 RID: 14135
		// (get) Token: 0x0600AAB2 RID: 43698 RVA: 0x0028DE1D File Offset: 0x0028C01D
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x0400609C RID: 24732
		private readonly string vDir;

		// Token: 0x0400609D RID: 24733
		private readonly string errorMessage;
	}
}
