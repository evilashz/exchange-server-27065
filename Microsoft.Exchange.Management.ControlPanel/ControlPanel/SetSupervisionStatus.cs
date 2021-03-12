using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Supervision;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200048D RID: 1165
	[DataContract]
	public class SetSupervisionStatus : SetObjectProperties
	{
		// Token: 0x06003A30 RID: 14896 RVA: 0x000B048C File Offset: 0x000AE68C
		public SetSupervisionStatus()
		{
			this.OnDeserializing(default(StreamingContext));
		}

		// Token: 0x06003A31 RID: 14897 RVA: 0x000B04AE File Offset: 0x000AE6AE
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.MyClosedCampusOutboundPolicyConfiguration = new SetClosedCampusOutboundPolicyConfiguration();
		}

		// Token: 0x170022FE RID: 8958
		// (get) Token: 0x06003A32 RID: 14898 RVA: 0x000B04BB File Offset: 0x000AE6BB
		// (set) Token: 0x06003A33 RID: 14899 RVA: 0x000B04C3 File Offset: 0x000AE6C3
		public SetClosedCampusOutboundPolicyConfiguration MyClosedCampusOutboundPolicyConfiguration { get; private set; }

		// Token: 0x170022FF RID: 8959
		// (get) Token: 0x06003A34 RID: 14900 RVA: 0x000B04CC File Offset: 0x000AE6CC
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-SupervisionPolicy";
			}
		}

		// Token: 0x17002300 RID: 8960
		// (get) Token: 0x06003A35 RID: 14901 RVA: 0x000B04D3 File Offset: 0x000AE6D3
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}

		// Token: 0x17002301 RID: 8961
		// (get) Token: 0x06003A36 RID: 14902 RVA: 0x000B04DA File Offset: 0x000AE6DA
		// (set) Token: 0x06003A37 RID: 14903 RVA: 0x000B04F6 File Offset: 0x000AE6F6
		[DataMember]
		public bool ClosedCampusPolicyEnabled
		{
			get
			{
				return (bool)(base[SupervisionPolicySchema.ClosedCampusInboundPolicyEnabled] ?? false);
			}
			set
			{
				base[SupervisionPolicySchema.ClosedCampusInboundPolicyEnabled] = value;
				this.MyClosedCampusOutboundPolicyConfiguration.ClosedCampusOutboundPolicyEnabled = value;
			}
		}

		// Token: 0x17002302 RID: 8962
		// (get) Token: 0x06003A38 RID: 14904 RVA: 0x000B0515 File Offset: 0x000AE715
		// (set) Token: 0x06003A39 RID: 14905 RVA: 0x000B0531 File Offset: 0x000AE731
		[DataMember]
		public Identity[] ClosedCampusInboundGroupExceptions
		{
			get
			{
				return Identity.FromIdParameters(((RecipientIdParameter[])base[SupervisionPolicySchema.ClosedCampusInboundGroupExceptions]).ToStringArray());
			}
			set
			{
				base[SupervisionPolicySchema.ClosedCampusInboundGroupExceptions] = ((IEnumerable<Identity>)value).ToIdParameters();
				this.MyClosedCampusOutboundPolicyConfiguration.ClosedCampusOutboundGroupExceptions = value;
			}
		}

		// Token: 0x17002303 RID: 8963
		// (get) Token: 0x06003A3A RID: 14906 RVA: 0x000B0555 File Offset: 0x000AE755
		// (set) Token: 0x06003A3B RID: 14907 RVA: 0x000B056C File Offset: 0x000AE76C
		[DataMember]
		public string[] ClosedCampusInboundDomainExceptions
		{
			get
			{
				return ((SmtpDomain[])base[SupervisionPolicySchema.ClosedCampusInboundDomainExceptions]).ToStringArray();
			}
			set
			{
				SmtpDomain[] array = new SmtpDomain[value.Length];
				for (int i = 0; i < value.Length; i++)
				{
					try
					{
						array[i] = SmtpDomain.Parse(value[i]);
					}
					catch (FormatException ex)
					{
						throw new FaultException(ex.Message);
					}
				}
				base[SupervisionPolicySchema.ClosedCampusInboundDomainExceptions] = array;
				this.MyClosedCampusOutboundPolicyConfiguration.ClosedCampusOutboundDomainExceptions = value;
			}
		}

		// Token: 0x17002304 RID: 8964
		// (get) Token: 0x06003A3C RID: 14908 RVA: 0x000B05D4 File Offset: 0x000AE7D4
		// (set) Token: 0x06003A3D RID: 14909 RVA: 0x000B05E6 File Offset: 0x000AE7E6
		[DataMember]
		public string BadWordsList
		{
			get
			{
				return (string)base[SupervisionPolicySchema.BadWordsList];
			}
			set
			{
				base[SupervisionPolicySchema.BadWordsList] = value;
			}
		}

		// Token: 0x17002305 RID: 8965
		// (get) Token: 0x06003A3E RID: 14910 RVA: 0x000B05F4 File Offset: 0x000AE7F4
		// (set) Token: 0x06003A3F RID: 14911 RVA: 0x000B0610 File Offset: 0x000AE810
		[DataMember]
		public bool BadWordsPolicyEnabled
		{
			get
			{
				return (bool)(base[SupervisionPolicySchema.BadWordsPolicyEnabled] ?? false);
			}
			set
			{
				base[SupervisionPolicySchema.BadWordsPolicyEnabled] = value;
			}
		}

		// Token: 0x17002306 RID: 8966
		// (get) Token: 0x06003A40 RID: 14912 RVA: 0x000B0623 File Offset: 0x000AE823
		// (set) Token: 0x06003A41 RID: 14913 RVA: 0x000B063F File Offset: 0x000AE83F
		[DataMember]
		public bool AntiBullyingPolicyEnabled
		{
			get
			{
				return (bool)(base[SupervisionPolicySchema.AntiBullyingPolicyEnabled] ?? false);
			}
			set
			{
				base[SupervisionPolicySchema.AntiBullyingPolicyEnabled] = value;
			}
		}
	}
}
