using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006AD RID: 1709
	[DataContract]
	public class JsonFaultDetail : ErrorInformationBase
	{
		// Token: 0x06004900 RID: 18688 RVA: 0x000DF538 File Offset: 0x000DD738
		public JsonFaultDetail(Exception ex) : base(ex)
		{
			this.ExceptionDetail = new ExceptionDetail(ex);
			this.ExceptionType = ex.GetType().FullName;
			if (!string.IsNullOrEmpty(base.CallStack))
			{
				this.StackTrace = base.CallStack;
				base.CallStack = string.Empty;
			}
			if (!(ex is FaultException) && PowerShellMessageTranslator.ShouldTranslate)
			{
				this.Message = PowerShellMessageTranslator.Instance.Translate(null, ex, Strings.WebServiceErrorMessage);
			}
		}

		// Token: 0x170027C6 RID: 10182
		// (get) Token: 0x06004901 RID: 18689 RVA: 0x000DF5B8 File Offset: 0x000DD7B8
		// (set) Token: 0x06004902 RID: 18690 RVA: 0x000DF5C0 File Offset: 0x000DD7C0
		[DataMember]
		public ExceptionDetail ExceptionDetail { get; set; }

		// Token: 0x170027C7 RID: 10183
		// (get) Token: 0x06004903 RID: 18691 RVA: 0x000DF5C9 File Offset: 0x000DD7C9
		// (set) Token: 0x06004904 RID: 18692 RVA: 0x000DF5D1 File Offset: 0x000DD7D1
		[DataMember]
		public string ExceptionType { get; set; }

		// Token: 0x170027C8 RID: 10184
		// (get) Token: 0x06004905 RID: 18693 RVA: 0x000DF5DA File Offset: 0x000DD7DA
		// (set) Token: 0x06004906 RID: 18694 RVA: 0x000DF5E2 File Offset: 0x000DD7E2
		[DataMember]
		public string StackTrace { get; set; }
	}
}
