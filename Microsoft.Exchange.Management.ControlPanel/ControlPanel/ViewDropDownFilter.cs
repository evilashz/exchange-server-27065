using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000E4 RID: 228
	[DataContract]
	public abstract class ViewDropDownFilter : SearchTextFilter
	{
		// Token: 0x170019A0 RID: 6560
		// (get) Token: 0x06001E29 RID: 7721 RVA: 0x0005B456 File Offset: 0x00059656
		// (set) Token: 0x06001E2A RID: 7722 RVA: 0x0005B45E File Offset: 0x0005965E
		[DataMember]
		public string SelectedView
		{
			get
			{
				return this.selectedView;
			}
			set
			{
				this.selectedView = value;
			}
		}

		// Token: 0x04001C06 RID: 7174
		private string selectedView;
	}
}
