using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.BOX.UI.Shell
{
	// Token: 0x02000081 RID: 129
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "Alert", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.UI.Shell")]
	[DebuggerStepThrough]
	public class Alert : IExtensibleDataObject
	{
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0000E75C File Offset: 0x0000C95C
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x0000E764 File Offset: 0x0000C964
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

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0000E76D File Offset: 0x0000C96D
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x0000E775 File Offset: 0x0000C975
		[DataMember]
		public string ActionClick
		{
			get
			{
				return this.ActionClickField;
			}
			set
			{
				this.ActionClickField = value;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0000E77E File Offset: 0x0000C97E
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x0000E786 File Offset: 0x0000C986
		[DataMember]
		public string ActionTarget
		{
			get
			{
				return this.ActionTargetField;
			}
			set
			{
				this.ActionTargetField = value;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0000E78F File Offset: 0x0000C98F
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x0000E797 File Offset: 0x0000C997
		[DataMember]
		public string ActionText
		{
			get
			{
				return this.ActionTextField;
			}
			set
			{
				this.ActionTextField = value;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0000E7A0 File Offset: 0x0000C9A0
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x0000E7A8 File Offset: 0x0000C9A8
		[DataMember]
		public string ActionUrl
		{
			get
			{
				return this.ActionUrlField;
			}
			set
			{
				this.ActionUrlField = value;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0000E7B1 File Offset: 0x0000C9B1
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x0000E7B9 File Offset: 0x0000C9B9
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

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0000E7C2 File Offset: 0x0000C9C2
		// (set) Token: 0x060004D6 RID: 1238 RVA: 0x0000E7CA File Offset: 0x0000C9CA
		[DataMember]
		public AlertPriority Priority
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

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x060004D8 RID: 1240 RVA: 0x0000E7DB File Offset: 0x0000C9DB
		[DataMember]
		public string Title
		{
			get
			{
				return this.TitleField;
			}
			set
			{
				this.TitleField = value;
			}
		}

		// Token: 0x0400029A RID: 666
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400029B RID: 667
		private string ActionClickField;

		// Token: 0x0400029C RID: 668
		private string ActionTargetField;

		// Token: 0x0400029D RID: 669
		private string ActionTextField;

		// Token: 0x0400029E RID: 670
		private string ActionUrlField;

		// Token: 0x0400029F RID: 671
		private string MessageField;

		// Token: 0x040002A0 RID: 672
		private AlertPriority PriorityField;

		// Token: 0x040002A1 RID: 673
		private string TitleField;
	}
}
