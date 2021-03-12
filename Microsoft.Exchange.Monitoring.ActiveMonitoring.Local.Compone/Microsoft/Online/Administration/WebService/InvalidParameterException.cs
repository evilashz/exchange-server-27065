using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000391 RID: 913
	[KnownType(typeof(InvalidUserLicenseException))]
	[KnownType(typeof(TenantNotPartnerTypeException))]
	[KnownType(typeof(LicenseQuotaExceededException))]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "InvalidParameterException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[KnownType(typeof(InvalidLicenseConfigurationException))]
	[KnownType(typeof(InvalidUserLicenseOptionException))]
	[KnownType(typeof(InvalidSubscriptionStatusException))]
	public class InvalidParameterException : MsolAdministrationException
	{
		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x0008C055 File Offset: 0x0008A255
		// (set) Token: 0x06001672 RID: 5746 RVA: 0x0008C05D File Offset: 0x0008A25D
		[DataMember]
		public string ParameterName
		{
			get
			{
				return this.ParameterNameField;
			}
			set
			{
				this.ParameterNameField = value;
			}
		}

		// Token: 0x0400100B RID: 4107
		private string ParameterNameField;
	}
}
