using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000065 RID: 101
	public class UserExperienceVerificationRequest
	{
		// Token: 0x0600035A RID: 858 RVA: 0x00016E6B File Offset: 0x0001506B
		public UserExperienceVerificationRequest(string tenantName, string stage, IList<string> components)
		{
			this.tenantName = tenantName;
			this.stage = stage;
			this.components = components;
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00016E88 File Offset: 0x00015088
		public string TenantName
		{
			get
			{
				return this.tenantName;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00016E90 File Offset: 0x00015090
		public IList<string> Components
		{
			get
			{
				return this.components;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00016E98 File Offset: 0x00015098
		public string Stage
		{
			get
			{
				return this.stage;
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00016EA0 File Offset: 0x000150A0
		public override bool Equals(object obj)
		{
			UserExperienceVerificationRequest userExperienceVerificationRequest = obj as UserExperienceVerificationRequest;
			return userExperienceVerificationRequest != null && this.TenantName == userExperienceVerificationRequest.TenantName && this.Stage == userExperienceVerificationRequest.Stage;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00016EDF File Offset: 0x000150DF
		public override string ToString()
		{
			return this.TenantName + "_" + this.Stage;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00016EF7 File Offset: 0x000150F7
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0400027A RID: 634
		private readonly string tenantName;

		// Token: 0x0400027B RID: 635
		private readonly string stage;

		// Token: 0x0400027C RID: 636
		private readonly IList<string> components;
	}
}
