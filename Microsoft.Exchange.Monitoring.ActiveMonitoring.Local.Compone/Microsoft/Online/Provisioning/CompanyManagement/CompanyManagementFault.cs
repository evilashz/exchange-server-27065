using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Provisioning.CompanyManagement
{
	// Token: 0x020002A0 RID: 672
	[KnownType(typeof(InvalidServiceInstanceFault))]
	[KnownType(typeof(ServiceUnavailableFault))]
	[KnownType(typeof(CompanyNotFoundFault))]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "CompanyManagementFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Provisioning.CompanyManagement")]
	[KnownType(typeof(BindingRedirectionFault))]
	[KnownType(typeof(DifferentServiceInstanceAlreadyExistsFault))]
	public class CompanyManagementFault : IExtensibleDataObject
	{
		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x00085994 File Offset: 0x00083B94
		// (set) Token: 0x06001323 RID: 4899 RVA: 0x0008599C File Offset: 0x00083B9C
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

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x000859A5 File Offset: 0x00083BA5
		// (set) Token: 0x06001325 RID: 4901 RVA: 0x000859AD File Offset: 0x00083BAD
		[DataMember(IsRequired = true)]
		public TimeSpan BackOffDuration
		{
			get
			{
				return this.BackOffDurationField;
			}
			set
			{
				this.BackOffDurationField = value;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x000859B6 File Offset: 0x00083BB6
		// (set) Token: 0x06001327 RID: 4903 RVA: 0x000859BE File Offset: 0x00083BBE
		[DataMember(IsRequired = true)]
		public bool CanRetry
		{
			get
			{
				return this.CanRetryField;
			}
			set
			{
				this.CanRetryField = value;
			}
		}

		// Token: 0x04000E83 RID: 3715
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000E84 RID: 3716
		private TimeSpan BackOffDurationField;

		// Token: 0x04000E85 RID: 3717
		private bool CanRetryField;
	}
}
