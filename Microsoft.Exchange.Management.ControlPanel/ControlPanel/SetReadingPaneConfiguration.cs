using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200009E RID: 158
	[DataContract]
	public class SetReadingPaneConfiguration : SetMessagingConfigurationBase
	{
		// Token: 0x170018CA RID: 6346
		// (get) Token: 0x06001BFB RID: 7163 RVA: 0x00057C08 File Offset: 0x00055E08
		// (set) Token: 0x06001BFC RID: 7164 RVA: 0x00057C1A File Offset: 0x00055E1A
		[DataMember]
		public string PreviewMarkAsReadBehavior
		{
			get
			{
				return (string)base["PreviewMarkAsReadBehavior"];
			}
			set
			{
				base["PreviewMarkAsReadBehavior"] = value;
			}
		}

		// Token: 0x170018CB RID: 6347
		// (get) Token: 0x06001BFD RID: 7165 RVA: 0x00057C28 File Offset: 0x00055E28
		// (set) Token: 0x06001BFE RID: 7166 RVA: 0x00057C3A File Offset: 0x00055E3A
		[DataMember]
		public string EmailComposeMode
		{
			get
			{
				return (string)base["EmailComposeMode"];
			}
			set
			{
				base["EmailComposeMode"] = value;
			}
		}

		// Token: 0x170018CC RID: 6348
		// (get) Token: 0x06001BFF RID: 7167 RVA: 0x00057C48 File Offset: 0x00055E48
		// (set) Token: 0x06001C00 RID: 7168 RVA: 0x00057C50 File Offset: 0x00055E50
		[DataMember]
		public string PreviewMarkAsReadDelaytime { get; set; }

		// Token: 0x06001C01 RID: 7169 RVA: 0x00057C5C File Offset: 0x00055E5C
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (this.PreviewMarkAsReadDelaytime != null)
			{
				int num;
				if (int.TryParse(this.PreviewMarkAsReadDelaytime, out num) && num >= 0 && num <= 30)
				{
					base["PreviewMarkAsReadDelaytime"] = num;
					return;
				}
				if (this.PreviewMarkAsReadBehavior == Microsoft.Exchange.Data.Storage.Management.PreviewMarkAsReadBehavior.Delayed.ToString())
				{
					throw new FaultException(OwaOptionStrings.PreviewMarkAsReadDelaytimeErrorMessage);
				}
			}
		}
	}
}
