using System;
using System.Runtime.Serialization;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;
using Microsoft.Office.CompliancePolicy.Tasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004A0 RID: 1184
	[DataContract]
	public class UHPolicyDistributionErrorDetails
	{
		// Token: 0x06003ADD RID: 15069 RVA: 0x000B246C File Offset: 0x000B066C
		public UHPolicyDistributionErrorDetails(PolicyDistributionErrorDetails errorDetails)
		{
			this.Endpoint = errorDetails.Endpoint;
			this.ErrorCode = (int)errorDetails.ResultCode;
			this.ErrorMessage = errorDetails.ResultMessage;
			this.ObjectId = errorDetails.ObjectId;
			this.dateTime = errorDetails.LastResultTime;
			this.workload = errorDetails.Workload;
			this.objectType = errorDetails.ObjectType;
			if (errorDetails.ObjectType == ConfigurationObjectType.Scope)
			{
				this.Source = this.Endpoint;
				return;
			}
			this.Source = this.Workload;
		}

		// Token: 0x17002341 RID: 9025
		// (get) Token: 0x06003ADE RID: 15070 RVA: 0x000B24F5 File Offset: 0x000B06F5
		// (set) Token: 0x06003ADF RID: 15071 RVA: 0x000B24FD File Offset: 0x000B06FD
		[DataMember]
		public string Endpoint { get; private set; }

		// Token: 0x17002342 RID: 9026
		// (get) Token: 0x06003AE0 RID: 15072 RVA: 0x000B2506 File Offset: 0x000B0706
		// (set) Token: 0x06003AE1 RID: 15073 RVA: 0x000B250E File Offset: 0x000B070E
		[DataMember]
		public int ErrorCode { get; private set; }

		// Token: 0x17002343 RID: 9027
		// (get) Token: 0x06003AE2 RID: 15074 RVA: 0x000B2517 File Offset: 0x000B0717
		// (set) Token: 0x06003AE3 RID: 15075 RVA: 0x000B251F File Offset: 0x000B071F
		[DataMember]
		public string ErrorMessage { get; private set; }

		// Token: 0x17002344 RID: 9028
		// (get) Token: 0x06003AE4 RID: 15076 RVA: 0x000B2528 File Offset: 0x000B0728
		// (set) Token: 0x06003AE5 RID: 15077 RVA: 0x000B255D File Offset: 0x000B075D
		[DataMember]
		public string LastErrorTimeString
		{
			get
			{
				if (this.dateTime != null)
				{
					return this.dateTime.Value.ToString();
				}
				return null;
			}
			private set
			{
				this.dateTime = new DateTime?(DateTime.Parse(value));
			}
		}

		// Token: 0x17002345 RID: 9029
		// (get) Token: 0x06003AE6 RID: 15078 RVA: 0x000B2570 File Offset: 0x000B0770
		// (set) Token: 0x06003AE7 RID: 15079 RVA: 0x000B2590 File Offset: 0x000B0790
		[DataMember]
		public DateTime LastErrorTimeDateTime
		{
			get
			{
				if (this.dateTime != null)
				{
					return this.dateTime.Value;
				}
				return DateTime.MinValue;
			}
			private set
			{
				this.dateTime = new DateTime?(value);
			}
		}

		// Token: 0x17002346 RID: 9030
		// (get) Token: 0x06003AE8 RID: 15080 RVA: 0x000B259E File Offset: 0x000B079E
		// (set) Token: 0x06003AE9 RID: 15081 RVA: 0x000B25A6 File Offset: 0x000B07A6
		[DataMember]
		public Guid ObjectId { get; private set; }

		// Token: 0x17002347 RID: 9031
		// (get) Token: 0x06003AEA RID: 15082 RVA: 0x000B25B0 File Offset: 0x000B07B0
		// (set) Token: 0x06003AEB RID: 15083 RVA: 0x000B2609 File Offset: 0x000B0809
		[DataMember]
		public string Workload
		{
			get
			{
				switch (this.workload)
				{
				case Microsoft.Office.CompliancePolicy.PolicyConfiguration.Workload.Exchange:
					return Strings.CPPWorkloadExchange;
				case Microsoft.Office.CompliancePolicy.PolicyConfiguration.Workload.SharePoint:
					return Strings.CPPWorkloadSharePoint;
				case Microsoft.Office.CompliancePolicy.PolicyConfiguration.Workload.Intune:
					return Strings.CPPWorkloadIntune;
				}
				return Strings.CPPWorkloadNone;
			}
			private set
			{
				throw new NotImplementedException("The Workload property cannot be set at the moment; the getter returns the value of the private workload field, which is set in the constructor");
			}
		}

		// Token: 0x17002348 RID: 9032
		// (get) Token: 0x06003AEC RID: 15084 RVA: 0x000B2618 File Offset: 0x000B0818
		// (set) Token: 0x06003AED RID: 15085 RVA: 0x000B267A File Offset: 0x000B087A
		[DataMember]
		public string ObjectType
		{
			get
			{
				switch (this.objectType)
				{
				case ConfigurationObjectType.Policy:
					return Strings.CPPConfigurationObjectTypePolicy;
				case ConfigurationObjectType.Rule:
					return Strings.CPPConfigurationObjectTypeRule;
				case ConfigurationObjectType.Association:
					return Strings.CPPConfigurationObjectTypeAssociation;
				case ConfigurationObjectType.Binding:
					return Strings.CPPConfigurationObjectTypeBinding;
				default:
					return Strings.CPPConfigurationObjectTypeScope;
				}
			}
			private set
			{
				throw new NotImplementedException("The ObjectType property cannot be set at the moment; the getter returns the value of the private objectType field, which is set in the constructor");
			}
		}

		// Token: 0x17002349 RID: 9033
		// (get) Token: 0x06003AEE RID: 15086 RVA: 0x000B2686 File Offset: 0x000B0886
		// (set) Token: 0x06003AEF RID: 15087 RVA: 0x000B268E File Offset: 0x000B088E
		[DataMember]
		public string Source { get; private set; }

		// Token: 0x04002737 RID: 10039
		private DateTime? dateTime;

		// Token: 0x04002738 RID: 10040
		private Workload workload;

		// Token: 0x04002739 RID: 10041
		private ConfigurationObjectType objectType;
	}
}
