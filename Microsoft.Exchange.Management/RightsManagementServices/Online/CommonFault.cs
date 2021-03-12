using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.RightsManagementServices.Online
{
	// Token: 0x02000739 RID: 1849
	[DataContract(Name = "CommonFault", Namespace = "http://microsoft.com/RightsManagementServiceOnline/2011/04")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[Serializable]
	public class CommonFault : IExtensibleDataObject
	{
		// Token: 0x170013F0 RID: 5104
		// (get) Token: 0x06004190 RID: 16784 RVA: 0x0010C7F0 File Offset: 0x0010A9F0
		// (set) Token: 0x06004191 RID: 16785 RVA: 0x0010C7F8 File Offset: 0x0010A9F8
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

		// Token: 0x170013F1 RID: 5105
		// (get) Token: 0x06004192 RID: 16786 RVA: 0x0010C801 File Offset: 0x0010AA01
		// (set) Token: 0x06004193 RID: 16787 RVA: 0x0010C809 File Offset: 0x0010AA09
		[DataMember]
		public bool IsPermanentFailure
		{
			get
			{
				return this.IsPermanentFailureField;
			}
			set
			{
				this.IsPermanentFailureField = value;
			}
		}

		// Token: 0x170013F2 RID: 5106
		// (get) Token: 0x06004194 RID: 16788 RVA: 0x0010C812 File Offset: 0x0010AA12
		// (set) Token: 0x06004195 RID: 16789 RVA: 0x0010C81A File Offset: 0x0010AA1A
		[DataMember(Order = 1)]
		public ServerErrorCode ErrorCode
		{
			get
			{
				return this.ErrorCodeField;
			}
			set
			{
				this.ErrorCodeField = value;
			}
		}

		// Token: 0x170013F3 RID: 5107
		// (get) Token: 0x06004196 RID: 16790 RVA: 0x0010C823 File Offset: 0x0010AA23
		// (set) Token: 0x06004197 RID: 16791 RVA: 0x0010C82B File Offset: 0x0010AA2B
		[DataMember(Order = 2)]
		public Guid CorrelationId
		{
			get
			{
				return this.CorrelationIdField;
			}
			set
			{
				if (!this.CorrelationIdField.Equals(value))
				{
					this.CorrelationIdField = value;
					this.RaisePropertyChanged("CorrelationId");
				}
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06004198 RID: 16792 RVA: 0x0010C850 File Offset: 0x0010AA50
		// (remove) Token: 0x06004199 RID: 16793 RVA: 0x0010C888 File Offset: 0x0010AA88
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600419A RID: 16794 RVA: 0x0010C8C0 File Offset: 0x0010AAC0
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x0400295A RID: 10586
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400295B RID: 10587
		private bool IsPermanentFailureField;

		// Token: 0x0400295C RID: 10588
		private ServerErrorCode ErrorCodeField;

		// Token: 0x0400295D RID: 10589
		[OptionalField]
		private Guid CorrelationIdField;
	}
}
