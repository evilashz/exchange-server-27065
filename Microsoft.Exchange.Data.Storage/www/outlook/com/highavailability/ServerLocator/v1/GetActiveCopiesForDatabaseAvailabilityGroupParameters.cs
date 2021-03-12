using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace www.outlook.com.highavailability.ServerLocator.v1
{
	// Token: 0x02000D36 RID: 3382
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "GetActiveCopiesForDatabaseAvailabilityGroupParameters", Namespace = "http://www.outlook.com/highavailability/ServerLocator/v1/")]
	[DebuggerStepThrough]
	public class GetActiveCopiesForDatabaseAvailabilityGroupParameters : IExtensibleDataObject
	{
		// Token: 0x17001F7C RID: 8060
		// (get) Token: 0x0600754D RID: 30029 RVA: 0x00208365 File Offset: 0x00206565
		// (set) Token: 0x0600754E RID: 30030 RVA: 0x0020836D File Offset: 0x0020656D
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

		// Token: 0x17001F7D RID: 8061
		// (get) Token: 0x0600754F RID: 30031 RVA: 0x00208376 File Offset: 0x00206576
		// (set) Token: 0x06007550 RID: 30032 RVA: 0x0020837E File Offset: 0x0020657E
		[DataMember]
		public bool CachedData
		{
			get
			{
				return this.CachedDataField;
			}
			set
			{
				this.CachedDataField = value;
			}
		}

		// Token: 0x0400518E RID: 20878
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400518F RID: 20879
		private bool CachedDataField;
	}
}
