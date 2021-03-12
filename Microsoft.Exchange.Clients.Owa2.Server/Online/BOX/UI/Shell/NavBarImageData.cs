using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.BOX.UI.Shell
{
	// Token: 0x0200007D RID: 125
	[DataContract(Name = "NavBarImageData", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.UI.Shell")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class NavBarImageData : IExtensibleDataObject
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0000E63D File Offset: 0x0000C83D
		// (set) Token: 0x060004A8 RID: 1192 RVA: 0x0000E645 File Offset: 0x0000C845
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

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0000E64E File Offset: 0x0000C84E
		// (set) Token: 0x060004AA RID: 1194 RVA: 0x0000E656 File Offset: 0x0000C856
		[DataMember]
		public string AltText
		{
			get
			{
				return this.AltTextField;
			}
			set
			{
				this.AltTextField = value;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0000E65F File Offset: 0x0000C85F
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x0000E667 File Offset: 0x0000C867
		[DataMember]
		public NavBarImageClipInfo ClipInfo
		{
			get
			{
				return this.ClipInfoField;
			}
			set
			{
				this.ClipInfoField = value;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0000E670 File Offset: 0x0000C870
		// (set) Token: 0x060004AE RID: 1198 RVA: 0x0000E678 File Offset: 0x0000C878
		[DataMember]
		public NavBarImageClipInfo HoverClipInfo
		{
			get
			{
				return this.HoverClipInfoField;
			}
			set
			{
				this.HoverClipInfoField = value;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x0000E681 File Offset: 0x0000C881
		// (set) Token: 0x060004B0 RID: 1200 RVA: 0x0000E689 File Offset: 0x0000C889
		[DataMember]
		public NavBarImageClipInfo PressedClipInfo
		{
			get
			{
				return this.PressedClipInfoField;
			}
			set
			{
				this.PressedClipInfoField = value;
			}
		}

		// Token: 0x0400028B RID: 651
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400028C RID: 652
		private string AltTextField;

		// Token: 0x0400028D RID: 653
		private NavBarImageClipInfo ClipInfoField;

		// Token: 0x0400028E RID: 654
		private NavBarImageClipInfo HoverClipInfoField;

		// Token: 0x0400028F RID: 655
		private NavBarImageClipInfo PressedClipInfoField;
	}
}
