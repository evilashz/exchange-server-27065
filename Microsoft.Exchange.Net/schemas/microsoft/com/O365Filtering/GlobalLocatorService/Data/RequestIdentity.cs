using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C20 RID: 3104
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "RequestIdentity", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class RequestIdentity : IExtensibleDataObject
	{
		// Token: 0x1700110E RID: 4366
		// (get) Token: 0x0600440B RID: 17419 RVA: 0x000B6982 File Offset: 0x000B4B82
		// (set) Token: 0x0600440C RID: 17420 RVA: 0x000B698A File Offset: 0x000B4B8A
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

		// Token: 0x1700110F RID: 4367
		// (get) Token: 0x0600440D RID: 17421 RVA: 0x000B6993 File Offset: 0x000B4B93
		// (set) Token: 0x0600440E RID: 17422 RVA: 0x000B699B File Offset: 0x000B4B9B
		[DataMember(IsRequired = true)]
		public string CallerId
		{
			get
			{
				return this.CallerIdField;
			}
			set
			{
				this.CallerIdField = value;
			}
		}

		// Token: 0x17001110 RID: 4368
		// (get) Token: 0x0600440F RID: 17423 RVA: 0x000B69A4 File Offset: 0x000B4BA4
		// (set) Token: 0x06004410 RID: 17424 RVA: 0x000B69AC File Offset: 0x000B4BAC
		[DataMember]
		public Guid RequestTrackingGuid
		{
			get
			{
				return this.RequestTrackingGuidField;
			}
			set
			{
				this.RequestTrackingGuidField = value;
			}
		}

		// Token: 0x17001111 RID: 4369
		// (get) Token: 0x06004411 RID: 17425 RVA: 0x000B69B5 File Offset: 0x000B4BB5
		// (set) Token: 0x06004412 RID: 17426 RVA: 0x000B69BD File Offset: 0x000B4BBD
		[DataMember]
		public Guid TrackingGuid
		{
			get
			{
				return this.TrackingGuidField;
			}
			set
			{
				this.TrackingGuidField = value;
			}
		}

		// Token: 0x040039C9 RID: 14793
		private ExtensionDataObject extensionDataField;

		// Token: 0x040039CA RID: 14794
		private string CallerIdField;

		// Token: 0x040039CB RID: 14795
		private Guid RequestTrackingGuidField;

		// Token: 0x040039CC RID: 14796
		private Guid TrackingGuidField;
	}
}
