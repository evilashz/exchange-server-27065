using System;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000238 RID: 568
	internal sealed class ServicePlanOffer
	{
		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x000547BD File Offset: 0x000529BD
		// (set) Token: 0x0600131F RID: 4895 RVA: 0x000547C5 File Offset: 0x000529C5
		public string ProgramId { get; private set; }

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x000547CE File Offset: 0x000529CE
		// (set) Token: 0x06001321 RID: 4897 RVA: 0x000547D6 File Offset: 0x000529D6
		public string OfferId { get; private set; }

		// Token: 0x06001322 RID: 4898 RVA: 0x000547DF File Offset: 0x000529DF
		internal ServicePlanOffer(string programId, string offerId)
		{
			this.ProgramId = programId;
			this.OfferId = offerId;
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x000547F5 File Offset: 0x000529F5
		public override string ToString()
		{
			return (this.ProgramId ?? string.Empty) + "-" + (this.OfferId ?? string.Empty);
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x00054820 File Offset: 0x00052A20
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is ServicePlanOffer)
			{
				ServicePlanOffer servicePlanOffer = obj as ServicePlanOffer;
				return string.Equals(this.ProgramId, servicePlanOffer.ProgramId, StringComparison.OrdinalIgnoreCase) && string.Equals(this.OfferId, servicePlanOffer.OfferId, StringComparison.OrdinalIgnoreCase);
			}
			return false;
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0005486C File Offset: 0x00052A6C
		public override int GetHashCode()
		{
			string text = this.ToString().ToLower();
			return text.GetHashCode();
		}
	}
}
