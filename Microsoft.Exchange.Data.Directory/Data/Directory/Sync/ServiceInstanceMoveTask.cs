using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008A9 RID: 2217
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class ServiceInstanceMoveTask
	{
		// Token: 0x17002722 RID: 10018
		// (get) Token: 0x06006E11 RID: 28177 RVA: 0x00176059 File Offset: 0x00174259
		// (set) Token: 0x06006E12 RID: 28178 RVA: 0x00176061 File Offset: 0x00174261
		[XmlAttribute]
		public string ContextId
		{
			get
			{
				return this.contextIdField;
			}
			set
			{
				this.contextIdField = value;
			}
		}

		// Token: 0x17002723 RID: 10019
		// (get) Token: 0x06006E13 RID: 28179 RVA: 0x0017606A File Offset: 0x0017426A
		// (set) Token: 0x06006E14 RID: 28180 RVA: 0x00176072 File Offset: 0x00174272
		[XmlAttribute]
		public string TaskId
		{
			get
			{
				return this.taskIdField;
			}
			set
			{
				this.taskIdField = value;
			}
		}

		// Token: 0x17002724 RID: 10020
		// (get) Token: 0x06006E15 RID: 28181 RVA: 0x0017607B File Offset: 0x0017427B
		// (set) Token: 0x06006E16 RID: 28182 RVA: 0x00176083 File Offset: 0x00174283
		[XmlAttribute]
		public string OldServiceInstance
		{
			get
			{
				return this.oldServiceInstanceField;
			}
			set
			{
				this.oldServiceInstanceField = value;
			}
		}

		// Token: 0x17002725 RID: 10021
		// (get) Token: 0x06006E17 RID: 28183 RVA: 0x0017608C File Offset: 0x0017428C
		// (set) Token: 0x06006E18 RID: 28184 RVA: 0x00176094 File Offset: 0x00174294
		[XmlAttribute]
		public string NewServiceInstance
		{
			get
			{
				return this.newServiceInstanceField;
			}
			set
			{
				this.newServiceInstanceField = value;
			}
		}

		// Token: 0x17002726 RID: 10022
		// (get) Token: 0x06006E19 RID: 28185 RVA: 0x0017609D File Offset: 0x0017429D
		// (set) Token: 0x06006E1A RID: 28186 RVA: 0x001760A5 File Offset: 0x001742A5
		[XmlAttribute]
		public ServiceInstanceMoveTaskStatusCode StatusCode
		{
			get
			{
				return this.statusCodeField;
			}
			set
			{
				this.statusCodeField = value;
			}
		}

		// Token: 0x17002727 RID: 10023
		// (get) Token: 0x06006E1B RID: 28187 RVA: 0x001760AE File Offset: 0x001742AE
		// (set) Token: 0x06006E1C RID: 28188 RVA: 0x001760B6 File Offset: 0x001742B6
		[XmlAttribute]
		public string TaskFailureReason
		{
			get
			{
				return this.taskFailureReasonField;
			}
			set
			{
				this.taskFailureReasonField = value;
			}
		}

		// Token: 0x040047A6 RID: 18342
		private string contextIdField;

		// Token: 0x040047A7 RID: 18343
		private string taskIdField;

		// Token: 0x040047A8 RID: 18344
		private string oldServiceInstanceField;

		// Token: 0x040047A9 RID: 18345
		private string newServiceInstanceField;

		// Token: 0x040047AA RID: 18346
		private ServiceInstanceMoveTaskStatusCode statusCodeField;

		// Token: 0x040047AB RID: 18347
		private string taskFailureReasonField;
	}
}
