using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000149 RID: 329
	[DataContract]
	public class FormSettings
	{
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x00022995 File Offset: 0x00020B95
		// (set) Token: 0x06000AE5 RID: 2789 RVA: 0x0002299D File Offset: 0x00020B9D
		[DataMember]
		public FormSettings.FormSettingsType SettingsType { get; set; }

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x000229A6 File Offset: 0x00020BA6
		// (set) Token: 0x06000AE7 RID: 2791 RVA: 0x000229AE File Offset: 0x00020BAE
		[DataMember]
		public string SourceLocation { get; set; }

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x000229B7 File Offset: 0x00020BB7
		// (set) Token: 0x06000AE9 RID: 2793 RVA: 0x000229BF File Offset: 0x00020BBF
		[DataMember(EmitDefaultValue = false)]
		public int RequestedHeight { get; set; }

		// Token: 0x0200014A RID: 330
		[DataContract]
		public enum FormSettingsType
		{
			// Token: 0x040006CF RID: 1743
			[EnumMember]
			ItemRead,
			// Token: 0x040006D0 RID: 1744
			[EnumMember]
			ItemEdit
		}
	}
}
