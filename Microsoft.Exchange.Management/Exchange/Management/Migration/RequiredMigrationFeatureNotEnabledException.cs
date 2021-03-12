using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x0200111F RID: 4383
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RequiredMigrationFeatureNotEnabledException : LocalizedException
	{
		// Token: 0x0600B489 RID: 46217 RVA: 0x0029CEDF File Offset: 0x0029B0DF
		public RequiredMigrationFeatureNotEnabledException(MigrationFeature feature) : base(Strings.ErrorRequiredMigrationFeatureNotEnabled(feature))
		{
			this.feature = feature;
		}

		// Token: 0x0600B48A RID: 46218 RVA: 0x0029CEF4 File Offset: 0x0029B0F4
		public RequiredMigrationFeatureNotEnabledException(MigrationFeature feature, Exception innerException) : base(Strings.ErrorRequiredMigrationFeatureNotEnabled(feature), innerException)
		{
			this.feature = feature;
		}

		// Token: 0x0600B48B RID: 46219 RVA: 0x0029CF0A File Offset: 0x0029B10A
		protected RequiredMigrationFeatureNotEnabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.feature = (MigrationFeature)info.GetValue("feature", typeof(MigrationFeature));
		}

		// Token: 0x0600B48C RID: 46220 RVA: 0x0029CF34 File Offset: 0x0029B134
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("feature", this.feature);
		}

		// Token: 0x1700392A RID: 14634
		// (get) Token: 0x0600B48D RID: 46221 RVA: 0x0029CF54 File Offset: 0x0029B154
		public MigrationFeature Feature
		{
			get
			{
				return this.feature;
			}
		}

		// Token: 0x04006290 RID: 25232
		private readonly MigrationFeature feature;
	}
}
