using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Supervision;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200048E RID: 1166
	[DataContract]
	public class SetClosedCampusOutboundPolicyConfiguration : SetObjectProperties
	{
		// Token: 0x17002307 RID: 8967
		// (get) Token: 0x06003A42 RID: 14914 RVA: 0x000B0652 File Offset: 0x000AE852
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-SupervisionPolicy";
			}
		}

		// Token: 0x17002308 RID: 8968
		// (get) Token: 0x06003A43 RID: 14915 RVA: 0x000B0659 File Offset: 0x000AE859
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}

		// Token: 0x17002309 RID: 8969
		// (get) Token: 0x06003A44 RID: 14916 RVA: 0x000B0660 File Offset: 0x000AE860
		// (set) Token: 0x06003A45 RID: 14917 RVA: 0x000B067C File Offset: 0x000AE87C
		[DataMember]
		public bool ClosedCampusOutboundPolicyEnabled
		{
			get
			{
				return (bool)(base[SupervisionPolicySchema.ClosedCampusOutboundPolicyEnabled] ?? false);
			}
			set
			{
				base[SupervisionPolicySchema.ClosedCampusOutboundPolicyEnabled] = value;
			}
		}

		// Token: 0x1700230A RID: 8970
		// (get) Token: 0x06003A46 RID: 14918 RVA: 0x000B068F File Offset: 0x000AE88F
		// (set) Token: 0x06003A47 RID: 14919 RVA: 0x000B06AB File Offset: 0x000AE8AB
		[DataMember]
		public Identity[] ClosedCampusOutboundGroupExceptions
		{
			get
			{
				return Identity.FromIdParameters(((RecipientIdParameter[])base[SupervisionPolicySchema.ClosedCampusOutboundGroupExceptions]).ToStringArray());
			}
			set
			{
				base[SupervisionPolicySchema.ClosedCampusOutboundGroupExceptions] = ((IEnumerable<Identity>)value).ToIdParameters();
			}
		}

		// Token: 0x1700230B RID: 8971
		// (get) Token: 0x06003A48 RID: 14920 RVA: 0x000B06C3 File Offset: 0x000AE8C3
		// (set) Token: 0x06003A49 RID: 14921 RVA: 0x000B06DC File Offset: 0x000AE8DC
		[DataMember]
		public string[] ClosedCampusOutboundDomainExceptions
		{
			get
			{
				return ((SmtpDomain[])base[SupervisionPolicySchema.ClosedCampusOutboundDomainExceptions]).ToStringArray();
			}
			set
			{
				SmtpDomain[] array = new SmtpDomain[value.Length];
				for (int i = 0; i < value.Length; i++)
				{
					array[i] = SmtpDomain.Parse(value[i]);
				}
				base[SupervisionPolicySchema.ClosedCampusOutboundDomainExceptions] = array;
			}
		}
	}
}
