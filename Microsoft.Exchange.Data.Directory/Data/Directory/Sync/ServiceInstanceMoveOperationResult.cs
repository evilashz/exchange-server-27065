using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008AB RID: 2219
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class ServiceInstanceMoveOperationResult
	{
		// Token: 0x17002728 RID: 10024
		// (get) Token: 0x06006E1E RID: 28190 RVA: 0x001760C7 File Offset: 0x001742C7
		// (set) Token: 0x06006E1F RID: 28191 RVA: 0x001760CF File Offset: 0x001742CF
		[XmlElement(IsNullable = true, Order = 0)]
		public ServiceInstanceMoveTask ServiceInstanceMoveTask
		{
			get
			{
				return this.serviceInstanceMoveTaskField;
			}
			set
			{
				this.serviceInstanceMoveTaskField = value;
			}
		}

		// Token: 0x17002729 RID: 10025
		// (get) Token: 0x06006E20 RID: 28192 RVA: 0x001760D8 File Offset: 0x001742D8
		// (set) Token: 0x06006E21 RID: 28193 RVA: 0x001760E0 File Offset: 0x001742E0
		[XmlAttribute]
		public int OperationStatusCode
		{
			get
			{
				return this.operationStatusCodeField;
			}
			set
			{
				this.operationStatusCodeField = value;
			}
		}

		// Token: 0x1700272A RID: 10026
		// (get) Token: 0x06006E22 RID: 28194 RVA: 0x001760E9 File Offset: 0x001742E9
		// (set) Token: 0x06006E23 RID: 28195 RVA: 0x001760F1 File Offset: 0x001742F1
		[XmlAttribute]
		public string ErrorMessage
		{
			get
			{
				return this.errorMessageField;
			}
			set
			{
				this.errorMessageField = value;
			}
		}

		// Token: 0x040047B1 RID: 18353
		private ServiceInstanceMoveTask serviceInstanceMoveTaskField;

		// Token: 0x040047B2 RID: 18354
		private int operationStatusCodeField;

		// Token: 0x040047B3 RID: 18355
		private string errorMessageField;
	}
}
