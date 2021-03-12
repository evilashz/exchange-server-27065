using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services
{
	// Token: 0x02000855 RID: 2133
	[DataContract(Name = "SmtpProfileEntry", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[Serializable]
	internal class SmtpProfileEntry : IExtensibleDataObject
	{
		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06002D8A RID: 11658 RVA: 0x00065E8C File Offset: 0x0006408C
		// (set) Token: 0x06002D8B RID: 11659 RVA: 0x00065E94 File Offset: 0x00064094
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

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06002D8C RID: 11660 RVA: 0x00065E9D File Offset: 0x0006409D
		// (set) Token: 0x06002D8D RID: 11661 RVA: 0x00065EA5 File Offset: 0x000640A5
		[DataMember]
		internal string IP
		{
			get
			{
				return this.IPField;
			}
			set
			{
				this.IPField = value;
			}
		}

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06002D8E RID: 11662 RVA: 0x00065EAE File Offset: 0x000640AE
		// (set) Token: 0x06002D8F RID: 11663 RVA: 0x00065EB6 File Offset: 0x000640B6
		[DataMember]
		internal int Priority
		{
			get
			{
				return this.PriorityField;
			}
			set
			{
				this.PriorityField = value;
			}
		}

		// Token: 0x040027AD RID: 10157
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x040027AE RID: 10158
		[OptionalField]
		private string IPField;

		// Token: 0x040027AF RID: 10159
		[OptionalField]
		private int PriorityField;
	}
}
