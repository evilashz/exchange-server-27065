using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace www.outlook.com.highavailability.ServerLocator.v1
{
	// Token: 0x02000D37 RID: 3383
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "DatabaseServerInformationFault", Namespace = "http://www.outlook.com/highavailability/ServerLocator/v1/")]
	public class DatabaseServerInformationFault : IExtensibleDataObject
	{
		// Token: 0x17001F7E RID: 8062
		// (get) Token: 0x06007552 RID: 30034 RVA: 0x0020838F File Offset: 0x0020658F
		// (set) Token: 0x06007553 RID: 30035 RVA: 0x00208397 File Offset: 0x00206597
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

		// Token: 0x17001F7F RID: 8063
		// (get) Token: 0x06007554 RID: 30036 RVA: 0x002083A0 File Offset: 0x002065A0
		// (set) Token: 0x06007555 RID: 30037 RVA: 0x002083A8 File Offset: 0x002065A8
		[DataMember]
		public DatabaseServerInformationType ErrorCode
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

		// Token: 0x17001F80 RID: 8064
		// (get) Token: 0x06007556 RID: 30038 RVA: 0x002083B1 File Offset: 0x002065B1
		// (set) Token: 0x06007557 RID: 30039 RVA: 0x002083B9 File Offset: 0x002065B9
		[DataMember]
		public string Type
		{
			get
			{
				return this.TypeField;
			}
			set
			{
				this.TypeField = value;
			}
		}

		// Token: 0x17001F81 RID: 8065
		// (get) Token: 0x06007558 RID: 30040 RVA: 0x002083C2 File Offset: 0x002065C2
		// (set) Token: 0x06007559 RID: 30041 RVA: 0x002083CA File Offset: 0x002065CA
		[DataMember(Order = 2)]
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

		// Token: 0x17001F82 RID: 8066
		// (get) Token: 0x0600755A RID: 30042 RVA: 0x002083D3 File Offset: 0x002065D3
		// (set) Token: 0x0600755B RID: 30043 RVA: 0x002083DB File Offset: 0x002065DB
		[DataMember(Order = 3)]
		public string StackTrace
		{
			get
			{
				return this.StackTraceField;
			}
			set
			{
				this.StackTraceField = value;
			}
		}

		// Token: 0x04005190 RID: 20880
		private ExtensionDataObject extensionDataField;

		// Token: 0x04005191 RID: 20881
		private DatabaseServerInformationType ErrorCodeField;

		// Token: 0x04005192 RID: 20882
		private string TypeField;

		// Token: 0x04005193 RID: 20883
		private string MessageField;

		// Token: 0x04005194 RID: 20884
		private string StackTraceField;
	}
}
