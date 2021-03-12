using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services
{
	// Token: 0x02000859 RID: 2137
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[DebuggerStepThrough]
	[KnownType(typeof(CompanyResponseInfo))]
	[DataContract(Name = "ResponseInfo", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[KnownType(typeof(DomainResponseInfo))]
	[Serializable]
	internal class ResponseInfo : IExtensibleDataObject
	{
		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x06002D9C RID: 11676 RVA: 0x00065F23 File Offset: 0x00064123
		// (set) Token: 0x06002D9D RID: 11677 RVA: 0x00065F2B File Offset: 0x0006412B
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x06002D9E RID: 11678 RVA: 0x00065F34 File Offset: 0x00064134
		// (set) Token: 0x06002D9F RID: 11679 RVA: 0x00065F3C File Offset: 0x0006413C
		[DataMember]
		internal ServiceFault Fault
		{
			get
			{
				return this.FaultField;
			}
			set
			{
				this.FaultField = value;
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06002DA0 RID: 11680 RVA: 0x00065F45 File Offset: 0x00064145
		// (set) Token: 0x06002DA1 RID: 11681 RVA: 0x00065F4D File Offset: 0x0006414D
		[DataMember]
		internal ResponseStatus Status
		{
			get
			{
				return this.StatusField;
			}
			set
			{
				this.StatusField = value;
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06002DA2 RID: 11682 RVA: 0x00065F56 File Offset: 0x00064156
		// (set) Token: 0x06002DA3 RID: 11683 RVA: 0x00065F5E File Offset: 0x0006415E
		[DataMember]
		internal TargetObject Target
		{
			get
			{
				return this.TargetField;
			}
			set
			{
				this.TargetField = value;
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06002DA4 RID: 11684 RVA: 0x00065F67 File Offset: 0x00064167
		// (set) Token: 0x06002DA5 RID: 11685 RVA: 0x00065F6F File Offset: 0x0006416F
		[DataMember]
		internal string[] TargetValue
		{
			get
			{
				return this.TargetValueField;
			}
			set
			{
				this.TargetValueField = value;
			}
		}

		// Token: 0x040027B4 RID: 10164
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x040027B5 RID: 10165
		[OptionalField]
		private ServiceFault FaultField;

		// Token: 0x040027B6 RID: 10166
		[OptionalField]
		private ResponseStatus StatusField;

		// Token: 0x040027B7 RID: 10167
		[OptionalField]
		private TargetObject TargetField;

		// Token: 0x040027B8 RID: 10168
		[OptionalField]
		private string[] TargetValueField;
	}
}
