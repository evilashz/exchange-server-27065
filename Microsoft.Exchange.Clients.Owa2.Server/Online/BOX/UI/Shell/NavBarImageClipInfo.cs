using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.BOX.UI.Shell
{
	// Token: 0x02000080 RID: 128
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "NavBarImageClipInfo", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.UI.Shell")]
	public class NavBarImageClipInfo : IExtensibleDataObject
	{
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x0000E6FF File Offset: 0x0000C8FF
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x0000E707 File Offset: 0x0000C907
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

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0000E710 File Offset: 0x0000C910
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x0000E718 File Offset: 0x0000C918
		[DataMember]
		public int Height
		{
			get
			{
				return this.HeightField;
			}
			set
			{
				this.HeightField = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0000E721 File Offset: 0x0000C921
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x0000E729 File Offset: 0x0000C929
		[DataMember]
		public int Width
		{
			get
			{
				return this.WidthField;
			}
			set
			{
				this.WidthField = value;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x0000E732 File Offset: 0x0000C932
		// (set) Token: 0x060004C5 RID: 1221 RVA: 0x0000E73A File Offset: 0x0000C93A
		[DataMember]
		public int X
		{
			get
			{
				return this.XField;
			}
			set
			{
				this.XField = value;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0000E743 File Offset: 0x0000C943
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x0000E74B File Offset: 0x0000C94B
		[DataMember]
		public int Y
		{
			get
			{
				return this.YField;
			}
			set
			{
				this.YField = value;
			}
		}

		// Token: 0x04000295 RID: 661
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000296 RID: 662
		private int HeightField;

		// Token: 0x04000297 RID: 663
		private int WidthField;

		// Token: 0x04000298 RID: 664
		private int XField;

		// Token: 0x04000299 RID: 665
		private int YField;
	}
}
