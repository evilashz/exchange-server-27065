using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008A0 RID: 2208
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "BindingRedirectionFault", Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
	public class BindingRedirectionFault : IExtensibleDataObject
	{
		// Token: 0x1700271C RID: 10012
		// (get) Token: 0x06006DE2 RID: 28130 RVA: 0x00175F3C File Offset: 0x0017413C
		// (set) Token: 0x06006DE3 RID: 28131 RVA: 0x00175F44 File Offset: 0x00174144
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

		// Token: 0x1700271D RID: 10013
		// (get) Token: 0x06006DE4 RID: 28132 RVA: 0x00175F4D File Offset: 0x0017414D
		// (set) Token: 0x06006DE5 RID: 28133 RVA: 0x00175F55 File Offset: 0x00174155
		[DataMember(EmitDefaultValue = false)]
		public string Location
		{
			get
			{
				return this.LocationField;
			}
			set
			{
				this.LocationField = value;
			}
		}

		// Token: 0x0400478E RID: 18318
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400478F RID: 18319
		private string LocationField;
	}
}
