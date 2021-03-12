using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C2B RID: 3115
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "LocatorServiceFault", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class LocatorServiceFault : IExtensibleDataObject
	{
		// Token: 0x1700112A RID: 4394
		// (get) Token: 0x0600444E RID: 17486 RVA: 0x000B6BB6 File Offset: 0x000B4DB6
		// (set) Token: 0x0600444F RID: 17487 RVA: 0x000B6BBE File Offset: 0x000B4DBE
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

		// Token: 0x1700112B RID: 4395
		// (get) Token: 0x06004450 RID: 17488 RVA: 0x000B6BC7 File Offset: 0x000B4DC7
		// (set) Token: 0x06004451 RID: 17489 RVA: 0x000B6BCF File Offset: 0x000B4DCF
		[DataMember]
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

		// Token: 0x1700112C RID: 4396
		// (get) Token: 0x06004452 RID: 17490 RVA: 0x000B6BD8 File Offset: 0x000B4DD8
		// (set) Token: 0x06004453 RID: 17491 RVA: 0x000B6BE0 File Offset: 0x000B4DE0
		[DataMember]
		public int IntErrorCode
		{
			get
			{
				return this.IntErrorCodeField;
			}
			set
			{
				this.IntErrorCodeField = value;
			}
		}

		// Token: 0x1700112D RID: 4397
		// (get) Token: 0x06004454 RID: 17492 RVA: 0x000B6BE9 File Offset: 0x000B4DE9
		public ErrorCode ErrorCode
		{
			get
			{
				return (ErrorCode)this.IntErrorCode;
			}
		}

		// Token: 0x1700112E RID: 4398
		// (get) Token: 0x06004455 RID: 17493 RVA: 0x000B6BF1 File Offset: 0x000B4DF1
		// (set) Token: 0x06004456 RID: 17494 RVA: 0x000B6BF9 File Offset: 0x000B4DF9
		[DataMember]
		public string Message
		{
			get
			{
				return this.MessageField;
			}
			set
			{
				this.MessageField = value;
			}
		}

		// Token: 0x1700112F RID: 4399
		// (get) Token: 0x06004457 RID: 17495 RVA: 0x000B6C02 File Offset: 0x000B4E02
		// (set) Token: 0x06004458 RID: 17496 RVA: 0x000B6C0A File Offset: 0x000B4E0A
		[DataMember]
		public string[] MissingData
		{
			get
			{
				return this.MissingDataField;
			}
			set
			{
				this.MissingDataField = value;
			}
		}

		// Token: 0x040039E5 RID: 14821
		private ExtensionDataObject extensionDataField;

		// Token: 0x040039E6 RID: 14822
		private bool CanRetryField;

		// Token: 0x040039E7 RID: 14823
		private int IntErrorCodeField;

		// Token: 0x040039E8 RID: 14824
		private string MessageField;

		// Token: 0x040039E9 RID: 14825
		private string[] MissingDataField;
	}
}
