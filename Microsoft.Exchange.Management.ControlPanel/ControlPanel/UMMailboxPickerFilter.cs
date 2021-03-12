using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000366 RID: 870
	[DataContract]
	public class UMMailboxPickerFilter : RecipientFilter
	{
		// Token: 0x17001F1E RID: 7966
		// (get) Token: 0x06002FEA RID: 12266 RVA: 0x00091E6D File Offset: 0x0009006D
		public override string RbacScope
		{
			get
			{
				return "@R:Organization";
			}
		}

		// Token: 0x17001F1F RID: 7967
		// (get) Token: 0x06002FEB RID: 12267 RVA: 0x00091E74 File Offset: 0x00090074
		// (set) Token: 0x06002FEC RID: 12268 RVA: 0x00091E7C File Offset: 0x0009007C
		[DataMember]
		public Identity DialPlanIdentity { get; set; }

		// Token: 0x17001F20 RID: 7968
		// (get) Token: 0x06002FED RID: 12269 RVA: 0x00091E85 File Offset: 0x00090085
		protected override RecipientTypeDetails[] RecipientTypeDetailsParam
		{
			get
			{
				return UMMailboxPickerFilter.allowedRecipientTypeDetails;
			}
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x00091E8C File Offset: 0x0009008C
		protected override void UpdateFilterProperty()
		{
			base.UpdateFilterProperty();
			string text = (string)base["Filter"];
			if (!string.IsNullOrEmpty(text))
			{
				text = string.Format("({0}) -and ({1})", text, this.GetUMFilter());
			}
			else
			{
				text = this.GetUMFilter();
			}
			base["Filter"] = text;
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x00091EE0 File Offset: 0x000900E0
		private string GetUMFilter()
		{
			string result = "(UMEnabled -eq 'true')";
			if (this.DialPlanIdentity != null)
			{
				ADObjectId adobjectId;
				if (!ADObjectId.TryParseDnOrGuid(this.DialPlanIdentity.RawIdentity, out adobjectId))
				{
					throw new FaultException(new ArgumentException(this.DialPlanIdentity.RawIdentity, "DialPlanIdentity").Message);
				}
				adobjectId = ADSystemConfigurationObjectIDResolver.Instance.ResolveObject(adobjectId);
				if (adobjectId != null)
				{
					result = string.Format("(UMEnabled -eq 'true') -and (UMRecipientDialPlanId -eq '{0}')", adobjectId.DistinguishedName);
				}
			}
			return result;
		}

		// Token: 0x04002326 RID: 8998
		private const string UMEnabledFilter = "(UMEnabled -eq 'true')";

		// Token: 0x04002327 RID: 8999
		private const string UMRecipientDialPlanIdFilterFormat = "(UMEnabled -eq 'true') -and (UMRecipientDialPlanId -eq '{0}')";

		// Token: 0x04002328 RID: 9000
		private static readonly RecipientTypeDetails[] allowedRecipientTypeDetails = UMMailbox.GetUMRecipientTypeDetails();
	}
}
