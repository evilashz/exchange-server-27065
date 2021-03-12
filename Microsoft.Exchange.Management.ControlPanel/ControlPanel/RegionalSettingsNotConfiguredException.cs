using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200000F RID: 15
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class RegionalSettingsNotConfiguredException : LocalizedException
	{
		// Token: 0x0600185C RID: 6236 RVA: 0x0004B68A File Offset: 0x0004988A
		public RegionalSettingsNotConfiguredException(ADObjectId user) : base(Strings.RegionalSettingsNotConfigured(user))
		{
			this.user = user;
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x0004B69F File Offset: 0x0004989F
		public RegionalSettingsNotConfiguredException(ADObjectId user, Exception innerException) : base(Strings.RegionalSettingsNotConfigured(user), innerException)
		{
			this.user = user;
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x0004B6B5 File Offset: 0x000498B5
		protected RegionalSettingsNotConfiguredException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.user = (ADObjectId)info.GetValue("user", typeof(ADObjectId));
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x0004B6DF File Offset: 0x000498DF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("user", this.user);
		}

		// Token: 0x170017B9 RID: 6073
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x0004B6FA File Offset: 0x000498FA
		public ADObjectId User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x04001852 RID: 6226
		private readonly ADObjectId user;
	}
}
