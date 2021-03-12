using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000E7 RID: 231
	[DataContract]
	public abstract class SetGroupBase : SetObjectProperties
	{
		// Token: 0x170019A5 RID: 6565
		// (get) Token: 0x06001E37 RID: 7735 RVA: 0x0005B6A0 File Offset: 0x000598A0
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-Group";
			}
		}

		// Token: 0x170019A6 RID: 6566
		// (get) Token: 0x06001E38 RID: 7736 RVA: 0x0005B6A7 File Offset: 0x000598A7
		// (set) Token: 0x06001E39 RID: 7737 RVA: 0x0005B6B9 File Offset: 0x000598B9
		[DataMember]
		public string Notes
		{
			get
			{
				return (string)base[WindowsGroupSchema.Notes];
			}
			set
			{
				base[WindowsGroupSchema.Notes] = value;
			}
		}
	}
}
